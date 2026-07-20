# Nested Loops

The nested loops are programming constructs consisting of several loops 
located into each other. The innermost loop is executed mo re times, and the 
outermost – less times. Let’s see how two nested loops look like: 
for (initialization, verification, update) 
```csharp
{ 
 for (initialization, verification, update) 
 { 

```
  executable code 
 } 
 … 
} 
After initialization of the first for loop, the execution of its body will start, 
which contains the second (nested) loop. Its variable will be initialized, its 
condition will be checked and the code within its body will be executed, then 
the variable will be updated and execution will contin ue until the condition 
returns false. After that the second iteration of the first for loop will 
continue, its variable will be updated and the whole second loop will be 
performed once again. The inner loop will be fully executed as many times as 
the body of the outer loop. 
Let’s now consider a real example that will demonstrate how useful the nested 
loops are.

## Printing a Triangle – Example

Let’s solve the following problem: for a given number n, to print on the 
console a triangle with n number of lines, looking like this: 
1 
1 2 
1 2 3 
… 
1 2 3 … n 
We will solve the problem with two for-loops. The outer loop will traverse 
the lines, and the inner one – the elements in them. When we are on the first 
line, we have to print "1" (1 element, 1 iteration of the inner loop). On the 
second line we have to print "1 2" (2 elements, 2 iterations of the internal 
loop). We see that there is a correlation between the line on which we are and 
the number of the elements that we print. This tells us how to organize the 
inner loop’s structure: 
- We initialize the loop variable with 1 (the first number that we will 
print): col = 1; 
- The repetition condition depends on the line on which we are: col <= 
row; 
- We increase the loop variable with one unit at each iteration  of th e 
internal loop. 
Basically, we need to implement a for-loop (external) from 1 to n (for the 
lines) and put another for-loop (internal) in it – for the numbers on the 
current line, which should spin from 1 to the number of the current line. The 

external loo p should go through the lines while the internal – through the 
columns of the current line. 
Finally we get the following code: 
int n = int.Parse(Console.ReadLine()); 
for (int row = 1; row <= n; row++) 
```csharp
{ 
 for (int col = 1; col <= row; col++) 
 { 
  Console.Write(col + " "); 
 } 
 Console.WriteLine(); 
} 
```
If we execute it, we will make sure that it works correctly. Here is how the 
result for n=7 looks like: 
1 
1 2 
1 2 3 
1 2 3 4 
1 2 3 4 5 
1 2 3 4 5 6 
1 2 3 4 5 6 7 
Note: when n > 9 the triangle will have a small defect. Think how to fix it!

## Prime Numbers in an Interval – Example

Let’s consider another example of nested loops . We set a goal to print on 
the console all prime number in the interval [n …m]. We will limit the interval 
by a for-loop and in order to check  for a prime number we will use a nested 
while loop: 
```csharp
Console.Write("n = "); 
int n = int.Parse(Console.ReadLine()); 
Console.Write("m = "); 
int m = int.Parse(Console.ReadLine()); 
 
for (int num = n; num <= m; num++) 
{ 
 bool prime = true; 
 int divider = 2; 
 int maxDivider = (int)Math.Sqrt(num); 
 while (divider <= maxDivider) 

 { 
  if (num % divider == 0) 
  { 
   prime = false; 
   break; 
  } 
  divider++; 
 } 
 if (prime) 
 { 
  Console.Write(" " + num); 
 } 
} 
Using the outer for-loop we check each of the numbers n, n+1, …, m  if it is 
prime. At each iteration  of the outer for-loop we check whether its loop 
variable num is a prime number. The logic by which we check for a prime 
```
number is already familiar to us. At first we initialize the variable prime with 
true. With the internal while loop we check for each of the numbers 
[2…√num] if it is a divisor of num and if so, we set prime to false. After 
finishing the while loop the Boolean variable prime indicates whether the 
number is prime or not. If the number is prime we print it on the console. 
If we execute the example for n=3 and m=75, we will obtain the following 
result: 
n = 3 
m = 75 
 3 5 7 11 13 17 19 23 29 31 37 41 43 47 53 59 61 67 71 73

## Lucky Numbers – Example

Let’s consider another example through which we will s how that we can put 
even more than two loops into each other . Our purpose is to find and 
print all four -digit numbers of the type ABCD, where: A+B = C+D  (we call 
them lucky numbers). We will implement it with four for-loops – one for each 
digit. The outerm ost loop will define the thousands. It will start from 1 and 
the rest of the loops – from 0. They will determine the hundreds, the tens and 
the units. We will perform a check if our current number in the  most inner 
loop is a lucky one and if so, we will pr int it on the console. Here is an 
implementation example: 
for (int a = 1; a <= 9; a++) 
```csharp
{ 
 for (int b = 0; b <= 9; b++) 

 { 
  for (int c = 0; c <= 9; c++) 
  { 
   for (int d = 0; d <= 9; d++) 
   { 
    if ((a + b) == (c + d)) 
    { 
     Console.WriteLine( 
      " " + a + " " + b + " " + c + " " + d); 
    } 
   } 
  } 
 } 
} 
```
Here is a part of the printed result (the entire result is too long): 
1 0 0 1 
1 0 1 0 
1 1 0 2 
1 1 1 1 
1 1 2 0 
1 2 0 3 
1 2 1 2 
1 2 2 1 
… 
We leave as homework for the diligent reader to offer a more efficient solution 
to the same problem, using only three nested loops rather than four.

## Lottery 6/49 – Example

In the following example we will find all possible combina tions of the 
lottery game "6/49 ". We have to find and print all possible extracts of 6 
different numbers, each of which is in the range [1 …49]. We will use 6 for-
loops. Unlike the previous example, the numbers cannot be repeated. To 
avoid repetitions we will strive for each subsequent number to be larger than 
the previous. Therefore, th e internal loops will not start from 1 but from the 
number to which the previous loop got + 1. We will have to go through the 
first loop until it reaches 44 (and not to 49), the second – 45, etc. The last 
loop will be up to 49. If you make all loops to 49 you will receive matching 
numbers in certain combinations. For the same reason, each subsequent cycle 
starts from the previous loop counter + 1. Let’s see what will happen: 
for (int i1 = 1; i1 <= 44; i1++) 
```csharp
{ 

 for (int i2 = i1 + 1; i2 <= 45; i2++) 
 { 
  for (int i3 = i2 + 1; i3 <= 46; i3++) 
  { 
   for (int i4 = i3 + 1; i4 <= 47; i4++) 
   { 
    for (int i5 = i4 + 1; i5 <= 48; i5++) 
    { 
     for (int i6 = i5 + 1; i6 <= 49; i6++) 
     { 
      Console.WriteLine(i1 + " " + i2 + " " + 
       i3 + " " + i4 + " " + i5 + " " + i6); 
     } 
    } 
   } 
  } 
 } 
} 
```
Everything looks correct. Let’s run the program. It seems to work but there is 
one problem – there are too many combinations and the program does not 
end (it is so slow, that hardly anyone is going to wait). This is correct and it 
is one of the reasons why there is Lottery 6/49 – there really are lots of 
combinations. We are leaving the curious reader to practice changing the 
example above just to calculate all lottery combinations, instead of printing 
them. This change will dramatically reduce the volume of the printed results 
on the console and the program will end surprisingly quickly. 
 
Printing excessive amount of text on the console is very slow 
and should be avoided. A modern computer (as of 2012) can 
perform 3 00,000,000 operations per second but can print 
only 10,000 – 20,000 text lines per second.