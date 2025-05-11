# 🎮 Strategy Pattern in Game Combat: A Practical Guide

## 📋 Overview

This guide explores a practical implementation of the Strategy pattern in a game combat system. The code demonstrates how different fighting strategies can be applied to characters during battle, allowing them to adapt their combat style based on the situation.

---

## 🎯 Purpose of the Code

The primary purpose of this implementation is to:

1. **Allow characters to change their combat behaviour dynamically during battle**
2. **Encapsulate different fighting algorithms in separate, interchangeable classes**
3. **Maintain a history of combat rounds for analysis and replay**
4. **Provide a flexible system that can easily accommodate new fighting strategies**

The code simulates a turn-based combat system where characters can adopt different fighting postures (aggressive, defensive, or evasive) that affect their attack and defence capabilities.

---

## 🧩 How the Strategy Pattern is Implemented

### The Strategy Interface

At the core of the implementation is the `IFightStrategy` interface, which defines the contract for all combat strategies:

```csharp
public interface IFightStrategy
{
    public Character ApplyFightStrategy(Character character);
}
```

This simple interface declares a single method that takes a character and applies a specific combat algorithm to it, returning the modified character.

### Concrete Strategies

Three concrete strategy classes implement the interface, each providing a different combat approach:

#### 🗡️ Aggressive Strategy

```csharp
internal class AggressiveFightStrategy : IFightStrategy
{
    private const float ATTACK_FACTOR = 1.8f;
    private const float DEFENCE_FACTOR = 0.6f;

    public Character ApplyFightStrategy(Character character)
    {
        ArgumentNullException.ThrowIfNull(character);

        character.FightAttack = character.BaseAttack * ATTACK_FACTOR;
        character.FightDefence = character.BaseDefence * DEFENCE_FACTOR;

        return character;
    }
}
```

This strategy significantly boosts attack (1.8x) while reducing defence (0.6x), making it ideal for offensive play.

#### 🛡️ Defensive Strategy

```csharp
internal class DefensiveFightStrategy : IFightStrategy
{
    private const float ATTACK_FACTOR = 0.6f;
    private const float DEFENCE_FACTOR = 1.8f;

    public Character ApplyFightStrategy(Character character)
    {
        ArgumentNullException.ThrowIfNull(character);

        character.FightAttack = character.BaseAttack * ATTACK_FACTOR;
        character.FightDefence = character.BaseDefence * DEFENCE_FACTOR;

        return character;
    }
}
```

This strategy does the opposite, reducing attack (0.6x) but boosting defence (1.8x), making it suitable for defensive play.

#### 💨 Evasive Strategy

```csharp
internal class EvasiveFightStrategy : IFightStrategy
{
    private const float ATTACK_FACTOR = 0.4f;
    private const float DEFENCE_FACTOR = 0.8f;

    public Character ApplyFightStrategy(Character character)
    {
        ArgumentNullException.ThrowIfNull(character);

        character.FightAttack = character.BaseAttack * ATTACK_FACTOR;
        character.FightDefence = character.BaseDefence * DEFENCE_FACTOR;

        return character;
    }
}
```

This strategy reduces both attack (0.4x) and defence (0.8x) but presumably would be balanced by other mechanics (like increased dodge chance) in a complete implementation.

### The Context Class

The `FightContext` class acts as the context in the Strategy pattern:

```csharp
public class FightContext
{
    private IFightStrategy _fightStrategy;

    public FightContext(IFightStrategy fightStrategy)
    {
        ArgumentNullException.ThrowIfNull(fightStrategy);
        _fightStrategy = fightStrategy;
    }

    public void SetFightStrategy(IFightStrategy fightStrategy)
    {
        ArgumentNullException.ThrowIfNull(fightStrategy);
        _fightStrategy = fightStrategy;
    }

    public Character EnterCombatMode(Character character)
    {
        ArgumentNullException.ThrowIfNull(character);
        _fightStrategy.ApplyFightStrategy(character);
        return character;
    }
}
```

The context maintains a reference to the current strategy and provides methods to:
- Set or change the strategy (`SetFightStrategy`)
- Apply the strategy to a character (`EnterCombatMode`)

---

## 🎬 Use Case: Turn-Based Combat System

### The Character Model

The `Character` class represents entities in the game world:

```csharp
public class Character : ICloneable
{
    public Character(string characterClass, float health, float baseAttack, float baseDefence)
    {
        // Parameter validation...
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

    // Methods for clone, damage calculation, etc.
}
```

Key attributes include:
- **Base attributes** (`BaseAttack`, `BaseDefence`) - inherent character stats
- **Fight attributes** (`FightAttack`, `FightDefence`) - modified by strategies
- **Health** - reduced when taking damage

### The Battle Manager

The `BattleManager` orchestrates combat between characters:

```csharp
public class BattleManager
{
    private Character _player;
    private Character _enemy;
    private readonly FightContext _combatContext;
    private readonly List<FightRound> _fightHistory;
    
    // Constructor and other methods...

    public void FightSingleRound(IFightStrategy playerFightStrategy, IFightStrategy enemyFightStrategy)
    {
        // Parameter validation...

        // Apply strategies to characters
        _combatContext.SetFightStrategy(playerFightStrategy);
        _player = _combatContext.EnterCombatMode(_player);

        _combatContext.SetFightStrategy(enemyFightStrategy);
        _enemy = _combatContext.EnterCombatMode(_enemy);

        // Calculate and apply damage
        float damageDealtToPlayer = CalculateDamage(_enemy.FightAttack, _player.FightDefence);
        _player.ReceiveDamage(damageDealtToPlayer);

        float damageDealtToEnemy = CalculateDamage(_player.FightAttack, _enemy.FightDefence);
        _enemy.ReceiveDamage(damageDealtToEnemy);

        // Record fight history
        _fightHistory.Add(
            new FightRound(
                playerFightStrategy?.ToString() ?? UNKNOWN_STRATEGY_NAME_MSG,
                enemyFightStrategy?.ToString() ?? UNKNOWN_STRATEGY_NAME_MSG,
                damageDealtToPlayer,
                damageDealtToEnemy,
                (Character)_player.Clone(),
                (Character)_enemy.Clone()
            )
        );
    }
    
    // Other methods...
}
```

The `FightSingleRound` method demonstrates the Strategy pattern in action:
1. It applies different strategies to the player and enemy
2. Calculates damage based on the modified attack and defence values
3. Records the outcome in a fight history log

### Battle History

The `FightRound` record captures the state after each round:

```csharp
public record FightRound(
    string PlayerFightStrategy,
    string EnemyFightStrategy,
    float DamageDealtToPlayer,
    float DamageDealtToEnemy,
    Character CurrentPlayerStatus,
    Character CurrentEnemyStatus);
```

This allows for detailed analysis and replay of battle sequences.

---

## 🔄 Complete Flow: How It All Works Together

Let's walk through a complete battle scenario:

### 1. Setup Phase

```csharp
// Create characters
var player = new Character("Ranger", 15, 12, 8);
var enemy = new Character("Skeleton", 30, 5, 5);

// Create strategies
var aggressiveStrategy = new AggressiveFightStrategy();
var defensiveStrategy = new DefensiveFightStrategy();

// Create the combat context
var fightContext = new FightContext(aggressiveStrategy);

// Initialize the battle manager
var battleManager = new BattleManager(player, enemy, fightContext);
```

### 2. Combat Phase

```csharp
// Round 1: Both use aggressive strategy
battleManager.FightSingleRound(aggressiveStrategy, aggressiveStrategy);

// Round 2: Player stays aggressive, enemy switches to defensive
battleManager.FightSingleRound(aggressiveStrategy, defensiveStrategy);

// Continue until a winner is determined
while (battleManager.AreBothCharactersAlive()) {
    // Choose strategies based on game state
    // ...
    battleManager.FightSingleRound(chosenPlayerStrategy, chosenEnemyStrategy);
}
```

### 3. Resolution Phase

```csharp
// Determine the winner
var winner = battleManager.GetWinner();
if (winner != null) {
    Console.WriteLine($"The winner is {winner.CharacterClass}!");
} else {
    Console.WriteLine("The battle ended in a draw!");
}

// Analysis phase: review the fight history
foreach (var round in battleManager.FightHistory) {
    Console.WriteLine($"Round: Player used {round.PlayerFightStrategy}, Enemy used {round.EnemyFightStrategy}");
    Console.WriteLine($"Player dealt {round.DamageDealtToEnemy} damage, Enemy dealt {round.DamageDealtToPlayer} damage");
    // More analysis...
}
```

---

## 🌟 Key Benefits of This Implementation

### 1. **Dynamic Strategy Selection**

Characters can change their fighting style on each turn based on the battle situation.

```csharp
// If player health is low, switch to defensive
if (player.Health < 5) {
    battleManager.FightSingleRound(defensiveStrategy, enemyStrategy);
}
```

### 2. **Easily Extendable**

New strategies can be added without modifying existing code:

```csharp
// A new balanced strategy
public class BalancedFightStrategy : IFightStrategy
{
    private const float ATTACK_FACTOR = 1.2f;
    private const float DEFENCE_FACTOR = 1.2f;

    public Character ApplyFightStrategy(Character character)
    {
        // Implementation...
    }
}
```

### 3. **Clean Separation of Concerns**

Each strategy focuses solely on its algorithm, while the FightContext handles strategy management and the BattleManager orchestrates the combat flow.

### 4. **Detailed Battle History**

The implementation maintains a complete record of the battle, including strategies used and their effects, enabling analysis and replay.

---

## 🧠 Advanced Usage: AI Strategy Selection

This pattern enables sophisticated AI decision-making for enemy characters:

```csharp
public IFightStrategy SelectOptimalStrategy(Character self, Character opponent)
{
    // If opponent has high attack but low defence
    if (opponent.FightAttack > opponent.FightDefence * 2) {
        return new DefensiveFightStrategy(); // Defensive is best against high attackers
    }
    
    // If self has low health
    if (self.Health < self.MaxHealth * 0.3) {
        // Randomly choose between defensive and evasive
        return Random.Next(2) == 0 
            ? new DefensiveFightStrategy() 
            : new EvasiveFightStrategy();
    }
    
    // Default to aggressive
    return new AggressiveFightStrategy();
}
```

---

## 🎓 Educational Examples

### Example 1: Player vs. Enemy Battle

```csharp
// Create player and enemy
var player = new Character("Paladin", 25, 10, 15);
var enemy = new Character("Orc", 20, 12, 8);

// Create strategies
var aggressive = new AggressiveFightStrategy();
var defensive = new DefensiveFightStrategy();
var evasive = new EvasiveFightStrategy();

// Set up battle
var context = new FightContext(aggressive);
var battle = new BattleManager(player, enemy, context);

// Fight until someone wins
while (battle.AreBothCharactersAlive()) {
    // Player always uses defensive strategy
    // Enemy alternates between aggressive and evasive
    var enemyStrategy = battle.FightHistory.Count % 2 == 0 ? aggressive : evasive;
    
    battle.FightSingleRound(defensive, enemyStrategy);
    
    // Display current status
    var lastRound = battle.FightHistory.Last();
    Console.WriteLine($"Player HP: {lastRound.CurrentPlayerStatus.Health}, " +
                      $"Enemy HP: {lastRound.CurrentEnemyStatus.Health}");
}

// Announce winner
var winner = battle.GetWinner();
Console.WriteLine($"The winner is: {winner?.CharacterClass ?? "Nobody (draw)"}");
```

### Example 2: Strategy Selection Based on Character State

```csharp
IFightStrategy SelectStrategyForCharacter(Character character)
{
    // Health-based selection
    var healthPercentage = character.Health / 100.0f; // Assuming 100 is max health
    
    if (healthPercentage < 0.3f) {
        // Low health - play defensive
        return new DefensiveFightStrategy();
    } else if (healthPercentage > 0.7f) {
        // High health - play aggressive
        return new AggressiveFightStrategy();
    } else {
        // Medium health - play evasive
        return new EvasiveFightStrategy();
    }
}
```

---

## 📝 Conclusion

This implementation demonstrates the Strategy pattern's power in a game combat system. By encapsulating different fighting behaviours in separate classes and allowing runtime switching between them, the code achieves flexibility and extensibility while maintaining clean separation of concerns.

The pattern creates a system where:

- Characters can dynamically change their combat approach
- New strategies can be added without modifying existing code
- Combat logic is cleanly separated from character state
- The battle flow is recorded for analysis and replay

For game developers, this pattern provides an elegant solution for implementing varied, dynamic combat behaviours that can adapt to the evolving state of the battle.
