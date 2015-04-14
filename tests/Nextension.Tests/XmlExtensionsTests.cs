using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using System.Xml.Linq;
using Xunit;

//// ReSharper disable PossibleNullReferenceException

namespace Nextension
{
	public class XmlExtensionsTests
	{
		[SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2001:CheckAllowedIndentationCharacters", Justification = "Reviewed. Suppression is OK here.")]
		private const String XmlContent = @"
<response>
  <data code='0'>
    <url>http://img.cn/af8200cdee8214?size=m&amp;d&amp;m</url>
    <message>OK</message>
    <success>True</success>
    <count>1</count>
  </data>
</response>";

		private static readonly XElement RootElement = XElement.Parse(XmlContent);

		[Theory]
		[MemberData("ValueOfChild_Data")]
		public void ChildValueForXElement(String source, Type type, String name, Object expected)
		{
			var methodDefinition = new Func<XElement, XName, Object>(XmlExtensions.Value<Object>)
				.Method
				.GetGenericMethodDefinition()
				.MakeGenericMethod(type);

			Object actually;

			try
			{
				actually = methodDefinition.Invoke(null, new Object[] { source.ValueOrDefault(XElement.Parse), (XName)name });
			} catch (Exception e)
			{
				Assert.Equal(expected, e.GetBaseException().GetType());
				return;
			}

			Assert.Equal(expected, actually);
		}


		[Theory]
		[MemberData("ValueOfChild_Data")]
		public void ChildValueForXmlElement(String source, Type type, String name, Object expected)
		{
			var methodDefinition = new Func<XmlElement, XName, Object>(XmlExtensions.Value<Object>)
				.Method
				.GetGenericMethodDefinition()
				.MakeGenericMethod(type);

			XmlElement element = null;
			if (source != null)
			{
				var document = new XmlDocument();
				document.LoadXml(source);
				element = document.DocumentElement;
			}

			Object actually;

			try
			{
				actually = methodDefinition.Invoke(null, new Object[] { element, (XName)name });
			} catch (Exception e)
			{
				Assert.Equal(expected, e.GetBaseException().GetType());
				return;
			}

			Assert.Equal(expected, actually);
		}

		public static IEnumerable<Object[]> ValueOfChild_Data()
		{
			// ReSharper disable once PossibleNullReferenceException
			var source = RootElement.Element("data").ToString(SaveOptions.DisableFormatting);

			yield return new Object[] { source, typeof(String), "url", "http://img.cn/af8200cdee8214?size=m&d&m" };
			yield return new Object[] { source, typeof(String), "code", "0" };
			yield return new Object[] { source, typeof(Int32), "code", 0 };
			yield return new Object[] { source, typeof(Boolean), "code", false };
			yield return new Object[] { source, typeof(Boolean), "success", true };
			yield return new Object[] { source, typeof(String), "unexist", null };
			yield return new Object[] { source, typeof(Int32), "unexist", typeof(InvalidOperationException) };
			yield return new Object[] { null, typeof(String), "whatever", null };
			yield return new Object[] { null, typeof(Int32), "whatever", typeof(InvalidOperationException) };
		}

		[Fact]
		public void InnerXml()
		{
			var dataElement = RootElement.Element("data");

			var dataInnerXml = dataElement.InnerXml();
			Assert.StartsWith("<url>", dataInnerXml);
			Assert.DoesNotContain("\r", dataInnerXml);
			Assert.DoesNotContain("\n", dataInnerXml);
			Assert.Contains("&amp;", dataInnerXml);

			var urlElement = dataElement.Element("url");
			Assert.Equal("http://img.cn/af8200cdee8214?size=m&amp;d&amp;m", urlElement.InnerXml());
			Assert.Equal("http://img.cn/af8200cdee8214?size=m&d&m", urlElement.Value);
		}

		[Fact]
		public void OuterXml()
		{
			var dataElement = RootElement.Element("data");

			var dataOuterXml = dataElement.OuterXml();
			Assert.StartsWith("<data", dataOuterXml);
			Assert.DoesNotContain("\r", dataOuterXml);
			Assert.DoesNotContain("\n", dataOuterXml);
			Assert.Contains("&amp;", dataOuterXml);

			var urlElement = dataElement.Element("url");
			Assert.Equal("<url>http://img.cn/af8200cdee8214?size=m&amp;d&amp;m</url>", urlElement.OuterXml());
		}
	}
}
