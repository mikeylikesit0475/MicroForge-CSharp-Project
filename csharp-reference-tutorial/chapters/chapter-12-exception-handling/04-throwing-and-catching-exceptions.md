# Throwing and Catching Exceptions

Let’s look in more details at throwing and catching exceptions.

## Nested Exceptions

We’ve already seen that each exception could contain a nested (inner) 
exception. Let’s explain in more detail s why it is a common practice in OOP 
error handling to wrap exceptions in this way. 
In software engineering, it is a good practice for every software component to 
define small number of specific application exceptions . The component 
then would throw only these specific application exceptions and not the 
standard .NET exceptions. In this way the users of the software component 
would know what exceptions could expect from it. 
For instance, if we have a banking software and we have a com ponent dealing 
with interests, this component would define (and throw) exceptions like 
InterestCalculationException and InvalidPeriodException. The interest 
component should not throw exceptions like FileNotFoundException, 
DivideByZeroException and NullReferenceException. When an error 
occurs, which is not directly related to interest calculation, the respective 
exception is wrapped in InterestCalculationException and the calling code 
will be informed that the interest calculation was not correctly done. 
Still, these business application exceptions usually do not have detailed 
technical information about the nature of the problem. This is why, it is 
considered a good practice to include technical details about the problem and 
this is where inner exceptions come in handy. When the component throws its 
application exception, it should keep the original exception as an inner 
exception in order to preserve the technical details about the error. 
Another example is when a software component (let ’s call it Component  A) 
defines its own application exceptions ( A-exceptions). This component 
internally uses another component (called Component B). If for some reason B 
throws a B exception (an exception defined in B), perhaps A will have to 
propagate the error because it w ill not be able to do its task. And because A 
cannot simply throw a B -exception, it must throw an A-exception, containing 
the B-exception as a nested exception. 

There could be various reasons why A cannot simply throw a B exception: 
- Component A users should not even know Component B exists (see the 
discussion regarding abstractions in the "Principles of OOP" chapter); 
- Component A had not declared it would throw Component B exceptions; 
- Component A users are not prepared to receive Component B 
exceptions. They expect component A exceptions only.

## How to Read the Stack Trace with Nested Exceptions?

Below we have  an example that creates an exc eption chain. We will 
demonstrate how such exception chain is created and how the stack trace 
looks like in the output: 
37 
38 
39 
40 
41 
42 
43 
44 
45 
46 
47 
48 
49 
50 
51 
52 
53 
54 
55 
```csharp
static void Main() 
{ 
```
 try 
```csharp
 { 
  string fileName = "WrongFileName.txt"; 
  ReadFile(fileName); 
 } 
```
 catch (Exception e) 
```csharp
 { 
  throw new ApplicationException("Smth. bad happened", e); 
 } 
} 
static void ReadFile(string fileName) 
{ 
 TextReader reader = new StreamReader(fileName); 
 string line = reader.ReadLine(); 
 Console.WriteLine(line); 
 reader.Close(); 
} 
```
In this example, we call the ReadFile() method (line 42), which will throw an 
exception (line 51) because the file " WrongFileName.txt" does not exist. In 
the Main() method we catch all exceptions  (line 44), wrap them into a new 
exception of type ApplicationException and throw them again  (line 46). As 
we shall see later in the section "Grouping Different Error Types" , caching an 
Exception also catches all its desc endant exceptions in its hierarchy. Finally 
the thrown exception  (at line 46)  is caught by .NET Framework and its stack 
trace is dumped on the console. 
The result of running the above example is shown below: 

 
Let’s look more carefully at the stack trace. We now see an additional section 
marking the end of the nested exception: 
--- End of inner exception stack trace --- 
This gives useful information about how the exception was thrown. 
If you look more closely on the first line, you will notice it contains information 
in the following format: 
This shows that an exception of type Exception1 is wrapped around an 
exception of type Exception2. After each exception type, we can see the 
message of the re spective exception (as contained in the Message property). 
Using the information in the stack -trace (the file name, the method and the 
line number), we can find out how the exceptions occurred and where.

## Visualizing Exceptions

In console applications  errors are usually printed in the output although 
this might not be the most user-friendly way to notify the user for problems. 
Unhandled Exception: Exception1: Msg1 ---> Exception2: Msg2 

In Web applications, errors are frequently shown in the beginning or at the 
bottom of the page or near the UI field related to the error. 
In GUI applications we should show the errors in a dialog window containing 
user-friendly description of the error . An example of user -friendly error 
message dialog box is given below: 
 
As you can see, there is no single 'right' way to handle a nd visualize 
exceptions as it depends on the type of the application and its intended 
audience. Still there are some recommendations regarding how to handle 
exceptions and what is the best way to show them to the users. We will 
discuss these recommendations in the "Best Practices" section.

## Which Exceptions to Handle and Which Not?

There is one universal rule regarding exception handling: 
 
A method should only handle exceptions which it expects and 
which it knows how to process. All the other exceptions must 
be left to the calling method. 
If we follow this rule and every method leaves the exceptions it is not 
competent to process to the cal ling method, eventually we would reach the 
Main() method (or the starting method of the respective thread of execution) 
and if this method does not catch the exception, the CLR will display the error 


on the console (or visualize it in some other way) and w ill terminate the 
program. 
A method is competent to handle an exception if it expects this exception, it 
has the information why the exception has been thrown and what to do in this 
situation. If we have a method that must read a text file and return its 
contents as a string, that method might catch FileNotFoundException and 
return an empty string in this case. Still, this same method will hardly be able 
to correctly handle OutOfMemoryException. What should the method do in 
case of insufficient memory? Retu rn an empty string? Throw some other 
exception? Do something completely different? So apparently the method is 
not competent to handle such exception and thus the best way is to pass the 
exception up to the calling method so it could (hopefully) be handled  at some 
other level by a method competent to do it. Using this simple philosophy 
allows exception handling to be done in a structured and systematic way.

## Throwing Exceptions from the Main() Method – Example

Throwing Exceptions from the Main() Method – 
Example 
Throwing exceptions from the Main() method is generally not a good 
practice. Instead, it is better all exceptions to be caught in Main(). Still it is of 
course possible to throw exceptions from Main() just as from any other 
method: 
```csharp
static void Main() 
{ 
 throw new Exception("Ooops!"); 
} 
```
Every exception which is not handled in Main() is eventually caught by the 
CLR and visualized by printing the stack trace on the console output or in 
some other way. While for small applications it is not such a problem, big and 
complex applications generally should not crash in such ungraceful manner.

## Catching Exceptions at Different Levels – Example

The ability to pass (or bubble) exceptions through a given method up to the 
calling method allows structured exception handling to be done at multiple 
levels. This means that we can catch certain types of exceptions in given 
methods and pass all other exceptions to the previous levels in the call -stack. 
In the example below, the exceptions in the ReadFile() method are handled 
at two levels (in the try-catch block of the ReadFile() method itself and in 
the try-catch block of the Main() method): 
```csharp
static void Main() 
{ 
```
 try 

```csharp
 { 
  string fileName = "WrongFileName.txt"; 
  ReadFile(fileName); 
 } 
```
 catch (Exception e) 
```csharp
 { 
  throw new ApplicationException("Bad thing happened", e); 
 } 
} 
 
static void ReadFile(string fileName) 
{ 
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
  Console.WriteLine("The file {0} does not exist!", 
   filename); 
 } 
} 
```
In this example the ReadFile() method catches and handles only 
FileNotFoundException while passing all other exceptions up to the Main() 
method. In the Main() method we handle only exceptions of type 
IOException and will let the CLR to handle all other exceptions (for instance, 
if OutOfMemoryException is thrown during program ’s execution, it will be 
handled by the CLR). 
If the Main() method passes a wrong filen ame, FileNotFoundException will 
be thrown while initializing the TextReader in ReadFile(). This exception will 
be handled by the ReadFile() method itself. If on the other hand the file 
exists but there is some problem reading it (insufficient permissions, damaged 
file contents etc.), the respective exception that will be thrown will be handled 
in the Main() method. 
Handling exceptions at different levels allows the error conditions to be 
handled at the most suitable place for the particular error. This allo ws the 
program code to be clear and structured and the flexibility achieved is 
enormous.