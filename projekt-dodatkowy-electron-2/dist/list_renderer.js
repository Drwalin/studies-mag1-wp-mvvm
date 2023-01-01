var __spreadArray = (this && this.__spreadArray) || function (to, from, pack) {
    if (pack || arguments.length === 2) for (var i = 0, l = from.length, ar; i < l; i++) {
        if (ar || !(i in from)) {
            if (!ar) ar = Array.prototype.slice.call(from, 0, i);
            ar[i] = from[i];
        }
    }
    return to.concat(ar || Array.prototype.slice.call(from));
};
function Elem(type) {
    var content = [];
    for (var _i = 1; _i < arguments.length; _i++) {
        content[_i - 1] = arguments[_i];
    }
    var e = document.createElement(type);
    e.replaceChildren.apply(e, content);
    return e;
}
function CreateRow(type, onpresselem) {
    var content = [];
    for (var _i = 2; _i < arguments.length; _i++) {
        content[_i - 2] = arguments[_i];
    }
    var children = [];
    content.forEach(function (value) {
        var e = Elem(type, value[0]);
        if (onpresselem != null) {
            e.onclick = function () {
                onpresselem(value[1]);
            };
        }
        children.push(e);
    });
    return Elem.apply(void 0, __spreadArray(["tr"], children, false));
}
function CreateHeaderRow(use_sort) {
    return CreateRow("th", use_sort ?
        function (id) {
            console.log("click: " + id);
            con.send("command_sort", id);
        } : null, ["PID", "PID"], ["Name", "Name"], ["CPU Time [s]", "Time"], ["Memory [B]", "Memory"]);
}
function RenderList(lst, listBoxId) {
    var table = document.getElementById(listBoxId);
    var children = [];
    children.push(CreateHeaderRow(listBoxId == "main_list"));
    lst.forEach(function (value) {
        var row = CreateRow("td", null, [value.pid], [value.name], [value.time], [value.memory]);
        row.onclick = function () {
            console.log("click pid: " + value.pid);
            con.send("choose_process", +value.pid);
        };
        children.push(row);
    });
    table.replaceChildren.apply(table, children);
}
//# sourceMappingURL=list_renderer.js.map