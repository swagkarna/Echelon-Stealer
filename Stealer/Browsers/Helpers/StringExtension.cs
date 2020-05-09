using System;
using System.Globalization;
using System.Text;

namespace Echelon
{
	public static class StringExtension
	{
		public static T ForceTo<T>(this object @this)
		{
			return (T)Convert.ChangeType(@this, typeof(T));
		}

		public static string Remove(this string input, string strToRemove)
		{
			if (input.IsNullOrEmpty())
			{
				return null;
			}
			return input.Replace(strToRemove, "");
		}

		public static string Left(this string input, int minusRight = 1)
		{
			if (input.IsNullOrEmpty() || input.Length <= minusRight)
			{
				return null;
			}
			return input.Substring(0, input.Length - minusRight);
		}

		public static CultureInfo ToCultureInfo(this string culture, CultureInfo defaultCulture)
		{
			if (!culture.IsNullOrEmpty())
			{
				return defaultCulture;
			}
			return new CultureInfo(culture);
		}

		public static string ToCamelCasing(this string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				return value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1);
			}
			return value;
		}

		public static double? ToDouble(this string value, string culture = "en-US")
		{
			try
			{
				return double.Parse(value, new CultureInfo(culture));
			}
			catch 
			{
				return null;
			}
		}

		public static bool? ToBoolean(this string value)
		{
			bool result = false;
			if (bool.TryParse(value, out result))
			{
				return result;
			}
			return null;
		}

		public static int? ToInt32(this string value)
		{
			int result = 0;
			if (int.TryParse(value, out result))
			{
				return result;
			}
			return null;
		}

		public static long? ToInt64(this string value)
		{
			long result = 0L;
			if (long.TryParse(value, out result))
			{
				return result;
			}
			return null;
		}

		public static string AddQueyString(this string url, string queryStringKey, string queryStringValue)
		{
			string text = "";
			text = ((url.Split('?').Length <= 1) ? "?" : "&");
			return url + text + queryStringKey + "=" + queryStringValue;
		}

		public static string FormatFirstLetterUpperCase(this string value, string culture = "en-US")
		{
			return CultureInfo.GetCultureInfo(culture).TextInfo.ToTitleCase(value);
		}

		public static string FillLeftWithZeros(this string value, int decimalDigits)
		{
			if (!string.IsNullOrEmpty(value))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(value);
				string[] array = value.Split(',');
				for (int i = array[array.Length - 1].Length; i < decimalDigits; i++)
				{
					stringBuilder.Append("0");
				}
				value = stringBuilder.ToString();
			}
			return value;
		}

		public static string FormatWithDecimalDigits(this string value, bool removeCurrencySymbol, bool returnZero, int? decimalDigits)
		{
			if (value.IsNullOrEmpty())
			{
				return value;
			}
			if (!value.IndexOf(",").Equals(-1))
			{
				string[] array = value.Split(',');
				if (array.Length.Equals(2) && array[1].Length > 0)
				{
					value = array[0] + "," + array[1].Substring(0, (array[1].Length >= decimalDigits.Value) ? decimalDigits.Value : array[1].Length);
				}
			}
			if (!decimalDigits.HasValue)
			{
				return value;
			}
			return value.FillLeftWithZeros(decimalDigits.Value);
		}

		public static string FormatWithoutDecimalDigits(this string value, bool removeCurrencySymbol, bool returnZero, int? decimalDigits, CultureInfo culture)
		{
			if (removeCurrencySymbol)
			{
				value = value.Remove(culture.NumberFormat.CurrencySymbol).Trim();
			}
			return value;
		}
	}
}
