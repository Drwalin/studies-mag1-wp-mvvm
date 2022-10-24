using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

namespace samotnik {
	public partial class MainWindow : Window {
		TabControl mainTabControl;
		//private Button?[,] map;
		//private Ellipse?[,] ellipses;
		private readonly Grid grid;

		private GameGrid gameGrid;

		public MainWindow() {
			InitializeComponent();
			this.Width = 600;
			this.Height = 600;
			grid = new Grid();
			this.Content = grid;
			grid.ShowGridLines = true;

			gameGrid = new GameGrid(grid);
			gameGrid.InitMap(InitialMapState.defaultMap);
			/*
			InitMap(
				new int[,] {
					{ 0, 0, 1, 1, 1, 0, 0 },
					{ 0, 0, 1, 1, 1, 0, 0 },
					{ 1, 1, 1, 1, 1, 1, 1 },
					{ 1, 1, 1, 1, 1, 1, 1 },
					{ 1, 1, 1, 1, 1, 1, 1 },
					{ 0, 0, 1, 1, 1, 0, 0 },
					{ 0, 0, 1, 1, 1, 0, 0 }
				},
				new int[,] {
					{ 0, 0, 1, 1, 1, 0, 0 },
					{ 0, 0, 1, 1, 1, 0, 0 },
					{ 1, 1, 1, 1, 1, 1, 1 },
					{ 1, 1, 1, 0, 1, 1, 1 },
					{ 1, 1, 1, 1, 1, 1, 1 },
					{ 0, 0, 1, 1, 1, 0, 0 },
					{ 0, 0, 1, 1, 1, 0, 0 }
				});
				*/
		}
		
		
/*
		Button AddButton(int x, int y, RoutedEventHandler o) {
			Button b = new Button();
			map[x, y] = b;
			b.Height = 32;
			b.Width = 32;
			grid.Children.Add(b);
			Grid.SetColumn(b, x);
			Grid.SetRow(b, y);
			b.Click += o;
			return b;
		}

		Ellipse AddEllipse(int x, int y) {
			Ellipse e = new Ellipse();
			ellipses[x, y] = e;
			e.Height = 24;
			e.Width = 24;
			grid.Children.Add(e);
			Grid.SetColumn(e, x);
			Grid.SetRow(e, y);
			SetColor(x, y, false);
			e.IsHitTestVisible = false;
			return e;
		}

		void InitMap(int[,] m, int[,] n) {
			DeleteMap();
			MakeEmptyGrid(m.GetLength(0), m.GetLength(1));
			for(int i = 0; i < map.GetLength(0); ++i) {
				for(int j = 0; j < map.GetLength(1); ++j) {
					if(m[i, j] != 0) {
						AddButton(i, j, OnClick);
						if(n[i, j] != 0) {
							AddEllipse(i, j);
						}
					}
				}
			}
		}

		void MakeEmptyGrid(int w, int h) {
			map = new Button[w, h];
			ellipses = new Ellipse[w, h];
			for(int i = 0; i < w; ++i) {
				var c = new ColumnDefinition();
				grid.ColumnDefinitions.Add(c);
				for(int j = 0; j < h; ++j) {
					if(i == 0) {
						var r = new RowDefinition();
						grid.RowDefinitions.Add(r);
					}
				}
			}
		}

		private int X = -1, Y = -1;

		void OnClick(object o, RoutedEventArgs args) {
			Button b = o as Button;
			if(b == null) return;
			int x = Grid.GetColumn(b);
			int y = Grid.GetRow(b);

			if(X == -1 && Y == -1) {
				if(ellipses[x, y] != null) {
					SetColor(x, y, true);
					X = x;
					Y = y;
				}
			} else if(X == x && Y == y) {
				if(ellipses[x, y] != null) {
					SetColor(x, y, false);
					X = -1;
					Y = -1;
				}
			} else if(ellipses[x, y] == null) {
				int _x = x - X;
				int _y = y - Y;
				if((_x == 2 && _y == 0) || (_x == 0 && _y == 2)
				                        || (_x == -2 && _y == 0)
				                        || (_x == 0 && _y == -2)) {
					int A = (x + X) / 2;
					int B = (y + Y) / 2;
					if(ellipses[A, B] != null) {
						SetColor(X, Y, false);
						RemoveEllipse(A, B);
						MoveEllipse(X, Y, x, y);
						X = -1;
						Y = -1;
						return;
					}
				}
			}

			if(X >= 0 && Y >= 0) {
				if(ellipses[X, Y] != null) {
					SetColor(X, Y, false);
				}

				X = -1;
				Y = -1;
				if(ellipses[x, y] != null) {
					SetColor(x, y, true);
					X = x;
					Y = y;
				}
			}
		}

		void RemoveEllipse(int x, int y) {
			grid.Children.Remove(ellipses[x, y]);
			ellipses[x, y] = null;
		}

		void MoveEllipse(int ax, int ay, int bx, int by) {
			Ellipse? e = ellipses[ax, ay];
			ellipses[ax, ay] = null;
			ellipses[bx, by] = e;
			Grid.SetColumn(e, bx);
			Grid.SetRow(e, by);
		}

		void SetColor(int x, int y, bool down) {
			if(ellipses[x, y] != null) {
				if(down) {
					ellipses[x, y].Fill = Brushes.SaddleBrown;
					ellipses[x, y].Stroke = Brushes.SandyBrown;
				} else {
					ellipses[x, y].Fill = Brushes.SandyBrown;
					ellipses[x, y].Stroke = Brushes.SaddleBrown;
				}
			}
		}

		void DeleteMap() {
			if(map != null) {
				map = null;
				grid.Children.Clear();
				ellipses = null;
			}
		}*/
	}
}