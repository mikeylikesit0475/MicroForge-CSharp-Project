# Structures

In C# and .NET Framework there are two implementations of the concept of 
"class" from the object -oriented programming: classes and structures. 
Classes are defined through the keyword class while the structures are 
defined through the keyword struct. The main difference between a 
structure and a class is that: 
- Classes are reference types  (references to some address in the heap 
which holds their members). 
- Structures (structs) are value types  (they directly hold their 
members in the program execution stack).

## Structure (struct) – Example

Let’s define a structure to hold a point in the 2D space, similar to the class 
Point defined in the section "Example of Encapsulation": 
Point2D.cs 
```csharp
struct Point2D 
{ 
 private double x; 
 private double y; 
 
 public Point2D(int x, int y) 
 { 
  this.x = x; 
  this.y = y; 

 } 
 
 public double X 
 { 
  get { return this.x; } 
  set { this.x = value; } 
 } 
 
 public double Y 
 { 
  get { return this.y; } 
  set { this.y = value; } 
 } 
} 
```
The only difference is th at now we defined Point2D as struct, not as class. 
Point2D is a structure, a value type , so  its instances behave like int and 
double. They are value types (not objects), which means they cannot be null 
and they are passed by value when taken as a method parameters.

## Structures are Value Types

Unlike classes, the structures are value types. To illustrate this we will play 
a bit with the Point2D structure: 
```csharp
class PlayWithPoints 
{ 
 static void PrintPoint(Point2D p) 
 { 
  Console.WriteLine("({0},{1})", p.X, p.Y); 
 } 
 
 static void TryToChangePoint(Point2D p) 
 { 
  p.X++; 
  p.Y++; 
 } 
 
 static void Main() 
 { 
  Point2D point = new(3, -2);
  PrintPoint(point); 
  TryToChangePoint(point); 
  PrintPoint(point); 
 } 

} 
```
If we run the above example, the result will be as follows: 
(3,-2) 
(3,-2) 
Obviously the structures are value types  and when passed as parameters 
to a method their fields are copied  (just like int parameters) and when 
changed inside the method, the change affects only the copy, not the original. 
This can be illustrated by the next few figures. 
First, the point variable is created which holds a value of (3, -2): 
 
Next, the method TryToChangePoint(Point2D p) is called and it copies the 
value of the variable point into another place in the stack , allocated for 
the parameter p of the method. When the parameter p is changed in the 
method’s body, it is modified in the stack and this does not affect the 
original variable point which was previously passed as argument  when 
calling the method: 
  
HeapStack
3
point
Point2D instance
(nothing is stored
in the heap)-2
HeapStack
3
point
Point2D instance
(nothing is stored
in the heap)-2
4
p
Point2D (copy)
-1

If we change Point2D from struct to class, the result will be very different: 
(3,-2) 
(4,-1) 
This is because the variable point will be now passed by reference (not by 
value) and its value will be shared between point and p in the heap . The 
figure below illustrates what happens in the memory at the end of the method 
TryToChangePoint(Point2D p) when Point2D is a class:

## Class or Structure?

How to decide when to use a class and when a structure ? We will give 
you some general guidelines. 
Use structures  to hold simple data structures consisting of few fields that 
come together. Examples are coordinates, sizes, locations, colors, etc. 
Structures are not supposed to have functionality inside (no methods except 
simple ones like ToString() and comparators). Use structures for small data 
structures consisting of set of fields that should be passed by value. 
Use classes  for more complex scenarios where  you combine data and 
programming logic into a class. If you have logic, use a class. If you have 
more than few simple fields, use a class. If you need to pass variables by 
reference, use a class. If you need to assign a null value, prefer using a 
class. If you prefer working with a reference type, use a class. 
Classes are used more often than structures. Use structs as exception, and 
only if you know well what are you doing! 
There are few other differences between class and structure  in addition 
that classes are reference types and structures are values types, but we will 
not going to discuss them.  For more details refer to the following article in 
MSDN: http://msdn.microsoft.com/en-us/library/ms173109.aspx. 
HeapStack
point (reference variable)
Point2D@a8fe24
3 -2
Point2D object
p (parameter by reference)
Point2D@a8fe24