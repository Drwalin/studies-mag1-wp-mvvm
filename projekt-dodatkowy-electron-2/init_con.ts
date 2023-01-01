
const { ConnectionBuilder } = require('electron-cgi');

const con = new ConnectionBuilder()
	.connectTo('dotnet', 'run', '--project', 'procesy')
	.build()
con.onDisconnect = () => {
	console.log('Lost connection to the .Net process');
};
