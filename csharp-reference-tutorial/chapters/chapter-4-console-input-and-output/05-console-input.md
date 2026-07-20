# Console Input

As in the beginning of th is chapter we explained, the most suitable for small 
applications is the console communication because it is easiest to implement. 
The standard input device is the part of the operating system that controls 
from where the program will receive its input data. By defa ult "the standard 
input device" reads its input from a driver "attached" to the keyboard. This 
can be changed and the standard input can be redirected to another location, 
for example to a file, but this is rarely done. 
Each programming language has a mech anism for reading and writing to the 
console. The object that controls the standard input stream in C#, is 
Console.In. 
From the console we can read different data: 
- text; 
- other types after parsing the text; 
Actually for reading the standard input stream Console.In is rarely used 
directly. The class Console provides two methods Console.Read() and 
Console.ReadLine() that run on this stream and usually reading from the 
console is done by them.

## Reading through Console.ReadLine()

The method Console.ReadLine() provides great convenience for reading 
from console. How does it work? When this method is invoked, the program 
prevents its work and wait for input from the console. The user enters some 
string on the console and presses the [Enter] key. At this moment th e 
console understands that the user has finished entering and reads the string. 
The method Console.ReadLine() returns as result the string entered by the 
user. Now perhaps it is clear why this method has this name. 
The following example demonstrates the operation of Console.ReadLine(): 
UsingReadLine.cs 
```csharp
class UsingReadLine 
{ 
 static void Main() 
 { 
  Console.Write("Please enter your first name: "); 
  string firstName = Console.ReadLine(); 
 
  Console.Write("Please enter your last name: "); 
  string lastName = Console.ReadLine(); 
 
  Console.WriteLine("Hello, {0} {1}!", firstName, lastName); 
 } 
} 
 
// Output: Please enter your first name: John 
//     Please enter your last name: Smith 
//     Hello, John Smith! 
We see how easy it is to read text from the console by using the method 
Console.ReadLine(): 
```
- We print some text in the console, which asks for a user name (this is 
only for the convenience of the user and is not obligatory). 
- We execute reading of an entire line from the console using the method 
ReadLine(). This leads to blocking the program until the user enters 
some text and presses [Enter]. 
- Then we repeat these two steps for the last name. 
- Once we have gathered the necessary  information we print it on the 
console.

## Reading through Console.Read()

The method Read() behaves slightly different than ReadLine(). As a 
beginning it reads only one character and not the entire line. The other 
significant difference is that the method do es not return directly the read 
character but its code. If we want to use the result as a character we must 
convert it to a character or use the method Convert.ToChar() on it. There is 
one important characteristic: the character is read only when the [Ente r] 
key is pressed. Then the entire string written on the console is transferred to 
the buffer of the standard input string and the method Read() reads the first 
character of it. In subsequent invocations of the method if the buffer is not 
empty (i.e. there are already entered in but still unread characters) then the 
program execution will not stop and wait, but will directly read the next 
character from the buffer and thus  until the buffer is empty. Only then the 
program will wait again for a user input if Read() is called again. Here is an 
example: 
UsingRead.cs 
```csharp
class UsingRead 
{ 
 static void Main() 
 { 
  int codeRead = 0; 
```
  do 
```csharp
  { 
   codeRead = Console.Read(); 
   if (codeRead != 0) 
   { 
    Console.Write((char)codeRead); 
   } 
  } 
  while (codeRead != 10); 
 } 
} 
```
This program reads one line entered by the user and prints it character by 
character. This is possible due to a small trick – we are previously aware that 
the [Enter] key actually enters two characters in the buffer. These are the 
"carriage return " code ( Unicode 13) followed by the " linefeed" code 
(Unicode 10). In order to understand that  one line is finished we are looking 
for a character with code 10 in the Unicode table . Thus the program reads 
only one line and exits the loop. 
We should mention that the method  Console.Read() is rarely used in 
practice if there is an alternative to use Console.ReadLine(). The reason for 
this is that the possibility of mistaking with Console.Read() is much greater 

than if we choose an alternative approach and the code will most likely be 
unnecessarily complicated.

## Reading Numbers

Reading numbers from the console in C# is not done directly . In order to 
read a number we should have previously read the input as a string (using 
ReadLine()) and then convert this string to a number. The operation of 
converting a string into another type is called parsing. All pr imitive types 
have methods for parsing. We will give a simple example for reading and 
parsing of numbers: 
ReadingNumbers.cs 
```csharp
class ReadingNumbers 
{ 
 static void Main() 
 { 
  Console.Write("a = "); 
  int a = int.Parse(Console.ReadLine()); 
 
  Console.Write("b = "); 
  int b = int.Parse(Console.ReadLine()); 
 
  Console.WriteLine("{0} + {1} = {2}", a, b, a + b); 
  Console.WriteLine("{0} * {1} = {2}", a, b, a * b); 
 
  Console.Write("f = "); 
  double f = double.Parse(Console.ReadLine()); 
  Console.WriteLine("{0} * {1} / {2} = {3}", 
   a, b, f, a * b / f); 
 } 
} 
```
The result of program execution might be as follows (provided that we enter 
5, 6 and 7.5 as input): 
a = 5 
b = 6 
5 + 6 = 11 
5 * 6 = 30 
f = 7.5 
5 * 6 / 7.5 = 4 
In this particular example the specific thing is that we use parsing methods 
of numerical types and when wrong a result is passed (such as text) this 

will cause an error (exception) System.FormatException. This is especially 
true when reading real numbers,  because the delimiter used between the 
whole and fractional part is different in various cultures and depends on 
regional settings of the operating system. 
 
The separator for floating point numbers depends on the 
current language settings of the operatin g system (Regional 
and Language Options in Windows). In some systems as 
separator the character comma can be used, in others – point 
(dot). Entering a point (dot) instead of a comma will cause 
System.FormatException when the current  language settings 
use comma. 
The exceptions as a mechanism for reporting errors wi ll be discussed in the 
chapter "Exception Handling ". For now you can consider that when the 
program provides an error this is associated with the o ccurrence of an 
exception that prints detailed information about the error on the console. For 
example, let’s suppose that the regional settings of the computer are 
Bulgarian and we execute the following code: 
```csharp
Console.Write("Enter a floating-point number: "); 
string line = Console.ReadLine(); 
double number = double.Parse(line); 
Console.WriteLine("You entered: {0}", number); 
If we enter the number "3.14" (with a wrong decimal separator for the 
Bulgarian settings) we will get the following exception (error message): 
Unhandled Exception: System.FormatException: Input string was 
```
not in a correct format. 
 at System.Number.StringToNumber(String str, NumberStyles 
options, NumberBuffer& number, NumberFormatInfo info, Boolean 
parseDecimal) 
 at System.Number.ParseDouble(String value, NumberStyles 
options, NumberFormatInfo numfmt) 
 at System.Double.Parse(String s, NumberStyles style, 
NumberFormatInfo info) 
 at System.Double.Parse(String s) 
 at ConsoleApplication.Program.Main() in 
C:\Projects\IntroCSharpBook\ConsoleExample\Program.cs:line 14

## Parsing Numbers Conditionally

When parsing a string to a number using the method Int32.Parse(string) 
or by Convert.ToInt32(string) if the submitted string is not a number we 


get an exception. Sometimes it is ne cessary to catch the failed parsing and to 
print an error message or to ask the user to enter in a new value. 
Interception of an incorrectly entered number when parsing a sting can be 
done in two ways: 
- by catching exceptions (see the chapter "Exception Handling"); 
- by conditional parsing (using the method TryParse(…)). 
Let’s consider the conditional parsing of numbers  in .NET Framework. The 
method Int32.TryParse(…) accepts two parameters – a parsing string and a 
variable to record the result of parsing. If the parsing is successful the method 
returns value true. For greater clarity, let’s consider an example: 
string str = Console.ReadLine(); 
int intValue; 
bool parseSuccess = Int32.TryParse(str, out intValue); 
```csharp
Console.WriteLine(parseSuccess ? 
```
 "The square of the number is " + intValue * intValue + "." 
 : "Invalid number!"); 
In the example, conditional parsing of a string entered from the console to the 
integer type Int32 is performed. If we enter as  input "2", parsing will be 
successful so the result of TryParse() will be true, and the parsed number 
will be recorded in the variable intValue and on the console the squared 
number will be printed: 
Result: The square of the number is 4. 
If we try to parse an invalid number such as "abc", TryParse() will return 
false as a result and the user that will be notified that he has entered an 
invalid number: 
Invalid number! 
Note that the method TryParse() as a result of its work returns 
simultaneously two values : the parsed number (as an output parameter) 
and a Boolean value as a result of the method invocation. Returning multiple 
values at once is possible because one of the values is returned as an output 
parameter (out parameter). The output parameters return value in a 
predefined for the purpose variable coinciding with their type. When calling a 
method the output parameters must be preceded by the keyword out.

## Reading by Console.ReadKey()

The method Console.ReadKey() waits for key pressing on the console and 
reads its character equivalent without the need of pressing [Enter]. The 
result of invoking ReadKey() is information about the pressed key  (or 

more accurately a key combination) as an object of type ConsoleKeyInfo. 
The obtained object contains the character that is entered by the pressed key 
combination (property KeyChar) along with information about the keys 
[Shift], [Ctrl] and [Alt] (property Modifiers). For example, if we press 
[Shift+A] we will read a capital letter 'A' while in the Modifiers property we will 
have the Shift flag. Here is an example: 
ConsoleKeyInfo key = Console.ReadKey(); 
```csharp
Console.WriteLine(); 
Console.WriteLine("Character entered: " + key.KeyChar); 
Console.WriteLine("Special keys: " + key.Modifiers); 
```
If we execute the program and press [Shift+A], we will obtain the following 
result: 
A 
Character entered: A 
Special keys: Shift

## Simplified Reading of Numbers through Nakov.IO.Cin

There is no standard easy way to read several numbers, located on the same 
line, separated by a space. In C# and .NET Framework we need to read a 
string, split it into tokens using the space as separator and parse the obtained 
tokens to extract the numbers. In other languages and platforms like C++ we 
can dire ctly read numbers, characters and text from the console without 
parsing. This is not available in C# but we can use an external library or class.  
The standard library  Nakov.IO.Cin provides a simplified way to read 
numbers from the console. You can read about it from the blog of its author 
Svetlin Nakov: http://www.nakov.com/blog/2011/11/23/cin-class-for-csharp-
read-from-console-nakov-io-cin/. Once we have copied the file Cin.cs from 
Nakov.IO.Cin into our Visual Studio C# project, we could write code like this: 
```csharp
using Nakov.IO; 
```
… 
int x = Cin.NextInt(); 
double y = Cin.NextDouble(); 
decimal d = Cin.NextDecimal(); 
```csharp
Console.WriteLine("Result: {0} {1} {2}", x, y, d); 
```
If we execute the code, we can enter 3 numbers by putting any amount of 
whitespace separators between them. For example we can enter the first 
number, two spaces, the second number, a new line + space and the last 
number + space. The numbers will be read correctly  and the output will 
be as follows: 

3   2.5 
 3.58 
Result: 3 2.5 3.58