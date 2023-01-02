var ConnectionBuilder = require('electron-cgi').ConnectionBuilder;
var con = new ConnectionBuilder()
    .connectTo('dotnet', 'run', '--project', 'procesy')
    .build();
con.onDisconnect = function () {
    console.log("Lost connection to dotnet process");
    alert("Lost connection to dotnet process");
};
//# sourceMappingURL=init_con.js.map