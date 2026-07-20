# Abstraction

The next core principle of object -oriented programming we  are about to 
examine is "abstraction". Abstraction means working with something we 
know how to use without  knowing how it works internally . A good 
example is a television set. We don’t need to know the inner workings of a TV, 
in order to use it. All we need is a remote control with a small set of buttons 
(the interface of the remote) and we will be able to watch TV. 
The same goes for objects in OOP. If we have an object Laptop and it needs a 
processor, we use the object Processor. We do not know (or rather it is of no 
concern to us) how it calculates. In order to use it, it ’s sufficient to call the 
method Calculate() with appropriate parameters. 
Abstraction is something we do every day. This is an action, which obscures 
all details of a certain object that do not concern us and only uses the details, 
which are relevant to the problem we  are solving. For example, in hardware 
configurations, there is an abstraction called "data storage devi ce" which 
can be a hard disk, USB memory stick  or CD-ROM drive. Each of these 
works in a different way internally but, from the point of view of the operating 
system and its applications, it is used in the same way – it stores files and 
folders. In Windows  we have Windows Explorer and it can work with all 
devices in the same way, regardless of whether a device is a hard drive or a 
USB stick. It works with the abstraction "storage device" and is not involved 
with how data is read or written. The drivers of t he particular device take care 
of that. They are implementations of the interface "data storage device". 
Zoo
-elephants
-snakes
+Weight() : int
Lion
-weight : int
*
Human Brain
1 1

Abstraction is one of the most important concepts  in programming and 
OOP. It allows us to write code, which  works with abstract data 
structures (like dictionaries, lists, arrays and others). We can work with an 
abstract data type by using its interface without concerning ourselves with its 
implementation. For instance, we can save to a file all elements from a list 
without bothering if it is implemented with an array, a linked list, etc. The 
code remains unchanged, when we work with other data types. We can even 
write new data types (we will discuss this later) and make them work with our 
program without changing it. 
Abstraction allows us to do something very important – define an interface 
for our applications , i.e. to define  all tasks the program is capable to  
execute and their respective input and output data. That way we can make a 
couple of small programs, each ha ndling a smaller task. When we combine 
this with the ability to work with abstract data, we achieve great flexibility in 
integrating these small programs and much more opportunities for code 
reuse. These small subprograms are referred to as components. This 
approach for writing programs is widely adopted since it allows us to reuse 
not only objects, but entire subprograms as well.

## Abstraction – Abstract Data Example

Here is an example, where we define a specific data type "African lion", but 
use it later  on in an abstract manner through the "Felidae" abstraction. This 
abstraction does not concern itself with the details of all types of lions. 
AbstractionExample.cs 
```csharp
public class AbstractionExample 
{ 
 static void Main() 
 { 
  Lion lion = new(true, 150);
  Felidae bigCat1 = lion; 
 
  AfricanLion africanLion = new(true, 80);
  Felidae bigCat2 = africanLion; 
 } 
}
```

## Interfaces

In the C# language the interface is a definition of a role (a group of abstract 
actions). It defines what sort of behavior a certain object must exhibit, 
without specifying how this behavior should be implemented.  Interfaces are 
also known as contracts or specifications of behavior. 

An object can have multiple roles (or implement multiple interfaces  / 
contracts) and its users can utilize it from different points of view. 
For example, an object of type Person can have the roles of Soldier (with 
behavior "shoot your enemy"), Husband (with behavior "love your wife" ) and 
Taxpayer (with behavior "pay your taxes"). However, every person 
implements its behavior in a different way; John pays his taxes on time, 
George pays them overdue and Peter doesn’t pay them at all. 
Some may ask why the base class of all objects (the class Object) is not an 
interface. The reason is because in such case, every class would have to 
implement a small, but very important group of methods and this would take 
an unnecessary amount of time. It turns out that not all classes need a 
specific imp lementation of Object.GetHashCode(), Object.Equals(…) and 
Object.ToString(), i.e. the default implementation suffices in most cases. 
It’s not necessary to override any of the methods in the Object class, but if 
the situation calls for it we can. Overriding methods is explained in the 
virtual methods section.

### Interfaces – Key Concepts

An interface can only declare methods and constants. 
A method signature  is the combination of a method ’s name and a 
description of its parameters (type and order). In a class  / interface all 
methods have to have different signatures and should not be identical with 
signatures of inherited methods. 
A method declaration is the combination of a method’s return type and its 
signature. The return type only specifies what the method returns. 
 
A method is identified by its signature. The return type is not 
a part of it. If two methods' only difference is the return type 
(as in the case when a class inherits another), then it cannot 
be unequivocally decided which method must be executed. 
A class / method implementation is the source code of a class  / method. 
Usually it is between curly brackets: " {" and "}". Regarding methods, this is 
also referred to as the method body.

### Interfaces – Example

An interface in .NET is defined with the keyword interface. An interface can 
contain only method declarations and constants. Here is an example of an 
interface: 
Reproducible.cs 
```csharp
public interface Reproducible<T> where T : Felidae 
{ 


 T[] Reproduce(T mate); 
} 
```
We explained the g enerics in the  "Defining Classes " chapter (section 
"Generics"). The interface we wrote has a method of type T (T must inherit 
Felidae) which returns an array of T. 
And this is how the class Lion, which implements the interface  
Reproducible looks like: 
Lion.cs 
```csharp
public class Lion : Felidae, Reproducible<Lion> 
{ 
 // … 
 
```
 Lion[] Reproducible<Lion>.Reproduce(Lion mate) 
```csharp
 { 
  return new Lion[]{new Lion(true, 12), new Lion(false, 10)}; 
 } 
} 
The name of the interface is coded in the declaration of the class (on the first 
```
row) and specifies the generic class. 
We can indicate which method from a specific interface we implement  by 
typing its name explicitly: 
Lion[] Reproducible<Lion>.Reproduce(Lion mate) 
In an interface, methods are only declared; the implementation is coded in 
the class implementing the interface, i.e. – Lion. 
The class that implements a certain interface must implement all methods 
in it. The only exception is when the class is abstract. Then it can implement 
none, some or all of the methods. All remaining methods have to be 
implemented in some of the inheriting classes.

### Abstraction and Interfaces

The best way to achieve abstraction is by working though interfaces. A 
component works with interfaces which another implements. That way, a 
change in the second component will not affect the first one as long as the 
new component implements the old interface. The interface is also called a 
contract. Every comp onent upholds a certain contract (the signature of 
certain methods). That way, two components upholding a contract can 
communicate with each other without knowing how their counterpart works. 

Some important interfaces from the Common Type System (CTS) are the list 
and collection interfaces: System.Collections.Generic.IList<T> and 
System.Collections.Generic.ICollection<T>. All of the standard .NET 
collection classes implement these interfaces and the various components 
pass different implementations (arrays,  linked lists, hash tables, etc.) to one 
another using a common interface. 
Collections are an excellent example of an object -oriented library with 
classes and interfaces that actively use all core principles of OOP: abstraction, 
inheritance, encapsulation and polymorphism.

### When Should We Use Abstraction and Interfaces?

The answer to this question is: always when we want to achieve 
abstraction of data or actions , whose implementation can change later on. 
Code, which communicates  with another piece of code th rough interfaces, is 
much more resilient to changes than code written using specific classes. 
Working through interfaces is common and a highly recommended 
practice – one of the basic rules for writing high-quality code.

### When Should We Write Interfaces?

It is always a good idea to use interfaces when functionality is exposed to 
another component . In the interface we include only the functionality (in 
the form of a declaration) that others need to see. 
Internally, a program  / component can use interfaces for  defining roles . 
That way, an object can be used by different classes through different roles.