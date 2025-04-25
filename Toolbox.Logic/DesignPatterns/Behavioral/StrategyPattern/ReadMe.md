
# Strategy Pattern – Deep Dive

## 🧭 What Problem Does the Strategy Pattern Solve?

When you have a class that performs a task using different behaviors depending on a condition, the logic often ends up in conditional blocks like:

```csharp
if (condition == "A") DoA();
else if (condition == "B") DoB();
```

This results in:
- Hard-to-maintain code
- Poor testability
- Violation of the Open/Closed Principle

---

## 🧱 Structure and Design

### UML-Style Breakdown

```
        [ IStrategy ]
            ↑
   ---------------------
   ↑         ↑         ↑
ConcreteA  ConcreteB  ConcreteC

           [ Context ]
               |
     Uses IStrategy to perform action
```

### C# Interface-Based Structure

```csharp
public interface IStrategy
{
    void Execute();
}

public class ConcreteStrategyA : IStrategy
{
    public void Execute() => Console.WriteLine("Strategy A");
}

public class ConcreteStrategyB : IStrategy
{
    public void Execute() => Console.WriteLine("Strategy B");
}

public class Context
{
    private readonly IStrategy _strategy;

    public Context(IStrategy strategy)
    {
        _strategy = strategy;
    }

    public void DoWork()
    {
        _strategy.Execute();
    }
}
```

---

## 🔍 Internal Mechanics

- **Composition over inheritance**
- **Dependency injection**
- Promotes flexible behavior assignment at runtime

> “Don’t ask a class to decide what to do. Instead, inject the behavior into it.”

---

## ✅ Benefits

| Benefit             | Description                                          |
|--------------------|------------------------------------------------------|
| 🔧 Extensibility    | Add new behavior without changing the context        |
| 🧪 Testability      | Strategies can be tested independently               |
| ♻️ Reusability       | Strategies can be reused across different contexts   |
| 🚦 Separation of concerns | Each strategy encapsulates one behavior/algorithm |

---

## ⚠️ Trade-offs

| Concern             | Detail                                               |
|---------------------|------------------------------------------------------|
| More Classes        | Each behavior means one additional class             |
| Complexity Overhead | Might be overkill for simple logic                   |
| Statelessness       | Strategies should remain stateless for reusability   |

---

## 🆚 Compared To Alternatives

| Pattern            | Use When...                                           |
|--------------------|-------------------------------------------------------|
| **Strategy**       | You want to switch behavior at runtime                |
| **State**          | Behavior depends on internal state                    |
| **Template Method**| You want to override parts of an algorithm            |
| **Command**        | You want to encapsulate a request as an object        |

---

## 🧪 Testing Strategy

Each strategy is easily unit tested:

```csharp
[Fact]
public void Execute_ShouldPerformStrategyA()
{
    var strategy = new ConcreteStrategyA();
    
    strategy.Execute(); // Assert expected behavior/output
}
```

You can also test the context with mock strategies if needed.
