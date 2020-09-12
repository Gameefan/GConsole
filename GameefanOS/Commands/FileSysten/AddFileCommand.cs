using GameefanOS.Interfaces;
using GameefanOS.Utils;
using GameefanOS.Utils.FileSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameefanOS.Commands.FileSysten
{
	public class AddFileCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			if(args.Length!=2)
			{
				Output.WriteError("Invalid arguments!");
				return;
			}
			FSManager.AddFile(new Structs.FilePerms { ar = true, aw = false, ax = false, flag = Structs.FileFlag.None, or = true, os = false, ow = true, ox = false, owner=user.userID }, path: FSManager.DirListToString(FSManager.currentDirectory), name: args[1], data: "", id:user.userID);
		}

		public string LongHelpMessage_Author()
		{
			return OSInfo.MAIN_APP_DEV;
		}

		public string LongHelpMessage_Function()
		{
			return "Adds a file with a name <name>";
		}

		public string LongHelpMessage_SeeAlso()
		{
			return "ls; del";
		}

		public string LongHelpMessage_Syntax()
		{
			return "addfile <name>";
		}

		public string ShortHelpMessage()
		{
			return "Creates an empty file";
		}
	}
}
