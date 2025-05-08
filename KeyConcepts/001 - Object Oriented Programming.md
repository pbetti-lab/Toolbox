# 🔍 Understanding Object-Oriented Programming in C#

This guide walks through the key concepts of Object-Oriented Programming (OOP) in C#, starting with objects and then exploring the foundational principles.

---

## 1. 🧩 Objects and Classes

At the heart of OOP is the concept of an **object**. An object is a software entity that represents a real-world entity or concept, combining:

- **State** (data/attributes)
- **Behavior** (methods/functions)

A **class** is essentially a blueprint or template that defines what an object will look like and how it will behave.

> **Key Insight:** Objects are instances of classes, combining data and the operations that can be performed on that data.

```csharp
// This is a class definition
public class Car
{
    // State (attributes/fields)
    public string Make;
    public string Model;
    public int Year;
    
    // Behavior (methods)
    public void StartEngine()
    {
        Console.WriteLine($"The {Make} {Model}'s engine starts!");
    }
    
    public void Drive()
    {
        Console.WriteLine($"The {Make} {Model} is moving!");
    }
}

// Creating an object (instance) of the Car class
Car myCar = new Car();
myCar.Make = "Toyota";
myCar.Model = "Corolla";
myCar.Year = 2023;

// Using the object's behavior
myCar.StartEngine(); // Outputs: The Toyota Corolla's engine starts!
```

📝 **Example Explanation:**
- We defined a `Car` class with properties (`Make`, `Model`, `Year`) and methods (`StartEngine`, `Drive`)
- We created an instance of that class (`myCar`)
- We set the object's state by assigning values to its properties
- We triggered the object's behavior by calling its method

## 2. 🔒 Encapsulation

**Encapsulation** is about bundling data and methods that operate on that data within a single unit (the class) and restricting direct access to some of the object's components.

### Benefits:
- ✅ Protects data from unintended modifications
- ✅ Hides implementation details
- ✅ Promotes modularity

> **Design Principle:** Expose only what is necessary and hide the internal details.

```csharp
public class BankAccount
{
    // Private field - hidden from outside the class
    private decimal _balance;
    
    // Public property - controlled access to the private field
    public decimal Balance 
    { 
        get { return _balance; }
        private set { _balance = value; } // Only accessible within the class
    }
    
    public string AccountNumber { get; private set; }
    
    // Constructor
    public BankAccount(string accountNumber, decimal initialDeposit)
    {
        AccountNumber = accountNumber;
        _balance = initialDeposit;
    }
    
    // Public methods to interact with the balance
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Deposit amount must be positive");
            
        _balance += amount;
    }
    
    public bool Withdraw(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Withdrawal amount must be positive");
            
        if (_balance >= amount)
        {
            _balance -= amount;
            return true;
        }
        return false;
    }
}
```

📝 **Code Highlights:**
- The `_balance` field is **private**, meaning it can't be accessed directly from outside the class
- The `Balance` property has a public getter but private setter, allowing read access but controlled modification
- The `Deposit` and `Withdraw` methods enforce business rules (no negative amounts)
- External code must use these methods rather than modifying the balance directly

## 3. 🌳 Inheritance

**Inheritance** allows a class to inherit properties and methods from another class, establishing an "is-a" relationship. It promotes code reuse and establishes hierarchy among classes.

> **Core Concept:** With inheritance, a derived class automatically gets all the behavior from its parent class, and can then extend or modify that behavior.

```csharp
// Base/Parent class
public class Animal
{
    public string Name { get; set; }
    public int Age { get; set; }
    
    public void Eat()
    {
        Console.WriteLine($"{Name} is eating.");
    }
    
    public virtual void MakeSound()
    {
        Console.WriteLine($"{Name} makes a sound.");
    }
}

// Derived/Child class
public class Dog : Animal
{
    public string Breed { get; set; }
    
    // Override the base class method
    public override void MakeSound()
    {
        Console.WriteLine($"{Name} barks: Woof woof!");
    }
    
    // Add new behavior specific to dogs
    public void Fetch()
    {
        Console.WriteLine($"{Name} is fetching a ball!");
    }
}

// Usage
Dog myDog = new Dog();
myDog.Name = "Rex";
myDog.Age = 3;
myDog.Breed = "German Shepherd";

// Methods from the base class
myDog.Eat();        // Rex is eating.

// Overridden method
myDog.MakeSound();  // Rex barks: Woof woof!

// Method specific to Dog
myDog.Fetch();      // Rex is fetching a ball!
```

📝 **Inheritance in Action:**
- The `Dog` class inherits from `Animal` using the `:` syntax
- `Dog` automatically gets all the properties and methods of `Animal` (like `Name`, `Age`, and `Eat()`)
- The `virtual` keyword in the base class allows the method to be overridden
- The `override` keyword in the derived class changes the inherited behavior
- The derived class can add new properties and methods not in the base class

## 4. 🔄 Polymorphism

**Polymorphism** allows objects of different classes to be treated as objects of a common base class, while maintaining their unique behaviors.

> **Key Principle:** "Poly" means many and "morph" means forms - this concept allows us to work with objects of different types through a common interface.

```csharp
public class Shape
{
    public virtual double CalculateArea()
    {
        return 0;
    }
}

public class Circle : Shape
{
    public double Radius { get; set; }
    
    public Circle(double radius)
    {
        Radius = radius;
    }
    
    public override double CalculateArea()
    {
        return Math.PI * Radius * Radius;
    }
}

public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }
    
    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }
    
    public override double CalculateArea()
    {
        return Width * Height;
    }
}

// Polymorphism in action
Shape[] shapes = new Shape[2];
shapes[0] = new Circle(5);
shapes[1] = new Rectangle(4, 6);

foreach (Shape shape in shapes)
{
    // The correct implementation of CalculateArea() is called
    // based on the actual object type
    Console.WriteLine($"Area: {shape.CalculateArea()}");
}
// Outputs:
// Area: 78.53981633974483 (Circle area)
// Area: 24 (Rectangle area)
```

📝 **Polymorphism Explained:**
- We have different shape classes that all inherit from the `Shape` base class
- Each shape calculates its area differently but shares the same method name
- We can store different shape types in an array of the base `Shape` type
- When we call `CalculateArea()`, the correct implementation runs based on the actual object type
- The code calling the method doesn't need to know which specific shape it's working with

## 5. 🏗️ Abstraction

**Abstraction** is the concept of hiding complex implementation details and showing only essential features. It helps manage complexity by hiding unnecessary details.

> **Design Philosophy:** Abstraction lets you focus on what an object does rather than how it does it, simplifying system design and usage.

```csharp
// Abstract class
public abstract class Vehicle
{
    public string Registration { get; set; }
    
    // Abstract method - must be implemented by derived classes
    public abstract void Start();
    
    // Regular method with implementation
    public void RegisterVehicle()
    {
        Console.WriteLine($"Vehicle registered with number: {Registration}");
    }
}

// Concrete implementations
public class ElectricCar : Vehicle
{
    public override void Start()
    {
        Console.WriteLine("Electric car starts silently");
    }
    
    public void ChargeBattery()
    {
        Console.WriteLine("Charging the battery");
    }
}

public class DieselTruck : Vehicle
{
    public override void Start()
    {
        Console.WriteLine("Diesel truck starts with a rumble");
    }
    
    public void RefuelDiesel()
    {
        Console.WriteLine("Refueling with diesel");
    }
}
```

📝 **Abstraction Features:**
- The `abstract` keyword creates a class that cannot be instantiated directly
- Abstract classes can contain both abstract methods (no implementation) and concrete methods (with implementation)
- Abstract methods define a contract that derived classes must fulfill
- This creates a common interface while allowing specialized implementations
- Each concrete class must provide its own implementation of the abstract methods

## 6. 📋 Interfaces

An **interface** defines a contract that classes can implement. Unlike abstract classes, interfaces cannot provide any implementation.

> **Design Pattern:** Interfaces are pure contracts that define what an implementing class must do, without dictating how to do it.

```csharp
// Interface definition
public interface IPayable
{
    decimal CalculatePayment();
    void ProcessPayment();
}

// Another interface
public interface ITaxable
{
    decimal CalculateTax();
}

// Class implementing multiple interfaces
public class Employee : IPayable, ITaxable
{
    public string Name { get; set; }
    public decimal HourlyRate { get; set; }
    public int HoursWorked { get; set; }
    public decimal TaxRate { get; set; }
    
    public decimal CalculatePayment()
    {
        return HourlyRate * HoursWorked;
    }
    
    public void ProcessPayment()
    {
        decimal payment = CalculatePayment();
        Console.WriteLine($"Processing payment of ${payment} for {Name}");
    }
    
    public decimal CalculateTax()
    {
        return CalculatePayment() * TaxRate;
    }
}

// Another class implementing the same interface
public class Invoice : IPayable
{
    public string InvoiceNumber { get; set; }
    public decimal Amount { get; set; }
    
    public decimal CalculatePayment()
    {
        return Amount;
    }
    
    public void ProcessPayment()
    {
        Console.WriteLine($"Processing payment for invoice {InvoiceNumber}: ${Amount}");
    }
}
```

📝 **Interface Benefits:**
- Interfaces only declare methods and properties without any implementation
- Classes can implement multiple interfaces (unlike inheritance, which is limited to one base class)
- Different class types can implement the same interface, enabling polymorphic behavior
- In this example, both `Employee` and `Invoice` are `IPayable`, so they can be processed the same way
- The `Employee` class also implements a second interface `ITaxable`, showing how interfaces can be combined

## 7. 🧰 Composition

**Composition** is a design principle where a class contains objects of other classes to create complex functionality, establishing a "has-a" relationship.

> **Design Principle:** "Favor composition over inheritance" is a common OOP guideline, as composition can provide more flexibility and less coupling.

```csharp
// Component classes
public class Engine
{
    public int Horsepower { get; set; }
    
    public void Start()
    {
        Console.WriteLine($"Engine with {Horsepower}hp starts");
    }
    
    public void Stop()
    {
        Console.WriteLine("Engine stops");
    }
}

public class Transmission
{
    public string Type { get; set; } // Automatic or Manual
    
    public void ChangeGear(int gear)
    {
        Console.WriteLine($"{Type} transmission changes to gear {gear}");
    }
}

// Composite class using composition
public class Car
{
    // Composition: Car has-an Engine and has-a Transmission
    private Engine _engine;
    private Transmission _transmission;
    
    public string Make { get; set; }
    public string Model { get; set; }
    
    public Car(string make, string model, int horsePower, string transmissionType)
    {
        Make = make;
        Model = model;
        _engine = new Engine { Horsepower = horsePower };
        _transmission = new Transmission { Type = transmissionType };
    }
    
    public void Start()
    {
        Console.WriteLine($"{Make} {Model} is starting...");
        _engine.Start();
    }
    
    public void Drive(int gear)
    {
        _transmission.ChangeGear(gear);
        Console.WriteLine($"{Make} {Model} is moving");
    }
    
    public void Stop()
    {
        Console.WriteLine($"{Make} {Model} is stopping...");
        _engine.Stop();
    }
}
```

📝 **Composition Structure:**
- A `Car` **has-an** `Engine` and **has-a** `Transmission` (rather than inheriting from them)
- The `Car` class contains and manages instances of other classes
- The `Car` delegates specific behaviors to its component objects
- The public methods of `Car` create a simpler interface for the complex system
- Components can be swapped out without changing the `Car` class structure

## 8. 🏗️ Constructors and Destructors

**Constructors** are special methods that initialize objects, while **destructors** (finalizers in C#) clean up resources.

> **Lifecycle Management:** Constructors ensure objects begin in a valid state, while finalizers help clean up unmanaged resources.

```csharp
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    
    // Default constructor
    public Person()
    {
        Name = "Unknown";
        Age = 0;
        Console.WriteLine("Person created with default values");
    }
    
    // Parameterized constructor
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
        Console.WriteLine($"Person created with name: {name}, age: {age}");
    }
    
    // Destructor/Finalizer (rarely used in modern C#)
    ~Person()
    {
        Console.WriteLine($"Person object for {Name} is being destroyed");
    }
}
```

📝 **Constructor Patterns:**
- Constructors have the same name as the class and no return type
- The default constructor takes no parameters and sets default values
- Parameterized constructors allow customizing the object during creation
- Multiple constructors provide different ways to create objects (constructor overloading)
- The finalizer (prefixed with ~) is called by the garbage collector (rarely needed in managed code)

## 9. 🔑 Properties and Access Modifiers

**Properties** provide a flexible mechanism to read, write, or compute the values of private fields, while **access modifiers** control the visibility of classes and members.

> **Access Control:** Properties act as smart fields that can validate data, while access modifiers create boundaries around your code to prevent misuse.

```csharp
public class Student
{
    // Private backing field
    private string _name;
    
    // Property with validation
    public string Name
    {
        get { return _name; }
        set 
        { 
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Name cannot be empty");
            _name = value;
        }
    }
    
    // Auto-implemented property
    public int Id { get; set; }
    
    // Read-only property (can only be set in constructor or by the class itself)
    public DateTime EnrollmentDate { get; private set; }
    
    // Calculated property (no backing field)
    public bool IsEnrolled
    {
        get { return EnrollmentDate != DateTime.MinValue; }
    }
    
    // Private method - only accessible within this class
    private void UpdateStudentRecord()
    {
        Console.WriteLine("Updating student record...");
    }
    
    // Protected method - accessible from this class and derived classes
    protected void NotifyChange()
    {
        Console.WriteLine("Student record has changed");
    }
    
    // Public method - accessible from anywhere
    public void Enroll()
    {
        EnrollmentDate = DateTime.Now;
        UpdateStudentRecord();
        NotifyChange();
    }
}
```

📝 **Access Control Features:**
- **Access Modifiers:**
  - `public`: Accessible from anywhere
  - `private`: Only accessible within the containing class
  - `protected`: Accessible within the class and derived classes
  - `internal`: Accessible within the same assembly

- **Property Patterns:**
  - Full property with a backing field and custom getter/setter logic
  - Auto-implemented property (compiler creates the backing field automatically)
  - Read-only property (has a public getter but private setter)
  - Calculated property (computes a value rather than storing one)

## 10. 🔄 Method Overloading and Overriding

**Method overloading** allows multiple methods with the same name but different parameters, while **method overriding** allows a derived class to provide a specific implementation of a method defined in the base class.

> **Flexible APIs:** Overloading creates multiple entry points with the same name, while overriding allows customizing inherited behavior.

```csharp
public class Calculator
{
    // Method overloading - same name, different parameters
    public int Add(int a, int b)
    {
        return a + b;
    }
    
    public double Add(double a, double b)
    {
        return a + b;
    }
    
    public int Add(int a, int b, int c)
    {
        return a + b + c;
    }
}

public class Animal
{
    public virtual void MakeSound()
    {
        Console.WriteLine("Animal makes a generic sound");
    }
}

public class Cat : Animal
{
    // Method overriding - changes the behavior of the inherited method
    public override void MakeSound()
    {
        Console.WriteLine("Cat meows");
    }
}
```

📝 **Method Variations:**

- **Method Overloading (Static Polymorphism):**
  - Multiple methods with the same name in the same class
  - Must differ in number, type, or order of parameters
  - Return type alone is not enough to differentiate overloaded methods
  - Resolved at compile time

- **Method Overriding (Dynamic Polymorphism):**
  - Reimplementing a method from a base class in a derived class
  - Must have the same signature (name, parameters, and return type)
  - Base method must be marked with `virtual`, `abstract`, or `override`
  - Derived method must use the `override` keyword
  - Resolved at runtime based on the actual object type

These core concepts form the foundation of object-oriented programming in C#. By understanding and applying them correctly, you can create well-structured, maintainable, and robust applications.
