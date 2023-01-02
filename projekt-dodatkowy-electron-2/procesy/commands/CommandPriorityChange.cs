using System;
using System.Diagnostics;
using System.Windows;

namespace a; 

public class CommandPriorityChange : ListViewCommandBase {
	
	public CommandPriorityChange(ViewModelProcessList viewModelProcessList) : base(viewModelProcessList) {
	}

	public override void Execute(object? parameter) {
		if(viewModelProcessList.process != null) {
			try {
				var p = viewModelProcessList.process;
				switch(p.PriorityClass) {
					case ProcessPriorityClass.Idle:
						p.PriorityClass = ProcessPriorityClass.BelowNormal;
						break;
					case ProcessPriorityClass.BelowNormal:
						p.PriorityClass = ProcessPriorityClass.Normal;
						break;
					case ProcessPriorityClass.Normal:
						p.PriorityClass = ProcessPriorityClass.AboveNormal;
						break;
					case ProcessPriorityClass.AboveNormal:
						p.PriorityClass = ProcessPriorityClass.High;
						break;
					case ProcessPriorityClass.High:
						p.PriorityClass = ProcessPriorityClass.RealTime;
						break;
					case ProcessPriorityClass.RealTime:
						p.PriorityClass = ProcessPriorityClass.Idle;
						break;
				}

				viewModelProcessList.UpdateProcessPriority();
			} catch {
				MessageBox.Show("Access denied to change priority: "
					+ viewModelProcessList.process?.ProcessName);
			}
		}
	}
}
