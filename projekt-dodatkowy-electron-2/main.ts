import { BrowserWindow } from 'electron';
const ipc = require('electron').ipcMain;
import InitEvents from './events';

export default class Main {
	static mainWindow: Electron.BrowserWindow;
	static window: Electron.BrowserWindow;
	static application: Electron.App;
	static BrowserWindow;

	private static onWindowAllClosed() {
		if (process.platform !== 'darwin') {
			Main.application.quit();
		}
	}

	private static onClose() {
		// Dereference the window object. 
		Main.mainWindow = null;
	}

	private static onReady() {
		Main.mainWindow = new Main.BrowserWindow({
			width: 800,
			height: 600,
			webPreferences: {
				nodeIntegration: true,
				nativeWindowOpen: true,
				enableRemoteModule: true,
				contextIsolation: false,
				//preload: Main.application.getAppPath() + '\\' + 'preload.js'
				preload: `${__dirname}/preload.js`
			}
		});
		Main.window = Main.mainWindow;
		Main.mainWindow.loadURL('file://' + __dirname + '/../index.html');
		Main.mainWindow.on('closed', Main.onClose);
		Main.mainWindow.webContents.openDevTools()
	}

	static main(app: Electron.App, browserWindow: typeof BrowserWindow) {
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

	}
}