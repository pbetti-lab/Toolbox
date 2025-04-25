namespace Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Models
{
	public record FightRound(
		string PlayerFightStrategy,
		string EnemyFightStrategy,
		float PlayerDamageSuffered,
		float EnemyDamageSuffered,
		Character CurrentPlayerStatus,
		Character CurrentEnemyStatus);
}
