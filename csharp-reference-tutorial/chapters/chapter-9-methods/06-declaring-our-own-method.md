# Declaring Our Own Method

Before we learn how to declare our own method, it is important to know 
where we are allowed to do it.

## Where Is Method Declaration Allowed?

Although we still haven’t explained how to declare a class, we  have seen it in 
the exercises before. We know that every class has opening and closing curly 
brackets – "{" and " }", between which the program code is placed. More 
detailed description for this can be f ound in the chapter " Defining Classes ", 
however we mention it here, because a method exists only if it is declared 
between the opening and closing brackets of a class  – "{" and "}". In 
addition a method cannot be declared inside another method's body (this will 
be clarified later). 
 
In the C# language, a method can be declared only between 
the opening "{" and the closing "}" brackets of a class. 
A typical example for a method is the already known method Main(…) – that 
is always declared between the opening and the closing curly brackets of our 
```csharp
class. An example for this is shown below: 
```
HelloCSharp.cs 
```csharp
public class HelloCSharp 
{ // Opening brace of the class 
 


 // Declaring our method between the class' body braces 
 static void Main(string[] args) 
 { 
  Console.WriteLine("Hello C#!"); 
 } 
} // Closing brace of the class
```

## Method Declaration

To declare a method means to register the method in our program. This is 
shown with the following declaration: 
[static] <return_type> <method_name>([<param_list>]) 
There are some mandatory elements to declare method: 
- Type of the result, returned by the method – <return_type>. 
- Method’s name – <method_name>. 
- List of parameters to the method – <param_list> – it can be empty list 
or it can consist of a sequence of parameters declarations. 
To clarify the elements of method ’s declaration, we can use the Main(…) 
method from the example HelloCSharp show in the previous block: 
static void Main(string[] args) 
As can be seen the type of returned value  is void (i.e. that method does 
not return a result), the method ’s name is Main, followed by round brackets, 
between which is a list with the method ’s parameters. In the particular 
example it is actually only one parameter – the array string[] args. 
The sequence, in which the elements of a method are written, is strictly 
defined. Always, at the very first place, is the type of the value that method 
returns <return_type>, followed by the method ’s name <method_name> and 
list of parameters at the end <param_list> placed between in round brackets 
– "(" and " )". Optionally the declarations can have access modifi ers (as 
public and static). 
 
When a method is declared keep the sequence of its 
elements description: first is the type of the valu e that the 
method returns, then is the method’s name, and at the end is 
a list of parameters placed in round brackets. 
The list with parameters is allowed to be void (empty). In that case the only 
thing we have to do is to type " ()" after the method ’s name. Although the 


method has not parameters the round brackets must follow its name in the 
declaration. 
 
The round brackets – "(" and ")", are always placed after the 
method’s name, no matter whether it has or has not any 
parameters. 
For now we will no t focus at what <return_type> is. For now we will use 
void, which means the method will not return anything. Later, we will  see 
how that can be changed 
The keyword  static in the description of the declaration above  is not 
mandatory but should be used in sm all simple programs . It has a special 
purpose that will be explained later in this chapter. Now the methods that we 
will use for example, will include the keyword static in their declaration. 
More about methods that are not declared as static will be discussed in the 
chapter "Defining Classes", section "Static Members".

## Method Signature

Before we go on with the basic elements from the method ’s declaration, we 
must pay attention to something more important. In object -oriented 
programming a method is identified by a pair of elements of its declaration: 
name of the method, and list of parameters. These two elements define the 
so-called method spec ification (often can be found as a method 
signature). 
C#, as a language used for object oriented programming, also distinguishes 
the methods using their specification (signature) – method’s name 
<method_name> and the list with parameters – <param_list>. 
Here we must note that the type of returned value of a method is only part of 
its declaration, not of its signature. 
 
What identifies a method is its signature. The return type is 
not part of the method signature. The reason is that if two 
methods differ on ly by their return value types, for the 
program is not clear enough which of them must be called. 
A more detailed explanation on why the type of the returned value is not part 
of the method signature, you will find later in this chapter.

## Method Names

Every method solves a particular task from the whole problem that our 
program solves. Method’s name is used when method is called. Whenever 
we call (start) a particular method, we type its name and if necessary we pass 
values (if there are any). 
In the example below, the name of our method is PrintLogo: 


```csharp
static void PrintLogo() 
{ 
 Console.WriteLine("Microsoft"); 
 Console.WriteLine("www.microsoft.com"); 
}
```

## Rules to Name a Method

It is recommended, when declare a method, to follow the rules for method 
naming suggested by Microsoft: 
- The name of a method must start with capital letter. 
- The PascalCase rule must be applied, i.e. each new word, that 
concatenates so to form the method name, must start with capital 
letter. 
- It is recommended that the method name must consist of verb, or verb 
and noun. 
Note that these rules are not mandatory, but recommendable. If we aim our 
C# code to follow the style of all good programmers over the globe, we must 
use Microsoft ’s code conven tion. A more detailed recommendation about 
method naming will be given in the chapter " High-Quality Code ", section 
"Naming Methods". 
Here some examples for well named methods: 
Print 
GetName 
PlayMusic 
SetUserName 
And some examples for bad named methods: 
Abc11 
Yellow___Black 
foo 
_Bar 
It is very important that the method name describes the method ’s purpose. 
All behind this idea is that when a person that is not familiar with our program 
reads the method name, they can easily understand what that method does, 
without the need to look at the method’s source code. 
 
To name a method it is good to follow these rules: 
- Method name must describe the method’s purpose. 
- Method name must begin with capital letter. 


- The PascalCase rule must be applied. 
- The method name must consist of verb, or verb and 
noun.

## Modifiers

A modifier is a keyword in C#, which gives additional information to the 
compiler for a certain code. 
We have already met some modifiers – public and static. Now we will 
briefly describe what modifiers are actually. Detailed description will be given 
later in the ch apter "Defining Classes ", section " Access Modifiers ". So let ’s 
begin with an example: 
```csharp
public static void PrintLogo() 
{ 
 Console.WriteLine("Microsoft"); 
 Console.WriteLine("www.microsoft.com"); 
} 
With this example we define a public method by the modifier public. It is a 
```
special type modifier, called also access modifier and is used to show that 
method can be called by any C# class, no matter where it is. Public modifiers 
are not restricted in the meaning of “who” can call them. 
Another example for access modifier, that we can meet, is the modifier 
private. Its function i s opposite to that of the public, i.e. if a method is 
declared by access modifier private, it cannot be called from anywhere, 
except from the class in which it is declared. 
If a method is declared without an  access modifier  (either public or 
```csharp
private), it is accessible from all classes  in the current assembly, but not 
accessible for any other assemblies (let say from other projects in Visual 
```
Studio). For the same reason, when we are writing small programs, like those 
in this chapter, we will not specify access modifiers. 
For now, the only thing that has to be learned is that in method declaration 
there cannot be more than one access modifier. 
When a method has a keyword static, in its declaration, this method is 
called static. To call a static method there is no need to have an instance of a 
```csharp
class in which the static method is declared. For now the reader can accept 
that, the methods must be static. Dealing with non -static methods will be 
```
explained in the chapter "Defining Classes", section "Methods".