using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace a; 

public class ProcessSelect {
	public static Process[] GetProcesses() {
		return Process.GetProcesses();
	}

	public static List<Process> GetChildrenProcesses(Process process) {
		if(process == null) return new List<Process>();
		return new ManagementObjectSearcher(
				"SELECT * FROM Win32_Process WHERE ParentProcessId="
				+ process.Id)
			.Get().Cast<ManagementObject>().Select(x =>
				Process.GetProcessById(
					(int)((UInt32)x.GetPropertyValue("ProcessId")))).ToList();
	}
}
