# Accessing Non-Static Data of the Class

In "Creating and Using Objects ", we have discussed how based on the "dot" 
operator we can access fields and to call the methods of a given class. Now, 
let’s recall how we use conventional non -static methods of a given class, i.e. 
the methods do not have the modifier static in theirs declaration. 
E.g. let’s have the class Dog with the field age. To print the value of this field 
we need to  create a Dog instance and access the field of this instance via a 
“dot” notation: 
```csharp
public class Dog 
{ 
 int age = 2; 
 
 static void Main() 
 { 
  Dog dog = new();
  Console.WriteLine("Dog's age is: " + dog.age); 
 } 
} 
```
The result will be: 
Dog's age is: 2

## Accessing Non-Static Fields from Non-Static Method

The access to the value of one field can be done via the “dot” notation (as in 
the last example dog.age), or via a method or property. Now, let’s create in 
the class Dog a method, which will return the value of age: 
```csharp
public int GetAge() 
{ 
 return this.age; 
} 
```
As we see, to access the value of the age field, inside, from the owner class, 
we use the reserved word this. We know that the word this is a reference 
to the current object, in which the method resides. Therefore, in our example, 

with "return this.age", we say "from the current object ( this) take (the 
use of the operator “dot”), the value of the field age, and return it as result 
from the method (with the help of the reserved word return). Then, instead 
from the Main() method to access the values of the  field age of the object 
dog, we simple call the method GetAge(): 
```csharp
static void Main() 
{ 
 Dog dog = new();
 Console.WriteLine("Dog's age is: " + dog.GetAge()); 
} 
```
The result of the execution based on the change will be the same. 
Formally, the declaration of access to a field in the boundaries of a class is the 
following: 
this.<field_name> 
Let’s emphasize, that this access option is possible only from non -static code, 
i.e. method or block, which is without static modifier. 
Except for retrieving of the value of one field, we can use the reserved word 
this for modification of the field. 
E.g., let’s declare a method MakeOlder(), which will be called every year on 
the date of the birthday of our pet and this method will incremen t the age 
with one year: 
```csharp
public void MakeOlder() 
{ 
 this.age++; 
} 
To check if this is correct in the Main() method we add the following lines: 
// One year later, at the birthday date… 
dog.MakeOlder(); 
Console.WriteLine("After one year dog's age is: " + dog.age); 
```
After the execution of the program, the result is the following: 
Dog's age is: 2 
After one year dog's age is: 3

## Calling Non-Static Methods

Like the fields, which do not have static modifier in theirs declarations, the 
methods, which are also  non-static, can be called in the body of a class via 
the reserved word this. This is happening again with the "dot" notation and 
more specifically with the required arguments (if there are any): 
this.<method_name>(…) 
For example, let’s create a method PrintAge(), which prints the age of the 
object from type Dog, and for this purpose calls the method GetAge(): 
```csharp
public void PrintAge() 
{ 
 int myAge = this.GetAge(); 
 Console.WriteLine("My age is: " + myAge); 
} 
```
The first line of the example is indicating that we want to receive the age (the 
value of the field age) of the current object, using the method GetAge(). This 
is done via the reserved word this. 
 
The access to the non -static elements of a class (fields a nd 
methods) is done via the reserved word this and the 
operator for access – "dot".

## Skip "this" Keyword When Accessing Non-Static Data

When we access the fields of a class or we call its non -static methods, it is 
possible to omit the reserved word this. Then both methods, which we 
already declared will be written in this way: 
```csharp
public int GetAge() 
{ 
 return age; // The same like this.age 
} 
 
public void MakeOlder() 
{ 
 age++; // The same like this.age++ 
} 
```
The reserved word this is used to indicate explicitly that we want to have 
access to a non-static field of a class or to call some of its non -static methods. 
When this explicit clarification is not needed, it can be skipped and directly to 
access the elements of the class. 


Although it is understood clearly, the reserved word this is often used for 
access to fields in the class, because it helps to make the code easier to read, 
understand and maintain, by explicitly stating that we access a field and not a 
local variable. 
 
When it is not required explicitly t he reserved word this can 
be skipped when we access the elements of the class.  For 
better readability use this keyword even when not required.