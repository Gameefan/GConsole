using GameefanOS.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using static GameefanOS.Structs;

namespace GameefanOS.Interfaces
{
	public interface ICommand
	{
		public void Execute(string[] args, User user);
		public string ShortHelpMessage();
		public string LongHelpMessage_Syntax();
		public string LongHelpMessage_Function();
		public string LongHelpMessage_Author();
		public string LongHelpMessage_SeeAlso();
	}
}
