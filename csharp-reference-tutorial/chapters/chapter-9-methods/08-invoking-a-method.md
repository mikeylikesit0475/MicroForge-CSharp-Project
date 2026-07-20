# Invoking a Method

Invoking or calling a method  is actually the process of execution of the 
method’s code, placed into its body. 
It is very easy to invoke a method. T he only thing that has to be done is to 
write the method’s name <method_name>, followed by the round brackets and 
semicolon ";" at the end: 
<method_name>(); 
Later will see an example for when the invoked method has a parameter list 
(in the case here the method has no parameters). 
To clarify how meth od invocation works, the next fragment shows how the 
method PrintLogo() will be called: 
PrintLogo(); 
Result of method’s execution is: 
Microsoft 
www.microsoft.com

## Who Takes Control over the Program when We Invoke a Method?

Who Takes Control over the Program when We 
Invoke a Method? 
When a method executes it takes control over the program. If in the caller 
method, however, we call another one, the caller will give the control to the 
called method. The called method will return back the control to the caller 

right after its execution finishes. The execution of the caller will continue from 
that line, where it was before calling the other method. 
For example, let’s call PrintLogo() from the Main() method: 
 
First the code of method Main(), that is marked with (1)  will be executed, 
then the control of the program will  be given to the method PrintLogo() – 
the dotted arrow (2). This will cause the execution of the code in method 
PrintLogo(), numbered with (3). When the method PrintLogo() work is 
done, the control over the program is returned back to the method Main() – 
dotted arrow (4). Execution of Main() will continue from the line after 
PrintLogo() call – marked with (5).

## Where a Method Can Be Invoked From?

A method can be invoked from the following places: 
- From the main program method – Main(): 
```csharp
static void Main() 
{ 
 PrintLogo(); 
} 
```
- From some other method: 
```csharp
static void PrintLogo() 
{ 
 Console.WriteLine("Microsoft"); 


 Console.WriteLine("www.microsoft.com"); 
} 
 
static void PrintCompanyInformation() 
{ 
 // Invoking the PrintLogo() method 
 PrintLogo(); 
 
 Console.WriteLine("Address: One, Microsoft Way"); 
} 
```
- A method can be invoked from its own body. Such a call is referred to as 
recursion. We will discuss it in details in the chapter "Recursion".

## Method Declaration and Method Invocation

In C# the order of the methods in the class is not important. We are allowed 
to invoke (call) a method before it is declared in code: 
```csharp
static void Main() 
{ 
 // … 
 PrintLogo(); 
 // … 
} 
 
static void PrintLogo() 
{ 
 Console.WriteLine("Microsoft"); 
 Console.WriteLine("www.microsoft.com"); 
} 
If we create a class that contains the code above, we will see that the code 
```
will compile and run successfully. It doesn ’t matter whether we declared the 
method before or after the main method. In some other languages (like 
Pascal), invocation of a method that is declared below the line of the 
invocation is not allowed. 
 
If a method is called in the same class, where it is declared 
and implemented, it can be called at a line before the line at 
which it is declared.