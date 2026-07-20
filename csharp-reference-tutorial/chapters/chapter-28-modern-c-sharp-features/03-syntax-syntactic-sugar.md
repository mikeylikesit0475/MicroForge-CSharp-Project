# Modern Syntax and Syntactic Sugar

Modern C# eliminates boilerplate code and introduces powerful functional programming concepts.

## 1. Top-Level Statements (C# 9)
Instead of wrapping every entrypoint in a class and Main method:
```csharp
using System;
namespace IntroToCSharp
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello C#!");
        }
    }
}
```
You can simply write:
```csharp
using System;
Console.WriteLine("Hello C#!");
```

## 2. Target-Typed New (C# 9)
When the type is declared on the left-hand side, you can omit it on the right:
```csharp
// Old
List<int> numbers = new();

// Modern
List<int> numbers = new();
```

## 3. Collection Expressions (C# 12)
You can initialize arrays and lists using square brackets `[]`:
```csharp
int[] array = [1, 2, 3];
List<string> list = ["Alice", "Bob"];
```

## 4. Records (C# 9)
Records are lightweight, immutable data structures designed for data transfer objects (DTOs):
```csharp
public record Person(string FirstName, string LastName);
```

## 5. Pattern Matching (C# 7-9)
Pattern matching simplifies conditional statements and type checking:
```csharp
if (shape is Circle c)
{
    Console.WriteLine($"Radius: {c.Radius}");
}

// Switch expressions
string message = errorCode switch
{
    404 => "Not Found",
    500 => "Server Error",
    _ => "Unknown Error"
};
```
