# Constructors

In object-oriented programming, when creating an object from a given class, 
it is necessary to call a special method of the class known as a constructor.

## What Is a Constructor?

Constructor of a class is a pseudo -method, which does not have a return 
type, has the name of the class and is called using the keyword  new. The 
task of the constructor is to initialize the memor y, allocated for the object, 
where its fields will be stored (those which are not static ones).

### Calling a Constructor

The only one way to call a constructor  in C# is through the keyword new. 
It allocates memory for the new object (in the stack or in the he ap, depending 
on whether the object is a value type or a reference type), resets its fields to 
zero, calls their constructors (or chain of constructors, formed in succession), 
and at the end returns a reference to the newly created object. 
Consider an example, which will clarify how the constructor works. We know 
from chapter "Creating and Using Objects" how to create an object: 
Dog myDog = new Dog();  
In this case by using the keyword new we call the constructor of the class Dog 
and by doing this, memory is allocated, needed for the newly created object 
of the Dog type. When it comes to classes they are allocated in the dynamic 
memory (in the so called "managed heap"). 
Let’s follow the process of calling a constructor during the creation of new 
object step by step. 
First, memory is allocated for the object: 

 
Next, its fields (if any) are initialized with the default values  for their 
respective types: 
 
If the creation of the new object is succe ssfully completed, the constructor 
returns a reference to it, which is assigned to the variable myDog, from class 
type Dog:

## Declaring a Constructor

If we have the class Dog, here is how its most simplified constructor  (without 
parameters) will look like: 
```csharp
public Dog() 
{ 
} 
```
Formally, the declaration of the constructor appears in the following way: 
[<modifiers>] <class_name>([<parameters_list>]) 
As we already know, the constructors are similar to methods, but they do not 
have a return type (therefore we called them pseudo-methods).

### Constructor’s Name

In C# it is mandatory that the name of every constructor matches the 
name of the class in which it resides  – <class_name>. In the example 
above the name of the constructor is the same as the na me of the class – 
Dog. We should know that, as with methods, the name of the constructor is 
always followed by round brackets – "(" and ")". 
In (C#) it is not allowed to declare a method whose name matches the 
name of the class  (hence the name of the constructors). If nevertheless, a 
method is declared with the class name, this will cause a compilation error. 
```csharp
public class IllegalMethodExample 
{ 
 // Legal constructor 
 public IllegalMethodExample () 
 { 
 } 
 
 // Illegal method 
 private string IllegalMethodExample() 
 { 
  return "I am illegal method!"; 
 } 
} 
When we try to compile this class the compiler will display the following  
```
compilation error message: 
SampleClass: member names cannot be the same as their enclosing 
type

### Parameter List

Similar to the methods, if we need extra data to create an object, the 
constructor gets it through a parameter list – <parameters_list>. In the 
example constructor of the class Dog there is no need of additional data to 
create an object of t his type and therefore there is no parameter list. More 
about the parameter list will be explained in one of the later sections –
"Declaring a Constructor with Parameters". 
Of course, after the declaration of the constructor its body is following, which 
is like every method body in C#, but generally contains mostly initialization 
logic, i.e. setting the initial values of the fields of the class.

### Modifiers

It is evident that modifiers can be added in the declaration  of the 
constructors – <modifiers>. For modifiers that we know and which  are not 
access modifiers, i.e.  const and static, we should know that only const is 
not allowed to be used in constructors. Later in this chapter, in the " Static 
Constructors" section we will learn more about the constructors declared with 
modifier static.

## Visibility of the Constructors

Similar to the methods and the fields, the constructors can be declared with 
levels of visibility : public, protected, internal, protected, internal 
and private. The access levels protected and protected internal will be 
explained in chapter "Object-Oriented Programming Principles ". The rest of 
the access levels have the same meaning and behavior as with fields and 
methods.

## Initialization of the Fields in the Constructor

As explained earlier when creating a new object and calling its constructor, a 
new memory is allocated for the non-static fields of the object of the class and 
they are initialized with the default values  for their types (see the section 
"Calling a Constructor"). 
Furthermore, through the constructors we mainly initialize the f ields of the 
class with values set by us and not with the default ones. 
E.g., in the examples we discussed so far, the field name of the object from 
type Dog is always initialized during its declaration: 
string name = "Sharo"; 
Instead of doing this during the declaration of the field, a better programming 
style is to assign its value in the constructor: 
```csharp
public class Dog 

{ 
 private string name; 
 
 public Dog() 
 { 
  this.name = "Sharo"; 
 } 
 
 // … The rest of the class body … 
} 
```
Although we initialize the fields in the constructor, some people recommend 
explicitly assigning their type ’s default values  during initialization with 
the purpose of improving the readability of the code, but it is a matter of 
personal choice: 
```csharp
public class Dog 
{ 
 private string name = null; 
 
 public Dog() 
 { 
  this.name = "Sharo"; 
 } 
 
 // … The rest of the class body … 
}
```

### Fields Initialization in the Constructor

Let’s see in details what the constructor does after being called and the class 
fields have been initialized in its body. We know that, when called, it will 
allocate memory  for each field and this memory will be initialized  with 
the default values. 
If the field s are of primitive type, then after the default values, we shall 
assign new values. 
In case the fields are from reference type, such as our field name, the 
constructor will initialize them with null. It will then create the object of the 
corresponding type, in this case the string "Sharo" and at the end a reference 
will be assigned to the new object in the respective field, in our case the field 
name. 
The same will happen if we have other fields, which are not primitive types, 
and then initialize them in the constructor. E.g. let’s have a class called Collar, 
which describes a dog’s accessory – Collar: 

```csharp
public class Collar 
{ 
 private int size; 
 
 public Collar() 
 { 
 } 
} 
Let our class Dog has a field called collar, which is from type Collar and 
```
which is initialized in the constructor of the class: 
```csharp
public class Dog 
{ 
 private string name; 
 private int age; 
 private double length; 
 private Collar collar; 
 
 public Dog() 
 { 
  this.name = "Sharo"; 
  this.age = 3; 
  this.length = 0.5; 
  this.collar = new Collar(); 
 } 
 
 static void Main() 
 { 
  Dog myDog = new();
 } 
}
```

### Representation in the Memory

Let’s follow the steps through which the constructor goes, after being called in 
the Main() method. 
As we know , as a first step  it will allocate memory in the heap  for all the 
fields and will initialize them with their default values: 

 
Then, the constructor will have to ensure the creation of the object for the 
field name. It will call the constructor of the class string, which will do the 
work on the string creation): 
 
Now the constructor will keep the reference to the new string in the field name 
of the Dog object: 


 
Then is the creation of the object from type Collar. Our constructor (of the 
```csharp
class Dog) calls the constructor of the class Collar, which allocates memory 
for the object: 
 
Next, the constructor will initialize it with the default value  for the 
```
respective type . The size of the Collar is not explicitly assigned so it will 
take the default value for its type (0 for int): 


 
After that the reference to the newly created object, which the constructor of 
the class Collar returns as a result, will be assigned to the field collar: 
 
Finally, the reference to the new object from type Dog will be assigned to 
the local variable myDog in the method Main():

### Order of Initialization of the Fields

To avoid confusion, let’s explain the order in which the fields of a class 
are initialized regardless of whether we have assigned to them values and / 
or initialized them in the constructor. 
First memory is allocated  for the respective field in the heap and this 
memory is initialized with the default value of the field type. E.g. let’s again 
consider the example with the class Dog: 
```csharp
public class Dog 
{ 
 private string name; 
 
 public Dog() 
 { 
  Console.WriteLine( 
   "this.name has value of: \"" + this.name + "\""); 
  // … No other code here … 
 } 
 // … Rest of the class body … 
} 
When we try to create a new object of our class type the console will show: 


```
this.name has value of: ""  
After the initialization of the fields with the default value for the respective 
type, the second step in CLR (Common Language Runtime) is to assign a 
value to the field if such has been set when declaring the field. 
So, if we change the line in the class Dog, where we declare the field name, it 
will first be initialized with the value null and then it will be assigned the 
value "Rex". 
```csharp
private string name = "Rex"; 
Respectively, for every creation of a new object of the class: 
static void Main() 
{ 
 Dog dog = new();
} 
```
The following will be printed: 
this.name has value of: "Rex" 
Only after these two steps of initializing the fields of the class (default value 
initialization and possibly the value set by the programmer during the 
declaration of the field) the constructor of the class is called . At this time, 
the fields get the values, which are set in the body of the constructor.

## Declaring a Constructor with Parameters

In the previous section, we saw how we can set values to the fields, other 
than the default values. Very often, however, during the declaration of the 
constructor, we don ’t know what values the various fields will take. To tackle 
this problem, the required information, similar to the methods with 
parameters, the fields are assigned th e values, given to them in the body of 
the constructor. For example: 
```csharp
public Dog(string dogName, int dogAge, double dogLength) 
{ 
 name = dogName; 
 age = dogAge; 
 length = dogLength; 
 collar = new Collar(); 
} 

```
Similarly, the call of a constructor with parame ters is done in the same 
way as the call of method with parameters – the required values are supplied 
as a list, the elements of which are separated with commas: 
```csharp
static void Main() 
{ 
 Dog myDog = new Dog("Moby", 2, 0.4); // Passing parameters 
 
 Console.WriteLine("My dog " + myDog.name + 
```
  " is " + myDog.age + " year(s) old. " + 
  " and it has length: " + myDog.length + " m."); 
} 
The result of the execution of this Main() method is the following: 
My dog Moby is 2 year(s) old. It has length: 0.4 m. 
There is no limitation for the number of the constructors of a class in C#. The 
only requirement is that they differ in their signature (what signature is we 
already explained in chapter "Methods").

### Scope of Parameters of the Constructor

By analogy with the scope of the variables in the parameter list of a method, 
the variables in the parameter list of one constructor have a scope 
from the opening bracket of the constructor to the closing bracket, i.e. 
throughout the body of the constructor. 
Very often, when we declare a constructor with parameters it is possible to 
name the variables from the parameter list with the same names  as the 
names of the fields, which are going to be initialized. Let ’s, for example, 
consider the constructor of the class Dog: 
```csharp
public Dog(string name, int age, double length) 
{ 
 name = name; 
 age = age; 
 length = length; 
 collar = new Collar(); 
} 
```
Let’s compile and execute the Main() method declared a little bit above: 
My dog is 0 year(s) old. It has length: 0 m 
Strange result, isn ’t it? In fact this result is not so awkward. The explanation 
is the following: the scope, in which the variables from the list of the 
constructor parameters are acting, overlaps the scope of acting of the fields 

with the same names in the constructor. Thus,  we do not assign any value 
to the fields because in practice we have no access to them. For example, 
instead of assigning the variable value to the field age, we assign the value of 
the variable age to the variable itself: 
age = age; 
As we saw from the s ection "Hiding Fields with Local Variables ", to avoid this 
problem we should access the field, to which we want to assign a value, 
```csharp
using the keyword this: 
public Dog(string name, int age, double length) 
{ 
 this.name = name; 
 this.age = age; 
 this.length = length; 
 this.collar = new Collar(); 
} 
```
Now, assuming we execute again the Main() method: 
```csharp
static void Main() 
{ 
 Dog myDog = new("Moby", 2, 0.4);
 
 Console.WriteLine("My dog " + myDog.name + 
```
  " is " + myDog.age + " year(s) old. " + 
  " and it has length: " + myDog.length + " m"); 
} 
The result will be exactly what we expect it to be: 
My dog Moby is 2 year(s) old. It has length: 0.4 m

## Constructor with Variable Number of Arguments

Similar to methods with variable number of arguments , discussed in  
chapter "Methods", constructors can also be declared with a parameter for a 
variable number of arguments. The rules for declaring and calling constructors 
with a variable number of arg uments are the same as the ones, described for 
declaring and calling with the methods: 
1. When we declare a constructor with variable number of arguments, we 
must use the reserved word params, and then insert the type of the 
parameters, followed by square par entheses. Finally the name of the 
array follows, in which array the arguments used for the calling of the 

method are stored. For example for whole number arguments we can 
use params int[] numbers. 
2. It is allowed for the constructor with a variable number of  arguments to 
have other parameters too in the parameter list. 
3. The parameter for the variable number of arguments must be the last in 
the parameter list of the constructor. 
Consider a sample declaration of a constructor of a class, which describes a 
lecture: 
```csharp
public Lecture(string subject, params string[] studentsNames) 
{ 
 // … Initialization of the instance variables … 
} 
```
The first parameter in the declaration is the name of the subject of the lecture 
and the next parameter represents a variable number of arguments – the 
names of the students. Here is how a sample object of this class would be 
constructed: 
Lecture lecture = 
 new Lecture("Biology", "Peter", "Mike", "Steven"); 
Accordingly, as the first parameter is the name of the subject – "Biology", 
and all the rest arguments – the names of the attending students.

## Constructor Overloading

As we saw, we can declare constructors with parameters. This gives us a 
possibility to declare constructors with different signatures (number and order 
of the parameters)  with the purpose of providing convenience to those who 
will create objects from our class. Creating constructors with different 
signatures is called constructor overloading. 
Consider, for example, the class Dog. We can declare different constructors: 
// No parameters 
```csharp
public Dog() 
{ 
 this.name = "Axl"; 
 this.age = 1; 
 this.length = 0.3; 
 this.collar = new Collar(); 
} 
 
// One parameter 

public Dog(string name) 
{ 
 this.name = name; 
 this.age = 1; 
 this.length = 0.3; 
 this.collar = new Collar(); 
} 
 
// Two parameters 
public Dog(string name, int age) 
{ 
 this.name = name; 
 this.age = age; 
 this.length = 0.3; 
 this.collar = new Collar(); 
} 
 
// Three parameters 
public Dog(string name, int age, double length) 
{ 
 this.name = name; 
 this.age = age; 
 this.length = length; 
 this.collar = new Collar(); 
} 
 
// Four parameters 
public Dog(string name, int age, double length, Collar collar) 
{ 
 this.name = name; 
 this.age = age; 
 this.length = length; 
 this.collar = collar; 
}
```

### Reusing Constructors

In our last example we saw that, depending on the needs for creating objects 
of our class, we can declare different variants of the constructors. It is easy to 
notice that a large part of the constructor code is repeated . This leads us 
to the question whether there is an alternative way for a constructor, which is 
already doing an initializing, to be reused by the others to perform the same 
initialization. On the other hand, at the beginning of the chapter it was 
mentioned that a constructor cannot be calle d in the manner in which the 

methods are called but by the keyword new. There should be a way – 
otherwise a lot of code will be repeated unnecessarily. 
In C# a mechanism exists through which one constructor can call another  
one declared in the same class. This is done again with the keyword this, but 
used in another syntax structure in declaring the constructors: 
[<modifiers>] <class_name>([<parameters_list_1>]) 
 : this([<parameters_list_2>]) 
To the well -known form of declaring a constructor (the first line of the 
declaration above), we can add a colon, followed by the keyword this, 
followed by parentheses. If the constructor we want to call has parameters, in 
the brackets we need to add a list  of parameters parameters_list_2 to be 
supplied. 
Here is how the code from the section about constructor overloading  would 
look like, in which instead of repeating the initialization of each of the fields, 
we will call the constructors declared in the same class: 
// No parameters 
public Dog() 
 : this("Axl") // Constructor call 
```csharp
{ 
 // More code could be added here 
} 
 
// One parameter 
public Dog(string name) 
```
 : this(name, 1) // Constructor call 
```csharp
{ 
} 
 
// Two parameters 
public Dog(string name, int age) 
```
 : this(name, age, 0.3) // Constructor call 
```csharp
{ 
} 
 
// Three parameters 
public Dog(string name, int age, double length) 
 : this(name, age, length, new Collar()) // Constructor call 
{ 
} 
 
// Four parameters 

public Dog(string name, int age, double length, Collar collar) 
{ 
 this.name = name; 
 this.age = age; 
 this.length = length; 
 this.collar = collar; 
} 
As indicated by comments in the first constructor in the example above, if 
```
necessary, in addition to calling any of the other constructors with certain 
parameters, every constructor can add into its body a code, which performs 
additional initializations or other actions.

## Default Constructor

Consider the following question – what happens if we don ’t declare a 
constructor in our class? How can we create objects from this type? 
As it often happens, when a class is without a single constructor, this issue is 
resolved by C#. When we do not declare any constructors, the compiler will 
create one for us and this one will be used to create objects such as our class. 
This constructor is called  default implicit constructor  and it will not have 
any parameters and will b e empty (i.e. it will not do anything in addition to 
the default zeroing of the object fields). 
 
When we do not declare any constructor in a given class, the 
compiler will create one, known as a default implicit 
constructor. 
For example, let’s declare the class Collar, without declaring any constructor 
in it: 
```csharp
public class Collar 
{ 
 private int size; 
 
 public int Size 
 { 
  get { return size; } 
 } 
} 
```
Although we do not have an explicitly declared constructor without 
parameters, we can create objects of this class in the following way: 
Collar collar = new Collar(); 


The default parameterless constructor looks the following way: 
<access_level> <class_name>() { } 
We should know that the default constructor is always named like the class 
<class_name>, and its parameter list is always empty as well as its body. The 
compiler simply adds one if there is no constructor in the class. The default 
constructor is usually public (except for some very specific situations, where 
it is protected). 
 
The default constructor is always without parameters. 
To make sure that the default constructor is always without parameters let’s 
try to call the default constructor by setting it with parameters: 
Collar collar = new Collar(5); 
The compiler will display the following error message: 
'Collar' does not contain a constructor that takes 1 arguments

### How the Default Constructor Works?

As we can guess, the only thing the default constructor will do when creating 
objects of our class, is to zero the fields of the  class. For example, if in the 
```csharp
class Collar we have not declared any constructor and we create an object 
```
from it, and later we try to print the value in the field size: 
```csharp
static void Main() 
{ 
 Collar collar = new();
 Console.WriteLine("Collar's size is: " + collar.Size); 
}  
```
The result will be: 
Collar's size is: 0 
We see that the value saved in the field size of the object collar is just the 
default value of the whole number type – int.

### When a Default Constructor Will Not Be Created?

We have to know that if we declare at least one constructor in a given class 
then the compiler will not create a default constructor. 
To investigate this, consider the following example: 


public Collar(int size) 
 : this() 
```csharp
{ 
 this.size = size; 
} 
Let this be the only constructor in the class Collar. We try to call a 
```
constructor without parameters in it, hoping that the compiler will have 
created a default parameterless constructor for us. After we try to compile, 
we will find out that what we are trying to do is not possible. The compiler will 
show the following error: 
'Collar' does not contain a constructor that takes 0 arguments 
The rule about the default implicit parameterless constructor is: 
 
If we declare at least one constructor in a given class, the 
compiler will not create a default constructor for us.

### Difference between a Default Constructor and a Constructor without Parameters

Difference between a Default Constructor and a Constructor 
without Parameters 
Before we finish this section for the constructors, we will clarify something 
very important: 
 
Although the default const ructor and the one without 
parameters are similar in signature, they are completely 
different. 
The difference is that the default implicit constructor is created by the 
compiler, if we do not declare any constructor in our class, and the 
constructor without parameters is declared by us. 
Moreover, as explained earlier, the default constructor will always have access 
level protected or public, depending on the access modifier of the class, 
while the level of access of the constructor without parameters all d epends on 
us – we define it.