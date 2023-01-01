
//const ipc = require('electron').ipcRenderer;

con.on("greeting_response", (e)=>{
	//document.getElementById('body').innerHTML += '</br>'+e;
});

con.send("greeting", "Z renderera connection");




con.on("message_box", (msg)=>{
	alert(msg);
});

con.on("update_main_process_list", (lst:any[])=>{
	RenderList(lst, "main_list");
});

con.on("update_child_process_list", (lst:any[])=>{
	RenderList(lst, "child_list");
});

con.on("update_selected_process", (process)=>{

});
