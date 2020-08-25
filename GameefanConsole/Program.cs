using System;
using System.Collections.Generic;

namespace GameefanConsole
{
	class Program
	{
		public static List<string> currentDirectory = new List<string>();
		public static List<File> files = new List<File>();

		public static List<string> history = new List<string>();

		public static string hostname = "host";
		public static string username = "user";

		static void Main(string[] args)
		{
			WriteConsoleColor($"USERNAME [{Environment.UserName}]: ", ConsoleColor.DarkYellow);
			username = Console.ReadLine();
			if (username == "") username = Environment.UserName;
			WriteConsoleColor($"HOSTNAME [{Environment.MachineName}]: ", ConsoleColor.DarkYellow);
			hostname = Console.ReadLine();
			if (hostname == "") hostname = Environment.MachineName;

			AddDirectory("home", "", true, flags: "SP");
			AddDirectory(username, "/home", true, flags: "SP");

			currentDirectory.Add("home");
			currentDirectory.Add(username);

			/*AddDirectory("a", $"/home/{username}", false);
			AddDirectory("b", $"/home/{username}/a", false);*/

			Console.Clear();

			/*
			string s = $"/home/{username}";
			for (int i = 0; i < 2000; i++)
			{
				AddDirectory($"{i}", $"{s}", false);
				currentDirectory.Add(i.ToString());
				Console.WriteLine(i.ToString());
				s += "/"+i;
			}*/


			AddDirectory("testing", "", true, flags: "SP");

			files.Add(new File()
			{
				canUserRead = true,
				canUserExecute = false,
				canUserWrite = true,
				data = "data1",
				flags = "F",
				name = "test1",
				ownerID = username,
				path = "/testing"
			});
			files.Add(new File()
			{
				canUserRead = true,
				canUserExecute = true,
				canUserWrite = true,
				data = "data2",
				flags = "F",
				name = "test2",
				ownerID = username,
				path = "/testing"
			});
			files.Add(new File()
			{
				canUserRead = false,
				canUserExecute = false,
				canUserWrite = false,
				data = "data3",
				flags = "F",
				name = "test3",
				ownerID = "stein",
				path = "/testing"
			});


			while (true)
			{
				string cmd = ReadShell(currentDirectory, username, hostname);
				string[] cmdArgs = cmd.Split(' ');
				/*for (int i = 0; i < cmdArgs.Length; i++)
				{
					Console.WriteLine($"{i}: {cmdArgs[i]}");
				}*/
				CheckCommand(cmdArgs, false);
			}
		}

		public static void CheckCommand(string[] args, bool sudo)
		{
			if (username == "root")
				sudo = true;

			bool dontAddHistory = false;

			switch (args[0].ToLower())
			{
				case "quit":
				case "exit":
					ExitCommand(args, sudo);
					break;
				case "mkdir":
				case "md":
					MkDirCommand(args, sudo);
					break;
				case "rmdir":
				case "rd":
					RmDirCommand(args, sudo);
					break;
				case "ls":
				case "dir":
					LsCommand(args, sudo);
					break;
				case "cd":
					CdCommand(args, sudo);
					break;
				case "sudo":
					string[] newArgs = new string[args.Length - 1];
					for (int i = 1; i < args.Length; i++)
					{
						newArgs[i - 1] = args[i];
					}
					CheckCommand(newArgs, true);
					break;
				case "del":
					DelCommand(args, sudo);
					break;
				case "filemod":
				case "fm":
				case "fmod":
					FileModCommand(args, sudo);
					break;
				case "cls":
				case "clear":
					Console.Clear();
					break;
				case "cat":
				case "ct":
					CatCommand(args, sudo);
					break;
				case "history":
				case "hist":
					HistoryCommand(args, sudo);
					break;
				case "new":
					NewCommand(args, sudo);
					break;
				case "":
					dontAddHistory = true;
					break;
				default:
					WriteConsoleError($"Command '{args[0]}' not found!\n");
					break;
			}
			if (dontAddHistory)
				return;
			string str = "";
			foreach (string s in args) { str += $"{s} "; }
			history.Add(str);
		}

		private static void NewCommand(string[] args, bool sudo)
		{
			if (args.Length != 2 && args.Length != 3)
			{
				WriteConsoleError($"Invalid arguments for command: {args[0]}\n");
				return;
			}
			files.Add(new File()
			{
				canUserRead = true,
				canUserExecute = false,
				canUserWrite = true,
				data = (args.Length == 2 ? "" : args[2]),
				flags = "F",
				name = args[1],
				ownerID = username,
				path = DirectoryListToString(currentDirectory)
			});
		}

		private static void HistoryCommand(string[] args, bool sudo)
		{
			if (args.Length == 2 && args[1] == "clear")
			{
				history.Clear();
				return;
			}

			for (int i = 0; i < history.Count; i++)
			{
				WriteConsoleColor($"	{i}", ConsoleColor.Yellow);
				WriteConsoleColor($": ", ConsoleColor.Gray);
				WriteConsoleColor($"{history[i]}\n", ConsoleColor.White);
			}
		}

		public static void CatCommand(string[] args, bool sudo)
		{
			if (args.Length < 2)
			{
				WriteConsoleError($"Invalid arguments for command: {args[0]}\n");
				return;
			}
			for (int i = 1; i < args.Length; i++)
			{
				foreach (File file in files)
				{
					if (file.name == args[i] && file.path == DirectoryListToString(currentDirectory))
					{
						if (file.canUserRead || sudo)
						{
							WriteConsole(file.data + "\n");
						}
						else
						{
							WriteConsoleError($"You dont have permission to read this file!\n");
						}
					}
				}
			}
		}

		private static void FileModCommand(string[] args, bool sudo)
		{
			if (args.Length != 4)
			{
				WriteConsoleError($"Invalid arguments for command: {args[0]}\n");
				return;
			}
			foreach (File file in files)
			{
				if ((file.path == DirectoryListToString(currentDirectory) && file.name == args[3]) && ((file.flags.Contains("S") && !sudo) || file.flags.Contains("P")))
				{
					WriteConsoleError($"'{args[3]}' is a critical system file! Deleting it may break your computer!\n");
					return;
				}
			}

			switch (args[1])
			{
				case "allow":
					foreach (File file in files) { if (file.path == DirectoryListToString(currentDirectory) && file.name == args[3] && !file.canUserWrite) return; }
					switch (args[2])
					{
						case "read":
							foreach (File file in files) { if (file.path == DirectoryListToString(currentDirectory) && file.name == args[3]) file.canUserRead = true; }
							break;
						case "write":
							foreach (File file in files) { if (file.path == DirectoryListToString(currentDirectory) && file.name == args[3]) file.canUserWrite = true; }
							break;
						case "execute":
							foreach (File file in files) { if (file.path == DirectoryListToString(currentDirectory) && file.name == args[3]) file.canUserExecute = true; }
							break;
						default:
							WriteConsoleError($"Invalid arguments for command: {args[0]}\n");
							return;
					}
					break;
				case "deny":
					foreach (File file in files) { if (file.path == DirectoryListToString(currentDirectory) && file.name == args[3] && !file.canUserWrite) return; }
					switch (args[2])
					{
						case "read":
							foreach (File file in files) { if (file.path == DirectoryListToString(currentDirectory) && file.name == args[3]) file.canUserRead = false; }
							break;
						case "write":
							foreach (File file in files) { if (file.path == DirectoryListToString(currentDirectory) && file.name == args[3]) file.canUserWrite = false; }
							break;
						case "execute":
							foreach (File file in files) { if (file.path == DirectoryListToString(currentDirectory) && file.name == args[3]) file.canUserExecute = false; }
							break;
						default:
							WriteConsoleError($"Invalid arguments for command: {args[0]}\n");
							return;
					}
					break;
				case "check":
					foreach (File file in files) { if (file.path == DirectoryListToString(currentDirectory) && file.name == args[3] && !file.canUserRead) return; }
					switch (args[2])
					{
						case "read":
							foreach (File file in files) { if (file.path == DirectoryListToString(currentDirectory) && file.name == args[3]) WriteConsole($"READ value of {file.name} is set to {file.canUserRead}\n"); }
							break;
						case "write":
							foreach (File file in files) { if (file.path == DirectoryListToString(currentDirectory) && file.name == args[3]) WriteConsole($"WRITE value of {file.name} is set to {file.canUserWrite}\n"); }
							break;
						case "execute":
							foreach (File file in files) { if (file.path == DirectoryListToString(currentDirectory) && file.name == args[3]) WriteConsole($"EXECUTE value of {file.name} is set to {file.canUserExecute}\n"); }
							break;
						default:
							WriteConsoleError($"Invalid arguments for command: {args[0]}\n");
							return;
					}
					break;
				case "owner":
					foreach (File file in files) { if (file.path == DirectoryListToString(currentDirectory) && file.name == args[3] && !file.canUserWrite && sudo) return; }
					switch (args[2])
					{
						case "set":
							foreach (File file in files) { if (file.path == DirectoryListToString(currentDirectory) && file.name == args[3]) file.ownerID = args[2]; }
							break;
						default:
							WriteConsoleError($"Invalid arguments for command: {args[0]}\n");
							return;
					}
					break;
				default:
					WriteConsoleError($"Invalid arguments for command: {args[0]}\n");
					return;
			}

		}

		private static void DelCommand(string[] args, bool sudo)
		{
			if (args.Length != 2)
			{
				WriteConsoleError($"Invalid arguments for command: {args[0]}\n");
				return;
			}
			int cnt = files.Count;
			for (int i = 0; i < cnt; i++)
			{
				if (files[i].path == DirectoryListToString(currentDirectory) && files[i].name == args[1])
				{
					if (files[i].canUserWrite == true || sudo)
					{
						if (files[i].flags.Contains("P"))
						{
							WriteConsoleError($"'{args[1]}' is a critical system file! Deleting it may break your computer!\n");
							return;
						}
						if (files[i].flags.Contains("S"))
						{
							WriteConsoleWarning("You are trying to delete a system file! Doing this may break the computer!\nPress Y to continue or any other key to cancel!\n");
							if (Console.ReadKey(true).Key != ConsoleKey.Y)
							{
								return;
							}
						}
						files.Remove(files[i]);
					}
					else
					{
						WriteConsoleError($"You don't have permission to delete {files[i].name}\n");
					}
					return;
				}
			}
			WriteConsoleError($"File '{args[1]}' not found!\n");
		}

		private static void RmDirCommand(string[] args, bool sudo)
		{
			if (args.Length != 2)
			{
				WriteConsoleError($"Invalid arguments for command: {args[0]}\n");
				return;
			}
			bool dirExists = false;
			List<File> filesToDelete = new List<File>();
			int cnt = files.Count;
			foreach (File file in files)
			{
				if (file.path == $"{DirectoryListToString(currentDirectory)}/{args[1]}" && file.name == "folder.sys")
				{
					if (file.flags.Contains("P"))
					{
						WriteConsoleError($"'{args[1]}' is a critical system folder! Deleting it may break your computer!\n");
						return;
					}
				}
				//Console.WriteLine($"{file.name} {file.path}");
				if (file.path.Equals($"{DirectoryListToString(currentDirectory)}/{args[1]}"))
				{
					dirExists = true;
					filesToDelete.Add(file);
					//cnt--;
				}

			}
			foreach (File file in filesToDelete)
				files.Remove(file);
			if (!dirExists)
				WriteConsoleError($"Directory '{args[1]}' not found!\n");
		}

		private static void MkDirCommand(string[] args, bool sudo)
		{
			if (args.Length != 2)
			{
				WriteConsoleError($"Invalid arguments for command: {args[0]}\n");
				return;
			}
			AddDirectory(args[1], DirectoryListToString(currentDirectory), sudo);
		}

		public static void AddDirectory(string name, string path, bool sudo, string data = "", string flags = "S", bool read = false, bool write = false, bool exec = false)
		{
			foreach (File file in files)
			{
				if (file.path == $"{path}/{name}")
				{
					WriteConsoleError($"Directory '{name}' already exists!\n");
					return;
				}
			}
			files.Add(new File()
			{
				canUserRead = read,
				canUserExecute = exec,
				canUserWrite = write,
				data = data,
				name = "folder.sys",
				path = $"{path}/{name}",
				flags = flags
			});
		}

		public static void LsCommand(string[] args, bool sudo)
		{
			List<string> seenFolders = new List<string>();
			//WriteConsole("PERMS    FLAGS    OWNER    NAME\n");
			WriteConsoleColor("\nPERS", ConsoleColor.DarkGreen);
			WriteConsoleColor("	FLAGS", ConsoleColor.Cyan);
			WriteConsoleColor("	OWNER", ConsoleColor.DarkYellow);
			WriteConsoleColor("	NAME\n", ConsoleColor.DarkRed);
			foreach (File file in files)
			{
				if (file.path == DirectoryListToString(currentDirectory))
				{
					//WriteConsole($"{(file.canUserRead ? "r" : "-")}{(file.canUserWrite ? "w" : "-")}{(file.canUserExecute ? "x" : "-")}	{file.flags}	{file.ownerID}	{file.name}\n");
					WriteConsoleColor($"{(file.canUserRead ? "r" : "-")}", file.canUserRead ? ConsoleColor.DarkGreen : ConsoleColor.Red);
					WriteConsoleColor($"{(file.canUserWrite ? "w" : "-")}", file.canUserRead ? ConsoleColor.DarkGreen : ConsoleColor.Red);
					WriteConsoleColor($"{(file.canUserExecute ? "x" : "-")}", file.canUserRead ? ConsoleColor.DarkGreen : ConsoleColor.Red);
					WriteConsoleColor($"	{file.flags}", ConsoleColor.Cyan);
					WriteConsoleColor($"	{file.ownerID}", ConsoleColor.DarkYellow);
					WriteConsoleColor($"	{file.name}\n", ConsoleColor.DarkRed);
				}

				//WriteConsoleColor(file.path.Remove(0, 1)+"\n", ConsoleColor.Magenta);


			}
			WriteConsoleColor("\nFOLDERS\n", ConsoleColor.DarkGreen);
			foreach (File file in files)
			{
				if (file.path.StartsWith(DirectoryListToString(currentDirectory)) && !file.path.EndsWith(DirectoryListToString(currentDirectory)) && !seenFolders.Contains(file.path) && !file.path.Remove(0, DirectoryListToString(currentDirectory).ToCharArray().Length).Remove(0, 1).Contains("/"))
				{
					if (DirectoryListToString(currentDirectory) == "/")
						WriteConsoleColor($"{file.path.Remove(0, DirectoryListToString(currentDirectory).ToCharArray().Length)}/\n", ConsoleColor.DarkCyan);
					else
						WriteConsoleColor($"{file.path.Remove(0, DirectoryListToString(currentDirectory).ToCharArray().Length + 1)}/\n", ConsoleColor.DarkCyan);
					seenFolders.Add(file.path);
				}
			}
		}

		public static void CdCommand(string[] args, bool sudo)
		{
			if (args.Length == 1)
			{
				int a = currentDirectory.Count;
				for (int i = 0; i < a; i++)
				{
					currentDirectory.RemoveAt(0);
				}
				currentDirectory.Add("home");
				currentDirectory.Add(username);
			}
			else if (args.Length == 2)
			{

				switch (args[1])
				{
					case ".":
						break;
					case "/":
						int a = currentDirectory.Count;
						for (int i = 0; i < a; i++)
						{
							currentDirectory.RemoveAt(0);
						}
						break;
					case "..":
						if (currentDirectory.Count > 0)
							currentDirectory.RemoveAt(currentDirectory.Count - 1);
						else
							WriteConsoleError($"No higher directories found!\n");
						break;
					default:
						bool dirExists = false;
						foreach (File file in files)
						{
							//Console.WriteLine(file.path + " | "+ $"{DirectoryListToString(currentDirectory)}/{args[1]}");
							if (file.path == $"{DirectoryListToString(currentDirectory)}/{args[1]}".Remove(0, ((currentDirectory.Count == 0) ? 1 : 0)))
							{
								dirExists = true;
								//Console.WriteLine("NOW!");
							}
						}
						if (!dirExists)
						{
							WriteConsoleError($"Directory '{args[1]}' doesn't exist!\n");
							return;
						}
						currentDirectory.Add(args[1]);
						break;
				}
			}
			else
			{
				WriteConsoleError($"Invalid arguments for command: {args[0]}\n");
			}
		}

		public static void ExitCommand(string[] args, bool sudo)
		{
			WriteConsole("Quitting...\n");
			Environment.Exit(0);
		}

		public static void WriteConsole(string s)
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write($"{s}");
			Console.ForegroundColor = ConsoleColor.Gray;
		}
		public static void WriteConsoleWarning(string s)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write($"WARNING: {s}");
			Console.ForegroundColor = ConsoleColor.Gray;
		}
		public static void WriteConsoleError(string s)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write($"ERROR: {s}");
			Console.ForegroundColor = ConsoleColor.Gray;
		}

		public static void WriteConsoleColor(string s, ConsoleColor color)
		{
			Console.ForegroundColor = color;
			Console.Write($"{s}");
			Console.ForegroundColor = ConsoleColor.Gray;
		}

		public static string DirectoryListToString(List<string> list)
		{
			string s = "";

			if (list.Count <= 0)
			{
				return "/";
			}

			foreach (string str in list)
			{
				s += $"/{str}";
			}
			return s;
		}

		public static string ReadShell(List<string> directory, string user, string host)
		{
			WriteShell(directory, user, host);
			return Console.ReadLine();
		}

		public static void WriteShell(List<string> directory, string user, string host)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write($"{user}@{host}");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write($":");
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.Write($"{DirectoryListToString(directory)}");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write((user == "root") ? "#" : "$");
			Console.Write(" ");
		}
	}
}
