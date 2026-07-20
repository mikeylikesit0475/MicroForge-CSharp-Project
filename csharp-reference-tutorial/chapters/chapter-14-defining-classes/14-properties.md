# Properties

In the world of object -oriented programming there is an element of the 
classes called property, which is somewhere between a field and a 
method and serves to better protect the state in the class. In some 
languages f or object -oriented programming, like C#, Delphi / Pascal, Visual 
Basic, Python, JavaScript, and others, the properties are a part of the 
language, i.e. there is a special mechanism to declare and use them. Other 
languages like Java do not support the prope rty concept and for this purpose 


the programmers should declare a pair of methods (for reading and modifying 
the property) to provide this functionality.

## Properties in C# – Introduction by Example

Using the properties is a good and proven practice and an i mportant part of 
the concepts for object -oriented programming. The creation of a property in 
programming is done by declaring two methods – one for access (reading) 
and one for modifying (setting) the value of the respective property. 
Consider an example. Assume we have again class Dog, which describes a 
dog. A characteristic of a dog is, for example, its color. The access to the 
property "color" of a dog and its corresponding modification can be 
accomplished in the following way: 
// Getting (reading) a property 
string colorName = dogInstance.Color; 
 
// Setting (modifying) a property 
dogInstance.Color = "black";

## Properties – Encapsulation of Fields

The main objective of the properties is to ensure the encapsulation of the 
state of the class  in which they are declared, i.e. to protect the class from 
falling into invalid state. 
Encapsulation is hiding of the physical representation  of data in one 
```csharp
class so that if we subsequently change this presentation, it will not reflect on 
```
other classes, which use this class. 
Though the C# syntax this is done by declaring the fields (physical 
presentation of data) with possibly the most limited level of visibility (mostly 
with the modifier private) and declaring that access to these fields (reading 
and modifying) is to take place only through special accessor methods.

### Example of Encapsulation

To illustrate what the encapsulation, which provides properties to a class, is 
and what the properties themselves represent, we shall consider an example. 
Let’s have a class, which  represents a point from the 2D space with 
properties representing the coordinates {x, y}. Here is how it would look like 
if we declare each of the coordinates as a field: 
Point.cs 
```csharp
class Point 
{ 

 private double x; 
 private double y; 
 
 public Point(int x, int y) 
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
The fields of the objects of our class (i.e. the point ’s coordinates) are declared 
as private and cannot be accessed by a "dot" notation. If we create an 
object from class Point, we can modify and read the properties (the 
coordinates) of the point only through the properties for access to them: 
```
PointTest.cs 
```csharp
using System; 
 
class PointTest 
{ 
 static void Main() 
 { 
  Point myPoint = new(2, 3);
 
  double myPointXCoord = myPoint.X; // Access a property 
  double myPointYCoord = myPoint.Y; // Access a property 
 
  Console.WriteLine("The X coordinate is: " + myPointXCoord); 
  Console.WriteLine("The Y coordinate is: " + myPointYCoord); 
 } 
} 

```
The result of the execution of the Main() method will be: 
The X coordinate is: 2 
The Y coordinate is: 3 
If, however, we decide to change the internal representation of the point ’s 
properties, e.g. instead of two fields, we declare them as a one -dimensional 
array with two elements; we can do it without affecting in any way of the 
other classes of our project: 
Point.cs 
```csharp
using System; 
 
class Point 
{ 
 private double[] coordinates; 
 
 public Point(int xCoord, int yCoord) 
 { 
  this.coordinates = new double[2]; 
 
  // Initializing the x coordinate 
  coordinates[0] = xCoord; 
 
  // Initializing the y coordinate 
  coordinates[1] = yCoord; 
 } 
 
 public double X 
 { 
  get { return coordinates[0]; } 
  set { coordinates[0] = value; } 
 } 
 
 public double Y 
 { 
  get { return coordinates[1]; } 
  set { coordinates[1] = value; } 
 } 
} 
```
The result of the implementation of the Main() method will not be changed 
and will be the same even without changing a single character in the code of 
the class PointTest. 

The demonstration is a good example of data encapsulation  of an object, 
provided by the mechanism of the properties. Through them we hide the 
internal representation  of the information by declaring properties  and 
methods for accessing it, and if later a change occurs in the representation, 
this will not affect t he other classes using our class, because they only use its 
properties and do not know how the information is represented “behind the 
scene”. 
Of course, the example shows only one of the benefits of class fields wrapping 
(packing) into properties. Properties allow further control over the data  
in the class and they can check whether the assigned values are correct 
according to some criteria. For example, if we have a property “maximum 
speed” for a class Car, it is possible, through properties, to require its value to 
be within the range of 1 to 300 km/h.

## Physical Presentation of the Properties in a Class

As we saw above, the properties may have different presentation in one 
```csharp
class at a physical level. In our example, the information about the properties 
of the class Point initially was stored in two fields and later in one field–array. 
However, if we decide instead of keeping the information about the properties 
```
of the point in a field, to save it in a file or a database and every time we 
need to access the respective property, we can read or write from the file or 
the database rather th an use the fields of the class as in the previous 
examples. Since the properties are accessed by special methods (called 
methods for access and modification or accessor methods) to be discussed 
later, for the classes that will use our class the question ho w the information 
will be stored would not matter (because of the good encapsulation). 
In the most common case, however, the information about the properties of 
the class is saved in a field of the class, which has the most rigorous level of 
visibility – private. 
 
It does not matter how the information for the properties in 
a class in C# is saved, but usually this is done by a class field 
with the most restrictive access level (private).

### Property without Declaration of a Field

Consider an example, in whic h the property is stored neither in the field, nor 
anywhere else, but recalculated when trying to access it. 
Let’s have the class Rectangle, which represents the geometric shape of a 
rectangle. Accordingly, this class has two fields – for width and for height. 
Assume our class has one more property – area. Because we always can 
calculate the property “area”  of a rectangle based on the width and the 
height, it is not required to define a separate field in the class to keep this 
value. Therefore, we can simpl y declare a method for obtaining the area 
through which we calculate the area of a rectangle: 


Rectangle.cs 
```csharp
using System; 
 
class Rectangle 
{ 
 private float height; 
 private float width; 
 
 public Rectangle(float height, float width) 
 { 
  this.height = height; 
  this.width = width; 
 } 
 
 // Obtaining the value of the property area 
 public float Area 
 { 
  get { return this.height * this.width; } 
 } 
} 
```
As we will see later, a property does not necessarily have an accessing and a 
modifying method at the same time. Therefore, it is allowed to declare only a 
method for reading the property Area of the rectangle. There is no point to 
have a method, which modifies the value of the area of a rectangle because 
the area is always one and the same based on given lengths of the sides.

## Declaring Properties in C#

To declare a property in C#, we have to declare access methods (for reading 
and changing) of the resp ective property and to decide how we will store the 
information related to the property in the class. 
Before we declare the methods, however, we have to declare the property of 
the class. Formal declaration of properties appears in the following way: 
[<modifiers>] <property_type> <property_name> 
With <modifiers> we have denoted both the access modifiers and other  
modifiers (e.g. static, to be discussed in the next section of th is chapter). 
They are not a mandatory part of the declaration of a field. 
The type of the property  <property_type> specifies the type of the values 
of the property. It may be either a primitive type (e.g. int), or a reference 
type (e.g. array). 

Respectively, <property_name> is the name of the property. It must begin 
with a capital letter and to satisfy the PascalCase rule, i.e. every new word 
that is adjoined to the end part of the property name, starts with a capital 
letter. Here are some examples of properly named properties: 
// MyValue property 
```csharp
public int MyValue { get; set; } 
 
// Color property 
public string Color { get; set; } 
 
// X-coordinate property 
public double X { get; set; }
```

### The Body of a Property

Like classes and methods in C# properties also have bodies, where the 
methods for access are declared (accessors). 
[<modifiers>] <property_type> <property_name> 
```csharp
{ 
 // … Property's accessors methods go here 
} 
The body of a property begins with an opening bracket " {" and ends with a 
closing bracket – "}". Properties should always have a body.
```

### Method for Reading the Value of a Property (Getter)

As we explained, the declaration of a method for reading a value of a 
property (in the literature called a getter) is made in the body of a property 
by using the following syntaxes: 
get { <accessor_body> } 
The content of the block surrounded by the braces (<accessor_body>) is 
similar to the contents of any method. The actions, which should be 
performed to return the result of the method, are declared in it. 
The method of reading the value of a prope rty must end with a return or 
throw operation. The type of the value, which is returned as a result of this 
method, has to be the same as <property_type> described in the property 
declaration. 
Although earlier in this section we considered many examples of  declared 
properties with a method for reading their values, let’s consider another 
example of a property – Age, which is of type int and is declared via a field in 
the same class: 

```csharp
private int age;      // Field declaration 
 
public int Age     // Property declaration 
{ 
 get { return this.age; } // Getter declaration 
}
```

### Calling a Method for Reading Property’s Value

Assume that the property Age from the last example is declared in the class 
Dog. Then calling the method for reading the value of the property is  done by 
a “dot” notation, applied to a variable of the type, in the class of which the 
property is declared: 
Dog dogInstance = new Dog(); 
// … 
int dogAge = dogInstance.Age;     // Getter invocation 
```csharp
Console.WriteLine(dogInstance.Age);  // Getter invocation 
```
The last two lines of the example show that when accessing through a dot 
notation the name of the property, its getter method (method for reading its 
value) is called automatically.

### Method for Modifying Property’s Value (Setter)

Like the method of readin g the property ’s value we can also declare the 
method of changing ( modifying) the value of a property  (in the literature 
known as setter). It is declared in the body of a property with void return 
value and the assigned value is accessible through an implicit parameter 
value. 
The declaration is made in the body of the property through the  following 
syntax: 
set { <accessor_body> } 
The contents of the block surrounded by arrow brackets – <accessor_body> 
are similar to the content of any method. It declares the actions that must be 
performed to change the value of the property. The method uses a hidden 
parameter called value, which is available in C# by default and contains the 
new value of the property. The type of the parameter is the same as the type 
of the property. 
Let’s add the example for the property Age in the class Dog to illustrate what 
we discussed so far: 
```csharp
private int age;      // Field declaration 
 

public int Age     // Property declaration 
{ 
 get { return this.age; } 
 set { this.age = value; } // Setter declaration 
}
```

### Calling a Method for Modifying the Property’s Value

Calling the method to modify the property ’s value is performed via the “dot” 
notation, applied to the variable of the type, in the class of which the property 
is declared: 
Dog dogInstance = new Dog(); 
// … 
dogInstance.Age = 3;     // Setter invocation 
In the last line where the value 3 is assigned the setter method of the 
property Age is called. In this way the value is saved in the parameter value 
and is assigned to the setter method of the property Age. In our example, the 
value of the variable value is assigned to the field age from the class Dog, but 
in the general case this can be handled in a more complicated way.

### Assertion of the Input Values

It is a good practice in the programming process to check the validity of 
the input values  for the setter  method of modifying a property and if they 
are not valid to take the necessary “measures”. Mostly, in case of incorrect 
input data an exception is caused. 
Consider again the example with the age of the dog. As we know the age has 
to be a positive number. To prevent someone from assigning a negative 
number or a zero to the property Age, we add the following validation at the 
beginning of the setter method: 
```csharp
public int Age 
{ 
 get { return this.age; } 
 set 
 { 
  // Take precaution: perform check for correctness 
  if (value < 0) 
  { 
   throw new ArgumentException( 
    "Invalid argument: Age should be a positive number."); 
  } 
  // Assign the new correct value 
  this.age = value; 

 } 
} 
```
In case someone tries to assign a value to Age, which is a negative number or 
0, the code will throw an exception from the type ArgumentException, with 
details of the problem. 
To protect itself from invalid data a class must verify the input values for 
all properties and constructors submitted to the setter methods, as well as 
all methods, which can change a field of a class. This programming practice to 
protect classes from invalid data and invalid internal states is widely used and 
is a part of the " Defensive Programming" concept, which we will discuss in 
chapter "High-Quality Programming Code".

## Automatic Properties in C#

In C# we could define properties without explicitly defining the underlying 
field behind them. This is called automatic properties: 
Point.cs 
```csharp
using System; 
 
class Point 
{ 
 public double X { get; set; } 
 public double Y { get; set; } 
 
 public Point(int x, int y) 
 { 
  this.X = x; 
  this.Y = y; 
 } 
} 
 
class PointTest 
{ 
 static void Main() 
 { 
  Point myPoint = new(2, 3);
  Console.WriteLine("The X coordinate is: " + myPoint.X); 
  Console.WriteLine("The Y coordinate is: " + myPoint.Y); 
 } 
} 

The above example declares a class Point with two automatic properties: X 
```
and Y. These properties do not have explicitly defined underlying fields and 
the compiler defines them during the compilation. It looks like the get and 
set methods are empty but in fact the compiler defines an underlying field 
and fills the body of the get and set accessors with some code to read / write 
the automatically defined underlying field. 
Use automatic properties for simple classes  where you want to write less 
code but have in mind that when you use automatic properties your control 
over the assigned values is limited. You might have difficulties to add checks 
for invalid data.

## Types of Properties

Depending on their definition we can classify the properties as follows: 
1. Read-only, i.e. these properties have only a get method as shown by 
the area of the rectangle. 
2. Write-only, i.e. these properties have only a set method, but no 
method for reading the value of the property. 
3. And the most  common case is read-write, where the property has 
methods both for reading and for changing the value. 
Some properties are designed to be read-only. Others are supposed to 
support both read and write operations . The developers should decide 
whether someone should be able to change the value of given property and 
define it as read -only or read / write. Write-only properties are used very 
rarely.