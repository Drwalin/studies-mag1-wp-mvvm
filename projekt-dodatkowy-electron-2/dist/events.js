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
    if (process.empty) {
        document.getElementById("priority_process_button").disabled = true;
        document.getElementById("kill_process_button").disabled = true;
    }
    else {
        document.getElementById("priority_process_button").disabled = false;
        document.getElementById("kill_process_button").disabled = false;
    }
    document.getElementById("selected_process_name").textContent = process.name;
    document.getElementById("selected_process_pid").textContent = process.empty ? "" : process.pid;
});
con.on("process_priority", function (priority) {
    document.getElementById("process_priority_string").textContent = priority;
});
con.on("auto_refresh", function (auto_refresh) {
    document.getElementById("auto_refresh_toggle_button").textContent
        = "Auto refresh: " + (auto_refresh ? "Enabled" : "Disabled");
});
//# sourceMappingURL=events.js.map