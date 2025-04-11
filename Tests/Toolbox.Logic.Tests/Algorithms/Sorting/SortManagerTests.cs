using FluentAssertions;
using Toolbox.Logic.Algorithms.Sorting;
using Toolbox.Logic.Algorithms.Sorting.BubbleSortAlgorithm;
using Toolbox.Logic.Algorithms.Sorting.Enums;
using Toolbox.Logic.Algorithms.Sorting.SelectionSortAlgorithm;

namespace Toolbox.Logic.Tests.Algorithms.Sorting
{
	public class SortManagerTests
	{
		[Fact]
		public void GetInstance_WithInvalidSortType_ThrowArgumentNullException()
		{
			// Arrange
			var inputValues = new List<int>();
			var sortManager = new SortManager<int>();
			Action getInstance = () => sortManager.GetInstance((SortType)9999, inputValues);

			// Assert
			getInstance.Should().ThrowExactly<ArgumentException>();
		}

		[Fact]
		public void GetInstance_WithNullInputValues_ThrowArgumentNullException()
		{
			// Arrange
			var sortManager = new SortManager<int>();
			Action getInstance = () => sortManager.GetInstance(SortType.BubbleSort, null);

			// Assert
			getInstance.Should().ThrowExactly<ArgumentNullException>();
		}

		[Fact]
		public void GetInstance_AskForBubbleSort_GetBubbleSortInstance()
		{
			// Arrange
			var inputValues = new List<int>();
			var bubbleSort = new SortManager<int>()
				.GetInstance(SortType.BubbleSort, inputValues);

			// Assert
			bubbleSort.Should().BeOfType(typeof(BubbleSort<int>));
		}

		[Fact]
		public void GetInstance_AskForSelectionSort_GetSelectionSortInstance()
		{
			// Arrange
			var inputValues = new List<int>();
			var selectionSort = new SortManager<int>()
				.GetInstance(SortType.SelectionSort, inputValues);

			// Assert
			selectionSort.Should().BeOfType(typeof(SelectionSort<int>));
		}
	}
}
