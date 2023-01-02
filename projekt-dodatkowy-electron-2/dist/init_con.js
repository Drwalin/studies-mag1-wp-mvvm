var ConnectionBuilder = require('electron-cgi').ConnectionBuilder;
function CreateConnection() {
    var conIn = new ConnectionBuilder()
        .connectTo('dotnet', 'run', '--project', 'procesy')
        .build();
    conIn.onDisconnect = function () {
        var msg = "Lost connection to dotnet process.";
        console.log(msg);
        alert(msg);
    };
    return conIn;
}
var con = CreateConnection();
//# sourceMappingURL=init_con.js.map