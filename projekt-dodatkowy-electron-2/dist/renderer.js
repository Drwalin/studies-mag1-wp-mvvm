//const ipc = require('electron').ipcRenderer;
con.on("greeting_response", function (e) {
    //document.getElementById('body').innerHTML += '</br>'+e;
});
con.send("greeting", "Z renderera connection");
con.on("message_box", function (msg) {
    alert(msg);
});
con.on("update_main_process_list", function (lst) {
    RenderList(lst, "main_list");
});
con.on("update_child_process_list", function (lst) {
    RenderList(lst, "child_list");
});
con.on("update_selected_process", function (process) {
});
//# sourceMappingURL=renderer.js.map