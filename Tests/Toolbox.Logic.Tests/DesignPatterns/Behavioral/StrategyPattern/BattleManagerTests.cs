using FluentAssertions;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.FightStrategies;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Models;

namespace Toolbox.Logic.Tests.DesignPatterns.Behavioral.StrategyPattern
{
	public class BattleManagerTests
	{
		[Theory]
		[MemberData(nameof(BattleTestData))]
		public void GetInstance_WithNullValues_ThrowArgumentNullException(
			Character player, 
			Character enemy,
			FightContext fightContext)
		{
			// arrange
			var getInstance = () => new BattleManager(player, enemy, fightContext);

			// assert
			getInstance.Should().ThrowExactly<ArgumentNullException>();
		}


		[Theory]
		[MemberData(nameof(AreBothCharacterAliveTestData))]
		public void AreBothCharacterAlive_AllCombinations_ReturnTrueOnlyIfBothAreAlive(
			Character player,
			Character enemy,
			FightContext fightContext,
			bool expectedResult)
		{
			// arrange
			var battleManager = new BattleManager(player, enemy, fightContext);

			// assert
			battleManager.AreBothCharactersAlive().Should().Be(expectedResult);
		}

		[Theory]
		[MemberData(nameof(AreBothCharacterDeadTestData))]
		public void AreBothCharacterDead_AllCombinations_ReturnTrueOnlyIfBothAreDead(
			Character player,
			Character enemy,
			FightContext fightContext,
			bool expectedResult)
		{
			// arrange
			var battleManager = new BattleManager(player, enemy, fightContext);

			// assert
			battleManager.AreBothCharactersDead().Should().Be(expectedResult);
		}

		[Theory]
		[MemberData(nameof(GetWinnerTestData))]
		public void GetWinner_AllCombinations_ReturnTheWinnerCharacterOrNullIfIsntThereAWinner(
			Character player,
			Character enemy,
			FightContext fightContext,
			Character expectedResult)
		{
			// arrange
			var battleManager = new BattleManager(player, enemy, fightContext);

			// assert
			battleManager.GetWinner().Should().Be(expectedResult);
		}


		[Fact]
		public void Fight_PlayerStrogerThanEnemy_PlayerWin()
		{
			// arrange
			var player = new Character("Ranger", 15, 12, 8);
			var enemy = new Character("Skeleton", 30, 5, 5);

			var aggressiveFightStrategy = new AggressiveFightStrategy();
			var defensiveFightStrategy = new DefensiveFightStrategy();

			var fightContext = new FightContext(aggressiveFightStrategy);

			var battleManager = new BattleManager(player, enemy, fightContext);

			// act
			battleManager.FightSingleRound(aggressiveFightStrategy, aggressiveFightStrategy);
			battleManager.FightSingleRound(aggressiveFightStrategy, defensiveFightStrategy);

			// assert
			battleManager.GetWinner().Should().NotBe(null);
			battleManager.GetWinner().CharacterClass.Should().Be("Ranger");

			battleManager.FightHistory.Count().Should().Be(2);

			battleManager.FightHistory[0].DamageDealtToPlayer.Should().BeApproximately(4.2f, 0.001f);
			battleManager.FightHistory[0].CurrentPlayerStatus.Health.Should().BeApproximately(10.8f, 0.001f);
			battleManager.FightHistory[0].DamageDealtToEnemy.Should().BeApproximately(18.6f, 0.001f);
			battleManager.FightHistory[0].CurrentEnemyStatus.Health.Should().BeApproximately(11.4f, 0.001f);

			battleManager.FightHistory[1].DamageDealtToPlayer.Should().BeApproximately(0f, 0.001f);
			battleManager.FightHistory[1].CurrentPlayerStatus.Health.Should().BeApproximately(10.8f, 0.001f);
			battleManager.FightHistory[1].DamageDealtToEnemy.Should().BeApproximately(12.6f, 0.001f);
			battleManager.FightHistory[1].CurrentEnemyStatus.Health.Should().BeApproximately(0f, 0.001f);
		}


		public static IEnumerable<object[]> BattleTestData() =>
			[
				[null, new Character("Goblin", 12, 10, 7), new FightContext(new AggressiveFightStrategy())],
				[new Character("Thief", 13, 9, 8), null, new FightContext(new DefensiveFightStrategy())],
				[new Character("Thief", 13, 9, 8), new Character("Goblin", 12, 10, 7), null],
		];

		public static IEnumerable<object[]> AreBothCharacterAliveTestData() =>
			[
				[new Character("Thief", 13, 9, 8), new Character("Goblin", 12, 10, 7), new FightContext(new AggressiveFightStrategy()), true],
				[new Character("Thief", 0, 9, 8), new Character("Goblin", 12, 10, 7), new FightContext(new AggressiveFightStrategy()), false],
				[new Character("Thief", 13, 9, 8), new Character("Goblin", 0, 10, 7), new FightContext(new AggressiveFightStrategy()), false],
				[new Character("Thief", 0, 9, 8), new Character("Goblin", 0, 10, 7), new FightContext(new AggressiveFightStrategy()), false],
		];

		public static IEnumerable<object[]> AreBothCharacterDeadTestData() =>
			[
				[new Character("Thief", 13, 9, 8), new Character("Goblin", 12, 10, 7), new FightContext(new AggressiveFightStrategy()), false],
				[new Character("Thief", 0, 9, 8), new Character("Goblin", 12, 10, 7), new FightContext(new AggressiveFightStrategy()), false],
				[new Character("Thief", 13, 9, 8), new Character("Goblin", 0, 10, 7), new FightContext(new AggressiveFightStrategy()), false],
				[new Character("Thief", 0, 9, 8), new Character("Goblin", 0, 10, 7), new FightContext(new AggressiveFightStrategy()), true],
		];

		public static IEnumerable<object[]> GetWinnerTestData()
		{
			var thiefWinner = new Character("Thief", 13, 9, 8);
			var goblinWinner = new Character("Goblin", 12, 10, 7);

			return [
				[new Character("Thief", 13, 9, 8), new Character("Goblin", 12, 10, 7), new FightContext(new AggressiveFightStrategy()), null],
				[new Character("Thief", 0, 9, 8), new Character("Goblin", 0, 10, 7), new FightContext(new AggressiveFightStrategy()), null],
				[thiefWinner, new Character("Goblin", 0, 10, 7), new FightContext(new AggressiveFightStrategy()), thiefWinner],
				[new Character("Thief", 0, 9, 8), goblinWinner, new FightContext(new AggressiveFightStrategy()), goblinWinner],
			];

		}
}
}
