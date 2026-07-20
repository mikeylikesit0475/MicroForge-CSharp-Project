# Generics

In this section we will explain the concept of generic classes (generic data 
types, generics) . Before we begin, however, let ’s look through an example 
that will help us understand more easily the idea.

## Shelter for Homeless Animals – Example

Let’s assume that we have two classes. A class Dog, which describes a dog: 
Dog.cs 
```csharp
public class Dog 
{ 
} 
And let a class Cat, which describes a cat: 
```
Cat.cs 
```csharp
public class Cat 
{ 
} 
Then we want to create a class that describes a shelter for homeless 
animals – AnimalShelter. This class has a specific number of free cells, 
```
which determines the number of animals, which could find refuge in the 
shelter. The special feature of the class, that we want to create, is that it only 
needs to accommodate animals of the same kind, in our case, dogs or cats 
only, because the coexistence of different species is not always a good idea.  
If we think about how to solve the task with the knowledge that we have until 
here, we will come to the following  conclusion – to ensure that our class will 
contain elements only from one and the same type we need to use an array of 

identical objects. These objects may be dogs, cats  or simply instances of the 
universal type object. 
For instance, if we want to make a shelter for dogs, here is how our class 
would look like: 
AnimalsShelter.cs 
```csharp
public class AnimalShelter 
{ 
 private const int DefaultPlacesCount = 20; 
 
 private Dog[] animalList; 
 private int usedPlaces; 
 
 public AnimalShelter() : this(DefaultPlacesCount) 
 { 
 } 
 
 public AnimalShelter(int placesCount) 
 { 
  this.animalList = new Dog[placesCount]; 
  this.usedPlaces = 0; 
 } 
 
 public void Shelter(Dog newAnimal) 
 { 
  if (this.usedPlaces >= this.animalList.Length) 
  { 
   throw new InvalidOperationException("Shelter is full."); 
  } 
  this.animalList[this.usedPlaces] = newAnimal; 
  this.usedPlaces++; 
 } 
 
 public Dog Release(int index) 
 { 
  if (index < 0 || index >= this.usedPlaces) 
  { 
   throw new ArgumentOutOfRangeException( 
    "Invalid cell index: " + index); 
  } 
  Dog releasedAnimal = this.animalList[index]; 
  for (int i = index; i < this.usedPlaces - 1; i++) 
  { 
   this.animalList[i] = this.animalList[i + 1]; 

  } 
  this.animalList[this.usedPlaces - 1] = null; 
  this.usedPlaces--; 
 
  return releasedAnimal; 
 } 
} 
```
Shelter capacity (number of animals, which it is capable to accommodate) is 
set when the object is created. By default it is the value of the constant 
DefaultPlacesCount. We use the field usedPlaces to monitor the occupied 
cells (at the same time we use it  to index into the array for "pointing" to the 
first space from left to right in the array). 
 
We have created a method for adding a new dog into the shelter – 
Shelter() and respectively for releasing from the shelter – Release(int). 
The method Shelter() adds each new animal in the first free cell in the right 
side of the array (if there is any free). 
The method Release(int) accepts the number of cell from which the dog will 
be released (i.e. the index number in the array, where it is stored a link to the  
object of type Dog). 
 
usedPlaces
Occupied
0
Occupied
1
Empty
2
Empty
3
Empty
4
usedPlacesusedPlaces
Occupied
0
Occupied
0
Occupied
1
Occupied
1
Empty
2
Empty
2
Empty
3
Empty
3
Empty
4
Empty
4
usedPlaces
Empty
4
Occupied
0
Occupied
1
Occupied
2
Occupied
3
release usedPlacesusedPlaces
Empty
4
Empty
4
Occupied
0
Occupied
0
Occupied
1
Occupied
1
Occupied
2
Occupied
2
Occupied
3
Occupied
3
releaserelease

Then it moves all animals which are having a bigger cell number then the 
current cell, from which we will release a dog, with a position to the left (steps 
2 and 3 are shown in the diagram below). 
 
Released cell at position usedPlaces-1 is marked as free, and a value null is 
assigned to it. This provides release of the reference to it and respectively  
allows the system to clean memory (garbage collector), to release the object 
if it is not used anywhere else in the program at  this moment. This prevents 
from indirect loss of memory (memory leak). 
Finally, it assigns the number of the last free cell to a  usedPlaces field 
(steps 4 and 5 of the scheme above). 
 
It is visible that the “removal” of an animal from a cell could be a s low 
operation, because it requires the transfer of all animals from the next cells 
with one position left. In the chapter " Linear Data Structures " we will discuss 
also more efficient ways of presenting t he animal shelter, but for now let ’s 
focus on the topic about generic types. 
So far we succeed implementing functionality of the shelter – the class 
AnimalShelter. When we work with objects of type Dog, everything compiles 
and executes smoothly: 
usedPlaces
Empty
4
Occupied
0
Occupied
1
Occupied
2
Occupied
3
releasedAnimal
1
2 3 4
5
usedPlaces
Empty
4
Occupied
0
Occupied
1
Occupied
2
Occupied
3
releasedAnimal
1
2 3 4
5
usedPlaces
Empty
4
Occupied
0
Occupied
1
Occupied
2
Occupied
3
releasedAnimal
1
2 3 4
5
usedPlaces
Empty
4
Occupied
0
Occupied
1
Occupied
2
Occupied
3
releasedAnimal usedPlacesusedPlaces
Empty
4
Empty
4
Occupied
0
Occupied
0
Occupied
1
Occupied
1
Occupied
2
Occupied
2
Occupied
3
Occupied
3
releasedAnimal
releasedAnimal
1
2 3 4
5
usedPlaces
Empty
4
Occupied
0
Occupied
1
Empty
3
Occupied
2
releasedAnimal usedPlacesusedPlaces
Empty
4
Empty
4
Occupied
0
Occupied
0
Occupied
1
Occupied
1
Empty
3
Empty
3
Occupied
2
Occupied
2
releasedAnimal
releasedAnimal

```csharp
static void Main() 
{ 
 AnimalShelter dogsShelter = new(10);
 Dog dog1 = new();
 Dog dog2 = new();
 Dog dog3 = new();
 
 dogsShelter.Shelter(dog1); 
 dogsShelter.Shelter(dog2); 
 dogsShelter.Shelter(dog3); 
 
 dogsShelter.Release(1); // Releasing dog2 
} 
What happens, however, if we attempt to use an AnimalShelter class for 
```
objects of type Cat: 
```csharp
static void Main() 
{ 
 AnimalShelter dogsShelter = new(10);
 
 Cat cat1 = new();
 
 dogsShelter.Shelter(cat1); 
} 
```
As expected, the compiler displays an error: 
The best overloaded method match for 'AnimalShelter.Shelter(  
Dog)' has some invalid arguments. Argument 1: cannot convert 
from 'Cat' to 'Dog' 
Consequently, if we want to create a shelter for cats, we will not be able to 
reuse the class that we already created, although the operations of adding 
and removing animals from the shelter will be identical. Therefore, we have to 
literally copy AnimalShelter class and change only the type of the objects, 
which are handled – Cat. 
Yes, but if we decide to make a shelter for other species? How many classes 
of shelters for the particular type of animals we shall create? 
We can see that this solution of the pr oblem is not sufficiently 
comprehensive and does not fully meets the terms, which we were set – to 
exist a single class that describes our shelter for any kind of animal  (i.e. 
for all objects) and by working with it, it should contain only one kind of 
animals (i.e. only objects of one and the same type). 

We could use instead of the type Dog, the universal type object, which can 
take values as Dog, Cat and all other data types, but this will create some 
inconvenience, associated with the need to convert back  from the object to 
the Dog, when creating a shelter for dogs and it contains cells of type object, 
instead of type Dog. 
To solve the task efficiently, we have to use a feature of the C# language that 
allows us to satisfy all required conditions simultaneo usly. It is called 
generics (template classes).

## What Is a Generic Class?

As we know if a method needs additional information to operate properly, this 
information is passed to the method using parameters. During the execution 
of the program, when calling t his particular method, we pass arguments to 
the method, which are assigned to its parameters and then used in the 
method’s body. 
Like the methods, when we know, that the functionality (actions) 
encapsulated into a class, can be applied not only to objects of one, but to 
many (heterogeneous) types, and these types are not known at the time of 
declaring the class, we can use a functionality of the language C# called 
generics (generic types). 
It allows us to declare parameters of this class, by indicating an 
unknown type  that the class will work eventually with. Then, when we 
instantiate our generic class, we replace the unknown with a particular. 
Accordingly, the newly created object will only work with objects of this type 
that we have assigned at its initialization. The specific type can be any data 
type that the compiler recognizes, including class, structure, enumeration or 
another generic class. 
To get a clea ner picture of the nature of the generic types, let’s return to our 
task from the previous section. As you might guess, the class that describes 
the animal shelter (AnimalShelter) can operate with different types of 
animals. Consequently, if we want to create a general solution of the ta sk, 
during the declaration of class AnimalShelter, we cannot know what type of 
animals will be sheltered to shelter. This is sufficient indication, that we can 
typify our class, adding to the declaration of the class as a parameter, the 
unknown type of animals. 
Later, when we want to create a dog ’s shelter for example, this parameter of 
the class will pass the name of our type – class Dog. Accordingly, if you create 
a shelter for cats, we will pass the type Cat, etc. 
 
Typifying a class (creating a generic class) means to add to 
the declaration of a class a parameter (replacement) of 
unknown type, which the class will use during its operation . 
Subsequently, when the class is instantiated, this parameter 
is replaced with the name of some specific type. 


In the following sections we will introduce the syntax of generic classes and 
we will modify our previous example to use generics.

## Declaration of Generic Class

Formally, the parameterizing of a class  is done by adding  <T> to the 
declaration of the class, after its name, where T is the substitute (parameter) 
of the type, which will be used later: 
[<modifiers>] class <class_name><T> 
```csharp
{ 
} 
```
It should be noticed that the characters ' <' and ' >', which surround the 
substitution T are an obligatory part of the syntax of language C# and must 
participate in the declaration of a generic class. 
The declaration of generic class , which describes a shelter for homeless 
animals, should look like as follows: 
```csharp
class AnimalShelter<T> 
{ 
 // Class body here … 
} 
Let’s can imagine that we are creating a template of our class  
```
AnimalShelter, which we will specify later, replacing T with a specific type, 
for instance a Dog. 
A particular class may have more than one substitute (to be parameterized by 
more than one type), depending on its needs: 
[<modifiers>] class <class_name><T1 [, T2, [… [, Tn]]]> 
```csharp
{ 
} 
If the class needs several different unknown types , these types should be 
```
listed by a comma between the characters ' <' and '>' in the declaration of the 
```csharp
class, as each of the substitutes used must be different identifier (e.g. a 
```
different letter) – in the definition they are indicated as T1, T2, …, Tn. 
In case, we should to create a shelter for animals of a mixed type, one that 
accommodates both – dogs and cats, we should declare the class as follows: 
```csharp
class AnimalShelter<T, U> 
{ 
 // Class body here … 
} 

```
If this were our case, we would use the first parameter T, to indicate objects 
of type Dog, which our class would operate with, and with U – to indicate 
objects of type Cat.

## Specifying Generic Classes

Before we present more details about generics, we shoul d look at how to use 
generic classes. The using of generic classes should be done as follows: 
<class_name><concrete_type><variable_name> = 
 new <class_name><concrete_type>(); 
Again, similar to T substitution in the declaration of our class, the characters  
'<' and '>' surrounding a particular class concrete_type, are required. 
If we want to create two shelters, one for dogs and one for cats, we should 
use the following code: 
AnimalShelter<Dog> dogsShelter = new AnimalShelter<Dog>(); 
AnimalShelter<Cat> catsShelter = new AnimalShelter<Cat>(); 
In this way, we ensure that the shelter dogsShelter will always contain 
objects of a type Dog and the variable catsShelter will always operate with 
objects of type Cat.

## Using Unknown Types by Declaring Fields

Once used during the class declaration, the parameters that are used to 
indicate the unknown types are visible in the whole body of the class, 
therefore they can be used to declare the field as each other type: 
[<modifiers>] T <field_name>; 
As we can guess, in our example with shelter for homeless animals, we can 
use this feature provided by language C#, to declare the type of field 
animalsList, which holds references to objects for the housed animals, 
instead of a specific type of Dog, with parameter T: 
```csharp
private T[] animalList; 
```
Let’s assume when we create an object of our class, setting a specific type 
(e.g. Dog) during the execution of the program, t he unknown type T will 
be replaced with the above type. If we choose to create a shelter for dogs, 
we can consider that our field is declared as follows: 
```csharp
private Dog[] animalList; 

```
Accordingly, when we want to initialize a particular field in the constructor of 
our class, we should do it as usual – creating an array, using substitution of 
the unknown type – T: 
```csharp
public AnimalShelter(int placesNumber) 
{ 
 animalList = new T[placesNumber]; // Initialization 
 usedPlaces = 0; 
}
```

## Using Unknown Types in a Method’s Declaration

As an unknown type used in the declaration of a generic class is visible from 
opening to closing brace of the class body, except for field ’s declaration, it 
can be used in a method declaration, namely: 
As a parameter in the list of parameters of the method: 
<return_type> MethodWithParamsOfT(T param) 
- As a result of implementation of the method: 
T MethodWithReturnTypeOfT(<params>) 
As we already guessed, using our example, we can adapt the methods 
Shelter(…) and Release(…), respectively: 
- As a method of unknown type parameter T: 
```csharp
public void Shelter(T newAnimal) 
{ 
 // Method's body goes here … 
} 
```
- And a method, which returns a result of unknown type T: 
```csharp
public T Release(int i) 
{ 
 // Method's body goes here … 
} 
As we already know when we create an object from our class shelter and 
```
replace the unknown type with a specific one (e.g. Cat), during the execution 
of the program, the above methods will have the following form: 
- The parameter of method Shelter will be of type Cat: 

```csharp
public void Shelter(Cat newAnimal) 
{ 
 // Method's body goes here … 
} 
```
- The method Release will return a result of type Cat: 
```csharp
public Cat Release(int i) 
{ 
 // Method's body goes here … 
}
```

## Typifying (Generics) – Behind the Scenes

Before we continue, let ’s us explain what happens into the memory of the 
computer, when we work with generic classes. 
 
First we declare our generic class MyClass<T> (generic class description in 
the scheme above). Then the compiler translates our code to an intermediate 
language (MSIL), as translated code contains information that the class is 
generic, i.e. it works with u ndefined types until now. At runtime, when 
someone tries to work with our generic class and tries to use it with a specific 
type, a new description of the class  is created (specific type class 
description in the diagram above), which is identical to the ge neric class, with 
the difference that where it has been used T, now is replaced by a specific 
type. For example, if you try to use MyClass<int>, everywhere in your code, 
where the unknown parameter T is used, it will be replaced with int. Only 
then we can create object of a generic class with a specific type int. The 
interesting thing here is that to create this object, the description of the class, 
which was created in the meantime (specific type class description), will be 
used. Instantiating of a generic  class by given specific types of its parameters 
is called "specialization of the type" or "extension of generic class". 
Using our example, if we create an object of type AnimalShelter<T>, which 
works only with objects of type Dog, if we try to add an obje ct of type Cat, 
this will cause a compile error almost identical to the errors, that were derived 
by an attempt to add an object of type Cat, into an object of type 
AnimalShelter, which we have created in section " Shelter for Homeless 
Animals – Example": 
MyClass<T> MyClass<concrete_type> MyClass<concrete_type> 
instance
generic class 
description
concrete type
```csharp
class description
```
concrete type
```csharp
class instance
```
MyClass<T> MyClass<concrete_type> MyClass<concrete_type> 
instance
generic class 
description
concrete type
```csharp
class description
```
concrete type
```csharp
class instance

static void Main() 
{ 
 AnimalShelter<Dog> dogsShelter = new(10);
 
 Cat cat1 = new();
 
 dogsShelter.Shelter(cat1); 
} 
As expected, we get the following compilation error messages: 
The best overloaded method match for 'AnimalShelter< 
```
Dog>.Shelter(Dog)' has some invalid arguments 
 
Argument 1: cannot convert from 'Cat' to 'Dog'

## Generic Methods

Like classes, when the type of method ’s parameters cannot be specified, we 
can parameterize (typify) the method . Accordingly, the indication of a 
specific type will happen during the invocation of the method, replacing the 
unknown type with a specific one, as we did in the classes. 
Typifying of a method is done, when after the name and before the opening 
bracket of the method, we add <K>, where K is the replacement of the type 
that will be used later: 
<return_type><methods_name><K>(<params>) 
Accordingly, we can use unknown type K for parameters in the parameter ’s 
list of method <params>, whose type is unknown and also for return value or 
to declare variables of type substitute K in the body of the method. 
For example, consider a method that swaps the values of two variables: 
```csharp
public void Swap<K>(ref K a, ref K b) 
{ 
 K oldA = a; 
 a = b; 
 b = oldA; 
} 
```
This is a method that swaps the values of two variables, without carrying of 
their types. That is why we define it as a generic, so we can use it for all 
types of variables. 

Accordingly, if we want to swap the values of two integers and then two string 
variables, we should use our method: 
int num1 = 3; 
int num2 = 5; 
```csharp
Console.WriteLine("Before swap: {0} {1}", num1, num2); 
// Invoking the method with concrete type (int) 
Swap<int>(ref num1, ref num2); 
Console.WriteLine("After swap: {0} {1}\n", num1, num2); 
 
string str1 = "Hello"; 
string str2 = "There"; 
Console.WriteLine("Before swap: {0} {1}!", str1, str2); 
// Invoking the method with concrete type (string) 
Swap<string>(ref str1, ref str2); 
Console.WriteLine("After swap: {0} {1}!", str1, str2); 
```
When you run this code, the result is as expected: 
Before swap: 3 5 
After swap: 5 3 
 
Before swap: Hello There! 
After swap: There Hello! 
We notice that in the list of parameters we have used also the keyword ref. 
This concerns the specification of the method – namely, to exchange the 
values of two references. By using the keyword ref, the method will use the 
same reference that was given by the calling method. This way, all changes 
on this variable made by our method, will remain after the method exits. 
We should know that by calling a generic method , we can miss the explicit 
declaration of a specific type (in our example <int>), because the compiler 
will detect it automatically, recognizing the type of the given parameters. In 
other words, our code can be simplified using the following calls: 
Swap(ref num1, ref num2); // Invoking the method Swap<int> 
Swap(ref str1, ref str2); // Invoking the method Swap<string> 
We should know that the compiler will be able to recognize what is the 
specific type , only if this type is involved in the parameter ’s list. The 
compiler cannot recognize what is the specific type of a generic method only 
by the type its return value or if it does not have parameters. In both cases, 
this specific type will have to be given explicit ly. In our example, it will be 
similar to the original method call, or by adding <int> or <string>. 

It should be noticed that static methods can also be typified, unlike the 
properties and constructors of the class. 
 
Static methods can also be typified, b ut properties and 
constructors of the class cannot.

## Features by Declaration of Generic Methods in Generic Classes

Features by Declaration of Generic Methods in 
Generic Classes 
As we have already seen in the section  "Using Unknown Types in a 
Declaration of Methods ", non -generic methods can use unknown types, 
described in the generic class declaration (e.g. methods Shelter() and 
Release() from the example “Shelter for Homeless Animals”): 
AnimalShelter.cs 
```csharp
public class AnimalShelter<T> 
{ 
 // … The rest of the code … 
 
 public void Shelter(T newAnimal) 
 { 
  // Method body here 
 } 
 
 public T Release(int i) 
 { 
  // Method body here 
 } 
} 
```
If we try to reuse the variable, which is used to mark the unknown type of the 
generic class, for example as T, in the declaration of generic method, then 
when we try to compile the class, we will get a warning CS0693. This is 
happening because the scope of action of the unknown type T, defined in 
declaration of the method, overlaps the scope of action of the unknown type 
T, in class declaration: 
CommonOperations.cs 
```csharp
public class CommonOperations<T> 
{ 
 // CS0693 
 public void Swap<T>(ref T a, ref T b) 
 { 


  T oldA = a; 
  a = b; 
  b = oldA; 
 } 
} 
```
When you try to compile this class, you receive the following message: 
Type parameter 'T' has the same name as the type parameter from 
outer type 'CommonOperations<T>' 
So if we want our code to be flexible, and our generic method safely to be 
called with a specific type, different from that in the generic class by 
instantiating it, we just have to declare the replacement of the unknown type 
in the declaration of the gene ric method to be different th an the 
parameter for the unknown type in the class declaration, as shown below: 
CommonOperations.cs 
```csharp
public class CommonOperations<T> 
{ 
 // No warning 
 public void Swap<K>(ref K a, ref K b) 
 { 
  K oldA = a; 
  a = b; 
  b = oldA; 
 } 
} 
```
Thus, always make sure that there will be no overlapping of substitutes of the 
unknown types of method and class.

## Using a Keyword "default" in a Generic Source Code

Once we have introduced the basics of generic types, let ’s try to redesign 
our first example in this section (Shelter for Homeless Animals ). The only 
thing we need to do is to replace the type Dog with some parameter T: 
AnimalsShelter.cs 
```csharp
public class AnimalShelter<T> 
{ 
 private const int DefaultPlacesCount = 20; 
 
 private T[] animalList; 

 private int usedPlaces; 
 
 public AnimalShelter() : this(DefaultPlacesCount) 
 { 
 } 
 
 public AnimalShelter(int placesCount) 
 { 
  this.animalList = new T[placesCount]; 
  this.usedPlaces = 0; 
 } 
 
 public void Shelter(T newAnimal) 
 { 
  if (this.usedPlaces >= this.animalList.Length) 
  { 
   throw new InvalidOperationException("Shelter is full."); 
  } 
  this.animalList[this.usedPlaces] = newAnimal; 
  this.usedPlaces++; 
 } 
 
 public T Release(int index) 
 { 
  if (index < 0 || index >= this.usedPlaces) 
  { 
   throw new ArgumentOutOfRangeException( 
    "Invalid cell index: " + index); 
  } 
  T releasedAnimal = this.animalList[index]; 
  for (int i = index; i <this.usedPlaces - 1; i++) 
  { 
   this.animalList[i] = this.animalList[i + 1]; 
  } 
  this.animalList[this.usedPlaces - 1] = null; 
  this.usedPlaces--; 
 
  return releasedAnimal; 
 } 
} 
```
Everything looks to work properly, until we try to compile the class. Then we 
get the following error: 

Cannot convert null to type parameter 'T' because it could be a 
non-nullable value type. Consider using 'default(T)' instead. 
The error is inside the method Release() and it is related to the recording a 
null value in the last released (rightmost) cell in the shelter. The problem is  
that we are trying to use the default value for a reference type, but we are 
not sure whether this type is a reference type or a primitive . Therefore 
the compiler displays the errors above. If the type AnimalShelter is 
instantiated by a structure and not by a class, then the null value is not valid. 
To handle this problem, in our code we have to use the construct default(T) 
instead of null, which returns the default value for the particular type that 
will be used instead of T. As we know, the default value  for reference type is 
null, and for numeric types – zero. We can make the following change: 
// this.animalList[this.usedPlaces - 1] = null; 
this.animalList[this.usedPlaces - 1] = default(T); 
Finally the compilation runs smoothly and the class AnimalShelter<T> 
operates correctly. We can test it as follows: 
```csharp
static void Main() 
{ 
 AnimalShelter<Dog> shelter = new();
 shelter.Shelter(new Dog()); 
 shelter.Shelter(new Dog()); 
 shelter.Shelter(new Dog()); 
 Dog d = shelter.Release(1); // Release the second dog 
 Console.WriteLine(d); 
 d = shelter.Release(0); // Release the first dog 
 Console.WriteLine(d); 
 d = shelter.Release(0); // Release the third dog 
 Console.WriteLine(d); 
 d = shelter.Release(0); // Exception: invalid cell index 
}
```

## Advantages and Disadvantages of Generics

Generic classes and methods increase the reusability of the code , the 
security and the performance compared to other non-generic alternatives. 
As a general rule, the programmer should strive to create and use 
generic classes,  whenever it is possible . The more generic types are 
used, the higher level of abstraction there is in the program and the source 
code becomes more flexible and reusable. However we should keep in mind, 
that overuse of generics can lead to over -generalization and the code may 
become unreadable and difficult to understand by other programmers.

## Naming the Parameters of the Generic Types

Before we finish generics as a topic, let ’s give you some guidance on working 
with the substitutes (parameters) of unknown types in a generic class: 
1. If there is just one unknown type in the generic, it is common to use the 
letter T, as a substitute for that unknown type. As an example we can 
give our class declaration AnimalShelter<T>, which we used until now. 
2. To the substitutes should be given the most descriptive names, unless a 
letter is not a sufficiently descriptive and well -chosen name, this will not 
improve readability of the source code. For instance, we can modify our 
example, replacing the letter T, with the more descriptive substitute for 
Animal: 
AnimalShelter.cs 
```csharp
public class AnimalShelter<Animal> 
{ 
 // … The rest of the code … 
 
 public void Shelter(Animal newAnimal) 
 { 
  // Method body here 
 } 
 
 public Animal Release(int i) 
 { 
  // Method body here 
 } 
} 
```
When we use descriptive names of substitutes instead of a letter, it is better 
to add T at the beginning of the name, to distinguish it more easily from the 
```csharp
class names in our application. In other words, instead of using a substitute 
```
Animal in the previous example, we should use TAnimal (T comes from the 
word "template" which means a parameterized / generic type).