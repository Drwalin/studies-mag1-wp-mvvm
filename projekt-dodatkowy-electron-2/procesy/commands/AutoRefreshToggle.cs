namespace a; 

public class AutoRefreshToggle : ListViewCommandBase {
	
	public AutoRefreshToggle(ViewModelProcessList viewModelProcessList) : base(viewModelProcessList) {
	}

	public override void Execute(object? parameter) {
		viewModelProcessList.autoRefresh ^= true;
		Con.connection.Send("auto_refresh", viewModelProcessList.autoRefresh);
	}
}
