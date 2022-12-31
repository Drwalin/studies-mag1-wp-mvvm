
document.getElementById('body').innerHTML += '</br>Mleko!';

const ipc = require('electron').ipcRenderer;
const { ConnectionBuilder } = require('electron-cgi');

const con = new ConnectionBuilder()
	.connectTo('dotnet', 'run', '--project', 'procesy')
	.build()
con.onDisconnect = () => {
	console.log('Lost connection to the .Net process');
};

con.on("greeting_response", (e)=>{
	document.getElementById('body').innerHTML += '</br>'+e;
});

con.send("greeting", "Z renderera connection");

