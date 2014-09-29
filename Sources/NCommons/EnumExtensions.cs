using System;
using System.Diagnostics;
using NCommons.Annotations;

namespace NCommons
{
	public static class EnumExtensions
	{
		/// <summary>
		/// Detect whether the <paramref name="source"/> is defined in the enum-declaration.
		/// </summary>
		/// <param name="source">The enum object. <c>null</c> will always be assumed undefined.</param>
		/// <returns>If <paramref name="source"/> is a defined value returns <c>true</c>, otherwise <c>false</c>.</returns>
		[DebuggerStepThrough]
		public static Boolean IsDefined([CanBeNull] this Enum source)
		{
			return source != null && Enum.IsDefined(source.GetType(), source);
		}

		/// <summary>
		/// Detect whether the <paramref name="source"/> is a valid representation of the <typeparamref name="TEnum"/>.
		/// The valid enum representation is: enum instance, enum literal, enum underlying type instance, enum underlying type literal.
		/// </summary>
		/// <typeparam name="TEnum">The enum type.</typeparam>
		/// <param name="source">The enum object. <c>null</c> will always be assumed undefined.</param>
		/// <returns>If <paramref name="source"/> is a defined value returns <c>true</c>, otherwise <c>false</c>.</returns>
		[DebuggerStepThrough]
		public static Boolean IsDefinedFor<TEnum>([CanBeNull] this Object source)
			where TEnum : struct
		{
#if NETFX4
			if (!typeof(TEnum).IsEnum)
			{
				throw new ArgumentException(Properties.Resources.TypeMustBeEnum);
			}
#else
			// Ignores the type checking, passing to the Enum.IsDefined(Type, Object);
#endif
			return IsDefinedFor(source, typeof(TEnum));
		}

		/// <summary>
		/// Detect whether the <paramref name="source"/> is a valid representation of the <paramref name="enumType"/>.
		/// The valid enum representation is: enum instance, enum literal, enum underlying type instance, enum underlying type literal.
		/// </summary>
		/// <param name="source">The enum object. <c>null</c> will always be assumed undefined.</param>
		/// <param name="enumType">The enum type.</param>
		/// <returns>If <paramref name="source"/> is a defined value returns <c>true</c>, otherwise <c>false</c>.</returns>
		[DebuggerStepThrough]
		public static Boolean IsDefinedFor([CanBeNull] this Object source, [NotNull] Type enumType)
		{
#if NETFX4
			enumType.CheckEnumType();
#else
			// Ignores the type checking, passing to the Enum.IsDefined(Type, Object);
#endif

			return source != null && Enum.IsDefined(enumType, source);
		}

		/// <summary>
		/// Change the enum to their underlying type.
		/// </summary>
		/// <param name="source">The enum object, passing <c>null</c> is permitted.</param>
		/// <returns>The boxed instance of the enum underlying type, <c>null</c> is returned is <paramref name="source"/> is.</returns>
		[DebuggerStepThrough]
		public static Object ToUndelyingType([CanBeNull] this Enum source)
		{
			if (source == null)
			{
				return null;
			}

#if NETFX4
			var underlyingObject = Convert.ChangeType(source, source.GetTypeCode());
#else
			var underlyingObject = Convert.ChangeType(source, source.GetType());
#endif

			return underlyingObject;
		}

#if NETFX4
		private static void CheckEnumType(this Type enumType)
		{
			Ensure.NotNull(enumType, "enumType");

			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Properties.Resources.TypeMustBeEnum, "enumType");
			}
		}
#endif

	}
}
