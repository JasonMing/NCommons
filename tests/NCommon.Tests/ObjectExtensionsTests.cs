using System;
using Xunit;

namespace NCommon
{
	public class ObjectExtensionsTests
	{
		[Fact]
		public void ValueOrDefault()
		{
			Assert.Null(((String)null).ValueOrDefault(x => x.ToString()));
			Assert.Equal(0, ((String)null).ValueOrDefault(x => x.As<IConvertible>().ToInt32(null)));
			Assert.Equal(String.Empty, String.Empty.ValueOrDefault(x => x.ToString()));
			Assert.Equal("foo", "foo".ValueOrDefault(x => x.ToString()));

			Assert.Equal("foo", ((String)null).ValueOrDefault(x => x.ToString(), "foo"));
			Assert.Equal(13, ((String)null).ValueOrDefault(x => x.As<IConvertible>().ToInt32(null), 13));
			Assert.Equal(String.Empty, String.Empty.ValueOrDefault(x => x.ToString(), "foo"));
			Assert.Equal("foo", "foo".ValueOrDefault(x => x.ToString(), "bar"));

			Assert.Equal("foo", ((String)null).ValueOrDefault(x => x.ToString(), () => "foo"));
			Assert.Equal(13, ((String)null).ValueOrDefault(x => x.As<IConvertible>().ToInt32(null), () => 13));
			Assert.Equal(String.Empty, String.Empty.ValueOrDefault(x => x.ToString(), () => "foo"));
			Assert.Equal("foo", "foo".ValueOrDefault(x => x.ToString(), () => "bar"));
		}

		[Fact]
		public void InstanceOf()
		{
			Assert.True(0.InstanceOf<Int32>());
			Assert.True(0.InstanceOf<Int32?>());
			Assert.True(0.InstanceOf<Object>());
			Assert.False(0.InstanceOf<Int64>());
		}

		[Fact]
		public void As()
		{
			Assert.Null(0.As<String>());
			Assert.NotNull(0.As<Object>());
		}

		[Fact]
		public void TryCast()
		{
			String value1;
			Assert.True("Foo".TryCast(out value1));
			Assert.Equal("Foo", value1);

			Int32? value2;
			Assert.False("Foo".TryCast(out value2));
			Assert.Null(value2);

			Int32 value3;
			Assert.False("Foo".TryCast(out value3));
			Assert.Equal(0, value3);
		}

		[Fact]
		public void TryConvert()
		{
			Int32 value1;
			Assert.True("1".TryConvert(out value1));
			Assert.Equal(1, value1);

			Int32? value2;
			Assert.False("abc".TryConvert(out value2));
			Assert.Null(value2);
		}

		[Fact]
		public void TryConvertWithException()
		{
			Exception exception;

			Int32 value1;
			Assert.True("1".TryConvert(out value1, out exception));
			Assert.Equal(1, value1);
			Assert.Null(exception);

			Int32? value2;
			Assert.True("1".TryConvert(out value2, out exception));
			Assert.Equal(1, value2);
			Assert.Null(exception);

			Int32 value3;
			Assert.False("abc".TryConvert(out value3, out exception));
			Assert.Equal(0, value3);
			Assert.NotNull(exception);

			Int32? value4;
			Assert.False("abc".TryConvert(out value4, out exception));
			Assert.Null(value4);
			Assert.NotNull(exception);
		}

		[Fact]
		public void CalculateHashCode()
		{
			// Assert no throws.
			Overrides.CalculateHashCode("a", "b", "c");
		}

		[Fact]
		public void EqualsWith()
		{
			var source = new
			{
				Prop1 = "1",
				Prop2 = 1,
				Prop3 = (Object)null,
			};

			var cloned = new
			{
				source.Prop1,
				source.Prop2,
				Prop3 = new Object(),
			};

			Assert.True(source.EqualsWith(source));
			Assert.False(source.EqualsWith(cloned));

			Assert.True(source.EqualsWith(cloned, x => x.Prop1, x => x.Prop2));
			Assert.False(source.EqualsWith(cloned, x => x.Prop1, x => x.Prop2, x => x.Prop3));

			Object unknownSource = source;
			Object unknownCloned = cloned;

			Assert.True(source.EqualsWith(unknownSource));
			Assert.False(source.EqualsWith(unknownCloned));

			Assert.True(source.EqualsWith(unknownCloned, x => x.Prop1, x => x.Prop2));
			Assert.False(source.EqualsWith(unknownCloned, x => x.Prop1, x => x.Prop2, x => x.Prop3));



			Assert.False(source.EqualsWith(String.Empty));
			Assert.False(source.EqualsWith(String.Empty, x => x.Prop1));
		}
	}
}
