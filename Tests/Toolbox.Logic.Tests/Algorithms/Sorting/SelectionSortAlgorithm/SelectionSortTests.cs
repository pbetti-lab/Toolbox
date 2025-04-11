using FluentAssertions;
using Toolbox.Logic.Algorithms.Sorting.Enums;
using Toolbox.Logic.Algorithms.Sorting;
using Toolbox.Logic.Algorithms.Sorting.SelectionSortAlgorithm;

namespace Toolbox.Logic.Tests.Algorithms.Sorting.SelectionSortAlgorithm
{
	public class SelectionSortTests
	{
		[Fact]
		public void NewInstance_WithNullInputValues_ThrowArgumentNullException()
		{
			// Arrange
			Action createNewInstance = () => new SelectionSort<int>(null);

			// Assert
			createNewInstance.Should().ThrowExactly<ArgumentNullException>();
		}

		[Theory]
		[MemberData(nameof(SortingDataTest))]
		public void Sorting_LinearSort_ReturnSortedList(List<int> inputValues, List<int> sortValues)
		{
			// Arrange
			var selectionSort = new SelectionSort<int>(inputValues);

			// Act
			var resultList = selectionSort.HasLinearSort
				? selectionSort.LinearSort()
				: null;

			// Assert
			resultList.Should().BeEquivalentTo(sortValues, option => option.WithStrictOrdering());
		}

		[Theory]
		[MemberData(nameof(SortingDataTest))]
		public void Sorting_RecursiveSort_ReturnSortedList(List<int> inputValues, List<int> sortValues)
		{
			// Arrange
			var selectionSort = new SelectionSort<int>(inputValues);

			// Act
			var resultList = selectionSort.HasRecursiveSort
				? selectionSort.RecursiveSort()
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
