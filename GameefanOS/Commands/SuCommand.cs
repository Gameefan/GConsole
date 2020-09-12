using GameefanOS.Interfaces;
using GameefanOS.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameefanOS.Commands
{
	public class SuCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			if(args.Length!=2)
			{
				Output.WriteError("Invalid arguments!\n");
				return;
			}

			User newUser = User.FetchUser(args[1]);
			if(newUser.userID<1000)
			{
				Output.WriteError("You can't switch to a system user!\n");
				return;
			}
			Output.Write("Password: ");
			Console.ForegroundColor = ConsoleColor.Black;
			if (Console.ReadLine() != newUser.passwd)
			{
				Console.ForegroundColor = ConsoleColor.Gray;
				Output.WriteError("Password incorrect!\n");
				return;
			}
			Console.ForegroundColor = ConsoleColor.Gray;
			User.currentUser = newUser.userID;
		}

		public string LongHelpMessage_Author()
		{
			return OSInfo.OS_CREATOR;
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
