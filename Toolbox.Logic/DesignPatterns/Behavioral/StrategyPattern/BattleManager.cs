using System.Collections.Generic;
using System.Text;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Interfaces;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Models;

namespace Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern
{
	public class BattleManager
	{
		private readonly Character _player;
		private readonly ICombatMode _playerCombatMode;
		private readonly Character _enemy;
		private readonly ICombatMode _enemyCombatMode;
		private readonly List<FightRound> _fightHistory;

		public BattleManager(
			Character player, 
			ICombatMode playerCombatMode,
			Character enemy,
			ICombatMode enemyCombatMode)
		{
			ArgumentNullException.ThrowIfNull(player);
			ArgumentNullException.ThrowIfNull(playerCombatMode);
			ArgumentNullException.ThrowIfNull(enemy);
			ArgumentNullException.ThrowIfNull(enemyCombatMode);

			_player = player;
			_playerCombatMode = playerCombatMode;
			_enemy = enemy;
			_enemyCombatMode = enemyCombatMode;
			_fightHistory = new List<FightRound>();
		}

		public IReadOnlyList<FightRound> FightHistory 
		{ 
			get { return _fightHistory.AsReadOnly<FightRound>(); } 
		}

		/// <summary>
		/// Return the winnner character. If both are death or alive return null 
		/// </summary>
		public Character? GetFightWinner()
		{
			if (AreBothCharacterAlive())
				return null;

			return _player.Health > 0 
				? _player 
				: _enemy;
		}

		public void Fight()
		{
			while (AreBothCharacterAlive())
			{
				var playerFightStrategyAdopted = ApplyFightStrategy(_player, _playerCombatMode);
				var enemyFightStrategyAdopted = ApplyFightStrategy(_enemy, _enemyCombatMode);

				float playerDamageSuffered = GetDamage(_enemy.FightAttack, _player.FightDefence);
				float enemyDamageSuffered = GetDamage(_player.FightAttack, _enemy.FightDefence);

				_player.ReceiveDamage(playerDamageSuffered);
				_enemy.ReceiveDamage(enemyDamageSuffered);

				_fightHistory.Add(
					new FightRound(playerFightStrategyAdopted,
						enemyFightStrategyAdopted,
						playerDamageSuffered,
						enemyDamageSuffered,
						(Character)_player.Clone(),
						(Character)_enemy.Clone()
					)
				);
			}
		}
		
		private bool AreBothCharacterAlive()
		{
			return _player.Health > 0
				&& _enemy.Health > 0;
		}

		private static float GetDamage(float fightAttack, float fightDefence)
		{
			float damage = fightAttack - fightDefence;

			return damage > 0 
				? damage 
				: 0;
		}

		private string ApplyFightStrategy(Character character, ICombatMode combatMode)
		{
			var fightStrategy = combatMode.GetFightStrategy() 
				?? throw new InvalidOperationException("The fight strategy cannot be null");
						
			fightStrategy.ApplyFightStrategy(character);

			return fightStrategy.ToString() ?? "It seems that the strategy chosen is unknown";
		}
	}
}
