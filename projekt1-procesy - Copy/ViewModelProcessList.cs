using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using samotnik;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

namespace a;

public class ViewModelProcessList : INotifyPropertyChanged {

	public ViewModelProcessList() {
		CommandFilter = new CommandRefresh(this);
		ClickAutoRefreshButton = new AutoRefreshToggle(this);
		CommandRefresh = new CommandRefresh(this);
		CommandSort = new CommandSort(this);
		CommandKill = new CommandKill(this);
		CommandPriorityChange = new CommandPriorityChange(this);
			
		Processes = new ObservableCollection<Process>();
		ChildProcesses = new ObservableCollection<Process>();
		SortProcesses("Id");
		
		UpdateList();

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
		var w = Application.Current?.Dispatcher;
		if(w == null)
			return true;
		return false;
	}
	

	public void UpdateProcessPriority() {
		if(process != null) {
			try {
				switch(process.PriorityClass) {
					case ProcessPriorityClass.Idle:
						ProcessPriorityText = "Idle";
						break;
					case ProcessPriorityClass.BelowNormal:
						ProcessPriorityText = "BelowNormal";
						break;
					case ProcessPriorityClass.Normal:
						ProcessPriorityText = "Normal";
						break;
					case ProcessPriorityClass.AboveNormal:
						ProcessPriorityText = "AboveNormal";
						break;
					case ProcessPriorityClass.High:
						ProcessPriorityText = "High";
						break;
					case ProcessPriorityClass.RealTime:
						ProcessPriorityText = "RealTime";
						break;
					default:
						ProcessPriorityText = "[INVALID]";
						break;
				}
			} catch {
				ProcessPriorityText = "[ACCESS DENIED]";
			}
		} else {
			ProcessPriorityText = "";
		}
	}
	
	
    public void SortProcesses(string columnName)
    {
        SortDescription newSortDescription = new SortDescription(columnName, ListSortDirection.Ascending);
        ;
        if (ProcessesCollection.SortDescriptions.Count > 0)
        {
            SortDescription oldSortDescription = ProcessesCollection.SortDescriptions[0];
            if (oldSortDescription.PropertyName.Equals(columnName))
            {
                newSortDescription = new SortDescription(
                    columnName,
                    oldSortDescription.Direction == ListSortDirection.Ascending
                        ? ListSortDirection.Descending
                        : ListSortDirection.Ascending
                );
            }
        }

        ProcessesCollection.SortDescriptions.Clear();
        ProcessesCollection.SortDescriptions.Add(newSortDescription);
        
        UpdateList(false);
    }


    public void UpdateList(bool fetchList = false) {
		Application.Current.Dispatcher.BeginInvoke(
			DispatcherPriority.Normal,
			() => {
				TriggerPorpertyChange("ProcessesCollection");
				if(fetchList || allProcesses == null ) {
					allProcesses = Process.GetProcesses();
				}
				Processes.Clear();
				foreach(var process in allProcesses) {
					Processes.Add(process);
				}
				TriggerPorpertyChange("Processes");
				TriggerPorpertyChange("ProcessesCollection");

				UpdateSelectedProcessData();
			});
	}

    void UpdateSelectedProcessData() {
	    if(Process != null) {
		    Process = Process.GetProcessById(Process.Id);
	    }

	    UpdateProcessPriority();
		 ChildProcesses.Clear();
		 if(Process != null) {
			  foreach(var p in ProcessSelect.GetChildrenProcesses(Process)) {
				  ChildProcesses.Add(p);
			  }
		 }
		 TriggerPorpertyChange("ChildProcesses");
		 TriggerPorpertyChange("ChildProcessesCollection");
    }


	public Process? process = null;

	public Process? Process {
		get {
			return process;
		}
		set {
			if(value != null) {
				process = value;
			}
			TriggerPorpertyChange("Process");
		}
	}


	private Process[] allProcesses;





	public ObservableCollection<Process> Processes { get; set; }
	public CollectionView ProcessesCollection {
		get {
			return (CollectionView)CollectionViewSource.GetDefaultView(Processes);
		}
	}

	

	public ObservableCollection<Process> ChildProcesses { get; set; }
	public CollectionView ChildProcessesCollection {
		get {
			return (CollectionView)CollectionViewSource.GetDefaultView(ChildProcesses);
		}
	}



	

	private int refreshRate = 1;
	public int RefreshRateSeconds {
		get {
			return refreshRate;
		}
		set {
			refreshRate = value;
			OnPropertyChanged("AutoRefreshText");
			UpdateList();
		}
	}

	

	private string processPriority = "";

	public string ProcessPriorityText {
		get {
			return processPriority;
		}
		set {
			processPriority = value;
			OnPropertyChanged("ProcessPriorityText");
		}
	}
	
	
	
	private string processFilter = "";
	public string ProcessFilter {
		get {
			return processFilter;
		}
		set {
			processFilter = value;

			ProcessesCollection.Filter = (object o) => {
				if(o is Process p) {
					return p.ProcessName.ToLower()
						       .Contains(processFilter.ToLower())
					       || processFilter == "";
				}
				return false;
			};
		}
	}


	private bool autoRefresh = true;

	public bool AutoRefresh {
		get {
			return autoRefresh;
		}
		set {
			autoRefresh = value;
			OnPropertyChanged("AutoRefresh");
		}
	}


	/*
	public string ChosenProcessName {
		get {
			return this.process?.ProcessName;
		}
	}
	*/
	
	public ICommand CommandFilter { get; set; }
	public ICommand ClickAutoRefreshButton { get; set; }
	public ICommand CommandRefresh { get; set; }
	public ICommand CommandSort { get; set; }
	public ICommand CommandKill { get; set; }
	public ICommand CommandPriorityChange { get; set; }
	
	
	
	

	public void TriggerPorpertyChange(string name) {
		OnPropertyChanged(name);
	}
	
	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void OnPropertyChanged(
		[CallerMemberName] string? propertyName = null) {
		PropertyChanged?.Invoke(this,
			new PropertyChangedEventArgs(propertyName));
	}

	protected bool SetField<T>(ref T field, T value,
		[CallerMemberName] string? propertyName = null) {
		if(EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}
}

