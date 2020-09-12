using System;
using System.Collections.Generic;
using System.Text;

namespace GameefanOS
{
	public static class Structs
	{
		public struct SystemPermissions
		{
			public bool isDisplayedAsAdmin;
			public bool isRoot;
			public bool canChangeSystemSettings;
			public bool canDoSudo;
			public bool canSeeAllUsers;
		}
		public enum SystemGroups
		{
			Null = -1,
			RootUser = 0,
			SystemControlledUser = 1,
			TemporaryUser = 2,
			FallbackUser = 3,
			NormalUser = 4,
			Admin = 5
		}
		public struct FilePerms
		{
			public bool or;
			public bool ow;
			public bool ox;
			public bool ar;
			public bool aw;
			public bool ax;

			public bool os;

			public int owner;

			public FileFlag flag;
		}
		public enum FileFlag
		{
			None = 0,
			Directory = 1,
			System = 2,
			DoesntExists = 3
		}
	}
}
