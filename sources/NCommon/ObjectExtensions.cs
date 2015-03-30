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
		/// Try cast the <paramref name="source"/> to the specific type <typeparamref name="T"/> without exceptions.
		/// Beware, the compile-time casting operator cannot be used in the runtime casting.
		/// NOTE: Be care using with the value-types, the boxing mechanism may causes the performance issue.
		/// </summary>
		public static Boolean TryCast<T>(this Object source, out T value)
		{
			// ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
			if (source is T)
			{
				value = (T)source;
				return true;
			} else
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
			Exception _;
			return TryConvert(source, out value, out _);
		}

		/// <summary>
		/// Try convert the <paramref name="source"/> to the specific type <typeparamref name="T"/> by using <see cref="Convert.ChangeType(Object, Type)"/>.
		/// NOTE: Be care using with the value-types, the boxing mechanism may causes the performance issue.
		/// </summary>
		public static Boolean TryConvert<T>(this Object source, out T value, out Exception exception)
		{
			var type = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);  // Unwrap the type if it is nullable.
			try
			{
				value = (T)Convert.ChangeType(source, type);
				exception = null;
				return true;
			} catch (Exception e)
			{
				value = default(T);
				exception = e;
				return false;
			}
		}
	}
}
