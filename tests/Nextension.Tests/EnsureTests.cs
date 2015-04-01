using System;
using System.Collections;
using Xunit;

namespace Nextension
{
	public class EnsureTests
	{
		[Theory]
		[InlineData(null, true)]
		[InlineData(default(Int32), false)]
		[InlineData("", false)]
		public void EnsureArgumentNotNull(Object argument, Boolean shouldCorrupt)
		{
			try
			{
				Ensure.ArgumentNotNull(argument, "argument");
				Assert.False(shouldCorrupt);
			} catch (ArgumentNullException)
			{
				Assert.True(shouldCorrupt);
			}

			try
			{
				Ensure.ArgumentNotNull(argument, "argument", "Argument should not be null.");
				Assert.False(shouldCorrupt);
			} catch (ArgumentNullException)
			{
				Assert.True(shouldCorrupt);
			}
		}

		[Fact]
		public void EnsureArgumentNotNull()
		{
			this.EnsureArgumentNotNull(Helpers.NonGenericEmptyEnumerable, false);
			this.EnsureArgumentNotNull(Helpers.GenericEmptyEnumerable<Object>(), false);
		}

		[Theory]
		[InlineData(null, true)]
		[InlineData("", true)]
		public void EnsureArgumentNotEmpty(IEnumerable argument, Boolean shouldCorrupt)
		{
			try
			{
				// ReSharper disable once PossibleMultipleEnumeration
				Ensure.ArgumentNotEmpty(argument, "argument");
				Assert.False(shouldCorrupt);
			} catch (ArgumentNullException)
			{
				throw new InvalidOperationException("Unexpected exception.");
			} catch (ArgumentException)
			{
				Assert.True(shouldCorrupt);
			}

			try
			{
				// ReSharper disable once PossibleMultipleEnumeration
				Ensure.ArgumentNotEmpty(argument, "argument", "Argument should not be null.");
				Assert.False(shouldCorrupt);
			} catch (ArgumentNullException)
			{
				throw new InvalidOperationException("Unexpected exception.");
			} catch (ArgumentException)
			{
				Assert.True(shouldCorrupt);
			}
		}

		[Fact]
		public void EnsureArgumentNotEmpty()
		{
			this.EnsureArgumentNotEmpty(Helpers.NonGenericEmptyEnumerable, true);
			this.EnsureArgumentNotEmpty(Helpers.GenericEmptyEnumerable<Object>(), true);
			this.EnsureArgumentNotEmpty(new[] { 1, 2, 3 }, false);
		}
	}
}
