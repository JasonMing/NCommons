using System;
using Xunit;

namespace NCommon
{
	public class EnumExtensionsTests
	{
		private enum Operation
		{
			None = 0,

			Add = 1,

			Minus = 2,

			Mutli = 3,

			Divide = 4,
		}

		[Theory]
		[InlineData(Operation.None, true)]
		[InlineData(Operation.Add, true)]
		[InlineData((Operation)(-1), false)]
		[InlineData(null, false)]
		[InlineData(PlatformID.Unix, true)]
		public void IsDefined(Enum e, Boolean expected)
		{
			Assert.Equal(expected, e.IsDefined());
		}

		[Theory]
		[InlineData(Operation.None, true)]
		[InlineData(Operation.Add, true)]
		[InlineData((Operation)(-1), false)]
		[InlineData(-1, false)]
		[InlineData(null, false)]
		[InlineData(PlatformID.Unix, false)]
		public void IsEnumOfOperation(Object obj, Boolean expected)
		{
			Assert.Equal(expected, obj.IsEnumOf<Operation>());
			Assert.Equal(expected, obj.IsEnumOf(typeof(Operation)));
		}

		[Fact]
		public void IsEnumOfNonEnumType()
		{
			Assert.Throws<ArgumentException>(() => this.IsEnumOf<Int32>());
			Assert.Throws<ArgumentException>(() => Operation.None.IsEnumOf(typeof(Enum)));
		}

		[Fact]
		public void ToUnderlyingType()
		{
			Assert.IsType<Int32>(Operation.None.ToUndelyingType());
			Assert.Null(((Enum)null).ToUndelyingType());
		}

	}
}
