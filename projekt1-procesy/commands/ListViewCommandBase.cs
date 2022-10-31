using System;
using System.Windows.Input;

namespace a; 

public abstract class ListViewCommandBase : ICommand {
	protected readonly ViewModelProcessList viewModelProcessList;

	public ListViewCommandBase(ViewModelProcessList viewModelProcessList) {
	    this.viewModelProcessList = viewModelProcessList;
	}

	public virtual bool CanExecute(object? parameter) {
	    return true;
	}
	
	public abstract void Execute(object? parameter);
	
	public event EventHandler? CanExecuteChanged;
}
