# Our First C# Program

Before we continue with an in depth description of the C# language and the 
.NET platform, let’s take a look at a simple example, illustrating how a 
program written in C# looks like: 
```csharp
class HelloCSharp 
{ 
 static void Main(string[] args) 
 { 
  System.Console.WriteLine("Hello C#!"); 
 } 
} 
```
The only thing this program does is to print the message "Hello, C#!"  on 
the default output. It is still early to execute it, which is why we will only take 
a look at its structure. Later we will describe in full how to compile and run a 
given program from the command prompt as well as from a development 
environment.

## How Does Our First C# Program Work?

Our first program consists of three logical parts: 
- Definition of a class HelloCSharp; 
- Definition of a method Main(); 
- Contents of the method Main().

### Defining a Class

On the first line of our program we define a class called HelloCSharp. The 
simplest definition of a class consists of the keyword class, followed by its 
name. In our case the name of the class is HelloCSharp. The content of the 
class is located in a block of program lines, surrounded by curly brackets: {}.

### Defining the Main() Method

On the third line we define a method with the name Main(), which is the 
starting point for our program. Every program written in C# starts from a 
Main() method with the following title (signature): 
static void Main(string[] args) 
The method must be declared as shown above, it must be static and void, it 
must have a name Main and as a list of parameters it must have only one 
parameter of type array of string. In our example the parameter is called 
args but that is not mandatory. This parameter is not used in most cases so it 
can be omitted (it is optional). In that case the entry point of the program can 
be simplified and will look like this: 
static void Main() 
If any of the aforementioned requirements is not met, the program will 
compile but it will not start because the starting point is not defined correctly.

### Contents of the Main() Method

The content of every method is found after its signature, surrounded by 
opening and closing curly brackets. On the next line of our sample program 
we use the system object System.Console and its method WriteLine() to 
print a message on the default output (the console), in this case "Hello, C#!". 
In the Main() method we can write a random sequence of expressions and 
they will be executed in the order we assigned to them. 
More information about expressions can be found in chapter " Operators and 
Expressions", working with the console is described in chapter " Console Input 
and Output", classes and methods can be found in chapter "Defining Classes".

## C# Distinguishes between Uppercase and Lowercase!

The C# language distinguishes between uppercase and lowercase letters so 
we should use the correct casing when we write C# code. In the example 
above we used some keywords like class, static, void and the names of 
some of the system classes and objects, such as System.Console. 
 
Be careful when writing! The same thi ng, written in upper -
case, lower-case or a mix of both, means different things in 
C#. Writing Class is different from class and System.Console 
is different from SYSTEM.CONSOLE. 
This rule applies to all elements of your program: keywords, names of 
variables, class names etc.

## The Program Code Must Be Correctly Formatted

Formatting is adding characters such as spaces, tabs and new lines, which are 
insignificant to the compiler and they give the code a logical structure and 
make it easier to read . Let’s for example take a look at our first program 
(the short version of the Main() method): 
```csharp
class HelloCSharp 
{ 
 static void Main() 
 { 
  System.Console.WriteLine("Hello C#!"); 
 } 
} 
```
The program contains seven lines of code and some of them are indented 
more than others. All of that can be written without tabs as well, like so: 
```csharp
class HelloCSharp 
{ 
static void Main() 
{ 
System.Console.WriteLine("Hello C#!"); 
} 
} 
```
Or on the same line: 
```csharp
class HelloCSharp{static void Main(){System.Console.WriteLine( 
"Hello C#!");}} 
```
Or even like this: 


```csharp
                                             class 
```
    HelloCSharp 
```csharp
{ 
    static void                             Main() 
    {                             System         . 
Console.WriteLine("Hello C#!")       ;}          } 
```
The examples above will compile and run exactly like the formatted code but 
they are more difficult to read and understand , and therefore difficult to 
modify and maintain. 
 
Never let your programs contain unformatted code! That 
severely reduces program readability and leads to difficulties 
for later modifications of the code.

## Main Formatting Rules

If we want our code to be correctly formatted, we must follow several 
important rules regarding indentation: 
- Methods are indented inside the definition of the class  (move to the 
right by one or more [Tab] characters); 
- Method contents are indented inside the definition of the method; 
- The opening curly bracket { must be on its own line and placed exactly 
under the method or class it refers to; 
- The closing curly bracket } must be on its own line, placed exactly 
vertically under the respective opening bracket ( with the same 
indentation); 
- All class names must start with a capital letter; 
- Variable names must begin with a lower-case letter; 
- Method names must start with a capital letter; 
Code indentation  follows a very simple rule: when some piece of code is 
logically inside another piece of code, it is indented (moved) on the right with 
a single [Tab]. For example if a method is defined inside a class, it is indented 
(moved to the right). In the same way  if a method body is inside a method, it 
is indented. To simplify this, we can assume that when we have the character 
“{“, all the code after it until its closing “}” should be indented on the right.

## File Names Correspond to Class Names

Every C# program co nsists of one or several class definitions . It is 
accepted that each class is defined in a separate file with a name 
corresponding to the class name and a .cs extension. When these 
requirements are not met, the program will still work but navigating the co de 


will be difficult. In our example, the class is named HelloCSharp, and as a 
result we must save its source code in a file called HelloCSharp.cs.

> [!TIP]
> **Modern C# Note: Top-Level Statements**
> In C# 9.0 and later, you no longer need to write a verbose `class` and `static void Main` wrapper for simple programs. You can write your code directly at the root of the file:
> ```csharp
> System.Console.WriteLine("Hello C#!");
> ```
