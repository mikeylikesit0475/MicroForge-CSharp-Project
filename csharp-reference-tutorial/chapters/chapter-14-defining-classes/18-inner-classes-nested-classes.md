# Inner Classes (Nested Classes)

In C# an inner (nested) class is called a class that is declared inside the 
body of another class. Accordingly, the class that encloses the inner class is 
called an outer class. 
The main reason to declare one class into another are: 
1. To better organize the code  when working with objects in the real 
world, among which have a special relationship and one cannot exist 
without the other. 
2. To hide a class in another class , so that the inner class cannot b e 
used outside the class wrapped it. 
In general, inner classes are used rarely, because they complicate the 
structure of the code and increase the nested levels.

## Declaration of Inner Classes

The inner classes are declared in the same way as normal classes,  but are 
located within another class . Allowed modifiers in the declaration of the 
```csharp
class are: 
1. public – an inner class is accessible from any assembly. 
2. internal – an inner class is available in the current assembly, in which 
```
is located the outer class. 
3. private – access is restricted only to the class holding the inner class. 
4. static – an inner class contains only static members. 
There are four more permitted modifiers – abstract, protected, protected 
internal, sealed and unsafe, which are outside the scope and subject of 
this chapter and will not be considered here. 
The keyword this to an inner class has relation only to the internal class, but 
not to the outside. Fields of the outside class cannot be accessed using the 
reference this. If necessary fields of  the outer class can be accessed by the 


internal, it needs in creating the internal class to submit a reference to an 
outer class. 
Static members  (fields, methods, properties) of the outer class are 
accessible from the inner class regardless of their level of access.

## Inner Classes – Example

Consider the following example: 
OuterClass.cs 
```csharp
public class OuterClass 
{ 
 private string name; 
 
 private OuterClass(string name) 
 { 
  this.name = name; 
 } 
 
 private class NestedClass 
 { 
  private string name; 
  private OuterClass parent; 
 
  public NestedClass(OuterClass parent, string name) 
  { 
   this.parent = parent; 
   this.name = name; 
  } 
 
  public void PrintNames() 
  { 
   Console.WriteLine("Nested name: " + this.name); 
   Console.WriteLine("Outer name: " + this.parent.name); 
  } 
 } 
 
 static void Main() 
 { 
  OuterClass outerClass = new("outer");
  NestedClass nestedClass = new 
   OuterClass.NestedClass(outerClass, "nested"); 
  nestedClass.PrintNames(); 
 } 
} 

In the example the outer class OuterClass defines into itself as a member 
the class InnerClass. Non-static inner class methods have access to their 
own body this as well as the instance of outside class parent (through 
syntax this.parent, if the parent reference is added by the developer). In 
the example while creating the inner class, parent reference is set to 
```
constructor of the outer class. 
If we run the above example, we will obtain the following result: 
Nested name: nested 
Outer name: outer

## Usage of Inner Classes

Consider an example. Let’s have a class for car – Car. Each car has an engine 
and doors. Unlike the car ’s door, however, the engine makes no sense 
regarded as being outside the car, because without it, the car cannot run, i.e. 
we have comp osition (see the section "Class Diagrams: Composition" in the 
chapter "Principles of Object-Oriented Programming"). 
 
When the connection between the two classes is a 
composition, the class, which consequently is a part of 
another class, is convenient to be declared as inner class. 
Therefore, if you declare the class for a car: Car would be appropriate to 
create an inner class Engine, which will reflect the appropriate concept for the 
car engine: 
Car.cs 
```csharp
class Car 
{ 
 Door FrontRightDoor; 
 Door FrontLeftDoor; 
 Door RearRightDoor; 
 Door RearLeftDoor; 
 Engine engine; 
 
 public Car() 
 { 
  engine = new Engine(); 
  engine.horsePower = 2000; 
 } 
 
 public class Engine 
 { 


  public int horsePower; 
 } 
}
```

## Declare Enumeration in a Class

Before proceeding to the next section that refers to generic types, it should be 
noticed, that sometimes enumeration should and can be declared within 
a class in order of better encapsulation of the class. 
For example, the enumeration of type CoffeeSize, we have created in the 
previous section, can be declared inside the body of the class Coffee, thereby 
it improves its encapsulation: 
Coffee.cs 
```csharp
class Coffee 
{ 
 // Enumeration declared inside a class 
 public static enum CoffeeSize 
 { 
  Small = 100, Normal = 150, Double = 300 
 } 
 
 // Instance variable of enumerated type 
 private CoffeeSize size; 
 
 public Coffee(CoffeeSize size) 
 { 
  this.size = size; 
 } 
 
 public CoffeeSize Size 
 { 
  get { return size; } 
 } 
} 
Respectively, the method for calculation of the price of coffee will be slightly 
```
modified slightly: 
```csharp
public double CalcPrice(Coffee.CoffeeSize coffeeSize) 
{ 
```
 switch (coffeeSize) 
```csharp
 { 

```
  case Coffee.CoffeeSize.Small: 
   return 0.20; 
  case Coffee.CoffeeSize.Normal: 
   return 0.40; 
  case Coffee.CoffeeSize.Double: 
   return 0.60; 
  default: 
   throw new InvalidOperationException( 
    "Unsupported coffee quantity: " + ((int)coffeeSize)); 
 } 
}