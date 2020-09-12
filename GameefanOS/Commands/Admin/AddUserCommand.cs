using GameefanOS.Interfaces;
using GameefanOS.Utils;
using System;
using static GameefanOS.Structs;

namespace GameefanOS.Commands.Admin
{
	public class AddUserCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			if (user.executeUserID != User.rootUser.executeUserID || !user.perms.canChangeSystemSettings)
			{
				Output.WriteError("Permission denied!\n");
				return;
			}
			Output.Write("Please input a username: ");
			string username = Console.ReadLine();
			Output.Write("Please input a password: ");
			Console.ForegroundColor = ConsoleColor.Black;
			string passwd = Console.ReadLine();
			Console.ForegroundColor = ConsoleColor.Gray;
			Output.Write("Please repeat the password: ");
			Console.ForegroundColor = ConsoleColor.Black;
			string rpasswd = Console.ReadLine();
			Console.ForegroundColor = ConsoleColor.Gray;
			if (passwd != rpasswd)
			{
				Output.WriteError("Passwords do not match\n");
				return;
			}
			bool isAdmin = false;
			if (args.Length == 2 && args[1] == "admin")
				isAdmin = true;
			User newUser = new User();

			newUser.executeUserID = User.GetNextUserID();
			newUser.groups.Add((int)SystemGroups.NormalUser);
			newUser.name = username;
			newUser.perms.canChangeSystemSettings = (isAdmin ? true : false);
			newUser.perms.canDoSudo = (isAdmin ? true : false);
			newUser.perms.isDisplayedAsAdmin = (isAdmin ? true : false);
			newUser.perms.isRoot = false;
			newUser.perms.canSeeAllUsers = (isAdmin ? true : false);
			newUser.userID = User.GetNextUserID();
			newUser.passwd = passwd;

			if (isAdmin)
				newUser.groups.Add((int)SystemGroups.Admin);
			User.AddUser(newUser);

		}

		public string LongHelpMessage_Author()
		{
			return OSInfo.MAIN_APP_DEV;
		}

		public string LongHelpMessage_Function()
		{
			return "Adds a user";
		}

		public string LongHelpMessage_SeeAlso()
		{
			return "listusers";
		}

		public string LongHelpMessage_Syntax()
		{
			return "adduser (admin?admin:empty)";
		}

		public string ShortHelpMessage()
		{
			return "Adds a user";
		}
	}
}