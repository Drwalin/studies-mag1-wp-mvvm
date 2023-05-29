using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace samotnik; 

public partial class GameBoard {
	private PawnPlace?[,] pawns;
	private readonly Grid grid;
	private InitialMapState lastStartedGameMap = InitialMapState.defaultMap;
	private HistoryManager historyManager;
	
	public HistoryManager GetHistoryManager() {
		return historyManager;
	}

	public GameBoard() {
		InitializeComponent();
		grid = new Grid();
		Content = grid;
		grid.ShowGridLines = true;
		historyManager = new HistoryManager(this);
	}

	public void ResetGame() {
		InitMap(lastStartedGameMap);
	}

	public void InitMap(InitialMapState initialState) {
		lastStartedGameMap = initialState;
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
						pawns[x, y]?.SetEnabled(true);
					}
				}
			}
		}
	}

	private PawnPlace? lastClickedPawn;

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
				lastClickedPawn = pawn;
				pawn.SetColor(true);
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
		PawnPlace? middle = pawns[x, y];
		if(middle != null && middle.GetEnabled()) {
			historyManager.PushStateChange( new PawnPair { from = from, to = to, middle = middle });
			middle.SetEnabled(false);
			PlayAnimation(from, to);
			lastClickedPawn = null;
		}

		if(IsEnd()) {
			MessageBox.Show(Window.GetWindow(this),
				IsWin() ? "You won!" : "You loose!");
		}
	}

	private void PlayAnimation(PawnPlace from, PawnPlace to) {
		// TODO: add animation
		from.SetEnabled(false);
		to.SetEnabled(true);


		bool rows = (from.x != to.x);

		Storyboard? storyboard = this.FindResource(rows?"EllipseRowAnimation":"EllipseColumnAnimation") as Storyboard;
		var timeline = storyboard?.Children as TimelineCollection;
		Int32Animation ia = timeline[0] as Int32Animation;
		
		ia.From = rows ? from.y : from.x;
		ia.To = rows ? to.y : to.x;

		//timeline[0] = ia;

		Storyboard.SetTarget(storyboard, to.ellipse);
		storyboard.Begin();
	}

	public void UndoMove(PawnPair delta) {
		delta.to.SetEnabled(false);
		delta.from.SetEnabled(true);
		delta.middle?.SetEnabled(true);
		if(lastClickedPawn == delta.to || lastClickedPawn == delta.from
				|| lastClickedPawn == delta.middle) {
			lastClickedPawn = null;
		}
	}


	public Grid GetGrid() {
		return grid;
	}


	private bool IsEnd() {
		foreach(var it in pawns) {
			if(it?.GetEnabled() == true) {
				int[,] moves = {
					{ 2, 0 },
					{ 0, 2 },
				};
				for(int i = 0; i < moves.GetLength(0); ++i) {
					if(it.x + moves[i, 0] < pawns.GetLength(0)
					   && it.y + moves[i, 1] < pawns.GetLength(1)) {
						var p = pawns[it.x + moves[i, 0], it.y + moves[i, 1]];
						if(p != null) {
							if(it.GetEnabled() != p.GetEnabled()) {
								int x = (p.x + it.x) / 2;
								int y = (p.y + it.y) / 2;
								var middle = pawns[x, y];
								if(middle != null) {
									if(middle.GetEnabled()) {
										return false;
									}
								}
							}
						}
					}
				}
			}
		}

		return true;
	}

	private bool IsWin() {
		int sum = 0;
		foreach(var it in pawns) {
			if(it?.GetEnabled() == true) {
				sum++;
			}
		}

		if(sum == 1) {
			return true;
		}

		return false;
	}
}

