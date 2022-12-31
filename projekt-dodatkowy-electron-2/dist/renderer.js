document.getElementById('body').innerHTML += '</br>Mleko!';
var ipc = require('electron').ipcRenderer;
var ConnectionBuilder = require('electron-cgi').ConnectionBuilder;
var con = new ConnectionBuilder()
    .connectTo('dotnet', 'run', '--project', 'procesy')
    .build();
con.onDisconnect = function () {
    console.log('Lost connection to the .Net process');
};
con.on("greeting_response", function (e) {
    document.getElementById('body').innerHTML += '</br>' + e;
});
con.send("greeting", "Z renderera connection");
//# sourceMappingURL=renderer.js.map