using System;
using System.Windows.Input;

namespace samotnik; 

public class CommandUndo : ICommand {
	private readonly HistoryManager historyManager;

	public CommandUndo(HistoryManager _historyManager) {
		this.historyManager = _historyManager;
	}

	public virtual bool CanExecute(object? parameter) {
		if(historyManager != null) {
			return historyManager.CanRevertHistory();
		}
		return false;
	}
	
	public virtual void Execute(object? parameter) {
		if(historyManager != null) {
			historyManager.RevertHistory();
		}
	}
	
	public event EventHandler? CanExecuteChanged;
}
