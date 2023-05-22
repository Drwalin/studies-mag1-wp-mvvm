using System;
using System.Windows.Input;

namespace samotnik; 

public class CommandUndo : ICommand {
	protected readonly GameBoard gameBoard;

	public CommandUndo(GameBoard gameBoard) {
		this.gameBoard = gameBoard;
	}

	public virtual bool CanExecute(object? parameter) {
		if(gameBoard != null) {
			return gameBoard.CanUndo();
		}
		//throw new Exception("CommandUndo execut null gameboard");
		return false;
	}
	
	public virtual void Execute(object? parameter) {
		if(gameBoard != null) {
			gameBoard.UndoMove();
		}
		//throw new Exception("CommandUndo execut null gameboard");
	}
	
	public event EventHandler? CanExecuteChanged;
}
