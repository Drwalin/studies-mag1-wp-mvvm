
namespace a; 

public class CommandKill : ListViewCommandBase {
	
	public CommandKill(ViewModelProcessList viewModelProcessList) : base(viewModelProcessList) {
	}

	public override void Execute(object? parameter) {
		try {
			viewModelProcessList.process?.Kill();
			viewModelProcessList.process = null;
			viewModelProcessList.UpdateList(true);
		} catch {
			MessageBox.Show("Access denied to kill: "
				+ viewModelProcessList.process?.ProcessName);
		}
	}
}
