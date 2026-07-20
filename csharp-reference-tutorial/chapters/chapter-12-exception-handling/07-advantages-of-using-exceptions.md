# Advantages of Using Exceptions

So far we reviewed the exceptions in details, their characteristics and how to 
use them. Now let ’s find out why they were introduced and why they are so 
widely used.

## Separation of the Exception Handling Code

Using exceptions allow us to separate th e code, which describes the normal 
execution of the program from the code required for unexpected execution 
and the code for error handling. We will demonstrate this separation 
concept in the following example: 
void ReadFile() 
```csharp
{ 
 OpenTheFile(); 
 while (FileHasMoreLines) 
 { 
  ReadNextLineFromTheFile(); 
  PrintTheLine(); 
 } 
 CloseTheFile(); 
} 
```
Let’s explore the example step by step. It does the following: 
- Open the file; 
- While the file has more lines: 
- Read the next line from the file; 


- Print the line; 
- Close the file; 
The method looks good but a closer look brings up some questions: 
- What will happen if the file does not exist? 
- What will happen if the file cannot be opened? 
- What will happen if reading a line fails? 
- What will happen if the file cannot be closed?

### Error Handling without Exceptions

Let’s change the method having these questions in mind without using 
exceptions. Let ’s use error codes  returned by any method that we use. 
Using error codes is standard way for handling errors in procedure oriented 
programming, where every method returns int, which provides information 
whether the method was executed correctly. Error code 0 means that 
everything is correct. Any other code means some error. Different kinds of 
errors have different codes (usually it is a negative number). 
int ReadFile() 
```csharp
{ 
 errorCode = 0; 
 openFileErrorCode = OpenTheFile(); 
 
 // Check whether the file is open 
 if (openFileErrorCode == 0) 
 { 
  while (FileHasMoreLines) 
  { 
   readLineErrorCode = ReadNextLineFromTheFile(); 
   if (readLineErrorCode == 0) 
   { 
    // Line has been read properly 
    PrintTheLine(); 
   } 
   else 
   { 
    // Error during line reading 
    errorCode = -1; 
    break; 
   } 
  } 
  closeFileErrorCode = CloseTheFile(); 
  if (closeFileErrorCode != 0 && errorCode == 0) 

  { 
   errorCode = -2; 
  } 
  else 
  { 
   errorCode = -3; 
  } 
 } 
 else if (openFileErrorCode == -1) 
 { 
  // File does not exist 
  errorCode = -4; 
 } 
 else if (openFileErrorCode == -2) 
 { 
  // File can't be open 
  errorCode = -5; 
 } 
 return errorCode; 
} 
```
As a result we have a hard to understand and easy to break “spaghetti” 
code. Program logic is mixed with the error handling logic. Big parts of the 
code are the rules for error handling. Errors don ’t have type, description or 
stack trace and we have to wonder what the different error codes mean.

### Error Handling with Exceptions

We can avoid all of the above spaghetti code just by using exceptions. Here 
is how the same method will look like using exceptions instead: 
void ReadFile() 
```csharp
{ 
```
 try 
```csharp
 { 
  OpenTheFile(); 
  while (FileHasMoreLines) 
  { 
   ReadNextLineFromTheFile(); 
   PrintTheLine(); 
  } 
 } 
```
 catch (FileNotFoundException) 
```csharp
 { 
  DoSomething(); 

 } 
```
 catch (IOException) 
```csharp
 { 
  DoSomethingElse(); 
 } 
```
 finally 
```csharp
 { 
  CloseTheFile(); 
 } 
} 
```
In fact exceptions don’t save us the effort in finding and processing errors but 
give us more elegant, short, clear and efficient way to do it.

## Grouping Different Error Types

The hierarchical nature of exceptions allows us to catch and handle whole 
groups of exceptions at one time. When using catch we are not only catching 
the given type of exception but the whole hierarchy of exception types 
that are inheritors of the declared type. 
catch (IOException e) 
```csharp
{ 
 // Handle IOException and all its descendants 
} 
```
The example above will catch not only the IOException, but all of its 
descendants including FileNotFoundException, EndOfStreamException, 
PathTooLongException and many others. In the same time exceptions like 
UnauthorizedAccessException and OutOfMemoryException will not be 
caught, because they don ’t inherit from IOException. We can look in MSDN 
for the exceptions hierarchy if we wander which exceptions to catch. 
It is not a good practice, but it is possible to catch all exceptions: 
catch (Exception e) 
```csharp
{ 
 // A (too) general exception handler 
} 
```
Catching Exception and all of its inheritors is not a good practice. It is better 
to catch more specific groups of exceptions like IOException or just one type 
of exception like for example FileNotFoundException.

## Catching Exceptions at the Most Appropriate Place

The ability  to catch exceptions at multiple locations  is extremely 
comfortable. It allows us to handle the exception at the most appropriate 
place. Let’s demonstrate this with a simple comparison with the old approach 
```csharp
using error codes. Let’s have the following method structure: 
```
Method3() 
```csharp
{ 
 Method2(); 
} 
 
```
Method2() 
```csharp
{ 
 Method1(); 
} 
 
```
Method1() 
```csharp
{ 
 ReadFile(); 
} 
```
The method Method3() calls Method2(), which calls Method1() where 
ReadFile() is called. Let ’s suppose that Method3() is the method interested 
in eventual error in the ReadFile() method. If such error occurs in 
ReadFile() it wouldn’t be easy to transfer the error to Method3() using the 
traditional approach with error codes: 
void Method3() 
```csharp
{ 
 errorCode = Method2(); 
 if (errorCode != 0) 
  process the error; 
 else 
  DoTheActualWork(); 
} 
 
int Method2() 
{ 
 errorCode = Method1(); 
 if (errorCode != 0) 
  return errorCode; 
 else 
  DoTheActualWork(); 
} 

 
int Method1() 
{ 
 errorCode = ReadFile(); 
 if (errorCode != 0) 
  return errorCode; 
 else 
  DoTheActualWork(); 
} 
```
First in Method1() we have to analyze the error code returned by 
ReadFile() method and eventually pass it to Method2(). In Method2() we 
have to analyze the error code returned by Method1() and eventually pass it 
to Method3() where to handle the error itself. 
How can we avoid all this? Let ’s remember that that the CLR searches for 
exceptions back in the call stack of the methods and lets each of them to 
define catching and handling of the exceptions. If the method is not interested 
in catching some exception it is simply sent back in the stack: 
void Method3() 
```csharp
{ 
```
 try 
```csharp
 { 
  Method2(); 
 } 
```
 catch (Exception e) 
```csharp
 { 
  process the exception; 
 } 
} 
 
void Method2() 
{ 
 Method1(); 
} 
 
void Method1() 
{ 
 ReadFile(); 
} 
```
If an error occurs during reading the file it will be ignored in Method1() and 
Method2() and will be caught and handled in  Method3() where is the most 
appropriate place to handle the error. Let ’s remember again the most 

important rule: every method should catch only exceptions that can handle 
and skip all the others.