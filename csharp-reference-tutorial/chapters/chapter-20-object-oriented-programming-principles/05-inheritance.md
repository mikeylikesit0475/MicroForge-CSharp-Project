# Inheritance

- Inheritance 
We will explain how class hierarchies  improve code readability and 
enable the reuse of functionality. 
- Abstraction 
We will learn how to work through abstractions: to deal with  objects 
considering their important characteristics and ignore all other details. 
- Polymorphism 
We will explain how to work in the same manner with different objects, 
which define a specific implementation of some abstract behavior. 
Some OOP theorists also put the concept of exception handling  as 
additional fifth fundamental principle of OOP . We shall not get into a 
detailed dispute about whether or not exceptions are part of OOP and rather 
will note that exceptions are supported in all modern object -oriented 
languages and are the primary mechanism of handling errors and unusual 
situations in object -oriented programming. Exceptions always come 
together with OOP  and their importance is explained in details in the 
chapter "Exception Handling". 

Inheritance 
Inheritance is a fundamental principle of object -oriented programming. It 
allows a class to "inherit" (behavior or characteristics) of another, more 
general class. For example, a lion belongs to the biological family of cats 
(Felidae). All cats that have four paws, are predators and hunt their prey. This 
functionality can be cod ed once in the Felidae class and all its predators can 
reuse it – Tiger, Puma, Bobcat, etc. Inheritance is described as is-kind-of 
relationship, e.g. Tiger is kind of Animal.

## How Does Inheritance Work in .NET?

Inheritance in .NET is defined with a special construct in the class declaration. 
In .NET and other modern programming languages, a class can inherit from a 
single class only (single inheritance), unlike C++ which supports inheriting 
from multiple classes (multiple inheritance). This limitation is necessitated 
by the difficulty in deciding which method to use when there are duplicate 
methods across classes (in C++, this problem is solved in a very complicated 
manner). In .NET, classes can inherit multiple i nterfaces, which we will 
discuss later. 
The class from which we inherit is referred to as parent class or base class 
/ super class.

### Inheritance of Classes – Example

Let’s take a look at an example of class inheritance in .NET. This is how a 
base class looks like: 
Felidae.cs 
/// <summary>Felidae is latin for "cats"</summary> 
```csharp
public class Felidae 
{ 
 private bool male; 
 
 // This constructor calls another constructor 
 public Felidae() : this(true) 
 {} 
 
 // This is the constructor that is inherited 
 public Felidae(bool male) 
 { 
  this.male = male; 
 } 
 
 public bool Male 
 { 

  get { return male; } 
  set { this.male = value; } 
 } 
} 
```
This is how the inheriting class, Lion, looks like: 
Lion.cs 
```csharp
public class Lion : Felidae 
{ 
 private int weight; 
 
 // Keyword "base" will be explained in the next paragraph 
 public Lion(bool male, int weight) : base(male) 
 { 
  this.weight = weight; 
 } 
 
 public int Weight 
 { 
  get { return weight; } 
  set { this.weight = value; } 
 } 
}
```

## The "base" Keyword

In the above example, we used the keyword base in the constructor of the 
```csharp
class Lion. The keyword indicates that the base class  must be used and 
```
allows access to its methods, constructors and member variables. Using 
base(), we can call the constructor of the base class. Using base.Method(…) 
we can invoke a method of the base class, pass parameters to it and use its 
results. Using base.field we can get the value of a member variable from 
the base class or assign a different one to it. 
In .NET, methods inherited from the base class and declared as virtual can 
be overridden. This means changing their implementation ; the original 
source code from the base class is ignored and new code takes its place. More 
on overriding methods we will discuss in "Virtual Methods". 
We can invoke non-overridden methods from the base class without using the 
keyword base. Using the keyword is required only if we have an overridden 
method or variable with the same name in the inheriting class. 

 
The keyword base can be used explicitly for clarity. 
base.method(…) calls a method, which is necessarily from the 
base class. Such source code is easier to read, because we 
know where to look for the method in question. 
Bear in mind that using the keyword this is not the same. It  
can mean accessing a method from the current, as well as 
the base class. 
You can take a look at the example in the section about access modifiers and 
inheritance. There it is clearly explained which members of the base class 
(methods, constructors and member variables) are accessible.

## Constructors with Inheritance

When inheriting a class, our constructors must call the base class constructor, 
so that it can initialize its member variables. If we do not do this explicitly, 
the compiler will place a call to the parameterless base class constructor, 
":base()", at the beginning of all our inheriting class' constructors. Here is an 
example: 
```csharp
public class ExtendingClass : BaseClass 
{ 
 public ExtendingClass() { … } 
} 
```
This actually looks like this (spot the differences): 
```csharp
public class ExtendingClass : BaseClass 
{ 
 public ExtendingClass() : base() { … } 
} 
If the base class has no default constructor (one without parameters) or that 
```
constructor is hidden, our constructors need to explicitly call one of the other 
base class constructors. The omission of such a call will result in a compile -
time error. 
 
If a class has private constructors only, then it cannot be 
inherited. 
If a class has private constructors only, then this could 
indicate many other things. For example, no -one (other than 
that class itself)  can create instances of such a class. 
Actually, that’s how one of the most popular design patterns 
(Singleton) is implemented. 
The Singleton design pattern is described in details at the end of this chapter.

### Constructors and the Keyword "base" – Example

Take a look at the Lion class from our last example. It does not have a 
default constructor. Let’s examine a class inheriting from Lion: 
AfricanLion.cs 
```csharp
public class AfricanLion : Lion 
{ 
 // … 
 
 // If we comment out the ": base(male, weight)" line 
 // the class will not compile. Try it. 
 public AfricanLion(bool male, int weight) 
```
  : base(male, weight) 
 {} 
 
```csharp
 public override string ToString() 
 { 
  return string.Format( 
   "(AfricanLion, male: {0}, weight: {1})", 
   this.Male, this.Weight); 
 } 
 
 // … 
} 
If we comment out the line ":base(male, weight);", the class AfricanLion 
```
will not compile. Try it. 
 
Calling the constructor of a base class happens outside the 
body of the constructor. The idea is that the fields of the 
base class should be initialized before we start initializing 
fields of the inheriting class, because they might depend on a 
base class field.

## Access Modifiers of Class Members and Inheritance

Let’s review: in the "Defining Classes" chapter, we examined the basic access 
modifiers. Regarding members of a class (methods, properties and member 
variables) we examined the modifiers public, private and internal. 
Actually, there are two other modifiers: protected and protected internal. 
This is what they mean: 
- protected defines class members which are not visible to users of the 
```csharp
class (those who initialize and use it), but are visible to all inheriting 
```
classes (descendants). 


- protected internal defines class members which are both internal, 
i.e. visible within the entire assembly, and protected, i.e. not visible 
outside the assembly, but visible to classes who inherit it (even outside 
the assembly). 
When a base class is inherited: 
- All of its public, protected and protected internal members 
(methods, properties, etc.) are visible to the inheriting class. 
- All of its private methods, properties and member -variables are not 
visible to the inheriting class. 
- All of its internal members are visible to the inheriting class, only if the 
base class and the inheriting class are in the  same assembly  (the 
same Visual Studio project). 
Here is an example, which  demonstrates the levels of visibility with 
inheritance: 
Felidae.cs 
/// <summary>Latin for "cats"</summary> 
```csharp
public class Felidae 
{ 
 private bool male; 
 
 public Felidae() : this(true) {} 
 
 public Felidae(bool male) 
 { 
  this.male = male; 
 } 
 
 public bool Male 
 { 
  get { return male; } 
  set { this.male = value; } 
 } 
} 
And this is how the class Lion looks like: 
```
Lion.cs 
```csharp
public class Lion : Felidae 
{ 
 private int weight; 

 
 public Lion(bool male, int weight) 
```
  : base(male) 
```csharp
 { 
  // Compiler error – base.male is not visible in Lion 
  base.male = male; 
  this.weight = weight; 
 } 
 
 // … 
} 
If we try to compile this example, we will get an error message, because the 
private variable male in the class Felidae is not accessible to the class 
```
Lion:

## The System.Object Class

Object-oriented programming practically bec ame popular with C++. In this 
language, it often becomes necessary to code classes, which must work with 
objects of any type. C++ solves this problem in a way that is not considered 
strictly object-oriented (by using void pointers). 
The architects of .NET take a different approach. They create a class, which 
all other classes inherit (directly or indirectly). All objects can be perceived 
as instances of this class. It is convenient that this class contains important 
methods and their default implementation. This class is called Object (which 
is the same as object and System.Object). 
In .NET every class, which does not inherit a class explicitly,  inherits the 
system class System.Object by default. The compiler takes care of that. 


Every class, which inherits from another class indirectly,  inherits Object from 
it. This way every class inherits expl icitly or implicitly from Object and 
contains all of its fields and methods. 
Because of this property, every class instance can be cast to  Object. A 
typical example of the advantages of implicit inheritance is its use with data 
structures, which  we saw in the chapters on data structures. Untyped list 
structures (like System.Collections.ArrayList) can hold all kinds of 
objects, because they treat them as instances of the class Object. 
 
The generic types (generics)  have been provided specifically 
for working with collections and objects of different types 
(generics are further discussed in  the chapter  "Defining 
Classes"). They allow creating typified classes, e.g. a 
collection which works only with objects of type Lion.

### .NET, Standard Libraries and Object

In .NET, there are a lot of predefined classes (we already covered a lot of 
them in the chapters on collections, text files and strings). These classes are 
part of the .NET framework; they are available wherever .NET is supported. 
These classes are referred to as Common Type System (CTS). 
.NET is one of the first frameworks, which  provide such an extensive set of 
predefined classes. A lot of them work with Object so that they can be used 
in as many situations as possible. 
.NET also provides a lot of libraries, which can be referenced additionally, and 
it stands to reason that they are called class libraries or external libraries.

### The Base Type Object Upcasting and Downcasting – Example

Let’s take a closer look at the Object class using an example: 
ObjectExample.cs 
```csharp
public class ObjectExample 
{ 
 static void Main() 
 { 
  AfricanLion africanLion = new(true, 80);
  // Implicit casting 
  object obj = africanLion; 
 } 
} 
```
In this example, we cast an AfricanLion to Object. This operation is called 
upcasting and is permitted because AfricanLion is an indirect child of the 
Object class. 


 
Now it is the time to mention that the keywords string and 
object are simply compiler tricks and are substituted with 
System.String and System.Object during compilation. 
Let’s continue with the example: 
ObjectExample.cs 
// … 
 
AfricanLion africanLion = new AfricanLion(true, 80); 
// Implicit casting 
object obj = africanLion; 
 
try 
```csharp
{ 
 // Explicit casting 
 AfricanLion castedLion = (AfricanLion) obj; 
} 
```
catch (InvalidCastException ice) 
```csharp
{ 
 Console.WriteLine("obj cannot be downcasted to AfricanLion"); 
} 
```
In this example, we cast an Object to AfricanLion. This operation is called 
downcasting and is permitted only if we indicate the type we want to cast 
to, because Object is a parent class of AfricanLion and it is not clear if the 
variable obj is of type AfricanLion. If it  is not, an InvalidCastException 
will be thrown.

### The Object.ToString() Method

One of the most commonly used methods, originating from the class Object 
is ToString(). It returns a textual representation of an object . Every 
object includes this method and therefore  has a textual representation. This 
```csharp
method is used when we print the object using Console.WriteLine(…).
```

### Object.ToString() – Example

Here is an example in which we call the ToString() method: 
ToStringExample.cs 
```csharp
public class ToStringExample 
{ 
 static void Main() 


 { 
  Console.WriteLine(new object()); 
  Console.WriteLine(new Felidae(true)); 
  Console.WriteLine(new Lion(true, 80)); 
 } 
} 
```
The result is: 
System.Object 
Chapter_20_OOP.Felidae 
Chapter_20_OOP.Lion 
In this case, the base class implementation is called, because Lion doesn’t 
override ToString(). Felidae also doesn’t override the method; therefore, 
we actually call the implementation inherited from  System.Object. The 
result above contains the namespace of the object and the name of the class.

### Overriding ToString() – Example

We will now demonstrate how useful overriding ToString() inherited from 
System.Object can be: 
AfricanLion.cs 
```csharp
public class AfricanLion : Lion 
{ 
 // … 
 
 public override string ToString() 
 { 
  return string.Format( 
   "(AfricanLion, male: {0}, weight: {1})", 
   this.Male, this.Weight); 
 } 
 
 // … 
} 
```
In the source code above, we use the method String.Format(…), in order to 
format the result appropriately. This is how we can then invoke the 
overridden method ToString(): 
OverrideExample.cs 
```csharp
public class OverrideExample 

{ 
 static void Main() 
 { 
  Console.WriteLine(new object()); 
  Console.WriteLine(new Felidae(true)); 
  Console.WriteLine(new Lion(true, 80)); 
  Console.WriteLine(new AfricanLion(true, 80)); 
 } 
} 
```
The result is: 
System.Object 
Chapter_20_OOP.Felidae 
Chapter_20_OOP.Lion 
(AfricanLion, male: True, weight: 80) 
Notice that ToString() is invoked implicitly. When we pass an object to the 
WriteLine() method, that object provides its string representation using 
ToString() and only then it is printed to the output stream. That way, 
there’s no need to explicitly get string representations o f objects when 
printing them.

## Virtual Methods: the "override" and "new" Keywords

We need to explicitly instruct the compiler that we want our method to 
override another. In order to do this, we use the override keyword. Notice 
what happens if we remove it: 
 
Let’s experiment and use the keyword new instead of override: 
```csharp
public class AfricanLion : Lion 
{ 
 // … 
 
 public new string ToString() 
 { 
  return string.Format( 


   "(AfricanLion, male: {0}, weight: {1})", 
   this.Male, this.Weight); 
 } 
 
 // … 
} 
 
public class OverrideExample 
{ 
 static void Main() 
 { 
  AfricanLion africanLion = new(true, 80);
  string asAfricanLion = africanLion.ToString(); 
  string asObject = ((object)africanLion).ToString(); 
  Console.WriteLine(asAfricanLion); 
  Console.WriteLine(asObject); 
 } 
} 
```
This is the result: 
(AfricanLion, male: True, weight: 80) 
Chapter_20_OOP.AfricanLion 
We notice that the implementation of Object.ToString() is invoked when we 
upcast AfricanLion to object. In other words, when we use the keyword 
new, we create a new method , which hides the old one. The old method can 
then only be called with an upcast. 
What would happen, if we reverted to using the keyword override in the 
previous example? Take a look for yourself: 
(AfricanLion, male: True, weight: 80) 
(AfricanLion, male: True, weight: 80) 
Surprising, isn’t it? It turns out that when we override a method, we cannot 
access the old implementation even if we use upcasting. This is because there 
are no longer two ToString() methods, but rather only the one we overrode. 
A method, which can be overridden,  is called virtual. In .NET, methods are 
not virtual by default. If we want a method to be overridable, we can do so 
by including the keyword virtual in the declaration of the method. 
The explicit instructions to the compiler that we want to override a method 
(by using override), is a protection against mistakes. If there ’s a typo in the 
method’s name or the types of its parameters, the compiler will inform us 

immediately of this mistake. It will know something is not right when it 
cannot find a method with the same signature in any of the base classes. 
Virtual Methods are explained in details in the section about polymorphism.

## Transitive Properties of Inheritance

In mathematics, transitivity indicates transferability of relationships. Let ’s 
take the indicator "larger than"  (>) as an example. If A>B and B>C, we can 
conclude that A>C. This means that the relation "larger than" (>) is 
transitive, because we can unequivocally determine whether A is larger or 
smaller than C and vice versa. 
If the class Lion inherits the class Felidae and the class AfricanLion 
inherits Lion, then this implies that AfricanLion inherits Felidae. Therefore, 
AfricanLion also has the property Male, which is defined in Felidae. This 
useful property allows a particular functionality to be defined in the most 
appropriate class.

### Transitiveness – Example

Here is an example, which  demonstrates the transitive property of 
inheritance: 
TransitivenesExample.cs 
```csharp
public class TransitivityExample 
{ 
 static void Main() 
 { 
  AfricanLion africanLion = new(true, 15);
  // Property defined in Felidae 
  bool male = africanLion.Male; 
  africanLion.Male = true; 
 } 
} 
```
It is because of the transitive property of inheritance that we can be sure that 
all classes include the method ToString() and all other methods of Object 
regardless of which class they inherit.

### Inheritance Hierarchy

If we try to describe all big cats, then, sooner or later, we will end up with a 
relatively large group of classes, which inherit one another. All these classes, 
combined with the base cl asses, form a hierarchy of big cat classes. The 
easiest way to describe such hierarchies is by using class diagrams . Let’s 
take a look at what a "class-diagram" is.

## Class Diagrams

A Class Diagram is one of several types of diagrams defined in UML. UML 
(Unified Modeling Language)  is a notation for visualizing different 
processes and objects related to software development. We will talk about 
this further in the section on UML notation. Now let’s discuss class diagrams, 
because they are used to describe visually class hierarchies, inheritance and 
the structure of the classes themselves.

### What is UML Class Diagram?

It is commonly accepted to draw class diagrams as rectangles with name, 
attributes (member variables) and operations (methods). The connections 
between them are denoted with various types of arrows. 
Briefly, we will explain two pieces of UML terminology, so we can understand 
the examples more easily. The first one is  generalization. Generalization is a 
term signifying the inheritance of a class  or the implementation of an 
interface (we will explain interfaces shortly). 
The other term is association. An association, would be, e.g. "The Lion has 
paws", where Paw is another class. Association is has-a relationship. 
 
Generalization and association are the two main ways to 
reuse code.

### A Class Based on a Class Diagram – Example

This is what a sample class diagram looks like: 
 
The class is represented as  a rectangle, divided in 3 boxes one under 
another. The name of the class is at the top. Next, there are the attributes 
(UML term) of the class (in .NET they are called member variables and 
properties). At the very bottom are the operations (UML term) or methods 
(in .NET jargon). The plus/minus signs indicate whether an attribute  / 
operation is visible ( + means public) or not visible ( - means private). 
Protected members are marked with #.

### Class Diagram – Example of Generalization

Here is a c lass diagram that visually illustrates generalization (Felidae 
inherited by Lion inherited by AfricanLion): 
+Male() : bool
Felidae
-male : bool

 
In this example, the arrows indicate generalization (inheritance).

### Associations

Associations denote connections between classes . They model mutual 
relations. They can define multiplicity (1 to 1, 1 to many, many to 1, 1 to 2, 
…, and many to many). 
A many-to-many association is depicted in the following way: 
 
A many-to-many association by attribute is depicted in the following way: 
 
In this case, there are connecting attributes, which  indicate the variables 
holding the connection between classes. 
A one-to-many association is depicted like this: 
+Male() : bool
Felidae
-male : bool
+Weight() : int
Lion
-weight : int
+ToString() : string
AfricanLion
CourseStudent
* *
CourseStudent -courses
*
-students
*

 
A one-to-one association is depicted like this:

### From Diagrams to Classes

Class diagrams are most often used for creating classes. Diagrams facilitate 
and speed up the design of classes in a software project. 
We can create classes directly following the diagram above.  Here is the 
Capital class: 
Capital.cs 
```csharp
public class Capital { } 
```
And the Country class: 
Country.cs 
```csharp
public class Country 
{ 
 /// <summary>Country's capital - association</summary> 
 private Capital capital; 
 
 // … 
 
 public Capital Capital 
 { 
  get { return capital; } 
  set { this.capital = value; } 
 } 
 
 // … 
} 
```
ExamStudent
1 *
CapitalCountry
1 1

### Aggregation

Aggregation is a special type of association. It models the relationship of 
kind "whole / part". We refer to the parent class as an aggregate. The 
aggregated classes are called components. There is an empty rhombus at 
one end of the aggregation:

### Composition

A filled rhombus represents composition. Composition is an aggregation 
where the components cannot exist without the aggregate: