
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
	if(process.empty) {
		(document.getElementById("priority_process_button") as HTMLButtonElement).disabled = true;
		(document.getElementById("kill_process_button") as HTMLButtonElement).disabled = true;
	} else {
		(document.getElementById("priority_process_button") as HTMLButtonElement).disabled = false;
		(document.getElementById("kill_process_button") as HTMLButtonElement).disabled = false;
	}
	document.getElementById("selected_process_name").textContent = process.name;
	document.getElementById("selected_process_pid").textContent = process.empty?"":process.pid;
});

con.on("process_priority", (priority)=>{
	document.getElementById("process_priority_string").textContent = priority;
});


con.on("auto_refresh", (auto_refresh:boolean)=>{
	document.getElementById("auto_refresh_toggle_button").textContent
		= "Auto refresh: " + (auto_refresh?"Enabled":"Disabled");
});


