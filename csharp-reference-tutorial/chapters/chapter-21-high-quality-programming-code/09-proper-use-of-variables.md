# Proper Use of Variables

In this section we review a few good practices for using local variables.

## Returning a Result

Whenever a result is returned, it should first be saved in a variable , before 
being returned. The following example does not hint at what exactly is 
returned: 
return days * hoursPerDay * ratePerHour; 
It would be better like that: 
int salary = days * hoursPerDay * ratePerHour; 
return salary; 
There are a few reasons for saving the result before returning it. For one, the 
additional variable contributes to self-documenting the code and makes it 
clear exactly what is returned. Another reason is tracing the returned value 
when debugging – we can s top the program from executing as soon as the 

value is computed and then inspect it. A third reason is that it helps us avoid 
long expressions, which can become quite convoluted.

## Principles for Initialization

In .NET, all member-variables (fields) belonging to a class are initialized 
automatically at the time of being declared (unlike C/C++). This is managed 
by the runtime and provides for a safer environment, less prone to errors 
caused from incorrectly initialized memory. All reference type variab les are 
initialized to null and all primitive types to 0 (false for bool). 
The compiler forces the explicit initialization of all local variables ; 
otherwise a compile-time error is given. Here is an example that would cause 
such an error, because an attempt is made to use an uninitialized variable: 
```csharp
static void Main() 
{ 
 int value; 
 Console.WriteLine(value); 
} 
```
Here is how the compilation attempt looks like in Visual Studio: 
 
Here is how the compilation attempt looks like in the console C# compiler: 


 
Let’s look at the following more complex example: 
int value; 
if (condition1) 
```csharp
{ 
 if (condition2) 
 { 
  value = 1; 
 } 
} 
else 
{ 
 value = 2; 
} 
Console.WriteLine(value); 
```
Fortunately, the compiler is smart enough to analyze the control flow  and 
to catch such prob lems – the same error is thrown, because not all scenarios 
assign correctly the variable. 
Note that adding an else to the nested if in the above code will make it 
compile without errors. If the variable is not initialized at its decl aration, but is 
assigned to a value in all the possible paths of the control flow, the compiler 
will still be happy. 
A good practice, however, is to initialize all variables explicitly at the time of 
their declaration: 
int value = 0; 
Student intern = null;

### Partially Initialized Objects

Some objects, in order to be properly initialized, should have at least a few of 
their fields set. For example, an object of type Person should have valid 
values set for at least the name and the family name  fields. This is 
something the compiler cannot prevent us from forgetting. 


One way to solve this problem is to remove the default constructor (the one 
not taking any parameters) and to add one or more constructors, which take 
the sufficient data as their p arameters, to enable the proper initialization of 
the object. This is the sole purpose of parameterized constructors.

## Declaring a Variable within a Block or a Method

According to the coding convention for .NET, a local variable should be 
declared at the beginning of its enclosing block or method: 
```csharp
static int Archive() 
{ 
 int result = 0; // beginning of method body 
 // … Code … 
} 
```
Another example: 
if (condition) 
```csharp
{ 
 int result = 0; // beginning of an "if" block 
 // … Code … 
} 
An exception is made for variables declared within the initialization part of a 
for-loop: 
for (int i = 0; i < data.Length; i++) { … } 
```
The above recommendation is pretty disputable. Most good programmers 
prefer to declare a local variable as close to its i ntended place of use  as 
possible. This helps reduce a variable ’s lifetime (refer to the next paragraph ), 
and, at the same time, the probability for mistakes.

## Scope, Lifetime and Span of Variables

The term variable scope actually denotes how “famous” a variable is . In 
.NET, three layers of variable scope exist: static variables, member-variables 
of a class (fields), and local variables inside a method.

### Minimizing the Variable Scope

The wider the scope of a variable, the higher the probability that more code 
will be tied to it, thereby increasing the level of coupling . Since strong 
coupling is not desirable, variable scope should be as narrow as possible. 
A good approach in using variables is to initial ly declare them with the 
minimal scope, and extend it only when necessary. This is a natural way of 

assigning a variable the scope it needs. If you don ’t know what scope to use, 
start with private and if needed, switch to protected or public. 
Static variab les should best be private and accessing them should be 
controlled via appropriate methods. 
Here is an example of bad semantic coupling  based on a static variable, a 
horribly bad practice: 
```csharp
public class Globals 
{ 
 public static int state = 0; 
} 
 
public class Genious 
{ 
 public static void PrintSomething() 
 { 
  if (Globals.state == 0) 
   Console.WriteLine("Hello."); 
  else 
   Console.WriteLine("Good bye."); 
 } 
} 
```
If the state variable was marked private, such coupling would be 
impossible, at least not possible directly.

### Minimizing the Variable Span

The span of a variable corresponds to the average amount of lines 
between its occurrences in the code . Considering minimal variable span, 
variables should be declared and initialized as close as possible to t heir 
first occurrence  in the code, and not necessarily in the beginning of a 
method or a code block. 
Keep the variable span as small as possible!  This improve s the code 
quality, readability, understandability and maintainability because less code 
needs to be inspected in order to read and understand the code.

### Minimizing the Variable Lifetime

The lifetime of a local variable inside a method lasts between the place of 
its declaration (the beginning of a block, most usually), until the end of 
the enclosing blo ck. Class fields (m ember-variables) exist as long as their 
```csharp
class is instantiated. Static variables last throughout the entire execution of 
```
the program. 
As you may guess, the lifetime should be kept minimal . This reduces the 
lines of code that you should co nsider at the same time when reading the 

code. This will maximized the portion of the code you can safely ignore when 
you read the code. This will reduce the total complexity in your brain, because 
it works better with smaller and simpler pieces of code, right?

### Minimizing the Variable Span and Lifetime – Example

Below we have  an example of bad use of local variables ( unnecessarily 
large span): 
1 
2 
3 
4 
5 
6 
7 
8 
9 
10 
11 
12 
13 
14 
15 
16 
17 
18 
19 
20 
21 
22 
23 
int count; 
int[] numbers = new int[100]; 
 
for (int i = 0; i < numbers.Length; i++) 
```csharp
{ 
 numbers[i] = i; 
} 
count = 0; 
 
for (int i = 0; i < numbers.Length / 2; i++) 
{ 
 numbers[i] = numbers[i] * numbers[i]; 
} 
 
for (int i = 0; i < numbers.Length; i++) 
{ 
 if (numbers[i] % 3 == 0) 
 { 
  count++; 
 } 
} 
 
Console.WriteLine(count); 
 
 
 
 
 
 
lifetime = 
```
23 lines 
span = 
23 / 4 = 
5.75 
In this example, the count variable’s purpose is to count the numbers, which 
are evenly divisible by 3. It is used only in the last for loop, but is declared 
and initialized long before it. 
What's wrong  with the above code? If you try to read it and find how the 
count is calculated, you will need to inspect all its 23 lines, right? The code 
might be written differently and the variable count might be declared and 
zeroed just before the last for-loop. Thus if we need to read the code and 
find how count is calculated, we will need to inspect only 10 lines, not 23. 
See below how the above code fragment can be refactored in order to reduce 
the lifetime and span of the count variable: 
1 int[] numbers = new int[100];  

2 
3 
4 
5 
6 
7 
8 
9 
10 
11 
12 
13 
14 
15 
16 
17 
18 
19 
20 
21 
for (int i = 0; i < numbers.Length; i++) 
```csharp
{ 
 numbers[i] = i; 
} 
 
for (int i = 0; i < numbers.Length / 2; i++) 
{ 
 numbers[i] = numbers[i] * numbers[i]; 
} 
 
int count = 0; 
for (int i = 0; i < numbers.Length; i++) 
{ 
 if (numbers[i] % 3 == 0) 
 { 
  count++; 
 } 
} 
 
Console.WriteLine(count); 
 
 
 
 
 
 
 
 
lifetime = 
```
10 lines 
 
span = 
10 / 3 = 
3.33 
It is important that the programmer tracks the usage of a particular variable, 
along with its scope, span and lifetime. The main objective is to reduce the 
scope, the lifetime  and the span  as much as possible. This leads to an 
important rule about correctly using variables: 
 
Declare local variables as late as possible, immediately 
before using them for the first time. Initialize them at the 
time of declaration. 
Variables with a wider scope  and a longer lifetime should have more 
descriptive names, such as totalStudentsCount instead of count. That is 
because they occur at more locations within a larger piece of code, and hence 
the context around them is not going to be entirely clear. 
Variables that span across just 4-5 lines can have short and simple names, 
for example count. They do not need long names because their purpose 
becomes clear from their limited context (a few lines), and ambiguities can 
rarely arise there.

## Use of Variables – More Rules

A very important rule is to use a variable for one purpose only. The excuse 
that memory is conserved the other way is not generally convincing. If a 
variable is used for multiple different purposes, what name can we give it? 
Consider a variable that is used to count the number of students, and 


occasionally the count of their grades. How would you call it: count, 
studentsCount, gradesCount or studentsOrGradesCount? 
 
Use one variable for a single purpose only. Otherwise, an  
appropriate name cannot be found. 
Unused local variables should not be present in the code. Their declarations 
alone are useless. Fortunately, most of the decent development environments 
do warn you about such anomalies. 
The use of local variables with h idden meaning should be avoided. For 
example, John has left the variable X for Tom to see, so that he could get to 
the conclusion to implement another method that would use that same 
variable. Didn’t get it? Good, let’s hope you don’t do it either.