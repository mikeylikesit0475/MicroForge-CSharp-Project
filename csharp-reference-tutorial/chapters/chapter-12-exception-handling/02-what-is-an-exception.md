# What Is an Exception?

When we write a program, we describe step -by-step what the computer must 
do (at least in imperative programming; in the functional programming things 
look a bit different) and in most of the cases we rely that the program will 
execute normally. Indeed, most of the time, programs are following this 
normal pattern, but there are some exceptions. Let ’s say we want to read a 
file and display its contents on the screen. Let ’s assume the file is located on 
a remote server and during the process of reading it, the connection goes 
down. The file then will be only partially loaded. The program will not be able 
to execute normally and show file ’s contents on the screen. In this case, we 
have an exception from the normal (and correct) program execution and this 
exception must be reported to the user and/or the administrator.

## Exceptions

Exception is a notification that something interrupts the normal 
program execution . Exceptions provide a programming paradigm for 
detecting and reacting to unexp ected events. When an exception arises, the 
state of the program is saved, the normal flow is interrupted and the control 
is passed to an exception handler (if such exists in the current context). 
Exceptions are raised or thrown by programming code that must send a 
signal to the executing program about an error or an unusual situation . 
For example, if we try to open a file, which doesn’t exist, the code responsible 
for opening the file will detect this and will throw an exception  with a proper 
error message. 

Exceptions are one of the main paradigms of object -oriented programming  
(OOP), which is described in detail s in the  chapter " Object-Oriented 
Programming Principles".

## Catching and Handling Exceptions

Exception handling is a mechanism, which  allows exceptions to be 
thrown and caught . This mechanism is provided internally by the CLR 
(Common Language Runtime). Parts of the exception handling infrastructure 
are the language c onstructs in C#  for throwing and catching exceptions. 
CLR takes care to propagate each exception to the code that can handle it.

## Exceptions in the Object-Oriented Programming

In Object -Oriented Programming (OOP), exceptions are a powerful mecha -
nism for centralized processing of errors  and exceptional situations. This 
mechanism replaces the procedure-oriented method of error handling in which 
each function returns a code indicating an error or a successful execution. 
Usually in OOP, a code executing some op eration will cause an exception if 
there is a problem and the  operation could not be successfully 
completed. The method causing the operation could catch the exception 
(and handle the error) or pass the exception through to the calling method. 
This allows handling errors to be delegated to some upper level in the call 
stack and in general, allows flexible management of errors and unexpected 
situations. 
Another fundamental concept is exceptions hierarchy . In OOP, exceptions 
are classes and they can be inheri ted to build hierarchies. When an exception 
is handled (caught), the handling mechanism could catch a whole class of 
exceptions and not just a particular error (as in the traditional procedural 
programming). 
In OOP, it is recommended to use exceptions for managing error 
situations or unexpected events  that may arise during a program 
execution. This replaces the procedural error -handling approach and gives 
important advantages such as centralized error processing, handling multiple 
errors in one place and ab ility to pass errors to a higher -level handler. 
Another important advantage is that exceptions self -describe themselves and 
can create hierarchies. 
Sometimes exceptions are used not so much to signal a problem but to 
handle some expected event. This is not  considered a good practice as 
exceptions should not control the normal flow of the program. At the end of 
the chapter we will look in more details into this.

## Exceptions in .NET

Exception in .NET is an object, which signals an error or an event, which is 
not anticipated in the normal program flow. When such unusual event takes 
place, the executing method ’throws' a special object containing information 

about the type of the error, the place in the program where the error occurred 
as well as the program state at the moment of the error. 
Each exception in .NET contains the so -called stack trace , which gives 
information of where exactly the error occurred. This will be discussed in more 
details later in this chapter.

## An Example Code Throwing an Exception

Here is an example for a code that will throw an exception: 
```csharp
class ExceptionsDemo 
{ 
 static void Main() 
 { 
  string fileName = "WrongTextFile.txt"; 
  ReadFile(fileName); 
 } 
 
 static void ReadFile(string fileName) 
 { 
  TextReader reader = new StreamReader(fileName); 
  string line = reader.ReadLine(); 
  Console.WriteLine(line); 
  reader.Close(); 
 } 
} 
This program will compile successfully but if you run it, the result will look like 
```
the following (FileNotFoundException dumped on the console): 
 


In this example, we have a code trying to open a text file for reading and then 
display the first line of this file on the screen. We will discuss working with 
files in more details in the chapter "Text Files". 
The first two lines of ReadFile() contain code that throws an exception. In 
this example, if the file WrongTextFile.txt doesn’t exist, the constructor 
StreamReader(string, fileName) will throw a FileNotFoundException. If 
an unexpected problem occurs during  the input-output operations, the stream 
methods, such as ReadLine() will throw an IOException. 
The code above will successfully compile but at run -time it will throw an 
exception if the WrongTextFile.txt file does not exist. The end result in this 
case is an error message displayed on the console. The console output also 
contains information of where and how the error occurred.

## How Do Exceptions Work?

If during the normal program execution one of the methods throws an 
exception, the normal flow of the program is interrupted. In the example 
above this happens when the StreamReader is initialized. Let’s take a look on 
the following line: 
TextReader reader = new StreamReader("WrongTextFile.txt"); 
If this line triggers an error, the reader local variable will not be initialized and 
it will have its default value of null. None of the lines that follow in the 
method will be executed. The program will be interrupted until the CLR finds a 
handler that can process the exception.

## Catching Exceptions in C#

After a method throws an exception, CLR is looking for an exception handler 
that can process the error. To understand how this works, we will take a 
closer look on the concept of a call -stack. The program call-stack is a stack 
structure that holds information about method calls, their local variables, 
method parameters and the memory for value types. 
.NET program s start from the Main(…) method, which is the entry point of 
the program. Another method, let ’s name it "Method 1" could be called from 
Main. Let "Method 1" call "Method 2" and so on until "Method N" is called. 
When "Method N" finishes, the program flow returns back to its calling 
method (in our example it would be "Method N -1"), then back to its calling 
method and so on . This goes on until the Main(…) method is reached. Once 
Main(…) finishes, the entire program exits. 
The general principle is that when a new method is called, it is pushed on 
top of the stack . When the method finishes, it is pulled back from the 
stack. At any given point in time,  the call -stack contains all the methods 
called during the execution – from the starting method Main(…) to the last 

called method, which  is currently executing , along with their local variables 
and arguments taken as input. 
The exception handling mechanism  follows a reversed process. When an 
exception is thrown, CLR begins searching an exception handler in the call-
stack starting from the method that has thrown the exception. This is 
repeated for each of the methods down the call -stack until a handler is fo und 
which catches the exception. If Main(…) is reached and no handler is found, 
CLR catches the exception and usually displays an error message (either in 
the console or in a special error dialog box). 
The described method call and exception handling process could be 
visualized in the following diagram (steps 1 through 5):

## The try-catch Programming Construct

To handle an exception, we must surround the code that could throw an 
exception with a try-catch block: 
try 
```csharp
{ 
 // Some code that may throw an exception 
} 
```
catch (ExceptionType objectName) 
2. Method call
3. Method call
4. Method call
8. Find handler
7. Find handler
6. Find handler
5. Throw an exception
Main()
Method 1
Method 2
Method N
…
Main()
Method 1
Method 2
Method N
…
.NET 
CLR


```csharp
{ 
 // Code handling an Exception 
} 
```
catch (ExceptionType objectName) 
```csharp
{ 
 // Code handling an Exception 
} 
```
The try-catch construct consists of one try block and one or more catch 
blocks. Within the try block we put the code that could throw exceptions. The 
ExceptionType in the catch block must be a type, derived from 
System.Exception or the code wouldn ’t compile. The expression within 
brackets after catch is also a declaration of a variable, thus inside the catch 
block we can use objectName to use the properties of the exception or call its 
methods.

## Catching Exceptions – Example

Let’s now modify the code in our previous example to make it handle its 
exceptions. To do this, we wrap the code that could create problems in try-
catch and then we add catch blocks to handle the two types of exceptions we 
know could arise. 
```csharp
static void ReadFile(string fileName) 
{ 
 // Exceptions could be thrown in the code below 
```
 try 
```csharp
 { 
  TextReader reader = new StreamReader(fileName); 
  string line = reader.ReadLine(); 
  Console.WriteLine(line); 
  reader.Close(); 
 } 
```
 catch (FileNotFoundException fnfe) 
```csharp
 { 
  // Exception handler for FileNotFoundException 
  // We just inform the user that there is no such file 
  Console.WriteLine( 
   "The file '{0}' is not found.", fileName); 
 } 
```
 catch (IOException ioe) 
```csharp
 { 
  // Exception handler for other input/output exceptions 
  // We just print the stack trace on the console 
  Console.WriteLine(ioe.StackTrace); 

 } 
} 
```
Now our method works in a different way. When FileNotFoundException is 
thrown during the StreamReader initialization when executing the constructor 
new StreamReader(filename), the CLR will not execute the following lines 
but will jump to the row where we catch the exception catch 
(FileNotFoundException fnfe): 
catch (FileNotFoundException fnfe) 
```csharp
{ 
 // Exception handler for FileNotFoundException 
 // We just inform the user that there is no such file 
 Console.WriteLine("The file '{0}' is not found.", fileName); 
} 
```
In our example, users will simply be informed that such file does not exist by 
a message printed on the standard output: 
The file 'WrongTextFile.txt' is not found. 
Similarly, if an IOException is thrown during reader.ReadLine(), it is 
handled by the block below: 
catch (IOException ioe) 
```csharp
{ 
 // Exception handler for FileNotFoundException 
 // We just print the stack trace on the screen 
 Console.WriteLine(ioe.StackTrace); 
} 
```
In this case, we display the exception stack trace on the standard output. 
The lines between where the exception is thrown and the catch block that 
processed it are not executed. 
 
Showing the full information about the exception to the end 
user is not always a good practice! 
We will discuss the best practices in exception handling later in this chapter.

## Stack Trace

The stack trace  contains detailed information about the exception  
including where exactly it occurred in the program. The stack trace is very 
useful for programmers when they try to understand the problem causing the 
exception. The information in the stack trace is very technical and is designed 


to be used by programmer s and system administrators and not by the end 
users. During debugging the stack trace is a priceless tool.

## Stack Trace – Example

Here is the stack trace from our first example: 
Unhandled Exception: System.IO.FileNotFoundException: Could not 
find file '…\WrongTextFile.txt'. 
 at System.IO.__Error.WinIOError(Int32 errorCode, String 
maybeFullPath) 
 at System.IO.FileStream.Init(String path, FileMode mode, 
FileAccess access, Int32 rights, Boolean useRights, FileShare 
share, Int32 bufferSize, FileOptions options, 
SECURITY_ATTRIBUTES secAttrs, String msgPath, Boolean 
bFromProxy, Boolean useLongPath) 
 at System.IO.FileStream..ctor(String path, FileMode mode, 
FileAccess access, FileShare share, Int32 bufferSize, 
FileOptions options) 
 at System.IO.StreamReader..ctor(String path, Encoding 
encoding, Boolean detectEncodingFromByteOrderMarks, Int32 
bufferSize) 
 at System.IO.StreamReader..ctor(String path) 
 at Exceptions.Demo1.ReadFile(String fileName) in 
Program.cs:line 17 
 at Exceptions.Demo1.Main() in Program.cs:line 11 
The system cannot find the file named “ WrongTextfile.txt” and the 
FileNotFoundException is thrown.

## Reading the Stack Trace

To be able to use the stack trace, we must be familiar with its structure. 
The stack trace contains the following information: 
- The full name of the exception class; 
- A message with additional information about the error; 
- Information about the call-stack; 
In our example above, the full name of the exception is 
System.IO.FileNotFoundException. The error message follows: " Could not 
find file ' …\WrongTextFile.txt'." What follows is a full call -stack dump, 
which is usually the longest part of the stack trace. Each line of the call stack 
dump contains something similar to the following: 
 at <namespace>.<class>.<method> in <source file>.cs:line <line> 

Every method is shown in a separate line. On the first line is the method that 
threw the exception and on the least line – the Main() method (notice that 
the Main() method might not be present in case of an exception thrown by a 
thread which is not the main thread of the program). Every method is given 
with full information about the class that contains it and (if possible) even  the 
line in the source code: 
 at Exceptions.Demo1.ReadFile(String fileName) in 
…\Program.cs:line 17 
The line numbers are included only if the respective class is compiled with 
debug information (this information contains line numbers, variable names 
and other technical information). The debug information is not included in the 
.NET assemblies but is in separate files called 'debug symbols' (.pdb). As you 
can see in the example stack trace, debug information is available for some 
assemblies, while for others (like the .NET assemblies) it is not . This is why 
some entries in the stack trace have line numbers and others – not. 
If the method throwing the exception is a constructor, then instead of method 
name, the stack trace contains the word .ctor, like in System.IO. 
StreamReader..ctor(String path). 
This rich information in the stack trace allows quickly and easily to find the 
class, the method and even the source line where the error has occurred. 
Then usually it is relatively straightforward to analyze the problem causing the 
error and fixing it.  This is not the same in primitive languages such as C and 
Pascal where the concept of stack trace is not supported.

## Throwing Exceptions (the throw Construct)

Exceptions in C# are thrown using the keyword throw. We need to provide an 
instance of the except ion, containing all the necessary information about the 
error. Exceptions are normal classes and the only requirement is that they 
inherit directly or indirectly from the System.Exception class. 
Here is an example: 
```csharp
static void Main() 
{ 
 Exception e = new("There was a problem");
 throw e; 
} 
```
The result from running this program is: 
Unhandled Exception: System.Exception: There was a problem 
 at Exceptions.Demo1.Main() in Program.cs:line 11