using FluentAssertions;
using Toolbox.Logic.DesignPatterns.Behavioral.StrategyPattern.Models;

namespace Toolbox.Logic.Tests.DesignPatterns.Behavioral.StrategyPattern.Models
{
	public class CharacterTests
	{
		[Fact]
		public void GetInstance_WithValidValues_CreateValidCharacter()
		{
			// arrange
			string characterClass = "Thief";
			float health = 20;
			float baseAttack = 15;
			float baseDefence = 13;

			var character = new Character(characterClass, health, baseAttack, baseDefence);

			// assert
			character.CharacterClass.Should().Be(characterClass);
			character.Health.Should().BeApproximately(health, 0.001f);
			character.BaseAttack.Should().BeApproximately(baseAttack, 0.001f);
			character.BaseDefence.Should().BeApproximately(baseDefence, 0.001f);
			character.FightAttack.Should().BeApproximately(baseAttack, 0.001f);
			character.FightDefence.Should().BeApproximately(baseDefence, 0.001f);
		}

		[Fact]
		public void GetInstance_WithInvalidCharacterClass_ThrowArgumentNullException()
		{
			// arrange
			string characterClass = null;
			float health = 20;
			float baseAttack = 15;
			float baseDefence = 13;

			var getInstace = () => new Character(characterClass, health, baseAttack, baseDefence);

			// assert
			getInstace.Should().ThrowExactly<ArgumentNullException>();
		}

		[Theory]
		[InlineData(-0.1f, 15f, 13f)]
		[InlineData(20f, -0.1f, 13f)]
		[InlineData(20f, 15f, -0.1f)]
		public void GetInstance_WithInvalidHealthOrAttackOrDefenceValues_ThrowArgumentException(
			float health, 
			float baseAttack,
			float baseDefence)
		{
			// arrange
			string characterClass = "Thief";

			var getInstace = () => new Character(characterClass, health, baseAttack, baseDefence);

			// assert
			getInstace.Should().ThrowExactly<ArgumentOutOfRangeException>();
		}

		[Fact]
		public void GetCloneCharacter_AfterUpdateAttackAndDefenceFightValues_HasValidValues()
		{
			// arrange
			string characterClass = "Thief";
			float health = 20;
			float baseAttack = 15;
			float baseDefence = 13;

			var character = new Character(characterClass, health, baseAttack, baseDefence);

			float fightAttack = baseAttack + 5;
			float fightDefence = baseDefence - 5;

			// act
			character.FightAttack = fightAttack;
			character.FightDefence = fightDefence;

			var clonedCharacter = (Character)character.Clone();

			// assert
			clonedCharacter.CharacterClass.Should().Be(characterClass);
			clonedCharacter.BaseAttack.Should().BeApproximately(baseAttack, 0.001f);
			clonedCharacter.BaseDefence.Should().BeApproximately(baseDefence, 0.001f);
			clonedCharacter.FightAttack.Should().BeApproximately(fightAttack, 0.001f);
			clonedCharacter.FightDefence.Should().BeApproximately(fightDefence, 0.001f);
		}

		[Fact]
		public void ReceiveDamage_WithNegativeDamage_PreserveCorrectHealt()
		{
			// arrange
			string characterClass = "Thief";
			float health = 20;
			float baseAttack = 15;
			float baseDefence = 13;

			var character = new Character(characterClass, health, baseAttack, baseDefence);

			float damage = -5;

			// act
			character.ReceiveDamage(damage);

			// assert
			character.Health.Should().BeApproximately(health, 0.001f);
		}

		[Fact]
		public void ReceiveDamage_WithDamageGreaterThanHealth_HealthRemainsZero()
		{
			// arrange
			string characterClass = "Thief";
			float health = 20;
			float baseAttack = 15;
			float baseDefence = 13;

			var character = new Character(characterClass, health, baseAttack, baseDefence);

			float damage = 25;

			// act
			character.ReceiveDamage(damage);

			// assert
			character.Health.Should().BeApproximately(0, 0.001f);
		}

		[Fact]
		public void Character_CallToString_ReturnValidMessage()
		{
			// arrange
			string characterClass = "Thief";
			float health = 20;
			float baseAttack = 15;
			float baseDefence = 13;

			var character = new Character(characterClass, health, baseAttack, baseDefence);

			// act
			float fightAttack = baseAttack + 5;
			float fightDefence = baseDefence - 5;

			character.FightAttack = fightAttack;
			character.FightDefence = fightDefence;

			string toStringMessage = $"{character.CharacterClass} with Health: {character.Health}, " +
				$"BaseAttack: {character.BaseAttack}, BaseDefence {character.BaseDefence}, " +
				$"FightAttack: {character.FightAttack}, FightDefence {character.FightDefence}";

			// assert
			character.ToString().Should().Be(toStringMessage);
		}
	}
}
