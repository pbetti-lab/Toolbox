using FluentAssertions;
using Toolbox.Logic.Algorithms.Sorting;
using Toolbox.Logic.Algorithms.Sorting.BubbleSortAlgorithm;
using Toolbox.Logic.Algorithms.Sorting.Enums;

namespace Toolbox.Logic.Tests.Algorithms.Sorting.BubbleSortAlgorithm
{
	public class BubbleSortTests
	{
		[Fact]
		public void NewInstance_WithNullInputValues_ThrowArgumentNullException()
		{
			// Arrange		
			Action createNewInstance = () => new BubbleSort<int>(null);

			// Assert
			createNewInstance.Should().ThrowExactly<ArgumentNullException>();
		}

		[Theory]
		[MemberData(nameof(SortingDataTest))]
		public void Sorting_LinearSort_ReturnSortedList(List<int> inputValues, List<int> sortValues)
		{
			// Arrange
			var bubbleSort = new BubbleSort<int>(inputValues);

			// Act
			var resultList = bubbleSort.HasLinearSort
				? bubbleSort.LinearSort()
				: null;

			// Assert
			resultList.Should().BeEquivalentTo(sortValues, option => option.WithStrictOrdering());
		}

		[Theory]
		[MemberData(nameof(SortingDataTest))]
		public void Sorting_RecursiveSort_ReturnSortedList(List<int> inputValues, List<int> sortValues)
		{
			// Arrange
			var bubbleSort = new BubbleSort<int>(inputValues);

			// Act
			var resultList = bubbleSort.HasRecursiveSort
				? bubbleSort.RecursiveSort()
				: null;

			// Assert
			resultList.Should().BeEquivalentTo(sortValues, option => option.WithStrictOrdering());
		}

		public static IEnumerable<object[]> SortingDataTest() =>
			[
				[new List<int> { }, new List<int> { }],
				[new List<int> { 1 }, new List<int> { 1 }],
				[new List<int> { 2, 1 }, new List<int> { 1, 2 }],
				[new List<int> { 1, 2 }, new List<int> { 1, 2 }],
				[new List<int> { 3, 2, 1 }, new List<int> { 1, 2, 3 }],
				[new List<int> { 2, 3, 1 }, new List<int> { 1, 2, 3 }],
				[new List<int> { 2, 1, 3 }, new List<int> { 1, 2, 3 }],
				[new List<int> { 3, 1, 2 }, new List<int> { 1, 2, 3 }],
				[new List<int> { 1, 3, 2 }, new List<int> { 1, 2, 3 }],
				[new List<int> { 4, 3, 2, 1 }, new List<int> { 1, 2, 3, 4 }],
				[new List<int> { 1, 2, 3, 4 }, new List<int> { 1, 2, 3, 4 }],
				[new List<int> { 1, 1, 1, 1 }, new List<int> { 1, 1, 1, 1 }],
			];
	}
}