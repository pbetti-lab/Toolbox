namespace Toolbox.Logic.Algorithms.Sorting.Interfaces
{	
	public interface ISortAlgorithm<T>
	{
		public bool HasLinearSort { get; }

		public bool HasRecursiveSort { get; }

		public List<T> LinearSort();

		public List<T> RecursiveSort();
	}
}
