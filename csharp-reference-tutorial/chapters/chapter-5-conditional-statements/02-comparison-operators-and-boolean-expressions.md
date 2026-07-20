# Comparison Operators and Boolean Expressions

In the following section we will recall the basic comparison operators in the 
C# language. They are important, because we use them to describe 
conditions in our conditional statements.

## Comparison Operators

There are several comparisons operator s in C#, which are used to compare 
pairs of integers, floating-point numbers, characters, strings and other types: 
Operator Action 
== Equal to 
!= Not equal to 
> Greater than 
>= Greater than or equal to 
< Less than 
<= Less than or equal to 
Comparison operators can be used to compare expressions such as two 
numbers, two numerical expressions, or a number and a variable. The result 
of the comparison is a Boolean value (true or false). 
Let’s look at an example of using comparisons: 

int weight = 700; 
```csharp
Console.WriteLine(weight >= 500); // True 
 
char gender = 'm'; 
Console.WriteLine(gender <= 'f'); // False 
 
double colorWaveLength = 1.630; 
Console.WriteLine(colorWaveLength > 1.621); // True 
 
int a = 5; 
int b = 7; 
bool condition = (b > a) && (a + b < a * b); 
Console.WriteLine(condition); // True 
 
Console.WriteLine('B' == 'A' + 1); // True 
```
In the sample code we perform a comparison between numbers and between 
characters. The numbers are compared by size while characters are compared 
by their lexicographical order (the operation uses the Unicode numbers for the 
corresponding characters). 
As seen in the example, the type char behaves like a number  and can be 
subtracted, added and compared to numbers freely. However, this should be 
used cautiously as it could make the code difficult to read and understand. 
By running the example we will produce the following output: 
True 
False 
True 
True 
True 
In C# several types of data that can be compared: 
- numbers (int, long, float, double, ushort, decimal, …) 
- characters (char) 
- Booleans (bool) 
- References to objects, also known as object pointers ( string, object, 
arrays and others) 
Every comparison can affect two nu mbers, two bool values, or two object 
references. It is allowed to compare expressions of different types , like 
an integer with a floating -point number for example. However, not every pair 
of data types can be compared directly. For example, we cannot compare a 
string with a number.

## Comparison of Integers and Characters

When comparing integers and characters, we directly compare their binary 
representation in memory i.e. we compare their values. For example, if we 
compare two numbers of type int, we will co mpare the values of their 
respective series of 4 bytes. Here is one example for integer and character 
comparisons: 
```csharp
Console.WriteLine("char 'a' == 'a'? " + ('a' == 'a'));  // True 
Console.WriteLine("char 'a' == 'b'? " + ('a' == 'b'));  // False 
Console.WriteLine("5 != 6? " + (5 != 6));               // True 
Console.WriteLine("5.0 == 5L? " + (5.0 == 5L));         // True 
Console.WriteLine("true == false? " + (true == false)); // False 
```
The result of the example is as follows: 
char 'a' == 'a'? True 
char 'a' == 'b'? False 
5 != 6? True 
5.0 == 5L? True 
true == false? False

## Comparison of References to Objects

In .NET Framework there are reference data types that do not contain their 
value (unlike the value types), but contain the address of the memory in the  
heap where their value is located. Strings, arrays and classes are such types. 
They behave like a pointer to some value and can have the value null, i.e. no 
value. When comparing reference type variables, we compare the 
addresses they hold, i.e. we check whether they point to the same location 
in the memory, i.e. to the same object. 
Two object pointers (references) can refer to the same object or to different 
objects, or one of them can point to nowhere (to have null value). In the 
following example we cre ate two variables that point to the same value 
(object) in the heap. 
string str = "beer"; 
string anotherStr = str; 
After executing the source code above, the two variables str and anotherStr 
will point to the same object ( string with value "beer"), which is located at 
some address in the heap (managed heap). 
We can check whether the variables point to the same object  with the 
comparison operator ( ==). For most reference types this operator does not 
compare the content of the objects but rather checks if they point at the same 

location in memory, i.e. if they are one and the same object. The size 
comparisons (<, >, <= and >=) are not applicable for object type variables. 
The following example illustrates the comparison of references to objects: 
string str = "beer"; 
string anotherStr = str; 
string thirdStr = "bee"; 
thirdStr = thirdStr + 'r'; 
```csharp
Console.WriteLine("str = {0}", str); 
Console.WriteLine("anotherStr = {0}", anotherStr); 
Console.WriteLine("thirdStr = {0}", thirdStr); 
Console.WriteLine(str == anotherStr); // True - same object 
Console.WriteLine(str == thirdStr); // True - equal objects 
Console.WriteLine((object)str == (object)anotherStr); // True 
Console.WriteLine((object)str == (object)thirdStr); // False 
If we execute the sample code, we will get the following result: 
str = beer 
anotherStr = beer 
thirdStr = beer 
```
True 
True 
True 
False 
Because the strings used in the example (instances of the class 
System.String, defined by the keyword string in C#) are of reference type, 
their values are set as objects in the heap. The two objects str and thirdStr 
have equal values, but are different objects, located at separate addresses in 
the memory. The variable anotherStr is also reference type and gets the 
address (the reference) of str, i.e. points to the existing object str. So by 
the comparison of the variables str and anotherStr, it appears that they are 
one and the same object and are equal. The result of the comparison between 
str and thirdStr is also equality, because the operator  == compares the 
strings by value and not by address (a very useful exception to the rule for 
comparison by address). However, if we convert the three variables to objects 
and then compare them, we will get a comparison of the addresses in the 
heap where their values are located and the result will be different. 
This above example shows that the operator == has a special behavior 
when comparing strings, but for the rest of the reference types (like arrays 
or classes) it applies comparison by address. 
You will learn more about the class String and the comparison of strings in 
the chapter about "Strings".

## Logical Operators

Let’s recall the logical operators in C#. They are often used to construct 
logical (Boolean) expressions. The logical operators are: &&, ||, ! and ^.

### Logical Operators && and ||

The logical operators && (logical AND) and || (logical OR) are only used on 
Boolean expressions (values of type bool). In order for the result – of 
comparing two expressions with the operator && – to be true (true), both 
operands must have the value true. For instance: 
bool result = (2 < 3) && (3 < 4); 
This expression is "true", because both the operands: (2 < 3) and (3 <  4) are 
"true". The logical operator && is also called short-circuit, because it does 
not lose time in additional unnecessary calculations. It evaluates the left part 
of the expression (the first operand) and if the result is false, it does not lose 
time for evaluating the second operand – it’s not possible the end result to be 
"true" when the first operand is not "true". For this reason it is also called 
short-circuit logical operator "and". 
Similarly, the operator || returns true if at least one of the tw o operands has 
the value "true". Example: 
bool result = (2 < 3) || (1 == 2); 
This example is "true", because its first operand is "true". Just like the && 
operator, the calculation is done fast – if the first operand is true, the second 
is not calculated at all, as the result is already known. It is also called short-
circuit logical operator "or".

### Logical Operators & and |

The operators for comparison & and | are similar to && and ||, respectively. 
The difference lies in the fact that both operands are calculated one after the 
other, although the final result is known in advance. That ’s why these 
comparison operators are also known as full-circuit logical operators  and 
are used very rarely. 
For instance, when two operands are compared with & and the firs t one is 
evaluated "false", the calculation of the second operand is still executed. The 
result is clearly "false". Likewise, when two operands are compared with | and 
the first one is "true", we still evaluate the second operand and the final result 
is nevertheless "true". 
We must not confuse the Boolean operators & and | with the bitwise 
operators & and |. Although they are written in the same way, they take 
different arguments (Boolean or integer expressions) and return different 
result (bool or integer) and their actions are not identical.

### Logical Operators ^ and !

The ^ operator, also known as exclusive OR (XOR) , belongs to the full -
circuit operators, because both operands are calculated one after the other. 
The result of applying the operator is true if exactly one of the operands 
is true, but not both simultaneously . Otherwise the result is false. Here 
is an example: 
```csharp
Console.WriteLine("Exclusive OR: "+ ((2 < 3) ^ (4 > 3))); 
```
The result is as follows: 
Exclusive OR: False 
The previous expression is evaluated as false, because both operands: (2 <3) 
and (4 > 3) are true. 
The operator ! returns the reversed value of the  Boolean expression to 
which it is attached. Example: 
bool value = !(7 == 5); // True 
```csharp
Console.WriteLine(value); 
```
The above expression can be read as "the opposite of  the truth of the phrase 
"7 == 5". The result of this pattern is True (the opposite of False). Note that 
when we print the value true it is displayed on the console as " True" (with 
capital letter). This "defect" comes from the VB .NET language that also runs 
in .NET Framework.