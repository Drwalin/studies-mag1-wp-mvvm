
function Elem(type: string, ...content: (string|Node)[]) {
	var e = document.createElement(type);
	e.replaceChildren(...content);
	return e;
}


function CreateRow(type:string, onpresselem:null|((id:string)=>void), ...content: string[][]) {
	var children: Node[] = [];
	content.forEach((value)=>{
		var e = Elem(type, value[0]);
		if(onpresselem != null) {
			e.onclick = ()=>{
				onpresselem(value[1]);
			};
		}
		children.push(e);
	});
	return Elem("tr", ...children);
}


function CreateHeaderRow(use_sort: boolean) {
	return CreateRow("th",
		use_sort ?
		(id:string)=>{
			console.log("click: " + id);
			con.send("command_sort", id);
		} : null,
		["PID","PID"],
		["Name","Name"],
		["CPU Time [s]","Time"],
		["Memory [B]","Memory"]
	);
}

function RenderList(lst: any[], listBoxId: string) {
	var table = document.getElementById(listBoxId);
	var children: (string|Node)[] = [];
	
	children.push(CreateHeaderRow(listBoxId=="main_list"));
	lst.forEach((value:any)=>{
		var row = CreateRow("td", null, [value.pid], [value.name], [value.time], [value.memory]);
		row.onclick = ()=>{
			console.log("click pid: " + value.pid);
			con.send("choose_process", +value.pid);
		};
		children.push(row);
	});

	table.replaceChildren(...children);
}
