# Static Classes and Static Members

We call an element static when it is declared with the modifier static. In C# 
we can declare fields, methods, properties, constructors and classes as static.  
We will first consider the static elements of a class or in other words we will 
look at the fields, methods, properties and constructors of a class and then we 
will study the concept of the static class.

## What the Static Elements Are Used For?

Before we study the working principle of the static elements, let ’s see the 
reasons why we need to use them.

### Method to Sum Two Numbers

Let’s imagine that we have a class with a single metho d that always works in 
the same manner. For example, its task is to get two numbers and return 
their sum as a result. In this scenario there is no matter exactly which object 
of that class is going to implement that method since it will always do the 
same thing – adding two numbers together, independent of the calling object. 

In practice the behavior of the method does not depend of the object 
state (the values in the object field). So why the need to create an object to 
accomplish that method provided that  the method does not depend on any of 
the objects of that class? Why not just get the class to implement that 
method?

### Instance Counter for Given Class

Consider a different scenario. Let ’s say we want to keep in our program the 
current number of objects, which have been created by a given class. How will 
we keep that variable, which stores the number of created objects? 
As we know, we will not be able to store the variable as a class field because 
for each created object there will be created a copy of that field, initialized 
with default value. Every single object will store its own field for indication of 
the number of objects and the objects will not be able to share information. 
It looks like the counter should be outside a class field rather than inside it. In 
the following subsections we will find out how to deal with such a problem.

## What Is a Static Member?

Formally speaking, a static member  of the class is every field, property, 
method or other member, which has a static modifier in its declaration 1. That 
means that fields, methods and properties, marked as static, belong to the 
particular class rather than to any particular object of the given class. 
Therefore, when we mark a field, method or property as static, we can use 
them without creating any ob ject of the given class. All we need is to have 
access (visibility) to the class so that we can call the object ’s static methods 
or its static fields and properties. 
 
Static elements of the class can be used without creating an 
object of the given class. 
On the other hand if we have created objects of the given class then it s static 
fields and properties will be shared  and there will be only one copy of the 
```csharp
static field or property which will be shared among all objects of the given 
class. Because of that  reason in the VB.NET language we have the keyword 
shared instead of the static keyword.
```

## Static Fields

When we create objects from a given class, each of them holds different 
values in its fields. For example, consider again the class Dog: 
```csharp
public class Dog 
{ 
 // Instance variables 
 private string name; 


 private int age; 
} 
There are two fields in the class, one for the name – name and another one for 
```
the age – age. Every object, each of these fields, has its own value, which is 
stored in a different place in the memory for every object. 
Sometimes, however, we want to have common fields  for all objects of a 
given class. To achieve that , we have to use the static modifier in the field 
declarations. As we already said, such fields are called static fields. In the 
literature they are also called class variables. 
We say that the static fields are class associated, rather than associated 
with any object from the particular class. That means that all objects, created 
by the description of a class share the static fields of the class. 
 
All objects, created by the description of a given class (that 
is, instances of a given class), share the static fields of the 
```csharp
class.
```

### Declaration of Static Fields

The static fields are declared the same way as the class fields. If there is 
access modifier, the keyword static should be added after it. 
[<access_modifier>] static <field_type> <field_name> 
Here is how a field named dogCount would look like. The field stores 
information about the count of the created objects from the class Dog: 
Dog.cs 
```csharp
public class Dog 
{ 
 // Static (class) variable 
 static int dogCount; 
 
 // Instance variables 
 private string name; 
 private int age; 
} 
The static fields are created when we try to access them for the first time 
```
(read / modify). After their creation they are initialized with their default 
values of their types.

### Initialization during Declaration

If during the static field declaration we set an initialization value, it will be 
assigned to the particular static field. The initialization executes only once 
when the field is accessed for the first time right after the assignment has 
finished. The next time when the field is accessed that field initialization will 
not execute. 
We can append the static field initialization in the example above: 
// Static variable – declaration and initialization 
```csharp
static int dogCount = 0; 
```
This initialization will complete during the first invocation to the static field. 
When we access some static field, an amount of memory will be reserved for 
it and it will be initialized with its default values. If the field has initialization 
during declaration (like it is in our case with the  dogCount field) this 
initialization will execute. If we try later to access the field from other part of 
our program this process will not  repeat, becau se the static field already 
exists and is initialized.

### Accessing Static Fields

In contrast to the common (non -static) class fields, the static fields that are 
associated with the particular class can be accessed through an external class. 
In order to do that we need to put a "dot" notation this way: 
<class_name>.<static_field_name> 
For example, if we want to print the value of the static field that holds the 
number of created objects of our class Dog we should do that: 
```csharp
static void Main() 
{ 
 // Access to the static variable through class name 
 Console.WriteLine("Dog count is now " + Dog.dogCount); 
} 
```
The result of the Main() method is: 
Dog count is now 0 
If we have a method in the class, which  is defined as a static field, we can 
access it directly without the class name, because it is known by default. 
<static_field_name>

### Modification of the Static Field Values

As we said before, the static variables are shared between all objects  of 
the class and do not belong to any object of the particular class. That way any 
object can access and modify the static field values and in th e same time 
other objects can “see” the modified values. 
That’s why if we want to count the number of created objects of the class Dog, 
we should use a static field  and increment it by one every time the 
constructor is invoked, i.e. every time we create and object of our class. 
```csharp
public Dog(string name, int age) 
{ 
 this.name = name; 
 this.age = age; 
 
 // Modifying the static counter in the constructor 
 Dog.dogCount += 1; 
} 
We access static field from the class Dog so we can use the following code in 
```
order to access the field dogCount: 
```csharp
public Dog(string name, int age) 
{ 
 this.name = name; 
 this.age = age; 
 
 // Modifying the static counter in the constructor 
 dogCount += 1; 
} 
```
The first way is preferable, it is clear that the field in the class Dog is static. 
The code is more readable. 
Let’s create some objects of the class Dog and print out their number in order 
to check if we are right: 
```csharp
static void Main() 
{ 
 Dog dog1 = new("Jackie", 1);
 Dog dog2 = new("Lassy", 2);
 Dog dog3 = new("Rex", 3);
 
 // Access to the static variable 
 Console.WriteLine("Dog count is now " + Dog.dogCount); 
} 

```
The output of the example is: 
Dog count is now 3

## Constants

Before we finish with the static fields we should get familiar with one more 
specific type of static fields. 
Like the  constants of mathematics, in C# special fields of a class called 
constants can be created. Once declared and initialized constants always 
have the same value for all objects of a particular type. 
In C# constants are of two types: 
1. Constants the values of which are extracted during the compilation of 
the program (compile-time constants). 
2. Constants the values of which are extracted during the e xecution of the 
program (run-time constants).

### Compile-Time Constants (const)

Constants, which are calculated at compile time (compile -time constants), are 
declared as follows, using modifier const: 
[<access_modifiers>] const <type> <name>; 
Constants, which are declared with special word const, are static fields. 
Nevertheless, the use of modifier static is not required (nor allowed by the 
compiler) in their declaration. 
 
Although the constants declared with a modifier const are 
static fields , they must not and cannot use the static 
modifier in their declaration. 
For example, if we want to declare as a constant the number "PI", which is 
known to us from mathematics, this can be done as follows: 
```csharp
public const double PI = 3.141592653589793; 
```
The value we assign to a particular constant can be an expression, which has 
to be calculated by the compiler at compile time. For example, as we know 
from mathematics, the constant "PI" can be represented as the approximate 
result of the division of numbers 22 and 7: 
```csharp
public const double PI = 22d / 7; 
```
When we try to print the value of the constant: 


```csharp
static void Main() 
{ 
 Console.WriteLine("The value of PI is: " + PI); 
} 
```
The command line will display: 
The value of PI is: 3.14285714285714 
If we do not give a value to a constant at its declaration, but later, we will get 
a compilation error. For example, if we take the example of the constant PI, 
we first declare the constant and later try to give it a value: 
```csharp
public const double PI; 
 
// … Some code … 
 
public void SetPiValue() 
{ 
 // Attempting to initialize the constant PI 
 PI = 3.141592653589793; 
} 
```
The compiler will issue an error like this one, indicating the line, where the 
constant is declared: 
A const field requires a value to be provided 
Let’s pay attention, again: 
 
Constants declared with modifier const must be initialized at 
the moment of their declaration.

### Assigning Constant Values at Runtime

Having learned how to declare constants that are being initialized at compile 
time, let’s consider the following example: we want to create a class for color 
(Color). We will use the so -called Red-Green-Blue (RGB) color  model, 
according to which, each color is represented by mixing the three primary 
colors – red, green and blue. These three pr imary colors are represented as 
three integers in the range from 0 to 255. For example, black is represented 
as (0, 0, 0), white as (255, 255, 255), blue – (0, 0, 255) etc. 
In our class we declare three integer fields for red, green and blue light and a 
constructor that accepts values for each of them: 


Color.cs 
```csharp
class Color 
{ 
 private int red; 
 private int green; 
 private int blue; 
 
 public Color(int red, int green, int blue) 
 { 
  this.red = red; 
  this.green = green; 
  this.blue = blue; 
 } 
} 
```
As some colors are used more frequently than others (for example, black and 
white) we can declare constants for them , with the idea that the users of 
our class will take them for granted, instead of creating their own objects for 
these particular colors every time. To do this, we modify the code of our class 
as follows, adding the declaration of the following color-constants: 
Color.cs 
```csharp
class Color 
{ 
 public const Color Black = new Color(0, 0, 0); 
 public const Color White = new Color(255, 255, 255); 
 
 private int red; 
 private int green; 
 private int blue; 
 
 public Color(int red, int green, int blue) 
 { 
  this.red = red; 
  this.green = green; 
  this.blue = blue; 
 } 
} 
Strangely, when we try to compile, we get the following error: 
```
'Color.Black' is of type 'Color'. A const field of a reference 
type other than string can only be initialized with null. 

'Color.White' is of type 'Color'. A const field of a reference 
type other than string can only be initialized with null. 
This is so because in C#, constants, declared with the modifier const, can be 
only of the following types: 
1. Primitive types: sbyte, byte, short, ushort, int, uint, long, ulong, 
char, float, double, decimal, bool. 
2. Enumerated types (discussed in section " Enumerations" at the end of 
this chapter). 
3. Reference types (mostly the type string). 
The problem with the compilation of the class in our example is connected 
with the reference types and the restriction on the compiler not to allow 
simultaneous use of the  operator new when declaring a constant when this 
constant is declared with the modifier const, unless the reference type can be 
calculated at compile time. 
As we can guess, the only reference type, which can be calculated at compile 
time while using the operator new is string. 
Therefore, the only possibilities for reference type constants that are declared 
with modifier const are, as follows: 
1. The constants must be of type string. 
2. The value, which we assign to the constant of reference type, other than 
string, is null. 
We can formulate the following definition: 
 
Constants declared with modifier const must be of primitive, 
enumeration or reference type, and if they are of reference 
type, this type must be either a string or the value, that we 
assign to the constant, must be null. 
Thus, using the modifier const, we will not be able to declare the constants 
Black and White of type Color in our color class because they aren ’t null. 
The next section will show us how to deal with this problem.

### Runtime Constants (readonly)

When we want to declare reference type constants, which cannot be 
calculated during compilation of the program, we must use a combination of 
static readonly modifiers, instead of const modifier. 
[<access_modifiers>] static readonly <reference-type> <name>; 


Accordingly, <reference-type> is a type the value of which cannot be 
calculated at compilation time. 
The compilation is successful if we replace  const by static readonly in the 
last example of the previous section: 
```csharp
public static readonly Color Black = new Color(0, 0, 0); 
public static readonly Color White = new Color(255, 255, 255);
```

### Naming the Constants

The constants names in C# follow the PascalCase rule according to the 
Microsoft’s official C# coding convention. If  the c onstant is composed  of 
several words, each new word  after the first one  begins with a capital letter. 
Here are some examples of correctly named constants: 
// The base of the natural logarithms (approximate value) 
```csharp
public const double E = 2.718281828459045; 
public const double PI = 3.141592653589793; 
public const char PathSeparator = '/'; 
public const string BigCoffee = "big coffee"; 
public const int MaxValue = 2147483647; 
public static readonly Color DeepSkyBlue = new Color(0,104,139); 
```
Sometimes naming in style ALL-CAPS is used but it is not officially supported 
by the Microsoft code conventions, even though it is widely distributed in 
programming: 
```csharp
public const double FONT_SIZE_IN_POINTS = 14; // 14pt font size 
The examples made it clea r that the difference between const and static 
```
readonly fields is in the moment of their value assignments. The compile -
time constants (const) must be initialized at the moment of declaration, while 
the run-time constants (static readonly) can be initializ ed at a later stage, 
for example in one of the constructors of the class in which they are defined.

### Using Constants

Constants are used in programming to avoid repetition of numbers, 
strings or other common values  (literals) in the program and to enable 
them to change easily. The use of constants instead of brutally hardcoded 
repeating values facilitates readability and maintenance of the code and is 
highly recommended practice. According to some authors all literals other 
than 0, 1, -1, empty string, true, false and null must be declared as 
constants, but this can make it difficult to read and maintain the code instead 
of making it simple. Therefore, it is believed that values, which occur more 
than once in the program or are likely to change over time,  must be 
declared as constants. 

In the chapter "High-Quality Programming Code" will we learn in details when 
and how to use constants efficiently.

## Static Methods

Like static fields, we declare a method  as static if we want it to be associated 
only with the class and not with a particular class object.

### Declaration of Static Methods

To declare a static method  syntactically means that we must add the 
keyword static in the method’s declaration: 
[<access_modifier>] static <return_type> <method_name>() 
Let’s for example declare the method of summing two numbers, which  we 
discussed at the beginning of this section: 
```csharp
public static int Add(int number1, int number2) 
{ 
 return (number1 + number2); 
}
```

### Accessing Static Methods

Like static fields, static methods can be accessed with the "dot" notation 
(the dot operator) applied to the name of the class and the class name can be 
skipped if the calling is performed by the same class, in which the static 
method is declared. Here is an example of calling the static method Add(…): 
```csharp
static void Main() 
{ 
 // Call the static method through its class 
 int sum = MyMathClass.Add(3, 5); 
 
 Console.WriteLine(sum); 
}
```

## Access between Static and Non-Static Members

In most cases static methods are used to access static fields  in the class 
they have been defined. For example, if we want to declare a method, which 
returns the number of the created objects of the Dog class, it must be static, 
because our counter will be static too: 
```csharp
public static int GetDogCount() 
{ 

 return dogCount; 
} 
But when we examine how static and non -static methods and fields can be 
```
accessed, not all combinations are allowed.

### Accessing Non-Static Members from Non-Static Method

Non-static methods can access non -static fields and other non -static methods 
of the class. For example, in the Dog class we can declare method 
PrintInfo(), which displays information about our dog: 
Dog.cs 
```csharp
public class Dog 
{ 
 // Static variable 
 static int dogCount; 
 
 // Instance variables 
 private string name; 
 private int age; 
 
 public Dog(string name, int age) 
 { 
  this.name = name; 
  this.age = age; 
 
  dogCount += 1; 
 } 
 
 public void Bark() 
 { 
  Console.Write("wow-wow"); 
 } 
 
 // Non-static (instance) method 
 public void PrintInfo() 
 { 
  // Accessing instance variables – name and age 
  Console.Write("Dog's name: " + this.name + "; age: " 
   + this.age + "; often says: "); 
 
  // Calling instance method 
  this.Bark(); 

 } 
} 
Of course, if we create an object of the Dog class and call his PrintInfo() 
```
method: 
```csharp
static void Main() 
{ 
 Dog dog = new("Doggy", 1);
 dog.PrintInfo(); 
} 
```
The result will be the following: 
Dog's name: Doggy; age: 1; often says: wow-wow

### Accessing Static Elements from Non-Static Method

We can access static fields and static methods of the class from non -static 
method. As we learned earlier, this is because static methods and variables 
are bound by class, rather than a specific method and the static elemen ts can 
be accessed from any object of the class, even of external classes (as long as 
they are visible to them). 
For example: 
Circle.cs 
```csharp
public class Circle 
{ 
 public static double PI = 3.141592653589793; 
 
 private double radius; 
 
 public Circle(double radius) 
 { 
  this.radius = radius; 
 } 
 
 public static double CalculateSurface(double radius) 
 { 
  return (PI * radius * radius); 
 } 
 
 public void PrintSurface() 
 { 

  double surface = CalculateSurface(radius); 
  Console.WriteLine("Circle's surface is: " + surface); 
 } 
} 
In the example, we provide access to the value of the static field PI of the 
non-static method PrintSurface(), by calling the static method 
CalculateSurface(). Let’s try to call this non-static method: 
static void Main() 
{ 
 Circle circle = new(3);
 circle.PrintSurface(); 
} 
```
After the compilation and the execution, the following result will be printed on 
the console: 
Circle's surface is: 28.2743338823081

### Accessing Static Elements of the Class from Static Method

We can call a  static method or static field of the class from another static 
method without any problems. 
For example, let ’s consider our class for mathematical calculations. We have 
declared the constant PI, in it. We can declare a static method for finding the 
length of the circle (the formula for finding perimeter of a circle is 2πr, where 
r is the radius of the circle), that uses the constant PI for calculating the 
perimeter of a circle. Then, to show that static method can call another static 
method, we can call th e static method for finding the perimeter of a circle 
from the static method Main(): 
MyMathClass.cs 
```csharp
public class MyMathClass 
{ 
 public const double PI = 3.141592653589793; 
 
 // The method applies the formula: P = 2 * PI * r 
 static double CalculateCirclePerimeter(double r) 
 { 
  // Accessing the static variable PI from static method 
  return (2 * PI * r); 
 } 
 

 static void Main() 
 { 
  double radius = 5; 
 
  // Accessing static method from other static method 
  double circlePerimeter = CalculateCirclePerimeter(radius); 
 
  Console.WriteLine("Circle with radius " + radius + 
   " has perimeter: " + circlePerimeter); 
 } 
} 
```
The code is compiled without errors and displays the following output: 
Circle with radius 5.0 has perimeter: 31.4159265358979

### Accessing Non-Static Elements from Static Method

Let’s look at the most interesting case of a combination of accessing non -
```csharp
static and static elements of the class – accessing non -static elements 
form a static method. 
We should know that from static method we can neither access non -static 
fields, nor call non -static methods. This is because static methods are bound 
to the class and do not “know” any object of the class. Therefore, the keyword 
this cannot be used in static methods – it is bound to a specific instance of 
the class. When we try to access non -static elements of the class (fields or 
methods) from static method, we will always get a compilation error.
```

### Unauthorized Access to Non-Static Field – Example

If in our class Dog we try to declare a static method PrintName(), which 
returns as a result the value of the non-static field name declared in the class: 
```csharp
public static void PrintName() 
{ 
 // Trying to access non-static variable from static method 
 Console.WriteLine(name); // INVALID 
} 
```
Accordingly, the compiler will respond with an error message: 
An object reference is required for the non -static field, 
method, or property 'Dog.name' 
If we try to access the field in the method, via the keyword this: 

```csharp
public void string PrintName() 
{ 
 // Trying to access non-static variable from static method 
 Console.WriteLine(this.name); // INVALID 
} 
```
The compiler will still not be satisfied and this time will fail to compile  the 
```csharp
class and will display the following message: 
Keyword 'this' is not valid in a static property, static method, 
or static field initializer
```

### Illegal Call of Non-Static Method from Static Method – Example

Now we will try to call non -static method from static method. Let declare in 
our class Dog, the non -static method PrintAge(), which prints the value of 
the field age: 
```csharp
public void PrintAge() 
{ 
 Console.WriteLine(this.age); 
} 
Accordingly, let’s try from the method Main(), which we declare in the class 
```
Dog, to call this method without creating an object of our class: 
```csharp
static void Main() 
{ 
 // Attempt to invoke non-static method from a static context 
 PrintAge(); // INVALID 
} 
When we try to compile we will get the following error: 
An object reference is required for the non -static field, 
```
method, or property 'Dog.PrintAge()' 
The result is similar, if we try to cheat the compiler, trying to call the method 
via the keyword this: 
```csharp
static void Main() 
{ 
 // Attempt to invoke non-static method from a static context 
 this.PrintAge(); // INVALID 
} 

Accordingly, as with the attempt to access the non -static field of a static 
method using the keyword this, the compiler displays the following error 
```
message and fails to compile our class: 
Keyword 'this' is not valid in a static property, static method, 
or static field initializer 
From the examples, we can make the following conclusion: 
 
Non-static elements of the class may NOT be used in a static 
context. 
The problem with the access to non -static elements of the class of static 
method has a single solution – these non -static elements are accessed by 
reference to an object: 
```csharp
static void Main() 
{ 
 Dog myDog = new("Lassie", 2);
 string myDogName = myDog.name; 
 Console.WriteLine("My dog \"" + myDogName +"\" has age of "); 
 myDog.PrintAge(); 
 Console.WriteLine("years"); 
} 
```
Accordingly, this code is compiled and the result is: 
My dog "Lassie" has age of 2 years

## Static Properties of the Class

Although rare, it is sometimes convenient to use and declare not the object 
characteristics, but the ones of the class. They possess the same 
characteristics like the properties related to the particular object of a 
particular class, which  we discussed abo ve, but with the difference that the 
static properties refer to the class (not its objects). 
As we can guess, all we need to do to turn a simple property into a static one, 
is to add the static keyword in its declaration. 
The static properties are declared as follows: 
[<modifiers>] static <property_type> <property_name> 
```csharp
{ 
 // … Property's accessors methods go here 
} 


Let’s consider an example. We have a class that describes a system. We can 
```
create many objects from it, but the model of the system has a version and a 
vendor, which are common to all instances created from this class. We can 
make the version and the vendors as static properties of the class: 
SystemInfo.cs 
```csharp
public class SystemInfo 
{ 
 private static double version = 0.1; 
 private static string vendor = "Microsoft"; 
 
 // The "version" static property 
 public static double Version 
 { 
  get { return version; } 
  set { version = value; } 
 } 
 
 // The "vendor" static property 
 public static string Vendor 
 { 
  get { return vendor; } 
  set { vendor = value; } 
 } 
 
 // … More (non)static code here … 
} 
In this example we have chosen to keep the value of static properties in static 
```
variables (which are logical, since they are bound only to the class). The 
properties that we consider are Version and Vendor, respectively. For each of 
them we have created static methods for reading and modification. Thus, all 
objects of this class will be able to retrieve the current version and vendor of 
the system, which describes the class. Accordingly, if one day an upgra de of 
the system version is done and the value becomes 0.2, as a result each 
object will receive the new version by accessing the class property.

### Static Properties and the Keyword “this”

Like static methods, the keyword this cannot be used in the static properties, 
as the static property is associated only with the class and does not 
“recognize” objects of a class. 
 
The keyword this cannot be used in static properties.

### Accessing Static Properties

Like the static fields and methods, static properties can be accessed by "dot" 
notation, applied only to the name of the class in which they are declared. 
To be sure, let ’s try to access the property Version through a variable of the 
```csharp
class SystemInfo: 
static void Main() 
{ 
 SystemInfo sysInfoInstance = new();
 Console.WriteLine("System version: " + 
  sysInfoInstance.Version); 
} 
When we try to compile the above code, we get the following error message: 
```
Member 'SystemInfo.Version.get' cannot be accessed with an 
instance reference; qualify it with a type name instead 
Accordingly, if we try to access the static properties through class name, the 
code compiles and works correctly: 
```csharp
static void Main() 
{ 
 // Invocation of static property setter 
 SystemInfo.Vendor = "Microsoft Corporation"; 
 
 // Invocation of static property getters 
 Console.WriteLine("System version: " + SystemInfo.Version); 
 Console.WriteLine("System vendor: " + SystemInfo.Vendor); 
} 
```
The code is compiled and the result of its execution is: 
System version: 0.1 
System vendor: Microsoft Corporation 
Before proceeding to the next section, let ’s look at the printed value of the 
property Vendor. It is "Microsoft Corporation", although we have initialized 
it with the value " Microsoft" in the SystemInfo class. This is because we 
changed the value of the property Vendor of the first line of the Main() 
method, by calling its method of modification. 
 
Static properties can be accessed only through dot notation, 
applied to the name of the class in which they are declared.

## Static Classes

For complete understanding we have to explain that we can also declare 
classes as static. Similar to static members, a class is static, when the 
keyword static is used in its declaration. 
[<modifiers>] static class <class_name> 
```csharp
{ 
 // … Class body goes here 
} 
When a class is declared as static, it is an indication that this class contains 
only static members (i.e. static fields, methods, properties) and cannot be 
```
instantiated. 
The use of static classes is rare and most often associated with the use of 
```csharp
static methods  and constants , which do not belong to any particular 
object. For this reason, the details of static classes go beyond the scope of 
```
this book. Curious reader can find  more information on the site of the 
Microsoft Developer Network (MSDN): http://msdn.microsoft.com/en-
us/library/79b3xss3.aspx.

## Static Constructors

To finish the section on static class members, we should mention that classes 
may also have static constructor  (i.e. constructor that has the static 
keyword in its declaration): 
[<modifiers>] static <class_name>([<parameters_list>]) 
```csharp
{ 
} 
```
Static constructors can be declared both in static and i n non-static classes. 
They are executed only once  when the first of the following two events 
occurs for the first time: 
1. An object of class is created. 
2. A static element of the class is accessed (field, method, property). 
Most often static constructors are used for initialization of static fields.

### Static Constructor – Example

Consider an example for the use of a static constructor . We want to make 
a class that quickly calculates the square root of an integer and returns the 
whole part of the result, which is  also an integer. Since calculating the square 
root is a time -consuming mathematical operation involving calculations with 
real numbers and calculating convergent series, it is a good idea these 
calculations to be done once at program startup and then to u se the already 

calculated values. Of course, to make such pre-computing of all square 
roots in a given range, we must first define this range and it should not be 
too wide (e.g. from 1 to 1000). Then we need, at first request for a square 
roots of a number, to recalculate all the square roots in this range and then to 
return the already calculated value. Upon a following request for a square 
root, all values in this range will have already been calculated and returned 
directly. If the program is never requi red to calculate the square root, 
preliminary calculations should not be fulfilled at all. 
Through the described process initially some CPU time is invested for 
preliminary calculations, but then the extraction of the square root later is 
done very quickly . If we have multiple calculations of the square root, the 
pre-calculation will significantly increase the performance. 
All this can be implemented in one static class with a static constructor , 
in which the square roots will be recalculated. The results, which have already 
been calculated, can be stored in a static array . A static method can be 
used to extract the already pre -calculated value. Since the preliminary 
calculations are being performed in the static co nstructor, if the class for pre -
calculated square roots is not used, they will not be executed and CPU time 
and memory will be saved. 
This is how the implementation might look like: 
```csharp
static class SqrtPrecalculated 
{ 
 public const int MaxValue = 1000; 
 
 // Static field 
 private static int[] sqrtValues; 
 
 // Static constructor 
 static SqrtPrecalculated() 
 { 
  sqrtValues = new int[MaxValue + 1]; 
  for (int i = 0; i < sqrtValues.Length; i++) 
  { 
   sqrtValues[i] = (int)Math.Sqrt(i); 
  } 
 } 
 
 // Static method 
 public static int GetSqrt(int value) 
 { 
  if ((value < 0) || (value > MaxValue)) 
  { 
   throw new ArgumentOutOfRangeException(String.Format( 

    "The argument should be in range [0...{0}].", 
    MaxValue)); 
  } 
  return sqrtValues[value]; 
 } 
} 
 
class SqrtTest 
{ 
 static void Main() 
 { 
  Console.WriteLine(SqrtPrecalculated.GetSqrt(254)); 
  // Result: 15 
 } 
}
```