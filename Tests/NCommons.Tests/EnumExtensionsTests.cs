using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCommons;

namespace Commons
{
	[TestClass]
	public class EnumExtensionsTests
	{
		private enum Operation
		{
			None = 0,

			Add = 1,

			Minus = 2,

			Mutli = 3,

			Divide = 4,
		}

		#region IsDefined

		[TestMethod]
		public void EnumIsDefined_WithExistEnum()
		{
			// Arrange
			const Operation @enum = Operation.Add;

			// Test
			var isDefined = @enum.IsDefined();

			// Assert
			Assert.IsTrue(isDefined);
		}

		[TestMethod]
		public void EnumIsDefined_WithUnexistEnum()
		{
			// Arrange
			const Operation @enum = (Operation)0xFFFFF;

			// Test
			var isDefined = @enum.IsDefined();

			// Assert
			Assert.IsFalse(isDefined);
		}

		[TestMethod]
		public void EnumIsDefined_WithNullEnum()
		{
			// Arrange

			// Test
			var isDefined = ((Enum)null).IsDefined();

			// Assert
			Assert.IsFalse(isDefined);
		}

		#endregion


		#region IsDefinedFor

		[TestMethod]
		public void EnumIsDefinedFor_WithExistEnumObject()
		{
			// Arrange
			Object enumObject = Operation.Add;

			// Test
			var isDefined = enumObject.IsDefinedFor<Operation>();

			// Assert
			Assert.IsTrue(isDefined);
		}

		[TestMethod]
		public void EnumIsDefinedFor_WithExistEnumUnderlyingValue()
		{
			// Arrange
			const Int32 enumValue = 3;

			// Test
			var isDefined = enumValue.IsDefinedFor<Operation>();

			// Assert
			Assert.IsTrue(isDefined);
		}

		[TestMethod]
		public void EnumIsDefinedFor_WithExistEnumName()
		{
			// Arrange
			const String enumName = "Add";

			// Test
			var isDefined = enumName.IsDefinedFor<Operation>();

			// Assert
			Assert.IsTrue(isDefined);
		}

		[TestMethod]
		public void EnumIsDefinedFor_WithExistEnumName_InMatchlessCases()
		{
			// Arrange
			const String enumName = "add";

			// Test
			var isDefined = enumName.IsDefinedFor<Operation>();

			// Assert
			Assert.IsFalse(isDefined);
		}

		[TestMethod]
		public void EnumIsDefinedFor_WithUnexistEnumObject()
		{
			// Arrange
			Object enumObject = (Operation)0xFFFF;

			// Test
			var isDefined = enumObject.IsDefinedFor<Operation>();

			// Assert
			Assert.IsFalse(isDefined);
		}

		[TestMethod]
		public void EnumIsDefinedFor_WithUnexistEnumUnderlyingValue()
		{
			// Arrange
			const Int32 enumValue = 0xFFFF;

			// Test
			var isDefined = enumValue.IsDefinedFor<Operation>();

			// Assert
			Assert.IsFalse(isDefined);
		}

		[TestMethod]
		public void EnumIsDefinedFor_WithNullSource()
		{
			// Arrange

			// Test
			var isDefined = ((Object)null).IsDefinedFor<Operation>();

			// Assert
			Assert.IsFalse(isDefined);
		}

		[TestMethod]
		public void EnumIsDefinedFor_WithUnexistEnumName()
		{
			// Arrange
			const String enumName = "Unexist";

			// Test
			var isDefined = enumName.IsDefinedFor<Operation>();

			// Assert
			Assert.IsFalse(isDefined);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void EnumIsDefinedFor_WithDifferentUnderlyingType()
		{
			// Arrange
			const Int64 enumValue = 3L;

			// Test
			enumValue.IsDefinedFor<Operation>();

			// Assert exceptions by attribute.
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void EnumIsDefinedFor_WithNonEnumTypeGenericParameter()
		{
			// Arrange
			const Int64 enumValue = 3L;

			// Test
			enumValue.IsDefinedFor<Int32>();

			// Assert exceptions by attribute.
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void EnumIsDefinedFor_WithNullEnumType()
		{
			// Arrange

			// Test
			((Object)null).IsDefinedFor(null);

			// Assert exceptions by attribute.
		}

		#endregion

		#region ToUnderlyingType

		[TestMethod]
		public void EnumToUnderlyingType()
		{
			// Arrange
			Enum @enum = Operation.Minus;

			// Test
			var underlyingType = @enum.ToUndelyingType();

			// Assert
			Assert.IsInstanceOfType(underlyingType, Enum.GetUnderlyingType(typeof(Operation)));
		}

		[TestMethod]
		public void EnumToUnderlyingType_WithNullSource()
		{
			// Arrange

			// Test
			var underlyingType = ((Enum)null).ToUndelyingType();

			// Assert
			Assert.IsNull(underlyingType);
		}

		#endregion

	}
}
