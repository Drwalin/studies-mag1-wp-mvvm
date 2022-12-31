
function init(ipc, con, is_renderer_process) {
	var renderer_receiver:(channel: string, callback: (arg: any) => void)=>void = null;
	var renderer_sender:(channel: string)=>void = null;
	if(is_renderer_process) {
		renderer_receiver = function(
				channel: string,
				callback: (arg: any) => void) {
			ipc.on(channel, (e, args)=>{callback(args[0]);});
		}
		renderer_sender = function(channel: string) {
		}
	} else {
		renderer_receiver = function(
				channel: string,
				callback: (arg: any) => void) {
			con.on(channel, (arg)=> {
				ipc.send(channel, arg);
			});
		}
		renderer_sender = function(channel: string) {
			ipc.on(channel, (e, args)=>{
				con.send(channel, args[0]);	
			});
		}
	}

	renderer_receiver("greeting_response", v=>{
		document.getElementById('body').innerHTML += '</br>' + v;
	});

	renderer_sender("greeting");
}

export default init;
