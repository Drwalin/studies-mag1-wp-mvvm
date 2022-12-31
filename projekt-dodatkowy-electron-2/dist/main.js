"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ipc = require('electron').ipcMain;
var Main = /** @class */ (function () {
    function Main() {
    }
    Main.onWindowAllClosed = function () {
        if (process.platform !== 'darwin') {
            Main.application.quit();
        }
    };
    Main.onClose = function () {
        // Dereference the window object. 
        Main.mainWindow = null;
    };
    Main.onReady = function () {
        Main.mainWindow = new Main.BrowserWindow({
            width: 800,
            height: 600,
            webPreferences: {
                nodeIntegration: true,
                nativeWindowOpen: true,
                enableRemoteModule: true,
                contextIsolation: false,
                //preload: Main.application.getAppPath() + '\\' + 'preload.js'
                preload: "".concat(__dirname, "/preload.js")
            }
        });
        Main.window = Main.mainWindow;
        Main.mainWindow.loadURL('file://' + __dirname + '/../index.html');
        Main.mainWindow.on('closed', Main.onClose);
        Main.mainWindow.webContents.openDevTools();
    };
    Main.main = function (app, browserWindow) {
        // we pass the Electron.App object and the  
        // Electron.BrowserWindow into this function 
        // so this class has no dependencies. This 
        // makes the code easier to write tests for 
        Main.BrowserWindow = browserWindow;
        Main.application = app;
        Main.application.on('window-all-closed', Main.onWindowAllClosed);
        Main.application.on('ready', Main.onReady);
        /*
        const { ConnectionBuilder } = require('electron-cgi');

        const con = new ConnectionBuilder()
            .connectTo('dotnet', 'run', '--project', 'C:/studies/pladotnet/projekty/dotnet-test')
            .build()
        con.onDisconnect = () => {
            console.log('Lost connection to the .Net process');
        };

        InitEvents(ipc, con, false);
        */
    };
    return Main;
}());
exports.default = Main;
//# sourceMappingURL=main.js.map