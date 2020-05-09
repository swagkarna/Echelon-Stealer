using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Echelon
{
	public class JavaScriptReader
	{
		private readonly StringBuilder SBuilder;

		private readonly TextReader Reader;

		private int Line = 1;

		private int Column;

		private int Peek;

		private bool HasPeek;

		private bool Prev_Lf;

		public JavaScriptReader(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			Reader = reader;
			SBuilder = new StringBuilder();
		}

		public object Read()
		{
			object result = ReadCore();
			SkipSpaces();
			if (ReadChar() >= 0)
			{
				throw JsonError($"extra characters in JSON input");
			}
			return result;
		}

		private object ReadCore()
		{
			SkipSpaces();
			int num = PeekChar();
			if (num < 0)
			{
				throw JsonError("Incomplete JSON input");
			}
			switch (num)
			{
			case 91:
			{
				ReadChar();
				List<object> list = new List<object>();
				SkipSpaces();
				if (PeekChar() == 93)
				{
					ReadChar();
					return list;
				}
				while (true)
				{
					object item = ReadCore();
					list.Add(item);
					SkipSpaces();
					num = PeekChar();
					if (num != 44)
					{
						break;
					}
					ReadChar();
				}
				if (ReadChar() != 93)
				{
					throw JsonError("JSON array must end with ']'");
				}
				return list.ToArray();
			}
			case 123:
			{
				ReadChar();
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				SkipSpaces();
				if (PeekChar() == 125)
				{
					ReadChar();
					return dictionary;
				}
				do
				{
					SkipSpaces();
					if (PeekChar() == 125)
					{
						ReadChar();
						break;
					}
					string key = ReadStringLiteral();
					SkipSpaces();
					Expect(':');
					SkipSpaces();
					dictionary[key] = ReadCore();
					SkipSpaces();
					num = ReadChar();
				}
				while (num == 44 || num != 125);
				int num2 = 0;
				KeyValuePair<string, object>[] array = new KeyValuePair<string, object>[dictionary.Count];
				{
					foreach (KeyValuePair<string, object> item2 in dictionary)
					{
						array[num2++] = item2;
					}
					return array;
				}
			}
			case 116:
				Expect("true");
				return true;
			case 102:
				Expect("false");
				return false;
			case 110:
				Expect("null");
				return null;
			case 34:
				return ReadStringLiteral();
			default:
				if ((48 <= num && num <= 57) || num == 45)
				{
					return ReadNumericLiteral();
				}
				throw JsonError($"Unexpected character '{(char)num}'");
			}
		}

		private int PeekChar()
		{
			if (!HasPeek)
			{
				Peek = Reader.Read();
				HasPeek = true;
			}
			return Peek;
		}

		private int ReadChar()
		{
			int num = HasPeek ? Peek : Reader.Read();
			HasPeek = false;
			if (Prev_Lf)
			{
				Line++;
				Column = 0;
				Prev_Lf = false;
			}
			if (num == 10)
			{
				Prev_Lf = true;
			}
			Column++;
			return num;
		}

		private void SkipSpaces()
		{
			while (true)
			{
				int num = PeekChar();
				if ((uint)(num - 9) <= 1u || num == 13 || num == 32)
				{
					ReadChar();
					continue;
				}
				break;
			}
		}

		private object ReadNumericLiteral()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (PeekChar() == 45)
			{
				stringBuilder.Append((char)ReadChar());
			}
			int num = 0;
			bool flag = PeekChar() == 48;
			int num2;
			while (true)
			{
				num2 = PeekChar();
				if (num2 < 48 || 57 < num2)
				{
					break;
				}
				stringBuilder.Append((char)ReadChar());
				if (flag && num == 1)
				{
					throw JsonError("leading zeros are not allowed");
				}
				num++;
			}
			if (num == 0)
			{
				throw JsonError("Invalid JSON numeric literal; no digit found");
			}
			bool flag2 = false;
			int num3 = 0;
			if (PeekChar() == 46)
			{
				flag2 = true;
				stringBuilder.Append((char)ReadChar());
				if (PeekChar() < 0)
				{
					throw JsonError("Invalid JSON numeric literal; extra dot");
				}
				while (true)
				{
					num2 = PeekChar();
					if (num2 < 48 || 57 < num2)
					{
						break;
					}
					stringBuilder.Append((char)ReadChar());
					num3++;
				}
				if (num3 == 0)
				{
					throw JsonError("Invalid JSON numeric literal; extra dot");
				}
			}
			num2 = PeekChar();
			if (num2 != 101 && num2 != 69)
			{
				if (!flag2)
				{
					if (int.TryParse(stringBuilder.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out int result))
					{
						return result;
					}
					if (long.TryParse(stringBuilder.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out long result2))
					{
						return result2;
					}
					if (ulong.TryParse(stringBuilder.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out ulong result3))
					{
						return result3;
					}
				}
				if (decimal.TryParse(stringBuilder.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out decimal result4) && result4 != decimal.Zero)
				{
					return result4;
				}
			}
			else
			{
				stringBuilder.Append((char)ReadChar());
				if (PeekChar() < 0)
				{
					throw new ArgumentException("Invalid JSON numeric literal; incomplete exponent");
				}
				switch (PeekChar())
				{
				case 45:
					stringBuilder.Append((char)ReadChar());
					break;
				case 43:
					stringBuilder.Append((char)ReadChar());
					break;
				}
				if (PeekChar() < 0)
				{
					throw JsonError("Invalid JSON numeric literal; incomplete exponent");
				}
				while (true)
				{
					num2 = PeekChar();
					if (num2 < 48 || 57 < num2)
					{
						break;
					}
					stringBuilder.Append((char)ReadChar());
				}
			}
			return double.Parse(stringBuilder.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture);
		}

		private string ReadStringLiteral()
		{
			if (PeekChar() != 34)
			{
				throw JsonError("Invalid JSON string literal format");
			}
			ReadChar();
			SBuilder.Length = 0;
			while (true)
			{
				int num = ReadChar();
				if (num < 0)
				{
					break;
				}
				switch (num)
				{
				case 34:
					return SBuilder.ToString();
				default:
					SBuilder.Append((char)num);
					break;
				case 92:
					num = ReadChar();
					if (num < 0)
					{
						throw JsonError("Invalid JSON string literal; incomplete escape sequence");
					}
					switch (num)
					{
					case 34:
					case 47:
					case 92:
						SBuilder.Append((char)num);
						break;
					case 98:
						SBuilder.Append('\b');
						break;
					case 102:
						SBuilder.Append('\f');
						break;
					case 110:
						SBuilder.Append('\n');
						break;
					case 114:
						SBuilder.Append('\r');
						break;
					case 116:
						SBuilder.Append('\t');
						break;
					case 117:
					{
						ushort num2 = 0;
						for (int i = 0; i < 4; i++)
						{
							num2 = (ushort)(num2 << 4);
							if ((num = ReadChar()) < 0)
							{
								throw JsonError("Incomplete unicode character escape literal");
							}
							if (48 <= num && num <= 57)
							{
								num2 = (ushort)(num2 + (ushort)(num - 48));
							}
							if (65 <= num && num <= 70)
							{
								num2 = (ushort)(num2 + (ushort)(num - 65 + 10));
							}
							if (97 <= num && num <= 102)
							{
								num2 = (ushort)(num2 + (ushort)(num - 97 + 10));
							}
						}
						SBuilder.Append((char)num2);
						break;
					}
					default:
						throw JsonError("Invalid JSON string literal; unexpected escape character");
					}
					break;
				}
			}
			throw JsonError("JSON string is not closed");
		}

		private void Expect(char expected)
		{
			int num;
			if ((num = ReadChar()) != expected)
			{
				throw JsonError($"Expected '{expected}', got '{(char)num}'");
			}
		}

		private void Expect(string expected)
		{
			int num = 0;
			while (true)
			{
				if (num < expected.Length)
				{
					if (ReadChar() != expected[num])
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			throw JsonError($"Expected '{expected}', differed at {num}");
		}

		private Exception JsonError(string msg)
		{
			return new ArgumentException($"{msg}. At line {Line}, column {Column}");
		}
	}
}
