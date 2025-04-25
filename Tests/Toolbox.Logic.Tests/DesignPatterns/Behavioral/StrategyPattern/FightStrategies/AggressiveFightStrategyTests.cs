using FluentAssertions;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.FightStrategies;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Models;

namespace Toolbox.Logic.Tests.DesignPatterns.Behavioral.StrategyPattern.FightStrategies
{
	public class AggressiveFightStrategyTests
	{
		[Fact]
		public void ApplyFightStrategy_PassingNullCharacter_ThrowArgumentNullException()
		{
			// Arrange 
			var strategy = new AggressiveFightStrategy();
			Action applyStrategy = () => strategy.ApplyFightStrategy(null);

			// Assert
			applyStrategy.Should().ThrowExactly<ArgumentNullException>();
		}

		[Fact]
		public void ApplyFightStrategy_PassingValidCharacter_ApplyCorrectStrategy()
		{
			// Arrange
			const float ATTACK_FACTOR = 1.8f;
			const float DEFENCE_FACTOR = 0.6f;
			
			var character = new Character("Barbarian", 100, 20, 13);
			var strategy = new AggressiveFightStrategy();

			// Act
			character = strategy.ApplyFightStrategy(character);

			// Assert
			character.Should().NotBeNull();
			character.FightAttack.Should().BeApproximately(character.BaseAttack * ATTACK_FACTOR, 0.001f);
			character.FightDefence.Should().BeApproximately(character.BaseDefence * DEFENCE_FACTOR, 0.001f);
		}

		[Fact]
		public void FightStrategy_CallToString_GetValidMessage()
		{
			// Arrange
			const float ATTACK_FACTOR = 1.8f;
			const float DEFENCE_FACTOR = 0.6f;

			string strategyToStringMessage = $"Aggressive Strategy - Attack factor: {ATTACK_FACTOR} " +
				$"- Defence factor {DEFENCE_FACTOR}";

			var strategy = new AggressiveFightStrategy();

			// Assert
			strategy.ToString().Should().Be(strategyToStringMessage);
		}
	}
}
