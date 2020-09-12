using GameefanOS.Interfaces;
using GameefanOS.Utils;
using System;
using static GameefanOS.Structs;

namespace GameefanOS.Commands
{
	public class SuCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			if (args.Length != 2)
			{
				Output.WriteError("Invalid arguments!\n");
				return;
			}

			User newUser = User.FetchUser(args[1]);
			Output.WriteDebug(newUser.userID.ToString() + "<1000\n");
			Output.WriteDebug(user.executeUserID.ToString() + "!=0\n");
			Output.WriteDebug(user.groups.Contains((int)SystemGroups.Admin).ToString() + "==True\n");
			if (newUser.userID < 1000 && user.executeUserID != 0)
			{
				Output.WriteError("You can't switch to a system user!\n");
				return;
			}
			if (user.executeUserID != 0)
			{
				Output.Write("Password: ");
				Console.ForegroundColor = ConsoleColor.Black;
				if (Console.ReadLine() != newUser.passwd)
				{
					Console.ForegroundColor = ConsoleColor.Gray;
					Output.WriteError("Password incorrect!\n");
					return;
				}
			}
			Output.WriteWarning("Are you sure? Y for yes, other key for no: ", displayPrefix:false);
			if (Console.ReadKey().Key != ConsoleKey.Y)
			{
				Output.Write("\n");
				return;
			}
			else
				Output.Write("\n");
			Console.ForegroundColor = ConsoleColor.Gray;
			User.currentUser = newUser.userID;
		}

		public string LongHelpMessage_Author()
		{
			return OSInfo.MAIN_APP_DEV;
		}

		public string LongHelpMessage_Function()
		{
			return "Switches the current user";
		}

		public string LongHelpMessage_SeeAlso()
		{
			return "sudo";
		}

		public string LongHelpMessage_Syntax()
		{
			return "su <username>";
		}

		public string ShortHelpMessage()
		{
			return "Change the current user";
		}
	}
}
