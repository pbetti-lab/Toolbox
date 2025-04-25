using FluentAssertions;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.FightStrategies;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Models;

namespace Toolbox.Logic.Tests.DesignPatterns.Behavioral.StrategyPattern.FightStrategies
{
	public class DefensiveFightStrategyTests
	{
		[Fact]
		public void ApplyFightStrategy_PassingNullCharacter_ThrowArgumentNullException()
		{
			// Arrange 
			var strategy = new DefensiveFightStrategy();
			Action applyStrategy = () => strategy.ApplyFightStrategy(null);

			// Assert
			applyStrategy.Should().ThrowExactly<ArgumentNullException>();
		}

		[Fact]
		public void ApplyFightStrategy_PassingValidCharacter_ApplyCorrectStrategy()
		{
			// Arrange
			const float ATTACK_FACTOR = 0.6f;
			const float DEFENCE_FACTOR = 1.8f;
			
			var character = new Character("Wizard", 65, 15, 10);
			var strategy = new DefensiveFightStrategy();

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
			const float ATTACK_FACTOR = 0.6f;
			const float DEFENCE_FACTOR = 1.8f;

			string strategyToStringMessage = $"Defensive Strategy - Attack factor: {ATTACK_FACTOR} " +
				$"- Defence factor {DEFENCE_FACTOR}";

			var strategy = new DefensiveFightStrategy();

			// Assert
			strategy.ToString().Should().Be(strategyToStringMessage);
		}
	}
}
