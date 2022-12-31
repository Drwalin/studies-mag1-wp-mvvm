using System;
using System.Windows;

namespace a; 

public class CommandKill : ListViewCommandBase {
	
	public CommandKill(ViewModelProcessList viewModelProcessList) : base(viewModelProcessList) {
	}

	public override void Execute(object? parameter) {
		try {
			viewModelProcessList.Process?.Kill();
			viewModelProcessList.process = null;
			viewModelProcessList.Process = null;
			viewModelProcessList.UpdateList();
		} catch {
			MessageBox.Show(Application.Current.MainWindow,
				"Access denied to kill: "
				+ viewModelProcessList.Process?.ProcessName);
		}
	}
}
