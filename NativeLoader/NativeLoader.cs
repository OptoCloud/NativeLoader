using MelonLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace NativeLoader
{
	public class NativeLoader : MelonMod
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr LoadLibrary(string libname);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		private static extern bool FreeLibrary(IntPtr hModule);

		private List<IntPtr> modules = new List<IntPtr>();

		public override void OnApplicationStart()
		{
			Directory.CreateDirectory("NativeMods");
			foreach (var file in Directory.GetFiles("NativeMods", "*.dll", SearchOption.TopDirectoryOnly))
			{
				IntPtr hdl = LoadLibrary(file);
				if (hdl == IntPtr.Zero)
				{
					MelonLogger.Msg($"Failed to load library (ErrorCode: {Marshal.GetLastWin32Error()})");
				}
				MelonLogger.Msg("Loaded " + file);
				modules.Add(hdl);
			}
		}
	}
}
