using System;
using System.Collections;
using System.Diagnostics;

namespace Commons
{
	/// <summary>
	/// The utility to ensure the given key matches the specified rules.
	/// </summary>
	public static class Ensure
	{
		/// <summary>
		/// Ensure the <paramref name="source"/> is not null.
		/// </summary>
		/// <param name="source">The <see cref="Object"/> to be ensured.</param>
		/// <param name="name">The object's name.</param>
		/// <exception cref="ArgumentNullException"><paramref name="source "/> is null.</exception>
		[DebuggerStepThrough]
		public static void NotNull(Object source, String name)
		{
			if (source == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		/// <summary>
		/// Ensure the <paramref name="source"/> is not null.
		/// </summary>
		/// <param name="source">The <see cref="Object"/> to be ensured.</param>
		/// <param name="name">The object's name.</param>
		/// <param name="message">The message of the throwing exception.</param>
		/// <exception cref="ArgumentNullException"><paramref name="source "/> is null.</exception>
		[DebuggerStepThrough]
		public static void NotNull(Object source, String name, String message)
		{
			if (source == null)
			{
				throw new ArgumentNullException(name, message);
			}
		}

		/// <summary>
		/// Ensure the <paramref name="source"/> is not null or zero-length.
		/// </summary>
		/// <param name="source">The <see cref="IEnumerable"/> to be ensured.</param>
		/// <param name="name">The object's name.</param>
		/// <exception cref="ArgumentNullException"><paramref name="source "/> is null or zero-length.</exception>
		[DebuggerStepThrough]
		public static void NotEmpty(IEnumerable source, String name)
		{
			if (source == null || !source.GetEnumerator().MoveNext())
			{
				throw new ArgumentNullException(name);
			}
		}

		/// <summary>
		/// Ensure the <paramref name="source"/> is not null.
		/// </summary>
		/// <param name="source">The <see cref="IEnumerable"/> to be ensured.</param>
		/// <param name="name">The object's name.</param>
		/// <param name="message">The message of the throwing exception.</param>
		/// <exception cref="ArgumentNullException"><paramref name="source "/> is or zero-length.</exception>
		[DebuggerStepThrough]
		public static void NotEmpty(IEnumerable source, String name, String message)
		{
			if (source == null || !source.GetEnumerator().MoveNext())
			{
				throw new ArgumentNullException(name, message);
			}
		}
	}
}