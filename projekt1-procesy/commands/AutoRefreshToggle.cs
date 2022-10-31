namespace a; 

public class AutoRefreshToggle : ListViewCommandBase {
	
	public AutoRefreshToggle(ViewModelProcessList viewModelProcessList) : base(viewModelProcessList) {
	}

	public override void Execute(object? parameter) {
		viewModelProcessList.AutoRefresh ^= true;
		viewModelProcessList.TriggerPorpertyChange("AutoRefresh");
	}
}
