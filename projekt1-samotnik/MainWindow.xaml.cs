using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace samotnik {
	public partial class MainWindow {
		private readonly GameBoard gameBoard;

		public MainWindow() {
			InitializeComponent();
			
			foreach(var e in (Content as Grid)?.Children) {
				if(e is GameBoard board) {
					gameBoard = board;
				}
			}
			
			if(gameBoard == null) {
				throw new Exception("Brak elementu GameBoard w oknie");
			}
			
			gameBoard.InitMap(InitialMapState.defaultMap);
			this.CommandUndo = new samotnik.CommandUndo(
					gameBoard.GetHistoryManager());

			CreateWindowMenu();

			KeyDown += (_, a) => {
				if(a.Key == Key.Escape) {
					Close();
				}
			};
		}

		void CreateWindowMenu() {
			Menu? menu = null;
			MenuItem? menuNewGame = null;
			
			foreach(var e in (Content as Grid)?.Children) {
				if(e is Menu menu1) {
					menu = menu1;
				}
			}

			if(menu == null) {
				throw new Exception("Brak elementu Menu w oknie");
			}

			foreach(var e in menu.Items) {
				if(e is MenuItem item) {
					if(item.Header.ToString()?.Trim() == "New Game") {
						menuNewGame = item;
					}
				}
			}

			if(menuNewGame == null) {
				throw new Exception("Brak elementu MenuItem('New Game') w oknie");
			}

			foreach(var map in InitialMapState.internalMaps) {
				MenuItem item = new();
				item.Click += (_, _) => {
					gameBoard.InitMap(map);
				};
				item.Header = map.name;
				menuNewGame.Items.Add(item);
			}
		}

		public ICommand CommandUndo { get; set; }
		//public ICommand CommandReset_ { get; set; }

		private void UndoCommand_Executed(object sender, ExecutedRoutedEventArgs e) {
			CommandUndo.Execute(sender);
			//gameBoard.UndoMove();
		}

		private void UndoCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
			//e.CanExecute = gameBoard.CanUndo();
			e.CanExecute = CommandUndo.CanExecute(sender);
		}

		private void ResetCommand_Executed(object sender, ExecutedRoutedEventArgs e) {
			gameBoard.ResetGame();
		}

		private void ResteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
			e.CanExecute = true;
		}
	}
}
