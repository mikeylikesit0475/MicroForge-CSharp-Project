# Iteration through Elements of an Array

As we can see, the iteration through the elements of an array is one of the 
most used techniques when we work with arrays. Consecutive iterating  
```csharp
using a loop will allow us to access each element through its index and we will 
```
be able to modify it as we want. We can do that with different loop constructs, 
but the most appropriate loop is the for-statement. We will examine in 
details how this type of iteration works.

## Iteration with a For Loop

It is a good practice to use for-loops, when we work with arrays and 
structures with indices. In the following example we will double the values of 
all elements of an array of numbers and we will print them: 
int[] array = new int[] { 1, 2, 3, 4, 5 }; 
 
```csharp
Console.Write("Output: "); 
for (int index = 0; index < array.Length; index++) 
{ 
 // Doubling the number 
 array[index] = 2 * array[index]; 
 // Print the number 
 Console.Write(array[index] + " "); 
} 
// Output: 2 4 6 8 10 
Using a for-loop we keep track of the current index of the array and we 
```
access the elements as needed. We do not have to iterate consecutively 
through all of them, which means the index that we are using in the for-loop 
may iterate through the elements in a way that our algorithm requires. For 
example we can iterate through some of the elements of the array, not 
through all of them: 
int[] array = new int[] { 1, 2, 3, 4, 5 }; 
 
```csharp
Console.Write("Output: "); 

for (int index = 0; index < array.Length; index += 2) 
{ 
 array[index] = array[index] * array[index]; 
 Console.Write(array[index] + " "); 
} 
// Output: 1 9 25 
```
In this example we are iterating through all elements at even positions and 
we square their values. 
Sometimes we want to iterate through the array in  a reverse order . We 
do that in a similar way, except that the for-loop will start with the index of 
the last element and the index will decrease on each step until its value gets 
to 0 (inclusive). Here is an example: 
int[] array = new int[] { 1, 2, 3, 4, 5 }; 
 
```csharp
Console.Write("Reversed: "); 
for (int index = array.Length - 1; index >= 0; index--) 
{ 
 Console.Write(array[index] + " "); 
} 
// Reversed: 5 4 3 2 1 
```
In this example we are iterating through the array in reverse order and we 
print each element to the console.

## Iteration with "foreach" Loop

One of the most used constructs for iterating through elements of an array is 
foreach. The foreach-loop construct in C# is as follows: 
foreach (var item in collection) 
```csharp
{ 
 // Process the value here 
} 
In this programming construct var is the type of the elements, which we 
```
iterate through. The collection is the array (or any other collection of 
elements) and item is the current element of the array on each step. 
In general the foreach loop construct has the same properties like the for-
loop. The main difference is that the iteration is made always through all 
elements – from the start to the end. We cannot access the current index, 
we are just iterating through the collection in a way, defined by the collection 
itself. For arrays the order of iteration is consecutive from the first element to 

the last one. The loop var iable in foreach-loops is read-only so we cannot 
modify the current loop item from the loop body. 
The foreach-loop statement is used, when we do not need to change the 
elements, but just to read them.

### Iteration with "foreach" Loop – Example

In the next exa mple we will learn how to use the foreach loop to iterate 
through the array: 
string[] capitals = 
 { "Sofia", "Washington", "London", "Paris" }; 
 
foreach (string capital in capitals) 
```csharp
{ 
 Console.WriteLine(capital); 
} 
```
After we declared an array of strings capitals, we iterate through the array 
```csharp
using foreach loop and we print the elements to the console. The current 
element on each step is stored in a variable capital. We get the following 
```
result when we execute the code: 
Sofia 
Washington 
London 
Paris