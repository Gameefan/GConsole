using System;
using System.Collections.Generic;
using System.Text;
using static GameefanOS.Structs;

namespace GameefanOS.Utils.FileSystem
{
	public static class FSManager
	{
		public static List<File> files = new List<File>();
		public static List<string> currentDirectory = new List<string>();
		public static void AddFile(FilePerms perms, string path = "~/", string name = "File", string data = "", int id = 0)
		{
			for (int i = 0; i < files.Count; i++)
			{
				if (files[i].name==name&&files[i].path == path)
				{
					Output.WriteError($"File '{name}' already exists!\n");
					return;
				}
			}

			files.Add(new File(path, name, data, perms));
		}
		public static void GetChildren()
		{
			foreach (File file in files)
			{
				Output.WriteDebug($"{file.path} | {file.name} | OW:{file.perms.ow} OR:{file.perms.or} OX:{file.perms.ox} | AW:{file.perms.ar} AR:{file.perms.aw} AX:{file.perms.ax} | {file.perms.owner}\n");
			}
		}
		public static void AddFolder(string name,User owner)
		{
			bool folderExists = false;
			for (int i = 0; i < files.Count; i++)
			{
				if (files[i].path.StartsWith(DirListToString(currentDirectory) + name + "/"))
				{
					folderExists = true;
				}
			}
			if(folderExists)
			{
				Output.WriteError("Directory already exists!\n");
				return;
			}
			AddFile(new FilePerms
			{
				ar = false,
				aw = false,
				ax = false,
				flag = FileFlag.Directory,
				or = true,
				os = false,
				ow = true,
				ox = false,
				owner=0
			}, path:$"{DirListToString(currentDirectory)}{name}/", name:"folder.sys", data:$"{owner.userID}");
		}
		public static void RemoveFolder(string name)
		{
			List<File> filesToRemove = new List<File>();
			for (int i = 0; i < files.Count; i++)
			{
				if (files[i].path.StartsWith(DirListToString(currentDirectory)+name+"/"))
				{
					filesToRemove.Add(files[i]);
				}
			}
			foreach (File file in filesToRemove)
			{
				files.Remove(file);
			}
		}
		public static void RemoveFile(string name)
		{
			foreach (File file in files)
			{
				if (file.name == name && file.path == DirListToString(currentDirectory))
				{
					files.Remove(file);
					break;
				}
			}
		}
		public static string CatFile(string fileName)
		{
			string data = string.Empty;
			foreach (File file in files)
			{
				if(file.name==fileName&&file.path==DirListToString(currentDirectory))
				{
					data = file.data;
					break;
				}
			}
			return data;
		}
		public static string DirListToString(List<string> list)
		{
			string str = "~/";
			foreach (string s in list)
			{
				str += s;
				str += "/";
			}
			return str;
		}
		public static FilePerms GetFilePerms(string name)
		{
			FilePerms fp = new FilePerms() { flag = FileFlag.DoesntExists };
			foreach (File file in files)
			{
				if(file.name==name&&file.path== DirListToString(currentDirectory))
				{
					fp = file.perms;
				}
			}
			return fp;
		}
		public static bool ChangeDirectory(string str)
		{
			if (str == "..")
			{
				currentDirectory.Reverse();
				currentDirectory.RemoveAt(0);
				currentDirectory.Reverse();
				return true;
			}
			if (str == "/")
			{
				currentDirectory.Clear();
				return true;
			}
			foreach (File file in files)
			{
				if (file.path == DirListToString(currentDirectory)+str+"/")
				{
					currentDirectory.Add(str);
					return true;
				}
			}
			Output.WriteError($"Directory '{str}/ doesn't exist!\n");
			return false;
		}

		public static string PresentWorkingDirectory()
		{
			return DirListToString(currentDirectory);
		}
	}
}
