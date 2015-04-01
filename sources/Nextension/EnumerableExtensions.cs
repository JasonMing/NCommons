using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Nextension.Annotations;

namespace Nextension
{
	/// <summary>
	/// The extensions for any <see cref="IEnumerable{T}"/>.
	/// Should NOT use this class directly.
	/// </summary>
	public static class EnumerableExtensions
	{

		#region Determination

		/// <summary>
		/// Determines whether the <paramref name="source"/> is null or zero-length.
		/// </summary>
		/// <param name="source">The non-generic version <see cref="IEnumerable"/>.</param>
		/// <returns><c>true</c> means empty, <c>false</c> otherwise.</returns>
		[DebuggerStepThrough]
		public static Boolean IsEmpty([CanBeNull] this IEnumerable source)
		{
			var asCollection = source as ICollection;
			if (asCollection != null)
			{
				return asCollection.Count == 0;
			}
			return source == null || !source.GetEnumerator().MoveNext();
		}

		/// <summary>
		/// Determines whether the <paramref name="source"/> is neither null nor zero-length.
		/// </summary>
		/// <param name="source">The non-generic version <see cref="IEnumerable"/>.</param>
		/// <returns><c>true</c> means not empty, <c>false</c> otherwise.</returns>
		[DebuggerStepThrough]
		public static Boolean IsNotEmpty([CanBeNull] this IEnumerable source)
		{
			var asCollection = source as ICollection;
			if (asCollection != null)
			{
				return asCollection.Count > 0;
			}
			return source != null && source.GetEnumerator().MoveNext();
		}

		/// <summary>
		/// Determines whether the <paramref name="source"/> is null or zero-length.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <param name="source"> The generic version <see cref="IEnumerable{T}"/>. </param>
		/// <returns> <c>true</c> means empty, <c>false</c> otherwise. </returns>
		[DebuggerStepThrough]
		public static Boolean IsEmpty<TSource>([CanBeNull] this IEnumerable<TSource> source)
		{
			var asCollection = source as ICollection;
			if (asCollection != null)
			{
				return asCollection.Count == 0;
			}
			return source == null || !source.Any();
		}

		/// <summary>
		/// Determines whether the <paramref name="source"/> is neither null nor zero-length.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <param name="source">The generic version <see cref="IEnumerable{T}"/>.</param>
		/// <returns><c>true</c> means not empty, <c>false</c> otherwise.</returns>
		[DebuggerStepThrough]
		public static Boolean IsNotEmpty<TSource>([CanBeNull] this IEnumerable<TSource> source)
		{
			var asCollection = source as ICollection;
			if (asCollection != null)
			{
				return asCollection.Count > 0;
			}
			return source != null && source.Any();
		}

		/// <summary>
		/// Determines whether the <paramref name="source"/> is in <paramref name="sources"/>.
		/// This is the shortcut of <c>sources.Any(s => s == source)</c>.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <param name="source">The element to determine whether is in <paramref name="sources"/>.</param>
		/// <param name="sources">The sequence for determining whether <paramref name="source"/> is in.</param>
		/// <returns><c>true</c> means <paramref name="source"/> is in <paramref name="sources"/>, <c>false</c> otherwise.</returns>
		[DebuggerStepThrough]
		public static Boolean IsIn<TSource>([CanBeNull] this TSource source, params TSource[] sources)
		{
			return sources.IsNotEmpty() && sources.Any(item => Equals(item, source));
		}

		/// <summary>
		/// Determines whether the <paramref name="source"/> is in <paramref name="sources"/>.
		/// This is the shortcut of <c>sources.Any(s => s == source)</c>.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <param name="source">The element to determine whether is in <paramref name="sources"/>.</param>
		/// <param name="comparer">The comparer for <typeparamref name="TSource"/>.</param>
		/// <param name="sources">The sequence for determining whether <paramref name="source"/> is in.</param>
		/// <returns><c>true</c> means <paramref name="source"/> is in <paramref name="sources"/>, <c>false</c> otherwise.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="comparer"/> is null.</exception>
		public static Boolean IsIn<TSource>([CanBeNull] this TSource source, IEqualityComparer<TSource> comparer, params TSource[] sources)
		{
			Ensure.ArgumentNotNull(comparer, "comparer");

			return sources.IsNotEmpty() && sources.Any(item => comparer.Equals(item, source));
		}

		/// <summary>
		/// Determines whether the <paramref name="source"/> is not in <paramref name="sources"/>.
		/// This is the shortcut of <c>sources.All(s => s != source)</c>.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <param name="source">The element to determine whether is not in <paramref name="sources"/>.</param>
		/// <param name="sources">The sequence for determining whether <paramref name="source"/> is not in.</param>
		/// <returns><c>true</c> means <paramref name="source"/> is not in <paramref name="sources"/>, <c>false</c> otherwise.</returns>
		[DebuggerStepThrough]
		public static Boolean IsNotIn<TSource>([CanBeNull] this TSource source, params TSource[] sources)
		{
			return sources.IsEmpty() || sources.All(item => !Equals(item, source));
		}

		/// <summary>
		/// Determines whether the <paramref name="source"/> is not in <paramref name="sources"/>.
		/// This is the shortcut of <c>sources.All(s => s != source)</c>.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <param name="source">The element to determine whether is not in <paramref name="sources"/>.</param>
		/// <param name="comparer">The comparer for <typeparamref name="TSource"/>.</param>
		/// <param name="sources">The sequence for determining whether <paramref name="source"/> is not in.</param>
		/// <returns><c>true</c> means <paramref name="source"/> is not in <paramref name="sources"/>, <c>false</c> otherwise.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="comparer"/> is null.</exception>
		[DebuggerStepThrough]
		public static Boolean IsNotIn<TSource>([CanBeNull] this TSource source, IEqualityComparer<TSource> comparer, params TSource[] sources)
		{
			Ensure.ArgumentNotNull(comparer, "comparer");

			return sources.IsEmpty() || sources.All(item => !comparer.Equals(item, source));
		}

		#endregion

		#region Modification

		/// <summary>
		/// Convenient method for <see cref="Enumerable.Concat{TSource}"/> allows compile-time params argument as the collection.
		/// </summary>
		/// <typeparam name="TSource">The elements type.</typeparam>
		/// <param name="first">The collection to be concat as the first part.</param>
		/// <param name="second">The collection (params arguments) to be concat to the tail of <paramref name="first"/>.</param>
		/// <returns>The concated collection.</returns>
		[NotNull]
		public static IEnumerable<TSource> Concat<TSource>([CanBeNull] this IEnumerable<TSource> first, params TSource[] second)
		{
			// ReSharper disable once InvokeAsExtensionMethod
			return Enumerable.Concat(first ?? Enumerable.Empty<TSource>(), second ?? Enumerable.Empty<TSource>());
		}

		/// <summary>
		/// Concat the (params arguments) collection to the head of <paramref name="second"/>.
		/// </summary>
		/// <typeparam name="TSource">The elements type.</typeparam>
		/// <param name="second">The collection (params arguments) to be concat to the head of <paramref name="first"/>.</param>
		/// <param name="first">The collection to be concat as the first part.</param>
		/// <returns>The concated collection.</returns>
		[NotNull]
		public static IEnumerable<TSource> ConcatHead<TSource>([CanBeNull] this IEnumerable<TSource> second, params TSource[] first)
		{
			// ReSharper disable once InvokeAsExtensionMethod
			return Enumerable.Concat(first ?? Enumerable.Empty<TSource>(), second ?? Enumerable.Empty<TSource>());
		}

		/// <summary>
		/// Returns distinct elements from a sequence by using a specified <paramref name="keySelector"/> to select the key for comparing values.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">The type of selected key of the elements of the input sequences.</typeparam>
		/// <param name="source">The sequence to remove duplicate elements from.</param>
		/// <param name="keySelector">Selector for select the key from <paramref name="source"/>.</param>
		/// <returns>An <see cref="IEnumerable{TSource}"/> that contains distinct elements from the <paramref name="source"/> sequence.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="keySelector"/> is null.</exception>
		public static IEnumerable<TSource> Distinct<TSource, TKey>([CanBeNull] this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			Ensure.ArgumentNotNull(keySelector, "keySelector");

			return source.ValueOrDefault(x => x.Distinct(ToEqualityComparer(keySelector)));
		}

		/// <summary>
		/// Returns distinct elements from a sequence by using a specified <paramref name="keySelector"/> to select the key for comparing values.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">The type of selected key of the elements of the input sequences.</typeparam>
		/// <param name="source">The sequence to remove duplicate elements from.</param>
		/// <param name="keySelector">Selector for select the key from <paramref name="source"/>.</param>
		/// <param name="keyComparer">The <see cref="IEqualityComparer{T}"/> for the <typeparamref name="TKey"/>.</param>
		/// <returns>An <see cref="IEnumerable{TSource}"/> that contains distinct elements from the <paramref name="source"/> sequence.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="keySelector"/> or <paramref name="keyComparer"/> is null.</exception>
		public static IEnumerable<TSource> Distinct<TSource, TKey>([CanBeNull] this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> keyComparer)
		{
			Ensure.ArgumentNotNull(keySelector, "keySelector");
			Ensure.ArgumentNotNull(keyComparer, "keyComparer");

			return source.ValueOrDefault(x => x.Distinct(ToEqualityComparer(keySelector, keyComparer)));
		}

		#endregion

		#region Iteration

		/// <summary>
		/// Enumerate the <paramref name="source"/> with <paramref name="action"/>.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <param name="source">The sequence to enumerate. It's null-safe.</param>
		/// <param name="action">The action to do for each elements.</param>
		/// <exception cref="ArgumentNullException"><paramref name="action"/> is null.</exception>
		public static void ForEach<TSource>([CanBeNull] this IEnumerable<TSource> source, [InstantHandle] Action<TSource> action)
		{
			Ensure.ArgumentNotNull(action, "action");

			if (source == null)
			{
				return;
			}

			foreach (var item in source)
			{
				action(item);
			}
		}

		/// <summary>
		/// Enumerate the <paramref name="source"/> with <paramref name="action"/>.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <param name="source">The sequence to enumerate. It's null-safe.</param>
		/// <param name="action">The action to do for each elements.</param>
		/// <exception cref="ArgumentNullException"><paramref name="action"/> is null.</exception>
		public static void ForEach<TSource>([CanBeNull] this IEnumerable<TSource> source, [InstantHandle] Action<Int32, TSource> action)
		{
			Ensure.ArgumentNotNull(action, "action");

			if (source == null)
			{
				return;
			}

			var i = 0;
			foreach (var item in source)
			{
				action(i, item);
				i += 1;
			}
		}

		#endregion

		/// <summary>
		/// Generic type helper for <see cref="KeyEqualityComparer{T,TKey}"/>.
		/// </summary>
		private static IEqualityComparer<TSource> ToEqualityComparer<TSource, TKey>(Func<TSource, TKey> keySelector)
		{
			return new KeyEqualityComparer<TSource, TKey>(keySelector);
		}

		/// <summary>
		/// Generic type helper for <see cref="KeyEqualityComparer{T,TKey}"/>.
		/// </summary>
		private static IEqualityComparer<TSource> ToEqualityComparer<TSource, TKey>(Func<TSource, TKey> keySelector, IEqualityComparer<TKey> keyComparer)
		{
			return new KeyEqualityComparer<TSource, TKey>(keySelector, keyComparer);
		}

		private sealed class KeyEqualityComparer<T, TKey> : IEqualityComparer<T>
		{
			private static readonly EqualityComparer<TKey> DefaultKeyEqualityComparer = EqualityComparer<TKey>.Default;

			private readonly Func<T, TKey> keySelector;

			private readonly IEqualityComparer<TKey> equalityComparer;

			public KeyEqualityComparer(Func<T, TKey> keySelector)
			{
				this.keySelector = keySelector;
				this.equalityComparer = DefaultKeyEqualityComparer;
			}

			public KeyEqualityComparer(Func<T, TKey> keySelector, IEqualityComparer<TKey> equalityComparer)
			{
				this.keySelector = keySelector;
				this.equalityComparer = equalityComparer;
			}

			public Boolean Equals(T x, T y)
			{
				return this.equalityComparer.Equals(this.keySelector(x), this.keySelector(y));
			}

			public Int32 GetHashCode(T obj)
			{
				return this.equalityComparer.GetHashCode(this.keySelector(obj));
			}
		}
	}
}
