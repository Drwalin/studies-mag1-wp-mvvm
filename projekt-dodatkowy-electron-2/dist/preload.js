/*
import { ipcRenderer, contextBridge, BrowserWindow } from 'electron'

contextBridge.exposeInMainWorld("api", {
  "hideMainWindow": BrowserWindow.getFocusedWindow().hide(),
})
{
    const InitEvents  = require('./events');
    const con = {on: (a:any, b:any)=>{}, send: (a:any, b:any)=>{}};
    InitEvents(ipc, con, true);
}

*/
var electron = require('electron');
var ipcRenderer = electron.ipcRenderer;
//# sourceMappingURL=preload.js.map