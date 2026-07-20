# Numeral Systems

So far we have taken a look at the history of numeral systems. Let’s now take 
a detailed look at what they really are and what is their role in computing.

## What Are Numeral Systems?

Numeral systems are a way of representing numbers by a finite type -set of 
graphical signs called digits. We must add to them the rules for depicting 
numbers. The characters, which are used to depict numbers in a given 
numeral system, can be perceived as that system’s alphabet. 
During the different stages of the development of human civilization, various 
numeral systems had gained popularity. We must note that today the most 
widely spread one is the Arabic numeral system . It uses the digits 0, 1, 2, 
3, 4, 5, 6, 7, 8 and 9, as its alphabet. (An interesting fact is that the depiction 
of Arabic numerals in modern times is different from the ten digits mentioned 

above but in spite of all they are still referred to the same numeral  system – 
the decimal one). 
Beside an alphabet, every numeral system has a base. The base is a number 
equal to the different digits used by the system for depicting the numbers in 
it. For example, the Arabic numeral system is decimal because it has 10 
digits. A random number can be chosen as a base, which has an absolute 
value different than 1 and 0. It can also be a real or a complex number with a 
sign. 
A practical question we can ask is: which is the best numeral system  that 
we should use? To answer it, w e must decide what the optimal way to depict 
a number (the digit count in the number) is and the number of digits the 
given numeral system uses – its base. Mathematically it can be proven that 
the best ratio between the length of depiction and the number o f used digits 
is accomplished by using Euler's number  (e = 2,718281828 ), which is the 
base of natural logarithms. 
Working in a system with such base e is extremely inconvenient and 
impractical because that number cannot be represented as a ratio of two 
natural numbers. This gives us grounds to conclude that the optimal base of a 
numeral system is either 2 or 3. 
Although the number 3 is closer to the Neper number, it is unsuitable for 
technical implementation. Because of that the binary numeral system is the 
only one suitable for practical use and it is used in the modern computers and 
electronic devices.

## Positional Numeral Systems

A positional numeral system  is a system, in which the position of the 
digits is significant for the value of the number. This means that the value of 
the digits in the number is not strictly defined and depends on which position 
the given digit is. For example, in the number 351 the digit 1 has a value of 1, 
while in the number 1024 it has a value of 1000. We must note that the b ases 
of the numeral systems are applicable only with positional numeral systems. 
In a positional numeral system the number A(p) = (a(n)a(n-1)…a(0),a(-1)a(-2)…a(-k)) 
can be represented in the following way: 




k
nm
mmp TaA )(
 
In this sum Tm has the meaning of a weight factor for the mth digit of the 
number. In most cases Tm = Pm, which means that: 




k
nm
m
mp PaA )(
 

Formed using the sum above, the number A(p) is respectively made up from 
its whole part ( a(n)a(n-1)…a(0)) and its fraction (a(-1)a(-2)…a(-k)), where every a 
belongs to the multitude of the natural numbers M={0, 1, 2, …, p-1}. We can 
easily see that in positional numeral systems the value of each digit is the -
base-of-the-system times bigger than the one before it (the digit to the right, 
which is the lower -order digit). As a direct result from this we must add one 
to the left (higher -order) digit, if we need to note a digit in the current digit 
that is bigger than the base. The systems with bases of 2, 8, 10 and 16 have 
become w ide spread in computing devices. In the table below we can see 
their notation of the numbers from 0 to 15: 
Binary Octal Decimal Hexadecimal 
0000 0 0 0 
0001 1 1 1 
0010 2 2 2 
0011 3 3 3 
0100 4 4 4 
0101 5 5 5 
0110 6 6 6 
0111 7 7 7 
1000 10 8 8 
1001 11 9 9 
1010 12 10 A 
1011 13 11 B 
1100 14 12 C 
1101 15 13 D 
1110 16 14 E 
1111 17 15 F

## Non-Positional Numeral Systems

Besides the positional numeral systems, there are also non-positional 
numeral systems, in which the value of each digit is a constant and does not 
strictly depend on its position in the number. Such numeral systems are the 
Roman and Greek numeral systems. All non -positional numeral systems 
have a common drawback – the notation of big numbe rs in them is very 
inefficient. As a result of this drawback, they have gained only limited use. 
This could often lead to inaccuracy when determining the value of numbers. 
We will take a very brief look at the Roman and Greek numeral systems.

### Roman Numeral System

The Roman numeral system uses sequences of the following symbols to 
represent the numbers: 
Roman Digit Decimal Value 
I 1 
V 5 
X 10 
L 50 
C 100 
D 500 
M 1000 
As we have already mentioned, in this numeral system the position of the 
digit has no significance for the value of the number and for determining the 
value, the following rules are applied: 
1. If two consecutively represented Roman digits are in such order that the 
value of the first one is bigger or equal to the value of the second one, 
their values are added. Examples: 
The number III = 3 (1 + 1 + 1). T he number MMD = 2500 (2000 + 2000 
+ 500). 
2. If two consecutively represented roman digits are in increasing order of 
their values, they are subtracted. This is done from right to left. Examples: 
The number IX  = 9 (-1 + 10) , the number MXL=1040 (1000 – 10 + 50) , 
but the number MXXIV = 1024 (1000 + 10 + 10 – 1 + 5).

### Greek Numeral System

The Greek numeral system is a decimal system, in which a grouping of fives 
is done. It uses the following digits: 
Greek Digit Decimal Value 
Ι 1 
Г 5 
Δ 10 
Η 100 
Χ 1,000 
Μ 10,000 

As we can see in the table, one is represented with a vertical line, five with 
the letter Г, and the powers of 10 with the first letter of the corresponding 
Greek word. 
Here are some examples of numbers in this system: 
- ΓΔ = 50 = 5 x 10 
- ΓH = 500 = 5 x 100 
- ΓX = 5000 = 5 x 1,000 
- ΓM = 50,000 = 5 x 10,000

## The Binary Numeral System – Foundation of Computing Technology

The Binary Numeral System – Foundation of 
Computing Technology 
The binary numeral system  is the system, which is used to represent and 
process numbers in modern computing machines. The main reason it is so 
widely spread is explained with the fact that devices with two stable states 
are very simple to implement and the production costs of bina ry arithmetic 
devices are very low. 
The binary digits 0 and 1 can be easily represented in the computing 
machines as "current" and "no current", or as "+5V" and "-5V". 
Along with its advantages, the binary system for number notation in 
computers has its dr awbacks, too. One of its biggest practical flaws is that 
numbers represented in binary numeral system are very long, meaning they 
have a large number of bits. This makes it inconvenient for direct use by 
humans. To avoid this disadvantage, systems with lar ger bases are used in 
practice.

## Decimal Numbers

Numbers represented in the decimal numeral system, are given in a primal 
appearance, meaning that they are easy to be understood by humans. This 
numeral system has the number 10 for a base. The numbers repres ented in it 
are ordered by the powers of the number 10. The lowest -order digit (first 
from right to left) of the decimal numbers is used to represent the ones 
(100=1), the next one to represent the tens (10 1=10), the next one to 
represent the hundreds (10 2=100), and so on. In other words – every 
following digit is ten times bigger than the one preceding it. The sum of the 
separate digits determines the value of the number. We will take the number 
95031 as an example, which can be represented in the decimal numeral 
system as: 
95031 = (9×104) + (5×103) + (0×102) + (3×101) + (1×100) 
Represented that way, the number 95031 is presented in a natural way for 
humans because the principles of the decimal numeral system have been 
accepted as fundamental for people. 

 
The discussed approaches are valid for the other numeral 
systems, too. They have the same logical setting but are 
applied to a system with a different base. The last statement 
is true for the binary and hexadecimal numeral systems, 
which we will discuss in details in a little bit.

## Binary Numbers

The numbers represented in the binary numeral system are represented in 
a secondary aspect – which means that they are easy to be understood by the 
computing machine. They are a bit harder to be understood by people. To 
represent a binary number, the binary numeral system is used, which has the 
number 2 for  a base. The numbers represented in it are ordered by the 
powers of two. Only the digits 0 and 1 are used for their notation. 
Usually, when a number is represented in a numeral system other than 
decimal, the numeral system ’s base is added as an index in br ackets next to 
the number. For example, with this notation 1110(2) we indicate a number in 
the binary numeral system. If no numeral system is explicitly specified, it is 
accepted that the number is in the decimal system. The number is 
pronounced by reading  its digits in sequence from left to right (we read from 
the highest-order to the lowest-order bit). 
Like with decimal numbers, each binary number being looked at from right to 
left is represented by a power of the number 2  in the respected sequence. 
The lowest-order position in a binary number corresponds to the zero power 
(20=1), the second position corresponds to 2 to the first power (2 1=2), the 
third position corresponds to 2 to the second power (2 2=4), and so on. If the 
number is 8 bits long, the last bit is 2 to the seventh power (2 7=128). If the 
number has 16 bits, the last bit is 2 to the fifteenth power. By using 8 binary 
digits (0 or 1) we can represent a total of 256 numbers, because 2 8=256. By 
```csharp
using 16 binary digits we can represent a total of 65 536 numbers, because 
216=65536. 
```
Let’s look at some examples of numbers in the binary numeral system. Take, 
for example, the decimal number 148. It is composed of three digits: 1, 4 
and 8, and it corresponds to the following binary number: 
10010100(2) 
148 = (1×27) + (1×24) + (1×22) 
The full notation of the number is depicted in the following table: 
Number 1 0 0 1 0 1 0 0 
Power 27 26 25 24 23 22 21 20 
Value 1×27
= 128 
0×26
= 0 
0×25
= 0 
1×24
= 16 
0×23
= 0 
1×22
= 4 
0×21
= 0 
0×20
= 0 


The sequence of eight zeros or ones represents one byte, an ordinary eight 
bit binary number. All numbers from 0 to 255 including can be represented in 
a single byte. In most cases this is not enough; as a result several 
consecutive bytes can be used to represent a big number. Two bytes form the 
so called "machine word" ( word), which corresponds to 16 bits (in 16 -bit 
computing machines). Besides it, computing machines use the so called 
double word or dword, corresponding to 32 bits. 
 
If a binary number ends in 0 it is even, if it ends in 1 it is 
odd.

## Converting From Binary to Decimal Numeral System

When turning from binary to decimal numeral system, we do a conversion of 
a binary number to a decimal number . Every number can be converted 
from one numeral  system to another by doing a sequence of operations that 
are possible in both numeral systems. As we have already mentioned, 
numbers in the binary system consist of binary digits, which are ordered by 
the powers of 2. Let’s take the number 11001(2). Converting into decimal is 
done by calculating the following sum: 
11001(2) = 1×24 + 1×23 + 0×22 + 0×21 + 1×20 = 
= 16(10) + 8(10) + 1(10) = 25(10) 
From this follows that 11001(2) = 25(10) 
In other words – every single binary digit is multiplied by 2 raised to the 
power of the position it is in . In the end all of the numbers resulting from 
the binary digits are added up to get the decimal value of the binary number.

### Horner Scheme

Another method of conversion exists, known as the Horner Scheme. When 
using it, we multiply the left most digit by 2 and add it to the one to its right. 
We multiply this result by two and the neighboring digit (one to the right) is 
added. This is repeated until all the digits in the number have been exhausted 
and we add the last digit without multiplying it. Here is an example: 
1001(2) = ((1 × 2 + 0) × 2 + 0) × 2 + 1 = 2 × 2 × 2 + 1 = 9

## Converting from Decimal to Binary Numeral System

When transitioning from decimal to binary numeral system , we convert a 
decimal number into a binary one. To accomplish this, we divide it by 2 with a 
remainder. This is how we get the quotient and the remainder, which is 
separated. 
Let’s use the number 148 again as an example. We do an integer division by 
the base we want to convert to (in this case it is 2). After that using the 
remainders of the division (they will always be either zero or one), we 


represent the converted number. We continue dividing until we get a zero 
quotient. Here is an example: 
148:2=74 with remainder 0; 
74:2=37 with remainder 0; 
37:2=18 with remainder 1; 
18:2=9  with remainder 0; 
9:2=4  with remainder 1; 
4:2=2  with remainder 0; 
2:2=1  with remainder 0; 
1:2=0  with remainder 1; 
After we are done with the division, we represent the remainders in reverse 
order as follows: 
10010100 
i.e. 148(10) = 10010100 (2)

## Operations with Binary Numbers

The arithmetical rules of addition, subtraction and multiplication are valid for a 
single digit of binary numbers: 
0 + 0 = 0   0 - 0 = 0   0 × 0 = 0 
1 + 0 = 1   1 - 0 = 1   1 × 0 = 0 
0 + 1 = 1   1 - 1 = 0   0 × 1 = 0 
1 + 1 = 10   10 - 1 = 1   1 × 1 = 1 
In addition, with binary numbers we can also do logical operations such as 
logical multiplication (conjunction), logical addition (disjunction) and the sum 
of modulo two (exclusive or). 
We must also n ote that when we are doing arithmetic operations with multi -
order numbers we must take into account the connection between the 
separate orders by transfer or loan, when doing addition or subtraction 
respectively. Let’s take a look at some details regarding bitwise operators.

### Bitwise "and"

The bitwise AND operator can be used for checking the value of a given bit in 
a number. For example, if we want to check if a given number is even (we 
check if the lowest-order bit is 1): 
10111011 AND 00000001 = 00000001 
The result is 1, which means that the number is odd (if the result was 0 the 
number would be even). 

In C# the bitwise "and" is represented with & and is used like this: 
int result = integer1 & integer2;

### Bitwise "or"

The bitwise OR operator can be used if we want, for example, to "raise" a 
given bit to 1: 
10111011 OR 00000100 = 10111111 
Bitwise "or" in C# is represented with | and is used like this: 
int result = integer1 | integer2;

### Bitwise "exclusive or"

The bitwise operator XOR – every binary digit is processed separately, and 
when we have a 0 in the second operand, the corresponding value of the bit 
in the first operand is copied in the result. At every position that has a value 
of 1 in the second operand, we reverse the value of  the corresponding 
position in the first operand and represent it in the result: 
10111011 XOR 01010101 = 11101110 
In C# the notation of the "exclusive or" operator is ^: 
int result = integer1 ^ integer2;

### Bitwise Negation

The bitwise operator  NOT – this is a unary operator, which means that it is 
applied to a single operand. What it does is to reverse every bit of the given 
binary number to its opposite value: 
NOT 10111011 = 01000100 
In C# the bitwise negation is represented with ~: 
int result = ~integer1;

## Hexadecimal Numbers

With hexadecimal numbers  we have the number 16  for a system base, 
which implies the use of 16 digits to represent all possible values from 0 to 15 
inclusive. As we have already shown in one of the tables in the previous 
sections, for notating numbers in the hexadecimal system, we use the digits 
from 0 to 9 and the Latin numbers from A to F. Each of them has the 
corresponding value: 
A=10, B=11, C=12, D=13, E=14, F=15 

We can give the following example for hexadecimal numbers: D2, 1F2F1, D1E 
and so on. 
Transition to decimal system is done by multiplying the value of the right 
most digit by 160, the next one to the left by 16 1, the next one to the left by 
162 and so on, and adding them all up in the end. Example: 
D1E(16) = E*160 + 1*161 + D*162 = 14*1 + 1*16 + 13*256 = 3358(10). 
Transition from decimal to hexadecimal numeral system is done by dividing 
the decimal number by 16 and taking the remainders in reverse order. 
Example: 
3358 / 16 = 209 + remainder 14 (E) 
209 / 16 = 13 + remainder 1 (1) 
13 / 16 = 0 + remainder 13 (D) 
We take the remainders in reverse order and get the number D1E(16).

## Fast Transition from Binary to Hexadecimal Numbers

The fast conversion from binary to hexadecimal numbers  can be quickly 
and easily done by dividing the binary number into groups of four bits 
(splitting it into half -bytes). If the number of digits is not divisible by four, 
leading zeros in the highest -orders are added. After the d ivision and the 
eventual addition of zeros, all the groups are replaced with their 
corresponding digits. Here is an example: 
Let’s look at the following: 1110011110(2). 
1. We divide it into half-bytes and add the leading zeros 
Example: 0011 1001 1110. 
2. We repl ace every half -byte with the corresponding hexadecimal digit 
and we get 39E(16). 
Therefore 1110011110 (2) = 39E(16).

## Numeral Systems – Summary

As a summary, we will formulate again in a short but clear manner the 
algorithms used for transitioning from one positional numeral system to 
another: 
- Transitioning from a decimal to a k -based numeral system  is done 
by consecutively dividing the decimal to the base of the k system and 
the remainders (their corresponding digit in the k based system) are 
accumulated in reverse order. 
- Transitioning from a k-based numeral system to decimal  is done by 
multiplying the last digit of the k -based number by k 0, the one before it 
by k1, the next one by k2 and so on, and the products are the added up. 

- Transitioning from a k -based numeral system to a p -based 
numeral system  is done by intermediately converting to the decimal 
system (excluding hexadecimal and binary numeral systems). 
- Transitioning from a binary to hexadecimal numeral system and 
back is done by converting each sequence of 4 binary bits into its 
corresponding hexadecimal number and vice versa.