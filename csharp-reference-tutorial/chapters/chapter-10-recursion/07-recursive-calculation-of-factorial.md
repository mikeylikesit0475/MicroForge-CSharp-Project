# Recursive Calculation of Factorial

The usage of recursion we will illustrate with a class ic example – recursive 
calculation of factorial. 
Factorial of n (written n!) is the product of all integers between 1 and n 
inclusive. By definition 0! = 1. 
n! = 1.2.3…n

## Recurrent Definition

When creating our solution, it is much more convenient to use the  
corresponding recurrent definition of factorial: 
n! = 1, for n = 0 
n! = n.(n-1)!, for n>0

## Finding a Recurrent Dependence

The presence of recurrent dependence is not always obvious. Sometimes we 
have to find it ourselves. In our case we can do this by analyzing the problem 
and calculating the values of the factorial for the first few integers. 
0! = 1 
1! = 1 = 1.1 = 1.0! 
2! = 2.1 = 2.1! 
3! = 3.2.1 = 3.2! 
4! = 4.3.2.1 = 4.3! 
5! = 5.4.3.2.1 = 5.4! 
From here you can easily see the recurrent dependability: 
n! = n.(n-1)!

## Algorithm Implementation

The bottom of our recursion is the simplest case n = 0, in which the value of 
the factorial is 1. 
In the other cases we solve the problem for n -1 and multiply the result by n. 
Thus after a certain count of steps we are definitely going to reach the bottom 

of the recursion, because between 0 and n there is a certain count of integer 
numbers. 
Once we have thes e substantial conditions we can write  a method, which  
computes factorial: 
```csharp
static decimal Factorial(int n) 
{ 
 // The bottom of the recursion 
 if (n == 0) 
 { 
  return 1; 
 } 
 // Recursive call: the method calls itself 
 else 
 { 
  return n * Factorial(n - 1); 
 } 
} 
By using this method we can create an application, which reads an integer 
```
from the console computes its factorial and then prints the obtained value: 
RecursiveFactorial.cs 
```csharp
using System; 
 
class RecursiveFactorial 
{ 
 static void Main() 
 { 
  Console.Write("n = "); 
  int n = int.Parse(Console.ReadLine()); 
 
  decimal factorial = Factorial(n); 
  Console.WriteLine("{0}! = {1}", n, factorial); 
 } 
 
 static decimal Factorial(int n) 
 { 
  // The bottom of the recursion 
  if (n == 0) 
  { 
   return 1; 
  } 
  // Recursive call: the method calls itself 

  else 
  { 
   return n * Factorial(n - 1); 
  } 
 } 
} 
Here is what the result of the execution of the application would be like if we 
enter 5 for n: 
n = 5 
5! = 120
```