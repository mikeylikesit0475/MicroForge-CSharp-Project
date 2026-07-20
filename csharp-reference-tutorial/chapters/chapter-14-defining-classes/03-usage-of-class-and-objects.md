# Usage of Class and Objects

In the chapter  "Creating and Using Objects " we saw in details how new 
objects of a given class are created and how they can be used. Now, shortly 
we will revise this programming technique.

## How to Use a Class Defined by Us (Custom Class)?

In order to be able to use a given class, first we need to create an object of it. 
This is done by the reserved word new in combination with some of the 
constructors of the class. This will create an object from a given class (type). 
If we want to manipulate the newly created object, we will have to assign it to 
a variable from its class type. By doing it, in this variable we will keep the 
connection (reference) to the object. 
Using the variable, and the “dot” notation, we can call the  methods and the 
properties of the object, and as well as gain access to the fields (member -
variables).

## Example – A Dog Meeting

Let’s have the example from the previous section where we defined the class 
Dog, describing a dog, and let’s add a method Main() to the class. In this 
method we will demonstrate how to use  the mentioned elements until here : 
create few Dog objects, assign properties to these objects and call methods on 
these objects: 

```csharp
static void Main() 
{ 
 string firstDogName = null; 
 Console.Write("Enter first dog name: "); 
 firstDogName = Console.ReadLine(); 
 
 // Using a constructor to create a dog with specified name 
 Dog firstDog = new(firstDogName);
 
 // Using a constructor to create a dog wit a default name 
 Dog secondDog = new();
 
 Console.Write("Enter second dog name: "); 
 string secondDogName = Console.ReadLine(); 
 
 // Using property to set the name of the dog 
 secondDog.Name = secondDogName; 
 
 // Creating a dog with a default name 
 Dog thirdDog = new();
 
 Dog[] dogs = new Dog[] { firstDog, secondDog, thirdDog }; 
 
 foreach (Dog dog in dogs) 
 { 
  dog.Bark(); 
 } 
} 
```
The output from the execution will be the following: 
Enter first dog name: Axl 
Enter second dog name: Bobby 
Axl said: Wow-wow! 
Bobby said: Wow-wow! 
[unnamed dog] said: Wow-wow! 
In the example program, with the help of Console.ReadLine(), we got the 
name of the objects of type dog, which the user should input. 
We assigned the first entered string to the variable firstDogName. Afterwards 
we used this variable when we created the first object from class type Dog – 
firstDog, by assigning it to the parameter of the constructor. 
We created the second object Dog, without using a string for the name of the 
dog in the constructor.  With the help of Console.ReadLine() we got the 

name of the dog and then the value was assigned to the property Name. This 
is done by using a “dot” convention, applied to the variable, which keeps the 
reference to the second object from type Dog – secondDog.Name. 
When we created the third object from class type Dog, we used for the name 
of the dog its default value  which is null. Note that in the Bark() method 
dogs whthout name (name == null) are printed as “[unnamed dog]”. 
Afterward we created an a rray from type Dog, by initializing it with the three 
newly created objects. 
At the end, we used a loop, to go through the array of objects from type Dog. 
For every element from the array we again used the “dot” notation, be calling 
the method Bark() for the particular object: dog.Bark().

## Nature of Objects

Let’s revise, when we create an object in .NET, one consists from two parts – 
the significant part (data), which contains its data and it is located in the 
memory of the operating system called a dynamic memory (heap) and a 
reference part to this object, which resides in the other part of the operating 
system’s memory, where are stored the local variable and parameters of the 
methods (the program execution stack). 
For example, let’s have a class called Dog, which has the properties for name, 
kind and age. Let’s create a variable dog from this class. This variable is a 
reference to the object and is in the dynamic memory (heap). 
The reference is a variable, which can access objects. The figure below 
depicts an example reference, which has link to the real object in the heap, 
and is called with the name dog. One, compare to the variable from primitive 
(value type), does not contain the real value (i.e. the data of the object), but 
the address, where one is located in the heap memory: 
 
When we declare one variable from type a particular class, and we do not 
want the variable to be associated with a specific object, then we assign to it 
the value null. The reserved word null in the C# language means, that the 
variable does not point to any object (there is a missing value): 
HeapStack
Dog@a8fe24
dog
dog reference
name
kind
age
dog object