using GameefanOS.Interfaces;
using GameefanOS.Utils;
using GameefanOS.Utils.FileSystem;

namespace GameefanOS.Commands.FileSysten
{
	public class CdCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			if (args.Length != 2)
			{
				Output.WriteError("Invalid arguments!\n");
				return;
			}
			switch(args[1])
			{
				case "..":
					if (FSManager.PresentWorkingDirectory() != "~/")
					{
						FSManager.ChangeDirectory("..");
						return;
					}else
						return;
				case "/":
					FSManager.ChangeDirectory("/");
					break;
				default:
					if (!FSManager.ChangeDirectory(args[1]))
						return;
					if(FSManager.CatFile("folder.sys")!=User.currentUser.ToString())
					{
						FSManager.ChangeDirectory("..");
						Output.WriteError("You don't have permission to access this folder!\n");
					}
					break;
			}
			/*
			if (args[1] == ".." && FSManager.PresentWorkingDirectory() != "~/")
			{
				FSManager.ChangeDirectory("..");
				return;
			}
			if (args[1] == ".." && FSManager.PresentWorkingDirectory() == "~/")
				return;
			bool undo = FSManager.ChangeDirectory(args[1]);
			if (!((FSManager.CatFile("folder.sys") == user.userID.ToString() && FSManager.GetFilePerms("folder.sys").or == true) || FSManager.GetFilePerms("folder.sys").ar == true || user == User.FetchUserID(0)))
			{
				Output.WriteError("You don't have permission to enter this folder!\n");
				FSManager.ChangeDirectory("..");
				return;
			}
			if (undo&&args[1]!="/")
			{
				FSManager.ChangeDirectory("..");
				FSManager.ChangeDirectory(args[1]);
			}*/
		}

		public string LongHelpMessage_Author()
		{
			return OSInfo.MAIN_APP_DEV;
		}

		public string LongHelpMessage_Function()
		{
			return "Changes the present working directory";
		}

		public string LongHelpMessage_SeeAlso()
		{
			return "mkdir; rmdir; pwd";
		}

		public string LongHelpMessage_Syntax()
		{
			return "cd <directory>";
		}

		public string ShortHelpMessage()
		{
			return "Changes the Present Working Directory";
		}
	}
}
