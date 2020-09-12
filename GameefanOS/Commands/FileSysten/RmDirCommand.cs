using GameefanOS.Interfaces;
using GameefanOS.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameefanOS.Commands.FileSysten
{
	public class RmDirCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			Output.WriteError("Command not implemented yet!\n.");
		}

		public string LongHelpMessage_Author()
		{
			return OSInfo.MAIN_APP_DEV;
		}

		public string LongHelpMessage_Function()
		{
			return "Removes the specified directory";
		}

		public string LongHelpMessage_SeeAlso()
		{
			return "mkdir; ls";
		}

		public string LongHelpMessage_Syntax()
		{
			return "rmdir <filename>";
		}

		public string ShortHelpMessage()
		{
			return "No command";
		}
	}
}
