# Visual Studio IDE

So far we  have examined how to compile and run C# programs using the 
Windows console (Command Prompt). Of course, there is an easier way to 
do it – by using an integrated development environment, which will execute 
all the commands we have used so far. Let’s take a look at  how to work with 
development environments (IDE) and how they will make our job easier.

## Integrated Development Environments

In the previous examples, we examined how to compile and run a program 
consisting of a single file. Usually programs are made of man y files, 
sometimes even tens of thousands. Writing in a text editor, compiling and 
executing a single file program from the command prompt are simple, but to 
do all this for a big project can prove to be a very complex and time -
consuming endeavor. There is  a single tool  that reduces the complexity, 
makes writing, compiling and executing software applications easier – the so 
called Integrated Development Environment  (IDE). Development 
environments usually offer many additions to the main development function s 


such as debugging, unit testing, checking for common errors, access to a 
repository and others.

## What Is Visual Studio?

Visual Studio is a powerful integrated environment ( IDE) for developing 
software applications for Windows and the .NET Framework platfo rm. Visual 
Studio (VS) supports different programming languages  (for example C#, 
VB.NET and C++) and different software development technologies  
(Win32, COM, ASP.NET, ADO.NET Entity Framework, Windows Forms, WPF, 
Silverlight, Windows Store apps and many mo re Windows and .NET 
technologies). It offers a powerful integrated environment for writing code, 
compiling, executing, debugging and testing applications, designing user 
interface (forms, dialogs, web pages, visual controls and others), data and 
class modeling, running tests and hundreds of other functions. 
IDE means “integrated development environment” – a tool where you write 
code, compile it, run it, test it, debug it, etc. and everything is integrated  
into a single place. Visual Studio is typical example of development IDE.  
.NET Framework 4.5 comes with Visual Studio 2012  (VS 2012). This is the 
latest version of Visual Studio as of March 2013. It is designed for C# 5, .NET 
4.5 and Windows 8 development. 
VS 2012 is a commercial product but has a free version called Visual Studio 
Express 2012, which can be downloaded for free from the Microsoft website 
at http://microsoft.com/visualstudio/downloads. 
Visual Studio 2012 Express  has several editions (for Desktop, for Web , for 
Windows 8 and others). If you want to write C# code following the content of 
this book, you may use Visual Studio 2012 Express for Desktop  or check 
whether you have a free licen se of the full Visual Studio from your University 
or organization. Many academic institutions (like Sofia University and Telerik 
Software Academy) provide free Microsoft DreamSpark accounts to their 
students to get licensed Windows, Visual Studio, SQL Serv er and other 
development tools. If you are student, ask your university administration 
about the DreamSpark program. Most universities worldwide are members of 
this program. 
In this book we will take a look at only the most important functions of VS 
Express 2012  – the ones related to coding. These are the functions for 
creating, editing, compiling, executing and debugging programs. 
Note that older Visual Studio versions such as VS 2010  and VS 2008  can 
also be used for the examples in this book but their use r interface might look 
slightly different. Our examples are based on VS 2012 on Windows 8. 
Before we continue with an example, let’s take a more detailed look of the 
structure of Visual Studio 2012’s visual interface. Windows are the main 
part of it. Each of them has a different function tied to the development of 
applications. Let’s see how Visual Studio 2012  looks after the default 
installation and configuration: 

 
Visual Studio  has several windows that we will explore (see the figure s 
above and below): 
- Start Page – from the start page we can easily open any of our latest 
projects or start a new one, to create our first C# program or to get 
help how to use C#. 
- Code Editor – keeps the program’s source code and allows opening and 
editing multiple files. 
- Error List – it shows the errors in the program we develop (if any). We 
learn how to use this window later when we compile C# programs in 
Visual Studio. 
- Solution Explorer – when no project is loaded, this window is empty, 
but it will become a part of our live s as C# programmers. It will show 
the structure of our project – all the files it contains, regardless if they 
are C# code, images or some other type of code or resources. 
- Properties – holds a list of the current object’s properties. Properties 
are used ma inly in the component -based programming, e.g. when we 
develop WPF, Windows Store or ASP.NET Web Forms application. 


 
There are many other windows with auxiliary functionality in Visual Studio but 
we will not review them at this time.

## Creating a New C# Project

Before doing anything else in Visual Studio, we must create a new project  
or load an existing one. The project groups many files, designed to implement 
a software application or system, in a logical manner. It is recommended that 
we create a separate project for each new program. 
We can create a project in Visual Studio by following these steps: 
- File -> New Project … 
- The “New Project” dialog appears and lists all the different types of 
projects we can create. We can choose a project type  (e.g. Console 
Application or WPF Application), programming language  (e.g. C# or 
VB.NET) and .NET Framework version (e.g. .NET Framework 4.5) and 
give a name to our project (in our case “IntroToCSharp”): 


 
- We choose Console Application . Console applications are programs, 
which use the console as a default input and output. Data is entered 
with the keyboard and when a result needs to be printed it appears on 
the console (as text on the screen in the program window). Aside from 
console applications, we can create application s with a graphical user 
interface (e.g. Windows Forms or WPF), Web applications, web services, 
mobile applications, Windows Store apps, database projects and others. 
- In the field "Name" we enter the name of the project. In our case we 
choose the name IntroToCSharp. 
- We press the [OK] button. 
The newly created project is now shown in the Solution Explorer. Also, our 
first file, containing the program code, is automatically added. It is named 
Program.cs. It is very important to give meaningful names  to ou r files, 
classes, methods and other elements of the program, so that we can easily 
find them and navigate the code.  A meaningful name  means a name that 
answers the question “what is the intent of this file / class / method / 
variable?” and helps developers to understand how the code works. Don’t use 
Problem3 for a name, even if you are solving the problem 3 from the 
exercises. Name your  project / class by its purpose. If your project is well 
named, after few months or a year you will be able to explain what it is 
intended to do without opening it and looking inside. Problem3 says nothing 
about what this project actually does. 
In order to  rename the Program.cs file, we right click on it in the Solution 
Explorer and select "Rename". We can name the  main file of our C# program 
HelloCSharp.cs. Renaming a file can also be done with the [F2] key when 
the file is selected in the Solution Explorer: 


 
A dialog window appears asking us if we want to rename class name as well 
as the file name. We select "Yes". 
 


After we complete all these steps we have our first console application named 
IntroToCSharp and containing a single class HelloCSharp (stored in the file 
HelloCSharp.cs): 
 
All we have to do is add code to the Main() method. By default, the 
HelloCSharp.cs code should be loaded and ready for editing. If it is not, we 
double click on the HelloCSharp.cs file in the Solution Explorer to load it. We 
enter the following source code:

## Compiling the Source Code

The compiling process in Visual Studio includes several steps: 
- Syntax error check; 
 
- A check for other errors, like missing libraries; 
- Converting the C# code into an executable file (a .NET as sembly). For 
console applications it is an .exe file. 
To compile a file in Visual Studio, we press the [F6] key or [Shift+Ctrl+B]. 
Usually, errors are underlined in red, to attract the programmer’s attention, 
while we are still writing or when compiling, a t the latest. They are listed in 
the "Error List" window if it is visible (if it is not , we can show it  from the 
"View" menu of Visual Studio). 
If our project has at least one error, it will be marked with a small red " x" in 
the "Error List" window. Short info about the problem is displayed for each 
error – filename, line number and project name. If we double click any of the 
errors in the "Error List", Visual Studio will automatically take us to the file 
and line of code where the error has occurred.  In the screenshot above the 
problem is that we have “using Systema;” instead of “using System”.

## Starting the Project

To start the project, we press [Ctrl+F5] (holding the [Ctrl] key pressed and 
at the same time pressing the [F5] key). 
The program will start and  the result will be displayed on the console, 
followed by the "Press any key to continue . . ." message: 
 


The last message is not part of the result produced by the program. It is a 
reminder by Visual Studio that our program has finished its execution  
and it gives us time to see the result. If we run the program by only pressing 
[F5], that message will not appear and the result will vanish instantly after 
appearing because the program will have finished its execution, and the 
window will be closed. That is  why we should always start our console 
applications by pressing [Ctrl+F5]. 
Not all project types can be executed. In order to execute a C# project, it 
needs to have one class with a Main() method declared in the way described 
earlier in this chapter.

## Debugging the Program

When our program contains errors, also known as bugs, we must find and 
remove them, i.e. we need to debug the program. The debugging process 
includes: 
- Noticing the problems (bugs); 
- Finding the code causing the problems; 
- Fixing the code so that the program works correctly; 
- Testing to make sure the program works as expected after the changes 
are made. 
The process can be repeated several times until the program starts working 
correctly. After we have noticed the problem, we need to find the code 
causing it. Visual Studio can help by allowing us to check step by step  
whether everything is working as planned. 
To stop the execution of the program at designated positions we can pla ce 
breakpoints. The breakpoint is associated with a line of the program. The 
program stops its execution  on the lines with breakpoints, allowing for the 
rest of the code to be executed step by step. On each step we can check and 
even change the values of the current variables. 
Debugging is a sort of step by step slow motion execution of the program. It 
gives us the opportunity to easily understand the details of the code and see 
where exactly and why the errors have occurred. 
Let’s create an intentional error in our program , to illustrate how to use 
breakpoints. We will add a line to the program, which will create an exception 
during the execution (we will take a detailed look at exceptions in the 
"Exception Handling" chapter). 
For now let’s edit our program in the following way: 
HelloCSharp.cs 
```csharp
class HelloCSharp 

{ 
 static void Main() 
 { 
  throw new System.NotImplementedException( 
   "Intended exception."); 
  System.Console.WriteLine("Hello C#!"); 
 } 
} 
When we start the program again with [Ctrl+F5] we will get an error and it 
```
will be printed on the console: 
 
Let’s see how breakpoints will help us  find the problem. We move the 
cursor to the line with the opening bracket of the Main() method and press 
[F9] (by doing so we place a breakpoint on that line). A red dot appears, 
indicating that the program will stop there if it is executed in debug mode: 
 
Now we must start the program in debug mode. We select Debug -> Start 
Debugging or pr ess [F5]. The program will start and immediately stop at 
the first breakpoint it encounters. The line will be colored in yellow and we 
can execute the program step by step. With the [F10 ] key we move to the 
next line. 
When we are on a given line and it is colored in yellow, the code on that line 
is not executed yet. It executes once we have passed that line. In this case 


we have not received the error yet despite the fact that we are on the line we 
added and should cause it: 
 
We press [F10] one more time t o execute the current line. This time Visual 
Studio displays a window specifying the line, where the error occurred as well 
as some additional details about it: 
 


Once we know where exactly the problem in the program is, we can easily 
correct it. To do so, first, we need to stop the execution of the program before 
it is finished. We select Debug –> Stop Debugging  or press [Shift+F5]. 
After that we delete the problem line and start the program in normal mode 
(without debugging) by pressing) [Ctrl+F5].

> [!TIP]
> **Modern C# Note: CLI and VS Code**
> Today, you don't need a heavy IDE like Visual Studio to compile and run C# programs. You can use the lightweight editor **Visual Studio Code (VS Code)** with the C# Dev Kit extension and the **dotnet CLI**:
> - Create a project: `dotnet new console`
> - Run a project: `dotnet run`
