# Fields

Objects describe things from the real world. In order to describe an object, we 
focus on its characteristics, which are related to the problems solved in our 
program. These characteristics of the real -world object we will hold in the 
declaration of the class in special types of variables. These variables, called 
fields (or member-variables), are holding the state of the object. When we 
create an object based on certain class definition, the values of the fields are 
containing the characteristics of the created object (its state) . These 
characteristics have different values different for the different objects.

## Declaring Fields in a Class

Until now we have discussed only two types of variables (see " Methods") 
depending on where they are declared: 
1. Local variables – these are the variables declared in the body of some 
method (or block). 
2. Parameters – these are the variables in the list of  parameters, which 
one method can have. 
In C# a third type of variable exists, called field or instance variable. 
Fields are declared in the body of the class, outside the body of a single 
method or constructor. 
 
Fields are declared in the body of the class but not in the 
bodies of the methods or the constructors. 
This is a sample code declaring several fields: 
```csharp
class SampleClass 
{ 
 int age; 
 long distance; 
 string[] names; 
 Dog myDog; 


} 
```
More formal, the declaration of a field is done in the following way: 
[<modifiers>] <field_type> <field_name>; 
The <field_type> part determinates the type of a given field. This type can 
be primitive (byte, short, char and so on), an array, or also some class type 
(e.g. Dog or string). 
The <field_name> part is the name of the field. As the name of the normal 
variables, when we declare the name of the instance -variables, we should 
obey the rules for naming of identifiers  in C# (see  chapter "Primitive Types 
and Variables"). 
The <modifiers> part is a definition, which describes the access modifiers 
and as well other modifiers. The last ones are not a mandatory part  of the 
field declaration. 
Modifiers and the access modifiers, allowed in the declaration of one field, are 
explained in chapter "Primitive Types and Variables". 
In this chapter, from the other mo difiers, which are not based on access 
levels, and can be used in the declaration of fields, we will discuss static, 
const and readonly.

### Scope

The scope of a class field starts from the line where is declared and ends at 
the closing bracket of the body of the class.

## Initialization during Declaration

When we declare one field it is possible to assign to it an initial value. We do 
this similarly to an assignment of normal local variable: 
[<modifiers>] <field_type> <field_name> = <initial_value>; 
Of course, the <initial_value> has to be a type compatible with the field ’s 
type, e.g.: 
```csharp
class SampleClass 
{ 
 int age = 5; 
 long distance = 234; // The literal 234 is of integer type 
 
 string[] names = new string[] { "Peter", "Martin" }; 
 Dog myDog = new();
 

 // … Other code … 
}
```

## Default Values of the Fields

Every time, when we create a new object of a given class, it is allocated 
memory in the heap for every field from the class. In order this to be done 
the memory is initialized automatically with the default values  for the 
certain field. The fields, which do not have explicitly a default value in the 
code, use the default value specified for the .NET type, to which they belong. 
This is different for the local variables defined in methods. If a local variable in 
a method does not have a value assigned, the code will not compile. If a 
member variable (field) in a class does not have a value assigned, it will be 
automatically zeroed by the compiler. 
 
When an object is created all of  the fields are initialized with 
their respective default values in .NET, except if they are not 
explicitly initialized with some other value. 
In some languages (as C and C++) the newly created objects are not 
initialized with default values of theirs data and this creates conditions for 
hard-to-find errors. The last leads to uncontrolled behavior , where the 
program sometimes works correctly (when the allocated memory by chance 
has good values), and sometimes does not work (when the allocated memory 
does not contain the proper values). In C# and generally in .NET Framework 
this problem is solved by the default values for each type coming from the 
framework. 
The value of all types is 0 or something similar. For the most used types these 
values are as the follows: 
Type of the Field Default Value 
bool false 
byte 0 
char '\0' 
decimal 0.0M 
double 0.0D 
float 0.0F 
int 0 
object reference null 
For more detailed information you can check  chapter "Primitive Types and 
Variables" and its section about the primitive types and their default values. 


For example, if we create a class Dog and we define for it fields name, age and 
length and check for the gender isMale, without explicitly initializing them, 
they will be automatically zeroed when we create an object of this class: 
```csharp
public class Dog 
{ 
 string name; 
 int age; 
 int length; 
 bool isMale; 
 
 static void Main() 
 { 
  Dog dog = new();
  Console.WriteLine("Dog's name is: " + dog.name); 
  Console.WriteLine("Dog's age is: " + dog.age); 
  Console.WriteLine("Dog's length is: " + dog.length); 
  Console.WriteLine("Dog is male: " + dog.isMale); 
 } 
} 
```
Respectively, when we execute the program we w ill have as output the 
following: 
Dog's name is: 
Dog's age is: 0 
Dog's length is: 0 
Dog is male: False

### Automated Initialization of Local Variables and Fields

If we define a local variable in one method, without initializing it, and 
afterward we try to use it (e.g. printing its value), this will trigger a 
compilation error, because the local variables are not initialized with default 
values when they are declared. 
 
Unlike fields, local variables are not initialized with default 
values when they are declared. 
Let’s have look into one example: 
```csharp
static void Main() 
{ 
 int notInitializedLocalVariable; 
 Console.WriteLine(notInitializedLocalVariable); 


} 
```
If we try to compile, we will receive the following error: 
Use of unassigned local variable 'notInitializedLocalVariable'

### Custom Default Values

A good programming practice is, when we declare fields in the class, to 
explicitly initialize them with some default value, even if the default value is 
zero. This will make our code clearer and easy to read. 
One example for such initialization is the modified example class SampleClass 
from the previous section: 
```csharp
class SampleClass 
{ 
 int age = 0; 
 long distance = 0; 
 string[] names = null; 
 Dog myDog = null; 
 
 // … Other code … 
}
```

## Modifiers "const" and "readonly"

As was explained in the beginning in this section, in the declaration of one 
field is allowed to use the modifications const and readonly. The fields, 
declared as const or readonly are called constants. They are used when a 
certain value is used several times . These values are declared only ones  
without repetitions . Examples of constants in the .NET Framework are the 
mathematical co nstants Math.PI and Math.E, and as well the constants 
String.Empty and Int32.MaxValue.

### Constants Based on "const"

The fields, declared with const, have to be initialized during the de facto 
declaration and afterwards theirs value cannot be changed. They ca n be 
accessed without to create an instance (an object) of the class and they are 
common for all created objects in our program. Something more, when we 
compile the code, the places where const fields are referred are replaced with 
theirs particular values directly without to use the constant variable at all. For 
this reason the const fields are called compile-time constants , because 
they are replaced with the value during the compilation process.

### Constants Based on "readonly"

The modifier readonly creates fields, which values cannot be changed once 
they are assigned. Fields, declared as readonly, allow one -time initialization 
either in the moment of the declaration or in the class constructors. Later 
theirs values cannot be changed. Because of this reason, the readonly fields 
are called run-time constants – constants, because their values cannot be 
changed after assignment and run -time, because this process happens during 
the execution of the program (in runtime). 
Let’s illustrate the foregoing with the following example: 
```csharp
public class ConstAndReadOnlyExample 
{ 
 public const double PI = 3.1415926535897932385; 
 public readonly double Size; 
 
 public ConstAndReadOnlyExample(int size) 
 { 
  this.Size = size; // Cannot be further modified! 
 } 
 
 static void Main() 
 { 
  Console.WriteLine(PI); 
  Console.WriteLine(ConstAndReadOnlyExample.PI); 
  ConstAndReadOnlyExample instance = 
   new ConstAndReadOnlyExample(5); 
  Console.WriteLine(instance.Size); 
 
  // Compile-time error: cannot access PI like a field 
  Console.WriteLine(instance.PI); 
 
  // Compile-time error: Size is instance field (non-static) 
  Console.WriteLine(ConstAndReadOnlyExample.Size); 
 
  // Compile-time error: cannot modify a constant 
  ConstAndReadOnlyExample.PI = 0; 
 
  // Compile-time error: cannot modify a readonly field 
  instance.Size = 0; 
 } 
}
```