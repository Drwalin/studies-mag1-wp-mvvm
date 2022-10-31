using System;

namespace a; 

public class CommandKill : ListViewCommandBase {
	
	public CommandKill(ViewModelProcessList viewModelProcessList) : base(viewModelProcessList) {
	}

	public override void Execute(object? parameter) {
		viewModelProcessList.Process?.Kill();
		viewModelProcessList.process = null;
		viewModelProcessList.Process = null;
		viewModelProcessList.UpdateList();
	}

	public virtual bool CanExecute(object? parameter) {
		return viewModelProcessList.Process != null;
	}
}
