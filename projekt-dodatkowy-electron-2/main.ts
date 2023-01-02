import { BrowserWindow } from 'electron';

export default class Main {
	static window: Electron.BrowserWindow;
	static application: Electron.App;
	static BrowserWindow;

	private static onWindowAllClosed() {
		if (process.platform !== 'darwin') {
			Main.application.quit();
		}
	}

	private static onClose() {
		Main.window = null;
	}

	private static onReady() {
		Main.window = new Main.BrowserWindow({
			width: 800,
			height: 600,
			webPreferences: {
				nodeIntegration: true,
				nativeWindowOpen: true,
				enableRemoteModule: true,
				contextIsolation: false
			}
		});
		Main.window.loadURL('file://' + __dirname + '/../index.html');
		Main.window.on('closed', Main.onClose);
	}

	static main(app: Electron.App, browserWindow: typeof BrowserWindow) {
		Main.BrowserWindow = browserWindow;
		Main.application = app;
		Main.application.on('window-all-closed', Main.onWindowAllClosed);
		Main.application.on('ready', Main.onReady);
	}
}