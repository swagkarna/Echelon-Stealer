using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Echelon
{
	public class JsonPrimitive : JsonValue
	{
		private object value;

		private static readonly byte[] true_bytes = Encoding.UTF8.GetBytes("true");

		private static readonly byte[] false_bytes = Encoding.UTF8.GetBytes("false");

		public object Value => value;

		public override JsonType JsonType
		{
			get
			{
				if (value == null)
				{
					return JsonType.String;
				}
				switch (Type.GetTypeCode(value.GetType()))
				{
				case TypeCode.Boolean:
					return JsonType.Boolean;
				case TypeCode.Object:
				case TypeCode.Char:
				case TypeCode.DateTime:
				case TypeCode.String:
					return JsonType.String;
				default:
					return JsonType.Number;
				}
			}
		}

		public JsonPrimitive(bool value)
		{
			this.value = value;
		}

		public JsonPrimitive(byte value)
		{
			this.value = value;
		}

		public JsonPrimitive(char value)
		{
			this.value = value;
		}

		public JsonPrimitive(decimal value)
		{
			this.value = value;
		}

		public JsonPrimitive(double value)
		{
			this.value = value;
		}

		public JsonPrimitive(float value)
		{
			this.value = value;
		}

		public JsonPrimitive(int value)
		{
			this.value = value;
		}

		public JsonPrimitive(long value)
		{
			this.value = value;
		}

		public JsonPrimitive(sbyte value)
		{
			this.value = value;
		}

		public JsonPrimitive(short value)
		{
			this.value = value;
		}

		public JsonPrimitive(string value)
		{
			this.value = value;
		}

		public JsonPrimitive(DateTime value)
		{
			this.value = value;
		}

		public JsonPrimitive(uint value)
		{
			this.value = value;
		}

		public JsonPrimitive(ulong value)
		{
			this.value = value;
		}

		public JsonPrimitive(ushort value)
		{
			this.value = value;
		}

		public JsonPrimitive(DateTimeOffset value)
		{
			this.value = value;
		}

		public JsonPrimitive(Guid value)
		{
			this.value = value;
		}

		public JsonPrimitive(TimeSpan value)
		{
			this.value = value;
		}

		public JsonPrimitive(Uri value)
		{
			this.value = value;
		}

		public JsonPrimitive(object value)
		{
			this.value = value;
		}

		public override void Save(Stream stream, bool parsing)
		{
			switch (JsonType)
			{
			case JsonType.Boolean:
				if ((bool)value)
				{
					stream.Write(true_bytes, 0, 4);
				}
				else
				{
					stream.Write(false_bytes, 0, 5);
				}
				break;
			case JsonType.String:
			{
				stream.WriteByte(34);
				byte[] bytes = Encoding.UTF8.GetBytes(EscapeString(value.ToString()));
				stream.Write(bytes, 0, bytes.Length);
				stream.WriteByte(34);
				break;
			}
			default:
			{
				byte[] bytes = Encoding.UTF8.GetBytes(GetFormattedString());
				stream.Write(bytes, 0, bytes.Length);
				break;
			}
			}
		}

		public string GetFormattedString()
		{
			switch (JsonType)
			{
			case JsonType.String:
				if (value is string || value == null)
				{
					string text2 = value as string;
					if (string.IsNullOrEmpty(text2))
					{
						return "null";
					}
					return text2.Trim('"');
				}
				if (value is char)
				{
					return value.ToString();
				}
				throw new NotImplementedException("GetFormattedString from value type " + value.GetType());
			case JsonType.Number:
			{
				string text = (!(value is float) && !(value is double)) ? ((IFormattable)value).ToString("G", NumberFormatInfo.InvariantInfo) : ((IFormattable)value).ToString("R", NumberFormatInfo.InvariantInfo);
				if (text == "NaN" || text == "Infinity" || text == "-Infinity")
				{
					return "\"" + text + "\"";
				}
				return text;
			}
			default:
				throw new InvalidOperationException();
			}
		}
	}
}
