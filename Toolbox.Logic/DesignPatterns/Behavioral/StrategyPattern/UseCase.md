# 🛡️ Battle Simulation System - Strategy Pattern Documentation

## 📌 Overview

This system is a simplified battle simulator built in C# that demonstrates the **Strategy Pattern**. It enables different combat behaviors (strategies) to be injected dynamically into characters during battle rounds.

The main goals of this implementation:
- Showcase the **Strategy Pattern** in a game-like domain.
- Enable extensible, testable combat behaviors.
- Support different modes of strategy selection for more realistic or varied simulation outcomes.

---

## 🔧 Components

### 1. `Character`
Represents a character in the battle. Each has base stats and dynamic fight stats adjusted by strategy.

**Properties:**
- `CharacterClass`: string
- `Health`: double
- `BaseAttack`, `BaseDefence`: base stats
- `FightAttack`, `FightDefence`: modified by strategy

**Methods:**
- `ReceiveDamage(double)`
- `Clone()`: for snapshotting during logging

---

### 2. `IFightStrategy`
Interface for strategies that modify a character's combat stats.

```csharp
public interface IFightStrategy
{
    Character ApplyFightStrategy(Character character);
}
```

**Implementations:**
- `AggressiveFightStrategy`: High attack, low defence.
- `DefensiveFightStrategy`: Low attack, high defence.
- `EvasiveFightStrategy`: Low attack and moderate defence.

Each overrides `ToString()` for readable logging.

---

### 3. `ICombatMode`
Interface for strategy selectors — it decides **which** strategy is used per round.

```csharp
public interface ICombatMode
{
    IFightStrategy GetFightStrategy();
}
```

**Implementations:**
- `MoreEvasiveLessAggressiveCombatMode`: Alternates mostly evasive, aggressive every 3rd round.
- `RandomCombatMode`: Uses random selection across all strategies.

> **Note**: This separation between strategy creation and strategy application illustrates the Strategy Pattern's flexibility.

---

### 4. `BattleManager`
Orchestrates the battle between two characters. It applies the fight strategies and logs each round.

**Constructor Parameters:**
- `Character player`, `ICombatMode playerCombatMode`
- `Character enemy`, `ICombatMode enemyCombatMode`

**Key Methods:**
- `Fight()`: Loops through battle rounds until one character's health drops to 0 or below.
- `GetLastFightLog()`: Returns a full text log of the battle.

**Damage Formula:**
```csharp
playerDamage = Max(0, enemy.FightAttack - player.FightDefence);
enemyDamage = Max(0, player.FightAttack - enemy.FightDefence);
```

---

## 💡 Design Highlights

### ✅ Strategy Injection
Each round, `BattleManager` asks the `ICombatMode` of each character to supply the appropriate `IFightStrategy`. This allows:
- Predictable strategies (e.g., `MoreEvasiveLessAggressiveCombatMode`)
- Random behaviors (`RandomCombatMode`)
- Future extensibility with AI-based selectors or difficulty modifiers

> ⚠️ This pattern enables **runtime injection** of strategy logic — a key feature of the Strategy Pattern.

---

## 🧪 Testing Suggestions
- Inject fixed strategies to test damage calculations.
- Mock `ICombatMode` to force consistent strategy behavior.
- Use `Clone()` to snapshot character status before and after strategy application.

---

## 🛠️ Potential Improvements
- Add a round limit to avoid infinite fights.
- Create a `FightLogger` class to cleanly separate logging from battle logic.
- Support team battles or more than 2 fighters.
- Persist battle history to file.

