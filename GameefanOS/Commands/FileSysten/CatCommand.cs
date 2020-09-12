using GameefanOS.Interfaces;
using GameefanOS.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameefanOS.Commands.FileSysten
{
	public class CatCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			Output.WriteError("Command not implemented yet!\n.");
		}

		public string LongHelpMessage_Author()
		{
			return OSInfo.OS_CREATOR;
		}

		public string LongHelpMessage_Function()
		{
			return "Display the contents of a file";
		}

		public string LongHelpMessage_SeeAlso()
		{
			return "ls; addfile; del";
		}

		public string LongHelpMessage_Syntax()
		{
			return "cat";
		}

		public string ShortHelpMessage()
		{
			return "No command";
		}
	}
}
