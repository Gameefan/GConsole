using GameefanOS.Utils;
using System;
using GameefanOS.Utils.FileSystem;
using static GameefanOS.Structs;

namespace GameefanOS
{
	class Program
	{
		static void Main(string[] args)
		{
			User.Initialize();
			CommandManager.SetupCommands();

			FSManager.AddFile(new FilePerms
			{
				ar = false,
				aw = false,
				ax = false,
				flag = FileFlag.Directory,
				or = true,
				os = false,
				ow = true,
				ox = false,
				owner = 0
			}, path: $"~/", name: "folder.sys", data:"1000");
			FSManager.AddFolder("test", User.FetchUserID(0));
			FSManager.AddFolder("home", User.FetchUserID(0));
			User.AddUser(new User()
			{
				perms =
				{
					isDisplayedAsAdmin = true,
					canChangeSystemSettings = true,
					canDoSudo = true,
					isRoot = false,
					canSeeAllUsers = true
				},
				executeUserID = User.GetNextUserID(),
				groups =
				{
					(int) SystemGroups.Admin,
					(int) SystemGroups.NormalUser,
				},
				name = "admin",
				userID = User.GetNextUserID(),
				passwd = "admin"
			});
			//FSManager.ChangeDirectory("test");
			//FSManager.GetChildren();
			while (true)
			{
				Output.Write($"SHELL({User.FetchUserID(User.currentUser).name}) {FSManager.PresentWorkingDirectory()} $ ");
				string cmd = Console.ReadLine();
				CommandManager.FetchCommand(cmd, User.FetchUserID(User.currentUser));
			}
		}
	}
}
