# Type Conversion and Casting

Generally, operators work over arguments with the same data type. However, 
C# has a wide variety of data types from which we can choose the most 
appropriate for a particular purpose. To perform an operation on variables of 
two different data types we need to convert both to the same data type. Type 
conversion (typecasting) can be explicit and implicit. 
All expressions in C# have a type. This type can derive from the expression 
structure and the types, variables and literals used in it. It is possible to wri te 
an expression which type is inappropriate for the current context. In some 
cases this will lead to a compilation error, but in other cases the context can 
get a type that is similar or related to the type of the expression. In this case 
the program performs a hidden type conversion. 

Specific conversion from type S to type T allows the expression of type S to be 
treated as an expression of type T during the execution of the program. In 
some cases this will require a validation of the transformation. Here are some 
examples: 
- Conversion of type object to type string will require verification at 
runtime to ensure that the value is really an instance of type string. 
- Conversion from string to object does not require any verification. The 
type string is an inheritor of the type object and can be converted to 
its base class without a risk of an error or data loss. We shall examine 
inheritance in details in the chapter "Object-Oriented Programmin g 
Principles". 
- Conversion of type int to long can be made without verification during 
the execution, because there is no risk of data loss since the set of 
values of type int is a subset of values of type long. 
- Conversion from type double to long requires conversion of 64 -bit 
floating-point value to 64 -bit integer. Depending on the value, data loss 
is possible and therefore it is necessary to convert the types explicitly. 
In C# not all types can be converted to all other types, but only to some of 
them. For convenience, we shall group some of the possible transformations 
in C# according to their type into three categories: 
- implicit conversion; 
- explicit conversion; 
- conversion to or from string;

## Implicit Type Conversion

Implicit (hidden) type conversion is possible only when there is no risk of data 
loss during the conversion, i.e. when converting from a lower range type to a 
larger range (e.g. from int to long). To make an implicit conversion it is not 
necessary to use any operator and therefore such transform ation is called 
implicit. The implicit conversion is done automatically by the compiler when 
you assign a value with lower range to a variable with larger range or if the 
expression has several types with different ranges. In such case the 
conversion is executed into the type with the highest range.

### Implicit Type Conversion – Examples

Here is an example of implicit type conversion: 
int myInt = 5; 
```csharp
Console.WriteLine(myInt); // 5 
long myLong = myInt; 
Console.WriteLine(myLong); // 5 

Console.WriteLine(myLong + myInt); // 10 
In the example we create a variable myInt of type int and assign it the value 
```
5. After that we create a variable myLong of type long and assign it the value 
contained in myInt. The value stored in myLong is automatically converted 
from type int to type long. Finally, we output the result from adding the two 
variables. Because the variables are from different types they are 
automatically converted to the type with the greater range, i.e. to type long 
and the result that is printed on the console is long again. Indeed, the given 
```csharp
parameter to the method Console.WriteLine() is of type long, but inside 
```
the method it will be converted again, this time to type string, so it can be 
printed on the console. This transformation is performed by the method 
Long.ToString().

### Possible Implicit Conversions

Here are some possible implicit conversions of primitive data types in C#:  
- sbyte → short, int, long, float, double, decimal; 
- byte → short, ushort, int, uint, long, ulong, float, double, 
decimal; 
- short → int, long, float, double, decimal; 
- ushort → int, uint, long, ulong, float, double, decimal; 
- char → ushort, int, uint, long, ulong, float, double, decimal 
(although char is a character type in some cases it may be regarded as 
a number and have a numeric type of behavior, it can even participate 
in numeric expressions); 
- uint → long, ulong, float, double, decimal; 
- int → long, float, double, decimal; 
- long → float, double, decimal; 
- ulong → float, double, decimal; 
- float → double. 
There is no data loss when converting types of smaller range to types 
with a larger range . The numerical value remains the same after 
conversion. There are a few exceptions. When you convert type int to type 
float (32-bit values), the difference is that int uses all bits for a whole 
number, whereas float has a part of bits used for representation of a 
fractional part. Hence, loss of precision is possible because of rounding when 
conversion from int to float is made. The same applies for the conversion of 
64-bit long to 64-bit double.

## Explicit Type Conversion

Explicit type conversion is used whenever there is a possibility of data loss. 
When converting floating point type to integer type there is always a loss of 
data coming from the elimination of the fractional part and an explicit 
conversion is obligatory (e.g. double to long). To make such a conversion it 
is necessary to use the operator for data conversion ( type). There may also 
be data loss when con verting a type with a wider range to type with a 
narrower one (double to float or long to int).

### Explicit Type Conversion – Example

The following example illustrates the use of explicit type conversion and data 
loss that may occur in some cases: 
double myDouble = 5.1d; 
```csharp
Console.WriteLine(myDouble); // 5.1 
 
long myLong = (long)myDouble; 
Console.WriteLine(myLong); // 5 
 
myDouble = 5e9d; // 5 * 10^9 
Console.WriteLine(myDouble); // 5000000000 
 
int myInt = (int)myDouble; 
Console.WriteLine(myInt); // -2147483648 
Console.WriteLine(int.MinValue); // -2147483648 
```
In the first line of the example we assign a value 5.1 to the variable 
myDouble. After we convert (explicitly) to type long using the operator 
(long) and print on the console the variable myLong we see that the variable 
has lost its fractional part, because long is an integer. Then we assign to the 
real double precision variable myDouble the value 5 billion. Finally, we convert 
myDouble to int by the operator (int) and print variable myInt. The result is 
the same like when we print int.MinValue because myDouble contains a 
value bigger than the range of int. 
 
It is not always possible to predict what the value of a 
variable will be after its scope overflows! Therefore, use 
sufficiently large types  and be careful when switching to a 
"smaller" type.

### Data Loss during Type Conversion

We will give an example for data loss during type conversion: 
long myLong = long.MaxValue; 


int myInt = (int)myLong; 
 
```csharp
Console.WriteLine(myLong); // 9223372036854775807 
Console.WriteLine(myInt); // -1 
```
The type conversion operator may also be used in case of an intentional 
implicit conversion. This contributes to the readability of code, reducing the 
chance for errors and it is considered good practice by many programmers. 
Here are some more examples for type conversions: 
float heightInMeters = 1.74f; // Explicit conversion 
double maxHeight = heightInMeters; // Implicit 
double minHeight = (double)heightInMeters; // Explicit 
float actualHeight = (float)maxHeight; // Explicit 
 
float maxHeightFloat = maxHeight; // Compilation error! 
In the example above at the last line we have an expression that will generate 
a compilation error. This is because we try implicitly to convert type double to 
float, which can cause data loss. C# is a strongly typed programming 
language and does not allow such appropriation of values.

### Forcing Overflow Exceptions during Casting

Sometimes it is convenient, instead of getting the wrong result, when a type 
overflows during switching from larger to smaller type, to get notification of 
the problem. This is done by the keyword checked which includes a check for 
overflow in integer types: 
double d = 5e9d; // 5 * 10^9 
```csharp
Console.WriteLine(d); // 5000000000 
int i = checked((int)d); // System.OverflowException 
Console.WriteLine(i); 
```
During the execution of the code fragment above an exception (i.e. 
notification of an error) of type OverflowException is raised. More 
information about the exceptions and the methods to catch and handle them  
can be found in the chapter "Exception Handling".

### Possible Explicit Conversions

The explicit conversions between numeral types in C# are possible between 
any couple among the following types: 
sbyte, byte, short, ushort, char, int, uint, long, ulong, float, double, 
decimal 

In these conversions data can be lost, like data about the number size or 
information about its precision. 
Notice that conversion to or from string is not possible through typecasting.

## Conversion to String

If it is necessary we can convert any type of data, including the value null, to 
string. The conversion of strings is done automatically whenever you use the 
concatenation operator (+) and one of the arguments is not of type string. In 
this case the argument is converted to a string  and the operator returns a 
new string representing the concatenation of the two strings. 
Another way to convert different objects to type string is to call the method 
ToString() of the variable or the value. I t is valid for all data types in .NET 
Framework. Even calling 3.ToString() is fully valid in C# and the result will 
return the string "3".

### Conversion to String – Example

Let’s take a look on several examples for converting different data types to 
string: 
int a = 5; 
int b = 7; 
 
string sum = "Sum = " + (a + b); 
```csharp
Console.WriteLine(sum); 
 
String incorrect = "Sum = " + a + b; 
Console.WriteLine(incorrect); 
 
Console.WriteLine( 
 "Perimeter = " + 2 * (a + b) + ". Area = " + (a * b) + "."); 
```
The result from the example is as follows: 
Sum = 12 
Sum = 57 
Perimeter = 24. Area = 35. 
From the results it is obvious, that concatenating a number to a character 
string returns in result the string followed by the text representation of the 
number. Note that the " +" for concatenating strings can cause unpleasant 
effects on the addition of numbers, because it has equal priority with the 
operator "+" for mathematical addition. Unless the priorities of the operations 
are changed by placing the brackets, they will alway s be executed from left to 
right. 

More details about converting from and to string we will look at the chapter 
"Console Input and Output".