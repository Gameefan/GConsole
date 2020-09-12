using GameefanOS.Interfaces;
using GameefanOS.Utils;
using GameefanOS.Utils.FileSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameefanOS.Commands.FileSysten
{
	public class CatCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			if(args.Length!=2)
			{
				Output.WriteError("Invalid arguments!\n");
				return;
			}
			if((FSManager.GetFilePerms(args[1]).or ==true&& FSManager.GetFilePerms(args[1]).owner==user.userID)|| FSManager.GetFilePerms(args[1]).ar==true||user.executeUserID==0)
			{
				Output.Write($"{FSManager.CatFile(args[1])}\n");
			}
			else
			{
				Output.WriteError("You don't have permission to read this file!\n");
			}
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
			return "ls; addfile; del";
		}

		public string LongHelpMessage_Syntax()
		{
			return "cat";
		}

		public string ShortHelpMessage()
		{
			return "Display the contents of a file";
		}
	}
}
