using GameefanOS.Interfaces;
using GameefanOS.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameefanOS.Commands
{
	public class ExitCommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			if (args.Length == 2)
			{
				try
				{
					Environment.Exit(int.Parse(args[1]));
				}
				catch
				{
					Output.WriteError($"'{args[1]}' is not a valid exit code!\n");
				}
				
			}else
			{
				Environment.Exit(0);
			}
		}

		public string LongHelpMessage_Author()
		{
			return OSInfo.OS_CREATOR;
		}

		public string LongHelpMessage_Function()
		{
			return "Performs a shutdown with an optional exit code";
		}

		public string LongHelpMessage_SeeAlso()
		{
			return "No SEE ALSO";
		}

		public string LongHelpMessage_Syntax()
		{
			return "exit [exitcode]";
		}

		public string ShortHelpMessage()
		{
			return "Shutdowns the system";
		}
	}
}
