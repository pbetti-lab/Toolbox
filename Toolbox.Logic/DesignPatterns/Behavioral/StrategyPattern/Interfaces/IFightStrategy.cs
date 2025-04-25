using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Models;

namespace Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Interfaces
{
	public interface IFightStrategy
	{
		public Character ApplyFightStrategy(Character character);
	}
}
