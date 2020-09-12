using GameefanOS.Utils.FileSystem;
using System.Collections.Generic;
using static GameefanOS.Structs;

namespace GameefanOS.Utils
{
	public class User
	{
		public static User rootUser;
		public static User fallbackUser;
		public SystemPermissions perms;
		public string name;
		public int userID;
		public int executeUserID;
		public List<int> groups = new List<int>();
		public string passwd = "";

		public static List<User> users = new List<User>();
		public static List<User> allUsers = new List<User>();
		public static int currentUser = 1000;

		public static int GetNextUserID()
		{
			return 1000 + users.Count;
		}

		public static void AddUser(User user)
		{
			users.Add(user);
			allUsers.Add(user);
			FSManager.ChangeDirectory("/");
			FSManager.ChangeDirectory("home");
			FSManager.AddFolder(user.name, user);
			FSManager.ChangeDirectory(user.name);
		}

		public static SystemGroups GetGroupNameFromGID(int id)
		{
			return (SystemGroups)id;
		}

		public static User FetchUser(string username)
		{
			for (int i = 0; i < allUsers.Count; i++)
			{
				if (allUsers[i].name == username)
				{
					return allUsers[i];
				}
			}
			Output.WriteError($"Couldn't fetch user '{username}', using {fallbackUser.name}({fallbackUser.userID}:{fallbackUser.executeUserID})\n");
			return fallbackUser;
		}

		public static User FetchUserID(int id)
		{
			for (int i = 0; i < allUsers.Count; i++)
			{
				if (allUsers[i].userID == id)
				{
					return allUsers[i];
				}
			}
			Output.WriteError($"Couldn't fetch user with ID '{id}', using {fallbackUser.name}({fallbackUser.userID}:{fallbackUser.executeUserID})\n");
			return fallbackUser;
		}

		public static User FetchUserEID(int eid)
		{
			for (int i = 0; i < allUsers.Count; i++)
			{
				if (allUsers[i].executeUserID == eid)
				{
					return allUsers[i];
				}
			}
			Output.WriteError($"Couldn't fetch user with EID '{eid}', using {fallbackUser.name}({fallbackUser.userID}:{fallbackUser.executeUserID})\n");
			return fallbackUser;
		}

		public static void Initialize()
		{
			fallbackUser = new User()
			{
				perms =
				{
					isDisplayedAsAdmin = false,
					canChangeSystemSettings = false,
					canDoSudo = false,
					isRoot = false,
					canSeeAllUsers = false
				},
				executeUserID = 1,
				groups =
				{
					(int) SystemGroups.TemporaryUser,
					(int) SystemGroups.FallbackUser,
				},
				name = "temp",
				userID = 1,
				passwd = "temp"
			};
			rootUser = new User()
			{
				perms =
				{
					isDisplayedAsAdmin = true,
					canChangeSystemSettings = true,
					canDoSudo = true,
					isRoot = true,
					canSeeAllUsers = true
				},
				executeUserID = 0,
				groups =
				{
					(int) SystemGroups.RootUser,
					(int) SystemGroups.SystemControlledUser,
				},
				name = "root",
				userID = 0,
				passwd = "root"
			};

			

			allUsers.Add(rootUser);
			allUsers.Add(fallbackUser);
		}
	}
}
