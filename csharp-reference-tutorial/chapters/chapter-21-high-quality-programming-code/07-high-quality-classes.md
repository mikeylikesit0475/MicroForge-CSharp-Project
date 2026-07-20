# High-Quality Classes

Let’s now discuss the classes and the best practices about using efficiently 
classes when writing high-quality code.

## Software Design

When a system is designed, separate subtasks are often divided into 
separate modules or subsystems. The task that each one solves must be 
clearly defined. The relationships between the modules should be decided in 
advance, not on the go. 
In the previous chapter  we explained OOP and we showed how object -
oriented modeling can be used to define classes  of the real actors in the 
domain of the solved problem. We mentioned design patterns as well. 
Good software design has minimal complexity and is easy to understand. 
It is maintained easily and changes are incorporated straightforwardly (see 
the "Spaghetti Code" section in the previous chapter). Every program element 
(method, class, module) is logically connected internally ( strong cohesion), 

functionally-independent and minimally tied to the other modules ( loose 
coupling). Well-designed code is easily reused.

## Object-Oriented Programming (OOP)

When creating  quality classes, the main rules stem from the four main OO 
principles: abstraction, inheritance, encapsulation and polymorphism.

## Abstraction

A few basic rules: 
- Public properties of a class should have the same level of abstraction. 
- The interface of a class should be simple and clear. 
- A class should describe only one thing. 
- A class should hide its internal implementation. 
Code is developed and  changes and evolves  over time. In spite of the 
evolution of classes, their interfaces should remain in -tact. A bad practice of 
a class having inconsistent interface is shown below: 
```csharp
class Employee 
{ 
 public string firstName; 
 public string lastName; 
```
 … 
```csharp
 public SqlCommand FindByPrimaryKeySqlCommand(int id); 
} 
```
The latter method is incompatible with the level of abstraction at which 
Employee works. The user of this class should not be aware at all that a 
database is used internally.

## Inheritance

Do not hide methods in derived classes: 
```csharp
public class Timer 
{ 
 public void Start() { … } 
} 
 
public class AtomTimer : Timer 
{ 
 public void Start() { … } 
} 

```
The method in the derived class hides the base (original)  implementation. 
This is not recommended. If, in a rare case, this is desired and necessary, 
the keyword new should be used. 
Move common methods, data and behavior as high as possible in the 
inheritance tree. This way, functionality is less likely to be duplicated and will 
be accessible to a wider audience. 
If you have a class with a single successor only , consider this suspicious. 
That level of abstraction is probably unnecessary. A suspicious method would 
be one that re -implements a base method, but does nothing more than the 
corresponding base method. 
Deep inheritance with more than 6 levels is hard for tracing, debugging and 
maintaining, and is not recommended . In a derived class, use member -
variables through properties, rather than directly. 
The example below demonstrates wrongly written code  when inheritance 
should be preferred over type checking: 
switch (shape.Type) 
```csharp
{ 
```
 case Shape.Circle: 
  shape.DrawCircle(); 
  break; 
 case Shape.Square: 
  shape.DrawSquare(); 
  break; 
 … 
} 
It would make more sense if Shape was inherited by Circle and Square, 
which implement the virtual method Shape.Draw().

## Encapsulation

A good approach is to make all members private. Only those of them that 
should be visible from outside could be marked protected, or eventually 
```csharp
public. 
Implementation details should be hidden. The user of a high-quality class 
should not be aware of its inner-workings; he should only know what it does 
```
and how it is used. 
Member-variables (fields) should be hidden  behind properties. Public 
member-variables are a manifestation of low -quality code. Constants are an 
exception in this regard. 
The public member s of a class should be consistent with the abstraction 
represented by this class. Do not make assumptions about the usage scenario 
of a class. 

 
Do not rely on undocumented, internal implementation logic.

## Constructors

It is preferred that all class member s are initialized in the constructor . 
Usage of an uninitialized class is dangerous. A half -initialized class is maybe 
even more dangerous. Initialize member -variables in the same order as they 
are declared.

## Deep and Shallow Copy

When we assign values somet ime we need to copy an object (make a 
duplicate). This can be done in two ways: deep copy or shallow copy. 
Deep copies  of a n object  are copies in which all member -variables are 
copied, and their member -variables also, and so on, until no other member -
variables refer to objects. In a shallow copy, only the members at the first 
level are copied. Example of deep copied object and its members: 
 
Shallow copies work differently. When a shallow copy is created, the original 
object and its copy share some of their members: 
 
Original John 14yold
Irene 5yold
Barack 48yold
Copy

Shallow copies are dangerous  because a change in one object leads to 
indirect changes in others. Notice how the change of Iren’s age in the original 
does not affect the age of Iren in the copy  when we use deep copies . With 
shallow copies, the change will be reflected in both places.