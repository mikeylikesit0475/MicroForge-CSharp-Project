# Reading an Array from the Console

Let’s see how we can read values of an array from the console. We will use a 
for-loop and the .NET Framework tools for reading from the console. 
Initially we read a line from the console using Console.ReadLine(), and then 
we parse that line to an integer number using int.Parse() and we set it to 
the variable n. We then use the number n as length of the array. 
int n = int.Parse(Console.ReadLine()); 
int[] array = new int[n]; 

Again we use a loop to iterate through the array. At each iteration we set the 
current element to what we have read from the console. The loop will 
continue n times, which means it will iterate through the array and so we will 
read a value for each element of the array: 
for (int i = 0; i < n; i++) 
```csharp
{ 
 array[i] = int.Parse(Console.ReadLine()); 
}
```

## Check for Symmetric Array – Example

An array is symmetric if the fi rst and the last elements are equal and at the 
same time the second element and the last but one are equal as well and so 
on. On the figure a few examples for symmetric arrays are shown: 
 
In the next example we will check whether an array is symmetric: 
```csharp
Console.Write("Enter a positive integer: "); 
int n = int.Parse(Console.ReadLine()); 
int[] array = new int[n]; 
 
Console.WriteLine("Enter the values of the array:"); 
 
for (int i = 0; i < n; i++) 
{ 
 array[i] = int.Parse(Console.ReadLine()); 
} 
 
bool symmetric = true; 
for (int i = 0; i < array.Length / 2; i++) 
{ 
 if (array[i] != array[n - i - 1]) 
 { 
  symmetric = false; 
  break; 
 } 
} 
 
Console.WriteLine("Is symmetric? {0}", symmetric); 
```
12321 123211221 1221 32 3 121 32 3 121

We initialize an array and we read its elements from the console. We need to 
iterate through half of the array to check whether it is symmetric. The middle 
element of the array has an index array.Length / 2. If the length is an odd 
number this index is exa ctly the middle one, but if it is an even number, the 
index is to the right of the middle (the middle is between two elements).  Thus 
the loop runs from 0 to array.Length / 2 (non-inclusive). 
To check whether an array is symmetric, we use a bool variable, and initially 
assume that the array is symmetric. During the iteration through the array we 
compare the first with the last element, the second with the last but one and 
so on. If at some point the compared elements are not equal, then we set the 
bool variable to false, which means the array is not symmetric. 
In the end we print the value of the bool variable to the console.