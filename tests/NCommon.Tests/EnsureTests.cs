using System;
using System.Collections;
using Xunit;

namespace NCommon
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
				// ReSharper disable once PossibleMultipleEnumeration
				Ensure.ArgumentNotEmpty(argument, "argument", "Argument should not be null.");
				Assert.False(shouldCorrupt);
			} catch (ArgumentNullException)
			{
				throw new InvalidOperationException("Unexpected excetion.");
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
		}
	}
}
