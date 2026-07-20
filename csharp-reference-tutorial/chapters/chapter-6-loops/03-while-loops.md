# While Loops

One of the simplest and most commonly used loops is while. 
while (condition) 
```csharp
{ 
 loop body; 
} 
```
In the code above example, condition is any expression that returns a 
Boolean result – true or false. It determines how long the loop body will 
be repeated and is called the loop condition. In this example the loop body 
is the programming code executed at each iteration of the loop, i.e. whenever 
the input condition is true. The behavior of while loops can be represented by 
the following scheme: 

 
In the while loop, first of all the Boolean expression is calculated and if it is 
true the sequence of operations in the body of the loop is executed. Then 
again the input condition is checked and if it is true again the body of the loop 
is executed. All this is repeated again and again until at some point the 
conditional expression returns value false. At this point the loop stops 
and the program continues to the next line, immediately after the body of the 
loop. 
The body of the while loop may not be executed even once if in the beginning 
the condition of the cycle returns false. If the condition of the cycle is never 
broken the loop will be executed indefinitely.

## Usage of While Loops

Let’s consider a very simple example of using the while loop. The purpose of 
the loop is to print on the console the numbers in the range from 0 to 9 in 
ascending order: 
// Initialize the counter 
int counter = 0; 
// Execute the loop body while the loop condition holds 
while (counter <= 9) 
```csharp
{ 
 // Print the counter value 
 Console.WriteLine("Number : " + counter); 
 // Increment the counter 
 counter++; 
} 
```
When executing the sample code we obtain the following result: 
Number : 0 
true 
Condition 
Loop body 
false 

Number : 1 
Number : 2 
Number : 3 
Number : 4 
Number : 5 
Number : 6 
Number : 7 
Number : 8 
Number : 9 
Let’s give some more examples in order to illustrate the usefulness of loops 
and to show some problems that can be solved by using loops.

## Summing the Numbers from 1 to N

In this example we will examine how by using the while loop we can find the 
sum of the numbers from 1 to n. The number n is read from the console: 
```csharp
Console.Write("n = "); 
int n = int.Parse(Console.ReadLine()); 
int num = 1; 
int sum = 1; 
Console.Write("The sum 1"); 
while (num < n) 
{ 
 num++; 
 sum += num; 
 Console.Write(" + " + num); 
} 
Console.WriteLine(" = " + sum); 
```
First we initialize the variables num and sum with the value of 1. In num we 
keep the current number, which we add to the sum of the preceding numbers. 
Trough each loop we increase num with 1 to get the next number, then in the 
condition of the loop we check whether it is in the range from 1 to n. The sum 
variable contains the sum of the numbers from 1 to num at any time. Upon 
entering the loop we add to sum the next number stored in num. We print on 
the console all num numbers from 1 to n with a separator " +" and the final 
result of the summing after the loop ’s ending. The result of the program ’s 
execution is as follows (we enter n = 17): 
N = 17 
The sum 1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10 + 11 + 12 + 13 + 
14 + 15 + 16 + 17 = 153 

Let’s give another example of using the while loop, before moving on to 
other structures for organizing loops.

## Check If a Number Is Prime – Example

We will write a program to check whether a given number is prime or 
not. We will read the number to check from the console. As we know from the 
mathematics, a prime number is any positive integer number, which, is not 
divisible by any other numbers except 1 and itself. We can check if the 
number num is prime when in a loop we  check if it divides by numbers from 2 
to √num: 
```csharp
Console.Write("Enter a positive number: "); 
int num = int.Parse(Console.ReadLine()); 
int divider = 2; 
int maxDivider = (int)Math.Sqrt(num); 
bool prime = true; 
while (prime && (divider <= maxDivider)) 
{ 
 if (num % divider == 0) 
 { 
  prime = false; 
 } 
 divider++; 
} 
Console.WriteLine("Prime? " + prime); 
```
We use the variable divider to store the value of a potential divisor of the 
number. First we initialize it with 2 (the smallest possible divider). The 
variable maxDivider is the maximum possible divisor, which is equal to the 
square root of the number. If we have a divisor bigger t han √num, then num 
should also have another divisor smaller than √num and that’s why it’s useless 
to check the numbers bigger than √num. This way we reduce the number of 
loop iterations. 
For the result we use a Boolean variable called prime. Initially, its value is 
true. While passing through the loop, if it turns out that the number has a 
divisor, the value of prime will become false. 
The condition of the while loop consists of two other sub-conditions which are 
related to the logical opera tor (logical and). In order to execute the loop, 
these two sub -conditions must be true simultaneously. If at some point we 
find a divisor of the number num, the variable prime becomes false and the 
condition of the loop is no longer satisfied. This means t hat the loop is 
executed until it finds the first divisor of the number or until it proves the fact 
that num is not divisible by any of the numbers in the range from 2 to √num. 

Here is how the result of the above example ’s execution looks like if the input  
values are respectively the numbers 37 and 34: 
Enter a positive number: 37 
Prime? True 
Enter a positive number: 34 
Prime? False

## Operator "break"

The break operator is used for prematurely exiting the loop , before it has 
completed its execution in a natu ral way. When the loop reaches the break 
operator it is terminated and the program ’s execution continues from the line 
immediately after the loop ’s body. A loop ’s termination with the break 
operator can only be done from its body, during an iteration of the loop. When 
break is executed  the code in the loop ’s body after it is skipped and not 
executed. We will demonstrate exiting from loop with break with an example.

## Calculating Factorial – Example

In this example we will calculate the factorial of a number entered from the 
console. The calculation is performed by using an infinite while loop and the 
operator break. Let’s remember from the mathematics what is factorial and 
how it is calculated.  The factorial of an integer n is a function that is 
calculated as a product of all integers less than or equal to n or equal to it. It 
is written down as n! and by definition the following formulas are valid for it: 
- N! = 1 * 2 * 3 … (n-1) * n, for n> 1; 
- 2! = 1 * 2; 
- 1! = 1; 
- 0! = 1. 
 The product n! can be expressed by a factorial of integers less than n: 
- N! = (N-1)! * N, by using the initial value of 0! = 1. 
In order to calculate the factorial of n we will directly use the definition: 
int n = int.Parse(Console.ReadLine()); 
// "decimal" is the biggest C# type that can hold integer values 
decimal factorial = 1; 
// Perform an "infinite loop" 
while (true) 
```csharp
{ 
 if (n <= 1) 
 { 

  break; 
 } 
 factorial *= n; 
 n--; 
} 
Console.WriteLine("n! = " + factorial); 
```
First we initialize the variable factorial with 1 and read n from the console. 
We construct an endless while loop by using true as a condition of the loop. 
We use the break operator, in order to terminate the loop, when n reaches a 
value less than or equal to 1. Otherwise, we multiply the current result by n 
and we reduce n with one unit. Practically in the first iteration of the loop the 
variable factorial has a value n, in the second – n*(n-1) and so on. In the 
last iteration of the loop the value of factorial is the product n*(n -1)*(n-
2)*…*3*2, which is the desired value of n!. 
If we execute the sample program and enter 10 as input, we obtain the 
following result: 
10 
n! = 3628800