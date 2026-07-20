# Printing to the Console

In most programming languages printing and reading the information from 
the console is implemented in similar ways and the most of the solutions are 
based on the concept of "standard input" and "standard output".

## Standard Input and Standard Output

The operating system is required to define standard input -output 
mechanisms for user interaction. When starting a given console program, 
system code running at the initialization of the program is responsible for 
opening (closing) of streams to the allocated by the operating system 
mechanisms for input -output. This system code in itializes the program 
abstraction for user interaction embedded in the respective programming 
language. In this way, the application started can automatically read the user 

input from the standard input stream  (in C# this is Console.In), print 
information on the standard output stream (in C# this is Console.Out) and 
can signal for problem situations in the standard error stream (in C# this is 
Console.Error). 
The concept of the streams will be later examined in details. For now we will 
focus on the theoretical basis related to the program input and output in C#.

## Devices for Console Input and Output

Besides the keyboard an application input can come from many other places, 
such as file, microphone, barcode reader and others. The output of a 
program may be on the console (on the screen), as well as in a file or another 
output device, such as a printer: 
 
We will show a basic example that illustrates text printing to the console 
through the abstraction for access ing the standard input and standard output  
provided to us by C#: 
Console.Out.WriteLine("Hello World"); 
The result of the above code execution would be the following: 
Hello World

## Console.Out Stream

System.Console class has different properties and methods (classes are 
considered in details in the chapter "Creating and Using Objects ") which are 
used to read and display text on the console as well as its formatting. Among 
them there are three properties that make impression and they are related to 
data entering and displaying, namely the Console.Out, Console.In and 
Console.Error. They p rovide access to the standard streams for printing on 
the console, for reading from the console and to the error messages reporting 
stream accordingly. Although we could use them directly, the other methods 
of System.Console give us the convenience for wor king with input -output 
console operations and actually most often these properties are ignored. 
However it is good to remember that this part of the console functionality is 
working on these streams. If needed, we can replace the default input / 
output / error streams at runtime by using the methods Console.SetOut(…), 
Console.SetIn(…) and Console.SetError(…) respectively. 


Now we will examine the most commonly used methods for text printing on 
the console.

## Using Console.Write(…) and Console.WriteLine(…)

Work with these methods is easy because they can print all the basic types 
(string, numeric and primitive types). 
Here are some examples of printing various types of data: 
// Print String 
```csharp
Console.WriteLine("Hello World"); 
 
// Print int 
Console.WriteLine(5); 
 
// Print double 
Console.WriteLine(3.14159265358979); 
```
The result of this code execution looks like this: 
Hello World 
5 
3.14159265358979 
```csharp
As we see by using Console.WriteLine(…) it is possible to print various data 
types because for each type there is a predefined version of the 
```
methodWriteLine(…) in the Console class. 
The difference between Write(…) and WriteLine(…) is that the Write(…) 
method prints on the console what it is pr ovided between the parentheses but 
does nothing in addition while the method WriteLine(…) means directly 
“write line”. This method does what the Write(…) one does but in addition 
goes to a new line. In fact the method does not print a new line but simply 
puts a “command” for moving  cursor to the position where the new line 
starts (this command consists of the character \r followed by \n). 
Here is an example, which  illustrates the difference between Write(…) and 
WriteLine(…): 
```csharp
Console.WriteLine("I love"); 
Console.Write("this "); 
Console.Write("Book!"); 
```
The output of this example is: 
I love 
this Book! 

We notice that the output of this example is printed on two lines, even though 
the code is on three. This happens because on the first line of code we use 
WriteLine(…) which prints "I love" and then goes to a new line. In the next 
two lines of the code uses the Write(…) method, which prints without going 
on a new line and thus the words " this" and " Book!" remain on the same 
line.

### Concatenation of Strings

In general C# does not allow the use of operators over string objects. The 
only exception to this rule is the addition operation ( +) which concatenates 
(joins) two strings  and returns as result a new string. This allows chaining 
of concatenate ( +) operations one after another in a sequence. The next 
example represents concatenation of three strings. 
string age = "twenty six"; 
string text = "He is " + age + " years old."; 
```csharp
Console.WriteLine(text); 
```
The result of this code execution is again a string: 
He is twenty six years old.

### Concatenation of Mixed Types

What happens when we want to print larger and more complex text, which  
consists of different types? Until now we used versions of the method 
WriteLine(…) for a specific type. Is it necessary when we want to print 
different types at once to use different versions of the method WriteLine(…) 
for each of these types? The answer to this question is “no” because in C# we 
can unite text and other data (for instance, numeric) by using the " +" 
operator. The followi ng example is like the previous but in it the years ( age) 
are from integer type: 
int age = 26; 
string text = "He is " + age + " years old."; 
```csharp
Console.WriteLine(text); 
```
In the example is concatenation and printing on the screen performed. The 
result of the example is the following: 
He is 26 years old. 
On the second line of the example code we see that a concatenation of the 
string "He is " and the integer type " age" is performed. We are trying to 
combine two different types . This is possible because of the presence of 
the following important rule. 

 
When a string is involved in concatenation with any other 
type the result is always a string. 
From the rule it is clear that the result of "He is " + age is again a string and 
then the result is added to the last part of the expression " years old.". So 
after calling a chain of + operators ultimately the result is a string and thus 
the string version of the method WriteLine(…) is invoked. 
For short the above example can be written as follows: 
int age = 26; 
```csharp
Console.WriteLine("He is " + age + " years old.");
```

### Some Features of String Concatenation

There are some interesting situations with concatenation (addition) of strings 
that you need to know and be careful about because they lead to errors. The 
following example represents a surprising behavior of the code: 
string s = "Four: " + 2 + 2; 
```csharp
Console.WriteLine(s); 
// Four: 22 
 
string s1 = "Four: " + (2 + 2); 
Console.WriteLine(s1); 
// Four: 4 
```
As seen from the example the operators’ execution order (see chapter 
"Operator and Expressions ") is of great importance! In our example first the 
concatenation of " Four: " to " 2" is performed and the result of the  
operation is string . After that, another concatenation with the second 
number is performed and the obtained unexpected result is " Four: 22 " 
instead of the expected " Four: 4 ". This is because the operations are 
performed from left to right and in this scen ario a string participates in each 
of them. 
In order to avoid this unpleasant situation we can use parentheses that will 
change the order of operators’ execution can be used to achieve the desired 
result. Parentheses are operators with highest priority and  make the 
execution of the operation "addition" of the two numbers happen before the 
concatenation with the string on the left. Thus first the addition of the two 
numbers is done and then they are concatenated with the string. 
This mistake is very common f or beginner programmers because they do not 
consider that string concatenation is performed from left to right because the 
addition of numbers is of the same priority than as concatenation. 


 
When you concatenate strings and also sum numbers, use 
parentheses to specify the correct order of operations. 
Otherwise they are executed from left to right.

## Formatted Output with Write(…) and WriteLine(…)

For printing long and elaborate series of elements, special options (also 
known as overloads) of the me thods Write(…) and WriteLine(…) have been 
introduced. These options have a completely different concept than the 
standard methods for printing in C#. Their main idea is to adopt a special 
string, formatted with special formatting characters and list of values, which 
should be substituted in place of “the format specifiers”. Here is how 
Write(…) is defined in the standard C# libraries: 
```csharp
public static void Write(string format, object arg0, 
 object arg1, object arg2, object arg3, …);
```

### Formatted Output – Examples

The following example prints twice the same thing but in different ways: 
string str = "Hello World!"; 
 
// Print (the normal way) 
```csharp
Console.Write(str); 
 
// Print (through formatting string) 
Console.Write("{0}", str); 
```
The result of this example execution is: 
Hello World!Hello World! 
We see as a result " Hello, World!" twice on one line. This is because there 
is no printing of a new line in the program. 
First we print the string in a well -known way in order to see the difference 
with the other approach. The second printing is the formatting Write(…) and 
the first argument is the format string. In this case {0} means to put the first 
argument after the for matting string in the place of {0}. The expression {0} 
is called a placeholder, i.e. a place that will be replaced by a specific value 
while printing. 
The next example will further explain this concept: 
string name = "John"; 
int age = 18; 


string town = "Seattle"; 
```csharp
Console.Write( 
 "{0} is {1} years old from {2}!\n", name, age, town); 
```
The result of this example execution is as follows: 
John is 18 years old from Seattle! 
From the signature of this Write(…) version we saw that the first argument is 
the format string. Following is a series of arguments, which are placed where 
we have a number enclosed in curly brackets. The expression {0} means to 
put in its place the first of the arguments submitted after the  format string 
(in this case name). Next is {1} which means to replace with the second of 
the arguments (age). The last placeholder is {2}, which means to replace with 
the next parameter ( town). Last is \n, which is a special character that 
indicates moving to a new line. 
It is appropriate to mention that actually the new line command on  Windows 
is \r\n, and on Unix-based operating systems  – \n. When working with 
the console it does not matter that we use only \n because the standard input 
stream considers \n as \r\n but if we write into a file, for example, using only 
\n is wrong (on Windows).

### Composite Formatting

The methods for formatted output of the Console class use the so -called 
composite formatting feature . The composite formatting is used for 
console printing as well as in certain operations with strings. We examined the 
composite formatting in the simplest of its kind in the previous example but it 
has significantly bigger potential than what we have seen so far. Basically the 
composite formatting u ses two things: composite formatting string  and 
series of arguments, which are replaced in certain places in the string.

### Composite Formatting String

The composite formatting string is a mixture of normal text and formatting 
items. In formatting the normal text remains the same as in the string and 
the places of formatting items are replaced by the values of the respective 
arguments printed according to certain rules. These rules are specified using 
the syntax of formatting items.

### Formatting Items

The format ting items provide the possibility for powerful control over the 
displayed value and therefore can obtain very complicated form. The following 
formation scheme represents the general syntax of formatting items: 
{index[,alignment][:formatString]} 

As we not ice the formatting item begins with an opening curly bracket { and 
ends with a closing curly bracket }. The content between the brackets is 
divided into three components of which only the index component is 
mandatory. Now we will examine each of them separately.

### Index Component

The index component is an integer and indicates the position  of the 
argument from the argument list.  The first argument is indicated by " 0", the 
second by "1", etc. The composite formatting string allows having multiple 
formatting it ems that relate to one and same argument. In this case index 
component of these items is one and the same number. There is no restriction 
on the sequence of arguments’ calling. For example, we could use the 
following formatting string: 
```csharp
Console.Write( 
 "{1} is {0} years old from {3}!", 18, "John", 0, "Seattle"); 
```
In cases where some of the arguments are not referenced by any of the 
formatting items, those arguments are simply ignored and do not play a role. 
However it is good to remove such arguments from the list of arguments 
because they introduce unnecessary complexity and may lead to confusion.  
In the opposite case , when a formatting item refers an argument that does 
not exist in the list of arguments , an exception is thrown . This may occur, 
for example, if we have formatting placeholder {4} and we submitted a list of 
only two arguments.

### Alignment Component

The alignment component is optional and indicates the string alignment. It 
is a positive or negative integer and the positive values indicate alignment 
to the right and the negative – alignment to the left. The value of the number 
indicates the number of positions in which to align the number. If the string 
we want to represe nt has a length greater than or equal to the value of the 
number, then this number is ignored. If it is less, however, the unfilled 
positions are filled in with spaces. 
For example, let’s try the following formatting: 
```csharp
Console.WriteLine("{0,6}", 123); 
Console.WriteLine("{0,6}", 1234); 
Console.WriteLine("{0,6}", 12); 
Console.Write("{0,-6}", 123); 
Console.WriteLine("--end"); 
```
It will output the following result: 
   123 

  1234 
    12 
123   --end 
If we decide to use the alignment component, we must separate it from the 
index component by a comma as it is done in the example above.

### The "formatString" Component

This component specifies the specific formatting of the string. It varies 
depending on the  type of argument. There are three main types of 
formatString components: 
- for numerical types of arguments 
- for arguments of type date (DateTime) 
- for arguments of type enumeration (listed types)

### Format String Components for Numbers

This type formatString component has two subtypes: standard -defined 
formats and user-defined formats (custom format strings).

### Standard Formats for Numbers

These formats are defined by one of several format specifiers , which are 
letters with particular importance. After the format specifier there can be a 
positive integer called precision, which has a different meaning for the 
different specifiers. When it affects the number of decimal places after the 
decimal point, the  result is rounded. The following table describes specifiers 
and their precision meaning: 
Specifier Description 
"C" or "c" 
Indicates the currency and the result will be displayed 
along with the currency sign for the current “culture” 
(for example, English ). The precision indicates the 
number of decimal places after the decimal point. 
"D" or "d" 
An integer number. The precision indicates the 
minimum number of characters for representing the 
string and, if necessary, zeroes are supplemented in the 
beginning. 
"E" or "e" Exponential notation . The precision indicates the 
number of places after the decimal point. 
"F" or "f" Integer or decimal number . The precision indicates 
the number of signs after the decimal point. 

"N" or "n" 
Equivalent to "F" but  represents also the corresponding 
separator for thousands, millions, etc. (for example, in 
the English language often the number " 1000" is 
represented as " 1,000" – with comma between the 
number 1 and the zeroes). 
"P" or "p" 
Percentage: it w ill multiply the number by 100 and will 
display the percent character upfront. The precision 
indicates the number of signs after the decimal point. 
"X" or "x" 
Displays the number in hexadecimal numeral system. 
It works only for integer numbers. The precision 
indicates minimum numbers of signs to display the 
string as the missing ones are supplemented with zeroes 
at the beginning. 
Part of the formatting is determined by the current “culture” settings, 
which are taken by default from the regional settings of the operating system. 
"The cultures" are set of rules that are valid for a given language or a given 
country and that indicate which character is to be used as decimal separator, 
how the cu rrency is displayed, etc. For example, for the Japanese "culture" 
the currency is displayed by adding " ¥" after the amount, while for the 
American "culture", the character "$" is displayed before the amount.  For 
Bulgarian currency is suffixed by " лв.".

### Standard Formats for Numbers – Example

Let’s see a few examples of usage of the specifiers represented in the table 
above. In the code below we assume the regional settings are Bulgarian so 
the currency will be printed in Bulgarian , the decimal separator will be "," and 
the thousands separator will be space (the regional settings can be changed 
from Control Panel in Windows): 
StandardNumericFormats.cs 
```csharp
class StandardNumericFormats 
{ 
 static void Main() 
 { 
  Console.WriteLine("{0:C2}", 123.456); 
  //Output: 123,46 лв. 
  Console.WriteLine("{0:D6}", -1234); 
  //Output: -001234 
  Console.WriteLine("{0:E2}", 123); 
  //Output: 1,23E+002 
  Console.WriteLine("{0:F2}", -123.456); 
  //Output: -123,46 

  Console.WriteLine("{0:N2}", 1234567.8); 
  //Output: 1 234 567,80 
  Console.WriteLine("{0:P}", 0.456); 
  //Output: 45,60 % 
  Console.WriteLine("{0:X}", 254); 
  //Output: FE 
 } 
} 
```
If we run the same code with English (United States) culture, the output will 
be as follows: 
$123.46 
-001234 
1.23E+002 
-123.46 
1,234,567.80 
45.60 % 
FE

### Custom Formats for Numbers

All formats that are not standard are assigned to the user (custom) formats. 
For the custom formats  are again defined a set of specifiers and the 
difference with the standard formats is that a number of specifiers can be 
used (in standard formats only a single specifier is used). The following table 
lists various specifiers and their meaning: 
Specifier Description 
0 Indicates a digit. If at this position of the result a digit is 
missing, a zero is written instead. 
# Indicates a digit. Does not print anything if at this 
position in the result a digit is missing. 
. Decimal separator for the respective “culture”. 
, Thousands separator for the respective “culture”. 
% Multiplies the result by 100 and prints the character for 
percent. 
E0 or E+0 or E-0 
Indicates an exponential notation. The number of zeroes 
indicates the number of signs of the exponent. The sign 
"+" means that we always want to represent also the 
number’s sign while minus means to display the sign 
only if the value is negative. 

There are many characteristics regarding the use of custom formats for 
numbers, but they will not be discussed here. You may find more information 
in MSDN. Here are some simple examples that illustrate how to use custom 
formatting strings (the output is given for the U.S. culture): 
CustomNumericFormats.cs 
```csharp
class CustomNumericFormats 
{ 
 static void Main() 
 { 
  Console.WriteLine("{0:0.00}", 1); 
  //Output: 1.00 
  Console.WriteLine("{0:#.##}", 0.234); 
  //Output: .23 
  Console.WriteLine("{0:#####}", 12345.67); 
  //Output: 12346 
  Console.WriteLine("{0:(0#) ### ## ##}", 29342525); 
  //Output: (02) 934 25 25 
  Console.WriteLine("{0:%##}", 0.234); 
  //Output: %23 
 } 
}
```

### Format String Components for Dates

When formatting dates we again have separation of standard and custom 
formats.

### Standard Defined Date Formats

Since the standard defined formats are many we will list only few of them. 
The rest can be easily checked on MSDN. 
Specifier Format (for English (United States) "culture") 
d 2/27/2012 
D February 27, 2012 
t 17:30 (hour) 
T 17:30:22 (hour) 
Y or y February 2012 (only month and year)

### Custom Date Formats

Similar to custom formats for numbers here we have multiple format 
specifiers and we can combine several of them. Since here are many 

specifiers we will show only some of them, which we will use to demonstrate 
how to use custom formats for dates. Consider the following table: 
Specifiers Format (for English (United States) "culture") 
d Day – from 1 to 31 
dd Day – from 01 to 31 
M Month – from 1 to 12 
MM Month – from 01 to 12 
yy The last two digits of the year (from 00 to 99) 
yyyy Year written in 4 digits (e.g. 2012) 
hh Hour – from 00 to 11 
HH Hour – from 00 to 23 
m Minutes – from 0 to 59 
mm Minutes – from 00 to 59 
s Seconds – from 0 to 59 
ss Seconds – from 00 to 59 
When using these specifiers we can insert different separators between the 
different parts of the date, such as "." or "/". Here are few examples: 
DateTime d = new DateTime(2012, 02, 27, 17, 30, 22); 
```csharp
Console.WriteLine("{0:dd/MM/yyyy HH:mm:ss}", d); 
Console.WriteLine("{0:d.MM.yy}", d); 
Execution of these examples gives the following result for the U.K. culture: 
```
27/02/2012 17:30:22 
27.02.12 
Note that the result can vary depending on the current culture. For example if 
we run the same code in the Bulgarian culture, the result will be different: 
27.02.2012 17:30:22 
27.02.12

### Format String Enumeration Components

Enumerations (listed types) are data types that can take as value one of 
several predefined possible values (e.g. the seven days of the week). We will 
examine them in details in the chapter "Defining Classes". 

In enumerations there is very little to be formatted. Four standard format 
specifiers are defined: 
Specifier Format 
G or g Represents enumeration as a string. 
D or d Represents enumeration as a number. 
X or x Represents enumeration as a number in hexadecimal 
numeral system and with eight digits. 
Here are some examples: 
```csharp
Console.WriteLine("{0:G}", DayOfWeek.Wednesday); 
Console.WriteLine("{0:D}", DayOfWeek.Wednesday); 
Console.WriteLine("{0:X}", DayOfWeek.Wednesday); 
While executing the above code we get the following result: 
```
Wednesday 
3 
00000003

### Formatting Strings and Localization

When using format strings it is possible one and same program to print 
different values depending on the localization settings for the operating 
system. For example, when printing the month from a given date if the 
current localization is English it will print in English, for example “August”, 
while if the localization is French it will print in French, for example "Août". 
When launching a console application it automatically retrieves the operating 
system localization  (culture settings)  and uses it for reading and writing 
formatted data (like numbers, dates, currency, etc.). 
Localization in .NET is also called "culture" and can be changed manually by 
the class System.Globalization.CultureInfo. Here is an example in which 
we print a number and a date by the U.S. and Bulgarian localization: 
CultureInfoExample.cs  
```csharp
using System; 
using System.Threading; 
using System.Globalization; 
 
class CultureInfoExample 
{ 
 static void Main() 

 { 
  DateTime d = new(2012, 02, 27, 17, 30, 22);
 
  Thread.CurrentThread.CurrentCulture = 
   CultureInfo.GetCultureInfo("en-US"); 
  Console.WriteLine("{0:N}", 1234.56); 
  Console.WriteLine("{0:D}", d); 
 
  Thread.CurrentThread.CurrentCulture = 
   CultureInfo.GetCultureInfo("bg-BG"); 
  Console.WriteLine("{0:N}", 1234.56); 
  Console.WriteLine("{0:D}", d); 
 } 
} 
```
When starting the example the following result is obtained: 
1,234.56 
Monday, February 27, 2012 
1 234,56 
27 Февруари 2012 г.