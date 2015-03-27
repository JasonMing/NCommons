using System;
using Xunit;

namespace NCommon
{
	public class StringExtensionsTests
	{
		[Theory]
		[InlineData(null, true)]
		[InlineData("", true)]
		[InlineData(" ", false)]
		[InlineData("\t", false)]
		[InlineData("\r", false)]
		[InlineData("foo", false)]
		public void IsEmpty(String source, Boolean expected)
		{
			Assert.Equal(expected, source.IsEmpty());
			Assert.NotEqual(expected, source.IsNotEmpty());
		}

		[Theory]
		[InlineData(null, true)]
		[InlineData("", true)]
		[InlineData(" ", true)]
		[InlineData("\t", true)]
		[InlineData("\r", true)]
		[InlineData("foo", false)]
		public void IsBlank(String source, Boolean expected)
		{
			Assert.Equal(expected, source.IsBlank());
			Assert.NotEqual(expected, source.IsNotBlank());
		}

		[Theory]
		[InlineData(null, false)]
		[InlineData("", false)]
		[InlineData("0", false)]
		[InlineData("00", true)]
		public void RegexMatch_TwoDigits(String source, Boolean expected)
		{
			Assert.Equal(expected, source.RegexMatch(@"\d{2}"));
		}

		[Fact]
		public void RegexMatch_NullPattern()
		{
			Assert.Throws<ArgumentNullException>(() => "foo".RegexMatch(null));
		}

		[Theory]
		[InlineData(null, null)]
		[InlineData("", "")]
		[InlineData("0", "0")]
		[InlineData("0 0\t0\r0\n", "0000")]
		public void RegexMatch_RemoveWhitespaces(String source, String expected)
		{
			Assert.Equal(expected, source.RegexReplace(@"\s", String.Empty));
		}

		[Fact]
		public void RegexReplace_NullPattern()
		{
			Assert.Throws<ArgumentNullException>(() => "foo".RegexReplace(null, String.Empty));
		}
	}
}
