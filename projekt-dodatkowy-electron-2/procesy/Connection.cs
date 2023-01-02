using ElectronCgi.DotNet;
using System;

namespace a;

public class Con {
	public static Connection connection = new ConnectionBuilder().WithLogging().Build();
	public static ViewModelProcessList? viewModel;
	public static void Main(string[] args) {
		viewModel = new ViewModelProcessList();
		connection.Listen();
		isConnected = false;
	}
	public static bool isConnected = true;

	public static bool IsConnected() {
		return isConnected;
	}
}