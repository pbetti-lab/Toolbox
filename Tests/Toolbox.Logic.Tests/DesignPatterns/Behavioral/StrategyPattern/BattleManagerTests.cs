using FluentAssertions;
using NSubstitute;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.CombatModes;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.FightStrategies;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Interfaces;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Models;

namespace Toolbox.Logic.Tests.DesignPatterns.Behavioral.StrategyPattern
{
	public class BattleManagerTests
	{
		[Theory]
		[MemberData(nameof(BattleDataTest))]
		public void GetInstance_WithNullValues_ThrowArgumentNullException(
			Character player, 
			ICombatMode playerCombatMode,
			Character enemy,
			ICombatMode enemyCombatMode)
		{
			// arrange
			var getInstance = () => new BattleManager(player, playerCombatMode, enemy, enemyCombatMode);

			// act
			getInstance.Should().ThrowExactly<ArgumentNullException>();
		}

		[Fact]
		public void Fight_PlayerStrogerThanEnemy_PlayerWin()
		{
			// arrange
			var player = new Character("Ranger", 15, 12, 8);
			var playerCombatMode = Substitute.For<ICombatMode>();
			playerCombatMode.GetFightStrategy().Returns(new EvasiveFightStrategy());

			var enemy = new Character("Skeleton", 6, 5, 5);
			var enemyCombatMode = Substitute.For<ICombatMode>();
			enemyCombatMode.GetFightStrategy().Returns(new AggressiveFightStrategy());

			var battleManager = new BattleManager(player, playerCombatMode, enemy, enemyCombatMode);

			// act
			battleManager.Fight();

			// assert
			battleManager.GetFightWinner().Should().NotBe(null);
			battleManager.GetFightWinner().CharacterClass.Should().Be("Ranger");

			battleManager.FightHistory.Count().Should().Be(4);
			battleManager.FightHistory[0].PlayerDamageSuffered.Should().BeApproximately(2.6f, 0.001f);
			battleManager.FightHistory[0].CurrentPlayerStatus.Health.Should().BeApproximately(12.4f, 0.001f);
			battleManager.FightHistory[0].EnemyDamageSuffered.Should().BeApproximately(1.8f, 0.001f);
			battleManager.FightHistory[0].CurrentEnemyStatus.Health.Should().BeApproximately(4.2f, 0.001f);

			battleManager.FightHistory[1].PlayerDamageSuffered.Should().BeApproximately(2.6f, 0.001f);
			battleManager.FightHistory[1].CurrentPlayerStatus.Health.Should().BeApproximately(9.8f, 0.001f);
			battleManager.FightHistory[1].EnemyDamageSuffered.Should().BeApproximately(1.8f, 0.001f);
			battleManager.FightHistory[1].CurrentEnemyStatus.Health.Should().BeApproximately(2.4f, 0.001f);

			battleManager.FightHistory[2].PlayerDamageSuffered.Should().BeApproximately(2.6f, 0.001f);
			battleManager.FightHistory[2].CurrentPlayerStatus.Health.Should().BeApproximately(7.2f, 0.001f);
			battleManager.FightHistory[2].EnemyDamageSuffered.Should().BeApproximately(1.8f, 0.001f);
			battleManager.FightHistory[2].CurrentEnemyStatus.Health.Should().BeApproximately(0.6f, 0.001f);

			battleManager.FightHistory[3].PlayerDamageSuffered.Should().BeApproximately(2.6f, 0.001f);
			battleManager.FightHistory[3].CurrentPlayerStatus.Health.Should().BeApproximately(4.6f, 0.001f);
			battleManager.FightHistory[3].EnemyDamageSuffered.Should().BeApproximately(1.8f, 0.001f);
			battleManager.FightHistory[3].CurrentEnemyStatus.Health.Should().BeApproximately(0f, 0.001f);
		}



		public static IEnumerable<object[]> BattleDataTest() =>
			[
				[null, new RandomCombatMode(), new Character("Goblin", 12, 10, 7), new RandomCombatMode()],
				[new Character("Thief", 13, 9, 8), null, new Character("Goblin", 12, 10, 7), new RandomCombatMode()],
				[new Character("Thief", 13, 9, 8), new RandomCombatMode(), null, new RandomCombatMode()],
				[new Character("Thief", 13, 9, 8), new RandomCombatMode(), new Character("Goblin", 12, 10, 7), null],
		];

	}
}
