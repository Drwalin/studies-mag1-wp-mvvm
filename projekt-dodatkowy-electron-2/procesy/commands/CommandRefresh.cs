namespace a; 

public class CommandRefresh : ListViewCommandBase {
	public CommandRefresh(ViewModelProcessList viewModelProcessList) : base(viewModelProcessList) {
	}

	public override void Execute(object? parameter) {
		viewModelProcessList.UpdateList(true);
	}
}
