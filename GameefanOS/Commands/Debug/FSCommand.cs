using GameefanOS.Interfaces;
using GameefanOS.Utils;
using GameefanOS.Utils.FileSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameefanOS.Commands.Debug
{
	public class FSCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			FSManager.GetChildren();
		}

		public string LongHelpMessage_Author()
		{
			return OSInfo.MAIN_APP_DEV;
		}

		public string LongHelpMessage_Function()
		{
			return "See all chuldren of directory";
		}

		public string LongHelpMessage_SeeAlso()
		{
			return "debug";
		}

		public string LongHelpMessage_Syntax()
		{
			return "fs";
		}

		public string ShortHelpMessage()
		{
			return "DEBUG COMMAND! REMOVE BEFORE RELEASE!!!";
		}
	}
}
