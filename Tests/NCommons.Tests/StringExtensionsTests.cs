using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NCommons
{
	[TestClass]
	public class StringExtensionsTests
	{
		#region IsEmpty

		[TestMethod]
		public void StringIsEmpty_WithNull()
		{
			// Arrange

			// Test
			var isEmpty = Helpers.NullString.IsEmpty();

			// Assert
			Assert.IsTrue(isEmpty);
		}

		[TestMethod]
		public void StringIsEmpty_WithEmpty()
		{
			// Arrange

			// Test
			var isEmpty = Helpers.EmptyString.IsEmpty();

			// Assert
			Assert.IsTrue(isEmpty);
		}

		[TestMethod]
		public void StringIsEmpty_WithBlank()
		{
			// Arrange
			const String source = " ";

			// Test
			var isEmpty = source.IsEmpty();

			// Assert
			Assert.IsFalse(isEmpty);
		}

		[TestMethod]
		public void StringIsEmpty_WithNormalString()
		{
			// Arrange
			const String source = " abc 123";

			// Test
			var isEmpty = source.IsEmpty();

			// Assert
			Assert.IsFalse(isEmpty);
		}

		#endregion

		#region IsNotEmpty

		[TestMethod]
		public void StringIsNotEmpty_WithNull()
		{
			// Arrange

			// Test
			var isNotEmpty = Helpers.NullString.IsNotEmpty();

			// Assert
			Assert.IsFalse(isNotEmpty);
		}

		[TestMethod]
		public void StringIsNotEmpty_WithEmpty()
		{
			// Arrange

			// Test
			var isNotEmpty = Helpers.EmptyString.IsNotEmpty();

			// Assert
			Assert.IsFalse(isNotEmpty);
		}

		[TestMethod]
		public void StringIsNotEmpty_WithBlank()
		{
			// Arrange
			const String source = " ";

			// Test
			var isNotEmpty = source.IsNotEmpty();

			// Assert
			Assert.IsTrue(isNotEmpty);
		}

		[TestMethod]
		public void StringIsNotEmpty_WithNormalString()
		{
			// Arrange
			const String source = " abc 123";

			// Test
			var isNotEmpty = source.IsNotEmpty();

			// Assert
			Assert.IsTrue(isNotEmpty);
		}

		#endregion

		#region IsBlank

		[TestMethod]
		public void StringIsBlank_WithNull()
		{
			// Arrange

			// Test
			var isBlank = Helpers.NullString.IsBlank();

			// Assert
			Assert.IsTrue(isBlank);
		}

		[TestMethod]
		public void StringIsBlank_WithEmpty()
		{
			// Arrange

			// Test
			var isBlank = Helpers.EmptyString.IsBlank();

			// Assert
			Assert.IsTrue(isBlank);
		}

		[TestMethod]
		public void StringIsBlank_WithBlank()
		{
			// Arrange
			const String source = " ";

			// Test
			var isBlank = source.IsBlank();

			// Assert
			Assert.IsTrue(isBlank);
		}

		[TestMethod]
		public void StringIsBlank_WithNormalString()
		{
			// Arrange
			const String source = " abc 123";

			// Test
			var isBlank = source.IsBlank();

			// Assert
			Assert.IsFalse(isBlank);
		}

		#endregion

		#region IsNotBlank

		[TestMethod]
		public void StringIsNotBlank_WithNull()
		{
			// Arrange

			// Test
			var isNotBlank = Helpers.NullString.IsNotBlank();

			// Assert
			Assert.IsFalse(isNotBlank);
		}

		[TestMethod]
		public void StringIsNotBlank_WithEmpty()
		{
			// Arrange

			// Test
			var isNotBlank = Helpers.EmptyString.IsNotBlank();

			// Assert
			Assert.IsFalse(isNotBlank);
		}

		[TestMethod]
		public void StringIsNotBlank_WithBlank()
		{
			// Arrange
			const String source = " ";

			// Test
			var isNotBlank = source.IsNotBlank();

			// Assert
			Assert.IsFalse(isNotBlank);
		}

		[TestMethod]
		public void StringIsNotBlank_WithNormalString()
		{
			// Arrange
			const String source = " abc 123";

			// Test
			var isNotBlank = source.IsNotBlank();

			// Assert
			Assert.IsTrue(isNotBlank);
		}

		#endregion

		#region IsMatch

		[TestMethod]
		public void StringIsMatch()
		{
			// Arrange
			const String source = " abc 123";

			// Test
			var isMatch = source.IsMatch(@"^\s\w{3} \d{3}$");

			// Assert
			Assert.IsTrue(isMatch);
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void StringIsMatch_SourceIsNull()
		{
			// Arrange
			const String source = null;

			// Test
			source.IsMatch(@".*");

			// Assert by ExpectedExceptionAttribute
		}


		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void StringIsMatch_PatternIsNull()
		{
			// Arrange
			const String source = " foo";

			// Test
			source.IsMatch(null);

			// Assert by ExpectedExceptionAttribute
		}

		#endregion

		#region RegexReplace

		[TestMethod]
		public void StringRegexReplace()
		{
			// Arrange
			const String source = " abc 123";

			// Test
			var replacement = source.RegexReplace(@"\s", "\t");

			// Assert
			Assert.AreEqual("\tabc\t123", replacement);
		}

		#endregion

	}
}
