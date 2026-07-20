# Problem 3: Evaluate an Arithmetic Expression

Write a program that evaluates a simple arithmetic expression  consisting 
of unsigned integers and the arithmetic operations " +" and " -". There will be 
no blank spaces between the integers. 
The expression will have the following format: 
<number><operation>…<number> 

Sample input: 
1+2-7+2-1+28+2+3-37+22 
Sample output: 
15

## Coming Up with an Appropriate Idea for a Solution

To solve this problem, we can take advantage of the strict  expression format, 
which guarantees we have a sequence of a number, operation, another 
number and so on. 
That way, we can first extract all numbers  from the expression, and then 
we can extract all operators  and finally evaluate the result by combining 
the numbers with the operators.

## Verifying the Idea

Sure enough, if we test this approach with a few expressions using pen and 
paper, we acquire a correct result. Initially, the result is equal to the first 
number and at each step we either add or subtract the  next number 
depending on the current operator.

## Data Structures and Efficiency

The problem is too simple for us to use complex data structures . The 
numbers and characters can be stored in arrays. Performance issues are out 
of the question, because the characters and numbers are processed exactly 
once, i.e. the complexity of the algorithm is linear. Even with millions of 
integers and operators, the algorithm is expected to work fast.

## Breaking Down the Problem into Subproblems

Now that we have made sure the idea works, we can move on to breaking 
down the problem into subproblems. The first subproblem we will  have to 
solve is extracting the numbers  from the expression. The second – 
extracting the operators. Finally, we will evaluate the entire ex pression 
```csharp
using the extracted numbers and operators.
```

## Step 1: Extracting the Numbers

In order to extract the numbers, we need to split the expression using the 
operators (+ and -) as separators. This is easily done using the Split(…) 
method of the string class. Afterwards, we have to convert the resulting 
array of strings to an array of integers: 

```csharp
private static int[] ExtractNumbers(string expression) 
{ 
 string[] splitResult = expression.Split('+', '-'); 
 int[] resultNumbers = new int[splitResult.Length]; 
 for (int i = 0; i < splitResult.Length; i++) 
 { 
  resultNumbers[i] = int.Parse(splitResult[i]); 
 } 
 return resultNumbers; 
} 
We use the Parse(…) method of the Int32 class to convert strings to 
integers. It takes a string as a parameter and returns the int eger value that 
the string represents. 
```
Why do we use an array to store the numbers ? Can’t we use a linked list 
or a dynamic array? Of course we can, but in our case we only need to store 
the integers and iterate through them when evaluating the result. The refore, 
an array is sufficient and is the simplest collection that will work.

### Testing the Extraction of Numbers

Before we move on to the next step we should check if the n umbers are 
extracted correctly. We may use the following example: 
```csharp
static void Main() 
{ 
 int[] numbers = ExtractNumbers("1+2-7+2-1+28"); 
 foreach (int x in numbers) 
 { 
  Console.Write("{0} ", x); 
 } 
} 
```
The result is exactly as it should be: 
1 2 7 2 1 28 
We examine the border case when the expression consists only of one 
number and no operators, and we make sure it is handled properly.

## Step 2: Extracting the Operators

We can extract the operators  by iterating through each consecutive 
character from the string and check if it is one of the specified operators: 
```csharp
private static char[] ExtractOperators(string expression) 

{ 
 string operatorCharacters = "+-"; 
 List<char> operators = new();
 foreach (char c in expression) 
 { 
  if(operatorCharacters.Contains(c)) 
  { 
   operators.Add(c); 
  } 
 } 
 return operators.ToArray(); 
}
```

### Testing the Extraction of Operators

Here’s how we test whether or not the method works correctly: 
```csharp
static void Main() 
{ 
 char[] operators = ExtractOperators("1+2-7+2-1+28+3+1"); 
 foreach (char oper in operators) 
 { 
  Console.Write("{0} ", oper); 
 } 
} 
```
The output after the program’s execution is correct: 
+ - + - + + + 
We create a test for the border case  when the expression consists of only 
one number and no operators. Just as expected, we get an empty array and 
the output of the above testing program is and empty string.

## Step 3: Evaluating the Expression

When evaluating the expression , we can make use of the fact that the 
numbers’ count is always greater than the operators’ count by one . 
Using a single loop we can evaluate the expression, provided we have the lists 
of numbers and operators: 
```csharp
private static int CalculateExpression(int[] numbers, 
 char[] operators) 
{ 
 int result = numbers[0]; 
 for (int i = 1; i < numbers.Length; i++) 

 { 
  char operation = operators[i - 1]; 
  int nextNumber = numbers[i]; 
  if (operation == '+') 
  { 
   result += nextNumber; 
  } 
  else if (operation == '-') 
  { 
   result -= nextNumber; 
  } 
 } 
 return result; 
}
```

### Test the Evaluation of Expression

We test the method’s execution: 
```csharp
static void Main() 
{ 
 // Expression: 1 + 2 - 3 + 4 
 int[] numbers = new int[] { 1, 2, 3, 4 }; 
 char[] operators = new char[] { '+', '-', '+' }; 
 int result = CalculateExpression(numbers, operators); 
 // Expected result is 4 
 Console.WriteLine(result); 
} 
```
The result seems to be correct: 
4 
We perform few other tests (e.g. a 1, 1+2, 1-1) and it still works correctly.

## Step 4: Console Input

We have to provide the user with the means to enter an expression: 
```csharp
private static string ReadExpression() 
{ 
 Console.Write("Enter expression: "); 
 string expression = Console.ReadLine(); 
 return expression; 
} 

```
This could be left untested, because it is too simple and it will be tested later 
when the entire program is finished and gets tested.

## Step 5: Putting All Together

All that’s left is to integrate all methods so that they can function together. 
```csharp
static void Main() 
{ 
 string expression = ReadExpression(); 
 
 int[] numbers = ExtractNumbers(expression); 
 char[] operators = ExtractOperators(expression); 
 
 int result = CalculateExpression(numbers, operators); 
 Console.WriteLine("{0} = {1}", expression, result); 
}
```

## Testing the Solution

We can use the example from the problem description  when testing the 
solution. We get a correct result: 
Enter expression: 1+2-7+2-1+28+2+3-37+22 
1+2-7+2-1+28+2+3-37+22 = 15  
In order to make sure the solution runs correctly, we must create a few 
more tests with different parameters, which will include the case when the 
expression consists of one number only. 
We can even test with an empty string. It’s not clear if such input is valid, 
but we can take it into account just to be on the safe side. It ’s also ambiguous 
what would happen, if "2 + 3" (with spaces) is entered, instead of "2+3". It’s 
a good idea to cope with these situations. 
One test case we neglected occurs when a number doesn ’t fit in an int 
variable. What do you think would happen, if the user enters the expression 
"11111111111111111111111111111+222222222222222222222222222222"?

## Small Corrections and Repeated Testing

The program will raise an exception (of type System.FormatException or 
System.OverflowException in all likelihood) when the expression is invalid. 
It’s sufficient to catch the exceptions, and when they  are thrown, to report 
that an invali d expression has been entered. Below is the solution ’s full 
implementation after this correction: 

SimpleExpressionEvaluator.cs 
```csharp
using System; 
using System.Collections.Generic; 
using System.Linq; 
 
public class SimpleExpressionEvaluator 
{ 
 private static int[] ExtractNumbers(string expression) 
 { 
  string[] splitResult = expression.Split('+', '-'); 
  int[] resultNumbers = new int[splitResult.Length]; 
  for (int i = 0; i < splitResult.Length; i++) 
  { 
   resultNumbers[i] = int.Parse(splitResult[i]); 
  } 
  return resultNumbers; 
 } 
 
 private static char[] ExtractOperators(string expression) 
 { 
  string operationsCharacters = "+-"; 
  List<char> operators = new();
  foreach (char c in expression) 
  { 
   if (operationsCharacters.Contains(c)) 
   { 
    operators.Add(c); 
   } 
  } 
  return operators.ToArray(); 
 } 
 
 private static int CalculateExpression( 
  int[] numbers, char[] operators) 
 { 
  int result = numbers[0]; 
  for (int i = 1; i < numbers.Length; i++) 
  { 
   char operation = operators[i - 1]; 
   int nextNumber = numbers[i]; 
   if (operation == '+') 
   { 
    result += nextNumber; 

   } 
   else if (operation == '-') 
   { 
    result -= nextNumber; 
   } 
  } 
  return result; 
 } 
 
 private static string ReadExpression() 
 { 
  Console.WriteLine("Enter expression:"); 
  string expression = Console.ReadLine(); 
  return expression; 
 } 
 
 static void Main() 
 { 
```
  try 
```csharp
  { 
   string expression = ReadExpression(); 
 
   int[] numbers = ExtractNumbers(expression); 
   char[] operators = ExtractOperators(expression); 
 
   int result = CalculateExpression(numbers, operators); 
   Console.WriteLine("{0} = {1}", expression, result); 
  } 
```
  catch (Exception ex) 
```csharp
  { 
   Console.WriteLine("Invalid expression!"); 
  } 
 } 
} 
```
To ensure everything works correctly after the fix we need to test the above 
code again  with: single number, two numbers, typical expression (like the 
sample input from the problem description), expression with spaces (e.g. "1 + 
2 -3"), expression with large numbers, invalid expression (e.g. -1).

## Performance Test

Finally we could test with very long expression (performance test), e.g. sum 
of 1,000,000 ones. We could generate a test of 1,000,000 numbers with  the 
following sample code: 

```csharp
static void Main() 
{ 
 StringBuilder expression = new();
 expression.Append("0"); 
 for (int i = 0; i < 1000000; i++) 
 { 
  expression.Append("+"); 
  expression.Append("1"); 
 } 
 string expr = expression.ToString(); 
 int[] numbers = ExtractNumbers(expr); 
 char[] operators = ExtractOperators(expr); 
 int result = CalculateExpression(numbers, operators); 
 Console.WriteLine(result); 
} 
```
The running time seems acceptable and the result is correct. 
But what will happen if we sum 1,000,000 times the value of 5,000,000 ? 
We will get an integer overflow. We might fix this by using long for the sum 
instead of int: 
```csharp
private static long CalculateExpression( 
 int[] numbers, char[] operators) 
{ 
 long result = numbers[0]; 
 for (int i = 1; i < numbers.Length; i++) 
 { 
  char operation = operators[i - 1]; 
  int nextNumber = numbers[i]; 
  if (operation == '+') 
  { 
   result += nextNumber; 
  } 
  else if (operation == '-') 
  { 
   result -= nextNumber; 
  } 
 } 
 return result; 
} 
```
After this small fix we sum 1,000,000 times the value of 5,000,000 and we 
get the correct result: 5,000,000,000,000. The problem is solved.