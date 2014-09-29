using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using NCommons.Annotations;

namespace NCommons
{
	public static class ObjectExtensions
	{
		[DebuggerStepThrough]
		public static TResult ValueOrDefault<TSource, TResult>([CanBeNull] this TSource source, Func<TSource, TResult> valuer)
			where TSource : class
		{
			return (source == null) ? default(TResult) : valuer(source);
		}

		[DebuggerStepThrough]
		public static TResult ValueOrDefault<TSource, TResult>([CanBeNull] this TSource source, Func<TSource, TResult> valuer, TResult defaultValue)
			where TSource : class
		{
			return (source == null) ? defaultValue : valuer(source);
		}

		[DebuggerStepThrough]
		public static TResult ValueOrDefault<TSource, TResult>([CanBeNull] this TSource source, Func<TSource, TResult> valuer, Func<TResult> defaultValuer)
			where TSource : class
		{
			return (source == null) ? defaultValuer() : valuer(source);
		}

		#region Helpers of overriding Object's method

		public static Int32 CalcHash(params Object[] objs)
		{
			return CalcHash(objs as IEnumerable);
		}

		public static Int32 CalcHash(IEnumerable objs)
		{
			const Int32 init = 17;
			const Int32 step = 23;

			unchecked // Overflow is fine, just wrap
			{
				return objs
					.Cast<Object>()
					.Where(obj => obj != null)
					.Aggregate(init, (current, obj) => current * step + obj.GetHashCode());
			}
		}

		public static Boolean EqualsWith<T>(this T source, T other, params Func<T, Object>[] selectors)
		{
			return ReferenceEquals(source, other) || selectors.All(s => s(source) == s(other));
		}

		public static Boolean EqualsWith<T>(this T source, Object other, params Func<T, Object>[] selectors)
		{
			if (ReferenceEquals(source, other))
			{
				return true;
			}

			T otherAsT;
			if (other is T)
			{
				otherAsT = (T)other;
			} else
			{
				return false;
			}

			return source.EqualsWith(otherAsT, selectors);
		}

		#endregion

	}
}
