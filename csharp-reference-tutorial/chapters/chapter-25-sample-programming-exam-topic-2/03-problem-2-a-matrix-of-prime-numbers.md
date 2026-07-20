# Problem 2: A Matrix of Prime Numbers

Write a program that reads a positive integer N from the standard input and 
prints the first N2 prime numbers as a square matrix of size N x N . The 
matrix must be filled with numbers starting from the first row and ending  at 
the last one. Each row must be filled with prime numbers from left to right. 
Note: A prime number is a number that has no divisors other than 1 and 
itself. The number 1 is not a prime number. 
Sample input: 
2         3         4 
Sample output: 
2 3        2 3 5       2 3 5 7 
5 7        7 11 13       11 13 17 19 
         17 19 23      23 29 31 37 
                  41 43 47 53

## Coming Up with an Appropriate Idea for a Solution

We can solve the problem by printing the rows and columns of the resulting 
matrix using two nested loops. For each of its elements we will extract and 
print the corresponding prime number.

## Breaking Down the Problem into Subproblems

We must solve at least two subproblems  – finding each successive 
prime number  and printing the prime numbers into a matrix . We can 
print the matrix right away, but the process of finding each successive prime 
number will require additional thinking. Perhaps the most intuitive way to 
accomplish this is to start testing the primality of each number starting  from 
the last prime number that we found. When a new prime is encountered, it is 
returned as a result. Thus, a new subproblem has come up – checking 
whether or not a number is a prime.

## Verifying the Idea

Our idea for a solution leads directly to the requi red result. We write down a 
couple of examples on a piece of paper and make sure that it works.

## Consider the Data Structures

The problem makes use of one data structure  only – a matrix. It ’s only 
natural to use a two-dimensional array (matrix).

## Consider the Efficiency

Displaying at the console large matrices (for example of size 1000 x 1000) 
cannot be properly handled. This means our solution should work for 
reasonably large matrices, e.g. on the order of N ≤ 200. We don ’t need to 
consider cases where the m atrix is too large. When N = 200, our algorithm 
will find the first 40,000 prime numbers and should not run slowly. 
Now we are ready for the implementation of the algorithm we invented.

## Step 1: Check to Find If a Number Is a Prime

To test a number for primality, we can define a method called IsPrime(…). 
The test will verify that dividing the number by any of its predecessors always 
yields a division remainder. To be more precise, it is sufficient to check the 
integers between 2 and the square root of the number . This holds true, 
because if the number p has a divisor x, then p = x.y, and at least one or 
both of the numbers x and y will be less than or equal to the square root of p. 
What follows is an implementation of the method: 
```csharp
private static bool IsPrime(int number) 
{ 

 int maxDivider = (int)Math.Sqrt(number); 
 for (int divider = 2; divider <= maxDivider; divider++) 
 { 
  if (number % divider == 0) 
  { 
   return false; 
  } 
 } 
 return true; 
} 
```
The algorithm complexity  of the above example is  O(sqrt(number)), 
because the amount of checks that will be made is not greater than the 
square root of the number. This complexity will suffice for the problem at 
hand, but is it possible to optimize this method even further? 
Come to think about it, every second number is even and all even numbers 
are divisible by 2. In that case, if the number we  are testing is odd, the above 
method will needlessly check all odd numbers from 2 to the square root of the 
number. How can we omit these  unnecessary checks? We could find out if the 
number is even at the very beginning of the method. If it ’s not, it will be 
processed in a modified version of the main loop that skips even numbers. 
Using this new approach, we have achieved the same  computational 
complexity of O(sqrt(number)), but with a better constant 1/2. 
This example illustrates how to optimize a bit the existing method: 
```csharp
private static bool IsPrime(int number) 
{ 
 if (number == 2) 
 { 
  return true; 
 } 
 if (number % 2 == 0) 
 { 
  return false; 
 } 
 
 int maxDivider = (int)Math.Sqrt(number); 
 for (int divider = 3; divider <= maxDivider; divider += 2) 
 { 
  if (number % divider == 0) 
  { 
   return false; 
  } 
 } 

 return true; 
} 
```
As we can see, there was a minimal amount of changes compared to the non-
optimized version.

### Testing the Prime Checking Method

We can make sure both methods work correctly by consecutively passing to 
them different numbers, some of which will be primes, and verifying the 
results. 
 
Always test a method to make sure it works before 
optimizing it. 
The reason to test your methods is that after optimization, the code usually 
gets longer, more difficult to read and therefore more difficult to debug, if it  is 
incorrect. 
 
Be careful when optimizing a piece of code. Do not go to 
extremes by making unnecessary optimizations that will 
make your program marginally faster at the expense of 
readability and maintenance. 
To check the prime checking method we could write a piece of code like this: 
```csharp
static void Main() 
{ 
 Console.WriteLine(IsPrime(2)); 
 Console.WriteLine(IsPrime(3)); 
 Console.WriteLine(IsPrime(4)); 
 Console.WriteLine(IsPrime(5)); 
 Console.WriteLine(IsPrime(121)); 
} 
```
It runs as expected and the produced results are correct: 
True 
True 
False 
True 
False

## Step 2: Finding the Next Prime Number

In order to find the next prime number, we can define a method that takes 
an integer as a parameter and returns the first prime number equal or larger 


than it. To check if the number is prime, we will  use the method from the 
previous step. Below is an implementation of the method: 
```csharp
private static int FindNextPrime(int startNumber) 
{ 
 int number = startNumber; 
 while (!IsPrime(number)) 
 { 
  number++; 
 } 
 return number; 
}
```

### Testing the Next Prime Number Finder

Once again we have to test the method by passing a few numbers and 
verifying that the result is correct: 
```csharp
static void Main() 
{ 
 Console.WriteLine(FindNextPrime(2)); 
 Console.WriteLine(FindNextPrime(3)); 
 Console.WriteLine(FindNextPrime(4)); 
 Console.WriteLine(FindNextPrime(5)); 
 Console.WriteLine(FindNextPrime(121)); 
} 
```
The result is correct, as expected: 
2 
3 
5 
5 
127

## Step 3: Printing the Matrix

Now that we have defined the previous methods, we are all set to print the 
entire matrix of prime numbers: 
```csharp
private static void PrintMatrixOfPrimes(int dimension) 
{ 
 int lastPrime = 1; 
 for (int row = 0; row < dimension; row++) 
 { 
  for (int col = 0; col < dimension; col++) 

  { 
   int nextPrime = FindNextPrime(lastPrime + 1); 
   Console.Write("{0,4}", nextPrime); 
   lastPrime = nextPrime; 
  } 
  Console.WriteLine(); 
 } 
} 
```
We will test this method as part of testing the entire program.

## Step 4: Console Input

All that’s left is to add functionality allowing us to read N from the console. 
```csharp
static void Main() 
{ 
 int n = ReadInput(); 
 PrintMatrixOfPrimes(n); 
} 
 
private static int ReadInput() 
{ 
 Console.Write("N = "); 
 string input = Console.ReadLine(); 
 int n = int.Parse(input); 
 return n; 
}
```

## Testing the Entire Solution

After we have completed all other steps, we can proceed with testing the 
entire solution. To do this, we could look up the first 25 prime numbers  (at 
a sheet of paper) and test the program’s output for values of N between 1 and 
5. We should include special border cases like N=0 and N=1 . We know that 
at border cases the likelihood of making a mistake is significantly higher. 
In our case, we can confine ourselves with the examples from the problem 
description, provided that the methods have been thoroughly tested at each 
step. This is the output of the program for N = 1, 2, 3 and 4 respectively: 
2    2 3      2 3 5       2 3 5 7 
    5 7      7 11 13       11 13 17 19 
           17 19 23      23 29 31 37 
                    41 43 47 53 

The result is correct and after few more tests we get convinced that we have 
solved correctly the problem “Matrix of Prime Numbers”. 
We can make sure the solution works relatively fast even for larger values of 
N. For example, there is no perceived lag for N = 200.

## A Word on Performance

We should point out, that our solution does not find prime numbers in the 
most efficient way . Despite of this drawback, due to the solution ’s clarity 
and the reasonably small size of the matrix, we can utilize this algorithm 
without performance issues.

### Improved Performance: Sieve of Eratosthenes

If we have to improve th e performance , we can find the first N 2 prime 
numbers using the Sieve of Eratosthenes. That way we will not need to check 
if every number is prime until we find N 2 prime numbers.  You might ask 
yourself how large Eratosthenes’s Sieve  will be needed if we wa nt to find 
the first N 2 prime numbers. You might use the following approximation 
(without any mathematical proof): 
long sieveSize = 
  (long)Math.Truncate(2.4 * n * n * Math.Log(n, Math.E)) + 2; 
If the Eratosthenes’s Sieve is at least sieveSize elements large, it will be 
enough to produce the first N 2 prime numbers and not too much above them. 
You could check this manually or you might invite a better formula  using 
complex mathematical calculations (see http://en.wikipedia.org/wiki/Prime-
counting_function). For example if N=10, the estimated sieveSize will be 
554 and it will find the first 101 prime numbers  (we need 10 2 = 100 prime 
numbers to fill the matrix, so these 101 prime numbers are enough) . If 
N=1,000, the sieveSize will be 16,578,614 and it will find the first 1,065,855 
prime numbers. For N=5,000 the sieveSize will be 511,031,593 and it will 
find the first 26,905,486 prime numbers. For significantly bigger sizes 
Eratosthenes’s Sieve will not fit in the memory. You might try to implement 
this algorithm and check how faster it is. When comparing the speed, you 
may redirect the output to a file to save some time  which is spent in printing 
the matrix.