# Foreach Loops

Foreach Loops 
The foreach loop (extended for-loop) is new for the C/C++/C# family of 
languages, but is well known for the VB and PHP programmers. This 
programming construct serves to iterate over all elements  of an array, list 
or other collection of elements  (IEnumerable). It passes t hrough all the 
elements of the specified collection even if the collection is not indexed. 

We will discuss arrays in more details in chapter "Arrays", but for now we can 
imagine one array as an ordered sequence of numbers or other elements. 
Here is how a foreach loop looks like: 
foreach (type variable in collection) 
```csharp
{ 
 statements; 
} 
As we see, it is significantly simpler than the standard for-loop and 
```
therefore is very often preferred by developers because it saves writing when 
you need to go through all the elements of a given collection. Here is an 
example that shows how we can use foreach: 
int[] numbers = { 2, 3, 5, 7, 11, 13, 17, 19 }; 
foreach (int i in numbers) 
```csharp
{ 
 Console.Write(" " + i); 
} 
Console.WriteLine(); 
string[] towns = { "London", "Paris", "Milan", "New York" }; 
foreach (string town in towns) 
{ 
 Console.Write(" " + town); 
} 
```
In the example we create an array of numbers, which are after that went 
through with a foreach loop, and its elements are printed on the console. 
Then an array of city names (strings) is created and in the same way it is 
went through and its elements are printed on the console. The result of the 
example is: 
2 3 5 7 11 13 17 19 
London Paris Milan New York