
namespace a; 

public abstract class ListViewCommandBase {
	protected readonly ViewModelProcessList viewModelProcessList;

	public ListViewCommandBase(ViewModelProcessList viewModelProcessList) {
	    this.viewModelProcessList = viewModelProcessList;
	}

	public abstract void Execute(object? parameter);
}
