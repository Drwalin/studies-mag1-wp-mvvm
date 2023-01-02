var ConnectionBuilder = require('electron-cgi').ConnectionBuilder;
var con;
function CreateConnection() {
    var conIn = new ConnectionBuilder()
        .connectTo('dotnet', 'run', '--project', 'procesy')
        .build();
    conIn.onDisconnect = function () {
        var msg = "Lost connection to dotnet process. Launching new dotnet process instance.";
        console.log(msg);
        alert(msg);
        con = CreateConnection();
    };
    return conIn;
}
con = CreateConnection();
//# sourceMappingURL=init_con.js.map