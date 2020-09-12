using System;
using System.Collections.Generic;
using System.Text;
using static GameefanOS.Structs;

namespace GameefanOS.Utils.FileSystem
{
	public class File
	{
		public string path = "~/";
		public string name = "File";
		public string data = "";
		public FilePerms perms;

		public File(string path, string name, string data, FilePerms perms)
		{
			this.path = path ?? throw new ArgumentNullException(nameof(path));
			this.name = name ?? throw new ArgumentNullException(nameof(name));
			this.data = data ?? throw new ArgumentNullException(nameof(data));
			this.perms = perms;
		}
	}
}
