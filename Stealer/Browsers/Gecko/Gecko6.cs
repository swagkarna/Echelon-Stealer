using System.Security.Cryptography;
using System.Text;

namespace Echelon
{
	public static class Gecko6
	{
		public static string lTRjlt(byte[] key, byte[] iv, byte[] input, PaddingMode paddingMode = PaddingMode.None)
		{
			using (TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider())
			{
				tripleDESCryptoServiceProvider.Key = key;
				tripleDESCryptoServiceProvider.IV = iv;
				tripleDESCryptoServiceProvider.Mode = CipherMode.CBC;
				tripleDESCryptoServiceProvider.Padding = paddingMode;
				using (ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor(key, iv))
				{
					return Encoding.Default.GetString(cryptoTransform.TransformFinalBlock(input, 0, input.Length));
				}
			}
		}
	}
}
