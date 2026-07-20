# Classes in C#

So far we have considered s everal common characteristics of OOP. A great 
part of the modern programming languages are object-oriented. Each of 
them has particular features for working with classes and objects. In this book 
we are going to focus only one of these languages – C#. It i s good to know 
that the knowledge of OOP in C# would be useful to the reader no matter 
which object-oriented language he uses in practice. That is because OOP is a 
fundamental concept in programming , used by virtually all modern prog -
ramming languages.

## What Are Classes in C#?

A class in C# is defined by the key word class, followed by an identifier 
(name) of the class and a set of data members and methods in a separate 
code block. 
Classes in C# can contain the following elements: 

- Fields – member-variables from a certain type; 
- Properties – these are a special type of elements, which  extend the 
functionality of the fields by giving the ability of extra data management 
when extracting and recording it in the class fields. We are going to 
focus on them in the chapter "Defining Classes"; 
- Methods – they implement the manipulation of the data.

## An Example Class

We are going to give an example of a class in C #, which contains the listed 
elements. The class Cat models the real -world object "cat" and has the 
properties name and color. The given class defines several fields, properties 
and methods, which we are going to use later. You can now see the definition 
of the cl ass (we are not going to consider in detail s the definition of the 
classes – we are going to focus on that in the chapter "Defining Classes"): 
```csharp
public class Cat 
{ 
 // Field name 
 private string name; 
 // Field color 
 private string color; 
 
 public string Name 
 { 
  // Getter of the property "Name" 
  get 
  { 
   return this.name; 
  } 
  // Setter of the property "Name" 
  set 
  { 
   this.name = value; 
  } 
 } 
 
 public string Color 
 { 
  // Getter of the property "Color" 
  get 
  { 
   return this.color; 
  } 
  // Setter of the property "Color" 

  set 
  { 
   this.color = value; 
  } 
 } 
 
 // Default constructor 
 public Cat() 
 { 
  this.name = "Unnamed"; 
  this.color = "gray"; 
 } 
 
 // Constructor with parameters 
 public Cat(string name, string color) 
 { 
  this.name = name; 
  this.color = color; 
 } 
 
 // Method SayMiau 
 public void SayMiau() 
 { 
  Console.WriteLine("Cat {0} said: Miauuuuuu!", name); 
 } 
} 
The example class Cat defines the properties Name and Color, which keep 
```
their values in the hidden (private) fields name and color. Furthermore, two 
constructors are defined for creating instances of the class Cat, respectively 
with and without parameters, and a method of the class SayMiau(). 
After the example class is defined we can now use it in the following way: 
```csharp
static void Main() 
{ 
 Cat firstCat = new();
 firstCat.Name = "Tony"; 
 firstCat.SayMiau(); 
 
 Cat secondCat = new("Pepy", "red");
 secondCat.SayMiau(); 
 Console.WriteLine("Cat {0} is {1}.", 
  secondCat.Name, secondCat.Color); 
} 

If we execute the example, we are going to get the following output: 
```
Cat Tony said: Miauuuuuu! 
Cat Pepy said: Miauuuuuu! 
Cat Pepy is Red. 
We saw a simple example for defining and using classes, and in the section 
"Creating and Using Objects" we are going to explain in detail s how to create 
objects, how to access their properties and how to call their methods and this 
is going to allow us to understand how this example works.

## System Classes

```csharp
Calling the method Console.WriteLine(…) of the class System.Console is 
an example of usage of a system class  in C#. We call system classes the 
classes defined in standard libraries  for building applications with C# (or 
```
another programming language). They can be used in all our .NET 
applications (in particular those written in C#). Such are for example the 
classes String, Environment and Math, which we are going to consider later. 
As we already know from chapter " Introduction to Programming " the .NET 
Framework SDK comes with a set of programming languages (like C# and 
VB.NET), compilers and standard class library  which provides thousands of 
system classes for accomplishing the most common tasks in programming like 
console-based input / output, text processing, collection classes, parallel 
execution, networking, database access, data processing, as well as creating  
Web-based, GUI and mobile applications. 
It is important to know that the implementation of the logic in classes is 
encapsulated (hidden) inside them. For the programmer it is important what 
they do, not how they do it and for this reason a great part of t he classes is 
not publicly available ( public). With system classes the implementation is 
often not available at all to the programmer. Thus, new layers of 
abstraction are created which is one of the basic principles in OOP. 
We are going to pay special attention to system classes later. Now it is time to 
get familiar with creating and using objects in programs.