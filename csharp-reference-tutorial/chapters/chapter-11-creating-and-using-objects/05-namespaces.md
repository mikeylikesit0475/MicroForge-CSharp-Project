# Namespaces

Namespace (package) in OOP we call a container for a group of classes, 
which are united by a common feature or are  used in a common context. The 
namespaces contribute to a better logical organization of the source code by 
creating a semantic division of the classes in categories and makes easier 
their usage in the source code. Now we are going to consider namespaces i n 
C# and are going to see how we can use them.

## What Are Namespaces in C#?

Namespaces in C# are named groups of classes, which  are logically 
related without any specific requirement on how to be placed in the file 
system. However, it is considered that the folder name should match the 
namespace name and the names of the files should match the names of the 
classes, which  are defined in them. We have to note that in some 
programming languages the compilation of the source code in a given 
namespace depends on t he distribution of the elements of the namespace in 
folders and files on the disk. In Java, for instance, the described file 

organization is mandatory (if it is not followed, compilation errors occur). C# 
is not so strict regarding this. 
Now, let’s consider the mechanism for defining namespaces.

## Defining Namespaces

In case we like to create a new namespace or a new class which belongs to a 
given namespace, in Visual Studio this happens automatically by the 
commands in the context menu of the Solution Explor er (on right click on the 
corresponding folder). By default the Solution Explorer is visualized like a 
Dock in the right part of the integrated environment. We are going to 
illustrate how we could add a new class in the already existing namespace 
MyNamespace by the context menu of Solution Explorer in Visual Studio: 
 
As the project is called MyConsoleApplication and we are adding in its folder 
MyNamespace, the newly created class is going to be in the following 
```csharp
namespace: 
namespace MyConsoleApplication.MyNamespace 
If we have defined a class in its own file and we like to add it in a new or 
```
already existing namespace, it is not hard to do it manually. It is enough to 
change the named block with a keyword namespace in the class: 


```csharp
namespace <namespace_name> 
{ 
```
 … 
} 
In the definition we use the keyword namespace, followed by the full name of 
the namespace. It is considered that the namespaces in C# start with a 
capital letter and are written in Pascal Case. For example, if we have to make 
a namespace containing classes for string processing, it is desirable we name 
it StringUtils, and not string_utils.

## Nested Namespaces

Except classes, namespaces can contain other namespaces in themselves 
(nested namespaces). This way, intuitively we create a hierarchy of 
namespaces, which allows even more precise distribution of classes according 
to their semantics. 
When naming namespaces in the hierarchy we use the character . as a 
separator (dot notation).  For example, the namespace System from .NET 
Framework contains in itself  the sub-namespace Collections and thus the 
full name of the nested namespace Collections is System.Collections.

## Full Names of Classes

In order to absolutely understand the meaning of namespaces, it is important 
for us to know the following: 
 
Classes are required to  have unique names only within the 
namespaces, in which they are defined. 
Outside a given namespace we can have classes with random names 
regardless of whether they match with any of the names of classes in the 
```csharp
namespace. This is because classes in the namespace are uniquely defined in 
```
its context. It is time to see how to define syntactically this uniqueness. 
Full name of the class we call the first name of the class, preceded by the 
name of the namespace in which it is defined. The full name of each class is 
unique. Again we use dot notation: 
<namespace_name>.<class_name> 
Let’s take, for example, the system class CultureInfo, defined in the 
namespace System.Globalization (we have already used it in the chapter 
"Console Input and Output "). According to the definition, the full name of the 
```csharp
class is System.Globalization.CultureInfo. 


```
In .NET Framework sometimes there are classes from different namespaces 
with matching names, for example: 
System.Windows.Forms.Control 
System.Web.UI.Control 
System.Windows.Controls.Control

## Inclusion of a Namespace

When building an application according to the object area, very often it is 
necessary to use the classes of a namespace multiple times. For the 
programmer’s convenience there is a mechanism for inclusion of a 
namespace in the current file with a source code. After the given namespace 
is included, all classes defined in it may be used without the need to use their 
full names. 
The inclusion of a nam espace in the current source cod e file is executed with 
the keyword using in the following way: 
```csharp
using <namespace_name>; 
```
We are going to pay attention to an important feature of including 
namespaces in the described way. All classes defined directly in the 
namespace <namespace_name> are included and can be used, but we have to 
know the following: 
 
Inclusion of namespaces is not recursive, i.e. when including 
a namespace the classes from the nested namespaces are 
not included. 
For example, the inclusion of namespaces System.Collections does not  
automatically include the classes from its nested  namespace System. 
Collections.Generic. When used, either we have to apply their full names, 
or to include the namespace, which contains them.

## Using a Namespace – Example

In order to illustrate the principle of inclusion of a namespace, we are going to 
consider the following program  which reads numbers, saves them in lists and 
counts how many of them are integer numbers and how many are double: 
```csharp
class NamespaceImportTest 
{ 
 static void Main() 
 { 
  System.Collections.Generic.List<int> ints = 
   new System.Collections.Generic.List<int>(); 


  System.Collections.Generic.List<double> doubles = 
   new System.Collections.Generic.List<double>(); 
 
  while (true) 
  { 
   int intResult; 
   double doubleResult; 
   Console.WriteLine("Enter an int or a double:"); 
   string input = Console.ReadLine(); 
 
   if (int.TryParse(input, out intResult)) 
   { 
    ints.Add(intResult); 
   } 
   else if (double.TryParse(input, out doubleResult)) 
   { 
    doubles.Add(doubleResult); 
   } 
   else 
   { 
    break; 
   } 
  } 
 
  Console.Write("You entered {0} ints:", ints.Count); 
  foreach (var i in ints) 
  { 
   Console.Write(" " + i); 
  } 
  Console.WriteLine(); 
 
  Console.Write("You entered {0} doubles:", doubles.Count); 
  foreach (var d in doubles) 
  { 
   Console.Write(" " + d); 
  } 
  Console.WriteLine(); 
 } 
} 
```
For this purpose the program uses the class System.Collections. 
Generic.List as it calls it by its full name. 
Let’s see how the program above works: we enter consecutively the values 4, 
1.53, 0.26, 7, 2, end. We get the following result on the standard output: 

You entered 3 ints: 4 7 2 
You entered 2 doubles: 1.53 0.26 
The program does the following: it gives the user the opportunity to enter 
consecutively numbers, which may be integer or double. This continues until 
the moment in which a value different from a number is entered. Then on the 
standard output two rows are displayed, respectively with integer and double 
numbers. 
For the implementation of the described actions we use two helping objects 
respectively of type System.Collections.Generic.List<int> and System. 
Collections.Generic.List<double>. Obviously, the full names of the 
classes make the code unreadable, and  cause inconveniences. We can easily 
avoid this effect by including the namespace System.Collections.Generic 
and use directly the classes by name. You can now see the shortened version 
of the program above: 
```csharp
using System.Collections.Generic; 
 
class NamespaceImportTest 
{ 
 static void Main() 
 { 
  List<int> ints = new();
  List<double> doubles = new();
```
  … 
 } 
}