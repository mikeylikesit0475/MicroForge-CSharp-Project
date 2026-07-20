# Operators

Every programming language uses operators, through which we can perform 
different actions on the data. Let’s take a look at the operators  in C# and see 
what they are for and how they are used.

## What Is an Operator?

After we have learned how to declare and set a variable in the previous 
chapter, we will discuss how to perform various operations with them. For this 
purpose we will get familiar with operators. 
Operators allow processing of primitive data types and objects. They take as 
an input one or more operands and return some value as a result. Operators 
in C# are special character s (such as " +", " .", "^", etc.) and they perform 
transformations on one, two or three operands. Examples of operators in C# 
are the signs for adding, subtracting, multiplication and division from math 
(+, -, *, /) and the operations they perform on the int egers and the real 
numbers.

## Operators in C#

Operators in C# can be separated in several different categories: 
- Arithmetic operators – they are used to perform simple mathematical 
operations. 

- Assignment operators – allow assigning values to variables. 
- Comparison operators – allow comparison of two literals and/or 
variables. 
- Logical operators – operators that work with Boolean data types and 
Boolean expressions. 
- Binary operators – used to perform operations on the binary 
representation of numerical data. 
- Type conversion operators – allow conversion of data from one type to 
another.

### Operator Categories

Below is a list of the operators, separated into categories: 
Category Operators 
arithmetic -, +, *, /, %, ++, -- 
logical &&, ||, !, ^ 
binary &, |, ^, ~, <<, >> 
comparison ==,!=, >, <, >=, <= 
assignment =, +=, -=, *=, /=, %=, &=, |=, ^=, <<=, >>= 
string concatenation + 
type conversion (type), as, is, typeof, sizeof 
other ., new, (), [], ?:, ??

### Types of Operators by Number of Arguments

Operators can be separated into different types according to the number of 
arguments they could take: 
Operator type Number of arguments (operands) 
unary takes one operand 
binary takes two operands 
ternary takes three operands 
All binary operators  in C# are left-associative, i.e. the expressions are 
calculated from left to right, except for the assignment operators. All 
assignment operators and conditional operators ?: and ?? are right-
associative, i.e. the expressions are calculated from right to left. The u nary 
operators are not associative. 
Some of the operators in C# perform different operations on the different 
data types. For example the operator +. When it is used on numeric data 

types (int, long, float, etc.), the operator performs mathematical additio n. 
However, when we use it on strings, the operator concatenates (joins 
together) the content of the two variables/literals and returns the new string.

### Operators – Example

Here is an example of using operators: 
int a = 7 + 9; 
```csharp
Console.WriteLine(a); // 16 
 
string firstName = "John"; 
string lastName = "Doe"; 
 
// Do not forget the space between them 
string fullName = firstName + " " + lastName; 
Console.WriteLine(fullName); // John Doe 
```
The example shows how, as explained above, when the operator + is used on 
numbers it returns a numerical value, and when it is used on strings it returns 
concatenated strings.

## Operator Precedence in C#

Some operators have precedence (priority) over others. For example, in 
math multiplication has precedence over addit ion. The operators with a higher 
precedence are calculated before those with lower. The operator () is used to 
change the precedence and like in math, it is calculated first. 
The following table illustrates the precedence of the operators in C#: 
Priority Operators 
Highest 
priority 
 
 
 
 
 
… 
 
 
 
 
(, ) 
++, -- (as postfix), new, (type), typeof, sizeof 
++, -- (as prefix), +, - (unary), !, ~ 
*, /, % 
+ (string concatenation) 
+, - 
<<, >> 
<, >, <=, >=, is, as 
==, != 
&, ^, | 

Lowest 
priority 
&& 
|| 
?:, ?? 
=, *=, /=, %=, +=, -=, <<=, >>=, &=, ^=, |= 
The operators located upper in the table have higher precedence  than 
those below them, and respectively they have an advantage in the calculation 
of an expression. To change the precedence of an operator we can use 
brackets. 
When we write expressions that are more complex or have many operators, it 
is recommended to use brackets  to avoid difficulties in reading and 
understanding the code. For example: 
// Ambiguous 
x + y / 100 
 
// Unambiguous, recommended 
x + (y / 100)

## Arithmetical Operators

The arithmetical operators in C# +, -, * are the same like the ones in math. 
They perform addition, subtraction and multiplication on numerical values and 
the result is also a numerical value. 
The division operator  / has different effect on integer and real numbers. 
When we divide an integer by an integer (like int, long and sbyte) the 
returned value is an integer ( no rounding, the fractional part is cut ). Such 
division is called an integer division. Example of integer division: 7 / 3 = 2. 
Integer division by 0 is not allowed  and causes a runtime exception 
DivideByZeroException. The remainder of integer division of integers can be 
obtained by the operator %. For example, 7 % 3 = 1, and –10 % 2 = 0. 
When dividing two real numbers or two numbers, one of which is real (e.g. 
float, double, etc.), a real division is done (not integer), and the result is a 
real number with a whole and a fractional part. For example: 5.0 / 2 = 2.5. In 
the division of real n umbers it is allowed to divide by 0.0  and respectively 
the result is +∞ (Infinity), -∞ (-Infinity) or NaN (invalid value). 
The operator for increasing by one  (increment) ++ adds one unit to the 
value of the variable, respectively the operator -- (decrement) subtracts one 
unit from the value. When we use the operators ++ and -- as a prefix (when 
we place them immediately before the variable), the new value is calculated 
first and then the result is returned. When we use the same operators as 
post-fix (meaning when we place them immediately after the variable) the 

original value of the operand is returned first, then the addition or subtraction 
is performed.

### Arithmetical Operators – Example

Here are some examples of arithmetic operators and their effect: 
int squarePerimeter = 17; 
double squareSide = squarePerimeter / 4.0; 
double squareArea = squareSide * squareSide; 
```csharp
Console.WriteLine(squareSide); // 4.25 
Console.WriteLine(squareArea); // 18.0625 
 
int a = 5; 
int b = 4; 
Console.WriteLine(a + b);      // 9 
Console.WriteLine(a + (b++));  // 9 
Console.WriteLine(a + b);      // 10 
Console.WriteLine(a + (++b));  // 11 
Console.WriteLine(a + b);      // 11 
Console.WriteLine(14 / a);     // 2 
Console.WriteLine(14 % a);     // 4 
 
int one = 1; 
int zero = 0; 
// Console.WriteLine(one / zero); // DivideByZeroException 
 
double dMinusOne = -1.0; 
double dZero = 0.0; 
Console.WriteLine(dMinusOne / zero); // -Infinity 
Console.WriteLine(one / dZero); // Infinity
```

## Logical Operators

Logical (Boolean) operators take Boolean values and return a Boolean result 
(true or false). The basic Boolean operators are " AND" (&&), " OR" (||), 
"exclusive OR" (^) and logical negation (!). 
The following table contains the logical operators in C# and th e operations 
that they perform: 
x y !x x && y x || y x ^ y 
true true false true true false 
true false false false true true 
false true true false true true 

false false true false false false 
The table and the following example show that the logical "AND" (&&) returns 
true only when both variables contain truth. Logical "OR" ( ||) returns true 
when at least one of the operands is true. The logical negation operator ( !) 
changes the value of the argument. For example, if the operand has a value 
true and a negation operator is applied, the new value will be false. The 
negation operator is a unary operator and it is placed before the argument. 
Exclusive "OR" (^) returns true if only one of the two operands has the value 
true. If the two operands have different values, exclusive "OR" will return the 
result true, if they have the same values it will return false.

### Logical Operators – Example

The following example illustrates the usage of the logical operators and their 
actions: 
bool a = true; 
bool b = false; 
```csharp
Console.WriteLine(a && b);              // False 
Console.WriteLine(a || b);              // True 
Console.WriteLine(!b);                  // True 
Console.WriteLine(b || true);           // True 
Console.WriteLine((5 > 7) ^ (a == b));  // False
```

### Laws of De Morgan

Logical operations fall under the laws of De Morgan  from the mathematical 
logic: 
!(a && b) == (!a || !b) 
!(a || b) == (!a && !b) 
The first law states that the negation of the conjunction (logical AND) of two 
propositions is equal to the disjunction (logical OR) of their negations. 
The second law states that the negation of the disjunction of both statements 
is equivalent to the conjunction of their negations.

## Operator for Concatenation of Strings

The operator + is used to join strings (string). It c oncatenates (joins) two 
or more strings and returns the result as a new string. If at least one of the 
arguments in the expression is of type string, and there are other operands 
of type different from string, they will be automatically converted to type 
string, which allows successful string concatenation. 
It is fantastic how .NET runtime handles such operation incompatibilities for 
us on the fly, saving us some coding time and allowing us to concentrate on 

the main objectives of our programming task! Howe ver, it is a good practice 
to not miss to cast the variables on which we wish to apply an operation; we 
should instead have them converted to the appropriate type for each 
operation, so that we are in full control of the end result and prevent implicit 
type casts. We will provide more detailed information on casting operations 
further down in the section "Type Conversion" of this chapter.

### Operator for Concatenation of Strings – Example

Here is an example, which shows conc atenations of two strings and a string 
with a number: 
string csharp = "C#"; 
string dotnet = ".NET"; 
string csharpDotNet = csharp + dotnet; 
```csharp
Console.WriteLine(csharpDotNet); // C#.NET 
string csharpDotNet4 = csharpDotNet + " " + 5; 
Console.WriteLine(csharpDotNet4); // C#.NET 5 
In the example we initialize two variables of type string and assign them 
```
values. On the third and fourth row we concatenate both strings and pass the 
```csharp
results to the method Console.WriteLine() to print it on the console. On the 
next line we join the resulting string with a space and the number 5. We 
```
assign the returned value to the variable csharpDotNet5, which will 
automatically be converted to type string. On the last row we print the 
result. 
 
Concatenation (joining, gluing) of strings is a slow operation 
and should be used carefully. It is recommended to use the 
StringBuilder class for iterative (repetitive) operations on 
strings. 
In the chapter " Strings" we will explain in detail why the StringBuilder class 
must be used for join operations on strings performed in a loop.

## Bitwise Operators

A bitwise operator is an operator that acts on the binary representation of 
numeric types. In computers all the data and particularly numerical data is 
represented as a series of ones and zeros. The binary numeral system  is 
used for this purpose. For example, numb er 55 in the binary numeral system 
is represented as 00110111. 
Binary representation  of data is convenient because zero and one in 
electronics can be implemented by Boolean circuits, in which zero is 
represented as "no electricity" or for example with a vo ltage of -5V and the 
one is presented as "have electricity" or say with voltage +5V. 


We will examine in depth the binary numeral system  in the chapter 
"Numeral Systems ", but just for now we can consider that th e numbers in 
computers are represented as ones and zeros, and bitwise operators are used 
to analyze and change those ones to zeros and vice versa. 
Bitwise operators  are very similar to the logical ones. In fact, we can 
imagine that the logical and bitwise operators perform the same thing but 
```csharp
using different data types. Logical operators work with the values true and 
false (Boolean values), while bitwise operators work with numerical values 
```
and are applied bitwise over their binary representation, i.e., they  work with 
the bits of the number (the digits 0 and 1 of which it consists). Just like the 
logical operators in C#, there are bitwise operators "AND" ( &), bitwise "OR" 
(|), bitwise negation (~) and excluding "OR" (^).

### Bitwise Operators and Their Performance

The bitwise operators' performance on binary digits 0 and 1 is shown in the 
following table: 
x y ~x x & y x | y x ^ y 
1 1 0 1 1 0 
1 0 0 0 1 1 
0 1 1 0 1 1 
0 0 1 0 0 0 
As we see bitwise and logical operators are very much alike. The difference in 
the writing of "AND" and "OR" is that the logical operators are written with 
double ampersand (&&) and double vertical bar ( ||), and the bitwise – with a 
single ampersand or vertical bar ( & and |). Bitwise and logical operators for 
exclusive "OR" are the same "^". For logical negation we use " !", while for 
bitwise negation (inversion) the "~" operator is used. 
In programming there are two bitwise operators that have no analogue in 
logical operators. These are the bit shift left  (<<) and bit shift right  (>>). 
Used on numerical values, they move all the bits of the value to the left or 
right. The bits that fall outside the number are lost and replaced with 0. 
The bit shifting operators are used in the following way: on the left side of 
the operator we place the va riable (operand) with which we want to use the 
operator, on the right side we put a numerical value, indicating how many bits 
we want to offset. For example, 3 << 2 means that we want to move the bits 
of the number three, twice to the left. The number 3 pr esented in bits looks 
like this: "0000 0011". When you move twice left, the binary value will look 
like this: "0000 1100", and this sequence of bits is the number 12. If we look 
at the example we can see that actually we have multiplied the number by 4. 
Bit shifting itself can be represented as multiplication (bitwise shifting left) or 
division (bitwise shifting right) by a power of 2. This occurrence is due to the 

nature of the binary numeral system. Example of moving to the right is 6 >> 
2, which means to move the binary number "0000 0110" with two positions to 
the right. This means that we will lose two right -most digits and feed them 
with zeros on the left. The end result will be "0000 0001" which is 1.

### Bitwise Operators – Example

Here is an example of using bitwise operators. The binary representation of 
the numbers and the results of the bitwise operators are shown in the 
comments (green text): 
byte a = 3;                 // 0000 0011 = 3 
byte b = 5;                 // 0000 0101 = 5 
 
```csharp
Console.WriteLine(a | b);   // 0000 0111 = 7 
Console.WriteLine(a & b);   // 0000 0001 = 1 
Console.WriteLine(a ^ b);   // 0000 0110 = 6 
Console.WriteLine(~a & b);  // 0000 0100 = 4 
Console.WriteLine(a << 1);  // 0000 0110 = 6 
Console.WriteLine(a << 2);  // 0000 1100 = 12 
Console.WriteLine(a >> 1);  // 0000 0001 = 1 
```
In the example we first create and initialize the values of two variables a and 
b. Then we print on the console the results of some bitwise operations on the 
two variables. The first operation that we apply is "OR". The example shows 
that for all positions where there was 1 in the binary representation of the 
variables a and b, there is also 1 in the result. The second operation is "AND". 
The result of the operation contains 1 only in the right -most bit, because the 
only place where a and b have 1 at the same time is their right -most bit. 
Exclusive "OR" returns ones only in positions whe re a and b have different 
values in their binary bits. Finally, the logical negation and bitwise shifting: 
left and right, are illustrated.

## Comparison Operators

Comparison operators in C# are used to compare two or more operands. C# 
supports the following comparison operators: 
- greater than (>) 
- less than (<) 
- greater than or equal to (>=) 
- less than or equal to (<=) 
- equality (==) 
- difference (!=) 

All comparison operators in C# are binary (take two operands) and the 
returned result is a Boolean value ( true or false). Comparison operators 
have lower priority than arithmetical operators but higher than the 
assignment operators.

### Comparison Operators – Example

The following example demonstrates the usage of comparison operators in 
C#: 
int x = 10, y = 5; 
```csharp
Console.WriteLine("x > y : " + (x > y));    // True 
Console.WriteLine("x < y : " + (x < y));    // False 
Console.WriteLine("x >= y : " + (x >= y));  // True 
Console.WriteLine("x <= y : " + (x <= y));  // False 
Console.WriteLine("x == y : " + (x == y));  // False 
Console.WriteLine("x != y : " + (x != y));  // True 
```
In the example, first we create two variables x and y and we assign them the 
values 10 and 5. On the next line we print on the console using the method 
```csharp
Console.WriteLine(…) the result from comparing the two variables x and y 
using the operator >. The returned value is true because x has a greater 
```
value than y. Similarly, in the next rows the results from the other 5 
comparison operators, used to compare the variables x and y, are printed.

## Assignment Operators

The operator for assigning value to a variable  is "=" (the character for 
mathematical equation). The syntax used for assigning value is as it follows:  
operand1 = literal, expression or operand2;

### Assignment Operators – Example

Here is an example to show the usage of the assignment operator: 
int x = 6; 
string helloString = "Hello string."; 
int y = x; 
In the example we assign value 6 to the variable x. On the second line we 
assign a text literal to the variable helloString, and on the third line we 
copy the value of the variable x to the variable y.

### Cascade Assignment

The assignment operator can be used in cascade (more than once in the 
same expression). In this case assignments are carried out consecutively from 
right to left. Here’s an example: 
int x, y, z; 
x = y = z = 25; 
On the first line in the example we initialize three variables and on the second 
line we assign them the value 25. 
 
The assignment operator in C# is " =", while the comparison 
operator is " ==". The exchange of the two operators is a 
common error when we are writing code. Be careful not to 
confuse the comparison operator and the assignment 
operator as they look very similar.

### Compound Assignment Operators

Except the assignment operator there are also compound assignment 
operators. They help to reduce the volume of the code by typing two 
operations together with an operator: operation and assignment. Compound 
operators have the following syntax: 
operand1 operator = operand2; 
The upper expression is like the following: 
operand1 = operand1 operator operand2; 
Here is an example of a compound operator for assignment: 
int x = 2; 
int y = 4; 
 
x *= y; // Same as x = x * y; 
```csharp
Console.WriteLine(x); // 8 
The most commonly used compound assignment operators are += (adds value 
of operand2 to operand1), -= (subtracts the value of the right operand from 
the value of the left one).Other compound assignment operators are *=, /= 
and %=. 
```
The following example gives a good idea of how the compound assignment 
operators work: 
int x = 6; 


int y = 4; 
 
```csharp
Console.WriteLine(y *= 2);  // 8 
int z = y = 3;              // y=3 and z=3 
 
Console.WriteLine(z);       // 3 
Console.WriteLine(x |= 1);  // 7 
Console.WriteLine(x += 3);  // 10 
Console.WriteLine(x /= 2);  // 5 
```
In the example, first we create the variables x and y and assign them values 
6 and 4. On the next line we print on the console y, after we have assigned it 
a new value using the operator *= and the literal 2.The result of the operation 
is 8. Further in the  example we apply the other compound assignment 
operators and print the result on the console.

## Conditional Operator ?:

The conditional operator ?: uses the Boolean value of an expression to 
determine which of two other expressions must be calculated and returned as 
a result. The operator works on three operands and that is why it is called 
ternary operator. The character " ?" is placed between the fi rst and second 
operand, and ":" is placed between the second and third operand. The first 
operand (or expression) must be Boolean, and the next two operands must 
be of the same type, such as numbers or strings. 
The operator ?: has the following syntax: 
operand1 ? operand2 : operand3 
It works like this: if operand1 is set to true, the operator returns as a result 
operand2. Otherwise (if operand1 is set to false), the operator returns as a 
result operand3. 
During the execution, the value of the first argumen t is calculated. If it has 
value true, then the second (middle) argument is calculated and it is 
returned as a result. However, if the calculated result of the first argument is 
false, then the third (last) argument is calculated and it is returned as a 
result.

### Conditional Operator "?:" – Example

The following example shows the usage of the operator "?:": 
int a = 6; 
int b = 4; 
```csharp
Console.WriteLine(a > b ? "a>b" : "b<=a"); // a>b 

int num = a == b ? 1 : -1; // num will have value -1
```

## Other Operators

So far we have examined arithmetic, logical and bitwise operators, the 
operator for concatenating strings, also the conditional operator ?:. Besides 
them in C # there are several other operators worth mentioning.

### The "." Operator

The access operator  "." (d ot) is used to access the member fields or 
methods of a class or object. Example of usage of point operator: 
```csharp
Console.WriteLine(DateTime.Now); // Prints the date + time
```

### Square Brackets [] Operator

Square brackets [] are used to access elements of an array by index , 
they are the so -called indexer. Indexers are also used for accessing 
characters in a string. Example: 
int[] arr = { 1, 2, 3 }; 
```csharp
Console.WriteLine(arr[0]); // 1 
string str = "Hello"; 
Console.WriteLine(str[1]); // e
```

### Brackets () Operator

Brackets () are used to override the priority of execution  of expressions 
and operators. We have already seen how the brackets work.

### Type Conversion Operator

The operator for type conversion (type) is used to convert a variable from 
one type to another. We will examine it in details in the section " Type 
Conversion".

### Operator "as"

The operator as also is used for type conversion  but invalid conversion 
returns null, not an exception.

### Operator "new"

The new operator is used to create and initialize new objects . We will 
examine it in details in the chapter "Creating and Using Objects".

### Operator "is"

The is operator is used to check whether an object is compatible with a given 
type (check object's type).

### Operator "??"

The operator ?? is similar to the conditional operator ?:. The difference is that 
it is placed between two operands and returns the left operand only if its 
value is not null, otherwise it returns the right operand. Example: 
int? a = 5; 
```csharp
Console.WriteLine(a ?? -1); // 5 
string name = null; 
Console.WriteLine(name ?? "(no name)"); // (no name)
```

### Other Operators – Examples

Here is an example that shows the operators we just explained: 
int a = 6; 
int b = 3; 
 
```csharp
Console.WriteLine(a + b / 2);                    // 7 
Console.WriteLine((a + b) / 2);                  // 4 
 
string s = "Beer"; 
Console.WriteLine(s is string);                  // True 
 
string notNullString = s; 
string nullString = null; 
Console.WriteLine(nullString ?? "Unspecified");  // Unspecified 
Console.WriteLine(notNullString ?? "Specified"); // Beer
```