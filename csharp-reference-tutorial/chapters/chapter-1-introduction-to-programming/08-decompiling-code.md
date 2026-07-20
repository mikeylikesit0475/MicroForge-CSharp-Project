# Decompiling Code

Decompiling Code 
Sometimes programmers need to see the code of a given module or program, 
not written by them and with no source code available. The process, which 
generates source code from an existing executable binary fi le (.NET 
assembly – .exe or .dll) is called decompiling. 
We might need to decompile code in the following cases: 

- We want to check how a given algorithm is implemented but we do not 
have the source code, e.g. to check how Array.Sort() internally works. 
- There are several options when using some .NET library, and we want t o 
find the optimal choice.  We want to see how to use certain API  
digging into some compiled code that uses it. 
- We have no information how a given library works , but we have the 
compiled code (.NET assembly), which uses it, and we want to find out 
how exactly the library works. 
- We have lost our source code and we want to recover it. Code 
recovery through decompilation will result in lost variable names, 
comments, formatting, and others, but is better than nothing. 
Decompiling is done with the help of tools, which are not standard part of 
Visual Studio. The first popular .NET decompiler was Red Gate’s Reflector 
(before it became commercial in early 2011). 
Telerik is offering a good and completely free .NET decompiler called 
JustDecompile. It can be downloaded from the company’s website: 
http://www.telerik.com/products/decompiler.aspx. JustDecompile allows code 
decompilation directly in Visual Studio and also has an external stand -alone 
GUI application for browsing assemblies and decompile their code: 
 


Another good decompilation tool for .NET is the ILSpy, which is developed 
around the SharpDevelop project. ILSpy can be downloaded at: 
http://ilspy.net. The program does not require installation. After we start it, 
ILSpy loads some of the standard .NET Framework libraries. Via the menu File 
-> Open, we can open a certain .NET assembly. We can also load an assembly 
from the GAC (Global Assembly Cache). This is how ILSpy looks like: 
 
In ILSpy there are two ways to find out how a given method is implemented. 
For example, if we want to see how the static method 
System.Currency.ToDecimal works, first we can use the tree on the left to 
find the Currency class in the System namespace and finally select the 
ToDecimal method. If we click on any method, we will be able to see its 
source code in C#. Another way to find a given class is using the search 
engine in ILSpy. It searches through the names of all classes, i nterfaces, 
methods, properties etc. from the loaded assemblies. Unfortunately, the 
version at the time of writing of this book (ILSpy 2.1) can decompile only the 
languages C#, VB.NET and IL. 
JustDecompile and ILSpy are extremely useful tools, which can help almost 
every day when developing .NET software and we should definitely download 
at least one and play with it. When we are wondering how a certain method 
works or how something is implemented in a given assembly, we can always 
rely on the decompiler to find out.