# Visibility of Fields and Methods

In the beginning of this chapter we have discussed the generality of the 
modifiers and the access levels  for the elements in one class in C#. Later 
we have discussed the access level in the declaration for one class. 
Now we will discuss the visibility levels of fields and methods  in a class. 
Because the fields and the methods are elements of the class (membe rs) and 
have similar rules for access levels, we will expose these rules simultaneously.  
Differently from the declaration of a class, when we declare fields and 
methods in the class we can use the four access levels – public, protected, 
internal and private. The access level protected will not be discussed in 
this chapter, because it is related to class inheritance and is explained in 
details in the chapter "Object-Oriented Programming Principles". 
Before we continue, let’s revise, if one class A is not visible (does not have 
access) from other class B, then none of its elements (fields and method) can 
be accessed from class B. 
 
If two classes are not visible one to other, then their 
members (fields and methods) are not visible also, 
regardless of what kind of access levels their elements have. 
In the next subsections, to the explanations until now, we will review 
examples, in which we have two class es (Dog and Kid) and which are visible 
one to othe r, i.e. every from the classes can create objects from the other 
type – the other class and to access its elements depending from the defined 
access level declared. Here is how the first class Dog looks like: 
```csharp
public class Dog 
{ 
 private string name = "Doggy"; 
 
 public string Name 
 { 
  get { return this.name; } 
 } 
 
 public void Bark() 
 { 
  Console.WriteLine("wow-wow"); 


 } 
 
 public void DoSomething() 
 { 
  this.Bark(); 
 } 
} 
```
In addition to the field s and the methods the property Name is used, which 
just returns the field ’s value. We will discuss in details the property concept 
later, so currently we will just focus on everything else except the properties. 
The code of the class Kid looks like this: 
```csharp
public class Kid 
{ 
 public void CallTheDog(Dog dog) 
 { 
  Console.WriteLine("Come, " + dog.Name); 
 } 
 
 public void WagTheDog(Dog dog) 
 { 
  dog.Bark(); 
 } 
} 
```
Currently, all elements (fields and methods) of both classes are declared with 
access modifier public, but when we discuss the other access modifiers we 
will change some of them accordingly. What we would like to find is how the 
change in the access levels of the elements (fields and methods) of the class 
Dog will be reflected, when the access is made with: 
- The own body of the class Dog. 
- The body of the class Kid, respectively, taking into account that Kid is 
in the same namespace (or assembly), in which the Dog class is defined 
or not.

## Access Level "public"

When a method or a value of a class is declared with access level public, the 
last can be used from other classes, independently from the fact if another 
class is declared in the same namespace, assembly or outside of it. 
Let’s review both type of access t o members of a class, which are matched in 
our classes Dog and Kid: 

 
The access to the member of the class is done inside the 
same class directly (the class refers itself). 
 
The access to the member of the class is done via a 
reference to an object crea ted in the body of another class 
(the class refers another class). 
When the members of both classes are public, we have the following: 
Dog.cs 
 
 
 
 
 
 
 
 
 
 
 
 
```csharp
class Dog 
{ 
 public string name = "Doggy"; 
 public string Name 
 { 
  get { return this.name; } 
 } 
 public void Bark() 
 { 
  Console.WriteLine("wow-wow"); 
 } 
 public void DoSomething() 
 { 
  this.Bark(); 
 } 
} 
 
```
Kid.cs 
 
 
 
 
 
 
 
```csharp
class Kid 
{ 
 public void CallTheDog(Dog dog) 
 { 
  Console.WriteLine("Come, " + dog.name); 
 } 
 public void WagTheDog(Dog dog) 
 { 
  dog.Bark(); 
 } 
} 
```
D
R
D
D
R
R

As we can see, we implement without problem the access to the field name 
and the method Bark() of the class Dog from the body of the same class. 
Independently, if the namespace of the class Kid is the same as Dog, we can, 
from its body, a ccess the field name and to call the method Bark() via the 
“dot” operator, applied to the reference dog of the object from type Dog.

## Access Level "internal"

When a member of some class is declared with access level internal, then 
this element from the class can be accessed from every class in the same 
assembly (i.e. in the same project in Visual Studio), but not from classes 
outside it (i.e. from other project s in Visual Studio  – from the same solution 
or from a different solution). 
Not that if we have a Vi sual Studio project, all classes in it are from the same 
assembly and classes defined in different Visual Studio projects (in the same 
or in a different solution) are from different assemblies. 
Below is the explanation about the access level internal: 
Dog.cs 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
```csharp
class Dog 
{ 
 internal string name = "Doggy"; 
 
 public string Name 
 { 
  get { return this.name; } 
 } 
 
 internal void Bark() 
 { 
  Console.WriteLine("wow-wow"); 
 } 
 
 public void DoSomething() 
 { 
  this.Bark(); 
 } 
} 
Respectively, for the class Kid, we discuss two cases: 
```
- When the class in the same assembly, then the access to the elements 
of Dog will be allowed, independent of whether the classes are in the 
same namespace or not: 
D
D

Kid.cs 
 
 
 
 
 
 
 
```csharp
class Kid 
{ 
 public void CallTheDog(Dog dog) 
 { 
  Console.WriteLine("Come, " + dog.name); 
 } 
 
 public void WagTheDog(Dog dog) 
 { 
  dog.Bark(); 
 } 
} 
```
- When the class Kid is external for the assembly , in which Dog is 
declared, then the access to the field name and the method Bark() will 
be denied: 
Kid.cs 
 
 
 
 
 
 
 
```csharp
class Kid 
{ 
 public void CallTheDog(Dog dog) 
 { 
  Console.WriteLine("Come, " + dog.name); 
 } 
 
 public void WagTheDog(Dog dog) 
 { 
  dog.Bark(); 
 } 
} 
Actually the access level internal for members of the class Dog is impossible 
for two reasons: insufficient visibility of the class and insufficient visibility of 
its members. To allow access from other assembly to the class Dog, one is 
required to be declared public and in the same time its members to be 
declared as public. If the class or its members have lower visibility, the 
```
access to it from other assemblies is denied (i.e. from other Visual Studio 
projects which compile to different .dll / .exe file). 
If we try to compile the class Kid, when one is external for t he assembly, in 
which the class Dog resides, we will get a compilation error. 
R
R
R
R

## Access Level "private"

The access level, which is the most restrictive, is private. The elements of 
the class, which are declared with access modifier private (or without any, 
because private is the default one), cannot be accessed outside of the 
```csharp
class in which they are declared. 
Therefore, if we declare the field name and the method Bark() of the class 
```
Dog with access modifier private, there is no problem to access them from 
the same instance of the class Dog, but access from any other classes is not 
permitted. If you try to access a private method from external class, a 
compilation error occur. Below is the figure about the access level private: 
Dog.cs 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
```csharp
class Dog 
{ 
 private string name = "Doggy"; 
 
 public string Name 
 { 
  get { return this.name; } 
 } 
 
 private void Bark() 
 { 
  Console.WriteLine("wow-wow"); 
 } 
 
 public void DoSomething() 
 { 
  this.Bark(); 
 } 
} 
Accessing the name fields from the same class is permitted, but accessing it 
from a different class (Kid) is restricted: 
```
Kid.cs 
 
 
 
 
 
```csharp
class Kid 
{ 
 public void CallTheDog(Dog dog) 
 { 
  Console.WriteLine("Come, " + dog.name); 
 } 
```
D
D
R

 
 
 
```csharp
 public void WagTheDog(Dog dog) 
 { 
  dog.Bark(); 
 } 
} 
```
We should know, when we assign access modifier to a filed, one in most of 
the cases has to be private, because this ensures the highest level of 
security applied to the field. Respectively, the access and the modification of 
the value from other classes (if it is required) will be done only via properties 
or methods. More about this technique we will learn in the section " Properties 
and Encapsulation of Fields" as well as in the "Encapsulation" section of the 
chapter "Object-Oriented Programming Principles".

## How to Decide Which Access Level to Use?

Before we end up the section regarding visi bility of the elements of a class, 
let’s try something. Let’s define in the class Dog the field name and the method 
Bark() witch access modifier private. Let’s also declare the method Main() 
with the following body: 
```csharp
public class Dog 
{ 
 private string name = "Doggy"; 
 
 // … 
 
 private void Bark() 
 { 
  Console.WriteLine("wow-wow"); 
 } 
 
 // … 
 
 static void Main() 
 { 
  Dog myDog = new();
  Console.WriteLine("My dog's name is " + myDog.name); 
  myDog.Bark(); 
 } 
} 
The question is, if the class Dog can compile when we have declared the 
elements with access modifier private and in the same time is applied a 
```
”dot” notation to myDog in Main()? 
R

The compilation finished successfully . Respectively, the result from the 
execution of the method Main() which is declared in the class Dog will be the 
following: 
My dog's name is Rolf 
Wow-wow 
Everything works, because the access modifiers for the elements of the class 
are applied to the class and not to a level objects. Because the variable myDog 
is defined in th e body of the class Dog (where also is situated Main() – the 
start method of the program), we can access its elements (fields and 
methods) via “dot” notation, regardless we have declared the access level as 
```csharp
private. If we try to do the same in the body of the class Kid, this will be not 
possible, because the access to private fields from outside class is forbidden.
```