using Toolbox.Logic.Algorithms.Sorting.Interfaces;

namespace Toolbox.Logic.Algorithms.Sorting.BubbleSortAlgorithm
{
	internal class BubbleSort<T> : ISortAlgorithm<T>
	{
		private readonly List<T> inputValues;

		public BubbleSort(List<T> inputValues)
		{
			ArgumentNullException.ThrowIfNull(inputValues);

			this.inputValues = inputValues;
		}

		public bool HasLinearSort => true;

		public bool HasRecursiveSort => true;

		public List<T> LinearSort()
		{
			if (CanBeConsideredSorted())
				return inputValues;

			InternalLinearSort();

			return inputValues;
		}

		public List<T> RecursiveSort()
		{
			if (CanBeConsideredSorted())
				return inputValues;

			int upperIndex = inputValues.Count - 1;
			InternalRecursiveSort(upperIndex);

			return inputValues;
		}

		private bool CanBeConsideredSorted() =>
			inputValues.Count == 0
			|| inputValues.Count == 1;

		private void InternalLinearSort()
		{
			int upperIndex = inputValues.Count - 1;

			while (upperIndex >= 0)
			{
				for (int idx = 0; idx <= upperIndex - 1; idx++)
				{
					if (ShouldSwapValues(idx))
						SwapValues(idx);
				}

				upperIndex--;
			}
		}

		private void InternalRecursiveSort(int upperIndex)
		{
			if (upperIndex < 0)
				return;

			for (int idx = 0; idx <= upperIndex - 1; idx++)
			{
				if (ShouldSwapValues(idx))
					SwapValues(idx);
			}

			InternalRecursiveSort(upperIndex - 1);
		}

		private void SwapValues(int idx) => 
			(inputValues[idx + 1], inputValues[idx]) = (inputValues[idx], inputValues[idx + 1]);

		private bool ShouldSwapValues(int idx) =>
			Comparer<T>.Default.Compare(inputValues[idx], inputValues[idx + 1]) > 0;
	}
}
