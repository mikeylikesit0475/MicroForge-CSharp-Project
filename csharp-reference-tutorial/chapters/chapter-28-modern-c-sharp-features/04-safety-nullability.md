# Safety and Nullability

One of the largest improvements in modern C# is how the compiler handles the "billion-dollar mistake"—the `null` reference.

## Nullable Reference Types (C# 8)
In legacy C#, all reference types (like strings and objects) could be null, leading to frequent `NullReferenceException` errors. In modern C#, reference types are non-nullable by default. You must explicitly declare a type as nullable using a `?`:

```csharp
string name = "John"; // Cannot be null
name = null;          // Compiler Warning!

string? nullableName = null; // OK
```

The compiler will now warn you if you attempt to access members of `nullableName` without checking for null first.

## Null-Coalescing Assignment (C# 8)
A simpler syntax to assign a value only if the variable is null:
```csharp
List<int> numbers = null;
numbers ??= new(); // initialized to new list because it was null
```
