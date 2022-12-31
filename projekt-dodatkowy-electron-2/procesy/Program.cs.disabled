using ElectronCgi.DotNet;
using System;

var connection = new ConnectionBuilder()
	.WithLogging()
	.Build();

// expects a request named "greeting" with a string argument and returns a string
connection.On<string>("greeting",
	(name) => {
		var str = $"Hello {name}!";
		Console.Error.WriteLine(str);
		connection.Send("greeting_response", str);
	});

// wait for incoming requests
connection.Listen();

