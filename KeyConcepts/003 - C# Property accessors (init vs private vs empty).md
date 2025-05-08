
# Understanding Property Accessors in C#

This guide explains the differences between three common ways of defining read-only or partially settable properties in C#, including how they behave in **classes**, **records**, and **structs**.

---

## 🔹 `public string Name { get; private set; }`

- **Description**:  
  A public getter with a private setter.

- **Behavior**:
  - Readable from outside the class.
  - Writable only from within the class or struct.

- **Use Case**:  
  Useful when a property should only be set or changed internally.

- **Example**:
  ```csharp
  public class Person
  {
      public string Name { get; private set; }

      public Person(string name)
      {
          Name = name;
      }

      public void Rename(string newName)
      {
          Name = newName;
      }
  }

  var p = new Person("Alice");
  p.Name = "Bob"; // ❌ Error: private setter
  ```

---

## 🔹 `public string Name { get; init; }`

- **Description**:  
  Introduced in **C# 9**, the `init` accessor allows setting the property only during initialization.

- **Behavior**:
  - Settable only in the constructor or using an object initializer.
  - Immutable after object creation.

- **Use Case**:  
  Ideal for **immutable types**, such as DTOs, configurations, and models.

- **Example with Class**:
  ```csharp
  public class Person
  {
      public string Name { get; init; }
  }

  var p = new Person { Name = "Alice" }; // ✅ OK
  p.Name = "Bob"; // ❌ Not allowed after creation
  ```

- **Example with Record**:
  ```csharp
  public record Person(string Name);

  var p1 = new Person("Alice");
  var p2 = p1 with { Name = "Bob" }; // ✅ Records allow with-expressions
  ```

- **Example with Struct**:
  ```csharp
  public struct Coordinates
  {
      public double X { get; init; }
      public double Y { get; init; }
  }

  var point = new Coordinates { X = 10, Y = 20 }; // ✅ OK
  point.X = 30; // ❌ Error
  ```

> ⚠️ In **structs**, you must assign all `init`-only properties during construction or initialization.

---

## 🔹 `public string Name { get; }`

- **Description**:  
  A truly read-only property with no setter. The value must be set via constructor or an inline initializer.

- **Behavior**:
  - Cannot be changed after construction.

- **Use Case**:  
  Useful for enforcing full immutability.

- **Example with Class**:
  ```csharp
  public class Person
  {
      public string Name { get; }

      public Person(string name)
      {
          Name = name;
      }
  }
  ```

- **Example with Struct**:
  ```csharp
  public struct Point
  {
      public double X { get; }
      public double Y { get; }

      public Point(double x, double y)
      {
          X = x;
          Y = y;
      }
  }
  ```

- **Example with Record**:
  ```csharp
  public record Book
  {
      public string Title { get; } = "Unknown";
  }
  ```

---

## 🧱 Records and Accessors

Records are **immutable by default**, making them a natural fit for `init` and `get` accessors.

- Primary constructor syntax:
  ```csharp
  public record Person(string Name, int Age);
  ```

- Equivalent to:
  ```csharp
  public record Person
  {
      public string Name { get; init; }
      public int Age { get; init; }
  }
  ```

You can also customize accessors:
```csharp
public record User
{
    public string Username { get; init; }
    public string Password { get; private set; } // Only modifiable internally
}
```

---

## 🔍 Summary Table

| Syntax                          | Class | Struct | Record | Settable After Creation? | Notes                                  |
|--------------------------------|-------|--------|--------|---------------------------|----------------------------------------|
| `get; private set;`            | ✅    | ✅     | ✅     | ✅ (internally)           | Mutable from within the type          |
| `get; init;`                   | ✅    | ✅     | ✅     | ❌                        | Immutable after object creation       |
| `get;`                         | ✅    | ✅     | ✅     | ❌                        | Must be initialized via constructor   |

---

## ✅ Recommendations

- Use `init` when working with immutable models and want cleaner syntax for initialization.
- Use `get;` when you want strict immutability and complete protection after construction.
- Use `get; private set;` when controlled mutation is needed inside the class only.
