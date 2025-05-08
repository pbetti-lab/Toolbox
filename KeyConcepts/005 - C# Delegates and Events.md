# 📘 C# Events and Delegates

## 🔍 Introduction

In C#, delegates and events are powerful mechanisms that enable loose coupling and implementation of the observer pattern. They allow objects to communicate with each other without being tightly dependent on each other's implementation.

---

## 🎯 Delegates: Function Pointers in C#

### What are Delegates? 

🔹 A delegate is a type that represents references to methods with a particular parameter list and return type.

🔹 Think of delegates as type-safe function pointers or a way to treat methods as first-class objects.

🔹 Delegates allow methods to be passed as parameters, returned as values, and stored in variables.

### Declaring a Delegate

```csharp
// Declaring a delegate type
public delegate int MathOperation(int x, int y);
```

This delegate can reference any method that:
- Takes two int parameters
- Returns an int value

### Using Delegates

```csharp
// Methods that match the delegate signature
public static int Add(int a, int b) => a + b;
public static int Multiply(int a, int b) => a * b;

// Using the delegate
public static void UseDelegates()
{
    // Creating delegate instances
    MathOperation addition = Add;
    MathOperation multiplication = Multiply;
    
    // Invoking the delegates
    int sum = addition(5, 3);        // Returns 8
    int product = multiplication(5, 3);  // Returns 15
    
    Console.WriteLine($"Sum: {sum}, Product: {product}");
}
```

### Multicast Delegates

🔹 C# delegates can reference multiple methods - known as multicast delegates.

🔹 The `+=` and `-=` operators are used to add and remove methods from a delegate's invocation list.

```csharp
public delegate void NotifyUser(string message);

public static void SendEmail(string message) 
{
    Console.WriteLine($"Email sent: {message}");
}

public static void SendSMS(string message) 
{
    Console.WriteLine($"SMS sent: {message}");
}

public static void UseMulticastDelegate()
{
    // Creating a multicast delegate
    NotifyUser notifier = SendEmail;
    notifier += SendSMS;
    
    // This will invoke both methods
    notifier("System update scheduled for tomorrow");
    
    // Output:
    // Email sent: System update scheduled for tomorrow
    // SMS sent: System update scheduled for tomorrow
    
    // Removing a method
    notifier -= SendEmail;
    
    // Now only SendSMS will be invoked
    notifier("Reminder: Update your password");
}
```

---

## 🔔 Events: A Special Kind of Delegate

### What are Events?

🔹 Events are a way to provide notifications when something interesting happens in your application.

🔹 Technically, an event is a delegate field with special access modifiers that restrict how it can be used.

🔹 Events follow the publisher-subscriber pattern (observer pattern).

### Key Difference from Plain Delegates

🔹 Events can only be invoked from within the class that declares them.

🔹 External classes can only subscribe to or unsubscribe from events (using += and -=).

🔹 Events prevent outside code from:
  - Directly invoking the delegate
  - Replacing the delegate (cannot use the = operator)
  - Clearing all subscribers

### Declaring and Raising Events

```csharp
// Common pattern for event arguments
public class StockPriceChangedEventArgs : EventArgs
{
    public string StockName { get; set; }
    public decimal PreviousPrice { get; set; }
    public decimal CurrentPrice { get; set; }
}

// Publisher class
public class StockMonitor
{
    // Declaring an event using built-in EventHandler<T> delegate
    public event EventHandler<StockPriceChangedEventArgs> StockPriceChanged;
    
    private string _stockName;
    private decimal _currentPrice;
    
    public StockMonitor(string stockName, decimal initialPrice)
    {
        _stockName = stockName;
        _currentPrice = initialPrice;
    }
    
    public void UpdateStockPrice(decimal newPrice)
    {
        decimal oldPrice = _currentPrice;
        _currentPrice = newPrice;
        
        // Raising the event
        OnStockPriceChanged(new StockPriceChangedEventArgs 
        { 
            StockName = _stockName,
            PreviousPrice = oldPrice,
            CurrentPrice = newPrice
        });
    }
    
    // Protected method for raising the event
    protected virtual void OnStockPriceChanged(StockPriceChangedEventArgs e)
    {
        // Thread-safe event invocation (checking for null)
        StockPriceChanged?.Invoke(this, e);
    }
}
```

### Subscribing to Events

```csharp
// Subscriber class
public class StockAnalyzer
{
    public void StartMonitoring(StockMonitor monitor)
    {
        // Subscribe to the event
        monitor.StockPriceChanged += OnStockPriceChanged;
    }
    
    public void StopMonitoring(StockMonitor monitor)
    {
        // Unsubscribe from the event
        monitor.StockPriceChanged -= OnStockPriceChanged;
    }
    
    // Event handler method
    private void OnStockPriceChanged(object sender, StockPriceChangedEventArgs e)
    {
        decimal changePercent = (e.CurrentPrice - e.PreviousPrice) / e.PreviousPrice * 100;
        
        Console.WriteLine($"ALERT: {e.StockName} price changed from {e.PreviousPrice:C} to {e.CurrentPrice:C} ({changePercent:0.##}%)");
        
        if (Math.Abs(changePercent) > 10)
        {
            Console.WriteLine("⚠️ Significant price movement detected!");
        }
    }
}
```

### Real-World Usage Example

```csharp
class Program
{
    static void Main(string[] args)
    {
        // Create publisher
        var msftMonitor = new StockMonitor("MSFT", 245.42m);
        
        // Create subscribers
        var analyst = new StockAnalyzer();
        
        // Subscribe to events
        analyst.StartMonitoring(msftMonitor);
        
        // Simulate stock price changes
        msftMonitor.UpdateStockPrice(252.75m);
        msftMonitor.UpdateStockPrice(248.88m);
        msftMonitor.UpdateStockPrice(278.53m);
        
        // Unsubscribe from events
        analyst.StopMonitoring(msftMonitor);
        
        // This update won't trigger any notifications
        msftMonitor.UpdateStockPrice(280.00m);
    }
}
```

---

## 📊 Comparing Delegates and Events

### Similarities

🔹 Both are based on delegate types in C#.

🔹 Both enable loose coupling between components.

🔹 Both use the same subscription mechanism (+=, -=).

### Differences

| 🔷 Delegates | 🔶 Events |
|-------------|----------|
| Can be assigned, cleared, and invoked from any scope | Can only be invoked from within the declaring class |
| Can use the = operator to replace all handlers | Can only use += and -= operators |
| Full control over the invocation list | Restricted access to the invocation list |
| Best for callback mechanisms | Best for implementing the observer pattern |
| Like a public field | Like a field with public add/remove but private invoke |

---

## 🧩 Common Delegate Types in .NET

### Built-in Delegate Types

🔹 **Action<T1, T2, ...>**: Delegates that return void and take 0-16 parameters

```csharp
Action<string> logMessage = (msg) => Console.WriteLine(msg);
logMessage("Using built-in Action delegate");
```

🔹 **Func<T1, T2, ..., TResult>**: Delegates that return a value and take 0-16 parameters

```csharp
Func<int, int, string> formatSum = (a, b) => $"{a} + {b} = {a + b}";
Console.WriteLine(formatSum(10, 5));  // Outputs: "10 + 5 = 15"
```

🔹 **Predicate<T>**: Delegates that take one parameter and return a boolean

```csharp
Predicate<int> isEven = (num) => num % 2 == 0;
Console.WriteLine(isEven(4));  // Outputs: True
```

🔹 **EventHandler and EventHandler<TEventArgs>**: Standard event delegate types

```csharp
// Using the standard event pattern
public event EventHandler<CustomEventArgs> SomethingHappened;

// Raising the event
SomethingHappened?.Invoke(this, new CustomEventArgs { Data = "Some data" });
```

---

## 🚀 Real-World Case Study: Building a Temperature Monitoring System

Let's implement a temperature monitoring system that demonstrates the practical use of delegates and events.

### The System Components

1. `TemperatureSensor` - publishes temperature readings
2. `TemperatureMonitor` - subscribes to readings and triggers alerts
3. `Logger` - records all temperature changes
4. `AlertSystem` - handles critical temperature alerts

### Implementation

```csharp
// Event arguments class
public class TemperatureEventArgs : EventArgs
{
    public double Temperature { get; set; }
    public DateTime Timestamp { get; set; }
    public string SensorLocation { get; set; }
}

// Publisher class
public class TemperatureSensor
{
    // Event declaration
    public event EventHandler<TemperatureEventArgs> TemperatureChanged;
    
    private double _currentTemperature;
    private string _location;
    private Random _random;
    
    public TemperatureSensor(string location, double initialTemperature)
    {
        _location = location;
        _currentTemperature = initialTemperature;
        _random = new Random();
    }
    
    // Method to simulate temperature readings
    public void SimulateTemperatureChange()
    {
        // Simulate small temperature fluctuation
        double change = _random.NextDouble() * 2 - 1; // -1 to +1 degree change
        double newTemperature = Math.Round(_currentTemperature + change, 1);
        
        if (newTemperature != _currentTemperature)
        {
            _currentTemperature = newTemperature;
            
            // Raising the event
            OnTemperatureChanged(new TemperatureEventArgs
            {
                Temperature = _currentTemperature,
                Timestamp = DateTime.Now,
                SensorLocation = _location
            });
        }
    }
    
    protected virtual void OnTemperatureChanged(TemperatureEventArgs e)
    {
        TemperatureChanged?.Invoke(this, e);
    }
}

// First subscriber class
public class TemperatureMonitor
{
    private double _criticalHigh;
    private double _criticalLow;
    
    // Delegate for alert callbacks
    public delegate void TemperatureAlertCallback(string message, double temperature);
    
    // Delegate field to store alert callbacks
    private TemperatureAlertCallback _alertCallback;
    
    public TemperatureMonitor(double criticalLow, double criticalHigh)
    {
        _criticalLow = criticalLow;
        _criticalHigh = criticalHigh;
    }
    
    // Method to register an alert callback
    public void RegisterAlertCallback(TemperatureAlertCallback callback)
    {
        _alertCallback += callback;
    }
    
    // Event handler for temperature changes
    public void HandleTemperatureChange(object sender, TemperatureEventArgs e)
    {
        Console.WriteLine($"[MONITOR] {e.SensorLocation}: Current temperature is {e.Temperature}°C at {e.Timestamp.ToShortTimeString()}");
        
        // Check for critical temperatures
        if (e.Temperature > _criticalHigh)
        {
            _alertCallback?.Invoke("HIGH TEMPERATURE ALERT", e.Temperature);
        }
        else if (e.Temperature < _criticalLow)
        {
            _alertCallback?.Invoke("LOW TEMPERATURE ALERT", e.Temperature);
        }
    }
}

// Second subscriber class
public class Logger
{
    private string _logPath;
    
    public Logger(string logPath)
    {
        _logPath = logPath;
        // Initialize log file
        File.WriteAllText(_logPath, "Time,Location,Temperature\n");
    }
    
    // Event handler method
    public void LogTemperatureChange(object sender, TemperatureEventArgs e)
    {
        string logEntry = $"{e.Timestamp.ToString("yyyy-MM-dd HH:mm:ss")},{e.SensorLocation},{e.Temperature}\n";
        
        // Append to log file
        File.AppendAllText(_logPath, logEntry);
        
        Console.WriteLine($"[LOGGER] Temperature logged: {e.Temperature}°C");
    }
}

// Alert callback implementation
public class AlertSystem
{
    public void SendAlert(string alertType, double temperature)
    {
        Console.WriteLine($"⚠️ {alertType}: Temperature is {temperature}°C");
        
        // In a real system, this might send SMS, emails, etc.
        Console.WriteLine($"[ALERT SYSTEM] Alert notification sent to facility manager.");
    }
}

// Using the system
class Program
{
    static void Main(string[] args)
    {
        // Create the components
        var sensor = new TemperatureSensor("Server Room", 22.5);
        var logger = new Logger("temperature_log.csv");
        var monitor = new TemperatureMonitor(18.0, 27.0);
        var alertSystem = new AlertSystem();
        
        // Wire up the events and delegates
        sensor.TemperatureChanged += logger.LogTemperatureChange;
        sensor.TemperatureChanged += monitor.HandleTemperatureChange;
        monitor.RegisterAlertCallback(alertSystem.SendAlert);
        
        // Simulate temperature readings
        Console.WriteLine("Starting temperature monitoring...");
        
        for (int i = 0; i < 20; i++)
        {
            sensor.SimulateTemperatureChange();
            Thread.Sleep(1000); // Wait 1 second between readings
        }
        
        Console.WriteLine("Monitoring complete.");
    }
}
```

---

## 🔑 Key Takeaways

### When to Use Delegates

🔹 When you need callback functionality.

🔹 When you want to pass methods as parameters.

🔹 For implementing strategy pattern where algorithms can be selected at runtime.

🔹 When you need full control over the invocation list.

### When to Use Events

🔹 When implementing the observer/publish-subscribe pattern.

🔹 When a class needs to notify other classes about state changes.

🔹 When you want to restrict how notification mechanisms are used.

🔹 When following .NET design guidelines for component interaction.

---

## 📝 Best Practices

1. **Thread Safety**: Always use null-conditional operator when raising events: `MyEvent?.Invoke(...)`

2. **Event Arguments**: Derive from `EventArgs` for event data and follow the standard `sender, e` pattern.

3. **Naming Conventions**:
   - Delegate types: suffix with `Delegate` or describe the action (e.g., `CalculateOperation`)
   - Events: use verbs in present or past tense (e.g., `Click`, `Changed`, `Closing`, `Closed`)

4. **Unsubscribing**: Always unsubscribe from events when you're done to prevent memory leaks.

5. **Protected Virtual Method Pattern**: Use a protected `OnEventName` method to raise events.

---

## 🎓 Advanced Topics

### 1. Anonymous Methods and Lambda Expressions

```csharp
// Traditional delegate instantiation
Button1.Click += new EventHandler(Button1_Click);

// Anonymous method
Button1.Click += delegate(object sender, EventArgs e) 
{ 
    Console.WriteLine("Button clicked"); 
};

// Lambda expression
Button1.Click += (sender, e) => Console.WriteLine("Button clicked");
```

### 2. Async Event Handlers

```csharp
public async void Button1_Click(object sender, EventArgs e)
{
    await Task.Delay(1000);
    Console.WriteLine("Async operation completed");
}
```

### 3. Generic Delegates

```csharp
public delegate T Operation<T>(T a, T b);

// Usage
Operation<double> multiply = (x, y) => x * y;
double result = multiply(5.5, 2.0);
```

---

## 🔄 Conclusion

Delegates and events are cornerstone features of C# that enable flexible, loosely-coupled code. While they are related concepts built on the same underlying mechanism, they serve different purposes:

- **Delegates** provide a way to treat methods as first-class objects, enabling callbacks and pluggable algorithms.
- **Events** provide a specialized form of delegates that implement the publisher-subscriber pattern with additional encapsulation.

Mastering these concepts will help you write more flexible, maintainable C# code with clean separation of concerns.