using GameefanOS.Interfaces;
using GameefanOS.Utils;

namespace GameefanOS.Commands
{
	public class ListUsersCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			if(!user.perms.canSeeAllUsers && args.Length == 2 && args[1] == "all")
			{
				Output.WriteWarning("You don't have permission to view all users, showing normal users...\n");
			}
			foreach (User aUser in ((user.perms.canSeeAllUsers && args.Length == 2 && args[1] == "all") ? User.allUsers : User.users))
			{
				Output.Write($"{aUser.name}:{aUser.userID}:{aUser.executeUserID}:");
				Output.Write("iDAA="+(aUser.perms.isDisplayedAsAdmin ? "1" : "0"));
				Output.Write(",iR=" + (aUser.perms.isRoot ? "1" : "0"));
				Output.Write(",cCSS=" + (aUser.perms.canChangeSystemSettings ? "1" : "0"));
				Output.Write(",cDS=" + (aUser.perms.canDoSudo ? "1" : "0"));
				Output.Write(",cSAU=" + (aUser.perms.canSeeAllUsers ? "1" : "0"));
				Output.Write(":");
				foreach (int group in aUser.groups)
				{
					Output.Write($"{group},");
				}
				Output.Write("\n");
			}
		}

		public string LongHelpMessage_Author()
		{
			return OSInfo.OS_CREATOR;
		}

		public string LongHelpMessage_Function()
		{
			return "Lists all users";
		}

		public string LongHelpMessage_SeeAlso()
		{
			return "adduser; su";
		}

		public string LongHelpMessage_Syntax()
		{
			return "listusers";
		}

		public string ShortHelpMessage()
		{
			return "Lists all users";
		}
	}
}
