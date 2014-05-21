using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Commons;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NCommons
{
	/// <summary>
	/// EnumerableExtensionsTests 的摘要说明
	/// </summary>
	[TestClass]
	public class EnumerableExtensionsTests
	{

		#region IsEmpty

		[TestMethod]
		public void NonGenericSequenceIsEmpty_WithNull()
		{
			// Arrange

			// Test
			var isEmpty = Helpers.NonGenericNullEnumerable.IsEmpty();

			// Assert
			Assert.IsTrue(isEmpty);
		}

		[TestMethod]
		public void NonGenericSequenceIsEmpty_WithEmpty()
		{
			// Arrange

			// Test
			var isEmpty = Helpers.NonGenericEmptyEnumerable.IsEmpty();

			// Assert
			Assert.IsTrue(isEmpty);
		}

		[TestMethod]
		public void NonGenericSequenceIsEmpty_WithBlank()
		{
			// Arrange

			// Test
			var isEmpty = Helpers.NonGenericBlankEnumerable().IsEmpty();

			// Assert
			Assert.IsFalse(isEmpty);
		}

		[TestMethod]
		public void NonGenericSequenceIsEmpty_WithItems()
		{
			// Arrange
			IEnumerable source = new[] { new { } };

			// Test
			var isEmpty = source.IsEmpty();

			// Assert
			Assert.IsFalse(isEmpty);
		}

		[TestMethod]
		public void GenericSequenceIsEmpty_WithNull()
		{
			// Arrange

			// Test
			var isEmpty = Helpers.GenericNullEnumerable<Object>().IsEmpty();

			// Assert
			Assert.IsTrue(isEmpty);
		}

		[TestMethod]
		public void GenericSequenceIsEmpty_WithEmpty()
		{
			// Arrange

			// Test
			var isEmpty = Helpers.GenericEmptyEnumerable<Object>().IsEmpty();

			// Assert
			Assert.IsTrue(isEmpty);
		}

		[TestMethod]
		public void GenericSequenceIsEmpty_WithBlank()
		{
			// Arrange

			// Test
			var isEmpty = Helpers.GenericBlankEnumerable<Object>().IsEmpty();

			// Assert
			Assert.IsFalse(isEmpty);
		}

		[TestMethod]
		public void GenericSequenceIsEmpty_WithItems()
		{
			// Arrange
			var source = new[] { new { } }.AsEnumerable();

			// Test
			var isEmpty = source.IsEmpty();

			// Assert
			Assert.IsFalse(isEmpty);
		}

		#endregion

		#region IsNotEmpty

		[TestMethod]
		public void NonGenericSequenceIsNotEmpty_WithNull()
		{
			// Arrange

			// Test
			var isNotEmpty = Helpers.NonGenericNullEnumerable.IsNotEmpty();

			// Assert
			Assert.IsFalse(isNotEmpty);
		}

		[TestMethod]
		public void NonGenericSequenceIsNotEmpty_WithEmpty()
		{
			// Arrange

			// Test
			var isNotEmpty = Helpers.NonGenericEmptyEnumerable.IsNotEmpty();

			// Assert
			Assert.IsFalse(isNotEmpty);
		}

		[TestMethod]
		public void NonGenericSequenceIsNotEmpty_WithBlank()
		{
			// Arrange

			// Test
			var isNotEmpty = Helpers.NonGenericBlankEnumerable().IsNotEmpty();

			// Assert
			Assert.IsTrue(isNotEmpty);
		}

		[TestMethod]
		public void NonGenericSequenceIsNotEmpty_WithItems()
		{
			// Arrange
			IEnumerable source = new[] { new { } };

			// Test
			var isNotEmpty = source.IsNotEmpty();

			// Assert
			Assert.IsTrue(isNotEmpty);
		}

		[TestMethod]
		public void GenericSequenceIsNotEmpty_WithNull()
		{
			// Arrange

			// Test
			var isNotEmpty = Helpers.GenericNullEnumerable<Object>().IsNotEmpty();

			// Assert
			Assert.IsFalse(isNotEmpty);
		}

		[TestMethod]
		public void GenericSequenceIsNotEmpty_WithEmpty()
		{
			// Arrange

			// Test
			var isNotEmpty = Helpers.GenericEmptyEnumerable<Object>().IsNotEmpty();

			// Assert
			Assert.IsFalse(isNotEmpty);
		}

		[TestMethod]
		public void GenericSequenceIsNotEmpty_WithBlank()
		{
			// Arrange

			// Test
			var isNotEmpty = Helpers.GenericBlankEnumerable<Object>().IsNotEmpty();

			// Assert
			Assert.IsTrue(isNotEmpty);
		}

		[TestMethod]
		public void GenericSequenceIsNotEmpty_WithItems()
		{
			// Arrange
			var source = new[] { new { } }.AsEnumerable();

			// Test
			var isNotEmpty = source.IsNotEmpty();

			// Assert
			Assert.IsTrue(isNotEmpty);
		}

		#endregion

		#region IsIn

		[TestMethod]
		public void IsInSequence()
		{
			// Arrange
			const String source = "foo";

			// Test
			var isIn = source.IsIn("foo", "bar");

			// Assert
			Assert.IsTrue(isIn);
		}

		[TestMethod]
		public void IsInSequence_WithNullSource()
		{
			// Arrange
			const String source = null;

			// Test
			var isIn = source.IsIn("foo", "bar");

			// Assert
			Assert.IsFalse(isIn);
		}

		[TestMethod]
		public void IsInSequence_WithNullSources()
		{
			// Arrange
			const String source = "foo";

			// Test
			var isIn = source.IsIn(null);

			// Assert
			Assert.IsFalse(isIn);
		}

		[TestMethod]
		public void IsInSequence_WithDifferentObjectReference_ButEqualsIsTrue()
		{
			// Arrange
			const String source = "foo";
			var sourceCopy = new String(source.ToCharArray());

			// Test
			var isIn = source.IsIn(sourceCopy, "bar");

			// Assert
			Assert.AreNotSame(source, sourceCopy);
			Assert.IsTrue(isIn);
		}

		#endregion

		#region IsNotIn

		[TestMethod]
		public void IsNotInSequence()
		{
			// Arrange
			const String source = "foo";

			// Test
			var isNotIn = source.IsNotIn("foo", "bar");

			// Assert
			Assert.IsFalse(isNotIn);
		}

		[TestMethod]
		public void IsNotInSequence_WithNullSource()
		{
			// Arrange
			const String source = null;

			// Test
			var isNotIn = source.IsNotIn("foo", "bar");

			// Assert
			Assert.IsTrue(isNotIn);
		}

		[TestMethod]
		public void IsNotInSequence_WithNullSources()
		{
			// Arrange
			const String source = "foo";

			// Test
			var isNotIn = source.IsNotIn(null);

			// Assert
			Assert.IsTrue(isNotIn);
		}

		[TestMethod]
		public void IsNotInSequence_WithDifferentObjectReference_ButEqualsIsTrue()
		{
			// Arrange
			const String source = "foo";
			var sourceCopy = new String(source.ToCharArray());

			// Test
			var isNotIn = source.IsNotIn(sourceCopy, "bar");

			// Assert
			Assert.AreNotSame(source, sourceCopy);
			Assert.IsFalse(isNotIn);
		}

		#endregion

		#region ForEach

		[TestMethod]
		public void SequenceForEach()
		{
			// Arrange
			var source = new[] { "foo", "bar" };

			// Test
			var c = 0;
			source.ForEach(_ => c++);

			// Assert
			Assert.AreEqual(source.Length, c);
		}

		[TestMethod]
		public void SequenceForEach_WithNullSource()
		{
			// Arrange

			// Test
			var c = 0;
			((IEnumerable<Object>)null).ForEach(_ => c++);

			// Assert
			Assert.AreEqual(0, c);
		}

		#endregion

		#region Distinct

		[TestMethod]
		public void DistinctSequence()
		{
			// Arrange
			var source = new[] { 1, 4, 2, 5, 3, 4, 2 };

			// Test
			var result = source.Distinct(i => i % 3L);

			// Assert
			Assert.AreEqual(3, result.Count());
			Assert.IsInstanceOfType(result, typeof(IEnumerable<Int32>));
		}

		[TestMethod]
		public void DistinctSequence_WithNullSource()
		{
			// Arrange

			// Test
			var result = ((IEnumerable<Object>)null).Distinct(o => o.GetHashCode());

			// Assert
			Assert.IsNull(result);
		}

		#endregion

		#region Except (A - B)

		[TestMethod]
		public void ExceptSequence()
		{
			// Arrange
			var first = new[] { 1, 2, 4, 8 };
			var second = new[] { 4, 5, 6, 7, 8 };

			// Test
			var result = first.Except(second, i => i * 1L);

			// Assert
			Assert.IsTrue(new HashSet<Int32>(result).SetEquals(new[] { 1, 2 }));
		}

		[TestMethod]
		public void ExceptSequence_WithNullFirst()
		{
			// Arrange
			var second = new[] { 4, 5, 6, 7, 8 };

			// Test
			var result = ((IEnumerable<Int32>)null).Except(second, i => i * 1L);

			// Assert
			Assert.IsNull(result);
		}

		[TestMethod]
		public void ExceptSequence_WithNullSecond()
		{
			// Arrange
			var first = new[] { 1, 2, 4, 8 };

			// Test
			var result = first.Except(null, i => i * 1L);

			// Assert
			Assert.IsTrue(new HashSet<Int32>(result).SetEquals(first));
		}

		[TestMethod]
		public void ExceptSequence_WithNullBoth()
		{
			// Arrange

			// Test
			var result = ((IEnumerable<Int32>)null).Except(null, i => i * 1L);

			// Assert
			Assert.IsNull(result);
		}

		#endregion

		#region Intersect (A ∩ B)

		[TestMethod]
		public void IntersectSequence()
		{
			// Arrange
			var first = new[] { 1, 2, 4, 8 };
			var second = new[] { 4, 5, 6, 7, 8 };

			// Test
			var result = first.Intersect(second, i => i * 1L);

			// Assert
			Assert.IsTrue(new HashSet<Int32>(result).SetEquals(new[] { 4, 8 }));
		}

		[TestMethod]
		public void IntersectSequence_WithNullFirst()
		{
			// Arrange
			var second = new[] { 4, 5, 6, 7, 8 };

			// Test
			var result = ((IEnumerable<Int32>)null).Intersect(second, i => i * 1L);

			// Assert
			Assert.IsNull(result);
		}

		[TestMethod]
		public void IntersectSequence_WithNullSecond()
		{
			// Arrange
			var first = new[] { 1, 2, 4, 8 };

			// Test
			var result = first.Intersect(null, i => i * 1L);

			// Assert
			Assert.IsNull(result);
		}

		[TestMethod]
		public void IntersectSequence_WithNullBoth()
		{
			// Arrange

			// Test
			var result = ((IEnumerable<Int32>)null).Intersect(null, i => i * 1L);

			// Assert
			Assert.IsNull(result);
		}

		#endregion

		#region Union (A ∪ B)

		[TestMethod]
		public void UnionSequence()
		{
			// Arrange
			var first = new[] { 1, 2, 4, 8 };
			var second = new[] { 4, 5, 6, 7, 8 };

			// Test
			var result = first.Union(second, i => i * 1L);

			// Assert
			Assert.IsTrue(new HashSet<Int32>(result).SetEquals(new[] { 1, 2, 4, 5, 6, 7, 8 }));
		}

		[TestMethod]
		public void UnionSequence_WithNullFirst()
		{
			// Arrange
			var second = new[] { 4, 5, 6, 7, 8 };

			// Test
			var result = ((IEnumerable<Int32>)null).Union(second, i => i * 1L);

			// Assert
			Assert.IsTrue(new HashSet<Int32>(result).SetEquals(second));
		}

		[TestMethod]
		public void UnionSequence_WithNullSecond()
		{
			// Arrange
			var first = new[] { 1, 2, 4, 8 };

			// Test
			var result = first.Union(null, i => i * 1L);

			// Assert
			Assert.IsTrue(new HashSet<Int32>(result).SetEquals(first));
		}

		[TestMethod]
		public void UnionSequence_WithNullBoth()
		{
			// Arrange

			// Test
			var result = ((IEnumerable<Int32>)null).Union(null, i => i * 1L);

			// Assert
			Assert.IsNull(result);
		}

		#endregion

	}
}
