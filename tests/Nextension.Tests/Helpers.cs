using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Nextension
{
	static class Helpers
	{
		public static readonly IEnumerable NonGenericNullEnumerable = null;

		public static readonly IEnumerable NonGenericEmptyEnumerable = new Object[0];

		public static IEnumerable NonGenericBlankEnumerable(Int32 length = 1)
		{
			return new Object[length];
		}

		public static IEnumerable<T> GenericNullEnumerable<T>()
		{
			return null;
		}

		public static IEnumerable<T> GenericEmptyEnumerable<T>()
		{
			return Enumerable.Empty<T>();
		}

		public static IEnumerable<T> GenericBlankEnumerable<T>(Int32 length = 1)
			where T : class
		{
			return Enumerable.Repeat<T>(null, length);
		}

		public static readonly String NullString = null;

		public static readonly String EmptyString = String.Empty;
	}
}
