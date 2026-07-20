# Do-While Loops

The do-while loop is similar to the while loop, but it checks the condition 
after each execution of its loop body. This type of loops is called loops with 
condition at the end (post-test loop). A do-while loop looks like this: 
do 
```csharp
{ 
 executable code; 
} while (condition); 
By design do-while loops are executed according to the following scheme: 
 
```
true 
Condition 
Loop body 
false 

Initially the loop body is executed. Then its condition is checked . If it is 
true, the loop ’s body is repeated, otherwise the loop ends. This logic is 
repeated until the condition of the loop is broken . The body of the loop is 
executed at least once. If the loop ’s condition is constantly true, the loop 
never ends.

## Usage of Do-While Loops

The do-while loop is used when we want to guarantee that the sequence of 
operations in it will be executed repeatedly and at least once in the beginning 
of the loop.

## Calculating Factorial – Example

In this example we will again calculate the factorial of a given number n, but 
this time instead of an infinite while loop we will use a do-while. The logic is 
similar to that in the previous example: 
```csharp
Console.Write("n = "); 
int n = int.Parse(Console.ReadLine()); 
decimal factorial = 1; 
```
do 
```csharp
{ 
 factorial *= n; 
 n--; 
} while (n > 0); 
Console.WriteLine("n! = " + factorial); 
```
At the beginning we start with a result of 1 and multiply consecutively the 
result at each iteration by n, and reduce n by one unit, until n reaches 0. This 
gives us the product n*(n-1)*…*1. Finally, we print the result on the console. 
This algorithm always performs at least one multiplication and that ’s why it 
will not work properly when n ≤ 0. 
Here is the result of the above example’s execution for n=7: 
n = 7 
n! = 5040

## Factorial of a Large Number – Example

You might be wondering what will happen if we set a large value for the 
number n in the previous example, say n=100. Then when, calculating the n! 
we will overflow the decimal type and the result will be an exception of type 
System.OverflowException: 
n = 100 

Unhandled Exception: System.OverflowException: Value was either 
too large or too small for a Decimal. 
 at System.Decimal.FCallMultiply(Decimal& result, Decimal d1, 
Decimal d2) 
 at System.Decimal.op_Multiply(Decimal d1, Decimal d2) 
 at TestProject.Program.Main() in 
C:\Projects\TestProject\Program 
.cs:line 17 
If we want to calculate 100! we can use data type BigInteger (which is new 
as of  .NET Fram ework 4.0 and is missing in the older .NET versions). This 
type represents an integer, which can be very large  (for example 100 ,000 
digits). There is no limit on the size of the numbers recorded in the class 
BigInteger (as long as you have enough RAM). 
In order to use BigInteger, we must first add a reference from our project 
to the assembly System.Numerics.dll (this is a standard .NET library for 
working with very large integers, which is not referenced by default by our VS 
projects). Adding a reference  to it is done by right -clicking on the current 
project references in the Solution Explorer window of Visual Studio: 
 
We search and choose the assembly System.Numerics.dll from the list: 
 


If the assembly is missing from the list, that means that the Visual Studio 
project probably does not target .NET Framework 4.0  or above  and you 
must either create a new project or change the version of the current one: 
 
Then we need to add "using System.Numerics;" before the beginning of the 
```csharp
class of our program and replace decimal with BigInteger. The program 
```
obtains the following form: 
```csharp
using System; 
using System.Numerics; 
 
class Factorial 
{ 
 static void Main() 
 { 
  Console.Write("n = "); 
  int n = int.Parse(Console.ReadLine()); 
  BigInteger factorial = 1; 
```
  do 
```csharp
  { 
   factorial *= n; 
   n--; 
  } while (n > 0); 
  Console.WriteLine("n! = " + factorial); 
 } 
} 
If we now run the program for n=100, we will get the value of 100 factorial, 
```
which is a 158-digit number: 
n = 100 
n! = 
9332621544394415268169923885626670049071596826438162146859296389
5217599993229915608941463976156518286253697920827223758251185210
916864000000000000000000000000 


By BigInteger you can calculate 1000!, 10000! and even 100000! It will take 
some time, but OverflowException will not occur. The BigInteger class is 
very powerful but it works many times slower than int and long. For our 
unpleasant surprise there is no class "big decimal" in .NET Framework, only 
"big integer".

## Product in the Range [N…M] – Example

Let’s give another, more interesting example of working with do-while loops. 
The goal is to find the product of all numbers in the range [n…m]. Here is an 
example solution to this problem: 
```csharp
Console.Write("n = "); 
int n = int.Parse(Console.ReadLine()); 
 
Console.Write("m = "); 
int m = int.Parse(Console.ReadLine()); 
 
int num = n; 
long product = 1; 
```
do 
```csharp
{ 
 product *= num; 
 num++; 
} while (num <= m); 
 
Console.WriteLine("product[n...m] = " + product); 
```
In the example code we consecutively assign to num at each iteration  the 
values n, n+1, …, m and in the variable product we accumulate the product 
of these values. We require the user to enter n, which should be less than m. 
Otherwise we will receive as a result the number n. 
If we run the program for n=2 and m=6 we will obtain the following result: 
n = 2 
m = 6 
product[n...m] = 720 
Be careful: the product grows very fast, so you may need to use BigInteger 
instead of long for the calculated result. Also beware of hidden integer 
overflow. Unchecked code will silently overflow and the code above will 
produce incorrect output instead of showing an error. To overcome this, you 
may surround the line holding the multiplication by the checked keyword.