# Polymorphism

The next fundamental principle of Object -Oriented Programmi ng is 
"Polymorphism". Polymorphism allows treating objects of a derived 
```csharp
class as objects of its base class . For example, big cats (base class) catch 
```
their prey (a method) in different ways. A Lion (derived class) sneaks on it, 
while a Cheetah (another derived class) simply outruns it. 
Polymorphism allows us to treat a cat of random size just like a big cat and 
command it "catch your prey", regardless of its exact size. 
Polymorphism can bear strong resemblance to abstraction, but it is mostly 
related to overriding methods in derived classes , in order to change 
their original behavior  inherited from the base class. Abstraction is 
associated with creating an interface of a component or functionality (defining 
a role). We are going to explain method overriding shortly.

## Abstract Classes

What happens if we want to specify that the class Felidae is incomplete and 
only its successors can have instances? This is accomplished by putting the 
keyword abstract before the name of the class and indicates that the class 
is not ready to be instantiated . We refer to such classes as abstract 
classes. And how do we indicate which exact part of the class is incomplete? 
Once again, this is accomplished by putting the keyword abstract before the 
name of the method to be implemented.  This method is called an abstract 
method and cannot have an implementation, but a declaration only. 
Each class with at least one abstract method  must be abstract. Makes 
sense, right? However, the opposite is not true. It is possible to define a class 
as an abstract one, even when there are no abstract methods in it. 
Abstract classes are something in the middle between classes and 
interfaces. They can define ordinary methods  and abstract methods . 
Ordinary methods have an implementation, whereas abstract met hods are 
empty (without an implementation) and remain to be implemented later by 
the derived classes.

### Abstract Class – Examples

Let’s take a look at an example of an abstract class: 
Felidae.cs 
/// <summary>Latin for "cats"</summary> 
```csharp
public abstract class Felidae 
{ 

 // … 
 
 protected void Hide() 
 { 
  // … 
 } 
 
 protected void Run() 
 { 
  // … 
 } 
 
 public abstract bool CatchPrey(object prey); 
} 
```
Notice how in the example above the ordinary methods Hide() and Run() 
have a body, while the abstract method CatchPrey() does not. Notice that 
the methods are declared as protected. 
Here is how the implementation of the above abstraction looks like: 
Lion.cs 
```csharp
public class Lion : Felidae, Reproducible<Lion> 
{ 
 protected void Ambush() 
 { 
  // … 
 } 
 
 public override bool CatchPrey(object prey) 
 { 
  base.Hide(); 
  this.Ambush(); 
  base.Run(); 
  // … 
  return false; 
 } 
} 
Here is one more example of abstract behavior , implemented with an 
abstract class and a polymorphic call to an abstract method.  In this example 
we define abstract method and we override it later in a descendant class. 
```
Let’s see the code and discuss it later. 
Firstly, we define the abstract class Animal: 

Animal.cs 
```csharp
public abstract class Animal 
{ 
 public void PrintInformation() 
 { 
  Console.WriteLine("I am a {0}.", this.GetType().Name); 
  Console.WriteLine(GetTypicalSound()); 
 } 
 
 protected abstract String GetTypicalSound(); 
} 
We also define the class Cat, which inherits the abstract class  Animal and 
defines an implementation of the abstract method GetTypicalSound(): 
```
Cat.cs 
```csharp
public class Cat : Animal 
{ 
 protected override String GetTypicalSound() 
 { 
  return "Meoooow!"; 
 } 
} 
```
If we execute the following program: 
```csharp
public class AbstractClassExample 
{ 
 static void Main() 
 { 
  Animal cat = new Cat(); 
  cat.PrintInformation(); 
 } 
} 
we are going to get the following result: 
```
I am a Cat. 
Meoooow! 
In the example, the PrintInformation() method from the abstract class 
does its work by relying on the result from a call to the abstract method  
GetTypicalSound() which is expected to be implemented in different ways by 

the kinds of animals (the various successors of the class  Animal). Different 
animals make distinct sounds, but the functionality for printing information 
about animals is common to all animals, and that ’s why it is exported to the 
base class.

### Purely Abstract Classes

Abstract classes, as well as interfaces, cannot be instantiated . If we try to 
create an instance of an abstract class, we are going to g et an error during 
compilation. 
 
Sometimes a class can be declared abstract, even if it has no 
abstract methods, in order to simply prohibit using it directly 
without creating an instance of a successor. 
A pure abstract class is an abstract class, which has no implemented 
methods and no member variables. It is very similar to an interface . The 
fundamental difference is that a class can implement many interfaces and 
inherit only one class (even if that class is abstract). 
Initially, interfaces were not  necessary in the presence of "multiple 
inheritance". They had to be conceived as a means to supersede it in 
specifying the numerous roles of an object.

## Virtual Methods

A method, which can  be overridden in a derived class,  is called a virtual 
method. Methods in .NET by default aren’t virtual. If we want to make a 
method virtual, we mark it with the keyword  virtual. Then the derived 
class can declare and define a method with the same signature. 
Virtual methods are important for method overriding, which lies at the heart 
of polymorphism.

### Virtual Methods – Example

We have a class inheriting another and the two classes share a common 
method. Both versions of the method write on the consol e. Here is how the  
Lion class looks like: 
Lion.cs 
```csharp
public class Lion : Felidae, Reproducible<Lion> 
{ 
 public override void CatchPrey(object prey) 
 { 
  Console.WriteLine("Lion.CatchPrey"); 
 } 
} 


Here is how the AfricanLion class looks like: 
```
AfricanLion.cs 
```csharp
public class AfricanLion : Lion 
{ 
 public override void CatchPrey(object prey) 
 { 
  Console.WriteLine("AfricanLion.CatchPrey"); 
 } 
} 
```
We make three attempts to create instances and call the method CatchPrey. 
VirtualMethodsExample.cs 
```csharp
public class VirtualMethodsExample 
{ 
 static void Main() 
 { 
  Lion lion = new(true, 80);
  lion.CatchPrey(null); 
  // Will print "Lion.CatchPrey" 
 
  AfricanLion lion = new(true, 120);
  lion.CatchPrey(null); 
  // Will print "AfricanLion.CatchPrey" 
 
  Lion lion = new AfricanLion(false, 60); 
  lion.CatchPrey(null); 
  // Will print "AfricanLion.CatchPrey", because 
  // the variable lion has a value of type AfricanLion 
 } 
} 
```
In the last attempt, you can clearly see how, in fact, the overwritten 
method is called and not the base method. This happens, because it is 
validated what the actual class behind the variable is and whether it 
implements (overwrites) that method.  Rewriting of methods  is also called 
overriding of virtual methods. 
Virtual methods  as wel l as abstract methods can be overridden. Abstract 
methods are actually virtual methods without a specific implementation. All 
methods defined in an interface are abstract and therefore virtual, although 
this is not explicitly defined.

### Virtual Methods and Methods Hiding

In the example above, the implementation of the base class is hidden and 
omitted. Here is how we can also use it as part of the new implementation (in 
case we want to complement the old implementation rather than override it). 
Here is how the AfricanLion class looks like: 
AfricanLion.cs 
```csharp
public class AfricanLion : Lion 
{ 
 public override void CatchPrey(object prey) 
 { 
  Console.WriteLine("AfricanLion.CatchPrey"); 
  Console.WriteLine("calling base.CatchPrey"); 
  Console.Write("\t"); 
  base.CatchPrey(prey); 
  Console.WriteLine("...end of call."); 
 } 
} 
```
In this example, three lines will be written on the console when 
AfricanLion.CatchPrey(…) is called: 
AfricanLion.CatchPrey 
calling base.CatchPrey 
  Lion.CatchPrey 
...end of call.

### The Difference between Virtual and Non-Virtual Methods

Some may ask what the difference between the virtual and non -virtual 
methods is. 
Virtual methods are used when we expect from derived classes to change / 
complement / alter some of the inherited functionality. For example, the 
method Object.ToString() allows derived classes to change / replace  its 
implementation in any way they want. Then, even if we work with an object 
not directly, but rather by upcasting it to Object, we use the overwritten 
implementation of the virtual methods. 
Virtual methods are a key characteristi c of objects when we talk about 
abstraction and working with abstract types. 
Sealing of methods is done when we rely on a piece of functionality and we 
don’t want it to be altered. We already know that methods are sealed by 
default. But if we want a base class ’ virtual method to become sealed in a 
derived class, we use override sealed. 

The string class has no virtual methods . In fact, inheriting string is 
entirely forbidden for inheritance  through the keyword sealed in its 
declaration. Here are parts of the declarations of string and object classes 
(the ellipses in square brackets indicate omitted, irrelevant code): 
```csharp
namespace System 
{ 
 […] public class Object 
 { 
  […] public Object(); 
  […] public virtual bool Equals(object obj); 
  […] public static bool Equals(object objA, object objB); 
  […] public virtual int GetHashCode(); 
  […] public Type GetType(); 
  […] protected object MemberwiseClone(); 
  […] public virtual string ToString(); 
 } 
 
 […] public sealed class String : […] 
 { 
  […] public String(char* value); 
  […] public int IndexOf(string value); 
  […] public string Normalize(); 
  […] public string[] Split(params char[] separator); 
  […] public string Substring(int startIndex); 
  […] public string ToLower(CultureInfo culture); 
```
  […] 
 } 
}

## When Should We Use Polymorphism?

The answer to this question is simple: whenever we want to enable 
changing a method’s implementation in a derived class . It’s a good rule 
to work with the most basic class possible or directly with an interface. That 
way, changes in used classes reflect to a much lesser extent on classes 
written by us. The less a program knows about its surrounding classes, the 
fewer changes (if any) it would have to undergo.