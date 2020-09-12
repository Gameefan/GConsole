using GameefanOS.Interfaces;
using GameefanOS.Utils;
using GameefanOS.Utils.FileSystem;
using System;
using System.Collections.Generic;
using System.Text;
using static GameefanOS.Structs;

namespace GameefanOS.Commands.FileSysten
{
	public class DelCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			FilePerms fp = FSManager.GetFilePerms(args[1]);
			if (fp.flag == FileFlag.DoesntExists)
			{
				Output.WriteError("File doesn't exist!\n");
				return;
			}
			if ((user.userID==fp.owner||fp.aw==true||user.executeUserID==0))
			{
				Output.WriteError("You don't have permission to delete this file!\n");
				return;
			}
			FSManager.RemoveFile(args[1]);
		}

		public string LongHelpMessage_Author()
		{
			return OSInfo.MAIN_APP_DEV;
		}

		public string LongHelpMessage_Function()
		{
			return "Deletes the specified file";
		}

		public string LongHelpMessage_SeeAlso()
		{
			return "ls; addfile";
		}

		public string LongHelpMessage_Syntax()
		{
			return "del <filename>";
		}

		public string ShortHelpMessage()
		{
			return "Deletes a file";
		}
	}
}
