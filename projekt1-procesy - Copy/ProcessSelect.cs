using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Windows.Documents;

namespace a; 

public class ProcessSelect {
	public static List<Process> GetProcesses() {
		return Process.GetProcesses().ToList();
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
