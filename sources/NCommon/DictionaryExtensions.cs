using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NCommon
{
	/// <summary>
	/// The extensions for <see cref="IDictionary{TKey,TValue}"/>.
	/// Should NOT use this class directly.
	/// </summary>
	public static class DictionaryExtensions
	{
		#region SafeDictioanry

		/// <summary>
		/// Wrap the <paramref name="source"/> by the access-safe dictionary.
		/// </summary>
		public static IDictionary<TKey, TValue> AsSafeDictionary<TKey, TValue>(this IDictionary<TKey, TValue> source)
		{
			Ensure.ArgumentNotNull(source, "source");
			return (source as SafeDictionary<TKey, TValue>) ?? new SafeDictionary<TKey, TValue>(source);
		}

		/// <summary>
		/// Create a new <see cref="IDictionary{TKey,TValue}"/> and copy all entries in <paramref name="source"/>.
		/// </summary>
		public static IDictionary<TKey, TValue> ToSafeDictionary<TKey, TValue>(this IDictionary<TKey, TValue> source)
		{
			return AsSafeDictionary(new Dictionary<TKey, TValue>(source));
		}

		/// <summary>
		/// Create a new <see cref="IDictionary{TKey,TValue}"/> and copy all entries in <paramref name="source"/> with <paramref name="comparer"/>.
		/// </summary>
		public static IDictionary<TKey, TValue> ToSafeDictionary<TKey, TValue>(this IDictionary<TKey, TValue> source, IEqualityComparer<TKey> comparer)
		{
			return AsSafeDictionary(new Dictionary<TKey, TValue>(source, comparer));
		}

		/// <summary>
		/// Create a new <see cref="IDictionary{TKey,TValue}"/>, by using <paramref name="keySelector"/> to select keys and entries of <paramref name="source"/> as values.
		/// A shorcut for <code>source.ToDictionary(keySelector).AsSafeDictionary()</code>.
		/// </summary>
		public static IDictionary<TKey, TSource> ToSafeDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			Ensure.ArgumentNotNull(keySelector, "keySelector");

			return source.ToDictionary(keySelector).AsSafeDictionary();
		}

		/// <summary>
		/// Create a new <see cref="IDictionary{TKey,TValue}"/>, by using <paramref name="keySelector"/> to select keys, with <paramref name="comparer"/>.
		/// A shorcut for <code>source.ToDictionary(keySelector, comparer).AsSafeDictionary()</code>.
		/// </summary>
		/// <exception cref="ArgumentNullException"><paramref name="keySelector"/> returns null, or <paramref name="comparer"/> is null.</exception>
		public static IDictionary<TKey, TSource> ToSafeDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			Ensure.ArgumentNotNull(keySelector, "keySelector");
			Ensure.ArgumentNotNull(comparer, "comparer");

			return source.ToDictionary(keySelector, comparer).AsSafeDictionary();
		}

		/// <summary>
		/// Create a new <see cref="IDictionary{TKey,TValue}"/>, by using <paramref name="keySelector"/> to select keys and <paramref name="elementSeletor"/> to select values.
		/// A shorcut for <code>source.ToDictionary(keySelector, elementSelector).AsSafeDictionary()</code>.
		/// </summary>
		/// <exception cref="ArgumentNullException"><paramref name="keySelector"/> returns null.</exception>
		public static IDictionary<TKey, TElement> ToSafeDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSeletor)
		{
			Ensure.ArgumentNotNull(keySelector, "keySelector");
			Ensure.ArgumentNotNull(elementSeletor, "elementSeletor");

			return source.ToDictionary(keySelector, elementSeletor).AsSafeDictionary();
		}

		/// <summary>
		/// Create a new <see cref="IDictionary{TKey,TValue}"/>, by using <paramref name="keySelector"/> to select keys and <paramref name="elementSeletor"/> to select values, with <paramref name="comparer"/>.
		/// A shorcut for <code>source.ToDictionary(keySelector, elementSelector, comparer).AsSafeDictionary()</code>.
		/// </summary>
		/// <exception cref="ArgumentNullException"><paramref name="keySelector"/> returns null, or <paramref name="comparer"/> is null.</exception>
		public static IDictionary<TKey, TElement> ToSafeDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSeletor, IEqualityComparer<TKey> comparer)
		{
			Ensure.ArgumentNotNull(keySelector, "keySelector");
			Ensure.ArgumentNotNull(elementSeletor, "elementSeletor");
			Ensure.ArgumentNotNull(comparer, "comparer");

			return source.ToDictionary(keySelector, elementSeletor, comparer).AsSafeDictionary();
		}

		#endregion

		#region TryGetValue

		/// <summary>
		/// Try getting the value in the specific type <typeparamref name="TOutValue"/> from the <paramref name="source"/>.
		/// </summary>
		/// <typeparam name="TKey">The key type of the <paramref name="source"/> dictionary.</typeparam>
		/// <typeparam name="TSourceValue">The value type of the <paramref name="source"/> dictionary.</typeparam>
		/// <typeparam name="TOutValue">The type of value you want cast to.</typeparam>
		/// <param name="source">The source dictionary.</param>
		/// <param name="key">The key to find in the <paramref name="source"/>.</param>
		/// <param name="value">The result value, if returns <c>true</c> this value is valid, else the <c>default(<typeparamref name="TOutValue"/>)</c> will be set.</param>
		/// <param name="convert">Indicates should use the <see cref="Convert.ChangeType(object,System.Type)"/> to convert the value.</param>
		/// <returns><c>true</c> indicates the <paramref name="value"/> is valid, otherwise <c>false</c>.</returns>
		public static Boolean TryGetValue<TKey, TSourceValue, TOutValue>(this IDictionary<TKey, TSourceValue> source, TKey key, out TOutValue value, Boolean convert = false)
		{
			TSourceValue sourceValue;

			if (source.TryGetValue(key, out sourceValue))
			{
				// Try cast to TOutValue.
				if (sourceValue.TryCast(out value))
				{
					return true;
				}

				// Try convert if convertible.
				if (convert && sourceValue.TryConvert(out value))
				{
					return true;
				}
			}

			value = default(TOutValue);
			return false;
		}

		/// <summary>
		/// Try getting the value from the non-generic version <see cref="IDictionary"/> and change the value to the specific type <typeparamref name="TValue"/>.
		/// </summary>
		/// <typeparam name="TValue">The type of value you want change to.</typeparam>
		/// <param name="source">The source dictionary.</param>
		/// <param name="key">The key to find in the <paramref name="source"/>.</param>
		/// <param name="value">The result value, if returns <c>true</c> this value is valid, else the <c>default(<typeparamref name="TValue"/>)</c> will be set.</param>
		/// <param name="convert">Indicates should use the <see cref="Convert.ChangeType(object,System.Type)"/> to convert the value.</param>
		/// <returns><c>true</c> indicates the <paramref name="value"/> is valid, otherwise <c>false</c>.</returns>
		public static Boolean TryGetValue<TValue>(this IDictionary source, Object key, out TValue value, Boolean convert = false)
		{
			var sourceValue = source[key];

			if (sourceValue != null)
			{
				// Try cast to TOutValue.
				if (sourceValue.TryCast(out value))
				{
					return true;
				}

				// Try convert if convertible.
				if (convert && sourceValue.TryConvert(out value))
				{
					return true;
				}
			}

			value = default(TValue);
			return false;
		}

		/// <summary>
		/// Try get value or default.
		/// </summary>
		public static TValue TryGetValue<TSource, TValue>(this IDictionary<TSource, TValue> source, TSource key)
		{
			TValue value;
			return (source != null && source.TryGetValue(key, out value)) ? value : default(TValue);
		}

		#endregion

	}
}
