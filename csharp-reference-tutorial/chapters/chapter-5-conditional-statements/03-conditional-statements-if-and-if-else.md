# Conditional Statements "if" and "if-else"

After reviewing how to compare expressions, we will continue with conditional 
statements, which will allow us to implement programming logic. 
Conditional statements if and if-else are conditional control statements. 
Because of them the program can behave  differently based on  a defined 
condition checked during the execution of the statement.

## Conditional Statement "if"

The main format of the conditional statements if is as follows: 
if (Boolean expression) 
```csharp
{ 
 Body of the conditional statement; 
} 
It includes: if-clause, Boolean expression and body of the conditional 
```
statement. 

The Boolean expression  can be a Boolean variable or Boolean logic al 
expression. Boolean expressions cannot be integer (unlike other programming 
languages like C and C++). 
The body of the statement  is the part locked between the curly brackets: 
{}. It may consist of one or more operations (statements). When there are 
several operations, we have a comp lex block operator, i.e. series of 
commands that follow one after the other, enclosed in curly brackets. 
The expression in the brackets which follows the keyword if must return the 
Boolean value true or false. If the expression is calculated to the value 
true, then the body of a conditional statement is executed. If the result is 
false, then the operators in the body will be skipped.

### Conditional Statement "if" – Example

Let’s take a look at an example of using a conditional statement if: 
```csharp
static void Main() 
{ 
 Console.WriteLine("Enter two numbers."); 
 Console.Write("Enter first number: "); 
 int firstNumber = int.Parse(Console.ReadLine()); 
 Console.Write("Enter second number: "); 
 int secondNumber = int.Parse(Console.ReadLine()); 
 int biggerNumber = firstNumber; 
 if (secondNumber > firstNumber) 
 { 
  biggerNumber = secondNumber; 
 } 
 Console.WriteLine("The bigger number is: {0}", biggerNumber); 
} 
If we start the example and enter the numbers 4 and 5 we will get the 
```
following result: 
Enter two numbers. 
Enter first number: 4 
Enter second number: 5 
The bigger number is: 5

### Conditional Statement "if" and Curly Brackets

If we have only one operator in the body of the if-statement, the curly 
brackets denoting the body of the conditional operator may be omitted, as 
shown below. However, it is a good practice to use them even if we have only 
one operator. This will make the code is more readable. 
Here is an example of omitting the curly brackets which leading to confusion:  

int a = 6; 
if (a > 5) 
```csharp
 Console.WriteLine("The variable is greater than 5."); 
 Console.WriteLine("This code will always execute!"); 
// Bad practice: misleading code 
```
In this example the code is misleadingly formatted and creates the impression 
that both printing statements are part of the body of the if-block. In fact, 
this is true only for the first one. 
 
Always put curly brackets { } for the body of “if” blocks even 
if they consist of only one operator!

## Conditional Statement "if-else"

In C#, as in most of the programming languages there is a conditional 
statement with else clause: the if-else statement. Its format is the 
following: 
if (Boolean expression) 
```csharp
{ 
 Body of the conditional statement; 
} 
else 
{ 
 Body of the else statement; 
} 
The format of the if-else structure consists of the reserved word if, 
Boolean expression, body of a conditional statement, reserved word else and 
else-body statement. The body of else-structure may consist of one or more 
```
operators, enclosed in curly bracke ts, same as the body of a conditional 
statement. 
This statement works as follows: the expression in the brackets (a Boolean 
expression) is calculated. The calculation result must be Boolean – true or 
false. Depending on the result there are two possible ou tcomes. If the 
Boolean expression is calculated to true, the body of the conditional 
statement is executed and the else-statement is omitted and its operators 
do not execute. Otherwise, if the Boolean expression is calculated to false, 
the else-body is exe cuted, the main body of the conditional statement is 
omitted and the operators in it are not executed.

### Conditional Statement "if-else" – Example

Let’s take a look at the next example and illustrate how the if-else 
statement works: 


```csharp
static void Main() 
{ 
 int x = 2; 
 if (x > 3) 
 { 
  Console.WriteLine("x is greater than 3"); 
 } 
 else 
 { 
  Console.WriteLine("x is not greater than 3"); 
 } 
} 
The program code can be interpreted as follows: if x>3, the result at the end 
```
is: "x is greater than 3 ", otherwise (else) the result is: " x is not greater 
than 3 ". In this case, since x=2, after the calculation of the Boolean 
expression the operator of the else structure will be executed . The result of 
the example is: 
x is not greater than 3 
The following scheme illustrates the process flow of this example:

## Nested "if" Statements

Sometimes the programming logic in a program or an application needs to be 
represented by multiple if-structures contained in each other. We call them 
nested if or nested if-else structures. 
We call nesting the placement of an if or if-else structure in the body of 
another if or else structure. In such situations every else clause 
corresponds to the closest previous if clause. This is how we understand 
which else clause relates to which if clause. 
It’s not a good practice to exceed three nested levels, i.e. we should not nest 
more than three conditional statements into one another. If for some reason 
we need to nest more than three structures, we should export a part of the 
code in a separate method (see chapter Methods).

### Nested "if" Statements – Example

Here is an example of using nested if structures: 
int first = 5; 
int second = 3; 
 
if (first == second) 
```csharp
{ 
 Console.WriteLine("These two numbers are equal."); 
} 
else 
{ 
 if (first > second) 
 { 
  Console.WriteLine("The first number is greater."); 
 } 
 else 
 { 
  Console.WriteLine("The second number is greater."); 
 } 
} 
```
In the example above we have two numbers and compare them in two steps: 
first we compare whether they are equal and if not, we compare again, to 
determine which one is the greater. Here is the result of the execution of the 
code above: 
The first number is greater.

## Sequences of "if-else-if-else-…"

Sometimes we need to use a sequence of if structures, where the else 
clause is a new if structure. If we use nested if structures, the code would 
be pushed too far to the right. That ’s why in such situations it is allowed to 
use a new if right after the else. It’s even considered a good practice. Here 
is an example: 
char ch = 'X'; 
if (ch == 'A' || ch == 'a') 
```csharp
{ 
 Console.WriteLine("Vowel [ei]"); 
} 
else if (ch == 'E' || ch == 'e') 
{ 
 Console.WriteLine("Vowel [i:]"); 
} 
else if (ch == 'I' || ch == 'i') 
{ 
 Console.WriteLine("Vowel [ai]"); 
} 
else if (ch == 'O' || ch == 'o') 
{ 
 Console.WriteLine("Vowel [ou]"); 
} 
else if (ch == 'U' || ch == 'u') 
{ 
 Console.WriteLine("Vowel [ju:]"); 
} 
else 
{ 
 Console.WriteLine("Consonant"); 
} 
```
The program in the example makes a series of comparisons of a variable to 
check if it is one of the vowels from the English alphabet. Every following 
comparison is done only in case that the previous comparison was not true. In 
the end, if none of the if-conditions is not fulfilled, the last else clause is 
executed. Thus, the result of the example is as follows: 
Consonant

## Conditional "if" Statements – Good Practices

Here are some guidelines, which we recommend for writing if, structures: 

- Use blocks, surr ounded by curly brackets {} after if and else in order 
to avoid ambiguity 
- Always format the code correctly by offsetting it with one tab inwards 
after if and else, for readability and avoiding ambiguity. 
- Prefer switch-case structure to of a series of if-else-if-else-… 
structures or nested if-else statement, if possible. The construct 
switch-case we will cover in the next section.