﻿using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Nextension
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
			if (typeof(T) == typeof(String))
			{
				return (T)(Object)value;
			}

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
		public static T Value<T>(this XElement element)
		{
			return element == null ? NullOrError<T>() : ConvertTo<T>(element.Value);
		}

		/// <summary>
		/// Get child value of <paramref name="element"/> and convert it to <typeparamref name="T"/>.
		/// This method will search the children in the child elements and it's owned attributes.
		/// The search order is element than attribute.
		/// </summary>
		/// <typeparam name="T">The type will be convert to.</typeparam>
		/// <returns>The converted value.</returns>
		public static T Value<T>(this XElement element, XName name)
		{
			if (element == null)
			{
				return NullOrError<T>();
			}

			String value;
			var childElemnt = element.Element(name);
			if (childElemnt == null)
			{
				var attribute = element.Attribute(name);
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

		/// <summary>
		/// Get the child element's literal value.
		/// </summary>
		/// <returns>The literal value.</returns>
		public static String Value(this XElement element, XName name)
		{
			return Value<String>(element, name);
		}

		/// <summary>
		/// Get the value of this element and convert it to <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The type will be convert to.</typeparam>
		/// <returns>The converted value.</returns>
		public static T Value<T>(this XmlElement element)
		{
			return element == null ? NullOrError<T>() : ConvertTo<T>(element.Value);
		}

		/// <summary>
		/// Get child value of <paramref name="element"/> and convert it to <typeparamref name="T"/>.
		/// This method will search the children in the child elements and it's owned attributes.
		/// The search order is element than attribute.
		/// </summary>
		/// <typeparam name="T">The type will be convert to.</typeparam>
		/// <returns>The converted value.</returns>
		public static T Value<T>(this XmlElement element, XName name)
		{
			Ensure.ArgumentNotNull(name, "name");

			if (element == null)
			{
				return NullOrError<T>();
			}

			String value;
			var childElemnts = element.GetElementsByTagName(name.LocalName, name.NamespaceName);
			if (childElemnts.Count == 0)
			{
				var attribute = element.GetAttributeNode(name.LocalName, name.NamespaceName);
				if (attribute == null)
				{
					return NullOrError<T>();
				}

				value = attribute.Value;
			} else
			{
				value = childElemnts[0].Value;
			}

			return ConvertTo<T>(value);
		}

		/// <summary>
		/// A convention method for <see cref="Value{T}(XmlElement, XName)"/> that using <see cref="XmlQualifiedName"/> as the <paramref name="name"/>.
		/// </summary>
		/// <seealso cref="Value{T}(XmlElement, XName)"/>
		public static T Value<T>(this XmlElement element, XmlQualifiedName name)
		{
			Ensure.ArgumentNotNull(name, "name");

			return Value<T>(element, XName.Get(name.Name, name.Namespace));
		}

		/// <summary>
		/// Get the child element's literal value.
		/// </summary>
		/// <returns>The literal value.</returns>
		public static String Value(this XmlElement element, XName name)
		{
			return Value<String>(element, name);
		}
	}
}
