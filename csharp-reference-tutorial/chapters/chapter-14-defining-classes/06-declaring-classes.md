# Declaring Classes

The definition of a class is based on strict syntactical rules: 
[<access_modifier>] class <class_name> 
When we declare a class, it is mandatory to use the reserved word class. 
After it must stay the name of the class <class_name>. 

Besides the reserved word class and the name of the class, in the declaration 
of the class can be used several modifiers, e.g. the reviewed until now 
modifiers.

## Class Visibility

Let’s consider two classes – A and B. We say that, class A accesses the 
elements of class B, if the first class can do one of the following: 
1. Creates an object (instance) from class type B. 
2. Can access distinct methods and fields in the class B, based on the 
access level assigned to the particular methods and fields. 
There is also another operation, which can be done over the classes, when the 
visibility allows it. The operation is called inheritance of a class , but we will 
discuss it later in the chapter Object-Oriented Programming Principles. 
As we understood, the access level term means " visibility". If the class A 
cannot "see" the class B, the access level of the methods and the fields in B 
does not matter. 
The access levels, which an outer class can have, are only public and 
internal. Inner classes can be defined with other access levels.

### Access Level "public"

If we declare a class access modifier as public, we can reach it from every 
```csharp
class and from every namespace , regardless of where it exists. It means 
that every other class can create objects from this type and has access to the 
methods and the fields of the public class. 
Just to know, if we want to use a class with access level public from other 
namespace, different from the current, we should use the reserved word for 
including different namespaces using or every time we should write the full 
```
name of the class.

### Access Level "internal"

If we declare one class with access modifier internal, one will be accessible 
only from the same namespace . It means that only the classes from the 
same assembly can create objects from this type class and to have access to 
the methods and fields (with related access level)  of the class. This access 
level is the default, where it is not used access modifier by the declaration of 
the class. 
If we have two projects in common solution in Visual Studio and we want to 
use a class from one project into the other one then the refer enced class 
should be declared as public.

### Access Level "private"

If we want to be exhaustive, we have to mention that as access modifier for a 
```csharp
class can be used the visibility modifier private, but this is related to the 
term "inner class" (nested class), which we will review in the " Nested Classes" 
section. Private classes like other private members are accessible only inside 
the class which defined them.
```

## Body of the Class

By similarity to the methods, after the declaration of the class follows its 
body, i.e. the part of the class where resides the following programming code:  
[<access_modifier>] class <class_name> 
```csharp
{ 
 // … Class body – the code of the class goes here … 
} 
The body of the class begins with opening curly bracket " {" and ends with 
closing one – "}". The class always should have a body.
```

## Class Naming Convention

Equal to the methods, for creation of the class names there are the following 
common standards: 
1. The names of the classes begin with capital letter, and the rest of the 
letters are lower case. If the name of the class consists of several 
words, every word begins with capital letter, without separator to be 
used. This is the well-known PascalCase convention. 
2. For name of the classes nouns are usually used. 
3. It is recommended the name of the class to be in English language. 
Here are some example class names, which are following the guidelines:  
Dog 
Account 
Car 
BufferedReader 
More about the name of the class es we will learn in the chapter "High-Quality 
Programming Code".