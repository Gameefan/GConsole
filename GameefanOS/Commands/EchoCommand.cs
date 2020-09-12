using System;
using System.Collections.Generic;
using System.Text;
using GameefanOS.Interfaces;
using GameefanOS.Utils;
using static GameefanOS.Structs;

namespace GameefanOS.Commands
{
	public class EchoCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			for (int i = 1; i < args.Length; i++)
			{
				Output.Write(args[i].Replace("\\n", "\n") + " ");
			}
			Output.Write("\n");
		}

		public string LongHelpMessage_Author()
		{
			return OSInfo.OS_CREATOR;
		}

		public string LongHelpMessage_Function()
		{
			return "Display <text> on the screen";
		}

		public string LongHelpMessage_SeeAlso()
		{
			return "help";
		}

		public string LongHelpMessage_Syntax()
		{
			return "echo <text>";
		}

		public string ShortHelpMessage()
		{
			return "Displays text to the console";
		}
	}
}
