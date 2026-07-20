# Cohesion and Coupling

The terms cohesion and coupling  are inseparable from OOP. They 
complement and explain further some of the principles we  have described so 
far. Let’s get familiar with them.

## Cohesion

The concept of cohesion shows to what degree a program’s or a component’s 
various tasks and responsibilities are related to one another, i.e. how much 
a program is focused on solving a single problem. Cohesion is  divided into 
strong cohesion and weak cohesion.

### Strong Cohesion

Strong cohesion indicates that the responsibilities and tasks of a piece of code 
(a method, class, component or a program) are related to one another and 
intended to solve a common problem . This is something we must always 
aim for. Strong cohesion is a typical characteristic of high-quality software.

### Strong Cohesion in a Class

Strong cohesion in a class indicates that the class defines only one entity . 
We mentioned earlier that an entity can have many roles (Peter is a soldier, 
husband and a taxpayer). Eac h of these roles is defined in the same class. 
Strong cohesion indicates that the class solves only one task, one problem, 
and not many at the same time. 
A class, which does many things at the same time , is difficult to 
understand and maintain. Consider a class, which  implements a hash table, 
provides functions for printing, sending an e -mail and working with 
trigonometric functions all at once. How do we name such a class? If we find 
it difficult to answer this question, this means that we have failed to  achieve 
strong cohesion and have to separate the class into several smaller classes, 
each solving a single task.

### Strong Cohesion in a Class – Example

As an example of strong cohesion we can point out the System.Math class. It 
performs a single task: it provides mathematical calculations and constants: 
- Sin(), Cos(), Asin() 
- Sqrt(), Pow(), Exp() 
- Math.PI, Math.E

### Strong Cohesion in a Method

A method is well written when it performs only one task and performs 
it well. A method, which does a lot of work related to different things,  has 
bad cohesion . It  has to be broken down into simpler methods , each 
solving only one task. Once again, the question is posed what name should 
we give to a method, which finds prime numbers, draws 3D graphics on the 
screen, communicates with the network and prints records extracted from a 
data base ? Such a method has bad cohesion  and has to be logically 
separated into several methods.

### Weak Cohesion

Weak cohesion is observed along with methods, which  perform several 
unrelated tasks. Such methods take several different groups of parameters, 
in order to perform different tasks.  Sometimes, this requires logically 
unrelated data to be unified for the sake of such methods. Weak cohesion is 
harmful and must be avoided!

### Weak Cohesion – Example

Here is a sample class with weak cohesion: 
```csharp
public class Magic 
{ 
 public void PrintDocument(Document d) { … } 
 public void SendEmail(string recipient, 
  string subject, string text) { … } 
 public void CalculateDistanceBetweenPoints( 
  int x1, int y1, int x2, int y2) { … } 
}
```

### Best Practices with Cohesion

Strong cohesion  is quite logically the "good" way of writing code. The 
concept is associated with simpler and clearer source code – code that is 
easier to maintain and reuse (because of the fewer tasks it has to perform). 
Contrarily, with weak cohesion each change is a ticking time bomb, because 
it could affect other functionality. Sometimes a logical task is spread out to 
several different mod ules and thus changing it is more labor intensive. Code 
reuse is also difficult, because a component does several unrelated tasks and 
to reuse it the exact same conditions must be met which is hard to achieve.

## Coupling

Coupling mostly describes the extent to which components / classes depend 
on one another. It is broken down into loose coupling and tight coupling. 
Loose coupling usually correlates with strong cohesion and vice versa.

### Loose Coupling

Loose coupling is defined by a piece of code ’s (program / class / component) 
communication with other code through clearly defined interfaces  
(contracts). A change in the implementation of a loosely coupled component 
doesn’t reflect on the others it communicates with. When you write source 
code, you must not rely on inner characteristics  of components (specific 
behavior that is not described by interfaces). 
The contract has to be maximally simplified and define only the required 
behavior for this component’s work by hiding all unnecessary details. 

Loose coupling is  a code characteristic you should aim for. It  is one of the 
characteristics of high-quality programming code.

### Loose Coupling – Example

Here is an example of loose coupling between classes and methods: 
```csharp
class Report 
{ 
 public bool LoadFromFile(string fileName) { … } 
 public bool SaveToFile(string fileName) { … } 
} 
 
class Printer 
{ 
 public static int Print(Report report) { … } 
} 
 
class Example 
{ 
 static void Main() 
 { 
  Report myReport = new();
  myReport.LoadFromFile("DailyReport.xml"); 
  Printer.Print(myReport); 
 } 
} 
```
In this example, none of the methods depend on the others . The 
methods rely only on some of the parameters, which  are passed to them. 
Should we need one of the methods in a next project, we could easily take it 
out and reuse it.

### Tight Coupling

We achieve tight coupling when there are many input parameters and output 
parameters; when we use undocumented (in the contract) characteristics of 
another component (for example, a dependency on static fields in another 
```csharp
class); and when we use many of the so called control parameters that 
```
indicate behavior with actual data. Tight coupling  between two or more 
methods, classes or components means that they cannot work 
independently of one another  and that a change in one of them will also 
affect the rest. This leads to difficult to read code and big problems with its 
maintenance.

### Tight Coupling – Example

Here is an example of tight coupling between classes and methods: 

```csharp
class MathParams 
{ 
 public static double operand; 
 public static double result; 
} 
 
class MathUtil 
{ 
 public static void Sqrt() 
 { 
  MathParams.result = CalcSqrt(MathParams.operand); 
 } 
} 
 
class SpaceShuttle 
{ 
 static void Main() 
 { 
  MathParams.operand = 64; 
  MathUtil.Sqrt(); 
  Console.WriteLine(MathParams.result); 
 } 
} 
```
Such code is difficult to understand and maintain , and the likelihood of 
mistakes when using it is great. Think about what happens if another method, 
which calls Sqrt(), passes its parameters through the same static variables 
operand and result. 
If we have  to use the same functionality for deriving square root in a 
subsequent project, we will not be able to simply copy the method Sqrt(), 
but rather we will  have to copy the classes MathParams and MathUtil 
together with all of their methods. This makes the code difficult to reuse. 
In fact, the above code is an example of bad code  according to all rules of 
Procedural and Object -Oriented Programming and if you think twice, you will 
certainly identify at least several more disregarded recommendations from 
those we have given you so far.

### Best Practices with Coupling

The most common and advisable way of invoking a well written module ’s 
functionality is through interfaces. That way, the functionality can be 
substituted without clients of the code requiring changes. T he jargon 
expression for this is "programming against interfaces". 
Most commonly, an interface describes a "contract" observed by this module. 
It is good practice not to rely on anything else other than what ’s described by 

this contract. The use of inner classes, which are not part of the public 
interface of a module,  is not recommended because their implementation can 
be substituted without substituting the contract (we already discussed this in 
the section "Abstraction"). 
It is good practice that the methods are made flexible  and ready to work 
with all components, which observe their interfaces,  and not only with 
definitive ones (i.e. to have implicit requirements). The latter would mean 
that these methods expect something specific from the components they can 
work with. It is also good practice that all 
dependencies are clearly described and 
visible. Otherwise, the maintenance of such 
code becomes difficult (it is riddled with 
stumbling-blocks). 
A good example of strong cohesion and loose 
coupling we can find  in the classes from the 
standard namespaces  System.Collections 
and System.Collections.Generic. The se 
.NET classes for working with collections have 
strong cohesion . Each solves a single 
problem and allows easy reuse. These classes 
have another characteristic of high -quality 
programming code: loose coupling . The 
classes, implementing the collections, are not 
related to one another. Each works through a 
strictly defined interface  and does not give away details of its 
implementation. All methods and fields not from the interface are hidden, in 
order to reduce the possibility of coupling with them . Methods in the collection 
classes do not depend on static variables and do not rely on any input data 
except for their inner state and passed parameters. This is good practice 
every programmer sooner or later attains with gained experience.

## Spaghetti Code

Spaghetti code is unstructured code with unclear logic ; it is difficult to 
read, understand and maintain ; it violates and mixes up consistency; it 
has weak cohesion and tight coupling . Such code is associated with 
spaghetti, because it is just as tangle d and twisted. When you pull out a 
strand of spaghetti (i.e. a class or method), the whole dish of spaghetti can 
turn out tangled in it (i.e. changes in one method or class lead to dozens of 
other changes because of the strong dependence between them). It is almost 
impossible to reuse spaghetti code, since there is no way to separate that part 
of the code, which is practically applicable. 
Spaghetti code is achieved when you have written code, supplement it and 
have to readapt it again and again every time t he requirements change. Time 
passes by until a moment comes when it has to be rewritten from scratch.

## Cohesion and Coupling in Engineering Disciplines

If you think that the principles of strong cohesion and loose coupling apply 
only to programming, you are deeply mistaken. These are fundamental 
engineering principles  you will come across in construction, machine 
building, electronics and thousands of other fields. 
Let’s take, for instance, a hard disk drive (HDD): 
It solves only one task doesn’t it? The hard disk 
solves the task of storing data. It does not  cool 
down the computer, does not  make sounds, has 
no computing power and is not  used as a 
keyboard. It is connected to the computer with 
two cables only, i.e. it has a simple interface for 
access and is not bound to other peripherals. The 
hard disk works separately  and other devices 
aren’t concerned about how it works exactly. The 
CPU commands it to "read" and it reads, then it 
commands it to "write" and it writes. How exactly 
it does this remains hidden inside it. Different models can work in different 
ways, but that is their own concern. You can see that the CPU has strong 
cohesion, loose coupling, good abstraction and good encapsulation. This is 
how you should implement your classes – they must do only one thing, do 
it well , bind them minimally to other classes  (or not link them at all 
whenever that’s possible), have a clear interfa ce and good abstraction and to 
hide the details of their internal workings. 
Here is another example: imagine what would happen, if the processor, the 
hard disk, the CD -ROM drive and the keyboard were soldered to the 
motherboard of the computer. It would me an that if any part of the keyboard 
were broken, you would have to throw away the whole computer. You can see 
how hardware cannot work well with tight coupling and weak cohesion. The 
same applies to software.