using System.Security.Cryptography;
using System.Text;

namespace GameefanOS.Utils
{
	public static class Crypto
	{
		public static string MD5(string sSourceData)
		{
			byte[] tmpSource;
			byte[] tmpHash;
			sSourceData = "MySourceData";

			//Create a byte array from source data.
			tmpSource = ASCIIEncoding.ASCII.GetBytes(sSourceData);
			tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

			return tmpHash.ToString();
		}
	}
}
