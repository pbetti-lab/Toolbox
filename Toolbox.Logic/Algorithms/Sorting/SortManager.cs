using Toolbox.Logic.Algorithms.Sorting.Interfaces;
using Toolbox.Logic.Algorithms.Sorting.BubbleSortAlgorithm;
using Toolbox.Logic.Algorithms.Sorting.Enums;

namespace Toolbox.Logic.Algorithms.Sorting
{
	public class SortManager<T>
	{
		public ISortAlgorithm<T> GetInstance(SortType sortingType, List<T> inputValues)
		{
			if (!Enum.IsDefined(typeof(SortType), sortingType))
			{ 
				throw new ArgumentException("Unknown sorting type value", nameof(sortingType));
			}

			ArgumentNullException.ThrowIfNull(inputValues);
			
			return sortingType switch
			{
				SortType.BubbleSort => new BubbleSort<T>(inputValues),
				_ => throw new NotImplementedException(),
			};
		}
	}
}
