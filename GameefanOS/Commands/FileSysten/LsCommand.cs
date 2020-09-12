using GameefanOS.Interfaces;
using GameefanOS.Utils;
using GameefanOS.Utils.FileSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameefanOS.Commands.FileSysten
{
	public class LsCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			if (!((FSManager.CatFile("folder.sys") == user.userID.ToString() && FSManager.GetFilePerms("folder.sys").or == true) || FSManager.GetFilePerms("folder.sys").ar == true || user == User.FetchUserID(0)))
			{
				try
				{
					Output.WriteError($"You don't have permission to read the contents of this folder! You need permission from {User.FetchUserID(int.Parse(FSManager.CatFile("folder.sys"))).name} to access this folder!\n");
				}
				catch
				{
					Output.WriteError("You don't have permission to read the contents of this folder!");
				}
				return;
			}
			FSManager.GetChildren();
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
			return "pwd";
		}

		public string LongHelpMessage_Syntax()
		{
			return "ls";
		}

		public string ShortHelpMessage()
		{
			return "Display the contents of the Present Working Directory";
		}
	}
}
