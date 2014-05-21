using System;
using System.Linq;
using System.Xml.Linq;

namespace NCommons
{
	public static class XmlExtensions
	{
		private static readonly String[] TrueStrings = { "True", "Yes", "Y", "T", "1" };

		private static T ConvertTo<T>(String value)
		{
			if (typeof(T) == typeof(Boolean))
			{
				return (T)(Object)ParseBoolean(value);
			}
			return (T)Convert.ChangeType(value, typeof(T));
		}

		private static T NullOrError<T>()
		{
			if (typeof(T).IsClass)
			{
				return default(T); // Return null.
			}
			throw new InvalidOperationException();
		}

		private static Boolean ParseBoolean(String value)
		{
			return TrueStrings.Any(s => String.Equals(s, value, StringComparison.OrdinalIgnoreCase));
		}

		public static T Value<T>(this XElement xElement)
		{
			return xElement == null ? NullOrError<T>() : ConvertTo<T>(xElement.Value);
		}

		public static T Value<T>(this XElement xElement, XName name)
		{
			if (xElement == null)
			{
				return NullOrError<T>();
			}
			String value;
			var childElemnt = xElement.Element(name);
			if (childElemnt == null)
			{
				var attribute = xElement.Attribute(name);
				if (attribute == null)
				{
					return NullOrError<T>();
				}
				value = attribute.Value;
			} else
			{
				value = childElemnt.Value;
			}

			return ConvertTo<T>(value);
		}

		public static String Value(this XElement xElement, XName name)
		{
			return xElement.Value<String>(name);
		}
	}
}
