using System;
using System.Collections;
using System.Collections.Generic;

namespace Nextension
{
	[Serializable]
	class SafeDictionary<TKey, TValue> : IDictionary<TKey, TValue>
	{
		private readonly IDictionary<TKey, TValue> inner;

		/// <summary>
		/// Create a <see cref="SafeDictionary{TKey,TValue}"/> that uses <paramref name="dictionary"/> as the inner data.
		/// </summary>
		/// <param name="dictionary">The referenced source dictionary.</param>
		public SafeDictionary(IDictionary<TKey, TValue> dictionary)
		{
			this.inner = dictionary;
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return this.inner.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.inner).GetEnumerator();
		}

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			this.inner.Add(item);
		}

		public void Clear()
		{
			this.inner.Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return this.inner.Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			this.inner.CopyTo(array, arrayIndex);
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return this.inner.Remove(item);
		}

		public int Count
		{
			get { return this.inner.Count; }
		}

		public bool IsReadOnly
		{
			get { return this.inner.IsReadOnly; }
		}

		public bool ContainsKey(TKey key)
		{
			return this.inner.ContainsKey(key);
		}

		public void Add(TKey key, TValue value)
		{
			this.inner.Add(key, value);
		}

		public bool Remove(TKey key)
		{
			return this.inner.Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return this.inner.TryGetValue(key, out value);
		}

		public TValue this[TKey key]
		{
			get
			{
				TValue value;
				return this.TryGetValue(key, out value) ? value : default(TValue);
			}
			set { this.inner[key] = value; }
		}

		public ICollection<TKey> Keys
		{
			get { return this.inner.Keys; }
		}

		public ICollection<TValue> Values
		{
			get { return this.inner.Values; }
		}
	}
}
