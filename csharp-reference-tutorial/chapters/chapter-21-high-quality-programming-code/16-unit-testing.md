# Unit Testing

Unit testing means to write a program that tests a certain method or 
```csharp
class. A typical unit test executes the method that should be tested, passes a 
```
sample data to (parameters and object state s) and checks whether the 
method’s result is correct (for this sample data) , i.e. whether the method 
does exactly what it should do and whether it does it correctly. 
A single method usually is tested by several unit tests, each implementing a 
different testing scenario. First, the typical case is checked. Then the border 
cases are checked . The border cases are  special cases which could need 
special p rocessing logic , e.g. the largest or the smallest possible value, the 
first or last element, etc.  Finally the method is tested with incorrect data  
and an exception is expected to be thrown. Sometimes a performance test 
may be involved to check whether the method is fast enough.

## Unit Testing – Example

Let’s see a small example – a method that sums an array of numbers: 
```csharp
static int Sum(int[] numbers) 
{ 
 int sum = numbers[0]; 
 for (int i = 1; i < numbers.Length; i++) 
 { 
  sum += numbers[i]; 
 } 
 return sum; 
} 

```
The above method may look correct but in fact it has several bugs that we 
will catch through unit testing. Let’s first test the typical case: 
if (Sum(new int[] {1, 2}) != 3) 
 throw new Exception("1 + 2 != 3"); 
Seems like the Sum(…) method is working correctly in its typical case: the 
sum 1+2 is 3 (as expected) and the above code produces nothing. The above 
piece of code is called “ unit test”. It tests a certain method, class or other 
functionality against certain testing scenario an d notifies us if the code 
behaves unexpectedly. If the test passes, the code produces no result. 
Let’s now test the border cases . What will happen if we sum only one 
number? Let’s try: 
if (Sum(new int[] {1}) != 1) 
 throw new Exception("Sum of 1 != 1"); 
Seems like our method still works correctly. New let’s try to sum an empty 
list of numbers. Their sum should be 0, right? Let’s try this: 
if (Sum(new int[] {}) != 0) 
 throw new Exception("Sum of 0 numbers != 0"); 
The above code produces unexpected exception in the Sum(…) method: 
Unhandled Exception: System.IndexOutOfRangeException: Index was 
outside the bounds of the array. 
We found a bug , right? Let’s fix it. We could start summing from 0 instead 
from the first element in the array (which could be  missing when an empty 
array is passed as an argument). Below is the fixed code: 
```csharp
static int Sum(int[] numbers) 
{ 
 int sum = 0; 
 for (int i = 0; i < numbers.Length; i++) 
 { 
  sum += numbers[i]; 
 } 
 return sum; 
} 
```
We repeat our last test (summing an empty array of numbers) and it now 
passes correctly . Next we could try other special (border) cases , e.g. 
summing negative numbers: 

if (Sum(new int[] {-1, -2}) != -3) 
 throw new Exception("-1 + -2 != -3"); 
What else to try? Seems like our method work correctly. We could try to find 
some extreme case  when the method eventually fails. What will happen if 
we sum too big numbers? Int32 cannot hold too big integers. Let’s try: 
if (Sum(new int[] { 2000000000, 2000000000 }) != 4000000000) 
 throw new Exception("2000000000 + 2000000000 != 4000000000"); 
We found another bug in our method for summing numbers: 
Unhandled Exception: System.Exception: 2000000000 + 
2000000000 != 4000000000 
Obviously the integer type Int32 overflows and this causes incorrect result 
when summing too large numbers. Let’s fix this. We can use long to keep the 
sum of the numbers instead of int: 
```csharp
static long Sum(int[] numbers) 
{ 
 long sum = 0; 
 for (int i = 0; i < numbers.Length; i++) 
 { 
  sum += numbers[i]; 
 } 
 return sum; 
} 
Let’s repeat the last test. Now it works. What else to test? What will happen if 
```
we pass null as an argument to the Sum(…) method? The recommendations 
about high -quality methods say that “ a method should return what its 
name says or throw and exception if it cannot do its job ”. So our 
method should throw an exception if we try to sum a null array. We could 
test this in the following way:  
try 
```csharp
{ 
 Sum(null); 
 // An exception is expected --> the test fails 
 throw new Exception("Null array cannot be summed."); 
} 
```
catch (NullReferenceException) 
```csharp
{ 
 // NullReferenceException is expected --> the test passes 
} 

```
The above unit test is a bit more complicated: it expects an exception  and 
if it is not thrown, it fails. 
What else to test? Maybe we could make a performance test? For example 
we could sum 10,000,000 numbers and expect this will take time less than 1 
second (we assume a modern computer will run the tests): 
DateTime startTime = DateTime.Now; 
int[] arr = new int[10000000]; 
for (int i = 0; i < arr.Length; i++) 
```csharp
{ 
 arr[i] = 5; 
} 
if (Sum(arr) != 50000000) 
 throw new Exception("5 + ... (10000000 times) != 50000000"); 
DateTime endTime = DateTime.Now; 
if (endTime - startTime > new TimeSpan(0, 0, 1)) 
 throw new Exception("Performance issue: summing 10000000 " + 
 "numbers takes more that 1 second"); 
```
The performance test passes without any issues. 
We repeat all tests again to ensure that after the modifications we made all 
tests are still working correctly. All te sts pass! We can now be confident that 
the Sum(…) method works correctly (even in unusual situations) and it is well 
tested. Let’s think about what are the benefits if we test in similar manner 
all methods in our code.

## Benefits of Unit Testing

Unit testing has many benefits  for our code quality . Let’s discuss the most 
important of them: 
- Unit testing significantly improves the code quality. If the unit tests 
are well written  and the entire functionality is covered , the code is 
expected to be bug free. In practice it is very hard to cover with tests 
any possible scenario so unit testing only dramatically reduces the 
number of bugs but does not make the code bug free. 
- Unit testing allows the tests to be executed many times, continuously, 
e.g. at every hour . If some test fails, the problem is caught almost 
instantly. In software engineering the practice of executing the unit 
tests continuously is called “continuous integration”. 
- The code quality  is preserved every time the method is modified. This 
dramatically simplifies the maintenance. If we change the algorithm 
inside some method or cl ass and we have covered it well with tests, we 
will be sure that the new algorithm behaves the same way like the old. 

- Unit tests allow code refactoring without worrying of something being 
broken. It can happen that we refactor the code to improve its internal 
quality but by mistake after the refactoring the code does not work 
correctly in all special cases. 
All serious software development companies and software prod ucts use unit 
testing. For example if you download the source code of Firefox, you will 
notice that half of the code is written to perform unit tests over the other half 
of code. In practice it is impossible to write complex product  (like for 
example MS Word or Android OS or Firefox browser) without unit testing.

## Benefits of Unit Testing – Example

Let’s see one of the benefits of unit testing: the ability to change the internal 
implementation of a method and re -test it to ensure the new implementation 
works as expected. Consider the following new implementation of the Sum(…) 
method that uses the Sum() extension method from System.Linq: 
```csharp
using System; 
using System.Linq; 
 
static long Sum(int[] numbers) 
{ 
 return numbers.Sum(); 
} 
```
We will explain how the above code works in the next chapter “ Lambda 
Expressions and LINQ ”. Now let’s test it to ensure this code behaves as 
expected. If we run the same set of tests we discussed above, we will find a 
problem: two of our tests do not work. The first failing test is: 
if (Sum(new int[] { 2000000000, 2000000000 }) != 4000000000) 
 throw new Exception("2000000000 + 2000000000 != 4000000000"); 
We found a bug in our new implementation of our Sum(…) method: instead of 
returning the correct result it produces System.OverflowException. We 
cannot find an easy solution to this problem so we can either assume that 
summing too big numbers will not be supported and modify the test to expect 
OverflowException or we can rewrite the Sum(…) method with a new 
implementation. 
If we pass ahead, we will find that one more unit test fails: when we try to 
sum a null array, we will get System.ArgumentNullException instead of 
NullReferenceException. This is easy to fix by modifying the unit test code: 

try 
```csharp
{ 
 Sum(null); 
 // An exception is expected --> the test fails 
 throw new Exception("Null array cannot be summed."); 
} 
```
catch (ArgumentNullException) 
```csharp
{ 
 // NullReferenceException is expected --> the test passes 
} 
```
Now all unit tests works correctly. The conclusion form the above experience 
is that when we modify the code  and a unit test fail s, either the tested 
code is incorrect, or the unit test is incorrect . In both cases we are 
notified that our new code behaves differe ntly than our old code. This is very 
important in software engineering process . When we develop a complex 
software product, we want the features that work in its current version to 
continue to works the same way in all its next versions. For example, if we  
work on MS Word and we add PDF export for its next version, we want to be 
sure that saving in DOCX format still works after the PDF export is introduced.

## Unit Testing Frameworks and Tools

To simplify writing unit tests and execute them many unit testing 
frameworks and tools have emerged. In C# we can use Visual Studio 
Team Test (VSTT)  or NUnit frameworks to simplify the process of writing 
tests, asserting test conditions and executing test cases and test suites.

## Unit Testing with Visual Studio Team Test (VSTT)

If you have installed Visual Studio 2010 edition which supports unit testing 
(e.g. Visual Studio 2010 Ultimate), you will have the [Create Unit Tests …]  
feature in the popup menu when you right click at some method in your C# 
code: 
 


The above feature was introduced in VS 2010 and is missing in VS 2012 for 
unknown reason. So if you are using Visual Studio 2012, you need to create a 
unit test project by hand (File  New Project  Unit Test Project). 
The unit tests in Visual Studio Team Test look like the following: 
```csharp
using System; 
using Microsoft.VisualStudio.TestTools.UnitTesting; 
 
```
[TestClass] 
```csharp
public class SumatorTest 
{ 
```
 [TestMethod] 
```csharp
 public void SumTestTypicalCase() 
 { 
  int[] numbers = new int[] { 1, 2 }; 
  long expected = 3; 
  long actual = Sumator_Accessor.Sum(numbers); 
  Assert.AreEqual(expected, actual); 
 } 
 
```
 [TestMethod] 
```csharp
 public void SumTestOverflow() 
 { 
  int[] numbers = new int[] { 2000000000, 2000000000 }; 
  long expected = 4000000000; 
  long actual = Sumator_Accessor.Sum(numbers); 
  Assert.AreEqual(expected, actual); 
 } 
 
```
 [TestMethod] 
 [ExpectedException(typeof(NullReferenceException))] 
```csharp
 public void SumTestNullArray() 
 { 
  Sumator_Accessor.Sum(null); 
 } 
} 
```
A detailed explanation of VSTT will not be given in this book, but anyone could 
research how to use unit testing in Visual Studio. As you see from the 
example above, VSTT simplifies unit testing by introducing test classes and 
test methods. Each test method has a meaningful name which and tests a 
certain test case. VSTT c an test private methods, can set time limit for the 
test execution and can expect exception to be thrown by certain test case – 
things that simplify writing the testing code . Visual Studio can execute 
and visualize the results of the test execution: