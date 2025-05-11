using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Interfaces;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Models;

namespace Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern
{
	public class FightContext
	{
		private IFightStrategy _fightStrategy;

		/// <summary>
		/// Initialize the context of the fight.
		/// <para>throw ArgumentNullException if fight strategy is null.</para>
		/// </summary>
		/// <param name="fightStrategy">The default strategy to use.</param>
		/// <exception cref="ArgumentNullException">If fight strategy is null.</exception>
		public FightContext(IFightStrategy fightStrategy)
		{
			ArgumentNullException.ThrowIfNull(fightStrategy);

			_fightStrategy = fightStrategy;
		}

		/// <summary>
		/// Set the fight strategy to use.
		/// <para>throw ArgumentNullException if fight strategy is null.</para>
		/// </summary>
		/// <param name="fightStrategy">The new strategy to use.</param>
		/// <exception cref="ArgumentNullException">If fight strategy is null.</exception>
		public void SetFightStrategy(IFightStrategy fightStrategy)
		{
			ArgumentNullException.ThrowIfNull(fightStrategy);

			_fightStrategy = fightStrategy;
		}

		/// <summary>
		/// Alter the character attack and defence according to the fight strategy selected.
		/// Return the character with the altered attack and defence scores.
		/// <para>throw ArgumentNullException if character is null.</para>
		/// </summary>
		/// <param name="character">The character which to apply the fight strategy selected.</param>
		/// <exception cref="ArgumentNullException">If fight strategy is null.</exception>
		public Character EnterCombatMode(Character character)
		{
			ArgumentNullException.ThrowIfNull(character);

			_fightStrategy.ApplyFightStrategy(character);

			return character;
		}
	}
}
