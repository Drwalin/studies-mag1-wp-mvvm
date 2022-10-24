using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace samotnik;

public class GameGrid {
	struct PawnPair {
		public PawnPlace from, to, middle;
	}

	private PawnPlace[,] pawns;
	private readonly Grid grid;
	private List<PawnPair> history = new();

	public GameGrid(Grid grid) {
		this.grid = grid;
	}

	public void InitMap(InitialMapState initialState) {
		history.Clear();
		grid.Children.Clear();
		grid.RowDefinitions.Clear();
		grid.ColumnDefinitions.Clear();
		pawns = new PawnPlace[initialState.GetWidth(),
			initialState.GetHeight()];
		for(int x = 0; x < initialState.GetWidth(); ++x) {
			grid.ColumnDefinitions.Add(new ColumnDefinition());
			for(int y = 0; y < initialState.GetWidth(); ++y) {
				if(x == 0) {
					grid.RowDefinitions.Add(new RowDefinition());
				}

				if(initialState.places[x, y] == 1) {
					pawns[x, y] = new PawnPlace(this, x, y);
					if(initialState.pawns[x, y] == 1) {
						pawns[x, y].SetEnabled(true);
					}
				}
			}
		}
	}

	private PawnPlace lastClickedPawn = null;

	public void OnClick(PawnPlace pawn, RoutedEventArgs args) {
		if(pawn == lastClickedPawn) {
			pawn.SetColor(false);
			lastClickedPawn = null;
		} else if(lastClickedPawn == null) {
			if(pawn.GetEnabled()) {
				lastClickedPawn = pawn;
				pawn.SetColor(true);
			}
		} else if(pawn.GetEnabled() == false) {
			if(lastClickedPawn.IsDistanceRight(pawn)) {
				DoMove(lastClickedPawn, pawn);
			}
		} else if(pawn.GetEnabled()) {
			lastClickedPawn.SetColor(false);
			pawn.SetColor(true);
			lastClickedPawn = pawn;
		}
	}

	private void DoMove(PawnPlace from, PawnPlace to) {
		int x = (to.x + from.x) / 2;
		int y = (to.y + from.y) / 2;
		PawnPlace middle = pawns[x, y];
		if(middle != null && middle.GetEnabled()) {
			history.Add(new PawnPair { from = from, to = to, middle = middle });
			middle.SetEnabled(false);
			PlayAnimation(from, to);
			lastClickedPawn = null;
		}
	}

	public void UndoMove() {
		if(history.Count > 0) {
			var last = history[history.Count - 1];
			last.to.SetEnabled(false);
			last.from.SetEnabled(true);
			last.middle.SetEnabled(true);
			if(lastClickedPawn == last.to || lastClickedPawn == last.from
			                              || lastClickedPawn == last.middle) {
				lastClickedPawn = null;
			}
		}
	}

	private void PlayAnimation(PawnPlace from, PawnPlace to) {
		// TODO: add animation
		from.SetEnabled(false);
		to.SetEnabled(true);
	}


	public Grid GetGrid() {
		return grid;
	}


	public bool IsEnd() {
		foreach(var it in pawns) {
			if(it.GetEnabled() == true) {
				int[,] moves = new int[,] {
					{ 2, 0 },
					{ 0, 2 },
				};
				for(int i = 0; i < moves.Length; ++i) {
					try {
						var p = pawns[it.x + moves[i, 0], it.y + moves[i, 1]];
						if(p != null) {
							if(it.GetEnabled() ^ p.GetEnabled()) {
								int x = (p.x + it.x) / 2;
								int y = (p.y + it.y) / 2;
								var middle = pawns[x, y];
								if(middle != null) {
									if(middle.GetEnabled() == true) {
										return false;
									}
								}
							}
						}
					} catch {
					}
				}
			}
		}

		return true;
	}

	public bool IsWin() {
		int sum = 0;
		foreach(var it in pawns) {
			if(it.GetEnabled() == true) {
				sum++;
			}
		}

		if(sum == 1) {
			return true;
		}

		return false;
	}
}
