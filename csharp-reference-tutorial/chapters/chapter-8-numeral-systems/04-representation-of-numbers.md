# Representation of Numbers

Binary code is used to store data in the operating memory of computing 
machines. Depending on the type of data we want to store (strings, integers  
or real numbers with an integral and fractal part) information is represented 
in a particular manner. It is determined by the data type. 
Even a programmer using a high level language must know how the data is 
allocated in the operating memory of the machi ne. This is also relevant to the 
cases when the data is stored on an external carrier, because when it is 
processed, it will be situated in the operating memory. 
In the current section we will take a look at the different ways to present 
and process differ ent types of data . In general they are based on the 
concepts of bit, byte and machine word. 
Bit is a binary unit of information with a value of either 0 or 1. 
Information in the memory is grouped in sequences of 8 bits, which form a 
single byte. 
For an arithmetic device to process the data, it must be presented in the 
memory by a set number of bytes (2, 4 or 8), which form a machine word. 
These are concepts, which every programmer must know and understand.

## Representing Integer Numbers in the Memory

One o f the things we have not discussed so far is the sign of numbers. 
Integers can be represented in the memory in two ways: with a sign  or 
without a sign. When numbers are represented with a sign, a signed order is 
introduced. It is the highest -order and has the value of 1 for negative 
numbers and the value of 0 for positive numbers. The rest of the orders are 
informational and only represent (contain) the value of the number. In the 
case of a number without a sign, all bits are used to represent its value.

## Unsigned Integers

For unsigned integers 1, 2, 4 or 8 bytes are allocated in the memory. 
Depending on the number of bytes used in the notation of a given number, 
different scopes of representation with variable size are formed. Through n 
bytes all integers in  the range [0, 2 n-1] can be represented. The following 
table shows the range of the values of unsigned integers: 

Number of bytes 
for representing 
the number in 
the memory 
Range 
Notation 
with order Regular notation 
1 0 ÷ 28-1 0 ÷ 255 
2 0 ÷ 216-1 0 ÷ 65,535 
4 0 ÷ 232-1 0 ÷ 4,294,967,295 
8 0 ÷ 264-1 0 ÷ 18,446,744,073,709,551,615 
We will give as an example a single -byte and a double -byte representation of 
the number 158, whose binary notation is the following 10011110 (2): 
1. Representation with 1 byte: 
1 0 0 1 1 1 1 0  
2. Representation with 2 bytes: 
0 0 0 0 0 0 0 0 1 0 0 1 1 1 1 0

## Representing Negative Numbers

For negative numbers 1, 2, 4 or 8 bytes are allocated in the memory of the 
computer, while the highest -order ( the left most bit ) has a signature 
meaning and carries the information about the sign of the number. As we 
have already mentioned, when the signature bit has a value of 1, the number 
is negative, otherwise it is positive. 
The next table shows the range of the values of the signed integer numbers in 
the computer according to the number of bytes used for their notation: 
Number of bytes 
for representing 
the number in the 
memory 
Rank 
Notation 
with order Regular notation 
1 -27 ÷ 27-1 -128 ÷ 127 
2 -215 ÷ 215-1 -32,768 ÷ 32,767 
4 -231 ÷ 231-1 -2,147,483,648 ÷ 2,147,483,647 
8 -263 ÷ 263-1 -9,223,372,036,854,775,808 ÷ 
9,223,372,036,854,775,807 
To encode negative numbers, straight, reversed and additional code  is 
used. In all these three notations signed integers are within the range:  [-2n-1, 

2n-1-1]. Positive numbers are always represented in the same way and the 
straight, reversed and additional code all coincide for them. 
Straight code (signed magnitude ) is the simplest representation of the 
number. The highest -order bit carries the  sign and the rest of the bits hold 
the absolute value of the number. Here are some examples: 
The number 3 in signed magnitude is represented as an eight-bit-long number 
00000011. 
The number -3 in signed magnitude is represented in an eight -bit-long 
number as 10000011. 
Reversed code (one’s complement) is formed from the signed magnitude 
of the number by inversion (replacing all ones with zeros and vice-versa). This 
code is not convenient for the arithmetical operations addition and subtraction 
because it is executed in a different way if subtraction is necessary. Moreover 
the sign carrying bits need to be processed separately from the information 
carrying ones. This drawback is avoided by using additional code, which 
instead of subtraction implements additio n with a negative number. The latter 
is depicted by its addition, i.e. the difference between 2 n and the number 
itself. Example: 
The number -127 in signed magnitude is represented as 1 1111111 and in 
one’s complement as 1 0000000. 
The number 3 in signed ma gnitude is represented as 0 0000011, and in one ’s 
complement looks like 0 1111100. 
Additional code (two’s complement) is a number in reversed code to 
which one is added (through addition). Example: 
The number -127 is represented with additional code as 1 0000001. 
In the Binary Coded Decimal , also known as BCD code, in one byte two 
decimal digits are recorded. This is achieved by encoding a single decimal 
digit in each half -byte. Numbers presented in this way can be packed, which 
means that they can be repre sented in a packed format. If we represent a 
single decimal digit in one byte we get a non-packed format. 
Modern microprocessors use one or several of the discussed codes to present 
negative numbers, the most widespread method is using two’s complement.

## Integer Types in C#

In C# there are eight integer data types either signed or unsigned. 
Depending on the amount of bytes allocated for each type, different value 
ranges are determined. Here are descriptions of the types: 
Type Size Range Type in .NET 
Framework 
sbyte 8 bits -128 ÷ 127 System.SByte 

byte 8 bits 0 ÷ 255 System.Byte 
short 16 bits -32,768 ÷ 32,767 System.Int16 
ushort 16 bits 0 ÷ 65,535 System.UInt16 
int 32 bits -2,147,483,648 ÷ 2,147,483,647 System.Int32 
uint 32 bits 0 ÷ 4,294,967,295 System.UInt32 
long 64 bits –9,223,372,036,854,775,808 ÷ 
9,223,372,036,854,775,807 System.Int64 
ulong 64 bits 0 ÷ 18,446,744,073,709,551,615 System.UInt64 
We will take a brief look at the most used ones. The most commonly used 
integer type is int. It is represented as a 32 -bit number with two ’s 
complement and takes a value in the range [ -231, 2 31-1]. Variables of this 
type are most frequently used to operate loops, index arrays and other 
integer calculations. In the following table an example of a variable of the 
type int is being declared: 
int integerValue = 25; 
int integerHexValue = 0x002A; 
int y = Convert.ToInt32("1001", 2); // Converts binary to int 
The type long is the largest signed integer type in C#. It has a size of 64 bits 
(8 bytes). When giving value to the variables of type long the Latin letters "l" 
or "L" are placed at the end of the integer literal. Placed at that position, this 
modifier signifies that the literal has a value of the type long. This is done 
because by default all integer literals are of the type int. In the next 
example, we declare and give 64-bit value to variables of type long: 
long longValue = 9223372036854775807L; 
long newLongValue = 932145699054323689l; 
An important condition is not to exceed the range of numbers that can be 
represented in the used type. However, C# offers the ability to control what 
happens when an overflow occurs . This is done via the checked and 
unchecked blocks. The first are used when the application needs to throw an 
exception (of the type System.OverflowException) in case that the range of 
the variable is exceeded. The following programming code does exactly that: 

checked 
```csharp
{ 
 int a = int.MaxValue; 
 a = a + 1; 
 Console.WriteLine(a); 
} 
```
In case the fragment is in an unchecked block, an exception will not be 
thrown and the output result will be wrong: 
-2147483648 
In case these blocks are not used, the C# compiler works in unchecked mode 
by default. 
C# includes unsigned types, which can be useful when a larger range is 
needed for the variables in the scope of the positive numbers. Below are some 
examples for declaring variables without a sign. We should pay attention to 
the suffixes of ulong (all combinations of U, L, u, l). 
byte count = 50; 
ushort pixels = 62872; 
uint points = 4139276850; // or 4139276850u, 4139276850U 
ulong y = 18446744073709551615; // or UL, ul, Ul, uL, Lu, lU

## Big-Endian and Little-Endian Representation

There are two ways for ordering bytes in the memory when representing 
integers longer than one byte: 
- Little-Endian (LE)  – bytes are ordered from left to right from the 
lowest-order to the highest. This representation is used in the Intel x86 
and Intel x64 microprocessor architecture. 
- Big-Endian (BE) – bytes are ordered from left to right starting with the 
highest-order and ending with the lowest. This representation is used in 
the PowerPC, SPARC and ARM microprocessor architecture. 
Here is an example: the number A8B6EA72(16) is presented in both byte orders 
in the following way: 
 
There are some classes in C# that offer the  opportunity to define which order 
standard to be used. This is important for operations like sending / receiving 


streams of information over the internet or other types of communication 
between devices made by different standards. The field IsLittleEndian of 
the BitConverter class for example shows what mode the class is working in 
and how it stores data on the current computer architecture.

## Representing Real Floating-Point Numbers

Real numbers consist of a whole and fraction parts. In computers, they are 
represented as floating-point numbers. Actually this representation comes 
from the Standard for Floating -Point Arithmetic (IEEE 754) , adopted by the 
leading microprocessor manufacturers. Most hardware platforms and 
programming languages allow or require the calculations to be done according 
to the requirements of this standard. The standard defines: 
- Arithmetical formats: a set of binary and decimal data with a floating -
point, which consists of a finite number of digits. 
- Exchange formats : encoding (bit sequences), which can be used for 
data exchange in an effective and compact form. 
- Rounding algorithms : methods, which are used for rounding up 
numbers during calculations. 
- Operations: arithmetic and other operations of the arithmetic formats. 
- Exceptions: they are signals for extraordinary events such as division 
by zero, overflowing and others. 
According to the IEEE -754 standard a random real number R can be 
presented in the following way: 
R = M * qp 
where M is the mantissa of the number, p is the order (exponent), and q 
accordingly is the base of the numeral system the number is in. The mantissa 
must be a positive or negative common fraction |M|<1, and the exponent – a 
positive or negative integer. 
In the mentioned method of representation of numbers, every floating -point 
number will have the following summarized format ±0,M*q±p. 
When notating numbers in the floating -point format using the binary numeral 
system in particular, we will have R = M * 2p. In this representation of real 
numbers in the computer memory, when we change the exponent, the 
decimal point in the mantissa moves ("floats"). The floating -point 
representation format has a semi -logarithmic form. It is depicted in the 
following figure:

### Representing Floating-Point Numbers – Example

Let’s give an example of how a floating -point number is represented in the 
memory. We want to write the number -21.15625 in 32 -bit (single precision) 
floating-point format according to the IEEE -754 standard. In this format, 23 
bits are used for the mantissa, 8 bits for the exponent and 1 bit for the sign. 
The notation of the number is as follows: 
 
The sign of the number is negative, which means that the mantissa has a 
negative sign: 
S = -1 
The exponent has a value of 4 (represented with a shifted order): 
p = (20 + 21 + 27) - 127 = (1+2+128) – 127 = 4 
For transitioning to the real value we subtract 127 from the additional code 
because we are working with 8 bits (127 = 2 7-1) starting from the zero 
position. 
The mantissa has the following value (without taking the sign into account):  
M  = 1 + 2-2 + 2-4 + 2-7 + 2-9 = 
= 1 + 0.25 + 0.0625 + 0.0078125 + 0.001953125 = 
= 1.322265625 
We should note that we added a one, which was missing from the binary 
notation of the mantissa. We did it because the mantissa is always normalized 
and starts with a one by default. 
The value of the number is calculated using the formula R = M * 2 p, which in 
our example looks like the following: 
R = -1,3222656 * 24 = -1,322265625 * 16 = -21,1562496 ≈ -21,15625

### Mantissa Normalization

To use the order grid more fully, the mantissa must contain a one in its 
highest-power order. Every mantissa fulfilling this condition is called 
normalized. In the IEEE-754 standard, the  one in the whole part of the 
mantissa is by default, meaning the mantissa is always a number between 1 
and 2. 
If during the calculations a result that does not fulfill this condition is reached, 
it means that the normalization is violated. This requires t he normalization of 


the number prior to its further processing, and for this purpose the decimal 
point in the mantissa is moved and the corresponding order change is made.

## The Float and Double Types in C#

In C# we have at our disposal two types, which can represent floating-point 
numbers. The float type is a 32 -bit real number with a floating -point and it 
is accepted to be called single precision floating -point number. The double is 
a 64 -bit real number with a floating -point and it is accepted that it has a  
double precision floating -point. These real data types and the arithmetic 
operations with them correspond to the specification outlined by the IEEE 
754-1985 standard. In the following table are presented the most important 
characteristics of the two types: 
Type Size Range Significant 
Digits 
Type in .NET 
Framework 
float 32 bits ±1.5 × 10−45 ÷ 
±3.4 × 1038 7 System.Single 
double 64 bits ±5.0 × 10−324 ÷ 
±1.7 × 10308 15-16 System.Double 
In the float type we have a mantissa, which contains 7 significant digits, 
while in the double type it stores 15 -16 significant digits. The remaining bits 
are used for specifying the sign of the mantissa and the value of the 
exponent. The double type, aside from the larger number of significant digits, 
also has a larger exponent, which means that it has a larger scope of the 
values it can assume. Here is an example how to declare variables of the 
float and double types: 
float total = 5.0f; 
float result = 5.0f; 
double sum = 10.0; 
double div = 35.4 / 3.0; 
double x = 5d; 
The suffixes placed after the numbers on the right side of the equation, serve 
the purpose of specifying what type the number should be treated as ( f for 
float, d for double). In this case they are in place because by default 5.0 will 
be interpreted as a double and 5 – as an int. 
 
In C#, floating -point numbers literals by default are of the 
double type. 
Integers and floating -point numbers can both be present in a given 
expression. In that case, the integer variables are converted to floating -point 
variables and the result is defined according to the following rules: 


1. If any of the floating -point types is a double, the result will be double 
(or bool). 
2. If there is no double type in the expression, the result is float (or 
bool). 
Many of the mathematical operations can yield results , which have no specific 
numerical value, like the value "+/ - infinity" or NaN (which means "Not a 
Number"), these values are not numbers. Here is an example: 
double d = 0; 
```csharp
Console.WriteLine(d); 
Console.WriteLine(1/d); 
Console.WriteLine(-1/d); 
Console.WriteLine(d/d); 
If we execute it we get the following result: 
```
0.0 
Infinity 
-Infinity 
NaN 
If we execute the code above using int instead of double, we will receive a 
System.DivideByZeroException, because integer division by 0 is not an 
allowed operation.

## Errors When Using Floating-Point Numbers

Floating-point numbers (presented according to the IEEE 754 standard) are 
very convenient for calculations in physics, where very big numbers are used 
(with several hundred digits) and also numbers that are very close to zero 
(with hundreds of digits after  the decimal point before the first significant 
digit). When working with these numbers, the IEEE 754 format  is 
exceptionally convenient because it keeps the number ’s order in the exponent 
and the mantissa is only used to store the significant digits. In 6 4-bit floating-
point numbers accuracy of 15 -16 digits, as well as exponents displacing the 
decimal point with 300 positions left or right can be achieved. 
Unfortunately not every real number has an exact representation in the 
IEEE 754 format , because not e ach number can be presented as a 
polynomial of a finite number of addends, which are negative powers of two. 
This is fully valid even for numbers, which  are used daily for the simplest 
financial calculations. For example the number 0.1 represented as a 32 -bit 
floating-point value is presented as 0.099999994. If the appropriate rounding 
is used, the number can be accepted as 0.1, but the error can be accumulated 
and cause serious deviations, especially in financial calculations. For example 
when adding up 1000 items with a unit price of 0.1 EUR each, we should get a 
sum of 100 EUR but if we use a 32-bit floating -point numbers for the 

calculations the result will be 99.99905. Here is C# example in action, which 
proves the errors caused by the inaccurate presen tation of decimal real 
numbers in the binary numeral system: 
float sum = 0f; 
for (int i = 0; i < 1000; i++) 
```csharp
{ 
 sum += 0.1f; 
} 
Console.WriteLine("Sum = {0}", sum); 
// Sum = 99.99905 
We can easily see the errors in such calculations if we execute the example or 
modify it to get even more striking errors.
```

## Precision of Floating-Point Numbers

The accuracy of the results from floating -point calculations depends on the 
following parameters: 
1. Precision of the number representation. 
2. Precision of the used number methods. 
3. Value of the errors resulting from rounding up, etc. 
Calculations with them can be inaccurate because they are represented in the 
memory with some kind of precision. Let’s look at the following code fragment 
as an example: 
double sum = 0.0; 
for (int i = 1; i <= 10; i++) 
```csharp
{ 
 sum += 0.1; 
} 
Console.WriteLine("{0:r}", sum); 
Console.WriteLine(sum); 
```
During the execution, in the loop we add the value 1/10 to the variable sum. 
When calling the WriteLine() method, we use the round-trip format specifier 
"{0:r}" to print the exact (not rounded) value contained in the variable, and 
after that we print the same value without specifying a format. We expect 
that when we execute the program we will get 1.0 as a result but in reality, 
when rounding is turned off,  the program returns a value very close to the 
correct one but still different: 
0.99999999999999989 
1 

As we can see in the example, by default, when printing floating -point 
numbers in .NET Framework, they are rounded , which seemingly reduces 
the errors of  their inaccurate notation in the IEEE 754 format. The result of 
the calculation above is obviously wrong but after the rounding it looks 
correct. However, if we add 0.1 a several thousand times, the error will 
accumulate and the rounding will not be able to compensate it. 
The reason for the wrong answer in the example is that the number 0.1 does 
not have an exact representation in the double type and it has to be rounded. 
Let’s replace double with float: 
float sum = 0.0f; 
for (int i = 1; i <= 10; i++) 
```csharp
{ 
 sum += 0.1f; 
} 
Console.WriteLine("{0:r}", sum); 
If we execute the code above, we will get an entirely different sum: 
```
1.00000012 
Again the reason for this is rounding. 
If we investigate why the program yields these results, we will see that the 
number 0.1 of the float type is represented in the following manner: 
 
All this looks correct except for the mantissa, which has a value slightly bigger 
than 1.6, not exactly 1.6 because this number cannot be presented as sum of 
the negative powers of 2. If we have to be very precise, the value of the 
mantissa is 1 + 1 / 2 + 1 / 16 + 1 / 32 + 1 / 256 + 1 / 512 + 1 / 4096 + 1 / 
8192 + 1 / 65536 + 1 / 131072 + 1 / 1048576 + 1 / 2097152 + 1 / 8388608 
≈ 1.60000002384185791015625 ≈ 1.6. Thus the number 0.1 presented in 
the IEE 754 is slightly more than 1.6 × 2-4 and the error occurs not during the 
addition but before that, when 0.1 is recorded in the float type. 
Double and Float types have a field called Epsilon, which is a constant, and 
it contains the smallest value larger than zero, which can be represented by 
an instance of System.Single or System.Double respectively. Each value 
smaller than Epsilon is considered to be equal to 0. For example, if we 


compare t wo numbers, which are different after all, but their difference is 
smaller than Epsilon, they will be considered equal.

## The Decimal Type

The System.Decimal type in .NET Framework uses decimal floating -point 
arithmetic and 128-bit precision, which is very suitable for big numbers and 
precise financial calculations. Here are some characteristics of the decimal 
type: 
Type Size Range Significant 
numbers 
Type in .NET 
framework 
decimal 128 bits ±1.0 × 10−28 ÷ 
±7.9 × 1028 28-29 System.Decimal 
Unlike the floating-point numbers, the decimal type retains its precision for 
all decimal number in its range. The secret to this excellent precision when 
working with decimal numbers lies in the fact that the internal representation 
of the mantissa is not  in the binary system but in the decimal one. The 
exponent is also a power of 10, not 2. This enables numbers to be 
represented precisely, without them being converted to the binary numeral 
system. 
Because the float and double types and the operations on t hem are 
implementer by the arithmetic coprocessor , which is part of all modern 
computer microprocessors, and decimal is implemented by the software  in 
.NET CLR, it is tens of times slower than double, but is irreplaceable for the 
execution of financial calculations. 
In case our target is to assign a given literal to variable of type decimal, we 
need to use the suffixes m or M. For example: 
decimal calc = 20.4m; 
decimal result = 5.0M; 
Let’s use decimal instead of float / double in the example from before: 
decimal sum = 0.0m; 
for (int i = 1; i <= 10000000; i++) 
```csharp
{ 
 sum += 0.0000001m; 
} 
Console.WriteLine(sum); 
```
This time the result is exactly what we expected: 
1.0000000 

Even though the decimal type has a higher precision than the floating -point 
types, it has a smaller value range and, for example, it cannot be used to 
represent the following value 1e-50. As a result, an overflow may occur when 
converting from floating-point numbers to decimal.

## Character Data (Strings)

Character (text) data in computing is text, encoded using a sequence of 
bytes. There are different encoding schemes used to encode text data. Most 
of them encode one character in one byte or in a sequence of several bytes. 
Such encoding schemes are ASCII, Windows-1251, UTF-8 and UTF-16.

### Encoding Schemes (Encodings)

The ASCII encoding scheme compares the unique number of the letters from 
the Latin alphabet and some other symbols and special characters and writes 
them in a single byte . The ASCII standard contains a total of 127 characters, 
each of which is written in one byte. A text, written as a sequence of bytes 
according to the ASCII standard, cannot contain Cyrillic or characters from 
other alphabets such as the Arabian, Korean and Chinese ones. 
Like the ASCII standard, the Windows-1251 encoding scheme compares the 
unique number of the letters in the Latin alphabet, Cyrillic and some other 
symbols and specialized characters and writes them in one byte. The 
Windows-1251 encoding def ines the numbers of 256 characters – exactly as 
many as the different values that can be written in one byte. A text written 
according to the Windows -1251 standard can contain only Cyrillic and Latin 
letters, Arabian, Indian or Chinese are not supported. 
The UTF-8 encoding is completely different. All characters in the Unicode 
standard – the letters and symbols used in all widely spread languages in the 
world (Cyrillic, Latin, Arabian, Chinese, Japanese, Korean and many other 
languages and writing systems) – can be encoded in it. The UTF -8 encoding 
contains over half a million symbols. In the UTF -8 encoding, the more 
commonly used symbols are encoded in 1 byte (Latin letters and digits for 
example), the second most commonly used symbols are coded in 2 bytes 
(Cyrillic letters for example), and the ones that are used even more rarely are 
coded in 3 or 4 bytes (like the Chinese, Japanese and Korean alphabet). 
The UTF-16 encoding, like UTF -8 can depict text of all commonly used 
languages and writing systems, described in the Unicode standard. In UTF-16, 
every symbol is written in 16 bits (2 bytes) and some of the more rarely used 
symbols are presented as a sequence of two 16-bit values.

### Presenting a Sequence of Characters

Character sequences can be presented in several ways. The most common 
method for writing text in the memory is to write in 2 or 4 bytes its length, 
followed by a sequence of bytes, which presents the text itself in some sort of 
encoding (for example Windows-1251 or UTF-8). 

Another, less common m ethod of writing texts in the memory, typical for the 
C language, represents texts as a sequence of characters, usually coded in 1 
byte, followed by a special ending character, most frequently a 0. When using 
this method, the length of the text saved at a given position in the memory is 
not known in advance. This is considered a disadvantage in many situations.

### Char Type

The char type in the C# language is a 16-bit value , in which a single 
Unicode character or part of it is coded. In most alphabets (for exa mple the 
ones used by all European languages) one l etter is written in a single 16 -bit 
value, and thus it is assumed that a variable of the char type represents a 
single character. Here is an example: 
char ch = 'A'; 
```csharp
Console.WriteLine(ch);
```

### String Type

The string type in C# holds text, encoded in UTF -16. A single string in 
C# consists of 4 bytes length and a seque nce of characters written as 16 -bit 
values of the char type. The string type can store texts written in all 
widespread alphabets and human writing systems – Latin, Cyrillic, Chinese, 
Japanese, Arabian and many, many others. Here is an example of the usage 
of the string: 
string str = "Example"; 
```csharp
Console.WriteLine(str);
```