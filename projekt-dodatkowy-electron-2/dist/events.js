"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function init(ipc, con, is_renderer_process) {
    var renderer_receiver = null;
    var renderer_sender = null;
    if (is_renderer_process) {
        renderer_receiver = function (channel, callback) {
            ipc.on(channel, function (e, args) { callback(args[0]); });
        };
        renderer_sender = function (channel) {
        };
    }
    else {
        renderer_receiver = function (channel, callback) {
            con.on(channel, function (arg) {
                ipc.send(channel, arg);
            });
        };
        renderer_sender = function (channel) {
            ipc.on(channel, function (e, args) {
                con.send(channel, args[0]);
            });
        };
    }
    renderer_receiver("greeting_response", function (v) {
        document.getElementById('body').innerHTML += '</br>' + v;
    });
    renderer_sender("greeting");
}
exports.default = init;
//# sourceMappingURL=events.js.map