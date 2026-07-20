# Extension Methods

In practice, programmers often have to add new functionality  to already 
existing code. If the code is available, we can simply add the required 
functionality and recompile. When a given assembly ( .exe or .dll file) has 
already been compiled, and the source code is not available, a common way 
to extend the functionality of the types is trough inheritance. This approach 
can be quite difficult to apply, due to the fact that we will have to change the 
instances of the base class with the instances of the derived one to be able to 
use our new functionality. Unfortunately, that is the least of our problems. If 
the type we want to inherit is marked with the keyword sealed, inheritance is 
not possible. 
Extension methods solve that v ery same problem – they present to us the 
opportunity to add new functionality to already existing type  (class or 
interface), without having to change its original code or use inheritance, i.e. 
also works fine with types that cannot be inherited. Notice th at trough 
extension methods we can add “implemented methods” even to interfaces. 
The extension methods  are defined as static in ordinary static classes. 
The type of their first argument is the class (or the interface) they extend. In 
front of it, we should  place the keyword  this. That is what makes them 
different from other static methods, and indicates the compiler that this is an 
extension method. The parameter with the keyword this in front of it can be 
used in the method body to create its functionality . Practically, it is the object 
that is used by the extension method. 

Extension methods can be applied directly to objects of the class/interface 
they extend. They can also be invoked statically through the static class they 
are defined in, but it is not a good practice. 
 
To refer to a specific extension method, we should add 
```csharp
“using” and the corresponding namespace, where the static 
class, describing this method, is defined. Otherwise the 
```
compiler has no way of knowing about their existence.

## Extension Methods – Examples

Let’s take for example the definition of an  extension method that counts 
the number of words in a given string. Have in mind, that the type string is 
sealed, so it cannot be inherited. 
```csharp
public static class StringExtensions 
{ 
 public static int WordCount(this string str) 
 { 
  return str.Split(new char[] { ' ', '.', '?', '!' }, 
   StringSplitOptions.RemoveEmptyEntries).Length; 
 } 
} 
The method WordCount(…) extends the class String. This is indicated by the 
```
keyword this before the type and the name of the first argument of the 
method (in our case str). The method itself is static and it is defined in the 
```csharp
static class StringExtensions. The usage of the extension method is done 
the same way as all the other methods of the class String. Do not forget to 
add the corresponding namespace, where the static class, describing the 
extension methods, is defined. Example of using an extension method: 
static void Main() 
{ 
 string helloString = "Hello, Extension Methods!"; 
 int wordCount = helloString.WordCount(); 
 Console.WriteLine(wordCount); 
} 
```
The method is invoked on the object helloString, which is of type string. It 
also takes the object as an argument and works with it (in our case refers to 
its Split(…) method and returns the number of elements of the array, 
produced by the Split(…) method).

## Extension Methods for Interfaces

Extension methods can not only be used on classes, but on interfaces as well. 
Our next example takes an instance of a class, that im plements the interface 
list of integers (IList<int>), and increases their value by a certain number. 
The method IncreaseWith(…) can access only those elements that are 
included in the interface IList (e.g. the property Count). 
```csharp
public static class IListExtensions 
{ 
 public static void IncreaseWith( 
  this IList<int> list,  int amount) 
 { 
  for (int i = 0; i < list.Count; i++) 
  { 
   list[i] += amount; 
  } 
 } 
} 
```
The extension methods also give us the opportunity to work on generic types. 
Let’s take for example a method that loops trough a collection, using 
foreach, implementing IEnumerable from generic type T. Its purpose is to 
convert to a meaningful string a sequence of elements (e.g. a list of integers): 
```csharp
public static class IEnumerableExtensions 
{ 
 public static string ToString<T>( 
```
  this IEnumerable<T> enumeration) 
```csharp
 { 
  StringBuilder result = new();
  result.Append("["); 
  foreach (var item in enumeration) 
  { 
   result.Append(item.ToString()); 
   result.Append(", "); 
  } 
  if (result.Length > 1) 
   result.Remove(result.Length - 2, 2); 
  result.Append("]"); 
  return result.ToString(); 
 } 
} 
```
Example of how to use the two extension methods declared above: 

```csharp
static void Main() 
{ 
 List<int> numbers = new List<int> { 1, 2, 3, 4, 5 }; 
 Console.WriteLine(numbers.ToString<int>()); 
 numbers.IncreaseWith(5); 
 Console.WriteLine(numbers.ToString<int>()); 
} 
```
The output of the execution of the program will be the following: 
[1, 2, 3, 4, 5] 
[6, 7, 8, 9, 10]