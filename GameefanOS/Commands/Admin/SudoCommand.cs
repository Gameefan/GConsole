using GameefanOS.Interfaces;
using GameefanOS.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameefanOS.Commands.Admin
{
	public class SudoCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			Output.Write("Password: ");
			Console.ForegroundColor = ConsoleColor.Black;
			if (Console.ReadLine() != user.passwd)
			{
				Output.WriteError("Wrong password!\n");
				return;
			}
			if (!user.perms.canDoSudo)
			{
				Output.WriteError("Access denied!\n");
				return;
			}
			Console.ForegroundColor = ConsoleColor.Gray;
			List<string> newArgs = new List<string>();
			newArgs.AddRange(args);
			newArgs.RemoveAt(0);
			string cmd = "";
			foreach (string arg in newArgs)
			{
				cmd += arg;
				cmd += " ";
			}
			CommandManager.FetchCommand(cmd, User.rootUser);
		}

		public string LongHelpMessage_Author()
		{
			return OSInfo.OS_CREATOR;
		}

		public string LongHelpMessage_Function()
		{
			return "Allows to execute commands as root(0:0)";
		}

		public string LongHelpMessage_SeeAlso()
		{
			return "su";
		}

		public string LongHelpMessage_Syntax()
		{
			return "sudo <command with arguments>";
		}

		public string ShortHelpMessage()
		{
			return "Executes a command as root(0:0)";
		}
	}
}
