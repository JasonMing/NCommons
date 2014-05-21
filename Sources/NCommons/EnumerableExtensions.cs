using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NCommons
{
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Determines whether the <paramref name="source"/> is null or zero-length.
		/// </summary>
		/// <param name="source">The non-generic version <see cref="IEnumerable"/>.</param>
		/// <returns><c>true</c> means empty, <c>false</c> otherwise.</returns>
		[DebuggerStepThrough]
		public static Boolean IsEmpty(this IEnumerable source)
		{
			return source == null || !source.GetEnumerator().MoveNext();
		}

		/// <summary>
		/// Determines whether the <paramref name="source"/> is neither null nor zero-length.
		/// </summary>
		/// <param name="source">The non-generic version <see cref="IEnumerable"/>.</param>
		/// <returns><c>true</c> means not empty, <c>false</c> otherwise.</returns>
		[DebuggerStepThrough]
		public static Boolean IsNotEmpty(this IEnumerable source)
		{
			return source != null && source.GetEnumerator().MoveNext();
		}

		/// <summary>
		/// Determines whether the <paramref name="source"/> is null or zero-length.
		/// </summary>
		/// <param name="source">The generic version <see cref="IEnumerable{T}"/>.</param>
		/// <returns><c>true</c> means empty, <c>false</c> otherwise.</returns>
		[DebuggerStepThrough]
		public static Boolean IsEmpty<TSource>(this IEnumerable<TSource> source)
		{
			return source == null || !source.Any();
		}

		/// <summary>
		/// Determines whether the <paramref name="source"/> is neither null nor zero-length.
		/// </summary>
		/// <param name="source">The generic version <see cref="IEnumerable{T}"/>.</param>
		/// <returns><c>true</c> means not empty, <c>false</c> otherwise.</returns>
		[DebuggerStepThrough]
		public static Boolean IsNotEmpty<TSource>(this IEnumerable<TSource> source)
		{
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
		public static Boolean IsIn<TSource>(this TSource source, params TSource[] sources)
		{
			if (sources == null)
			{
				return false;
			}

			return sources.Any(item => Equals(item, source));
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
		public static Boolean IsNotIn<TSource>(this TSource source, params TSource[] sources)
		{
			if (sources == null)
			{
				return true;
			}

			return sources.All(item => !Equals(item, source));
		}

		/// <summary>
		/// Produces the set difference of two sequences by using the specified <paramref name="keySelector"/> to select the key for comparing values.
		/// Note: This method handles the <c>null</c> sequence as the empty sequence.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <typeparam name="TKey">The type of selected key of the elements of the input sequences.</typeparam>
		/// <param name="first">An <see cref="IEnumerable{TSource}"/> whose elements that are not also in <paramref name="second"/> will be returned.</param>
		/// <param name="second">An <see cref="IEnumerable{TSource}"/> whose elements that also occur in the <paramref name="first"/> sequence will cause those elements to be removed from the returned sequence.</param>
		/// <param name="keySelector">Selector for select the key of elements of input sequences.</param>
		/// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="keySelector"/> is <c>null</c>.</exception>
		/// <seealso cref="Enumerable.Except{TSource}(IEnumerable{TSource}, IEnumerable{TSource}, IEqualityComparer{TSource})"/>
		public static IEnumerable<TSource> Except<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> keySelector)
		{
			Ensure.NotNull(keySelector, "keySelector");

			if (first == null)
			{
				return null;
			}

			if (second == null)
			{
				return first;
			}

			return first.Except(second, ToEqualityComparer(keySelector));
		}

		/// <summary>
		/// Produces the set intersection of two sequences by using the specified <paramref name="keySelector"/> to select the key for comparing values.
		/// Note: This method handles the <c>null</c> sequence as the empty sequence.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <typeparam name="TKey">The type of selected key of the elements of the input sequences.</typeparam>
		/// <param name="first"> An <see cref="IEnumerable{TSource}"/> whose distinct elements that also appear in <paramref name="second"/> will be returned.</param>
		/// <param name="second">An <see cref="IEnumerable{TSource}"/> whose distinct elements that also appear in <paramref name="first"/> will be returned.</param>
		/// <param name="keySelector">Selector for select the key of elements of input sequences.</param>
		/// <returns>A sequence that contains the elements that form the set intersection of two sequences.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="keySelector"/> is <c>null</c>.</exception>
		/// <seealso cref="Enumerable.Intersect{TSource}(IEnumerable{TSource}, IEnumerable{TSource}, IEqualityComparer{TSource})"/>
		public static IEnumerable<TSource> Intersect<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second,
			Func<TSource, TKey> keySelector)
		{
			Ensure.NotNull(keySelector, "keySelector");

			if (first == null || second == null)
			{
				return null;
			}

			return first.Intersect(second, ToEqualityComparer(keySelector));
		}

		/// <summary>
		/// Produces the set union of two sequences by using a specified <paramref name="keySelector"/> to select the key for comparing values.
		/// Note: This method handles the <c>null</c> sequence as the empty sequence.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
		/// <typeparam name="TKey">The type of selected key of the elements of the input sequences.</typeparam>
		/// <param name="first"> An <see cref="IEnumerable{TSource}"/> whose distinct elements form the <paramref name="first"/> set for the union.</param>
		/// <param name="second">An <see cref="IEnumerable{TSource}"/> whose distinct elements form the <paramref name="second"/> set for the union</param>
		/// <param name="keySelector">Selector for select the key of elements of input sequences.</param>
		/// <returns>An <see cref="IEnumerable{T}"/> that contains the elements from both input sequences, excluding duplicates.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="keySelector"/> is <c>null</c>.</exception>
		/// <seealso cref="Enumerable.Intersect{TSource}(IEnumerable{TSource}, IEnumerable{TSource}, IEqualityComparer{TSource})"/>
		public static IEnumerable<TSource> Union<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> keySelector)
		{
			Ensure.NotNull(keySelector, "keySelector");

			if (first == null && second == null)
			{
				return null;
			}

			first = first ?? Enumerable.Empty<TSource>();
			second = second ?? Enumerable.Empty<TSource>();
			return first.Union(second, ToEqualityComparer(keySelector));
		}

		/// <summary>
		/// Returns distinct elements from a sequence by using a specified <paramref name="keySelector"/> to select the key for comparing values.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <typeparam name="TKey">The type of selected key of the elements of the input sequences.</typeparam>
		/// <param name="source">The sequence to remove duplicate elements from.</param>
		/// <param name="keySelector">Selector for select the key from <paramref name="source"/>.</param>
		/// <returns>An <see cref="IEnumerable{TSource}"/> that contains distinct elements from the <paramref name="source"/> sequence.</returns>
		public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			Ensure.NotNull(keySelector, "keySelector");

			if (source == null)
			{
				return null;
			}

			return source.Distinct(ToEqualityComparer(keySelector));
		}

		/// <summary>
		/// Produces the sequence merge of two sequences by using a specified <paramref name="mergeSelector"/> to merge element of <paramref name="first"/> and <paramref name="second"/>.
		/// Note: This method handles the <c>null</c> sequence as the empty sequence.
		/// </summary>
		/// <typeparam name="TFirst">The type of the elements of <paramref name="first"/>.</typeparam>
		/// <typeparam name="TSecond">The type of the elements of <paramref name="second"/>.</typeparam>
		/// <typeparam name="TResult">The type of elements of returns.</typeparam>
		/// <param name="first">The first sequence for merge.</param>
		/// <param name="second">The second sequnece for merge.</param>
		/// <param name="mergeSelector">Picks two elements into on <typeparamref name="TResult"/>.</param>
		/// <returns>An <see cref="IEnumerable{TResult}"/> that contains new <typeparamref name="TResult"/> elements from <paramref name="first"/> and <paramref name="second"/>.</returns>
		public static IEnumerable<TResult> Merge<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> mergeSelector)
		{
			Ensure.NotNull(mergeSelector, "mergeSelector");

			var firstsEnumerator = first.GetEnumerator();
			var secondsEnumerator = second.GetEnumerator();

			while (true)
			{
				if (firstsEnumerator.MoveNext() && secondsEnumerator.MoveNext())
				{
					yield return mergeSelector(firstsEnumerator.Current, secondsEnumerator.Current);
				} else
				{
					yield break;
				}
			}
		}

		/// <summary>
		/// Enumerate the <paramref name="source"/> with <paramref name="action"/>.
		/// </summary>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
		/// <param name="source">The sequence to enumerate. It's null-safe.</param>
		/// <param name="action">The action to do for each elements.</param>
		public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
		{
			if (source == null)
			{
				return;
			}
			foreach (var item in source)
			{
				action(item);
			}
		}

		private static IEqualityComparer<TSource> ToEqualityComparer<TSource, TKey>(Func<TSource, TKey> keySelector)
		{
			return new KeyEqualityComparer<TSource, TKey>(keySelector);
		}

		private sealed class KeyEqualityComparer<T, TKey> : IEqualityComparer<T>
		{
			private static readonly EqualityComparer<TKey> InnerEqualityComparer = EqualityComparer<TKey>.Default;

			private readonly Func<T, TKey> keySelector;

			public KeyEqualityComparer(Func<T, TKey> keySelector)
			{
				this.keySelector = keySelector;
			}

			public Boolean Equals(T x, T y)
			{
				return InnerEqualityComparer.Equals(this.keySelector(x), this.keySelector(y));
			}

			public Int32 GetHashCode(T obj)
			{
				return InnerEqualityComparer.GetHashCode(this.keySelector(obj));
			}
		}
	}
}
