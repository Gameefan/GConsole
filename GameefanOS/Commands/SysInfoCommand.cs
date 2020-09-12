using GameefanOS.Interfaces;
using GameefanOS.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameefanOS.Commands
{
	public class SysInfoCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			Output.Write($"OS creator: {OSInfo.OS_CREATOR}\n");
			Output.Write($"Version: {OSInfo.OS_VERSION}\n");
		}

		public string LongHelpMessage_Author()
		{
			return OSInfo.MAIN_APP_DEV;
		}

		public string LongHelpMessage_Function()
		{
			return ShortHelpMessage();
		}

		public string LongHelpMessage_SeeAlso()
		{
			return "help; exit";
		}

		public string LongHelpMessage_Syntax()
		{
			return "sysinfo";
		}

		public string ShortHelpMessage()
		{
			return "Displays info about the system";
		}
	}
}
