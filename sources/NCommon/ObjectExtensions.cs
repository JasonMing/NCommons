using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using NCommon.Annotations;

namespace NCommon
{
	/// <summary>
	/// The extension for every object.
	/// Should NOT use this class directly.
	/// </summary>
	public static class ObjectExtensions
	{
		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TResult ValueOrDefault<TSource, TResult>([CanBeNull] this TSource source, Func<TSource, TResult> valuer)
			where TSource : class
		{
			return (source == null) ? default(TResult) : valuer(source);
		}

		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TResult ValueOrDefault<TSource, TResult>([CanBeNull] this TSource source, Func<TSource, TResult> valuer, TResult defaultValue)
			where TSource : class
		{
			return (source == null) ? defaultValue : valuer(source);
		}

		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TResult ValueOrDefault<TSource, TResult>([CanBeNull] this TSource source, Func<TSource, TResult> valuer, Func<TResult> defaultValuer)
			where TSource : class
		{
			return (source == null) ? defaultValuer() : valuer(source);
		}

		/// <summary>
		/// Provides method like "as" convert for using in the lambda expression.
		/// NOTE: Be care using with the value-types, the boxing mechanism may causes the performance issue.
		/// </summary>
		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T As<T>(this Object source)
			where T : class
		{
			return source as T;
		}

		/// <summary>
		/// Provides method like "is" convert for using in the lambda expression.
		/// NOTE: Be care using with the value-types, the boxing mechanism may causes the performance issue.
		/// </summary>
		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InstanceOf<T>(this Object source)
		{
			return source is T;
		}

		/// <summary>
		/// Try cast the <paramref name="source"/> to the specific type <typeparamref name="T"/>.
		/// NOTE: Be care using with the value-types, the boxing mechanism may causes the performance issue.
		/// </summary>
		public static Boolean TryCast<T>(this Object source, out T value)
		{
			try
			{
				value = (T)source;
				return true;
			} catch (InvalidCastException)
			{
				value = default(T);
				return false;
			}
		}

		/// <summary>
		/// Try convert the <paramref name="source"/> to the specific type <typeparamref name="T"/> by using <see cref="Convert.ChangeType(Object, Type)"/>.
		/// NOTE: Be care using with the value-types, the boxing mechanism may causes the performance issue.
		/// </summary>
		public static Boolean TryConvert<T>(this Object source, out T value)
		{
			try
			{
				value = (T)Convert.ChangeType(source, typeof(T));
				return true;
			} catch (Exception)
			{
				value = default(T);
				return false;
			}
		}

		/// <summary>
		/// Try convert the <paramref name="source"/> to the specific type <typeparamref name="T"/> by using <see cref="Convert.ChangeType(Object, Type)"/>.
		/// NOTE: Be care using with the value-types, the boxing mechanism may causes the performance issue.
		/// </summary>
		public static Boolean TryConvert<T>(this Object source, out T value, out Exception exception)
		{
			try
			{
				value = (T)Convert.ChangeType(source, typeof(T));
				exception = null;
				return true;
			} catch (Exception e)
			{
				value = default(T);
				exception = e;
				return false;
			}
		}

		#region Helpers of overriding Object's method

		public static Int32 CalcHash(params Object[] objs)
		{
			return ObjectExtensions.CalcHash(objs as IEnumerable);
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
