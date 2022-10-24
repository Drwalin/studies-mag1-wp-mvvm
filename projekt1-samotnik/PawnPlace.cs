using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace samotnik;

public class PawnPlace {
	private Button button;
	private Ellipse ellipse;
	private GameGrid gameGrid;
	private Grid grid;
	public readonly int x, y;

	public PawnPlace(GameGrid gameGrid, int x, int y) {
		this.gameGrid = gameGrid;
		grid = gameGrid.GetGrid();
		this.x = x;
		this.y = y;
		
		button = new Button();
		button.Height = 32;
		button.Width = 32;
		grid.Children.Add(button);
		Grid.SetColumn(button, x);
		Grid.SetRow(button, y);
		button.Click += OnClick;
		
		ellipse = new Ellipse();
		ellipse.Height = 24;
		ellipse.Width = 24;
		grid.Children.Add(ellipse);
		Grid.SetColumn(ellipse, x);
		Grid.SetRow(ellipse, y);
		ellipse.IsHitTestVisible = false;
		
		SetEnabled(false);
	}

	public bool IsDistanceRight(PawnPlace other) {
		int dx = x - other.x;
		int dy = y - other.y;
		int dist2 = dx * dx + dy * dy;
		return dist2 == 4;
	}

	public void SetColor(bool down) {
		if(down) {
			ellipse.Fill = Brushes.SaddleBrown;
			ellipse.Stroke = Brushes.SandyBrown;
		} else {
			ellipse.Fill = Brushes.SandyBrown;
			ellipse.Stroke = Brushes.SaddleBrown;
		}
	}

	public void SetEnabled(bool value) {
		if(value) {
			SetColor(false);
			ellipse.Visibility = Visibility.Visible;
		} else {
			SetColor(false);
			ellipse.Visibility = Visibility.Hidden;
		}
	}

	public bool GetEnabled() {
		return ellipse.Visibility == Visibility.Visible;
	}

	void OnClick(object o, RoutedEventArgs args) {
		gameGrid.OnClick(this, args);
	}
}
