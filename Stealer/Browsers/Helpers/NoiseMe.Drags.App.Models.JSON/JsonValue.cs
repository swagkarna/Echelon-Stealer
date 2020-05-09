using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace Echelon
{
	public abstract class JsonValue : IEnumerable
	{
		public virtual int Count
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		public abstract JsonType JsonType
		{
			get;
		}

		public virtual JsonValue this[int index]
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public virtual JsonValue this[string key]
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public static JsonValue Load(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			return Load(new StreamReader(stream, detectEncodingFromByteOrderMarks: true));
		}

		public static JsonValue Load(TextReader textReader)
		{
			if (textReader == null)
			{
				throw new ArgumentNullException("textReader");
			}
			return ToJsonValue(new JavaScriptReader(textReader).Read());
		}

		private static IEnumerable<KeyValuePair<string, JsonValue>> ToJsonPairEnumerable(IEnumerable<KeyValuePair<string, object>> kvpc)
		{
			foreach (KeyValuePair<string, object> item in kvpc)
			{
				yield return new KeyValuePair<string, JsonValue>(item.Key, ToJsonValue(item.Value));
			}
		}

		private static IEnumerable<JsonValue> ToJsonValueEnumerable(IEnumerable arr)
		{
			foreach (object item in arr)
			{
				yield return ToJsonValue(item);
			}
		}

		public static JsonValue ToJsonValue<T>(T ret)
		{
			if (ret == null)
			{
				return null;
			}
			T val;
			if ((val = ret) is bool)
			{
				bool value = (bool)(object)val;
				return new JsonPrimitive(value);
			}
			if ((val = ret) is byte)
			{
				byte value2 = (byte)(object)val;
				return new JsonPrimitive(value2);
			}
			if ((val = ret) is char)
			{
				char value3 = (char)(object)val;
				return new JsonPrimitive(value3);
			}
			if ((val = ret) is decimal)
			{
				decimal value4 = (decimal)(object)val;
				return new JsonPrimitive(value4);
			}
			if ((val = ret) is double)
			{
				double value5 = (double)(object)val;
				return new JsonPrimitive(value5);
			}
			if ((val = ret) is float)
			{
				float value6 = (float)(object)val;
				return new JsonPrimitive(value6);
			}
			if ((val = ret) is int)
			{
				int value7 = (int)(object)val;
				return new JsonPrimitive(value7);
			}
			if ((val = ret) is long)
			{
				long value8 = (long)(object)val;
				return new JsonPrimitive(value8);
			}
			if ((val = ret) is sbyte)
			{
				sbyte value9 = (sbyte)(object)val;
				return new JsonPrimitive(value9);
			}
			if ((val = ret) is short)
			{
				short value10 = (short)(object)val;
				return new JsonPrimitive(value10);
			}
			string value11;
			if ((value11 = (ret as string)) != null)
			{
				return new JsonPrimitive(value11);
			}
			if ((val = ret) is uint)
			{
				uint value12 = (uint)(object)val;
				return new JsonPrimitive(value12);
			}
			if ((val = ret) is ulong)
			{
				ulong value13 = (ulong)(object)val;
				return new JsonPrimitive(value13);
			}
			if ((val = ret) is ushort)
			{
				ushort value14 = (ushort)(object)val;
				return new JsonPrimitive(value14);
			}
			if ((val = ret) is DateTime)
			{
				DateTime value15 = (DateTime)(object)val;
				return new JsonPrimitive(value15);
			}
			if ((val = ret) is DateTimeOffset)
			{
				DateTimeOffset value16 = (DateTimeOffset)(object)val;
				return new JsonPrimitive(value16);
			}
			if ((val = ret) is Guid)
			{
				Guid value17 = (Guid)(object)val;
				return new JsonPrimitive(value17);
			}
			if ((val = ret) is TimeSpan)
			{
				TimeSpan value18 = (TimeSpan)(object)val;
				return new JsonPrimitive(value18);
			}
			Uri value19;
			if ((object)(value19 = (ret as Uri)) != null)
			{
				return new JsonPrimitive(value19);
			}
			IEnumerable<KeyValuePair<string, object>> enumerable = ret as IEnumerable<KeyValuePair<string, object>>;
			if (enumerable != null)
			{
				return new JsonObject(ToJsonPairEnumerable(enumerable));
			}
			IEnumerable enumerable2 = ret as IEnumerable;
			if (enumerable2 != null)
			{
				return new JsonArray(ToJsonValueEnumerable(enumerable2));
			}
			if (!(ret is IEnumerable))
			{
				PropertyInfo[] properties = ret.GetType().GetProperties();
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				PropertyInfo[] array = properties;
				foreach (PropertyInfo propertyInfo in array)
				{
					dictionary.Add(propertyInfo.Name, propertyInfo.GetValue(ret, null).IsNull("null"));
				}
				if (dictionary.Count > 0)
				{
					return new JsonObject(ToJsonPairEnumerable(dictionary));
				}
			}
			throw new NotSupportedException($"Unexpected parser return type: {ret.GetType()}");
		}

		public static JsonValue Parse(string jsonString)
		{
			if (jsonString == null)
			{
				throw new ArgumentNullException("jsonString");
			}
			return Load(new StringReader(jsonString));
		}

		public virtual bool ContainsKey(string key)
		{
			throw new InvalidOperationException();
		}

		public virtual void Save(Stream stream, bool parsing)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			Save(new StreamWriter(stream), parsing);
		}

		public virtual void Save(TextWriter textWriter, bool parsing)
		{
			if (textWriter == null)
			{
				throw new ArgumentNullException("textWriter");
			}
			Savepublic(textWriter, parsing);
		}

		private void Savepublic(TextWriter w, bool saving)
		{
			switch (JsonType)
			{
			case JsonType.Object:
			{
				w.Write('{');
				bool flag = false;
				foreach (KeyValuePair<string, JsonValue> item in (JsonObject)this)
				{
					if (flag)
					{
						w.Write(", ");
					}
					w.Write('"');
					w.Write(EscapeString(item.Key));
					w.Write("\": ");
					if (item.Value == null)
					{
						w.Write("null");
					}
					else
					{
						item.Value.Savepublic(w, saving);
					}
					flag = true;
				}
				w.Write('}');
				break;
			}
			case JsonType.Array:
			{
				w.Write('[');
				bool flag = false;
				foreach (JsonValue item2 in (IEnumerable<JsonValue>)(JsonArray)this)
				{
					if (flag)
					{
						w.Write(", ");
					}
					if (item2 != null)
					{
						item2.Savepublic(w, saving);
					}
					else
					{
						w.Write("null");
					}
					flag = true;
				}
				w.Write(']');
				break;
			}
			case JsonType.Boolean:
				w.Write(this ? "true" : "false");
				break;
			case JsonType.String:
				if (saving)
				{
					w.Write('"');
				}
				w.Write(EscapeString(((JsonPrimitive)this).GetFormattedString()));
				if (saving)
				{
					w.Write('"');
				}
				break;
			default:
				w.Write(((JsonPrimitive)this).GetFormattedString());
				break;
			}
		}

		public string ToString(bool saving = true)
		{
			StringWriter stringWriter = new StringWriter();
			Save(stringWriter, saving);
			return stringWriter.ToString();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new InvalidOperationException();
		}

		private bool NeedEscape(string src, int i)
		{
			char c = src[i];
			if (c >= ' ' && c != '"' && c != '\\' && (c < '\ud800' || c > '\udbff' || (i != src.Length - 1 && src[i + 1] >= '\udc00' && src[i + 1] <= '\udfff')) && (c < '\udc00' || c > '\udfff' || (i != 0 && src[i - 1] >= '\ud800' && src[i - 1] <= '\udbff')) && c != '\u2028' && c != '\u2029')
			{
				if (c == '/' && i > 0)
				{
					return src[i - 1] == '<';
				}
				return false;
			}
			return true;
		}

		public string EscapeString(string src)
		{
			if (src == null)
			{
				return null;
			}
			for (int i = 0; i < src.Length; i++)
			{
				if (NeedEscape(src, i))
				{
					StringBuilder stringBuilder = new StringBuilder();
					if (i > 0)
					{
						stringBuilder.Append(src, 0, i);
					}
					return DoEscapeString(stringBuilder, src, i);
				}
			}
			return src;
		}

		private string DoEscapeString(StringBuilder sb, string src, int cur)
		{
			int num = cur;
			for (int i = cur; i < src.Length; i++)
			{
				if (NeedEscape(src, i))
				{
					sb.Append(src, num, i - num);
					switch (src[i])
					{
					case '\b':
						sb.Append("\\b");
						break;
					case '\f':
						sb.Append("\\f");
						break;
					case '\n':
						sb.Append("\\n");
						break;
					case '\r':
						sb.Append("\\r");
						break;
					case '\t':
						sb.Append("\\t");
						break;
					case '"':
						sb.Append("\\\"");
						break;
					case '\\':
						sb.Append("\\\\");
						break;
					case '/':
						sb.Append("\\/");
						break;
					default:
						sb.Append("\\u");
						sb.Append(((int)src[i]).ToString("x04"));
						break;
					}
					num = i + 1;
				}
			}
			sb.Append(src, num, src.Length - num);
			return sb.ToString();
		}

		public static implicit operator JsonValue(bool value)
		{
			return new JsonPrimitive(value);
		}

		public static implicit operator JsonValue(byte value)
		{
			return new JsonPrimitive(value);
		}

		public static implicit operator JsonValue(char value)
		{
			return new JsonPrimitive(value);
		}

		public static implicit operator JsonValue(decimal value)
		{
			return new JsonPrimitive(value);
		}

		public static implicit operator JsonValue(double value)
		{
			return new JsonPrimitive(value);
		}

		public static implicit operator JsonValue(float value)
		{
			return new JsonPrimitive(value);
		}

		public static implicit operator JsonValue(int value)
		{
			return new JsonPrimitive(value);
		}

		public static implicit operator JsonValue(long value)
		{
			return new JsonPrimitive(value);
		}

		public static implicit operator JsonValue(sbyte value)
		{
			return new JsonPrimitive(value);
		}

		public static implicit operator JsonValue(short value)
		{
			return new JsonPrimitive(value);
		}

		public static implicit operator JsonValue(string value)
		{
			return new JsonPrimitive(value);
		}

		public static implicit operator JsonValue(uint value)
		{
			return new JsonPrimitive(value);
		}

		public static implicit operator JsonValue(ulong value)
		{
			return new JsonPrimitive(value);
		}

		public static implicit operator JsonValue(ushort value)
		{
			return new JsonPrimitive(value);
		}

		public static implicit operator JsonValue(DateTime value)
		{
			return new JsonPrimitive(value);
		}

		public static implicit operator JsonValue(DateTimeOffset value)
		{
			return new JsonPrimitive(value);
		}

		public static implicit operator JsonValue(Guid value)
		{
			return new JsonPrimitive(value);
		}

		public static implicit operator JsonValue(TimeSpan value)
		{
			return new JsonPrimitive(value);
		}

		public static implicit operator JsonValue(Uri value)
		{
			return new JsonPrimitive(value);
		}

		public static implicit operator bool(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToBoolean(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		public static implicit operator byte(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToByte(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		public static implicit operator char(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToChar(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		public static implicit operator decimal(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToDecimal(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		public static implicit operator double(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToDouble(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		public static implicit operator float(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToSingle(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		public static implicit operator int(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToInt32(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		public static implicit operator long(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToInt64(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		public static implicit operator sbyte(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToSByte(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		public static implicit operator short(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToInt16(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		public static implicit operator string(JsonValue value)
		{
			return value?.ToString();
		}

		public static implicit operator uint(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToUInt32(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		public static implicit operator ulong(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToUInt64(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		public static implicit operator ushort(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToUInt16(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		public static implicit operator DateTime(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (DateTime)((JsonPrimitive)value).Value;
		}

		public static implicit operator DateTimeOffset(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (DateTimeOffset)((JsonPrimitive)value).Value;
		}

		public static implicit operator TimeSpan(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (TimeSpan)((JsonPrimitive)value).Value;
		}

		public static implicit operator Guid(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (Guid)((JsonPrimitive)value).Value;
		}

		public static implicit operator Uri(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (Uri)((JsonPrimitive)value).Value;
		}
	}
}
