using GameefanOS.Interfaces;
using GameefanOS.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameefanOS.Commands
{
	public class BeepCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			if (args.Length == 2)
			{
				try
				{
					for (int i = 0; i < int.Parse(args[1]); i++)
					{
						Console.Beep();
					}
				}catch
				{
					Output.WriteError($"'{args[1]}' is not an integer!\n");
				}
			}
			else
			{
				Console.Beep();
			}
		}

		public string LongHelpMessage_Author()
		{
			return OSInfo.MAIN_APP_DEV;
		}

		public string LongHelpMessage_Function()
		{
			return "Beeps";
		}

		public string LongHelpMessage_SeeAlso()
		{
			return "echo";
		}

		public string LongHelpMessage_Syntax()
		{
			return "beep [count]";
		}

		public string ShortHelpMessage()
		{
			return "Beeps";
		}
	}
}
