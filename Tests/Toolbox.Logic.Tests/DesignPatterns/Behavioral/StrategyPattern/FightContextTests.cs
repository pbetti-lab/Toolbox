using FluentAssertions;
using NSubstitute;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.FightStrategies;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Models;

namespace Toolbox.Logic.Tests.DesignPatterns.Behavioral.StrategyPattern
{
	public class FightContextTests
	{
		[Fact]
		public void GetInstance_WithNullValue_ThrowArgumentNullException()
		{
			// arrange
			var getInstance = () => new FightContext(null);

			// assert
			getInstance.Should().ThrowExactly<ArgumentNullException>();
		}

		[Fact]
		public void SetFightStrategy_WithNullValue_ThrowArgumentNullException()
		{
			// arrange
			var fightContext = new FightContext(new AggressiveFightStrategy());
			var callSetStrategy = () => fightContext.SetFightStrategy(null);

			// assert
			callSetStrategy.Should().ThrowExactly<ArgumentNullException>();
		}

		[Fact]
		public void EnterCombatMode_WithNullValue_ThrowArgumentNullException()
		{
			// arrange
			var fightContext = new FightContext(new AggressiveFightStrategy());
			var callEnterCombatMode = () => fightContext.SetFightStrategy(null);

			// assert
			callEnterCombatMode.Should().ThrowExactly<ArgumentNullException>();
		}

		[Fact]
		public void EnterCombatMode_WithStrategyPassedDuringInstanceCreation_AlterCharacterAttackAndDefenceCorrectly()
		{
			// arrange
			var fightContext = new FightContext(new AggressiveFightStrategy());
			var player = new Character("Ranger", 15, 12, 8);

			// act
			var playerInCombatMode = fightContext.EnterCombatMode(player);

			// assert
			playerInCombatMode.CharacterClass.Should().Be("Ranger");
			playerInCombatMode.Health.Should().BeApproximately(15f, 0.001f); 
			playerInCombatMode.BaseAttack.Should().BeApproximately(12f, 0.001f);
			playerInCombatMode.BaseDefence.Should().BeApproximately(8f, 0.001f);
			playerInCombatMode.FightAttack.Should().BeApproximately(21.6f, 0.001f);
			playerInCombatMode.FightDefence.Should().BeApproximately(4.8f, 0.001f);
		}

		[Fact]
		public void EnterCombatMode_WithStrategyPassedAfterInstanceCreation_AlterCharacterAttackAndDefenceCorrectly()
		{
			// arrange
			var fightContext = new FightContext(new AggressiveFightStrategy());
			var player = new Character("Skeleton", 30, 5, 5);

			// act
			fightContext.SetFightStrategy(new DefensiveFightStrategy());
			var playerInCombatMode = fightContext.EnterCombatMode(player);

			// assert
			playerInCombatMode.CharacterClass.Should().Be("Skeleton");
			playerInCombatMode.Health.Should().BeApproximately(30f, 0.001f);
			playerInCombatMode.BaseAttack.Should().BeApproximately(5f, 0.001f);
			playerInCombatMode.BaseDefence.Should().BeApproximately(5f, 0.001f);
			playerInCombatMode.FightAttack.Should().BeApproximately(3f, 0.001f);
			playerInCombatMode.FightDefence.Should().BeApproximately(9f, 0.001f);
		}
	}
}
