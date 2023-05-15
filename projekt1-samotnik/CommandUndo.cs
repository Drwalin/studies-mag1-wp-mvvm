using System;
using System.Windows.Input;

namespace samotnik; 

public abstract class CommandUndo : ICommand {
	protected readonly GameBoard gameBoard;

	public CommandUndo(GameBoard gameBoard) {
		this.gameBoard = gameBoard;
	}

	public virtual bool CanExecute(object? parameter) {
		if(gameBoard != null) {
			return gameBoard.CanUndo();
		}
		return false;
	}
	
	public virtual void Execute(object? parameter) {
		if(gameBoard != null) {
			gameBoard.UndoMove();
		}
	}
	
	public event EventHandler? CanExecuteChanged;
}
