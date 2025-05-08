# Software Development Principles Guide
## DRY, KISS, YAGNI, and Composition over Inheritance in C#

---

## 📝 DRY - Don't Repeat Yourself

### Concept
The DRY principle states that "Every piece of knowledge must have a single, unambiguous, authoritative representation within a system." It aims to reduce repetition of code and information, promoting maintainability and reducing the likelihood of errors.

### Key Points

- ✅ **Eliminate code duplication** through abstraction
- ✅ **Single source of truth** for any particular functionality
- ✅ **Easier maintenance** as changes need to be made in only one place
- ✅ **Reduces bugs** since fixes are applied universally

### Real-World Example

#### ❌ Violating DRY (Bad Practice)

```csharp
public class CustomerService
{
    public bool ValidateCustomerEmail(string email)
    {
        // Complex email validation logic
        var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        return regex.IsMatch(email);
    }
}

public class EmployeeService
{
    public bool ValidateEmployeeEmail(string email)
    {
        // Same email validation logic duplicated
        var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        return regex.IsMatch(email);
    }
}
```

#### ✅ Following DRY (Good Practice)

```csharp
public static class ValidationUtility
{
    public static bool IsValidEmail(string email)
    {
        var regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        return regex.IsMatch(email);
    }
}

public class CustomerService
{
    public bool ValidateCustomerEmail(string email)
    {
        return ValidationUtility.IsValidEmail(email);
    }
}

public class EmployeeService
{
    public bool ValidateEmployeeEmail(string email)
    {
        return ValidationUtility.IsValidEmail(email);
    }
}
```

### Benefits in Practice

When a bug is found in the email validation logic or requirements change (e.g., need to support new TLDs), you only need to update the `ValidationUtility.IsValidEmail` method once, and all code using this method will automatically use the corrected version.

---

## 🪶 KISS - Keep It Simple, Stupid

### Concept
The KISS principle emphasizes that systems work best when they are kept simple rather than made complex. Simplicity should be a key goal in design, and unnecessary complexity should be avoided.

### Key Points

- ✅ **Favor clarity over cleverness**
- ✅ **Use straightforward solutions** that are easy to understand and maintain
- ✅ **Write code for humans**, not just for computers
- ✅ **Avoid premature optimization** and over-engineering

### Real-World Example

#### ❌ Violating KISS (Bad Practice)

```csharp
public decimal CalculateDiscount(Customer customer, Order order)
{
    decimal discount = 0;
    if (customer.Type == CustomerType.Regular && order.TotalAmount > 1000 
        || customer.Type == CustomerType.Premium && order.TotalAmount > 500 
        || (customer.Type == CustomerType.VIP && order.TotalAmount > 0) 
        || (customer.JoinDate < DateTime.Now.AddYears(-2) && order.TotalAmount > 100))
    {
        decimal baseDiscount = order.TotalAmount * 0.1m;
        discount = baseDiscount * (customer.Type == CustomerType.VIP ? 1.5m : 
                                  customer.Type == CustomerType.Premium ? 1.2m : 1.0m);
        if (order.Items.Count(i => i.Category == "Electronics") > 2)
        {
            discount += order.TotalAmount * 0.05m;
        }
    }
    return discount;
}
```

#### ✅ Following KISS (Good Practice)

```csharp
public decimal CalculateDiscount(Customer customer, Order order)
{
    // Base discount rate by customer type
    decimal discountRate = GetBaseDiscountRate(customer.Type);
    
    // Apply threshold based on customer type
    if (!MeetsOrderThreshold(customer, order))
    {
        return 0;
    }
    
    // Calculate the base discount
    decimal discount = order.TotalAmount * discountRate;
    
    // Add electronics bonus if applicable
    if (HasElectronicsBonus(order))
    {
        discount += order.TotalAmount * 0.05m;
    }
    
    return discount;
}

private bool MeetsOrderThreshold(Customer customer, Order order)
{
    switch (customer.Type)
    {
        case CustomerType.VIP:
            return true;
        case CustomerType.Premium:
            return order.TotalAmount > 500;
        case CustomerType.Regular:
            if (customer.JoinDate < DateTime.Now.AddYears(-2))
                return order.TotalAmount > 100;
            return order.TotalAmount > 1000;
        default:
            return false;
    }
}

private decimal GetBaseDiscountRate(CustomerType customerType)
{
    switch (customerType)
    {
        case CustomerType.VIP:
            return 0.15m;
        case CustomerType.Premium:
            return 0.12m;
        default:
            return 0.10m;
    }
}

private bool HasElectronicsBonus(Order order)
{
    return order.Items.Count(i => i.Category == "Electronics") > 2;
}
```

### Benefits in Practice

The KISS version separates concerns into smaller, focused methods that are easier to read, test, and maintain. Each method has a clear responsibility, making the code more understandable and less prone to logical errors.

---

## 🏗️ YAGNI - You Aren't Gonna Need It

### Concept
YAGNI is a principle that suggests developers should not add functionality until it is necessary. It advises against implementing features based on speculation about future needs.

### Key Points

- ✅ **Implement only what you need now**, not what you might need later
- ✅ **Avoid speculative generality** and overengineering
- ✅ **Reduces codebase bloat** and maintenance burden
- ✅ **Focus on current requirements** instead of hypothetical future ones

### Real-World Example

#### ❌ Violating YAGNI (Bad Practice)

```csharp
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    
    // Features we don't need yet but "might need someday"
    public List<Address> Addresses { get; set; } = new List<Address>();
    public List<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();
    public LoyaltyProgram LoyaltyProgram { get; set; }
    public CustomerPreferences Preferences { get; set; } = new CustomerPreferences();
    public bool SubscribedToNewsletter { get; set; }
    public Dictionary<string, string> CustomAttributes { get; set; } = new Dictionary<string, string>();
    
    // Methods for features we don't need yet
    public void SendPromotionalEmail(string promotion) { /* ... */ }
    public void CalculateLoyaltyPoints() { /* ... */ }
    public void ApplyDiscount(Order order) { /* ... */ }
}
```

#### ✅ Following YAGNI (Good Practice)

```csharp
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    
    // Only implement what's currently needed
}
```

Later, when an actual requirement for addresses emerges:

```csharp
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Address PrimaryAddress { get; set; } // Added when actually needed
}
```

### Benefits in Practice

By following YAGNI, you create a leaner codebase focused on actual requirements. This approach:
- Reduces development time spent on unused features
- Simplifies testing and maintenance
- Prevents design decisions based on hypothetical scenarios
- Allows the system to evolve based on real needs rather than assumptions

---

## 🧩 Composition over Inheritance

### Concept
This principle suggests that code reuse should be achieved by assembling smaller, specific components (composition) rather than relying on inheritance hierarchies.

### Key Points

- ✅ **Favors "has-a" relationships** over "is-a" relationships
- ✅ **Reduces tight coupling** between classes
- ✅ **More flexible and adaptable** to changing requirements
- ✅ **Avoids the fragile base class problem**
- ✅ **Encourages interface-based design**

### Real-World Example

#### ❌ Inheritance Approach (Less Flexible)

```csharp
public abstract class Vehicle
{
    public int Speed { get; set; }
    public int Capacity { get; set; }
    
    public abstract void Move();
    public abstract void Stop();
    public virtual void Refuel() 
    {
        Console.WriteLine("Generic refueling");
    }
}

public class Car : Vehicle
{
    public override void Move()
    {
        Console.WriteLine("Car drives on the road");
    }
    
    public override void Stop()
    {
        Console.WriteLine("Car stops with brakes");
    }
}

public class Airplane : Vehicle
{
    public int Altitude { get; set; }
    
    public override void Move()
    {
        Console.WriteLine("Airplane flies in the sky");
    }
    
    public override void Stop()
    {
        Console.WriteLine("Airplane lands on runway");
    }
    
    // What if airplanes and boats need to refuel differently?
    public override void Refuel()
    {
        Console.WriteLine("Special aviation fuel refueling");
    }
    
    public void TakeOff() 
    {
        Console.WriteLine("Airplane takes off");
    }
}

// What about a seaplane? It's both a boat and a plane...
// Inheritance creates problems with this kind of classification
```

#### ✅ Composition Approach (More Flexible)

```csharp
// Define capabilities as interfaces
public interface IMovable
{
    void Move();
}

public interface IStoppable
{
    void Stop();
}

public interface IRefuelable
{
    void Refuel();
}

public interface IFlyable
{
    void TakeOff();
    void Land();
    int Altitude { get; set; }
}

// Implement concrete movement strategies
public class RoadMovement : IMovable
{
    public void Move()
    {
        Console.WriteLine("Moves on the road");
    }
}

public class AirMovement : IMovable
{
    public void Move()
    {
        Console.WriteLine("Flies through the air");
    }
}

public class StandardFuel : IRefuelable
{
    public void Refuel()
    {
        Console.WriteLine("Standard refueling");
    }
}

public class AviationFuel : IRefuelable
{
    public void Refuel()
    {
        Console.WriteLine("Aviation fuel refueling");
    }
}

// Compose the vehicles using the appropriate components
public class Car
{
    private readonly IMovable _movement;
    private readonly IStoppable _braking;
    private readonly IRefuelable _refueling;
    
    public int Speed { get; set; }
    public int Capacity { get; set; }
    
    public Car()
    {
        _movement = new RoadMovement();
        _braking = new StandardBrakes();
        _refueling = new StandardFuel();
    }
    
    public void Move() => _movement.Move();
    public void Stop() => _braking.Stop();
    public void Refuel() => _refueling.Refuel();
}

public class Airplane
{
    private readonly IMovable _movement;
    private readonly IStoppable _braking;
    private readonly IRefuelable _refueling;
    private readonly IFlyable _flightCapabilities;
    
    public int Speed { get; set; }
    public int Capacity { get; set; }
    
    public Airplane()
    {
        _movement = new AirMovement();
        _braking = new AirplaneBrakes();
        _refueling = new AviationFuel();
        _flightCapabilities = new FlightControl();
    }
    
    public void Move() => _movement.Move();
    public void Stop() => _braking.Stop();
    public void Refuel() => _refueling.Refuel();
    public void TakeOff() => _flightCapabilities.TakeOff();
    public void Land() => _flightCapabilities.Land();
    public int Altitude 
    { 
        get => _flightCapabilities.Altitude;
        set => _flightCapabilities.Altitude = value;
    }
}

// Now we can easily create a Seaplane by composing the right components
public class Seaplane
{
    private readonly IMovable _airMovement;
    private readonly IMovable _waterMovement;
    private readonly IStoppable _braking;
    private readonly IRefuelable _refueling;
    private readonly IFlyable _flightCapabilities;
    
    public bool IsFlying { get; private set; }
    
    public Seaplane()
    {
        _airMovement = new AirMovement();
        _waterMovement = new WaterMovement();
        _braking = new WaterAndAirBrakes();
        _refueling = new AviationFuel();
        _flightCapabilities = new FlightControl();
    }
    
    public void Move()
    {
        if (IsFlying)
            _airMovement.Move();
        else
            _waterMovement.Move();
    }
    
    public void TakeOff()
    {
        _flightCapabilities.TakeOff();
        IsFlying = true;
    }
}
```

### Benefits in Practice

Using composition:
- Allows for more flexible combinations of behaviors
- Makes it easier to change behavior at runtime
- Avoids the limitations of single inheritance
- Creates more modular, testable components
- Makes it easier to add new types of objects without modifying existing code

---

## 🔄 Principles in Harmony

These principles work together to create maintainable, adaptable, and high-quality code:

- **DRY** keeps your codebase efficient by eliminating duplication
- **KISS** ensures your code remains understandable and straightforward
- **YAGNI** prevents unnecessary complexity from speculative features
- **Composition over Inheritance** provides flexibility to adapt to changing requirements

When applied thoughtfully, these principles help create systems that are:
- Easier to maintain
- More resilient to change
- Simpler to understand
- More testable
- Less prone to bugs

Remember that these are principles, not strict rules. Use your judgment to apply them appropriately based on your project's specific context and requirements.

