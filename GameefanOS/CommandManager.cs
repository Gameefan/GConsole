using System;
using System.Collections.Generic;
using System.Text;
using GameefanOS.Commands;
using GameefanOS.Interfaces;
using static GameefanOS.Structs;
using GameefanOS.Utils;
using GameefanOS.Commands.Debug;
using GameefanOS.Commands.FileSysten;
using GameefanOS.Commands.Admin;

namespace GameefanOS
{
	public static class CommandManager
	{
		public static Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();

		public static bool debugEnabled = false;

		public static string currentCommand = "";

		public static bool FetchCommand(string cmd, User user)
		{
			string[] args = cmd.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			if (args.Length <= 0)
				return false;
			if(commands.ContainsKey(args[0].ToLower()))
			{
				currentCommand = args[0].ToLower();
				commands[args[0].ToLower()].Execute(args, user);
				currentCommand = "";
				return true;
			}else
			{
				Output.WriteError($"Couldn't find command: {args[0]}\n");
				return false;
			}
		}

		public static void SetupCommands()
		{
			commands.Add("echo", new EchoCommand());
			commands.Add("exit", new ExitCommand());
			commands.Add("quit", new ExitCommand());
			commands.Add("beep", new BeepCommand());
			commands.Add("adduser", new AddUserCommand());
			commands.Add("useradd", new AddUserCommand());
			commands.Add("listusers", new ListUsersCommand());
			commands.Add("listuser", new ListUsersCommand());
			commands.Add("userlist", new ListUsersCommand());
			commands.Add("userslist", new ListUsersCommand());
			commands.Add("whoami", new WhoAmICommand());
			commands.Add("id", new WhoAmICommand());
			commands.Add("help", new HelpCommand());
			commands.Add("man", new HelpCommand());
			commands.Add("fs", new FSCommand());
			commands.Add("pwd", new PWDCommand());
			commands.Add("addfile", new AddFileCommand());
			commands.Add("fileadd", new AddFileCommand());
			commands.Add("del", new DelCommand());
			commands.Add("delfile", new DelCommand());
			commands.Add("filedel", new DelCommand());
			commands.Add("cat", new CatCommand());
			commands.Add("cd", new CdCommand());
			commands.Add("mkdir", new MkDirCommand());
			commands.Add("rmdir", new RmDirCommand());
			commands.Add("ls", new LsCommand());
			commands.Add("debug", new DebugCommand());
			commands.Add("sudo", new SudoCommand());
			commands.Add("su", new SuCommand());
			commands.Add("sysinfo", new SysInfoCommand());
		}
	}
}
