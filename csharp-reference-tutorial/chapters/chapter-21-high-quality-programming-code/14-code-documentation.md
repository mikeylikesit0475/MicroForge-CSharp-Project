# Code Documentation

The C# specification allows putting comments in the code. We are already 
familiar with the basic principles for writing comments . In the next few 
paragraphs we explain how to write effective comments.

## Self-Documenting Code

A very important point to remember is that comments in the code are not the 
primary source of documentation. Good programming style provides the 
best documentation . Self -documenting code rarely needs comments 
because its intention becomes  clear directly by reading it.  Self-documenting 
code means a code that is easy-to-read and easy-to-understand without 
having comments inside. 
 
The best way to document the code is to write quality code. 
Bad code should not be document ed but should rather be 
rewritten! Comments are only a complement to the well -
written code.

## Properties of Self-Documenting Code

Self-documenting code  boasts a good structure: everything mentioned in 
this chapter matters. The implementation should be as si mple as possible so 
that anyone can understand it.

## Self-Documenting Code – Important Questions

In order to qualify our code as self -documenting, there are a few questions 
we should ask ourselves: 
- Is the class name appropriate and does it describe its main purpose? 
- Is the public interface of the class intuitive to use? 
- Does the name of a method describe its main purpose? 
- Is every method performing a single, well-defined task? 
- Are the names of the variables  corresponding to the intent of their 
use? 
- Are loops performing only a single task? 
- Are conditional statements deeply nested? 
- Does the organization of the code illustrate its logical structure? 
- Is the design clear and unambiguous? 
- Are implementation details hidden as much as possible?

## Effective Comments

Comments can sometimes do more harm than good . Good comments do 
not repeat the code  and do not explain it line by line: they rather clarify its 
idea. Comments should describe at a higher level what our intentions are. 
Comments enable us to think better about what we want to implement. 
Here is an example of bad comments , which, instead of  making the code 
more comprehensible, are actually annoying: 
```csharp
public List<int> FindPrimes(int start, int end) 
{ 
 // Create new list of integers 
 List<int> primesList = new();
 // Perform a loop from start to end 
 for (int num = start; num <= end; num++) 
 { 
  // Declare boolean variable, initially true 
  bool prime = true; 
  // Perform loop from 2 to sqrt(num) 
  for (int div = 2; div <= Math.Sqrt(num); div++) 

  { 
   // Check if div divides num with no remainder 
   if (num % div == 0) 
   { 
    // We found a divider -> the number is not prime 
    prime = false; 
    // Exit from the loop 
    break; 
   } 
   // Continue with the next loop value 
  } 
 
  // Check if the number is prime 
  if (prime) 
  { 
   // Add the number to the list of primes 
   primesList.Add(num); 
  } 
 } 
 
 // Return the list of primes 
 return primesList; 
} 
```
If, instead of writing naive comments, we write comments to clarify the 
unobvious facts in the code, comments can be very useful. Here is how the 
same code can be commented meaningfully: 
/// <summary>Finds the primes from a range [start, end] and 
/// returns them in a list.</summary> 
/// <param name="start">Top of range</param> 
/// <param name="end">End of range</param> 
/// <returns>a list of all the found primes</returns> 
```csharp
public List<int> FindPrimes(int start, int end) 
{ 
 List<int> primesList = new();
 for (int num = start; num <= end; num++) 
 { 
  bool isPrime = IsPrime(num); 
  if (isPrime) 
  { 
   primesList.Add(num); 
  } 
 } 
 return primesList; 

} 
 
/// <summary>Checks if a number is prime by checking for any 
/// dividers in the range [2, sqrt(number)].</summary> 
/// <param name="number">The number to be checked</param> 
/// <returns>True if prime</returns> 
public bool IsPrime(int number) 
{ 
 for (int div = 2; div <= Math.Sqrt(number); div++) 
 { 
  if (number % div == 0) 
  { 
   return false; 
  } 
 } 
 
 return true; 
} 
```
The logic of the code is obvious and does not need any comments . In such 
case it is sufficient only to describe what are the particular method ’s purpose 
and its general idea, in a single sentence. 
In order to write effective comments, it is desirable to use pseudo -code, 
whenever possible. Comments should be written at the time the code is 
written, not after that. 
Productivity (i.e. writing code qu ickly) is never a good excuse for not writing 
comments. Everything that is not instantly obvious should be documented. 
Writing too much unnecessary comments is as bad as not having any at all. 
Bad code cannot be improved by putting more comments. It should instead 
be rewritten or refactored.

## XML Documentation in C#

You might have already noted the special comments in the code that explain 
the purpose of a class or a method and its parameters:  
/// <summary>Finds the primes from a range [start, end] and 
/// returns them in a list.</summary> 
/// <param name="start">Top of range</param> 
/// <param name="end">End of range</param> 
/// <returns>a list of all the found primes</returns> 
```csharp
public List<int> FindPrimes(int start, int end) 
{ … } 

```
This special style of documentation buil t-in the C# source code is called XML 
documentation. It is enclosed in the triple comments /// and use s few 
special XML tags : to document a type / method summary ( <summary>), to 
describe method’s parameters (<param name="…">), to describe a method’s 
return value ( <returns>), to document exceptions that eventually might be 
thrown (<exception cref="…" ), to make a cross -reference link to related 
type (<seealso cref="…"/>), to describe some remarks (<remarks>), to give 
an example how to use the type / method (<example>), etc. 
Using XML-style documentation in the source code has several advantages: 
- The XML documentation is built-in the source code itself. 
- The XML documentation is automatically processed by Visual Studio 
and is displayed in its autocomplete feature. 
- The XML documentation can be compiled into an MSDN -style web 
site or e -book (in CHM format) through specialized tools like 
Sandcastle (http://shfb.codeplex.com). 
More about writing and using XML documentation can be found in MSDN 
Library: http://msdn.microsoft.com/en-us/library/b2s063f7.aspx.