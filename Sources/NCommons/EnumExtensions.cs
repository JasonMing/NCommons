using System;
using System.Diagnostics;
using NCommons.Properties;

namespace NCommons
{
	public static class EnumExtensions
	{
		[DebuggerStepThrough]
		public static Boolean IsDefined(this Enum source)
		{
			return source != null && Enum.IsDefined(source.GetType(), source);
		}

		[DebuggerStepThrough]
		public static Boolean IsDefinedFor<TEnum>(this Object source)
			where TEnum : struct
		{
			return IsDefinedFor(source, typeof(TEnum));
		}

		[DebuggerStepThrough]
		public static Boolean IsDefinedFor(this Object source, Type enumType)
		{
			enumType.CheckEnumType();

			if (source == null)
			{
				return false;
			}

			return Enum.IsDefined(enumType, source);
		}

		/// <summary>
		/// Change the enum to their underlying type.
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		[DebuggerStepThrough]
		public static Object ToUndelyingType(this Enum source)
		{
			if (source == null)
			{
				return null;
			}

			var underlyingObject = Convert.ChangeType(source, source.GetTypeCode());

			return underlyingObject;
		}

		[DebuggerStepThrough]
		private static void CheckEnumType(this Type enumType)
		{
			Ensure.NotNull(enumType, "enumType");

			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Resources.TypeMustBeEnum, "enumType");
			}
		}

	}
}
