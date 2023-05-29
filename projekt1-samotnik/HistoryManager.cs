using System;
using System.Collections.Generic;

namespace samotnik;

public class HistoryManager {
	private readonly List<PawnPair> history = new();
	private GameBoard gameBoard;
	
	public HistoryManager(GameBoard _gameBoard) {
		this.gameBoard = _gameBoard;
	}
	
	public void Reset() {
		history.Clear();
	}
	
	public void PushStateChange(PawnPair pawnPair) {
		history.Add(pawnPair);
	}
	
	public void RevertHistory() {
		if(CanRevertHistory()) {
			var last = history[^1];
			gameBoard.UndoMove(last);
			history.Remove(last);
		}
	}
	
	public bool CanRevertHistory() {
		return history.Count > 0;
	}
}
