using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Interfaces;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Models;

namespace Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.FightStrategies
{
	internal class AggressiveFightStrategy : IFightStrategy
	{
		private const float ATTACK_FACTOR = 1.8f;
		private const float DEFENCE_FACTOR = 0.6f;

		public Character ApplyFightStrategy(Character character)
		{
			ArgumentNullException.ThrowIfNull(character);

			character.FightAttack = character.BaseAttack * ATTACK_FACTOR;
			character.FightDefence = character.BaseDefence * DEFENCE_FACTOR;

			return character;
		}

		public override string ToString()
		{
			return $"Aggressive Strategy - Attack factor: {ATTACK_FACTOR} - Defence factor {DEFENCE_FACTOR}";
		}
	}
}
