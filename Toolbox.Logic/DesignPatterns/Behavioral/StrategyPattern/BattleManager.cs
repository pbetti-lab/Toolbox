using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Interfaces;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Models;

namespace Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern
{
	public class BattleManager
	{
		private Character _player;
		private Character _enemy;
		private readonly FightContext _combatContext;
		private readonly List<FightRound> _fightHistory;

		/// <summary>
		/// Inizialize the battle manager with the componets needed to perform the fight.
		/// <para>throw ArgumentNullException if player, enemy or the fightContext are null.</para>
		/// </summary>
		/// <param name="player">The character used by the the player.</param>
		/// <param name="enemy">The character used by the the enemy.</param>
		/// <param name="fightContext">The contex used by strategy pattern to apply the strategy.</param>
		public BattleManager(Character player, Character enemy, FightContext fightContext)
		{
			ArgumentNullException.ThrowIfNull(player);
			ArgumentNullException.ThrowIfNull(enemy);
			ArgumentNullException.ThrowIfNull(fightContext);

			_player = player;
			_enemy = enemy;
			_combatContext = fightContext;
			_fightHistory = new List<FightRound>();
		}

		/// <summary>
		/// Contains the battle history in read-only mode.
		/// </summary>
		public IReadOnlyList<FightRound> FightHistory 
		{ 
			get { return _fightHistory.AsReadOnly<FightRound>(); } 
		}

		/// <summary>
		/// Return a value that indicates if both characters are alive.
		/// </summary>
		public bool AreBothCharactersAlive()
		{
			return _player.Health > 0
				&& _enemy.Health > 0;
		}

		/// <summary>
		/// Return a value that indicates if both characters are dead.
		/// </summary>
		public bool AreBothCharactersDead()
		{
			return _player.Health == 0
				&& _enemy.Health == 0;
		}

		/// <summary>
		/// Return the winnner character. If both characters are death or alive return null.
		/// </summary>
		public Character? GetWinner()
		{
			bool isntThereAWinner = AreBothCharactersAlive() ||
				AreBothCharactersDead(); 

			if (isntThereAWinner)
				return null;

			return _player.Health > 0 
				? _player 
				: _enemy;
		}

		/// <summary>
		/// Perform a round of the battle and save the result in the fight history collection.
		/// <para>throw ArgumentNullException if playerFightStrategy or the enemyFightStrategy are null.</para>
		/// </summary>
		/// <param name="playerFightStrategy">The character used by the the player.</param>
		/// <param name="enemyFightStrategy">The character used by the the enemy.</param>
		public void FightSingleRound(IFightStrategy playerFightStrategy, IFightStrategy enemyFightStrategy)
		{
			ArgumentNullException.ThrowIfNull(playerFightStrategy);
			ArgumentNullException.ThrowIfNull(enemyFightStrategy);

			const string UNKNOWN_STRATEGY_NAME_MSG = "Unknown strategy name";


			//start note: this is the place where strategy pattern is used to apply the strategy and get the expected output

			_combatContext.SetFightStrategy(playerFightStrategy);
			_player = _combatContext.EnterCombatMode(_player);

			_combatContext.SetFightStrategy(enemyFightStrategy);
			_enemy = _combatContext.EnterCombatMode(_enemy);

			//end note


			float damageDealtToPlayer = CalculateDamage(_enemy.FightAttack, _player.FightDefence);
			_player.ReceiveDamage(damageDealtToPlayer);

			float damageDealtToEnemy = CalculateDamage(_player.FightAttack, _enemy.FightDefence);
			_enemy.ReceiveDamage(damageDealtToEnemy);

			string playerFightStrategyName = playerFightStrategy?.ToString() ?? UNKNOWN_STRATEGY_NAME_MSG;
			string enemyFightStrategyName = enemyFightStrategy?.ToString() ?? UNKNOWN_STRATEGY_NAME_MSG;

			_fightHistory.Add(
				new FightRound(
					playerFightStrategyName,
					enemyFightStrategyName,
					damageDealtToPlayer,
					damageDealtToEnemy,
					(Character)_player.Clone(),
					(Character)_enemy.Clone()
				)
			);
		}
		
		private static float CalculateDamage(float firstPlayerAttackScore, float secondPlayerDefenceScore)
		{
			float damageDone = firstPlayerAttackScore - secondPlayerDefenceScore;

			return damageDone >= 0 
				? damageDone 
				: 0;
		}
	}
}
