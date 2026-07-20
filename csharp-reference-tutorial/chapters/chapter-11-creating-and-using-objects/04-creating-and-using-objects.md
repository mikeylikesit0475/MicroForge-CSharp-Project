# Creating and Using Objects

For now we are going to focus on creating and using objects  in our 
programs. We are going to work with already defined cla sses and mostly with 
system classes from .NET Framework. The specificities of defining our own 
classes we are going to consider later in the chapter "Defining Classes".

## Creating and Releasing Objects

The creat ion of objects from preliminarily defined classes during program 
execution is performed by the operator new. The newly created object is 
usually assigned to the variable from type coinciding with the class of the 

object (this, however, is not mandatory – read chapter " Principles of Object -
Oriented Programming "). We are going to note that in this assignment the 
object is not copied, and only a reference to the newly created object is 
recorded in the v ariable (its address in the memory). Here is a simple 
example of how it works: 
Cat someCat = new Cat(); 
The variable someCat of type Cat we assign the newly created instance of 
the class Cat. The variable someCat remains in the stack, and its value (the 
instance of the class Cat) remains in the managed heap:

### Creating Objects with Set Parameters

Now we are going to consider a slightly different variant of the example above 
in which we set parameters when creating the object: 
Cat someCat = new Cat("Johnny", "brown"); 
In this case we would like the objects someCat to represent a cat whose name 
is "Johnny" and is brown. We indicate this by using the words " Johnny" and 
"brown", written in the brackets after the name of the class. 
When creating an object with the operator new, two things happen: memory is 
set aside for this object and its data members are initialized. The 
initialization is performed by a special method called constructor. In the 
example above the initializing parame ters are actually parameters of the 
constructor of the class. 
We are going to discuss constructors after a while. As the member variables 
name and color of the class Cat are of reference type (of the class String), 
they are also recorded in the dynamic memory ( heap) and in the object 
itself are kept their references (addresses / pointers). 
The following figure illustrates how the Cat object is represented in the 
computer memory (arrows illustrated the references from one object to 
another): 
HeapStack
Cat@6e278a
someCat
(Cat members)

### Releasing the Objects

An important feature of working with objects in C# is that usually there is no 
need to manually destroy them and release the memory taken up by them. 
This is possible because of the embedded in .NET CLR system for cleaning the 
memory ( garbage colle ctor) which takes care of releasing unused objects 
instead of us. Objects to which there is no reference in the program at certain 
moment are automatically released and the memory they take up is 
released. This way many potential bugs and problems are prev ented. If we 
would like to manually release a certain object, we have to destroy the 
reference to it, for example this way: 
someCat = null; 
This does not destroy the object immediately, but puts it in a state in which it 
is inaccessible to the program and  the next time the garbage collector cleans 
the memory it is going to be released: 
 
HeapStack
Cat@6e278a
someCat name:
String@a272e8
color:
String@852fa4
Johny
brown
HeapStack
Cat@6e278a
someCat name:
String@a272e8
color:
String@852fa4
Johny
brown

## Access to Fields of an Object

The access to the fields  and properties of a given object is done by the 
operator . (dot) placed between the names of the object and the name  of 
the field (or the property). The operator . is not necessary in case we access 
field or property of given class in the body of a method of the same class. 
We can access the fields and the properties either to extract data from 
them, or to assign new da ta. In the case of a property the access is 
implemented in exactly the same way as in the case of a field – C# give us 
this ability. This is achieved by the keywords get and set in the definition of 
the property, which  perform respectively extraction of the value of the 
property and assignment of a new value. In the definition of the class Cat 
(given above) the properties are Name and Color.

### Access to the Memory and Properties of an Object – Example

We are going to give  an example of using a property of an object, as well as 
```csharp
using the already defined above class Cat. We create an instance myCat of the 
class Cat and assign "Alfred" to the property Name. After that we print on 
the standard output a formatted string with th e name of our cat. You can see 
```
an implementation of the example: 
```csharp
class CatManipulating 
{ 
 static void Main() 
 { 
  Cat myCat = new();
  myCat.Name = "Alfred"; 
 
  Console.WriteLine("The name of my cat is {0}.", 
   myCat.Name); 
 } 
}
```

## Calling Methods of Objects

Calling the methods of a given object is done through the invocation 
operator () and with the help of the operator . (dot). The operator dot is 
not obligatory only in case the method is called in the body of another method 
of the same class.  Calling a method is performed by its name followed by () 
or (<parameters>) for the case when we pass it some arguments. We 
already know how to invoke methods from the chapter "Methods". 
Now is the moment to mention the fact that methods of classes have access 
modifiers public, private or protected with which the ability to call them 
could be restricted. We are going to consider these modifiers in the chapter  
"Defining Classes ". For now it enough to know that the access modifier 

public does not introduce any restrictions for calling the method, i.e. makes 
it publicly available.

### Calling Methods of Objects – Example

We are going to complement the  example we already gave as we call the 
method SayMiau of the class Cat. Here is the result: 
```csharp
class CatManipulating 
{ 
 static void Main() 
 { 
  Cat myCat = new();
  myCat.Name = "Alfred"; 
 
  Console.WriteLine("The name of my cat is {0}.",myCat.Name); 
  myCat.SayMiau(); 
 } 
} 
```
After executing the program above the following text is going to be printed on 
the standard output: 
The name of my cat is Alfred. 
Cat Alfred said: Miauuuuuu!

## Constructors

The constructor is a special method of the class, which  is called 
automatically when creating an object  of this class, and performs 
initialization of its data (this is its purpose). The constructor has no type of 
returned value and its name is not random, and mandatorily coincides with 
the class name. The cons tructor can be with or without parameters . A 
constructor without parameters is also called parameterless constructor.

### Constructor with Parameters

The constructor can take parameters as well as any other method. Each 
```csharp
class can have different count of constr uctors with one only restriction – the 
```
count and type of their parameters have to be different  (different signature). 
When creating an object of this class, one of the constructors is called. 
In the presence of several constructors in a class naturally occurs the question 
which of them is called when the object is created. This problem is solved in a 
very intuitive way as with methods. The appropriate constructor is chosen 
automatically by the compiler according to the given set of parameters when 
creating the object. We use the principle of the best match.

### Calling Constructors – Example

Lets' take a look again at the definition of the class Cat and more particularly 
at the two constructors of the class: 
```csharp
public class Cat 
{ 
 // Field name 
 private string name; 
 // Field color 
 private string color; 
 
```
 … 
 
 // Parameterless constructor 
```csharp
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
 
```
 … 
} 
We are going to use these constructors to illustrate the usage of constructors 
with and without parameters. For the class Cat defined that way we are going 
to give an example of creating its instances by each of the two constructors. 
One of the objects is going to be an ordinary undefined cat, and the other – 
our brown cat Johnny. After that we are going to execute the method SayMiau 
for each of the cats and analyze the result. Source code follows: 
```csharp
class CatManipulating 
{ 
 static void Main() 
 { 
  Cat someCat = new();
 
  someCat.SayMiau(); 
  Console.WriteLine("The color of cat {0} is {1}.", 

   someCat.Name, someCat.Color); 
 
  Cat someCat = new("Johnny", "brown");
 
  someCat.SayMiau(); 
  Console.WriteLine("The color of cat {0} is {1}.", 
   someCat.Name, someCat.Color); 
 } 
} 
```
As a result of the program ’s execution the following text is printed on the 
standard output: 
Cat Unnamed said: Miauuuuuu! 
The color of cat Unnamed is gray. 
Cat Johnny said: Miauuuuuu! 
The color of cat Johnny is brown.

## Static Fields and Methods

The data members, which we considered up until,  now implement states of 
the objects and are directly related  to specific instances of the classes. In 
OOP there are special categories fields and methods, which  are associated 
with the data type (class), and not with the specific instance (object). We call 
them static members  because are independent of concrete obj ects. 
Furthermore, they are used without the need of creating an instance of the 
class in which they are defined. They can be fields, methods and constructors. 
Let’s consider shortly static members in C#. 
A static field or method  in a give n class is defined with the key word 
static, placed before the type of the field or the type of returned value of the 
method. When defining a static constructor, the word static is placed before 
the name of the constructor. Static constructors are not going to be disc ussed 
in this chapter – for now we are going to consider only static fields and 
methods (the more curious readers can look up in MSDN).

### When to Use Static Fields and Methods?

To find the answers of this question we have to understand very well the 
difference between static and non -static members. We are going to consider 
into details what it is. 
We have already explained the main difference between the two types of 
members. Let ’s interpret the class as a category of objects , and the 
object as a repr esentative of this category . Then the static members 
reflect the state and the behavior of the category itself, and the non -static the 
state and the behavior of the separate representatives of the category. 

Now we are going to pay special attention to the initialization of static and 
non-static fields. We already know that non -static fields are initialized with 
the call to the constructor of the class when creating an instance of it – either 
inside the body of the constructor, or outside. However, the initi alization of 
```csharp
static fields cannot be performed when the object of the class is created, 
```
because they can be used without a created instance of the class. It is 
important to know the following: 
 
Static fields are initialized when the data type (the class) is 
used for the first time, during the execution of the program. 
Now we shall see how to use static fields and methods in practice.

### Static Fields and Methods – Example

The example, which we are going to give,  solves the following simple 
problem: we need a  method that every time returns a value greater with one 
than the value returned at the previous call of the method. We choose the 
first returned value to be 0. Obviously this method generates the sequence of 
natural number. Similar functionality is widely  used in practice, for example, 
for uniform numbering of objects. Now we are going to see how this could be 
implemented with the means of OOP. 
Let’s assume that the method is called NextValue() and is defined in a class 
called Sequence. The class has a fie ld currentValue from type int, which 
contains the last returned value by the method. We would like the following 
two actions to be performed consecutively in the method body: the value of 
the field to be increased and its new value to be returned as a resu lt. 
Obviously the returned by the method value does not depend on the concrete 
instance of the class Sequence. For this reason the method and the field are 
```csharp
static. You can now see the described implementation of the class: 
public class Sequence 
{ 
 // Static field, holding the current sequence value 
 private static int currentValue = 0; 
 
 // Intentionally deny instantiation of this class 
 private Sequence() 
 { 
 } 
 
 // Static method for taking the next sequence value 
 public static int NextValue() 
 { 
  currentValue++; 
  return currentValue; 


 } 
} 
The observant reader has noticed that the so defined class has a default 
```
constructor, which is declared as private. This usage of a constructor may 
seem strange, but is quite deliberate. It is good to know the following: 
 
A class that has only private constructors cannot be 
instantiated. Such class usually has only static members and 
is called "utility class". 
For now we are not going to go into detail s about the access modifiers  
public, private and protected. We shall explain  them comprehensively in 
the chapter "Defining Classes". 
Let’s take a look at a simple program, which uses the class Sequence: 
```csharp
class SequenceManipulating 
{ 
 static void Main() 
 { 
  Console.WriteLine("Sequence[1...3]: {0}, {1}, {2}", 
```
   Sequence.NextValue(), Sequence.NextValue(), 
   Sequence.NextValue()); 
 } 
} 
The example prints on the standard output the first three natural numbers by 
triple consecutive call of the method NextValue() of the class Sequence. The 
result from this code is the following: 
Sequence[1...3]: 1, 2, 3 
If we try to create several different sequences, as the constructor of the class 
Sequence is declared private, we are going to get compile time error.

## Examples of System C# Classes

After we got acquainted with the basic functionality of objects, we are going 
to consider briefly several commonly used system classes  from the 
standard library of .NET Framework. This way we are going to see in practice 
the s o far explained material, and also show how system classes ease our 
every-day work.

### The System.Environment Class

We start with one of the basic system classes in .NET Framework : 
System.Environment. It contains a set of useful fields and methods, which 


ease getting information about the hardware and the operating system, and 
some of them, give the ability to interact with the program environment. Here 
is a part of the functionality provided by this class: 
- Information about the processors count, the computer network name, 
the version of the operating system, the name of the current user, the 
current directory, etc. 
- Access to externally defined properties and environment variables, 
which we are not going to consider in this book. 
Now we are going to show one in teresting application of a method of the class 
Environment, which is commonly used in practice when developing programs 
with critical fast performance. We are going to detect the time needed for the 
execution of the source code with the help of the propert y TickCount. Here it 
is how it works: 
```csharp
class SystemTest 
{ 
 static void Main() 
 { 
  int sum = 0; 
  int startTime = Environment.TickCount; 
 
  // The code fragment to be tested 
  for (int i = 0; i < 10000000; i++) 
  { 
   sum++; 
  } 
 
  int endTime = Environment.TickCount; 
  Console.WriteLine("The time elapsed is {0} sec.", 
   (endTime - startTime) / 1000.0); 
 } 
} 
The static property TickCount of the class Environment returns as a result 
```
the count of milliseconds that have passed since the computer is on until the 
time of the method call. With its help we detect the milliseconds past before 
and after the execution of the source code. Their difference is the wanted 
time for the execution of the fragment source code measured in milliseconds. 
As a result of the execution of the program on the standard output we print 
the result of the following type (the measured time varies according to the 
current computer configuration and its load): 
The time elapsed is 0.031 sec. 

In the example we have used two static members of two system classes: the 
static property Environment.TickCount and the static method Console. 
WriteLine(…).

### The System.String Class

We have already met the String (System.String) class of .NET Framework, 
which represents strings. Let ’s recall that we can think of strings as a 
primitive data type in C#, although the work with them is different from the 
work with different primitive data types (integers, floating point numbers, 
Boolean variables, etc.). We are going to describe them in detail s in the 
chapter "Strings and Text Processing".

### The System.Math Class

The System.Math class contains methods for performing basic numeric and 
mathematical operations such as raising a number to a power, taking a 
logarithm and square root, and some trigonometric functions. We are going to 
give a simple example, which illustrates its usage. 
We want to make a program, which calculates the area of a triangle by given 
two sides and an angle between them in degrees. Therefore we need the 
method Sin(…) and the constant PI of the class Math. With the help of the  π 
number we can easily convert to radians the entered in degrees angle. You 
can see an example implementation of the described logic: 
```csharp
class MathTest 
{ 
 static void Main() 
 { 
  Console.WriteLine("Length of the first side:"); 
  double a = double.Parse(Console.ReadLine()); 
  Console.WriteLine("Length of the second side:"); 
  double b = double.Parse(Console.ReadLine()); 
  Console.WriteLine("Size of the angle in degrees:"); 
  int angle = int.Parse(Console.ReadLine()); 
 
  double angleInRadians = Math.PI * angle / 180.0; 
  Console.WriteLine("Area of the triangle: {0}", 
   0.5 * a * b * Math.Sin(angleInRadians)); 
 } 
} 
We can easily test the program if we check whether it calculates correctly the 
```
area of an equilateral triangle . For further convenience we choose the 
length of the side to be 2 – then we find the area with the well -known 
formula: 

...7320508,1324
3 2 S
 
We enter consecutively the numbers 2, 2, 60 and on the standard output we 
can see: 
Face of the triangle: 1.73205080756888 
Depending on your system localization (Region and Language Settings) your 
output might be " 1,73205080756888" or "1.73205080756888". You might fix 
the decimal point to "." by this line of code, executed at your program start: 
System.Threading.Thread.CurrentThread.CurrentCulture = 
 System.Globalization.CultureInfo.InvariantCulture;

### The System.Math Class – More Examples

As we already saw, apart from mathematical methods, the Math class also 
defines two well known in mathematics constants: the trigonometric constant 
π and the Euler’s number e. Here is an example with them: 
```csharp
Console.WriteLine(Math.PI); 
Console.WriteLine(Math.E); 
When executing the code above, we get the following output: 
```
3.141592653589793 
2.718281828459045

### The System.Random Class

Sometimes in programming we have to use random numbers. For instance, 
we would like to generate 6 random numbers in the range 1 to 49 (not 
necessarily unequal). This could be done by using the System.Random class 
and its method  Next(). Before we use the Random class we have to create 
instance of it, at which point it is initialized with a random value (derived from 
the current system time in the operating system). After that we can randomly 
generate a number in the range [0…n) by calling the method Next(n). Notice 
that this method can return zero, but always r eturns a random number 
smaller than the set value n. Therefore, if we would like to get a number in 
the range [1…49], we have to use the expression Next(49) + 1. 
Below is an example source code of a program, which  generates 6 random 
numbers in the range from 1 to 49 by using the Random class (note that it is 
not guaranteed that the numbers are unique like in the classical Bulgarian 
lottery TOTO 6/49): 

```csharp
class RandomNumbersBetween1And49 
{ 
 static void Main() 
 { 
  Random rand = new();
  for (int number = 1; number <= 6; number++) 
  { 
   int randomNumber = rand.Next(49) + 1; 
   Console.Write("{0} ", randomNumber); 
  } 
 } 
} 
```
Here is how a possible output of the program looks like: 
16 49 7 29 1 28

### The System.Random Class – Generating a Random Password

To show you how useful the random numbers generator  in .NET 
Framework can be, we are going to set as a task to generate a random 
password which is between 8 and 15 characters long, contains at least two 
capital letters, at least two small letters, at least one digit and at least three 
special chars. For this purpose we are going to use the following algorithm: 
1. We start with an empty password. We create a generator of random 
numbers. 
2. We generate twice a rand om capital letter and place it at a random 
position in the password. 
3. We generate twice a random small letter and place it at a random 
position in the password. 
4. We generate twice a random digit and place it at a random position in 
the password. 
5. We generate three times a random special character and place it at a 
random position in the password. 
6. Until this moment the password should consist of 8 characters. In order 
to supplement it to 15 characters at most , we can insert random count 
of times (between 0 and 7) at a random position in the password a 
random character (a capital letter, a small letter or a special char). 
An implementation of the described algorithm is given below: 
```csharp
class RandomPasswordGenerator 
{ 
 private const string CapitalLetters = 

  "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; 
 private const string SmallLetters = 
  "abcdefghijklmnopqrstuvwxyz"; 
 private const string Digits = "0123456789"; 
 private const string SpecialChars = 
  "~!@#$%^&*()_+=`{}[]\\|':;.,/?<>"; 
 private const string AllChars = 
  CapitalLetters + SmallLetters + Digits + SpecialChars; 
 
 private static Random rnd = new Random(); 
 
 static void Main() 
 { 
  StringBuilder password = new();
 
  // Generate two random capital letters 
  for (int i = 1; i <= 2; i++) 
  { 
   char capitalLetter = GenerateChar(CapitalLetters); 
   InsertAtRandomPosition(password, capitalLetter); 
  } 
 
  // Generate two random small letters 
  for (int i = 1; i <= 2; i++) 
  { 
   char smallLetter = GenerateChar(SmallLetters); 
   InsertAtRandomPosition(password, smallLetter); 
  } 
 
  // Generate one random digit 
  char digit = GenerateChar(Digits); 
  InsertAtRandomPosition(password, digit); 
 
  // Generate 3 special characters 
  for (int i = 1; i <= 3; i++) 
  { 
   char specialChar = GenerateChar(SpecialChars); 
   InsertAtRandomPosition(password, specialChar); 
  } 
 
  // Generate few random characters (between 0 and 7) 
  int count = rnd.Next(8); 
  for (int i = 1; i <= count; i++) 
  { 

   char specialChar = GenerateChar(AllChars); 
   InsertAtRandomPosition(password, specialChar); 
  } 
 
  Console.WriteLine(password); 
 } 
 
 private static void InsertAtRandomPosition( 
  StringBuilder password, char character) 
 { 
  int randomPosition = rnd.Next(password.Length + 1); 
  password.Insert(randomPosition, character); 
 } 
 
 private static char GenerateChar(string availableChars) 
 { 
  int randomIndex = rnd.Next(availableChars.Length); 
  char randomChar = availableChars[randomIndex]; 
  return randomChar; 
 } 
} 
```
Let’s explain several unclear moments in the source code. Let ’s start from the 
definition of the constants: 
```csharp
private const string CapitalLetters = 
 "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; 
private const string SmallLetters = 
 "abcdefghijklmnopqrstuvwxyz"; 
private const string Digits = "0123456789"; 
private const string SpecialChars = 
 "~!@#$%^&*()_+=`{}[]\\|':;.,/?<>"; 
private const string AllChars = 
 CapitalLetters + SmallLetters + Digits + SpecialChars; 
```
Constants in C# are immutable variables whose values are assigned during 
their initialization in the source code of the program and after that they 
cannot be changed. They are declared with the modifier const. They are used 
for defining a number or a string, which afterwards is used many times in the 
program. This way repetition of certain values in the code is avoided and 
these values can be easily altered by changing only one place in the code. For 
example, if in a certain moment we decide that the character "," (c omma) 
should not be used when generating a password, we can change only one row 
in the program (the corresponding constant) and the change is going to 
reflect on every row where the constant is being used. In C# constants are 

written in Pascal Case (the wo rds in the name, merged together, each of 
them starts with an uppercase letter, and the rest of them are lowercase).  
More about constants we will learn in the section " Constants" in the chapter 
"Defining Classes". 
Let’s explain how the other parts of the program work. In the beginning, as a 
```csharp
static member variable in the class RandomPasswordGenerator is created the 
```
random number generator rnd. As this variable rnd is defined in the c lass 
(not in the Main() method), it is accessible by the whole class (by each of its 
methods), and as it is defined static, it is accessible by the static methods, 
too. Thus, anywhere the program needs a random integer variable the same 
random number gener ator is used. It is initialized when the class 
RandomPasswordGenerator is loaded. 
The method GenerateChar() returns a randomly chosen character in a set of 
characters given as a parameter. It works very simply: it chooses a random 
position in the set of characters (between 0 and the count of characters minus 
1) and returns the characters at this position. 
The method InsertAtRandomPosition() is not complicated too. It chooses a 
random position in the StringBuilder object, which is passed and inserts on 
this position the returned character. We are going to pay special attention to 
the class StringBuilder in the chapter "Strings and Text Processing". 
Here is a sample output of the program for generating passwords, which we 
just considered (this output is different at each program run due to its 
randomness by nature): 
8p#Rv*yTl{tN4