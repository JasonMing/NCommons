using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NCommon.Annotations;
using Xunit;

namespace NCommon
{
	public class EnumerableExtensionsTests
	{
		//// ReSharper disable PossibleMultipleEnumeration
		//// ReSharper disable ExpressionIsAlwaysNull

		[Theory]
		[InlineData(null, true)]
		public void IsEmpty(IEnumerable source, Boolean expect)
		{
			Assert.Equal(expect, source.IsEmpty());
			Assert.Equal(expect, ((IEnumerable<Object>)source).IsEmpty());
			Assert.NotEqual(expect, source.IsNotEmpty());
			Assert.NotEqual(expect, ((IEnumerable<Object>)source).IsNotEmpty());
		}

		[Fact]
		public void IsEmpty()
		{
			this.IsEmpty(Enumerable.Empty<Object>(), true);
			this.IsEmpty(new Object[] { null, null }, false);
			this.IsEmpty(new Object[] { 1, "String" }, false);
		}

		[Fact]
		public void IsIn()
		{
			const String foo = "foo";
			const String @null = null;

			Assert.True(foo.IsIn("foo", "abc"));
			Assert.True(foo.IsNotIn("Foo", "abc"));

			Assert.True(foo.IsIn(StringComparer.OrdinalIgnoreCase, "Foo", "abc"));
			Assert.False(foo.IsNotIn(StringComparer.OrdinalIgnoreCase, "Foo", "abc"));

			Assert.True(@null.IsIn(new String[] { null }));
			Assert.True(foo.IsNotIn((String[])null));
			Assert.True(foo.IsNotIn(/*Empty*/));
		}

		[Fact]
		public void ForEach()
		{
			var source = new[] { 1, 2, 3, 4, 5 };

			Int32[] i = { 0 };
			source.ForEach(item => i[0] += 1);
			Assert.Equal(5, i[0]);

			i[0] = 0;
			source.ForEach((index, item) => i[0] += 1);
			Assert.Equal(5, i[0]);
		}

		[Fact]
		public void ForEach_WithNullSource()
		{
			IEnumerable<Object> source = null;

			source.ForEach(item => { throw new InvalidOperationException("Should not reach here"); });

			source.ForEach((index, item) => { throw new InvalidOperationException("Should not reach here"); });
		}

		[Fact]
		public void Distinct()
		{
			Int32[] source = { 1, 2, 3, 4, 5, 6 };

			var distincted = source.Distinct(i => i % 2);

			Assert.Equal(2, distincted.Count());

			source = null;
			Assert.Null(source.Distinct(i => i));
		}

		[Fact]
		public void Distinct_WithComparer()
		{
			String[] source = { "Foo", "far", "func", "hello" };

			var distincted = source.Distinct(x => x.Substring(0, 1), StringComparer.OrdinalIgnoreCase);

			Assert.Equal(2, distincted.Count());

			Assert.Throws<ArgumentNullException>(() => source.Distinct(x => x.Substring(0, 1), null));

			String[] source2 = null;
			Assert.Null(source2.Distinct(i => i, StringComparer.OrdinalIgnoreCase));
		}

		[Theory]
		[MemberData("ConcatData")]
		public void Concat(IEnumerable<String> first, String[] second, IEnumerable<String> expect)
		{
			var concat = first.Concat(second);

			Assert.Equal(expect, concat);
		}

		public static IEnumerable<Object[]> ConcatData()
		{
			String[] source = { "a", "b", "c" };

			yield return new Object[] { source, new[] { "d" }, new[] { "a", "b", "c", "d" } };
			yield return new Object[] { null, new[] { "d" }, new[] { "d" } };
			yield return new Object[] { source, null, source };
			yield return new Object[] { null, null, Enumerable.Empty<String>() };
		}

		[Theory]
		[MemberData("ConcatHeadData")]
		public void ConcatHead(IEnumerable<String> second, String[] first, IEnumerable<String> expect)
		{
			var concat = second.ConcatHead(first);

			Assert.Equal(expect, concat);
		}

		public static IEnumerable<Object[]> ConcatHeadData()
		{
			String[] source = { "b", "c", "d" };

			yield return new Object[] { source, new[] { "a" }, new[] { "a", "b", "c", "d" } };
			yield return new Object[] { null, new[] { "a" }, new[] { "a" } };
			yield return new Object[] { source, null, source };
			yield return new Object[] { null, null, Enumerable.Empty<String>() };
		}
	}
}
