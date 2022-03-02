using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SteamLauncher
{
	class Program
	{
		[DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool ShowWindow([In] IntPtr hWnd, [In] int nCmdShow);

		static void Main(string[] args)
		{
			if (args.Length != 2)
			{
				Console.WriteLine("ERROR: Needs launch URL and EXE Name");
				return;
			}
			
			var xboxUrl = args[0];
			var exeName = args[1];

			// string epicUrl = @"shell:AppsFolder\SEGAofAmericaInc.Yazawa_s751p9cej88mt!Game0";
			// string exeName = "YakuzaLikeADragon";

			var ps = new ProcessStartInfo("explorer.exe", xboxUrl)
			{
				UseShellExecute = true,
				Verb = "open"
			};

			Console.WriteLine($"Starting url: {xboxUrl}");
			Process.Start(ps);

			Thread.Sleep(15000);

			var gameProcesses = Process.GetProcessesByName(exeName);

			if (gameProcesses.Length == 0)
			{
				Console.WriteLine($"Could not find a single process with name: {exeName}");
				return;
			}

			Console.WriteLine($"Game started.");

			IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
			ShowWindow(handle, 6);

			gameProcesses[0].WaitForExit();
		}
	}
}