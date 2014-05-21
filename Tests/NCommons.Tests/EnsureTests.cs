using System;
using System.Collections;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NCommons
{
	[TestClass]
	public class EnsureTests
	{
		private static String GetOriginMessage(ArgumentException e)
		{
			return e.Message.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).First();
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void EnsureNotNull_WithNull()
		{
			// Arrange
			Object source = null;

			// Test
			Ensure.NotNull(source, "source");

			// Assert
			Assert.Fail("Ensure.NotNull should be throw an ArugmentNullException");
		}

		[TestMethod]
		public void EnsureNotNull_WithNotNull()
		{
			// Arrange
			var source = new { };

			// Test
			Ensure.NotNull(source, "source");

			// Assert nothing
		}

		[TestMethod]
		public void EnsureNotNull_Null_WithWithMessage()
		{
			// Arrange
			Object source = null;
			const String message = "Some message";

			// Test
			try
			{
				Ensure.NotNull(source, "source", message);

				// Assert
				Assert.Fail("Ensure.NotNull should be throw an ArugmentNullException");
			} catch (ArgumentNullException e)
			{
				// Assert
				Assert.AreEqual(message, GetOriginMessage(e));
			}
		}

		[TestMethod]
		public void EnsureNotNull_NotNull_WithWithMessage()
		{
			// Arrange
			var source = new { };

			// Test
			Ensure.NotNull(source, "source", "Some message");

			// Assert nothing
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void EnsureNotEmpty_WithNull()
		{
			// Arrange
			IEnumerable source = null;

			// Test
			Ensure.NotEmpty(source, "source");

			// Assert
			Assert.Fail("Ensure.NotNull should be throw an ArugmentNullException");
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void EnsureNotEmpty_WithEmpty()
		{
			// Arrange
			var source = new Object[0];

			// Test
			Ensure.NotEmpty(source, "source");

			// Assert
			Assert.Fail("Ensure.NotNull should be throw an ArugmentNullException");
		}

		[TestMethod]
		public void EnsureNotEmpty_WithNotEmptyButBlank()
		{
			// Arrange
			var source = new Object[1];

			// Test
			Ensure.NotEmpty(source, "source");

			// Assert nothing
		}

		[TestMethod]
		public void EnsureNotEmpty_WithNotEmptyNotBlank()
		{
			// Arrange
			var source = new[] { new Object() };

			// Test
			Ensure.NotEmpty(source, "source");

			// Assert nothing
		}

		[TestMethod]
		public void EnsureNotEmpty_Null_WithWithMessage()
		{
			// Arrange
			IEnumerable source = null;
			const String message = "Some message";

			// Test
			try
			{
				Ensure.NotEmpty(source, "source", message);

				// Assert
				Assert.Fail("Ensure.NotNull should be throw an ArugmentNullException");
			} catch (ArgumentNullException e)
			{
				// Assert
				Assert.AreEqual(message, GetOriginMessage(e));
			}
		}

		[TestMethod]
		public void EnsureNotEmpty_Emtpy_WithWithMessage()
		{
			// Arrange
			var source = new Object[0];
			const String message = "Some message";

			// Test
			try
			{
				Ensure.NotEmpty(source, "source", message);

				// Assert
				Assert.Fail("Ensure.NotNull should be throw an ArugmentNullException");
			} catch (ArgumentNullException e)
			{
				// Assert
				Assert.AreEqual(message, GetOriginMessage(e));
			}
		}

		[TestMethod]
		public void EnsureNotEmpty_NotEmtpyButBlank_WithWithMessage()
		{
			// Arrange
			var source = new Object[1];
			const String message = "Some message";

			// Test
			Ensure.NotEmpty(source, "source", message);

			// Assert nothing
		}

		[TestMethod]
		public void EnsureNotEmpty_NotEmtpyNotBlank_WithWithMessage()
		{
			// Arrange
			var source = new[] { new Object() };
			const String message = "Some message";

			// Test
			Ensure.NotEmpty(source, "source", message);

			// Assert nothing
		}
	}
}
