## Memory Management in C#

### Memory Basics in C#

C# manages memory in two main regions:
1. **Stack**: Fast, limited memory for local variables and method calls
2. **Heap**: Larger, more flexible memory for dynamic allocation

### Value Types vs Reference Types

#### Value Types
- Stored directly on the stack
- Include: `int`, `float`, `double`, `bool`, `char`, `struct`, `enum`
- When you assign a value type, you're copying the actual data
- Memory is automatically allocated/deallocated when variables go in/out of scope

```csharp
int x = 10;
int y = x;  // y gets a copy of the value 10
x = 20;     // Only x changes, y remains 10
```

#### Reference Types
- Stack contains a reference (pointer) to data stored on the heap
- Include: `class`, `interface`, `delegate`, `string`, `array`, `object`
- When you assign a reference type, you're copying the reference, not the data
- Memory is managed by the garbage collector

```csharp
class Person { public string Name; }

Person p1 = new Person() { Name = "Alice" };
Person p2 = p1;  // p2 references the same object as p1
p1.Name = "Bob"; // Both p1.Name and p2.Name are now "Bob"
```

### Key Differences in Practice

1. **Memory Allocation**:
   - Value types: Fixed size, immediately allocated/deallocated
   - Reference types: Variable size, garbage collected when no longer referenced

2. **Assignment Behavior**:
   - Value types: Creates a new independent copy (deep copy)
   - Reference types: Creates a new reference to the same object (shallow copy)

3. **Equality Comparison**:
   - Value types: Compares actual data values
   - Reference types: By default compares reference (are they the same object?)

4. **Parameter Passing**:
   - Value types: Passed by value (copy)
   - Reference types: Reference is passed by value (copy of reference)

5. **Nullable Behavior**:
   - Value types: Can't be null by default (need `int?` syntax)
   - Reference types: Can be null

## Performance: Structs vs Classes

When choosing between `struct` (value type) and `class` (reference type), performance considerations become important:

```csharp
// As a struct (value type)
struct Point
{
    public int X;
    public int Y;
}

// As a class (reference type)
class PointClass
{
    public int X;
    public int Y;
}
```

### Performance Implications:

1. **Small, Immutable Data** (e.g., 3D vectors, coordinates):
   - Structs are faster because:
     - No heap allocation
     - No garbage collection overhead
     - Better CPU cache locality
   - Benchmark example: Creating 1 million points as structs is typically 2-3x faster than classes

2. **Large Objects** (>16 bytes):
   - Classes may perform better because:
     - Copying large structs is expensive
     - Passing large structs across method boundaries creates copies

3. **High-Frequency Operations**:
   - Value types avoid memory indirection (pointer dereferencing)
   - Example: Physics engines often use struct vectors for calculations performed millions of times per frame

## Boxing and Unboxing

Boxing occurs when a value type is converted to a reference type (object). Unboxing is the reverse.

```csharp
int number = 42;       // Value type on stack
object boxed = number; // Boxing: creates heap allocation, copies value
int unboxed = (int)boxed; // Unboxing: copies value back to stack
```

### Performance Impact:

Boxing/unboxing is extremely expensive because it:
1. Allocates heap memory
2. Performs type checking
3. Triggers garbage collection
4. Requires CPU instruction pipeline flushes

Common scenarios that cause hidden boxing:
```csharp
// Hidden boxing
List<object> items = new List<object>();
items.Add(42); // Boxing occurs here

// Using non-generic collections
ArrayList list = new ArrayList();
list.Add(42); // Boxing occurs here

// String formatting
string s = string.Format("Value: {0}", 42); // Boxing occurs here
```

## Garbage Collector Internals

The Garbage Collector (GC) manages reference types on the heap:

1. **Generational Design**:
   - Gen 0: New objects (frequent collections)
   - Gen 1: Objects surviving Gen 0 collection
   - Gen 2: Long-lived objects (infrequent collections)

2. **Collection Process**:
   - Mark: Identifies all reachable objects
   - Sweep: Reclaims memory from unreachable objects
   - Compact: Defragments memory (reduces fragmentation)

3. **GC Modes**:
   - Workstation GC: Optimized for responsiveness
   - Server GC: Optimized for throughput
   - Background GC: Reduces pause times

Impact on reference types:
```csharp
void ProcessLargeData()
{
    // Creates many temporary objects
    for (int i = 0; i < 1000000; i++)
    {
        string text = "Item " + i; // Each iteration creates new strings
        ProcessText(text);
    } 
    // GC must clean up ~2M objects (original strings + concatenated results)
}
```

## Memory Usage Patterns

### Large Object Heap (LOH)
- Objects >85KB go to LOH
- Not compacted by default
- Can cause fragmentation
- Critical for large arrays, bitmaps, strings

### Memory Pressure
High memory turnover can degrade performance:

```csharp
// Poor pattern: creates memory pressure
byte[] buffer = new byte[1024 * 1024];
for (int i = 0; i < 1000; i++)
{
    buffer = new byte[1024 * 1024]; // Discards previous buffer
    ProcessBuffer(buffer);
}

// Better pattern: reuses memory
byte[] buffer = new byte[1024 * 1024];
for (int i = 0; i < 1000; i++)
{
    ResetBuffer(buffer);
    ProcessBuffer(buffer);
}
```

### Memory Leaks in C#
Despite automatic memory management, leaks still occur:

1. **Event Handlers**: Forgetting to unsubscribe
2. **Static Collections**: Growing without bounds
3. **Cached Objects**: Never released from memory
4. **Disposable Resources**: Not calling Dispose()

## Best Practices

1. **Use Value Types When**:
   - Object is small (<16 bytes)
   - Object is immutable or rarely modified
   - Object has short lifetime
   - Object is frequently accessed in performance-critical code

2. **Use Reference Types When**:
   - Object is large
   - Object needs identity (two instances can be the same entity)
   - Object requires polymorphism
   - Object is shared across methods

3. **For Memory-Intensive Operations**:
   - Use object pooling for frequent allocations
   - Consider `Span<T>` and `Memory<T>` for working with memory regions
   - Implement `IDisposable` for deterministic cleanup
   - Use `StringBuilder` instead of string concatenation
   - Consider structs for collections of primitive types
