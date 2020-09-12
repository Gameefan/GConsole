using System;
using System.Collections.Generic;
using System.Text;

namespace GameefanOS.Utils
{
	public static class Output
	{
		public static string Write(string s, ConsoleColor color = ConsoleColor.Gray)
		{
			Console.ForegroundColor = color;
			string msg = $"{s}";
			Console.Write(msg);
			Console.ForegroundColor = ConsoleColor.Gray;
			return msg;
		}
		public static string WriteWarning(string s, ConsoleColor color = ConsoleColor.Yellow)
		{
			Console.ForegroundColor = color;
			string msg = $"Warning: {s}";
			Console.Write(msg);
			Console.ForegroundColor = ConsoleColor.Gray;
			return msg;
		}
		public static string WriteError(string s, ConsoleColor color = ConsoleColor.Red)
		{
			Console.ForegroundColor = color;
			string msg = $"Error: {s}";
			Console.Write(msg);
			Console.ForegroundColor = ConsoleColor.Gray;
			return msg;
		}
		public static string WriteDebug(string s, ConsoleColor color = ConsoleColor.Cyan)
		{
			if (!CommandManager.debugEnabled)
				return "";
			Console.ForegroundColor = color;
			string msg = $"Debug: {s}";
			Console.Write(msg);
			Console.ForegroundColor = ConsoleColor.Gray;
			return msg;
		}
	}
}
