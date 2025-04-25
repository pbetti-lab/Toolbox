using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.FightStrategies;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Interfaces;

namespace Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.CombatModes
{
	internal class RandomCombatMode : ICombatMode
	{
		public IFightStrategy GetFightStrategy()
		{
			var dice = Random.Shared.Next(1, 4);

			return dice switch
			{
				1 => new AggressiveFightStrategy(),
				2 => new DefensiveFightStrategy(),
				3 => new EvasiveFightStrategy(),
				_ => throw new IndexOutOfRangeException("Not valid FightStrategy selected"),
			};
		}
	}
}
