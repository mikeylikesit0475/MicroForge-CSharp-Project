# Data Types

Data types are sets (ranges) of values that have similar characteristics. For 
instance byte type specifies the set of integers in the range of [0 … 255].

## Characteristics

Data types are characterized by: 
- Name – for example, int; 
- Size (how much memory they use) – for example, 4 bytes; 
- Default value – for example 0.

## Types

Basic data types in C# are distributed into the following types: 
- Integer types – sbyte, byte, short, ushort, int, uint, long, ulong; 
- Real floating-point types – float, double; 
- Real type with decimal precision – decimal; 
- Boolean type – bool; 
- Character type – char; 
- String – string; 
- Object type – object. 
These data types are called primitive (built -in types) , be cause they are 
embedded in C# language at the lowest level. The table below represents the 
above mentioned data types, their range and their default values: 
Data 
Types 
Default 
Value Minimum Value Maximum Value 
sbyte 0 -128 127 
byte 0 0 255 
short 0 -32768 32767 
ushort 0 0 65535 
int 0 -2147483648 2147483647 
uint 0u 0 4294967295 
long 0L -9223372036854775808 9223372036854775807 
ulong 0u 0 18446744073709551615 
float 0.0f ±1.5×10-45 ±3.4×1038 
double 0.0d ±5.0×10-324 ±1.7×10308 
decimal 0.0m ±1.0×10-28 ±7.9×1028 
bool false Two possible values: true and false 
char '\u0000' '\u0000' '\uffff' 
object null - - 
string null - -

## Correspondence between C# and .NET Types

Primitive data types in C# have a direct correspondence with the types of the 
common type system (CTS) in .NET Framework. For instance, int type in C# 
corresponds to System.Int32 type in CTS and to Integer type in  VB.NET 
language, while long type in C# corresponds to System.Int64 type in CTS 
and to Long type  in VB.NET language. Due to the common types system 
(CTS) in .NET Framework there is compatibility between different prog -
ramming languages (like for instance, C#, Managed C++, VB.NET and F #). 
For the same reason int, Int32 and System.Int32 types in C# are actually 
different aliases for one and the same data type – signed 32-bit integer.

## Integer Types

Integer types  represent integer numbers and are sbyte, byte, short, 
ushort, int, uint, long and ulong. Let’s examine them one by one. 
The sbyte type is an 8-bit signed integer . This means that the number of 
possible values for it is 2 8, i.e. 256 values altogether, and they can be both, 
positive and negative. The minimum value that can be stored in sbyte is 
SByte.MinValue = -128 (-27), and the maximum value is SByte.MaxValue = 
127 (27-1). The default value is the number 0. 
The byte type is an 8-bit unsigned integer  type. It also has 256 different 
integer values (2 8) that can only be nonnegative. I ts default value is the 
number 0. The minimal taken value is Byte.MinValue = 0, and the maximum 
is Byte.MaxValue = 255 (28-1). 
The short type is a 16-bit signed integer . Its minimal value is 
Int16.MinValue = -32768 ( -215), and the maximum is Int16.MaxValue = 
32767 (215-1). The default value for short type is the number 0. 
The ushort type is 16-bit unsigned integer. The minimum value that it can 
store is UInt16.MinValue = 0, and the minimum value is – 
UInt16.MaxValue = 65535 (216-1). Its default value is the number 0. 
The next integer type that we will consider is int. It is a 32-bit signed 
integer. As we can notice, the growth of bits increases the possible values 
that a type can store. The default value for int is 0. Its minimal value is 
Int32.MinValue = -2,147,483,648 ( -231), and its maximum value is 
Int32.MaxValue = 2,147,483,647 (231-1). 
The int type is the most often used type in programming . Usually 
programmers use int when they work with integers because this type is 
natural for the 32 -bit microprocessor and is sufficiently "big" for most of the 
calculations performed in everyday life. 
The uint type is 32-bit unsigned integer  type. Its default value is the 
number 0u or 0U (the two are equivalent). The ' u' letter indicates that the 
number is of type uint (otherwise it is understood as int). The minimum 

value that it can take is UInt32.MinValue = 0, and the maximum value is 
UInt32.MaxValue = 4,294,967,295 (232-1). 
The long type is a 64-bit signed type with a default value of 0l or 0L (the 
two are equivalent but it is preferable to use 'L' because the letter 'l' is easily 
mistaken for the digit one ' 1'). The 'L' letter indicates that the number is of 
type long (otherwise it is understood int). The minimal value that can be 
stored in the long type is Int64.MinValue = -9,223,372,036,854,775,808 
(-263) and its maximum value is Int64.MaxValue = 9,223,372,036,854, 
775,807 (263-1). 
The biggest integer type  is the ulong type. It is a 64 -bit unsigned type, 
which has as a default value  – the number 0u, or 0U (the two are equivalent). 
The suffix ' u' indicates that the number is of type ulong (otherwise it is 
understood as long). The minimum value that can be recorded in the ulong 
type is UInt64.MinValue = 0  and the maximum is UInt64.MaxValue = 
18,446,744,073,709,551,615 (264-1).

### Integer Types – Example

Consider an example in which we declare several variables of the integer 
types we know, we initialize them and print their values to the console: 
// Declare some variables 
byte centuries = 20; 
ushort years = 2000; 
uint days = 730480; 
ulong hours = 17531520; 
// Print the result on the console 
```csharp
Console.WriteLine(centuries + " centuries are " + years + 
 " years, or " + days + " days, or " + hours + " hours."); 
 
// Console output: 
// 20 centuries are 2000 years, or 730480 days, or 17531520 
// hours. 
 
ulong maxIntValue = UInt64.MaxValue; 
Console.WriteLine(maxIntValue); // 18446744073709551615 
```
You would be able to see the declaration and initialization of a variable in 
detail in sections "Declaring Variables" and "Initialization of Variables" below, 
and it would become clear from the examples. 
In the code snippet above, we demonstrate the use of integer types. For small 
numbers we use byte type, and for very large – ulong. We use unsigned 
types because all used values are positive numbers.

## Real Floating-Point Types

Real types in C# are the real numbers we know from mathematics. They are 
represented by a floating-point according to the standard IEEE 754 and are 
float and double. Let’s consider in details these two data types and 
understand what their similarities and differences are.

### Real Type Float

The first type we will consider is the 32 -bit real floating-point type float. It 
is also known as a single precision real number . Its default value is 0.0f 
or 0.0F (both are equivalent). The character 'f' when put at the end explicitly 
indicates that the number is of type float (because by default all real 
numbers are considered double). More about this special suffix we can read 
bellow in the " Real Literals" section. The considered type has accuracy up to 
seven decimal places (the others are lost). For instance, if the number 
0.123456789 is stored as type float it will be rounded to 0.1234568. The 
range of values, which can be included in a float type (rounded with accuracy 
of 7 significant decimal digits), range from ±1.5 × 10-45 to ±3.4 × 1038.

### Special Values of the Real Types

The real data types have also several special values that are not real numbers 
but are mathematical abstractions: 
- Negative infinity  -∞ (Single.NegativeInfinity). It is obtained 
when for instance we are dividing -1.0f by 0.0f. 
- Positive infinity  +∞ (Single.PositiveInfinity). It is obtained 
when for instance we are dividing 1.0f by 0.0f. 
- Uncertainty (Single.NaN) – means that an invalid operation is 
performed on real numbers. It is obtained when for example we divide 
0.0f by 0.0f, as well as when calculating squar e root of a negative 
number.

### Real Type Double

The second real floating-point type in the C# language is the double type. 
It is also called double precision real number  and is a 64 -bit type with a 
default value of 0.0d and 0.0D (the suffix ' d' is not mandatory because by 
default all real numbers in C# are of type double). This type has precision of 
15 to 16 decimal digits. The range of values, which can be recorded in double 
(rounded with precision of 15 -16 significant decimal digits), is from  
±5.0 × 10-324 to ±1.7 × 10308. 
The smallest real value of type double is the constant Double.MinValue =  
-1.79769e+308 and the largest is Double.MaxValue = 1.79769e+308. The 
closest to 0 positive number of type double is Double.Epsilon = 4.94066e-
324. As with the type float the variables of type double can take the special 

values: Double.PositiveInfinity (+∞), Double.NegativeInfinity (-∞) 
and Double.NaN (invalid number).

### Real Floating-Point Types – Example

Here is an example in which we declare variab les of real number types, assign 
values to them and print them: 
float floatPI = 3.14f; 
```csharp
Console.WriteLine(floatPI); // 3.14 
double doublePI = 3.14; 
Console.WriteLine(doublePI); // 3.14 
double nan = Double.NaN; 
Console.WriteLine(nan); // NaN 
double infinity = Double.PositiveInfinity; 
Console.WriteLine(infinity); // Infinity
```

### Precision of the Real Types

In mathematics the real numbers in a given range are countless (as opposed 
to the integers in that range) as between any two real numbers a and b there 
are countless other real numbers c where a < c < b. This requires real 
numbers to be stored in computer memory with a limited accuracy. 
Since mathematics and physics mostly work with extremely large numbers 
(positive and negative) and with extremely small numbers  (very close to 
zero), real types in computing and electronic devices must be stored and 
processed appropriately. For example, according to the physics the mass of 
electron is approximately 9.109389*10 -31 kilograms and in 1 mole of 
substance there are approximately 6.02*10 23 atoms. Both these values can 
be stored easily in float and double types. 
Due to its flexibility, the modern floating-point representation of real 
numbers allows us to work with a maximum number of significant digits for 
very large numbers (for example, positive and negative numbers with 
hundreds of digits) and with numbers very close to zero (for example, positive 
and negative numbers with hundreds of zeros after the decimal point before 
the first significant digit).

### Accuracy of Real Types – Example

The real types in C# we went over – float and double – differ not only by 
the range of possible values they can take, but also by their precision (the 
number of decimal digits, which they can preserve). The first type has a 
precision of 7 digits, the second – 15-16 digits. 
Consider an example in which we declare several variables of the known real 
types, initialize them and print their values on the console. The purpose of the 
example is to illustrate the difference in their accuracy: 

// Declare some variables 
float floatPI = 3.141592653589793238f; 
double doublePI = 3.141592653589793238; 
 
// Print the results on the console 
```csharp
Console.WriteLine("Float PI is: " + floatPI); 
Console.WriteLine("Double PI is: " + doublePI); 
 
// Console output: 
// Float PI is: 3.141593 
// Double PI is: 3.14159265358979 
We see that the number π which we declared as float, is rounded to the 7-th 
digit, and the one we declared double – to 15-th digit. We can conclude that 
the real type double retains much greater precision than float, thus if we 
```
need a greater precision after the decimal point, we will use it.

### About the Presentation of the Real Types

Real floating-point numbers in C# consist of three components (according to 
the standard IEEE 754): sign (1 or -1), mantissa and order (exponent), 
and their values are  calculated by a complex formula. More detailed 
information about the representation of the real numbers is provided in the 
chapter " Numeral Systems " where we will take an in -depth look at the 
representation of numbers and other data types in computing.

### Errors in Calculations with Real Types

In calculations with real floating-point data types  it is possible to observe 
strange behavior, because during the representation of a given real number 
it often happens to lose accuracy. The reason for this is the inability of some 
real numbers to be represented exactly as a sum of negative powers of the 
number 2. Examples of numbers that do not have an  accurate representation 
in float and double types are for instance 0.1, 1/3, 2/7 and other. Here is a 
sample C# code, which demonstrates errors in calculations with floating -point 
numbers in C#: 
float f = 0.1f; 
```csharp
Console.WriteLine(f); // 0.1 (correct due to rounding) 
double d = 0.1f; 
Console.WriteLine(d); // 0.100000001490116 (incorrect) 
 
float ff = 1.0f / 3; 
Console.WriteLine(ff); // 0.3333333 (correct due to rounding) 
double dd = ff; 
Console.WriteLine(dd); // 0.333333343267441 (incorrect) 

The reason for the unexpected result in the first example is the fact that the 
```
number 0.1 (i.e. 1/10) has no accurate representation in the real floating-
point number format IEEE 754 and its approximate value is recorded. When 
printed directly the result looks correct because of the rounding. The rounding 
is done during the conversion of the number to string in order to be printed 
on the console. When switching from float to double the approximate 
representation of the number in the IEEE 754 format is more noticeable. 
Therefore, the rounding does no longer hide the incorrect representation and 
we can observe the errors in it after the eighth digit. 
In the second case the number 1/3 has no accurate representation and is 
rounded to a number very close to 0.3333333. The value of this number is 
clearly visible when it is written in double type, which preserves more 
significant digits. 
Both examples show that floating-point number arithmetic can produce 
mistakes, and is therefore not appropriate for precise financial calculations. 
Fortunately, C# supports decimal precision arithmetic where numbers like 0.1 
are presented in the memory without rounding. 
 
Not all real numbers have acc urate representation in float 
and double types. For example, the number 0.1 is represent-
ted rounded in float type as 0.099999994.

## Real Types with Decimal Precision

C# supports the so -called decimal floating -point arithmetic,  where 
numbers are represented  via the decimal numeral system rather than the 
binary one. Thus, the decimal floating point -arithmetic type in C# does not 
lose accuracy when storing and processing floating-point numbers. 
The type of data for real numbers with decimal precision in C# is the 128-
bit type decimal. It has a precision from 28 to 29 decimal places. Its minimal 
value is -7.9×1028 and its maximum value is +7.9×1028. The default value is 
0.0m or 0.0M. The 'm' character at the end indicates explicitly that the number 
is of type decimal (because by default all real numbers are of type double). 
The closest to 0 numbers, which can be recorded in decimal, are ±1.0 × 10-28. 
It is obvious that decimal can store neither very big positive or negative 
numbers (for example, with hundreds of digits), nor values very close to 0. 
However, this type is almost perfect for financial calculations because it 
represents the numbers as a sum of powers of 10 and losses from rounding 
are much smaller than when using binary representation. The real number s of 
type decimal are extremely convenient for financial calculations  – 
calculation of revenues, duties, taxes, interests, payments, etc. 
Here is an example in which we declare a variable of type decimal and assign 
its value: 


decimal decimalPI = 3.14159265358979323846m; 
```csharp
Console.WriteLine(decimalPI); // 3.14159265358979323846 
```
The number decimalPI, which we declared of type decimal, is not rounded 
even with a single position because we use it with precision of 21 digits , 
which fits in the type decimal without being rounded. 
Because of the high precision and the absence of anomalies  during 
calculations (which exist for float and double), the decimal type is 
extremely suitable for financial calculations where accuracy is critical. 
 
Despite its smaller range, the decimal type retains precision 
for all decimal numbers it can store! This makes it much 
more suitable for precise calculations, and very appropriate 
for financial ones. 
The main difference between real floating-point numbers  and real 
numbers with decimal precision is the accuracy of calculations and the 
extent to which they round up the stored values. The double type allows us 
to work with very large values and values very close to zero but at the 
expense of accuracy and some unpleasant rounding errors. The decimal type 
has smaller range but ensures greater accuracy in computation, as well as 
absence of anomalies with the decimal numbers. 
 
If you perform calculations with money use the decimal type 
instead of float or double. Otherwise, you may encounter 
unpleasant anomalies while calculating and errors as a 
result! 
As all calculations with data of type decimal are done completely by software, 
rather than directly at a low microprocessor level, the calcu lations of this type 
are from several tens to hundreds of times slower  than the same 
calculations with double, so use this type only when it is really necessary.

## Boolean Type

Boolean type is declared with the keyword bool. It has two possible values: 
true and false. Its default value is false. It is used most often to store the 
calculation result of logical expressions.

### Boolean Type – Example

Consider an example in which we declare several variables from the already 
known types, initialize them, compare the m and print the result on the 
console: 
// Declare some variables 


int a = 1; 
int b = 2; 
// Which one is greater? 
bool greaterAB = (a > b); 
// Is 'a' equal to 1? 
bool equalA1 = (a == 1); 
// Print the results on the console 
if (greaterAB) 
```csharp
{ 
 Console.WriteLine("A > B"); 
} 
else 
{ 
 Console.WriteLine("A <= B"); 
} 
 
Console.WriteLine("greaterAB = " + greaterAB); 
Console.WriteLine("equalA1 = " + equalA1); 
 
// Console output: 
// A <= B 
// greaterAB = False 
// equalA1 = True 
```
In the example above, we declare two variables of type int, compare them 
and assign the result to the bool variable greaterAB. Similarly, we do the 
same for the variable equalA1. If the variable  greaterAB is true, then A > B 
is printed on the console, otherwise A <= B is printed.

## Character Type

Character type is a single character  (16-bit number of a Unicode table 
character). It is declared in C# with the keyword char. The Unicode table is 
a technological standard that represe nts any character (letter, punctuation, 
etc.) from all human languages as writing systems (all languages and 
alphabets) with an integer or a sequence of integers. More about the Unicode 
table can be found in the chapter " Strings and Text Processing ". The smallest 
possible value of a char variable is 0, and the largest one is 65535. The 
values of type char are letters or other characters, and are enclosed in 
apostrophes.

### Character Type – Example

Consider an example in which we declare one variable of type char, initialize 
it with value 'a', then 'b', then 'A' and print the Unicode values of these 
letters to the console: 

// Declare a variable 
char ch = 'a'; 
// Print the results on the console 
```csharp
Console.WriteLine( 
 "The code of '" + ch + "' is: " + (int)ch); 
ch = 'b'; 
Console.WriteLine( 
 "The code of '" + ch + "' is: " + (int)ch); 
ch = 'A'; 
Console.WriteLine( 
 "The code of '" + ch + "' is: " + (int)ch); 
 
// Console output: 
// The code of 'a' is: 97 
// The code of 'b' is: 98 
// The code of 'A' is: 65
```

## Strings

Strings are sequences of characters . In C# they are declared by the 
keyword string. Their default value is null. Strings are enclosed in quotation 
marks. Various text -processing operations can be pe rformed using strings: 
concatenation (joining one string with another), splitting by a given separator, 
searching, replacement of characters and others. More information about text 
processing can be found in the chapter " Strings and Text Processing ", where 
you will find detailed explanation on what a string is, what its applications are 
and how we can use it.

### Strings – Example

Consider an example in which we declare several variables of type string, 
initialize them and print their values on the console: 
// Declare some variables 
string firstName = "John"; 
string lastName = "Smith"; 
string fullName = firstName + " " + lastName; 
// Print the results on the console 
```csharp
Console.WriteLine("Hello, " + firstName + "!"); 
Console.WriteLine("Your full name is " + fullName + "."); 
 
// Console output: 
// Hello, John! 
// Your full name is John Smith.
```

## Object Type

Object type is a special type, which is the parent of all other types in the .NET 
Framework. Declared with the keyword object, it can take values from any 
other type. It is a reference type, i.e. an index (address) of a memory area 
which stores the actual value.

### Using Objects – Example

Consider an example in which we declare several variables of type object, 
initialize them and print their values on the console: 
// Declare some variables 
object container1 = 5; 
object container2 = "Five"; 
 
// Print the results on the console 
```csharp
Console.WriteLine("The value of container1 is: " + container1); 
Console.WriteLine("The value of container2 is: " + container2); 
 
// Console output: 
// The value of container1 is: 5 
// The value of container2 is: Five. 
```
As you can see from the example, we can store the value of any other type in 
an object type variable. This makes the object type a universal data 
container.

## Nullable Types

Nullable types are specific wrappers around the value types (as int, 
double and bool) that allow storing data with a null value. This provides 
opportunity for types that generally do not allow lack of value (i.e. value 
null) to be used as reference types and to accept both normal values and the 
special one null. Thus nullable types hold an optional value. 
Wrapping a given type as nullable can be done in two ways: 
Nullable<int> i1 = null; 
int? i2 = i1; 
Both declarations are equivalent. The easiest way  to perform this operation is 
to add a question mark (?) after the type, for example int?, the more difficult 
is to use the Nullable<…> syntax. 
Nullable types are reference types i.e. they are reference to an object in the 
dynamic memory, which contains their actual value. They may or may not 
have a value and can be used as normal primitive data types, but with some 
specifics, which are illustrated in the following example: 

int i = 5; 
int? ni = i; 
```csharp
Console.WriteLine(ni); // 5 
 
// i = ni; // this will fail to compile 
Console.WriteLine(ni.HasValue); // True 
i = ni.Value; 
Console.WriteLine(i); // 5 
 
ni = null; 
Console.WriteLine(ni.HasValue); // False 
//i = ni.Value; // System.InvalidOperationException 
i = ni.GetValueOrDefault(); 
Console.WriteLine(i); // 0 
```
The example above shows how a nullable variable (int?) can have a value 
directly added even if the value is non-nullable (int). The opposite is not 
directly possible. For this purpose, the nullable types’ property Value can be 
used. It returns the value stored in the nullable type variable, or produces an 
error (InvalidOperationException) during program execution if the value is 
missing (null). In order to check whether a variable of nullable type has a 
value assigned, we can use the Boolean property HasValue. Another useful 
method is GetValueOrDefault(). If the nullable type variable has a value, 
this method will return its value, else it will return the default value for the 
nullable type (most commonly 0). 
Nullable types are used for storing information, which is not mandatory. For 
example, if we want to store data for a student such as the first name and 
last name as mandatory an d age as not required, we can use type int? for 
the age variable: 
string firstName = "John"; 
string lastName = "Smith"; 
int? age = null;