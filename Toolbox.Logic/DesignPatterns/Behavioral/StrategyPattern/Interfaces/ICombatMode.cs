namespace Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Interfaces
{
	public interface ICombatMode
	{
		public IFightStrategy GetFightStrategy();
	}
}
