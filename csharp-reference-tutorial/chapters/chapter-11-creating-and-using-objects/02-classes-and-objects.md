# Classes and Objects

Over the last few decades programming and informatics have experienced 
incredible growth and concepts, which have changed the way programs,  are 
built. Object-oriented programming (OOP) introduces such radical idea. 
We are going to make a short introduction to the principles of OOP and the 
concepts used in it. Firstly, we are going to explain what classes and objects 
are. These two terms are basic for OOP and inseparable part from the life of 
any modern programmer.

## What Is Object-Oriented Programming?

Object-oriented programming (OOP) is a programming paradigm, which uses 
objects and their interactions for building computer programs. Thus an easy 
to understand, simple model of the subject area is achieved, which gives an 
opportunity to the programmer to solve intuitively (by simple logic) many of 
the problems, which occur in the real world. 
For now we are not going to get into details what the goals and the 
advantages of OOP are, as well as  explaining in detail s the principles for 
building hierarchies of classes and objects. We are going to mention only that 
programming techniques of OOP often include encapsulation, abstraction, 
polymorphism and inheritance. These techniques are out of the g oals of 
the current chapter and we are going to consider them later in the chapter 
"Principles of Object-Oriented Programming". Now we will focus on objects as 
a basic concept in OOP.

## What Is an Object?

We are going to introduce the concept object in the context of OOP. Software 
objects model real world objects or abstract concepts (which are also 
regarded as objects). 
Examples of real-world objects are people, cars, goods, purchases, etc. 
abstract objects are concepts in an object area, which we have to model and 
use in a computer program. Examples of abstract objects are the data 
structures stack, queue, list and tree. They are not going to be a subject in 
this chapter, but we are going to see them in details in the next chapters. 
In objects from the real world (as well as in the abstract objects) we can 
distinguish the following two groups of their characteristics: 
- States – these are the characteristics of the object which define it in a 
way and describe it in general or in a specific moment 
- Behavior – these are the specific distinctive actions, which can be done 
by the object. 
Let’s take for example an object from the real world – "dog". The states of the 
dog can be "name", "fur color" and "breed", and its behavior – "barking", 
"sitting" and "walking". 
Objects in OOP combine data and the means for their processing in one. They 
correspond to objects in real world and contain data and actions: 
- Data members – embedded in objects variables, which  describe their 
states. 
- Methods – we have already considered them in detail s. They are a tool 
for building the objects.

## What Is a Class?

The class defines abstract characteristics of object s. It provides a structure 
for objects  or a pattern which we use to describe the nature of something 
(some object). Classes are building blocks of OOP  and are inseparably 
related to the objects. Furthermore, each object is an instance of exactly 
one specific class. 
We are going to give as an example a class and an object, which is its 
instance. We have a class Dog and an object Lassie, which is an instance of 
the class Dog (we say it is an object of type Dog). The class Dog describes the 
characteristics of all dogs whereas Lassie is a certain dog. 
Classes provide modularity in object-oriented programs. Their characteristics 
have to be meaningful in a common context so that they could be understood 
by people who are familiar with the problem area and are not programmers. 
For instance, the class Dog cannot have (or at least should not ) a 
characteristic "RAM" because in the context of this class such characteristic 
has no meaning.

## Classes, Attributes and Behavior

The class defines the characteristics of an object  (which we are going to 
call attributes) and its behavior (actions that can be performed by the 
object). The attributes of the class are defined as its own variables in its body 
(called member variables ). The behavior of objects is modeled by the 
definition of methods in classes. 
We are going to illustrate the f oregoing explanations through an example of 
a real-world definition of a class. Let’s return to the example with the dog. 
We would like to define a class Dog that models the real object "dog". The 
```csharp
class is going to include characteristics which are common for all dogs (such 
as breed and fur color), as well as typical for the dog behavior (such are 
```
barking, sitting, walking). In this case we are going to have attributes breed 
and furColor, and the behavior is going to be implemented by the methods 
Bark(), Sit() and Walk().

## Objects – Instances of Classes

From what has been said till now we know that each object is an instance of 
just one class and is created according to a pattern of this class. Creating the 
object of a defined class is called instantiation (creation). The instance is 
the object itself, which is created runtime. 
Each object is in instance of a specific class. This instance is characterized by 
state – set of values, associated with class attributes. 
In the context of such behavior the object con sists of two things: current 
state and behavior defined in the class of the object. The state is specific for 
the instance (the object), but the behavior is common for all objects which 
are instances of this class.