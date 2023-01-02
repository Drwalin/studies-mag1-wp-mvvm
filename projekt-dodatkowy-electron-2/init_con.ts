
const { ConnectionBuilder } = require('electron-cgi');

function CreateConnection() {
	var conIn = new ConnectionBuilder()
		.connectTo('dotnet', 'run', '--project', 'procesy')
		.build();
	conIn.onDisconnect = () => {
		var msg = "Lost connection to dotnet process.";
		console.log(msg);
		alert(msg);
	};
	return conIn;
}

const con = CreateConnection();

