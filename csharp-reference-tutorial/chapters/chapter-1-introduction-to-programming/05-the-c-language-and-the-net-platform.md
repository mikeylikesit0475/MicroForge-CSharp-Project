# The C# Language and the .NET Platform

The first version of C# was developed by Microsoft between 1999 and 20 02 
and was officially released to the public in 2002 as a part of the .NET 
platform. The .NET platform  aims to make software development for 
Windows easier by providing a new quality approach to programming, based 
on the concepts of the "virtual machine" and "managed code". At that time 
the Java language and platform reaped an enormous success in all fields of 
software development; C# and .NET were Microsoft’s natural response to the 
Java technology.

## The C# Language

C# is a modern, general -purpose, object -oriented, high -level prog -
ramming language . Its syntax is similar to that of C and C++ but many 
features of those languages are not supported in C# in order to simplify the 
language, which makes programming easier. 
The C# programs consist of one or several files with a .cs extension, which 
contain definitions of classes and other types. These files are compiled by the 
C# compiler (csc) to executable code and as a result assemblies are created, 
which are files with the same name but with a diff erent extension ( .exe or 
.dll). For example, if we compile HelloCSharp.cs, we will get a file with the 
name HelloCSharp.exe (some additional files will be created as well, but we 
will not discuss them at the moment). 
We can run the compiled code like any o ther program on our computer (by 
double clicking it). If we try to execute the compiled C# code (for example 
HelloCSharp.exe) on a computer that does not have the .NET Framework, 
we will receive an error message.

### Keywords

C# uses the following keywords to build its programming constructs (the list 
is taken from MSDN in March 2013 and may not be complete): 
abstract as base bool break byte 
case catch char checked class const 
continue decimal default delegate do double 
else enum event explicit extern false 
finally fixed float for foreach goto 
if implicit in int interface internal 
is lock long namespace new null 

object operator out override params private 
protected public readonly ref return sbyte 
sealed short sizeof stackalloc static string 
struct switch this throw true try 
typeof uint ulong unchecked unsafe ushort 
```csharp
using virtual void volatile while  
```
Since the creation of the first version of the C# language, not all keywords 
are in use . Some of them were added in later versions. The main program 
elements in C# (which are defined and used with the help of keywords) are 
classes, methods, operators, expressions, conditional statements , 
loops, data types, exceptions and few others. In the n ext few chapters of 
this book, we will review in details all these programming constructs along 
with the use of the most of the keywords from the table above.

### Automatic Memory Management

One of the biggest advantages of the .NET Framework is the built-in 
automatic memory management . It protects the programmers from the 
complex task of manually allocating memory for objects and then waiting for 
a suitable moment to release it. This significantly increases the developer 
productivity and the quality of the programs written in C#. 
In the .NET Framework, there is a special component of the CLR that looks 
after memory management. It is called a " garbage collector " (automated 
memory cleaning system). The garbage collector has the following main 
tasks: to check when  the allocated memory for variables is no longer in use, 
to release it and make it available for allocation of new objects. 
 
It is important to note that it is not exactly clear at what 
moment the memory gets cleaned of unused objects (local 
variables for  example). According to the C# language 
specifications, it happens at some moment after a given 
variable gets out of scope but it is not specified, whether this 
happens instantly, after some time or when the available 
memory becomes insufficient for the no rmal program 
operation.

## Independence from the Environment and the Programming Language

Independence from the Environment and the 
Programming Language 
One of the advantages of .NET is that programmers using different  .NET 
languages can easily exchange their code. For example a C# programmer 
can use the code written by  another programmer in VB.NET, Managed C++ 
or F#. This is possible because the programs written in different .NET 


languages share a common system of data types, execution infrastructure 
and a unified format of the compiled code (assemblies). 
A big advantag e of the .NET technology is the ability to run code, which is 
written and compiled only once, on different operating systems  and 
hardware devices. We can compile a C# program in a Windows environment 
and then execute it under Windows, Windows Mobile, Windo ws RT or Linux. 
Officially Microsoft only supports the .NET Framework on Windows, Windows 
Mobile and Windows Phone, but there are third party vendors that offer .NET 
implementation on other operating systems.

### Mono (.NET for Linux)

One example of .NET imple mentation for non -Windows environment is the 
open-source project Mono  (www.mono-project.com). It  implements the 
.NET Framework and most of its accompanying libraries for Linux, FreeBSD, 
iPhone and Android.  Mono i s unofficial .NET implementation  and some 
features may work not exactly as expected. It does implement well the core 
.NET standards (such as C# compiler and CLR) but does not support fully the 
latest .NET technologies and framework like WPF and ASP.NET MVC.

## Microsoft Intermediate Language (MSIL)

The idea for independence from the environment has been set in the earliest 
stages of creation of the .NET platform and is implemented with the help of a 
little trick. The output code is not compiled to instructions for a specific 
microprocessor and does n ot use the features of a specific operating system; 
it is compiled to the so called Microsoft Intermediate Language (MSIL) . 
This MSIL is not directly executed by the microprocessor but from a virtual 
environment called Common Language Runtime (CLR).

## Common Language Runtime (CLR) – the Heart of .NET

In the very center of the .NET platform beats its heart – the Common 
Language Runtime (CLR) – the environment that controls the execution of 
the managed code ( MSIL code). It ensures the execution of .NET programs  
on different hardware platforms and operating systems. 
CLR is an abstract computing machine ( virtual machine ). Similarly to 
physical computers , it supports a set of instructions, registries, memory 
access and input -output operations. CLR ensures a controlled execution of 
the .NET programs using the full capabilities of the processor and the 
operating system. CLR also carries out the managed access to the memory 
and the other resources of the computer, while adhering to the access rules 
set when the program is executed.

## The .NET Platform

The .NET platform contains the C# language , CLR and many auxiliary 
instruments and libraries ready for use. There  are a few versions of .NET 
according to the targeted user group: 
- .NET Framework is the most common version of the .NET environment 
because of its general purpose. It is used in the development of console 
applications, Windows applications with a graphical  user interface, web 
applications and many more. 
- .NET Compact Framework  (CF) is a "light" version of the standard 
.NET Framework and is used in the development of applications for 
mobile phones and other PDA devices using Windows Mobile Edition. 
- Silverlight is also a "light" version of the .NET Framework, intended to 
be executed on web browsers in order to implement multimedia and 
Rich Internet Applications. 
- .NET for Windows Store apps  is a subset of .NET Framework 
designed for development and execution of .NET applications in 
Windows 8  and Windows RT  environment (the so called Windows 
Store Apps).

### .NET Framework

The standard version of the .NET platform is intended for development and 
use of console applications, desktop applications, Web applications, Web 
services, Rich Internet Applications, mobile applications for tablets and smart 
phones and many more. Almost all .NET developers use the standard version.

## .NET Technologies

Although the .NET platform is big and comprehensive , it does not provide 
all the to ols required to solve every problem in software development. There 
are many independent software developers, who expand and add to the 
standard functionality offered by the .NET Framework. For example, 
companies like the Bulgarian software corporation Telerik develop subsidiary 
sets of components. These components are used to create graphical user 
interfaces, Web content management systems, to prepare reports and they 
make application development easier. 
The .NET Framework extensions are software components , which can be 
reused when developing .NET programs. Reusing code significantly facilitates 
and simplifies software development, because it provides solutions for 
common problems, offers implementations of complex algorithms and 
technology standards. The c ontemporary programmer uses libraries and 
components every day, and saves a lot of effort by doing so. 
Let’s look at the following example – software that visualizes data  in the 
form of charts and diagrams. We can use a library, written in .NET, which 
draws the charts. All that we need to do is input the correct data and the 

library will draw the charts for us. It is very convenient and efficient. Also it 
leads to reduction in the production costs because the programmers will not 
need to spend time working on additional functionality (in our case drawing 
the charts, which involves complex mathematical calculations and controlling 
the graphics card). The application itself will be of higher quality because the 
extension it uses is developed and supported by s pecialists with more 
experience in that specific field. 
Software technologies are sets of classes, modules, libraries, programming 
models, tools, patterns and best practices addressing some specific problem 
in software development. There are general softwa re technologies, such as 
Web technologies, mobile technologies, technologies for computer graphics 
and technologies related to some platform such as .NET or Java. 
There are many .NET technologies  serving for different areas of .NET 
development. Typical exa mples are the Web technologies (like ASP.NET and 
ASP.NET MVC), allowing fast and easy creation of dynamic Web applications 
and .NET mobile technologies (like WinJS), which make possible the creation 
of rich user interface multimedia applications working on the Internet. 
.NET Framework by default includes as part of itself many technologies and 
```csharp
class libraries with standard functionality, which developers can use. For 
```
example, there are ready -to-use classes in the system library working with 
mathematical fun ctions, calculating logarithms and trigonometric functions 
(System.Math class). Another example is the library dealing with networks 
(System.Net), it has a built -in functionality to send e -mails (using the 
System.Net.Mail.MailMessage class) and to download  files from the 
Internet (using System.Net.WebClient). 
A .NET technology  is the collection of .NET classes, libraries, tools, 
standards and other programming means and established development 
models, which determine the technological framework for creating  a certain 
type of application. A .NET library is a collection of .NET classes, which offer 
certain ready -to-use functionality. For example, ADO.NET is a technology 
offering standardized approach to accessing relational databases (like 
Microsoft SQL Server  and MySQL). The classes in the package (namespace) 
System.Data.SqlClient are an example of .NET library , which provide 
functionality to connect an SQL Server through the ADO.NET technology. 
Some of the technologies developed by software developers  outside of 
Microsoft become wide -spread and as a result establish themselves as 
technology standards. Some of them are noticed by Microsoft and later are 
added to the next iteration of the .NET Framework. That way, the .NET 
platform is constantly evolving and expanding with new libraries and 
technologies. For instance, the object -relational mapping technologies 
initially were developed as independent projects and products (like the open 
code project NHibernate and Telerik’s OpenAccess ORM). After they gaine d 
enormous popularity, their inclusion in the .NET Framework became a 
necessity. And this is how the LINQ -to-SQL and ADO.NET Entity Framework 
technologies were born, respectively in .NET 3.5 and .NET 4.0.

### Application Programming Interface (API)

Each .NET library or technology is utilized by creating objects and calling their 
methods. The set of public classes and methods in the programming libraries 
is called Application Programming Interface  or just API. As an example 
we can look at the .NET API itself; it  is a set of .NET class libraries, expanding 
the capabilities of the language and adding high -level functionality. All .NET 
technologies offer a public API. The technologies are often referred to simply 
as API, which adds certain functionality. For example : API for working with 
files, API for working with charts, API for working with printers, API for 
reading and creating Word and Excel documents, API for creating PDF 
documents, Web development API, etc.

### .NET Documentation

Very often it is necessary to docu ment an API, because it contains many 
namespaces and classes. Classes contain methods and parameters. Their 
purpose is not always obvious and needs to be explained . There are also 
inner dependencies between the separate classes, which need to be explained 
in order to be used correctly. These explanations and technical instructions on 
how to use a given technology, library or API, are called documentation. The 
documentation consists of a collection of documents with technical content.  
The .NET Framework also has a documentation officially developed and 
supported by Microsoft. It is publicly available on the Internet and is also 
distributed with the .NET platform as a collection of documents and tools for 
browsing and searching. 
 


The MSDN Library is Microsoft’s official documentation for all their products 
for developers and software technologies. The .NET Framework’s technical 
documentation is part of the MSDN Library and can be found here: 
http://msdn.microsoft.com/en-us/library/vstudio/gg145045.aspx. The above 
screenshot shows how it might look like (for .NET version 4.5).

## What We Need to Program in C#?

After we made ourselves familiar with the .NET platform, .NET libraries and 
.NET technologies, we can move on to writing, compiling and executing C# 
programs. 
In order to program in C#, we need two basic things – an installed .NET 
Framework and a text editor. We need the text editor to write and edit the 
C# code and the .NET Framework to compile and execute it.

### .NET Framework

By default, the .NET Framework is installed along with Windows, but in old 
Windows versions it could be missing. To install the .NET Framework, we must 
download it from Microsoft’s website (http://download.microsoft.com). It is 
best if we download and install the latest version. 
 
Do not forget that we need to install the .NET Framework 
before we begin! Otherwise, we will not be able to compile 
and execute the program. 
If we run Windows 8 or Windows 7, the .NET Framework will 
be already installed as part of Windows.

### Text Editor

The text editor is used to write the source code of the program and to save 
it in a file. After that, the code is compiled and executed. There are many text 
editing programs. We can use Windows’ built -in Notepad (it is very basic and 
inconvenient) or a better free text editor like Notepad++ ( notepad-
plus.sourceforge.net) or PSPad (www.pspad.com).

## Compilation and Execution of C# Programs

The time has come to compile and execute  the simple example program 
written in C# we already discussed. To accomplish that, we need to do the 
following: 
- Create a file named HelloCSharp.cs; 
- Write the sample program in the file; 
- Compile HelloCSharp.cs to an executable file HelloCSharp.exe using 
the console-based C# compiler (csc.exe); 
- Execute the HelloCSharp.exe file. 


Now, let’s do it on the computer! 
The instructions above vary depending on the operating system . Since 
programming on Linux is not the focus of this book, we will take a thorough 
look at what we need to write and execute the sample program on Windows. 
For those of you, who want to program in C# in a Linux environment, we 
already explained the Mono project, and you can download it and experiment. 
Here is the code of our first C# program: 
HelloCSharp.cs 
```csharp
class HelloCSharp 
{ 
 static void Main() 
 { 
  System.Console.WriteLine("Hello C#!"); 
 } 
}
```

### Creating C# Programs in the Windows Console

First we start the Windows command console, also known as Command 
Prompt. In Windows 7 this is done from the Windows Explorer start menu: 
Start -> Programs -> Accessories -> Command Prompt. 
It is advised that we run the console as administrator  (right click on the 
Command Prompt icon and choose “ Run as administrator ”). Otherwise 
some operations we want to use may be restricted. 
 


In Windows 8 the “Run as administrator” command is directly available when 
you right click the command prompt icon from the Win8 Start Screen: 
 
 
After opening the console , let’s create a dir ectory, in which we will 
experiment. We use the md command to create a directory and cd command 
to navigate to it (enter inside it): 
 


The directory will be named IntroCSharp and will be located in C:\. We 
change the current directory to C:\IntroCSharp and create a new file 
HelloCSharp.cs, by using the built-in Windows text editor – Notepad. 
To create the text file “HelloCSharp.cs”, we execute the following command 
on the console: 
notepad HelloCSharp.cs 
This will start Notepad with the following dialog window, confirming the 
creation of a new file: 
 
Notepad will warn us that no such file exists  and will ask us if we want to 
create it. We click [Yes]. The next step is to rewrite or simply Copy / Paste the 
program’s source code. 
 


We save it by pressing [Ctrl+S] and close the Notepad editor with [Alt+F4]. 
Now we have the initial code of our sample C# program, written in the file 
C:\IntroCSharp\HelloCSharp.cs.

### Compiling C# Programs in Windows

The only thing left to do is to compil e and execute it. Compiling is done by 
the csc.exe compiler. 
 
We got our first error – Windows cannot find an executable file or command 
with the name "csc". This is a very common problem and it is normal to 
appear if it is our first time using C#. Several reasons might have caused it: 
- The .NET Framework is not installed; 
- The .NET Framework is installed  correctly, but its directory 
Microsoft.NET\Framework\v4.0.xxx is not added to the system path 
for executable files and Windows cannot find csc.exe. 
The first problem is easily solved by installing the .NET Framework (in our 
case – version 4.5). The other problem can be solved by changing the system 
path (we will do this later) or by using the full path to csc.exe, as it is shown 
on the figure below. In our case, the full file path to the C# compiler is 
C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe (note that this 
path could vary depending o n the .NET framework version installed). Strange 
or not, .NET 4.5  coming with Visual Studio 2012 and C# 5 in stalls in a 
directory named “v4.0.30319” – this is not a mistake.

### Compiling and Running C# Programs in Windows

Now let’s invoke the csc compiler through it s full path and pass to it the file 
we want to compile as a parameter (HelloCSharp.exe): 
 


After the execution csc is completed without any errors, and we get the 
following file as a result: C:\IntroCSharp\HelloCSharp.exe. To run it, we 
simply need to write its name. The result of the execution of our program is 
the message "Hello, C#!" printed on the con sole. It is not great but it is a 
good start:

### Changing the System Paths in Windows

If we know to use the command line C# compiler ( csc.exe) without entering 
the full path to it, we could add its folder to the Windows system path. 
1. We open Control Panel  and select " System". As a result this well -
known window appears (the screenshot is taken from Windows 7): 
 
  In Windows 8 it might look a bit different, but is almost the same: 


 
2. We select " Advanced system settings ". The dialog window " System 
Properties" appears: 
 


3. We click the button " Environment Variables " and a window with all 
the environment variables shows up: 
 
4. We choose "Path" from the list of System variables, as shown on the 
figure, and press the "Edit" button. A small window appears, in which we 
enter the path to the directory where the .NET Framework is installed: 
 
Of course, first we need to find where our .NET Framework is installed. 
By default it is located somewhere inside the Windows system directory 
C:\Windows\Microsoft.NET, for example: 


C:\Windows\Microsoft.NET\Framework64\v4.0.30319 
Adding the additional path to the already existing ones in the Path 
variable of the environment is done by adjoining the path name to the 
others and using a semicolon (;) as a spacer. 
 
We must be careful because if we delete any of the existing 
system paths, some of Windows’ functions or part of the 
installed software might fail to operate properly! 
5. When we are done with setting the path, we can try running csc.exe, 
without entering its full path. To do s o, we open a new cmd.exe 
(Command Prompt) window (it is important to restart the Command 
Prompt) and type in the "csc" command. We should see the C# 
compiler version and a message that no input file has been specified:

> [!TIP]
> **Modern C# Note: Cross-Platform .NET**
> Nakov's book discusses the Windows-only *.NET Framework 4.5*. Since 2016, Microsoft has moved to a unified, open-source, and cross-platform framework simply called **.NET** (currently .NET 8/9). You can run this C# code natively on Windows, macOS, and Linux.
