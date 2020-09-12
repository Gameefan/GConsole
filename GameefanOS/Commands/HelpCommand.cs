using GameefanOS.Interfaces;
using GameefanOS.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameefanOS.Commands
{
	public class HelpCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			if(args.Length>2)
			{
				Output.WriteError("Invalid arguments\n");
				return;
			}
			if (args.Length == 1)
			{
				foreach (string str in CommandManager.commands.Keys)
				{
					Output.Write($"	{str}\n");
					Output.Write($"		{CommandManager.commands[str].ShortHelpMessage()}\n");
				}
			}else
			{
				if(!CommandManager.commands.ContainsKey(args[1]))
				{
					Output.WriteError("That command doesn't exist!\n");
					return;
				}
				Output.Write("\n	SYNTAX\n", color: ConsoleColor.White);
				Output.Write($"		{CommandManager.commands[args[1]].LongHelpMessage_Syntax()}\n");
				Output.Write("\n	FUNCTION\n", color: ConsoleColor.White);
				Output.Write($"		{CommandManager.commands[args[1]].LongHelpMessage_Function()}\n");
				Output.Write("\n	AUTHOR\n", color: ConsoleColor.White);
				Output.Write($"		{CommandManager.commands[args[1]].LongHelpMessage_Author()}\n");
				Output.Write("\n	ALSO SEE\n", color: ConsoleColor.White);
				Output.Write($"		{CommandManager.commands[args[1]].LongHelpMessage_SeeAlso()}\n");
			}
		}

		public string LongHelpMessage_Author()
		{
			return OSInfo.MAIN_APP_DEV;
		}

		public string LongHelpMessage_Function()
		{
			return "Shows a list of all commands and" +
				"\n		Help syntax:\n		* (statement?yes:no)" +
				"\n		* [optional]" +
				"\n		* <required>" +
				"\n		* this|or_this" +
				"\n		* empty";
		}

		public string LongHelpMessage_SeeAlso()
		{
			return "No SEE ALSO";
		}

		public string LongHelpMessage_Syntax()
		{
			return "help [command]";
		}

		public string ShortHelpMessage()
		{
			return "Displays all commands an their short descriptions";
		}
	}
}
