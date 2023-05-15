namespace a; 

public class CommandSort : ListViewCommandBase {
	
	public CommandSort(ViewModelProcessList viewModelProcessList) : base(viewModelProcessList) {
	}

	public override void Execute(object? parameter) {
		if(parameter is string v) {
			viewModelProcessList.SortProcesses(v);
		}
	}
}
