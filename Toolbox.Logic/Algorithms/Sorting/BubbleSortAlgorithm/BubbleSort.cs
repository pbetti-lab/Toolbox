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
			if (inputValues.Count == 0)
				return inputValues;

			int upperIndex = inputValues.Count - 1;

			while (upperIndex >= 0) 
			{
				for (int idx = 0; idx <= upperIndex-1; idx++)
				{
					if (ShouldSwapValues(idx))
					{
						(inputValues[idx + 1], inputValues[idx]) = (inputValues[idx], inputValues[idx + 1]);
					}
				}

				upperIndex--;
			}

			return inputValues;
		}

		public List<T> RecursiveSort()
		{
			throw new NotImplementedException();
		}

		private bool ShouldSwapValues(int idx)
		{
			return Comparer<T>.Default.Compare(inputValues[idx], inputValues[idx + 1]) > 0;
		}
	}
}
