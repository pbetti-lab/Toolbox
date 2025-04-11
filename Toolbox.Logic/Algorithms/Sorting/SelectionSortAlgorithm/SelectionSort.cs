using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Logic.Algorithms.Sorting.Interfaces;

namespace Toolbox.Logic.Algorithms.Sorting.SelectionSortAlgorithm
{
	internal class SelectionSort<T> : ISortAlgorithm<T>
	{
		private readonly List<T> inputValues;

		public SelectionSort(List<T> inputValues)
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

			int lowerIndex = 0;
			int upperIndex = inputValues.Count - 1;

			InternalRecursiveSort(lowerIndex, upperIndex);

			return inputValues;
		}

		private bool CanBeConsideredSorted() => 
			inputValues.Count == 0
			|| inputValues.Count == 1;

		private void InternalLinearSort()
		{
			int lowerIndex = 0;
			int upperIndex = inputValues.Count - 1;

			while (lowerIndex < upperIndex)
			{
				int minValueIndex = lowerIndex;

				for (int idx = lowerIndex; idx <= upperIndex; idx++)
				{
					if (FoundNewMinValue(minValueIndex, idx))
						minValueIndex = idx;
				}

				if (ShouldSwapValues(minValueIndex, lowerIndex))
					SwapValues(minValueIndex, lowerIndex);

				lowerIndex++;
			}
		}

		private void InternalRecursiveSort(int lowerIndex, int upperIndex)
		{
			if (lowerIndex > upperIndex)
				return;

			int minValueIndex = lowerIndex;

			for (int idx = lowerIndex; idx <= upperIndex; idx++)
			{
				if (FoundNewMinValue(minValueIndex, idx))
					minValueIndex = idx;
			}

			if (ShouldSwapValues(minValueIndex, lowerIndex))
				SwapValues(minValueIndex, lowerIndex);

			lowerIndex++;

			InternalRecursiveSort(lowerIndex, upperIndex);
		}

		private bool FoundNewMinValue(int minValueIndex, int idx) => 
			Comparer<T>.Default.Compare(inputValues[minValueIndex], inputValues[idx]) > 0;

		private static bool ShouldSwapValues(int minValueIndex, int lowerIndex) => 
			minValueIndex != lowerIndex;

		private void SwapValues(int minValueIndex, int lowerIndex)
		{
			(inputValues[minValueIndex], inputValues[lowerIndex]) =
				(inputValues[lowerIndex], inputValues[minValueIndex]);
		}
	}
}
