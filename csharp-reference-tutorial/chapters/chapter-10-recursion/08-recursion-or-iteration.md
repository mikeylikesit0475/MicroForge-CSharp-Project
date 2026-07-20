# Recursion or Iteration?

Recursion or Iteration? 
The calculation of factorial is often given as an example when explaining the 
concept of recursion, but in this case, as in many others, recursion is not the 
best approach. 
Very often, if we are given a recurrent definition of the problem, the 
recurrent solution is intuitive and not posing any difficulty, while iterative 
(consecutive) solution is not always obvious. 
In this particular case the implementation of the iterative solution is as short 
and simple, but is a bit more efficient: 
```csharp
static decimal Factorial(int n) 
{ 
 decimal result = 1; 
 
 for (int i = 1; i <= n; i++) 
 { 
  result = result * i; 
 } 
 
 return result; 
} 
We are going to consider the advantages and disadvantages of using 
```
recursion and iteration later in this chapter. 
For the moment we should remember that before proceeding with recursive 
implementation we should think about an iterative variant, after which we 
should choose the better solution according to the situation. 
Let’s look at another example where we could use recursion to solve the 
problem. This time we are going to consider an iterative solution, too.