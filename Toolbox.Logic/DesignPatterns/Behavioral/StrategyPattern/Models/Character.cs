namespace Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Models
{
	public class Character : ICloneable
	{
		public Character(string characterClass, float health, float baseAttack, float baseDefence)
		{
			ArgumentNullException.ThrowIfNullOrWhiteSpace(characterClass);

			if (health < 0.0)
				throw new ArgumentOutOfRangeException(nameof(health), "Value must be grater than 0");

			if (baseAttack <= 0.0)
				throw new ArgumentOutOfRangeException(nameof(baseAttack), "Value must be grater than 0");

			if (baseDefence <= 0.0)
				throw new ArgumentOutOfRangeException(nameof(baseDefence), "Value must be grater than 0");

			CharacterClass = characterClass;
			Health = health;
			BaseAttack = baseAttack;
			BaseDefence = baseDefence;
			FightAttack = baseAttack;
			FightDefence = baseDefence;
		}

		public string CharacterClass { get; private set; }
		public float Health { get; private set; }
		public float BaseAttack { get; private set; }
		public float BaseDefence { get; private set; }
		public float FightAttack { get; set; }
		public float FightDefence { get; set; }

		public object Clone()
		{
			return new Character(CharacterClass, Health, BaseAttack, BaseDefence)
			{
				FightAttack = FightAttack,
				FightDefence = FightDefence,
			};
		}

		/// <summary>
		/// Subtracts the damage from the total health. If the value is less then zero it subtracts zero.
		/// </summary>
		/// <param name="damage">The damage received by the character</param>
		public void ReceiveDamage(float damage)
		{
			if (damage < 0)
				return;

			Health = (Health - damage > 0)
				? Health - damage 
				: 0;
		}

		public override string ToString()
		{
			return $"{CharacterClass} with Health: {Health}, BaseAttack: {BaseAttack}, " +
				$"BaseDefence {BaseDefence}, FightAttack: {FightAttack}, FightDefence {FightDefence}";
		}
	}
}
