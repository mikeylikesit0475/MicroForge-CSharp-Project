# Custom Classes

The aim of every program written by the programmer is to solve a given 
problem based on the implementation of a certain idea. In order to create a 
solution, first, we sket ch a simplified actual model, which does not represent 
everything, but focuses on these facts, which  are significant for the end 
result. Afterwards, based on the sketched model, we are looking for an 
answer (i.e. to create an algorithm) for our problem and  the solution we 
describe via given programming language. 
Nowadays, the most used programming languages are the object -oriented. 
And because the object-oriented programming (OOP)  is close to the way 
humans think, using one easily allows us to describe mode ls of the 
surrounding life. Certain reason for this behavior is, because OOP offers tools 
to draw the set of concepts, which outline classes of objects in every model. 
The term – class and the definition of custom classes, different from the .NET 
system framework’s, is built-in feature of the C# programming language. The 
purpose of this chapter is to get us know with it.

## Let’s Recall: What Does It Mean Class and Object?

Class in the OOP is called a definition ( specification) of a given type of 
objects from the real-world. The class represents a pattern, which describes 
the different states and behavior of the certain objects (the copies), which are 
created from this class (pattern). 
Object is a copy created from the definition (specification) of a given clas s, 
also called an instance. When one object is created by the description of one 
class we say the object is from type "name of the class". 

For example, if we have a class type Dog, which describes some of the 
characteristics of a real dog, then, the objects based on the description of the 
```csharp
class (e.g. the doggies "Fido" and "Rex") are from type class Dog. It means 
the same when the string "some string" is from class type String. The  
```
difference is that objects from type Dog is are copies of the class, which is 
not part of the system library classes of the .NET Framework, but defined by 
ourselves (the users of the programming language).

## What Does a Class Contain?

Every class contains a definition of what kind of data types and objects has in 
order to be described. The object (the certain copy of this class) holds the 
actual data. The data defines the object’s state. 
In addition to the state, in the class is described the behavior of the objects. 
The behavior is represented by actions, which can be performed by the 
objects themselves. The resource in OOP, through which we can describe this 
behavior of the objects from a given class, is the declaration of methods in 
the class body.

## Elements of the Class

Now, we will go through the main elements of every class, and we will explain 
them in details latter. The main elements of a C# classes are the following: 
- Class declaration – this is the line where we declare the name of the 
```csharp
class, e.g.: 
public class Dog 
```
- Class body – similar to the method idioms in the language, the classes 
also have single class body. It is defined right after the class declaration, 
enclosed in curly brackets "{" and "}". The content inside the brackets is 
known as body of the class. The elements of the class, which are 
numbered below, are part of the body. 
```csharp
public class Dog 
{ 
 // … The body of the class comes here … 
}  
```
- Constructor – it is used for creating new objects . Here is a typical 
constructor: 
```csharp
public Dog() 
{ 
 // … Some code … 
} 

```
- Fields – they are variables, declared inside the class (somewhere in the 
literature are known as member-variables). The data of the object, 
which these variables represent, and are retained into them, is the 
specific state of an object, and  one is required for the proper work of 
object’s methods. The values, which are in the fields, reflect the specific 
state of the given object, but despite of this there are other types of 
fields, called static, which are shared among all the objects. 
// Field definition 
```csharp
private string name; 
```
- Properties – this is the way to describe the characteristics of a given 
```csharp
class. Usually, the value of the characteristics is kept in the fields of the 
```
object. Similar to the fields, the properties may be held by certain object 
or to be shared among the rest of the objects. 
// Property definition 
```csharp
private string Name { get; set; } 
```
- Methods – from the chapter " Methods" we know  that methods are 
named blocks of programming code. They perform particular actions and 
through them the objects achieve their behavior based on the clas s 
type. Methods execute the implemented programming logic (algorithms) 
and the handling of data.

## Sample Class: Dog

Here is how a class looks  like. The class Dog defined here owns all the 
elements, which we described so far: 
// Class declaration 
```csharp
public class Dog 
{ // Opening bracket of the class body 
 
 // Field declaration 
 private string name; 
 
 // Constructor declaration (peremeterless empty constructor) 
 public Dog() 
 { 
 } 
 
 // Another constructor declaration 
 public Dog(string name) 
 { 
  this.name = name; 

 } 
 
 // Property declaration 
 public string Name 
 { 
  get { return name; } 
  set { name = value; } 
 } 
 
 // Method declaration (non-static) 
 public void Bark() 
 { 
  Console.WriteLine("{0} said: Wow-wow!", 
   name ?? "[unnamed dog]"); 
 } 
} // Closing bracket of the class body 
```
At the moment we will not explain in greater details this code, because the 
related information will be presented later in this chapter.

> [!TIP]
> **Modern C# Note: Primary Constructors & Nullability**
> Modern C# (C# 8.0+) enables **Nullable Reference Types** by default, meaning `string name;` cannot be null unless declared as nullable: `string? name;`. This prevents `NullReferenceException` at compile-time.
> Additionally, C# 12 introduces **Primary Constructors** to define properties directly in the class header:
> ```csharp
> public class Person(string name, int age)
> {
>     public string Name { get; } = name;
>     public int Age { get; } = age;
> }
> ```
