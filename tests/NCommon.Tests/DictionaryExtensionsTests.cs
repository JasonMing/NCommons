using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace NCommon
{
	public class DictionaryExtensionsTests
	{
		[Fact]
		public void AsSafeDictionary()
		{
			var source = new Dictionary<String, String>();

			var safeDictionary = source.AsSafeDictionary();

			Assert.Empty(source);
			Assert.Empty(safeDictionary);

			source.Add("key", "value");

			Assert.Single(source);
			Assert.Single(safeDictionary);
		}

		[Fact]
		public void ToSafeDictionary_FromDictionary()
		{
			var source = new Dictionary<String, String>
			{
				{"foo", "value"},
				{"bar", "value2"},
			};

			var safeDictionary = source.ToSafeDictionary();
			var caseInsensitiveSafeDictionary = source.ToSafeDictionary(StringComparer.OrdinalIgnoreCase);

			source.Clear();

			Assert.NotEmpty(safeDictionary);
			Assert.NotEmpty(caseInsensitiveSafeDictionary);

			Assert.True(safeDictionary.ContainsKey("foo"));
			Assert.False(safeDictionary.ContainsKey("Foo"));

			Assert.True(caseInsensitiveSafeDictionary.ContainsKey("foo"));
			Assert.True(caseInsensitiveSafeDictionary.ContainsKey("Foo"));

			// Assert no exception throw.
			var value = safeDictionary["not_exist"];
			Assert.Null(value);
		}

		[Fact]
		public void ToSafeDictionary_WithOrdinalIgnoreCaseComparer_WithDuplicatedKey()
		{
			var source = new Dictionary<String, String>
			{
				{"foo", "value"},
				{"Foo", "value2"},
			};

			Assert.Throws<ArgumentException>(() => source.ToSafeDictionary(StringComparer.OrdinalIgnoreCase));
		}

		[Fact]
		public void ToSafeDictionary_FromEnumerable()
		{
			String[] source = { "a", "b", "c", "d", "e" };

			var safeDictionaries = new IDictionary<String, String>[4];

			safeDictionaries[0] = source.ToSafeDictionary(i => i);
			safeDictionaries[1] = source.ToSafeDictionary(i => i, StringComparer.OrdinalIgnoreCase);
			safeDictionaries[2] = source.ToSafeDictionary(i => i, i => i.ToUpper());
			safeDictionaries[3] = source.ToSafeDictionary(i => i, i => i.ToUpper(), StringComparer.OrdinalIgnoreCase);

			safeDictionaries.ForEach(x => Assert.Equal(5, x.Count));

			Assert.True(safeDictionaries[0].ContainsKey("a"));
			Assert.False(safeDictionaries[0].ContainsKey("A"));

			Assert.True(safeDictionaries[1].ContainsKey("a"));
			Assert.True(safeDictionaries[1].ContainsKey("A"));

			Assert.Equal("A", safeDictionaries[2]["a"]);
			Assert.Equal(null, safeDictionaries[2]["A"]);

			Assert.Equal("A", safeDictionaries[3]["a"]);
			Assert.Equal("A", safeDictionaries[3]["A"]);
		}

		[Fact]
		public void TryGetValue()
		{
			var source = new Dictionary<String, String>();

			Assert.Null(source.TryGetValue("not_exist"));
			Assert.Null(DictionaryExtensions.TryGetValue<String, String>(null, "what_ever"));

			var source2 = new Dictionary<String, Int32>();
			Assert.Equal(default(Int32), source2.TryGetValue("not_exist"));
			Assert.Equal(default(Int32), DictionaryExtensions.TryGetValue<String, Int32>(null, "what_ever"));
		}

		[Theory]
		[MemberData("TryGetValueChangeableData")]
		public void TryGetValueChangeable(String key, Boolean convert, Boolean expect, Type outputType, Object output)
		{
			var testMethod = new Action<String, Boolean, Boolean, Object>(TryGetValueChangeableInvoker)
				.Method
				.GetGenericMethodDefinition()
				.MakeGenericMethod(outputType);

			testMethod.Invoke(null, new[] { key, convert, expect, output });
		}

		private static void TryGetValueChangeableInvoker<T>(String key, Boolean convert, Boolean expect, T output)
		{
			var source = new Dictionary<String, Object>
			{
				{"string", "value"},
				{"int_string", "1"},
				{"int", 1},
				{"mytype", (Value)"value"},
			};

			T value;
			var result = source.TryGetValue(key, out value, convert);

			Assert.Equal(expect, result);
			Assert.Equal(output, value);

			T value2;
			var result2 = ((IDictionary)source).TryGetValue(key, out value2, convert);

			Assert.Equal(expect, result2);
			Assert.Equal(output, value2);
		}

		public static IEnumerable<Object[]> TryGetValueChangeableData()
		{
			// Get value unexist.
			yield return new Object[] { "unexist", false, false, typeof(Object), null };
			yield return new Object[] { "unexist", false, false, typeof(Int32), 0 };
			yield return new Object[] { "unexist", false, false, typeof(Value), null };

			// Get value with down-cast.
			yield return new Object[] { "string", false, true, typeof(String), "value" };
			yield return new Object[] { "int", false, true, typeof(Int32), 1 };
			yield return new Object[] { "mytype", false, true, typeof(Value), (Value)"value" };

			// Get value with cast-operator cast.
			yield return new Object[] { "string", false, false, typeof(Value), null };
			yield return new Object[] { "mytype", false, false, typeof(String), null };

			// Get value with convert.
			yield return new Object[] { "int_string", true, true, typeof(Int32), 1 };
			yield return new Object[] { "int", true, true, typeof(String), "1" };

			// Nullables.
			yield return new Object[] { "unexist", false, false, typeof(Int32?), null };
			yield return new Object[] { "int", false, true, typeof(Int32?), 1 };
			yield return new Object[] { "int_string", true, true, typeof(Int32?), 1 };
			yield return new Object[] { "string", true, false, typeof(Int32?), null };
		}

		private class Value
		{
			private readonly String data;

			private Value(String data)
			{
				this.data = data;
			}

			public static implicit operator String(Value value)
			{
				return value.data;
			}

			public static implicit operator Value(String data)
			{
				return new Value(data);
			}

			public override Boolean Equals(Object obj)
			{
				var that = obj as Value;

				return that != null && this.data.Equals(that.data);
			}

			public override Int32 GetHashCode()
			{
				return this.data.GetHashCode();
			}

			public override String ToString()
			{
				return this.data;
			}
		}
	}
}
