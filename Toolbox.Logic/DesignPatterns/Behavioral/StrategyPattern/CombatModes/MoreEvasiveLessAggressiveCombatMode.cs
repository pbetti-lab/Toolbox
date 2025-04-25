using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.FightStrategies;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Interfaces;

namespace Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.CombatModes
{
	internal class MoreEvasiveLessAggressiveCombatMode : ICombatMode
	{
		private int _strategyCounter;

		public MoreEvasiveLessAggressiveCombatMode()
		{
			_strategyCounter = 0;
		}

		public IFightStrategy GetFightStrategy()
		{
			_strategyCounter++;

			return _strategyCounter % 3 == 0
				? new AggressiveFightStrategy()
				: new EvasiveFightStrategy();
		}
	}
}
