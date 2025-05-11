namespace Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Models
{
	public record FightRound(
		string PlayerFightStrategy,
		string EnemyFightStrategy,
		float DamageDealtToPlayer,
		float DamageDealtToEnemy,
		Character CurrentPlayerStatus,
		Character CurrentEnemyStatus);
}
