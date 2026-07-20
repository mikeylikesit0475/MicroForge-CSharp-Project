# Literals

Primitive types, which we already met, are special data types built into the C# 
language. Their values specified in the source code of the progra m are called 
literals. One example will make this clearer: 
bool result = true; 
char capitalC = 'C'; 
byte b = 100; 
short s = 20000; 
int i = 300000; 
In the above example, literals are true, 'C', 100, 20000 and 300000. They 
are variable values set directly in the source code of the program.

## Types of Literals

In C# language, there are several types of literals: 
- Boolean 
- Integer 
- Real 
- Character 
- String 
- Object literal null

## Boolean Literals

Boolean literals are: 
- true 
- false 
When we assign a value to a variable of type bool we can use only one of 
these two values or a Boolean expression (which is calculated to true or 
false).

### Boolean Literals – Example

Here is an example of a declaration of a variable of type bool and assigning a 
value, which represents the Boolean literal true: 
bool result = true;

## Integer Literals

Integer literals are sequences of digits, a sign (+, -), suffixes and prefixes. 
Using prefixes we can present integers in the program source in decimal or 
hexadecimal format. More information about the different numeral systems 
we can find in the chapter " Numeral Systems ". In the integer literals the 
following prefixes and suffixes may take part: 
- "0x" and " 0X" as  prefix indicates hexadecimal values, for example 
0xA8F1; 
- 'l' and 'L' as suffix indicates long type data, for example 357L. 
- 'u' and 'U' as suffix indicates uint or ulong data type, for example 112u. 
By default (if no suffix is used) the integer literals are of type int.

### Integer Literals – Examples

Here are some examples of using integer literals: 
// The following variables are initialized with the same value 
int numberInDec = 16; 
int numberInHex = 0x10; 
 
// This will cause an error, because the value 234L is not int 
int longInt = 234L;

## Real Literals

Real literals are a sequence of digits, a sign (+, -), suffixes and the decimal 
point character. We use them for values of type float, double and decimal. 
Real literals can be represented in exponential format. They also use the 
following indications: 
- 'f' and 'F' as suffixes mean data of type float; 
- 'd' and 'D' as suffixes mean data of type double; 
- 'm' and 'm' as suffixes mean data of type decimal; 
- 'e' is an exponent, for example, " e-5" means the integer part multiplied 
by 10-5. 
By default (if there is no suffix), the real numbers are of type double.

### Real Literals – Examples

Here are some examples of real literals' usage: 
// The following is the correct way of assigning a value: 
float realNumber = 12.5f; 

 
// This is the same value in exponential format: 
realNumber = 1.25e+1f; 
 
// The following causes an error, because 12.5 is double 
float realNumber = 12.5;

## Character Literals

Character literals are single characters enclosed in apostrophes  (single 
quotes). We use them to set the values of type char. The value of a character 
literal can be: 
- a character, for example 'A'; 
- a character code, for example '\u0065'; 
- an escaping sequence;

### Escaping Sequences

Sometimes it is necessary to work with characters that are not displayed on 
the keyboard or with characters that have special meaning s, such as the “new 
line” character. They cannot be represented directly  in the source code of 
the program and in order to u se them we need special techniques, which we 
will discuss now. 
Escaping sequences are literals. They are a sequence of special characters, 
which describe a character that cannot be written directly in the source code. 
This is, for instance, the “new line” character. 
There are many examples of characters that cannot be represented directly in 
the source code: a double quotation mark, tab, new line, backslash and 
others. Here are some of the most frequently used escaping sequences: 
- \' – single quote 
- \" – double quotes 
- \\ – backslash 
- \n – new line 
- \t – offset (tab) 
- \uXXXX – char specified by its Unicode number, for example \u03A7. 
The character \ (backslash) is also called an escaping character because it 
allows the display on screen (or other output device) o f characters that have 
special meaning or effect and cannot be represented directly in the source 
code.

### Escaping Sequences – Examples

Here are some examples of character literals: 
// An ordinary character 
char character = 'a'; 
```csharp
Console.WriteLine(character); 
 
// Unicode character code in a hexadecimal format 
character = '\u003A'; 
Console.WriteLine(character); 
 
// Assigning the single quotiation character (escaped as \') 
character = '\''; 
Console.WriteLine(character); 
 
// Assigning the backslash character (escaped as \\) 
character = '\\'; 
Console.WriteLine(character); 
 
// Console output: 
// a 
// : 
// ' 
// \
```

## String Literals

String literals are used for data of type string. They are a sequence of 
characters enclosed in double quotation marks. 
All the escaping rules for the char type discussed above are also valid for 
string literals. 
Strings can be preceded by the @ character that specifies a quoted string  
(verbatim string). In quoted strings the rules for escaping are not valid, i.e. 
the character \ means \ and is not an escaping character. Only one character 
needs to be escaped in the quoted strings – the character " (double-quotes) 
and it is escaped in the following way – by repeating it "" (double double -
quotes). All other characters are treated literally, even the new line. Quoted 
strings are often used for the file system paths naming.

### String Literals – Examples

Here are few examples for string literals usage: 
string quotation = "\"Hello, Jude\", he said."; 

```csharp
Console.WriteLine(quotation); 
string path = "C:\\Windows\\Notepad.exe"; 
Console.WriteLine(path); 
string verbatim = @"The \ is not escaped as \\. 
I am at a new line."; 
Console.WriteLine(verbatim); 
// Console output: 
// "Hello, Jude", he said. 
// C:\Windows\Notepad.exe 
// The \ is not escaped as \\. 
// I am at a new line. 
```
More about strings we will find in the chapter "Strings and Text Processing".