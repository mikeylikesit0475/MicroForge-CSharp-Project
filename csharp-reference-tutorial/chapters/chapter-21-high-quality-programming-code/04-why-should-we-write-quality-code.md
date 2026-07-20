# Why Should We Write Quality Code?

Let’s have a look again at our example: 
```csharp
static void Main() 
{ 
  int value=010, i=5, w; 
  switch(value){case 10:w=5;Console.WriteLine(w);break;case 
9:i=0;break; 
          case 8:Console.WriteLine("8 ");break; 

     default:Console.WriteLine("def ");{ 
         Console.WriteLine("hoho "); } 
     for (int k = 0; k < i; k++, Console.WriteLine(k - 
'f'));break;} { Console.WriteLine("loop!"); } 
} 
```
Can you tell whether this code compiles without errors? Can you tell what 
it does just by glancing at it? Can you add new functionality  and be sure 
that you will not break it up? Can you tell what the purpose of the k or the w 
variable is? 
Visual Studio has an option for automatic code formatting. If the above 
code is put there and that option is invoked (via the keyboard combination 
[Ctrl+K, Ctrl+F])  it will be reformatted and will look completely differently. 
Unfortunately, the purpose of the variables will still remain unclear, but at  
least it should become obvious where each block ends: 
```csharp
static void Main() 
{ 
 int value = 010, i = 5, w; 
```
 switch (value) 
```csharp
 { 
  case 10: w = 5; Console.WriteLine(w); break; 
  case 9: i = 0; break; 
  case 8: Console.WriteLine("8 "); break; 
  default: Console.WriteLine("def "); 
   { 
    Console.WriteLine("hoho "); 
   } 
   for (int k = 0; k < i; k++, Console.WriteLine(k - 'f')); 
   break; 
 } { Console.WriteLine("loop!"); } 
} 
```
If everyone was writing code as in the above example, it would not be 
possible to create big and serious software projects, because they are written 
by large teams of software engineers. If every team member ’s code was like 
that, no one would ever be able to understand  how the other members’ 
code works (and whether it works at all), and  hardly anyone could even 
understand his / her own code. 
Over the time, a serious amount of good practices have emerged and a lot 
of experience has been gained for writing quality code, to enable each 
programmer to understand and maintain his colleagues’ c ode. These 
practices endorse a variety of rules and recommendations for code 
formatting, identifier naming and proper program structure, all of which 
make writing software easier. Consistent and quality code is especially helpful 

when changing and maintaining a program . Quality code is flexible and 
stable. Because it is self -documenting and intuitive, its intentions become 
clear at a first sight. Quality code is easy to reuse  because it does just one 
thing (strong cohesion), but does it well, depending on a  minimal amount of 
other components (loose coupling), using only their public interfaces. As an 
end result, quality code saves time and labor, and makes the produced 
software more valuable. 
Some programmers consider quality code as being overly simple. The y tend 
to think that it limits their opportunity to demonstrate their knowledge. That 
is the reason why they write code that is hard to read , and for using 
features of the language which are unpopular or poorly documented. They 
squeeze functions on a single line. This is an entirely wrong practice.

## Coding Conventions

Before continue with the recommendations on writing quality code, we should 
talk a bit about coding conventions. A coding convention is a set of rules 
for writing code, used within the boundaries of a particular organization or a 
project. It can include naming and formatting rules, and rules for logical 
composition. One such rule would recommend that class names start with a 
capital letter while variable names start with a lowercase letter. Anot her rule 
may state that the opening curly bracket preceding a block of statements 
should be on the same line, rather than on a new line. 
 
Inconsistent usage of a single convention is worse and more 
dangerous than not having a convention at all. 
Conventions started to emerge in big and serious projects, where a large 
number of programmers had each been writing in their own style and 
everyone was adhering to their own (if any) rules. This was making the code 
harder to read and has forced project ma nagers to introduce written rules. 
Later, the best coding conventions gained popularity and have become a de 
facto standard. 
Microsoft provides an official coding convention  called .NET Framework 
Guidelines and Best Practices for .NET 4.5 (http://msdn.microsoft.com/en-
us/library/ms184412.aspx). 
Since then, this coding convention has gained significant popularity and has 
become very widespread. The naming and formatting rules  presented here 
are in sync with the above convention from Microsoft. 
Large organizations adhere to strict conventions . Among separate teams, 
conventions may differ, however. Most team leaders choose to stick with the 
official convention of Microsoft, and they eventually extend it when necessary. 
 
Code quality is not just a set of rules, which must be adhered 
to; it is rather a way of thinking.

## Managing Complexity

The management of complexity plays a central role when writing software. 
The main objective is to reduce the complexity that each member has to 
deal with at a certain moment. This way the brain of each of the members is 
burdened with less stuff to think about. 
The complexity management  starts from the architecture and the design. 
Each and every m odule (or rather, each autonomous code unit) should be 
designed with reducing complexity in mind. 
Good practices should be applied at all levels – classes, methods, member 
variables, naming, operators, error handling, formatting, comments, etc. They 
transform a lot of the decisions about the code in a strictly -defined set of 
rules, which enables a developer to think about one thing less while reading 
and writing code. 
The complexity management can be approached in another way: it is 
especially helpful for a developer to be able to abstract himself away from 
the big picture while writing a small piece of code . For that to be 
possible, the piece of code should have very c lear boundaries, which are in-
tact with the big picture. The old Roman rule “Divide and conquer” still applies 
when complexity is concerned. 
The rules we are talking about later on are directed exactly towards 
eliminating complexity while working on a single, small piece of the system.