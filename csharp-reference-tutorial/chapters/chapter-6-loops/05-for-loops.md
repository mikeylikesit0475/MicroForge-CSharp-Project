# For Loops

For-loops are a slightly more complicated than while and do-while loops but 
on the other hand they can solve more complicated tasks with less code. Here 
is the scheme describing for-loops: 
 
They contain an initialization block  (A), condition (B), body (D) and 
updating commands  for the loop variables (C). We will explain them in 
details shortly. Before that, let ’s look at how the program code of a for-loop 
looks like: 
for (initialization; condition; update) 
```csharp
{ 
 loop's body; 
} 
It consists of an initialization part for the counter (in the pattern int i = 
0), a Boolean condition (i < 10 ), an expression for updating the counter 
(i++, it might be i-- or for instance, i = i + 3) and body of the loop. 
```
The counter of the loop  distinguishes it from other types of loops. Most 
often the counter changes from a given initial value to a final one in ascending 
order, for example from 1 to 100. The number of iterations of a given  for-
loop is usually known before its execution starts. A for-loop can have one or 
several loop variables that move in ascending or descending order or with a 
step. It is possible one loop variable to increase and the other – to decrease. 
It is even possible to make a loop from 2 to 1024 in steps of multiplication by 
2, since the update of the loop variables can contain not only addition, but 
any other arithmetic (as well as other) operations. 
true
B
D
false
C
for (A; B; C)
```csharp
{
D;
}
for (int i=0; i<10; i++)
{
/* loop body */
}
```
A

Since none of the listed elements of the for-loops is mandatory, we can skip 
them all and we will get an infinite loop: 
for ( ; ; ) 
```csharp
{ 
 // Loop body 
} 
```
Now let’s consider in details the separate parts of a for-loop.

## Initialization of For Loops

For-loops can have an initialization block: 
for (int num = 0; …; …) 
```csharp
{ 
 // The variable num is visible here and it can be used 
} 
// Here num can not be used 
```
It is executed only once , just before entering the loop. Usually the 
initialization block is used to declare the counter-variable (also called a loop 
variable) and to set its initial value. This variable is "visible" and can be used 
only within the loop. In the initialization block is possible to declare and 
initialize more than one variable.

## Condition of the For Loop

For-loops can have a loop condition: 
for (int num = 0; num < 10; …) 
```csharp
{ 
 // Loop body 
} 
```
The condition (loop condition) is evaluated once before each iteration of the 
loop, just like in the while loops. For result true the loop’s body is executed, 
for result false it is skipped and the loop ends (the program continues 
immediately after the last line of the loop’s body).

## Update of the Loop Variables

The last element of a for-loop contains code that updates the loop variable: 
for (int num = 0; num < 10; num++) 
```csharp
{ 
 // Loop body 

} 
```
This code is executed at each iteration , after the loop ’s body has been 
executed. It is most commonly used to update the value of the counter -
variable.

## The Body of the Loop

The body of the loop  contains a block with source code. The loop variables, 
declared in the initialization block of the loop are available in it.

## For-Loop – Example

Here is a complete example of a for-loop: 
for (int i = 0; i <= 10; i++) 
```csharp
{ 
 Console.Write(i + " "); 
} 
```
The result of its execution is the following: 
0 1 2 3 4 5 6 7 8 9 10 
Here is another, more complicated example of a for-loop, in which we have 
two variables i and sum, that initially have the value of 1, but we update them 
consecutively at each iteration of the loop: 
for (int i = 1, sum = 1; i <= 128; i = i * 2, sum += i) 
```csharp
{ 
 Console.WriteLine("i={0}, sum={1}", i, sum); 
} 
```
The result of this loop’s execution is the following: 
i=1, sum=1 
i=2, sum=3 
i=4, sum=7 
i=8, sum=15 
i=16, sum=31 
i=32, sum=63 
i=64, sum=127 
i=128, sum=255

## Calculating N^M – Example

As a further example we will write a program that raises the number n to a 
power of m, and for this purpose we will use a for-loop: 
```csharp
Console.Write("n = "); 
int n = int.Parse(Console.ReadLine()); 
Console.Write("m = "); 
int m = int.Parse(Console.ReadLine()); 
decimal result = 1; 
for (int i = 0; i < m; i++) 
{ 
 result *= n; 
} 
Console.WriteLine("n^m = " + result); 
First we initialize the result (result = 1). The loop starts by setting an initial 
value for the counter -variable (int i = 0 ). We define the condition for the 
```
loop’s execution (i < m). This way the loop will be executed from 0 to m-1 i.e. 
exactly m times. During each run of the loop we multiply the result by n and 
so n will be raised to the next power (1, 2, …, m) at each iteration. Finally we 
print the result to see if the program works properly. 
Here is how the outcome of the program for n = 2 and m = 10 looks like: 
n = 2 
m = 10 
n^m = 1024

## For-Loop with Several Variables

As we have already seen, in the construct of a for-loop we can use multiple 
variables at the same time. Here is an example in which we have two 
counters. One of the counters moves up from 1 and the other moves down 
from 10: 
for (int small=1, large=10; small<large; small++, large--) 
```csharp
{ 
 Console.WriteLine(small + " " + large); 
} 
The condition for loop termination is overlapping of the counters. Finally we 
get the following result: 
```
1 10 
2 9 

3 8 
4 7 
5 6

## Operator "continue"

The continue operator stops the current iteration  of the inner loop, 
without terminating the loop. With the following example we will examine how 
to use this operator. 
We will calculate the sum of all odd integers in the range [1 …n], which are not 
divisible by 7 by using the for-loop: 
int n = int.Parse(Console.ReadLine()); 
int sum = 0; 
for (int i = 1; i <= n; i += 2) 
```csharp
{ 
 if (i % 7 == 0) 
 { 
  continue; 
 } 
 sum += i; 
} 
Console.WriteLine("sum = " + sum); 
```
First we initialize the loop’s variable with a value of 1 as this is the first odd 
integer within the range [1 …n]. After each iteration of the loop we check if i 
has not yet exceeded n (i <= n). In the expression for updating the variable 
we increase it by 2 in order to pass only through the odd numbers. Inside the 
loop body we check whether the current number is divisible by 7. If so we call 
the operator continue, which skips the rest of the loop’s body (it skips adding 
the current number to the sum). If the number is not divisible by seven, it 
continues with updating of the sum with the current number. 
The result of the example for n = 11 is as follows: 
11 
sum = 29