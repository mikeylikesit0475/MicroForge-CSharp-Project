# Printing an Array to the Console

Printing an Array to the Console 
Often we have to print the elements of a given array  to the console, after 
we have finished working with it. 
We print elements of an array to the console similarly to the initializing of the 
elements, i.e. by using a loop to iterate through the array. There are no strict 
rules for printing, but often some sort of suitable formatting is used. 
A frequent mistake is an attempt to print an array like a number: 
string[] array = { "one", "two", "three", "four" }; 
```csharp
Console.WriteLine(array); 
```
Unfortunately this code does not print the elements of an array , just its 
type. Here is what we get after the execution of this code: 
 
We print the elements of an array by hand, by using a for-loop: 
string[] array = { "one", "two", "three", "four" }; 
 
for (int index = 0; index < array.Length; index++) 
```csharp
{ 
 // Print each element on a separate line 
 Console.WriteLine("Element[{0}] = {1}", index, array[index]); 
} 


We are iterating through the array using the for-loop, which will go 
array.Length times, and we will print the current element using 
Console.WriteLine() and a formatted string. Here is the result: 
Element[0] = one 
Element[1] = two 
Element[2] = three 
Element[3] = four
```