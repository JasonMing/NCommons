using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Nextension.Annotations;
using Nextension.Properties;

namespace Nextension
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
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ArgumentNotNull(Object source, [InvokerParameterName] String name)
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
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ArgumentNotNull(Object source, [InvokerParameterName] String name, String message)
		{
			if (source == null)
			{
				throw new ArgumentNullException(message, name);
			}
		}

		/// <summary>
		/// Ensure the <paramref name="source"/> is not null or zero-length.
		/// </summary>
		/// <param name="source">The <see cref="IEnumerable"/> to be ensured.</param>
		/// <param name="name">The object's name.</param>
		/// <exception cref="ArgumentException"><paramref name="source "/> is null or zero-length.</exception>
		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ArgumentNotEmpty(IEnumerable source, [InvokerParameterName] String name)
		{
			if (source.IsEmpty())
			{
				throw new ArgumentException(Resources.ArgumentCannotBeEmpty, name);
			}
		}

		/// <summary>
		/// Ensure the <paramref name="source"/> is not null.
		/// </summary>
		/// <param name="source">The <see cref="IEnumerable"/> to be ensured.</param>
		/// <param name="name">The object's name.</param>
		/// <param name="message">The message of the throwing exception.</param>
		/// <exception cref="ArgumentException"><paramref name="source "/> is null or zero-length.</exception>
		[DebuggerStepThrough]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ArgumentNotEmpty(IEnumerable source, [InvokerParameterName] String name, String message)
		{
			if (source.IsEmpty())
			{
				throw new ArgumentException(message, name);
			}
		}

	}
}