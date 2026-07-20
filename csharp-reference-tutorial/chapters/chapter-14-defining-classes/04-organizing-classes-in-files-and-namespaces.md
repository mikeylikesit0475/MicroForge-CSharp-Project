# Organizing Classes in Files and Namespaces

In C# the only one limitation regarding the saving of our own custom classes 
is: they have to be saved in files with file extension  .cs. In such a file 
several classes, structures and other types can be defined. Although it is not a 
requirement of the compiler, it is recommended every class to be stored in 
exactly one file, which corresponds to its name , i.e. the class Dog should 
be saved in a file Dog.cs.

## Organizing Classes in Namespaces

As we should know from the chapter "Creating and Using Objects ", the 
namespaces in C# are named group of classes , which are logically 
connected, without a requirement how they are stored in the file system. 
If we want to include in our code namespaces for the operation in our classes, 
declared in some file or set of files, this should be done by the so named 
```csharp
using directives. They are not required, but if they exist, they are on the 
first lines in the class file, before the declaration of the classes or other types. 
```
In the next paragraphs we will understand how they exactly are used. 
After the insertion of the used namespaces, the next is the declaration of the 
```csharp
namespace of the classes in the file. As we know, there is no requirement to 
declare classes in a namespace, but it is a good programming technique if we 
do it, because the class distribution in the namespace is used for better 
```
organization of the code and determination of the classes with equal names. 
The namespaces contain classes, structure, interfaces and other types of 
data, and as well other namespaces. An example of nested namespace is 
System, which contains the namespace Data. The full name of the second 
namespace is System.Data and one is nested in the namespace System. 
The full name of a class  in .NET Framework is the class name , preceded by 
the namespace in which the class is declared, e.g.: <namespace_name>. 
<class_name>. By the using reserved word we can use types from certain 
```csharp
namespace, without writing the full name, e.g.: 
using System; 
```
… 
DateTime date; 
HeapStack
null
dog
null reference null


Instead of: 
System.DateTime date; 
One typical declaration sequence, which we should follow when we create 
custom classes in .cs files, is: 
// Using directives – optional 
```csharp
using <namespace1>; 
using <namespace2>; 
 
// Namespace definition - optional 
namespace <namespace_name> 
{ 
 // Class declaration 
 class <first_class_name> 
 { 
  // … Class body … 
 } 
 
 // Class declaration 
 class <second_class_name> 
 { 
  // … Class body … 
 } 
 
 // … 
 
 // Class declaration 
 class <n-th_class_name> 
 { 
  // … Class body … 
 } 
} 
The declaring of the namespace and the relevant include of it is already 
```
explained in the chapter  "Creating and Using Objects " and therefore we will 
not discuss it again. 
Before we continue, let’s look into the first line of the previous snippet. 
Instead include of namespace it is a source code comment. This is not a 
problem in compilation time, the co mments are "removed" from the code and 
thus the first line is still the including statement.

## Encoding of Files and Using of Cyrillic and Unicode

While we are creating a .cs file, in which to declare our classes, it is good to 
think about its character encoding in the file system. 
In the .NET Framework the compiled code is represented in Unicode so it is 
possible to use characters in our code from alphabets other than Latin. In the 
next example we use Cyrillic l etters for identifiers in Bulgarian language as 
well as comments in the code, written in Bulgarian (in Cyrillic letters): 
```csharp
using System; 
 
public class EncodingTest 
{ 
 // Тестов коментар 
 static int години = 4; 
 
 static void Main() 
 { 
  Console.WriteLine("years: " + години); 
 } 
} 
```
This code will compile and execute without a problem, but to keep the 
characters readable in the Visual Studio editor we need to provide an 
appropriate encoding of the file. 
As we know from the " Strings" chapter, some not all characters can be stored 
in all encodings. If we use non -standard characters such as Chinese, Cyrillic 
or Arabic letters, we can use UTF-8 or other character encoding that supports 
these characters. By default Visual Studio u ses the default character encoding 
(system locale) defined in the regional settings in  Windows. This might be 
ISO-8859-1 in U.K. or U.S. and Windows-1251 in Bulgaria. 
To use a different encoding other than the system’s default encoding in Visual 
Studio, we need to choose the appropriate encoding of the file  when opening 
it in the editor: 
1. From the File menu we choose Open and then File. 
2. In the Open File  window we click on the option next to the button 
Open and we choose Open With… 
3. From the list in the Open With window we choose an editor with 
encoding support, for example CSharp Editor with Encoding. 
4. Then press [OK]. 
5. In the window Encoding we choose the appropriate encoding from the 
dropdown menu Encoding. 
6. Then press [OK]. 
 

 
The steps for saving files in the file system with a specific encoding are: 
1. From the File menu we choose Save As. 
2. In the window  Save File As  we press the drop -down box next to the 
button Save and choose Save with Encoding. 
3. In Advanced Save Options  we select the desired encoding from the 
list (preferably the universal UTF-8). 
4. From the Line Endings we select the desired line ending type. 
Although we have the ability to use characters from any non-English alphabet, 
in .cs files it is highly recommended to w rite all the identifiers and 
comments in English , because this way our code will be readable for more 
people in the world. 
Imagine that you live in Germany and you need to type a code written by a 
Vietnamese person, where the names of all variables and comments are in 
Vietnamese. You will prefer English, right? Then think about how a developer 
from Vietnam will handle variables and comments in German.