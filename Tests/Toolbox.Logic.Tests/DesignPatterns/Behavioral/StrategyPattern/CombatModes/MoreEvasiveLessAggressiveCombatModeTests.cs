using FluentAssertions;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.CombatModes;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.FightStrategies;

namespace Toolbox.Logic.Tests.DesignPatterns.Behavioral.StrategyPattern.CombatModes
{
	public class AggressiveFightStrategyTests
	{
		[Fact]
		public void CombatMode_MoreEvasiveLessAggressive_OneOutOfThreeIsAggressive()
		{
			// Act
			var combatMode = new MoreEvasiveLessAggressiveCombatMode();

			// Arrange

			// Assert
			for (int strategCount = 1; strategCount <= 12; strategCount++)
			{
				var strategy = combatMode.GetFightStrategy();

				if (strategCount % 3 == 0)
				{
					strategy.Should().BeOfType(typeof(AggressiveFightStrategy));
				}
				else
				{ 
					strategy.Should().BeOfType(typeof(EvasiveFightStrategy));
				}
			}
		}
	}
}
