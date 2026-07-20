# Example of Recursion

Example of Recursion 
Let’s consider the Fibonacci numbers . These are the elements of the 
following sequence: 
1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, … 
Each element of the sequence  is formed by the sum of the previous two 
elements. The first two elements are equal to 1 by definition, i.e. the next two 
rules apply: 
F1 = F2 = 1 
Fi = Fi-1 + Fi-2 (for i > 2) 
Proceeding directly from the definition, we can implement the following 
recursive method for finding the nth Fibonacci number: 
```csharp
static long Fib(int n) 
{ 
 if (n <= 2) 
 { 

  return 1; 
 } 
 return Fib(n - 1) + Fib(n - 2); 
} 
```
This example shows how simple and natural the implementation of a solution 
can be when using recursion. 
On the other hand, it can serve as an example of how attentive we have to be 
while programming with recursion. Although it is intuitive, the present 
solution is one of the classical examples when the usage of recursion is 
highly inefficient as there are many excessive calculations (of one and the 
same element of the sequence) due to the recursive calls. 
We are going to consider the advantages and the disadvantages of using 
recursion later in this chapter.