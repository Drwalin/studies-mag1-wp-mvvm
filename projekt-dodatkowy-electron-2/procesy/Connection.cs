using ElectronCgi.DotNet;
using System;

namespace a;

public class Con {
	public static Connection connection = new ConnectionBuilder().WithLogging().Build();
	public static ViewModelProcessList? viewModel;
	public static void Main(string[] args) {
		connection.On<string>("greeting",
			(name) =>
			{
				var str = $"Hello {name}!";
				Console.Error.WriteLine(str);
				connection.Send("greeting_response", str);
			});

		viewModel = new ViewModelProcessList();
		connection.Listen();
		isConnected = false;
	}
	public static bool isConnected = true;

	public static bool IsConnected() {
		return isConnected;
	}
}