using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using a;

namespace samotnik {
	public partial class MainWindow {

		public MainWindow() {
			InitializeComponent();

			KeyDown += (_, a) => {
				if(a.Key == Key.Escape) {
					Close();
				}
			};
		}
	}
}