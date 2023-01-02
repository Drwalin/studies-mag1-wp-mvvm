document.getElementById("priority_process_button").onclick = function () {
    con.send("command_priority_change");
};
document.getElementById("kill_process_button").onclick = function () {
    con.send("command_kill");
};
function ModifyRefreshRate(val) {
    var elem = document.getElementById("refresh_rate_value_input");
    var value = val;
    val += +(elem.value);
    if (val < +elem.min)
        val = +elem.min;
    else if (val > +elem.max)
        val = +elem.max;
    elem.value = "" + val;
}
document.getElementById("refresh_rate_value_input").onchange = function () {
    var elem = document.getElementById("refresh_rate_value_input");
    con.send("refresh_rate", +elem.value);
};
document.getElementById("filter_text_input").oninput =
    document.getElementById("filter_text_input").onchange = function () {
        var elem = document.getElementById("filter_text_input");
        con.send("command_filter", elem.value);
    };
document.getElementById("refresh_button").onclick = function () {
    con.send("command_refresh_trigger");
};
document.getElementById("auto_refresh_toggle_button").onclick = function () {
    con.send("command_auto_refresh_toggle");
};
//# sourceMappingURL=interactions.js.map