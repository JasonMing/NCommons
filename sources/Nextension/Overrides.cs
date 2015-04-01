using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Nextension.Annotations;

namespace Nextension
{
	/// <summary>
	/// The utilities for <see cref="Object"/>'s overriding method.
	/// </summary>
	public static class Overrides
	{
		private const Int32 HashCodeSeed = 17;

		private const Int32 HashCodeStep = 23;

		/// <summary>
		/// Calculates the passing objects' hashcode.
		/// This is also a convenient implement for the overrided <see cref="Object.GetHashCode"/>.
		/// </summary>
		/// <param name="objs">The objects to be caculating.</param>
		/// <returns>The hash code of the set of <paramref name="objs"/>.</returns>
		public static Int32 CalculateHashCode(params Object[] objs)
		{
			return CalculateHashCode(objs as IEnumerable);
		}

		/// <summary>
		/// Calculates the passing objects' hashcode.
		/// </summary>
		/// <param name="objs">The objects to be caculating.</param>
		/// <returns>The hash code of the set of <paramref name="objs"/>.</returns>
		public static Int32 CalculateHashCode(IEnumerable objs)
		{
			// Overflow is fine, just wrap
			unchecked
			{
				return objs
					.Cast<Object>()
					.Where(obj => obj != null)
					.Aggregate(HashCodeSeed, (current, obj) => (current * HashCodeStep) + obj.GetHashCode());
			}
		}

		/// <summary>
		/// Comparer equality between <paramref name="source"/> and <paramref name="other"/>.
		/// This is also a convenient implement for the overrided <see cref="IEqualityComparer{T}.Equals(T,T)"/>.
		/// </summary>
		/// <param name="source">The source object.</param>
		/// <param name="other">The other object.</param>
		/// <param name="selectors">The selector to select the value to compare.</param>
		public static Boolean EqualsWith<T>([CanBeNull] this T source, [CanBeNull] T other, params Func<T, Object>[] selectors)
		{
			Ensure.ArgumentNotNull(selectors, "selectors");

			return ReferenceEquals(source, other) || (source != null && other != null && selectors.Length != 0 && selectors.All(s => Equals(s(source), s(other))));
		}

		/// <summary>
		/// Comparer equality between <paramref name="source"/> and <paramref name="other"/>.
		/// This is also a convenient implement for the overrided <see cref="Object.Equals(Object)"/>.
		/// </summary>
		/// <param name="source">The source object.</param>
		/// <param name="other">The other object.</param>
		/// <param name="selectors">The selector to select the value to compare.</param>
		public static Boolean EqualsWith<T>([CanBeNull] this T source, [CanBeNull] Object other, params Func<T, Object>[] selectors)
		{
			Ensure.ArgumentNotNull(selectors, "selectors");

			if (ReferenceEquals(source, other))
			{
				return true;
			}

			T castedOther;
			return other.TryCast(out castedOther) && source.EqualsWith(castedOther, selectors);
		}
	}
}
