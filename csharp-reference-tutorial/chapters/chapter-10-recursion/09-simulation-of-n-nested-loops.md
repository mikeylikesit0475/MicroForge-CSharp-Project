# Simulation of N Nested Loops

Very often we have to write nested loops. It is very easy when they are two, 
three or any number previously assigned. However, if their count is not 
known in advance, we have to think of an alternative a pproach. This is the 
case with the following task. 
Write a program that simulates the execution of N nested loops from 1 to K, 
where N and K are entered by the user. The result of the performance of the 
program should be equivalent to the execution of following fragment: 
for (a1 = 1; a1 <= K; a1++) 
 for (a2 = 1; a2 <= K; a2++) 
  for (a3 = 1; a3 <= K; a3++) 
   … 
   for (aN = 1; aN <= K; aN++) 
```csharp
    Console.WriteLine("{0} {1} {2} … {N}", 
     a1, a2, a3, …, aN); 
For example, when N = 2 and K = 3 (which is equivalent to 2 nested loops 
from 1 to 3) and when N = 3 and K = 3, the results would be as follows: 
```
      1 1        1 1 1 
      1 2        1 1 2 
       1 3        1 1 3 
 N = 2   2 1   N = 3   1 2 1 
 K = 3 ->  2 2   K = 3 ->  … 
      2 3        3 2 3 
      3 1        3 3 1 
      3 2        3 3 2 
      3 3        3 3 3 
The algorithm for solving this problem is not as obvious as in the previous 
example. Let ’s consider two different solutions – one recursive, and one 
iterative. 
Each row of the result can be regarded as ordered sequence of N numbers. 
The first one represents the current value of the counter of the loop, the 
second one – of the second loop, etc. On each position we can have value 
between 1 and K. The solution of our task boils down to finding a ll ordered 
sequences of N elements for N and K given.

## Nested Loops – Recursive Version

If we are looking for a recursive solution to the problem, the first problem we 
are going to face is finding a recurrent dependence. Let ’s look more carefully 
at the example from the assignment and put some further consideration. 

Notice that, if we have calculated the answer for N = 2, then the answer for N 
= 3 can be obtained if we put on the first position each of the values of K (in 
this case from 1 to 3), and on the o ther two positions we put each of the 
couples of numbers, produced for N = 2. We can check that this rule applies 
for numbers greater than 3. 
 
This way we have obtained the following dependence – starting from the first 
position, we put on the current pos ition each of the values from 1 to K and 
continue recursively with the next position. This goes on until we reach 
position N, after which we print the obtained result  (bottom of the 
recursion). Here is how the method looks implemented in C#: 
```csharp
static void NestedLoops(int currentLoop) 
{ 
 if (currentLoop == numberOfLoops) 
 { 
  PrintLoops(); 
  return; 
 } 
 
 for (int counter=1; counter<=numberOfIterations; counter++) 
 { 
  loops[currentLoop] = counter; 
  NestedLoops(currentLoop + 1); 
 } 
} 
```
We are going to keep the sequence of values in an array called loops, which 
would be printed on the console by the method PrintLoops() when needed. 
The method NestedLoops(…) takes one parameter, indicating the position in 
which we are going to place values. 
Solution for N = 3
1 x x
Solution for N = 2
2 x x
Solution for N = 2
3 x x
Solution for N = 2
Solution for N = 2
1 x
Solution for N = 1
2 x
Solution for N = 1
3 x
Solution for N = 1
Solution for N = 1
1
2
3

In the loop we place consecutively on the current position each of the possible 
values (the variable numberOfIterations contains the value of K entered by 
the user), after which we call recursively the method NestedLoops(…) for the 
next position. 
The bottom of the recursion is reached when the current position becomes N 
(the variable numberOfIterations contains the value of N, entered by the 
user). In this moment we have values on all positions and we print the 
sequence. 
Here is a complete implementation of the recursive nested loops solution: 
RecursiveNestedLoops.cs 
```csharp
using System; 
 
class RecursiveNestedLoops 
{ 
 static int numberOfLoops; 
 static int numberOfIterations; 
 static int[] loops; 
 
 static void Main() 
 { 
  Console.Write("N = "); 
  numberOfLoops = int.Parse(Console.ReadLine()); 
 
  Console.Write("K = "); 
  numberOfIterations = int.Parse(Console.ReadLine()); 
 
  loops = new int[numberOfLoops]; 
 
  NestedLoops(0); 
 } 
 
 static void NestedLoops(int currentLoop) 
 { 
  if (currentLoop == numberOfLoops) 
  { 
   PrintLoops(); 
   return; 
  } 
 
  for (int counter=1; counter<=numberOfIterations; counter++) 
  { 
   loops[currentLoop] = counter; 

   NestedLoops(currentLoop + 1); 
  } 
 } 
 
 static void PrintLoops() 
 { 
  for (int i = 0; i < numberOfLoops; i++) 
  { 
   Console.Write("{0} ", loops[i]); 
  } 
  Console.WriteLine(); 
 } 
} 
If we run the application and enter for N and K respectively 2 and 4 as 
```
follows, we are going to obtain the following result: 
N = 2 
K = 4 
1 1 
1 2 
1 3 
1 4 
2 1 
2 2 
2 3 
2 4 
3 1 
3 2 
3 3 
3 4 
4 1 
4 2 
4 3 
4 4  
In the Main() method we enter values for N and K, create an array in which 
we are going to keep the sequence of values, after which we call the method 
NestedLoops(…), starting from the first position. 
Notice that as a parameter of the array we give 0 because we keep the 
sequence of values in an array, and as we already know, counting of array 
elements starts from 0. 
The method PrintLoops() iterates all elements of the array and prints them 
on the console.

## Nested Loops – Iterative Version

For the implementation of an iterative solution of the nested loops we can 
use the following algorithm, which  finds the next sequence of numbers and 
prints it at each iteration: 
1. In the beginning on each position place the number 1. 
2. Print the current sequence of numbers. 
3. Increment with 1 the number on position N. If the obtained value is 
greater than K replace it with 1 and increment with 1 the value on 
position N – 1. If its value has become greater then K, too, replace it 
with 1 and increment with 1 the value on position N – 2, etc. 
4. If the value on the first position has become greater than K, the 
algorithm ends its work. 
5. Go on with step 2. 
Below we propose  a straightforward implementation  of the described 
iterative nested loops algorithm: 
IterativeNestedLoops.cs 
```csharp
using System; 
 
class IterativeNestedLoops 
{ 
 static int numberOfLoops; 
 static int numberOfIterations; 
 static int[] loops; 
 
 static void Main() 
 { 
  Console.Write("N = "); 
  numberOfLoops = int.Parse(Console.ReadLine()); 
 
  Console.Write("K = "); 
  numberOfIterations = int.Parse(Console.ReadLine()); 
 
  loops = new int[numberOfLoops]; 
 
  NestedLoops(); 
 } 
 
 static void NestedLoops() 
 { 
  InitLoops(); 

 
  int currentPosition; 
 
  while (true) 
  { 
   PrintLoops(); 
 
   currentPosition = numberOfLoops - 1; 
   loops[currentPosition] = loops[currentPosition] + 1; 
 
   while (loops[currentPosition] > numberOfIterations) 
   { 
    loops[currentPosition] = 1; 
    currentPosition--; 
 
    if (currentPosition < 0) 
    { 
     return; 
    } 
    loops[currentPosition] = loops[currentPosition] + 1; 
   } 
  } 
 } 
 
 static void InitLoops() 
 { 
  for (int i = 0; i < numberOfLoops; i++) 
  { 
   loops[i] = 1; 
  } 
 } 
 
 static void PrintLoops() 
 { 
  for (int i = 0; i < numberOfLoops; i++) 
  { 
   Console.Write("{0} ", loops[i]); 
  } 
  Console.WriteLine(); 
 } 
} 
```
The methods Main() and PrintLoops() are the same  as in the 
implementation of the recursive solution. 

The NestedLoops() method is different. It now implements the algorithm for 
iterative solution of the problem and for this reason does not get any 
parameters, unlike in the recursive version. 
In the very beginning of this method we call the method InitLoops(), which 
iterates the elements of the array and places in each position 1. 
The steps of the algorithm we perform in an infinite loop, from which we are 
going to escape in an appropriate moment  by ending the execution of the 
methods via the operator return. 
The way we implement step 3 of the algorithm is very interesting. The 
verification of the values greater than K, their substitution with 1 and the 
incrementing with 1 the value on the previous position (after which we make 
the same verification for it too) we implement by using one while loop, which 
we enter only if the value is greater than K. 
For this purpose we first replace the value of the current position with 1. After 
that the position before it becomes current. Next we increment the value on 
the new position with 1 and  go back to the beginning of the loop. These 
actions continue until the value on the current position is not less than or 
equal to K (the variable numberOfIterations contains the value of K ), which 
is when we escape the loop. 
When the value on the first po sition becomes greater than K (this is the 
moment when we have to end the execution), on its place we put 1 and try to 
increment the value on the previous position. In this moment the value of the 
variable currentPosition becomes negative (as the first position of the array 
is 0) and we end the execution of the method using the operator return. This 
is the end of our task. 
We can now test it whit N = 3 and K = 2, for example: 
N = 3 
K = 2 
1 1 1 
1 1 2 
1 2 1 
1 2 2 
2 1 1 
2 1 2 
2 2 1 
2 2 2