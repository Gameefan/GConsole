using GameefanOS.Interfaces;
using GameefanOS.Utils;
using GameefanOS.Utils.FileSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameefanOS.Commands.FileSysten
{
	public class PWDCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			Output.Write(FSManager.PresentWorkingDirectory()+"\n");
		}

		public string LongHelpMessage_Author()
		{
			return OSInfo.OS_CREATOR;
		}

		public string LongHelpMessage_Function()
		{
			return ShortHelpMessage();
		}

		public string LongHelpMessage_SeeAlso()
		{
			return "mkdir; rmdir; ls";
		}

		public string LongHelpMessage_Syntax()
		{
			return "pwd";
		}

		public string ShortHelpMessage()
		{
			return "Displays the Present Working Directory";
		}
	}
}
