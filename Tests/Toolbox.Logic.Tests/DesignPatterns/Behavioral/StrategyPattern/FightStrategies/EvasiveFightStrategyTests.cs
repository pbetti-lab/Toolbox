using FluentAssertions;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.FightStrategies;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Models;

namespace Toolbox.Logic.Tests.DesignPatterns.Behavioral.StrategyPattern.FightStrategies
{
	public class EvasiveFightStrategyTests
	{
		[Fact]
		public void ApplyFightStrategy_PassingNullCharacter_ThrowArgumentNullException()
		{
			// Arrange 
			var strategy = new EvasiveFightStrategy();
			Action applyStrategy = () => strategy.ApplyFightStrategy(null);

			// Assert
			applyStrategy.Should().ThrowExactly<ArgumentNullException>();
		}

		[Fact]
		public void ApplyFightStrategy_PassingValidCharacter_ApplyCorrectStrategy()
		{
			// Arrange
			const float ATTACK_FACTOR = 0.4f;
			const float DEFENCE_FACTOR = 0.8f;
			
			var character = new Character("Barbarian", 100, 20, 13);
			var strategy = new EvasiveFightStrategy();

			// Act
			character = strategy.ApplyFightStrategy(character);

			// Assert
			character.Should().NotBeNull();
			character.FightAttack.Should().BeApproximately(character.BaseAttack * ATTACK_FACTOR, 0.001f);
			character.FightDefence.Should().BeApproximately(character.BaseDefence * DEFENCE_FACTOR, 0.001f);
		}

		public void FightStrategy_CallToString_GetValidMessage()
		{
			// Arrange
			const double ATTACK_FACTOR = 0.4f;
			const double DEFENCE_FACTOR = 0.8f;

			string strategyToStringMessage = $"Evasive Strategy - Attack factor: {ATTACK_FACTOR} " +
				$"- Defence factor {DEFENCE_FACTOR}";

			var strategy = new EvasiveFightStrategy();

			// Assert
			strategy.ToString().Should().Be(strategyToStringMessage);
		}
	}
}
