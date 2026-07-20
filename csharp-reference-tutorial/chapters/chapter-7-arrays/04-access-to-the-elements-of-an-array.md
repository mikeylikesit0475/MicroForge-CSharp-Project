# Access to the Elements of an Array

We access the array elements directly using their indices. Each element can 
be accessed through the name of the array and the element’s index 
(consecutive number) placed in t he brackets. We can access given elements 
of the array both for reading and for writing, which means we can treat 
elements as variables. 
Here is an example for accessing an element of an array: 
myArray[index] = 100; 
In the example above we set a value of 100 to the element, which is at 
position index. 
HeapStack
daysOfWeek
…
…
…
…
…
…
…
Monday
Tuesday
Wednesday
Thursday
Friday
Saturday
Sunday
0
1
2
3
4
5
6

Here is an example, where we allocate an array of numbers and then we 
change some of them: 
int[] myArray = new int[6]; 
myArray[1] = 1; 
myArray[5] = 5; 
After the change, the array is allocated in the memory as shown below: 
 
As we can see, all elements, except those for which values are explicitly set, 
are initialized with the value 0 when the memory of the array was allocated. 
We can iterate through the array using a loop statement. The most common 
form of such iteration is by using a for-loop: 
int[] arr = new int[5]; 
for (int i = 0; i < arr.Length; i++) 
```csharp
{ 
 arr[i] = i; 
}
```

## Going Out of Bounds of the Array

The .NET Framework does an automatic check  on each element access 
attempt, whether the index is valid or it is out of the range  of the array. 
When we try to access an invalid (not existing) element in an array, a 
System.IndexOutOfRangeException is thrown. The automatic check really 
helps the devel opers find errors while working with arrays. Of course, 
checking for exceptions has its price. Checks affect the performance, but 
that’s nothing compared to avoiding errors like "out of range", "access to 
unallocated memory", etc. 
Here is an example, where we are trying to access an element, which is out of 
the range of the array: 
IndexOutOfRangeExample.cs 
```csharp
class IndexOutOfRangeExample 
{ 
```
HeapStack
myArray
.
.
.
0 1 0 0 0 5
0 1 2 3 4 5

```csharp
 static void Main() 
 { 
  int[] myArray = { 1, 2, 3, 4, 5, 6 }; 
  Console.WriteLine(myArray[6]); 
 } 
} 
```
In the example above we allocate an array, which contains six integer 
numbers. The first index is 0, and the last index is 5. We are trying to print to 
the console an element with index 6, but because there is no such element 
this leads to an exception:

## Reversing an Array – Example

In the next example we will access elements and change them using their 
indices. The task is to print the elements in reversed order. We will reverse 
the elements of the array using a second, auxiliary array, where we will keep 
the elements of the first one, but in a reversed order. Note that the length of 
both arrays is the same and it stays unchanged after the first allocation: 
ArrayReverseExample.cs 
```csharp
class ArrayReverseExample 
{ 
 static void Main() 
 { 
  int[] array = { 1, 2, 3, 4, 5 }; 
  // Get array size 
  int length = array.Length; 
  // Declare and create the reversed array 
  int[] reversed = new int[length]; 
 
  // Initialize the reversed array 
  for (int index = 0; index < length; index++) 
  { 
   reversed[length - index - 1] = array[index]; 
  } 


 
  // Print the reversed array 
  for (int index = 0; index < length; index++) 
  { 
   Console.Write(reversed[index] + " "); 
  } 
 } 
} 
// Output: 5 4 3 2 1 
```
The example works in the following way: initially we allocate a one -
dimensional array of type int and we initialize it with the numbers from 1 to 
5. After that we keep the length of the array in the variable length. Note that 
we are using the property Length, which returns the total count of the 
elements of the array. In C# each array has a length property. 
After that we declare the array reversed with the same length, where we 
will keep elements of the original array, but in a reversed order. 
To reverse the elements we use a for-loop. At each iteration  we increment 
the index variable by one and we make sure we access all consecutive 
elements of the array. The loop condition ensures that the array will be 
iterated from end to end. 
Let’s follow what happens when we iterate through the array. On the first 
iteration, index has a value of 0. Using array[index] we access the first 
element of the array, and respectively with reversed[length - index - 1] 
we access the last element of the new array reversed where we assign the 
values. Thus, we appropriated the value of the first element of the array to 
the last element of the reversed array. At each iteration  index is 
incremented by one. This way, we access the next element in the order of 
array and the previous element in the order of reversed. 
As a result we reversed the array and printed it. In the ex ample we showed 
consecutive iterations through the array, which can also be done with 
different types of loop constructs (e.g. while and foreach).