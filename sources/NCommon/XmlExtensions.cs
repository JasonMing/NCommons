using System;
using System.Linq;
using System.Xml.Linq;

namespace NCommon
{
	/// <summary>
	/// The extensions for <see cref="System.Xml"/> and <see cref="System.Xml.Linq"/>.
	/// Should NOT use this class directly.
	/// </summary>
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
			return TrueStrings.Contains(value, StringComparer.OrdinalIgnoreCase);
		}

		/// <summary>
		/// Get the value of this element and convert it to <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type will be convert to.</typeparam>
		/// <returns>The converted value.</returns>
		public static T Value<T>(this XElement xElement)
		{
			return xElement == null ? NullOrError<T>() : ConvertTo<T>(xElement.Value);
		}

		/// <summary>
		/// Get child value of <paramref name="xElement"/> and convert it to <typeparamref name="T"/>.
		/// This method will search the children in the child elements and it's owned attributes.
		/// The search order is element than attribute.
		/// </summary>
		/// <typeparam name="T">The type will be convert to.</typeparam>
		/// <returns>The converted value.</returns>
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
					return XmlExtensions.NullOrError<T>();
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
