# Strings

In practice  we often come to the text processing : reading text files, 
searching for keywords and replacing them in a paragraph, validating user 
input data, etc… In such cases we can save the text content, which we will 
need in strings, and process them using the C# language.

## What Is a String?

A string is a sequence of characters stored in a certain address in memory. 
Remember the type char? In the variable of type char we can record only 
one character. Where it is necessary to process more than one character then 
strings come to our aid. 
In. NET Framework each character has a serial number from the Unicode 
table. The Unicode standard is established in the late 80s and early 90s in 
order to store different types of text data. Its predecessor ASCII is able to 
record only 128 or 256 characters (respective ASCII standard with 7 -bit or 8-
bit table). Unfortunately, thi s often does not meet user needs – as we can fit 
in 128 characters only digits, uppercase and lowercase Latin letters and some 
specific individual characters. When you have to work with text in Cyrillic or 
other specific language (e .g. Chinese or Arabian), 128 or 256 characters are 
extremely insufficient. Here is why .NET uses 16 -bit code table for the 
characters. With our knowledge of number systems and representation of 
information in computers, we can calculate that the code table store 2^16 = 

65,536 cha racters. Some characters are encoded in a specific way, so it is 
possible to use two characters of the Unicode table to create a new character 
– the resulting signs exceed 100,000.

## The System.String Class

The class System.String enables us to handle strings in C# . For declaring 
the strings we will continue using the keyword string, which is an alias in 
C# of the System.String class from .NET Framework. The work with string 
facilitates us in manipulating the text content: construction of texts, text 
search and many other operations. 
Example of declaring a string: 
string greeting = "Hello, C#"; 
We have just declared the variable greeting of type string whose content is 
the text phrase "Hello, C#". The representation of the content in the string 
looks closely to this: 
H e l l o ,  C # 
The internal representation of the class is quite simple – an array of 
characters. We can avoid the usage of the class by declaring a variable of 
type char[] and fill in the array ’s elements character by character. However, 
there are some disadvantages too: 
1. Filling in the array happens character by character, not at once. 
2. We should know the length of the text in order to be aware whether it 
will fit into the already allocated space for the array. 
3. The text processing is manual.

### The String Class: Universal Solution?

The usage of System.String is not the ideal and universal solution – 
sometimes it is appropriate to use different character structures. 
In C# we there are other classes for text processing – we will become familiar 
with some of them later in this chapter. 
The type string is more special from other data types. It is a class and as 
such it complies with the principles of object -oriented programming. Its 
values are stored in the dynamic memory  (managed heap ), and the 
variables of type string keeps a reference to an object in the heap.

### Strings are Immutable

The string class has an important feature – the character sequences stored 
in a variable of the class are never changing ( immutable). After being 
assigned once, the content of the variable does not change directly – if we try 

 
to change the value, it will be saved to a new location in the dynamic memory 
and the variable will point to it. Since this is an important feature, it will be 
illustrated later.

### Strings and Char Arrays

Strings are very similar to the char arrays ( char[]), but unlike them, they 
cannot be modified. Like the arrays, they have properties such as Length, 
which returns the length of the string and allows access by index. Indexing, 
as it is us ed in arrays, takes indices from 0 to Length-1. Access to the 
character of a certain position in a string is done with the operator [] 
(indexer), but it is allowed only to read characters (and not to write to them): 
string str = "abcde"; 
char ch = str[1]; // ch == 'b' 
str[1] = 'a'; // Compilation error! 
ch = str[50]; // IndexOutOfRangeException

## Strings – Simple Example

Let’s give an example for using variables from the type string: 
string message = "This is a sample string message."; 
 
```csharp
Console.WriteLine("message = {0}", message); 
Console.WriteLine("message.Length = {0}", message.Length); 
 
for (int i = 0; i < message.Length; i++) 
{ 
 Console.WriteLine("message[{0}] = {1}", i, message[i]); 
} 
// Console output: 
// message = This is a sample string message. 
// message.Length = 31 
// message[0] = T 
// message[1] = h 
// message[2] = i 
// message[3] = s 
// … 
Please note the string value – the quotes are not part of the text, they are 
```
enclosing its value. The example demonstrates how to print a string, how to 
extract its length and how to extract the character from which it is composed.

## Strings Escaping

As we a lready know, if we want to use quotes into the string content, we 
must put a slash before them to identify that we consider the quotes 
character itself and not using the quotation marks for ending the string: 
string quote = "Book's title is \"Intro to C#\""; 
// Book's title is "Intro to C#" 
The quotes in the example are part of the text. They are added in the variable 
by placing them after the escaping character backslash ( \). In this way the 
compiler recognizes that the quotes are not used to start or end  a string, but 
are a part of the data. Displaying special characters in the source code  is 
called escaping.

## Declaring a String

We can declare variables from the type string by the following rule: 
string str; 
Declaring a string  represents a variable declar ation of type string. This is 
not equivalent to setting a variable and allocating memory for it! With the 
declaration we inform the compiler that the variable str will be used and the 
expected type for it is string. We do not create a variable in the memory and 
it is not available for processing yet (value is null, which means no value).

## Creating and Initializing a String

In order to process the declared string variable, we must create it and 
initialize it. Creating a variable  of certain class (also known as instantiating) 
is a process associated with the allocation of the dynamic memory area  (the 
heap). Before setting a specific value to the string, its value is null. This can 
be confusing to the beginner programmers: uninitialized variables of type 
string do not contain empty values, it contains the special value null – and 
each attempt for manipulating such a string will generate an error (exception 
for access to a missing value NullReferenceException)! 
We can initialize variables in the following three ways: 
1. By assigning a string literal. 
2. By assigning the value of another string. 
3. By passing the value of an operation which returns a string.

### Setting a String Literal

Setting a string literal means to assign a predefined textual content  to a 
variable of type string. We use this type of initialization, when we know the 
value that must be stored in the variable. Example for setting a string literal: 

 
string website = "http://www.wikipedia.org"; 
In this example we created the variable website with value the above stated 
string literal.

### Assigning Value of Another String

Assigning a value is equivalent to directing a string value or a variable to a 
variable of type string. An example is the following code snippet: 
string source = "Some source"; 
string assigned = source; 
First, we declare and initialize the variable source. Then the variable 
assigned takes the value of source. Since the string class is a reference 
type, the text "Some source" is stored in the dynamic memory (heap) on an 
address defined by the first variable. 
 
In the second line we redirect the variable assigned to the same place, which 
the other variable points to. In this way the two objects receive the same 
address in dynamic memory and hence the same value. 
The change  of either variable will affect only itself  because of the 
immutability of the type string, as when a change occurs, a copy of the 
changed string will be created. This is not true for the rest of the reference 
types (the normal, mutable types) because with  them the changes are made 
directly in the address in memory and all references point to this changed 
address.

### Passing a String Expression

The third option to initialize a string is to pass the value of a string  
expression or operation, which  returns a str ing result. This can be a result 
from a method, which validates data; adding together the values of a number 
of constants and variables; transforming an existing variable, etc. 
Example of an expression, which returns a string: 
string email = "some@gmail.com"; 
HeapStack
string@42e816
source
Some source
string@42e816
assigned

string info = "My mail is: " + email; 
// My mail is: some@gmail.com 
The info variable has been created from the concatenation of literals and a 
variable.

## Reading and Printing to the Console

Let’s now take a look at the ways of reading strings, entered by the user and 
how we print strings to the console.

### Reading Strings

Reading strings can be accomplished through the methods of the well -known 
System.Console class: 
string name = Console.ReadLine(); 
In this example we read from the console the input data through the method  
ReadLine(). It waits for the user to input a value and to press [Enter]. After 
pressing the [Enter] key the variable name will contain the input name typed 
at the console (read from the keyboard). 
What can we do after the variable has been created and it has a value itself? 
We can use it, for example, in expressions with other strings, to pass it as a 
method’s parameter, to write it in text documents, etc. First, we can write it 
to the console in order to be sure that the data has been correctly read.

### Printing Strings

Taking the data to the standard output is made also by the well -known class 
System.Console: 
```csharp
Console.WriteLine("Your name is: " + name); 
By using the method WriteLine(…) we are getting the message " Your name 
```
is: " followed by the value of the name variable. After the end of the message 
a new line character is added. If we want to run away from the new line, 
which means the messages will appear at one  and the same line  then we use 
the method, Write(…). 
We can refresh our knowledge on the System.Console class from the chapter 
"Console Input and Output".

> [!TIP]
> **Modern C# Note: String Interpolation**
> In C# 6.0, **String Interpolation** was introduced. Instead of using `string.Format` or manual string concatenation, you can prefix the string with `$` and put variables directly in curly braces:
> ```csharp
> string name = "John";
> int age = 30;
> Console.WriteLine($"Hello, my name is {name} and I am {age} years old.");
> ```
