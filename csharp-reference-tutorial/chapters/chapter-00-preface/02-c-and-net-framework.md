# C# and .NET Framework

This book is about programming. It is intended to teach you to think as a 
programmer, to write code, to think in data structures and algorithms and to 
solve problems. 
We use C# and Microsoft .NET Framework (the platform behind C#) only 
as means for writing programming code and we do not scrutinize the 
language’s specifics. This same book can be found in versions for other 
languages like Java and C++, but the differences are not very significant. 


Nevertheless, let’s give a short account of C# (pronounced "see sharp"). 
 
C# is a modern programming language for development of 
software applications. 
If the words "C#" and ".NET Framework" are unknown to you, you’ll learn in 
details about them and their connection in the next chapter. Now let’s explain 
briefly what C#, .NET, .NET Framework, CLR a nd the other technologies 
related to C# are.

## The C# Programming Language

C# is a modern object -oriented, general -purpose programming 
language, created and developed by Microsoft together with the .NET 
platform. There is highly diverse software developed wi th C# and on the .NET 
platform: office applications, web applications, websites, desktop applications, 
mobile applications, games and many others. 
C# is a high-level language that is similar to Java and C++ and, to some 
extent, languages like Delphi, VB.NE T and C. All C# programs are object -
oriented. They consist of a set of definitions in classes that contain methods 
and the methods contain the program logic – the instructions which the 
computer executes. You will find out more details on what a class, a m ethod 
and C# programs are in the next chapter. 
Nowadays C# is one of the most popular programming languages . It is 
used by millions of developers worldwide. Because C# is developed by 
Microsoft as p art of their modern platform for development and execution of 
applications, the .NET Framework, the language is widely spread among 
Microsoft-oriented companies, organizations and individual developers. For 
better or for worse, as of this book writing, the  C# language and the .NET 
platform are maintained and managed entirely by Microsoft  and are not 
open to third parties. Because of this, all other large software corporations 
like IBM, Oracle and SAP base their solutions on the Java platform and use 
Java as their primary language for developing their own software products.  
Unlike C# and the .NET Framework, the Java language and platform are 
open-source projects that an entire community of software companies, 
organizations and individual developers take part in. The standards, the 
specifications and all the new features in the world of Java are developed by 
workgroups formed out of the entire Java commun ity, rather than a single 
company (as the case of C# and .NET Framework). 
The C# language is distributed together with a special environment on which 
it is executed, called the Common Language Runtime (CLR) . This 
environment is part of the platform .NET Fr amework, which includes CLR, a 
bundle of standard libraries providing basic functionality, compilers, 
debuggers and other development tools. Thanks to the framework CLR 
programs are portable and, once written they can function with little or no 
changes on various hardware platforms and operating systems. C# programs 


are most commonly run on MS Windows, but the .NET Framework and CLR 
also support mobile phones and other portable devices based on Windows 
Mobile, Windows Phone and Windows 8. C# programs can st ill be run under 
Linux, FreeBSD, iOS, Android, MacOS X and other operating systems through 
the free .NET Framework implementation Mono, which, however, is not 
officially supported by Microsoft.

## The Microsoft .NET Framework

The C# language is not distribute d as a standalone product – it is a part of 
the Microsoft .NET Framework platform (pronounced "Microsoft dot net 
framework"). .NET Framework generally consists of an environment for the 
development and execution of programs, written in C# or some other 
language, compatible with .NET (like VB.NET, Managed C++, J# or F#). It 
consists of: 
- the .NET programming languages (C#, VB.NET and others); 
- an environment for the execution of managed code ( CLR), which 
executes C# programs in a controlled manner; 
- a set of development tools, such as the csc compiler, which turns C# 
programs into intermediate code (called MSIL) that the CLR can 
understand; 
- a set of standard libraries , like ADO.NET, which allow access to 
databases (such as MS SQL Server or MySQL) and WCF which connects 
applications through standard communication frameworks and protocols 
like HTTP, REST, JSON, SOAP and TCP sockets. 
The .NET Framework is part of every modern Windows distribution and is 
available in different versions. The latest version can be down loaded and 
installed from Microsoft’s website. As of this book’s publishing, the latest 
version of the .NET Framework is 4.5 . Windows Vista includes out -of-the-
box .NET Framework 2.0, Windows 7 – .NET 3.5 and Windows 8 – .NET 4.5.

## Why C#?

There are many re asons why we chose C# for our book. It is a modern 
programming language, widely spread, used by millions of programmers  
around the entire world. At the same time C# is a very simple and easy to 
learn (unlike C and C++). It is natural to start with a langua ge that is 
suitable for beginners while still widely used in the industry by many large 
companies, making it one of the most popular programming languages  
nowadays.

### C# or Java?

Although this can be extensively discussed, it is commonly acknowledged that 
Java is the most serious competitor to C# . We will not make a 
comparison between Java and C#, because C# is undisputedly the better, 

more powerful, richer and just better engineered. But, for the purposes of this 
book, we have to emphasize that any modern pr ogramming language will be 
sufficient to learn programming and algorithms. We chose C#, because it is 
easier to learn  and is distributed with highly convenient, free integrated 
development environment (e.g. Visual C# Express Edition). Those who prefer 
Java can prefer to use the Java version of this book, which can be found here: 
www.introprogramming.info.

### Why Not PHP?

With regards to programing languages popularity, besides C# and Java, 
another widely used language is PHP. It is suitable for developing small web 
sites and web applications, but it gives rise to serious difficulties when 
implementing large and complicated  software systems. In the software 
industry PHP is used first and foremost for small projects , because it can 
easily lead developers into writing code that is bad, disorganized and hard to 
maintain, making it inconvenient for more substantial projects. Thi s subject is 
also debatable, but it is commonly accepted that, because of its antiquated 
concepts and origins it is built on and because of various evolutionary 
reasons, PHP is a language that tends towards low -quality 
programming, writing bad code and cre ating hard to maintain software. PHP 
is a procedural language in concept and although it supports the paradigms of 
modern object -oriented programming, most PHP programmers write 
procedurally. PHP is known as the language of "code monkeys" in the 
software e ngineering profession, because most PHP programmers write 
terrifyingly low-quality code. Because of the tendency to write low -quality, 
badly structured and badly organized programming code, the entire concept 
of the PHP language and platform is considered wrong and serious companies 
(like Microsoft, Google, SAP, Oracle and their partners) avoid it. Due to this 
reason, if you want to become a serious software engineer, start with C# or 
Java and avoid PHP (as much as possible). 
Certainly, PHP has its uses in the world of programming  (for example 
creating a blog with WordPress, a small web site with Joomla or Drupal, or a 
discussion board with PhpBB), but the entire PHP platform is not well -
organized and engineered for large systems like .NET and Java. When it 
comes to non-web-based applications and large industrial projects, PHP is not 
by a long shot among the available options. Lots and lots of experience is 
necessary to use PHP correctly and to develop high -quality professional 
projects with it. PHP developer s usually learn from tutorials, articles and low -
quality books and pick up bad practices and habits, which then are hard to 
eradicate. Therefore, do not learn PHP as your first development 
language. Start with C# or Java. 
Based on the large experience of t he authors' collective we advise you to 
begin programming with C# and ignore languages such as C, C++ and PHP 
until the moment you have to use them.

### Why Not C or C++?

Although this is also debatable, the C and C++ languages are considered 
complex and requires deep understanding of hardware . They still have their 
uses and are suitable for low-level programming  (e.g. programming for 
specialized hardware devices), but we do not advise you to use C / C++ when 
you are beginner who wants to learn programming. 
You can program in pure C, if you have to write an operating system, a 
hardware device driver or if you want to program an embedded device, 
because of the lack of alternatives and the need to control the hardware very 
carefully. The C language is very low -level and in no way do we advise 
you to begin programming with it. A programmer’s productivity under pure C 
is many times lower compared to their productivity under modern general -
purpose programming languages like C# and Java. A variant of C is used 
among Apple / iPhone developers, but not because it is a good language, but 
because there is no decent alternative. Most Apple -oriented programmers do 
not like Objective-C, but they have no choice in writing in something else.  In 
2014 Apple promoted their new lang uage Swift, which is of higher level and 
aims to replace Objective-C for the iOS platform. 
C++ is good when you have to program applications that require very close 
work with the hardware  or that have special performance requirements 
(like 3D games). For all other purposes (like Web applications development or 
business software) C++ is inadequate. We do not advise you to pursue it, if 
you are starting with programming just now. One reason it is still being 
studied in some schools and universities is heredit ary, because these 
institutions are very conservative. For example, the International Olympiad in 
Informatics (IOI) continues to promote C++ as the only language permitted 
to use at programming contests, although C++ is rarely used in the 
industry. If you don’t believe this, look through some job search site and 
count the percentage of job advertisements with C++. 
The C++ language lost its popularity mainly because of the inability to quickly 
write quality software with it. In order to write high -quality software in C++, 
you have to be an incredibly smart and experienced programmer, whereas 
the same is not strictly required for C# and Java. Learning C++ takes 
much more time  and very few programmers know it really well. The 
productivity of C++ programmers is many times lower than C#’s and that is 
why C++ is losing ground. Because of all these reasons, the C++ language 
is slowly fading away and therefore we do not advise you to learn it.

## Advantages of C#

C# is an object-oriented programming language. Such are a ll modern 
programming languages used for serious software systems (like Java and 
C++). The advantages of object -oriented programming are brought up in 
many passages throughout the book, but, for the moment, you can think of 
object-oriented languages as lan guages that allow working with objects from 
the real world (for example student, school, textbook, book and others). 

Objects have properties (e.g. name, color, etc.) and can perform actions (e.g. 
move, speak, etc.). 
By starting to program with C# and the . NET Framework platform, you are on 
a very perspective track . If you open a website with job offers for 
programmers, you’ll see for yourself that the demand for C# and .NET 
specialists is huge and is close to the demand for Java programmers. At the 
same time, the demand for PHP, C++ and other technology specialists is far 
lower than the demand for C# and Java engineers. 
For the good programmer, the language they use is of no significant meaning, 
because they know how to program. Whatever language and technology 
they might need, they will master it quickly. Our goal is not to teach you 
C#, but rather teach you programming!  After you master the 
fundamentals of programming and learn to think algorithmically, when you 
acquaint with other programming languages ,  you will see for yourself how 
much in common they have with C# and how easy it will be to learn them. 
Programming is built upon principles that change very slowly over the years 
and this book teaches you these very principles.

## Examples Are Given in C# 5 and Visual Studio 2012

All examples in this book are with regard to version 5.0 of the C# language 
and the .NET Framework 4.5  platform, which is the latest as of this book’s 
publishing. All examples on using the Visual St udio integrated development 
environment are with regard to version 2012 of the product, which were also 
the latest at the time of writing this book. 
The Microsoft Visual Studio 201 2 integrated development environment 
(IDE) has a free version, suitable for beginner C# programmers, called 
Microsoft Visual Studio Express 2012  for Windows Desktop . The difference 
between the free and the full version of Visual Studio (which is a commercial 
software product) lies in the availability of some functionalities, which we will 
not need in this book. 
Although we use C# 5 and Visual Studio 201 2, most examples in this book 
will work flawlessly under .NET Framework 2.0  / 3.5 / 4.0  and C# 2.0 / 3.5 / 
4.0 and can be compiled under Visual Studio 2005 / 2008 / 2010. 
It is of no  great significance which version of C# and Visual Studio you’ll use 
while you learn programming. What matters is that you learn the principles 
of programming and algorithmic thinking ! The C# language, the .NET 
Framework platform and the Visual Studio inte grated development 
environment are just tools and you can exchange them for others at any time.  
If you read this book and VS2012 is not currently the latest, be sure almost 
all of this book’s content will still be the same due to backward compatibility.