using GameefanOS.Interfaces;
using GameefanOS.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameefanOS.Commands.Debug
{
	public class DebugCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			if(CommandManager.debugEnabled)
			{
				Output.Write("Debug output turned off\n");
				CommandManager.debugEnabled = false;
			}
			else
			{
				Output.Write("Debug output turned on\n");
				CommandManager.debugEnabled = true;
			}
		}

		public string LongHelpMessage_Author()
		{
			return OSInfo.MAIN_APP_DEV;
		}

		public string LongHelpMessage_Function()
		{
			return "Enables debug ouput";
		}

		public string LongHelpMessage_SeeAlso()
		{
			return "fs";
		}

		public string LongHelpMessage_Syntax()
		{
			return "debug";
		}

		public string ShortHelpMessage()
		{
			return "Toggles debug output";
		}
	}
}
