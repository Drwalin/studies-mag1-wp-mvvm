using System;

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
		}
	}
}
