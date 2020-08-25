using System;
using System.Collections.Generic;
using System.Text;

namespace GameefanConsole
{
	public class File
	{
		public string path = "/";
		public string name = "New File";
		public string data;
		public bool canUserRead = true;
		public bool canUserWrite = true;
		public bool canUserExecute = false;
		public string flags = "F";
		public string ownerID = "root";
	}
}
