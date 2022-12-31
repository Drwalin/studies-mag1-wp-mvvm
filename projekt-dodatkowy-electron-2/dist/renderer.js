document.getElementById('body').innerHTML += '</br>Mleko!';
var ipc = require('electron').ipcRenderer;
var ConnectionBuilder = require('electron-cgi').ConnectionBuilder;
var con = new ConnectionBuilder()
    .connectTo('dotnet', 'run', '--project', 'C:/studies/pladotnet/projekty/dotnet-test')
    .build();
con.onDisconnect = function () {
    console.log('Lost connection to the .Net process');
};
con.on("greeting_response", function (e) {
    document.getElementById('body').innerHTML += '</br>' + e;
});
con.send("greeting", "Z renderera connection");
//ipc.invoke('greeting', 'Z renderera');
//# sourceMappingURL=renderer.js.map