using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Interfaces;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Models;

namespace Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.FightStrategies
{
	internal class EvasiveFightStrategy : IFightStrategy
	{
		private const float ATTACK_FACTOR = 0.4f;
		private const float DEFENCE_FACTOR = 0.8f;

		public Character ApplyFightStrategy(Character character)
		{
			ArgumentNullException.ThrowIfNull(character);

			character.FightAttack = character.BaseAttack * ATTACK_FACTOR;
			character.FightDefence = character.BaseDefence * DEFENCE_FACTOR;

			return character;
		}

		public override string ToString()
		{
			return $"Evasive Strategy - Attack factor: {ATTACK_FACTOR} - Defence factor {DEFENCE_FACTOR}";
		}
	}
}
