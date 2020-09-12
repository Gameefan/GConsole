using GameefanOS.Interfaces;
using GameefanOS.Utils;
using GameefanOS.Utils.FileSystem;

namespace GameefanOS.Commands.FileSysten
{
	public class MkDirCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			if(args.Length!=2)
			{
				Output.WriteError("Invalid arguments!\n");
				return;
			}
			Output.WriteDebug("Is user owner?: "+(FSManager.CatFile("folder.sys") == user.userID.ToString()).ToString() + "\n");
			Output.WriteDebug("OW: "+(FSManager.GetFilePerms("folder.sys").ow == true).ToString() + "\n");
			Output.WriteDebug("AW: "+(FSManager.GetFilePerms("folder.sys").aw == true).ToString() + "\n");
			Output.WriteDebug("ID: "+user.userID.ToString() + "\n");
			Output.WriteDebug("Folder owner: "+FSManager.CatFile("folder.sys") + "\n");
			if ((FSManager.CatFile("folder.sys") == user.userID.ToString() && FSManager.GetFilePerms("folder.sys").ow == true) || FSManager.GetFilePerms("folder.sys").aw == true||user==User.FetchUserID(0))
			{
				FSManager.AddFolder(args[1], User.FetchUserID(User.currentUser));
			}else
			{
				Output.WriteError("You dont have permission to make a folder here!\n");
			}
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
			return "rmdir; pwd; ls";
		}

		public string LongHelpMessage_Syntax()
		{
			return "mkdir <filename>";
		}

		public string ShortHelpMessage()
		{
			return "Creates a new directory";
		}
	}
}
