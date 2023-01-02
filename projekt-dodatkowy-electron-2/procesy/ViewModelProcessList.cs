using System.Diagnostics;
using System.Collections.Generic;

namespace a;

public class ViewModelProcessList {

	public Process? process = null;
	private Process[]? allProcesses = null;
	private List<Process> selectedProcesses = new();
	private List<Process> childProcesses = new();
	private int refreshRate = 1;
	private string processPriority = "";
	public string processFilter = "";
	public bool autoRefresh = true;

	public string sortColumn = "";
	public int revertSortProcess = 1;


	public ViewModelProcessList() {
		Con.connection.On("command_refresh_trigger", ()=>{
			new CommandRefresh(this).Execute(null);
		});
		Con.connection.On("command_auto_refresh_toggle", ()=>{
			new AutoRefreshToggle(this).Execute(null);
		});
		Con.connection.On<string>("command_sort", (p)=>{
			new CommandSort(this).Execute(p);
		});
		Con.connection.On("command_kill", ()=>{
			new CommandKill(this).Execute(null);
		});
		Con.connection.On("command_priority_change", ()=>{
			new CommandPriorityChange(this).Execute(null);
		});
		Con.connection.On<string>("command_filter", (filter)=>{
			processFilter = filter;
			UpdateList(false);
		});

		Con.connection.On<long>("choose_process", (pid)=>{
			if(allProcesses == null)
				return;
			foreach(Process proc in allProcesses) {
				if(proc.Id == pid) {
					process = proc;
					break;
				}
			}
			UpdateSelectedProcessData();
		});

		Con.connection.On<int>("refresh_rate", (value)=>{
			refreshRate = value;
		});
			
		SortProcesses("PID");

		new Thread(() => {
		   Thread.Sleep(millisecondsTimeout: 3000);
			while(!IsStop()) {
				while(autoRefresh == false) {
					 Thread.Sleep(millisecondsTimeout: 1000);
					 if(IsStop()) return;
				}
				UpdateList(true);
				for(int i = 0; i < this.refreshRate; ++i) {
					 Thread.Sleep(millisecondsTimeout: 1000);
					 if(IsStop()) return;
				}
			}
		}).Start();
	}

	public bool IsStop() {
		return !Con.IsConnected();
	}
	

	public void UpdateProcessPriority() {
		if(process != null) {
			try {
				switch(process.PriorityClass) {
					case ProcessPriorityClass.Idle:
						processPriority = "Idle";
						break;
					case ProcessPriorityClass.BelowNormal:
						processPriority = "BelowNormal";
						break;
					case ProcessPriorityClass.Normal:
						processPriority = "Normal";
						break;
					case ProcessPriorityClass.AboveNormal:
						processPriority = "AboveNormal";
						break;
					case ProcessPriorityClass.High:
						processPriority = "High";
						break;
					case ProcessPriorityClass.RealTime:
						processPriority = "RealTime";
						break;
					default:
						processPriority = "[INVALID]";
						break;
				}
			} catch {
				processPriority = "[ACCESS DENIED]";
			}
		} else {
			processPriority = "";
		}
		Con.connection.Send("process_priority", processPriority);
	}
	
    public void SortProcesses(string columnName) {
		if(sortColumn == columnName) {
			revertSortProcess *= -1;
		} else {
			revertSortProcess = 1;
		}
		sortColumn = columnName;
        UpdateList(false);
    }


    public void UpdateList(bool fetchList = false) {
		lock(this) {
			if(fetchList || allProcesses == null) {
				allProcesses = ProcessSelect.GetProcesses();
			}
			selectedProcesses.Clear();
			foreach(Process p in allProcesses) {
				if(p.ProcessName.ToLower().Contains(processFilter.ToLower())) {
					selectedProcesses.Add(p);
				}
			}

			Sort(selectedProcesses, sortColumn, revertSortProcess);
			SendProcessesList("update_main_process_list", this.selectedProcesses);
		}

		UpdateSelectedProcessData();
	}

	static void Sort(List<Process> processes, string sortColumn, int revertSortProcess) {
		if(sortColumn == "Name") {
			processes.Sort((Process l, Process r)=>{
				return revertSortProcess*l.ProcessName.CompareTo(r.ProcessName);
			});
		} else if(sortColumn == "Memory") {
			processes.Sort((Process l, Process r)=>{
				return revertSortProcess * l.PagedMemorySize64.CompareTo(r.PagedMemorySize64);
			});
		} else if(sortColumn == "Time") {
			processes.Sort((Process l, Process r)=>{
				TimeSpan a=new(), b=new();
				try {
					a = l.TotalProcessorTime;
				} catch {}
				try {
					b = r.TotalProcessorTime;
				} catch {}
				return revertSortProcess*a.CompareTo(b);
			});
		} else { // PID or unknown column
			processes.Sort((Process l, Process r)=>{
				return revertSortProcess*l.Id.CompareTo(r.Id);
			});
		}

	}

    void UpdateSelectedProcessData() {
		lock(this) {
			if(process != null) { // fetch process for it's updates
				process = Process.GetProcessById(process.Id);
			}

			UpdateProcessPriority();
			childProcesses.Clear();
			if(process != null) {
				foreach(var p in ProcessSelect.GetChildrenProcesses(process)) {
					childProcesses.Add(p);
				}
			}

			Con.connection.Send("update_selected_process", new ProcessSimple(process));
			SendProcessesList("update_child_process_list", childProcesses);
		}
    }



	struct ProcessSimple {
		public int pid = -1;
		public string name = "";
		public double time = -1;
		public long memory = -1;
		public bool empty = true;
		public ProcessSimple(Process? proc) {
			if(proc != null) {
				empty = false;
				try {
					time = proc.TotalProcessorTime.TotalSeconds;
				} catch {}
				pid = proc.Id;
				name = proc.ProcessName;
				memory = proc.PagedMemorySize64;
			}
		}
	}
	void SendProcessesList(string channel, List<Process> processes) {
		var lst = ConvertProcessesList(processes);
		Con.connection.Send(channel, lst);
	}
	ProcessSimple[] ConvertProcessesList(List<Process> processes) {
		var lst = new ProcessSimple[processes.Count];
		for(int i=0; i<processes.Count; ++i) {
			lst[i] = new ProcessSimple(processes[i]);
		}
		return lst;
	}

}

