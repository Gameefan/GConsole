using GameefanOS.Interfaces;
using GameefanOS.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameefanOS.Commands
{
	public class WhoAmICommand : ICommand
	{
		public void Execute(string[] args, User user)
		{
			Output.Write($"name={user.name},id={user.userID},eid={user.executeUserID},groups=");
			foreach (int gid in user.groups)
			{
				Output.Write($"{gid}({User.GetGroupNameFromGID(gid).ToString()}),");
			}
			Output.Write("\n");
		}

		public string LongHelpMessage_Author()
		{
			return OSInfo.OS_CREATOR;
		}

		public string LongHelpMessage_Function()
		{
			return "Shows info about the current user";
		}

		public string LongHelpMessage_SeeAlso()
		{
			return "adduser; listusers";
		}

		public string LongHelpMessage_Syntax()
		{
			return "whoami";
		}

		public string ShortHelpMessage()
		{
			return "Displays info about the user";
		}
	}
}
