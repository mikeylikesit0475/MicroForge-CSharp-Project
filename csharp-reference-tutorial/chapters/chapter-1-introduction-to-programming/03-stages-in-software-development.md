# Stages in Software Development

Writing software can be a very complex and time -consuming task, involving a 
whole team of software engineers and other specialists. As a result, many 
methods and practices, which make the life of programmers easier, have 
emerged. All they have in common is that the development of each software 
product goes through several different stages: 
- Gathering the requirements for the product and creating a task; 
- Planning and preparing the architecture and design; 


- Implementation (includes the writing of program code); 
- Product trials (testing); 
- Deployment and exploitation; 
- Support. 
Implementation, testing, deployment and support are mostly accomplished 
```csharp
using programming.
```

## Gathering the Requirements

In the beginning, only the idea for a certain product exists. It includes a list of 
requirements, which define actions by the user and the computer. In the 
general case, these actions make already existing activities easier – 
calculating salaries, calculating ballistic trajectories or searching for the 
shortest route on Google maps are some examples. In many cases the 
software implements a previously nonexistent functionality such as 
automation of a certain activity. 
The requirements for the product are usually defined in the form of 
documentation, written in English or any other language. There is no 
programming done at this stage. The requirements are defined by experts, 
who are familiar with the problems in a certain field. They can a lso write them 
up in such a way that they are easy to understand by the programmers. In 
the general case, these experts are not programming specialists, and they are 
called business analysts.

## Planning and Preparing the Architecture and Design

After all the requirements have been gathered comes the planning stage. At 
this stage, a technical plan for the implementation of the project is created, 
describing the platforms, technologies and the initial architecture (design) of 
the program. This step includes a f air amount of creative work, which is done 
by software engineers with a lot of experience. They are sometimes called 
software architects. According to the requirements, the following parts are 
chosen: 
- The type of the application – for example console application, desktop 
application (GUI, Graphical User Interface application), client -server 
application, Web application, Rich Internet Application (RIA), mobile 
application, peer-to-peer application or other; 
- The architecture of the software – for example sing le layer, double 
layer, triple layer, multi-layer or SOA architecture; 
- The programming language  most suitable for the implementation – 
for example C#, Java, PHP, Python, Ruby, JavaScript or C++, or a 
combination of different languages; 
- The technologies that will be used: platform (Microsoft .NET, Java EE, 
LAMP or another), database server (Oracle, SQL Server, MySQL, NoSQL 

database or another), technologies for the user interface (Flash, 
JavaServer Faces, Eclipse RCP, ASP.NET, Windows Forms, Silverlight, 
WPF or another), technologies for data access (for example Hibernate, 
JPA or ADO.NET Entity Framework), reporting technologies (SQL Server 
Reporting Services, Jasper Reports or another) and many other 
combinations of technologies that will be used for the imp lementation of 
the various parts of the software system. 
- The development frameworks that will simplify the development, e.g. 
ASP.NET MVC (for .NET), Knockout.js (for JavaScript), Rails (for Ruby), 
Django (for Python) and many others. 
- The number and skills of the people who will be part of the 
development team (big and serious projects are done by large and 
experienced teams of developers); 
- The development plan  – separating the functionality in stages, 
resources and deadlines for each stage. 
- Others (size of the team, locality of the team, methods of 
communication etc.). 
Although there are many rules facilitating the correct analysis and planning, a 
fair amount of intuition and insight is required at this stage. This step 
predetermines the further advancement of the development process. There is 
no programming done at this stage, only preparation.

## Implementation

The stage, most closely connected with programming, is the implementation 
stage. At this phase, the program (application) is implemented (written) 
according to the given task, design and architecture. Programmers 
participate by writing the program  (source) code. The other stages can 
either be short or completely skipped when creating a small project, but the 
implementation always presents; otherwise the process is not software 
development. This book is dedicated mainly to describing the skills used 
during implementation – creating a programmer’s mindset and building the 
knowledge to use all the resources provided by the C# language and the .NET 
platform, in order to create software applications.

## Product Testing

Product testing is a very important stage of so ftware development. Its 
purpose is to make sure that all the requirements are strictly followed and 
covered. This process can be implemented manually, but the preferred way to 
do it is by automated tests . These tests are small programs, which 
automate the trials as much as possible. There are parts of the functionality 
that are very hard to automate, which is why product trials include automated 
as well as manual procedures to ensure the quality of the code. 

The testing (trials) process is implemented by quality assurance engineers 
(QAs). They work closely with the programmers to find and correct errors 
(bugs) in the software. At this stage, it is a priority to find defects in the code 
and almost no new code is written. 
Many defects and errors are usually found during the testing stage and the 
program is sent back to the implantation stage. These two stages are very 
closely tied and it is common for a software product to switch between them 
many times before it covers all the requirements and i s ready for the 
deployment and usage stages.

## Deployment and Operation

Deployment is the process which puts a given software product into 
exploitation. If the product is complex and serves many people, this process 
can be the slowest and most expensive one.  For smaller programs this is a 
relatively quick and painless process. In the most common case, a special 
program, called installer, is developed. It ensures the quick and easy 
installation of the product. If the product is to be deployed at a large 
corporation with tens of thousands of copies, additional supporting software is 
developed just for the deployment. After the deployment is successfully 
completed, the product is ready for operation. The next step is to train 
employees to use it. 
An example would  be the deployment of a new version of Microsoft Windows 
in the state administration. This includes installation and configuration of 
the software as well as training employees how to use it. 
The deployment is usually done by the team who has worked on the  software 
or by trained deployment specialists . They can be system administrators, 
database administrators (DBA), system engineers, specialized consultants and 
others. At this stage, almost no new code is written but the existing code is 
tweaked and config ured until it covers all the specific requirements for a 
successful deployment.

## Technical Support

During the exploitation process , it is inevitable that problems will appear . 
They may be caused by many factors – errors in the software, incorrect usage 
or faulty configuration, but most problems occur when the users change their 
requirements. As a result of these problems, the software loses its abilities to 
solve the business task it was created for. This requires additional 
involvement by the developers and  the support experts . The support 
process usually continues throughout the whole life -cycle of the software 
product, regardless of how good it is. 
The support is carried out by the development team and by specially trained 
support experts . Depending on the  changes made, many different people 
may be involved in the process – business analysts, architects, programmers, 
QA engineers, administrators and others. 

For example, if we take a look at a software program that calculates salaries, 
it will need to be updated every time the tax legislation, which concerns the 
serviced accounting process, is changed. The support team’s intervention will 
be needed if, for example, the hardware of the end user is changed because 
the software will have to be installed and configured again.

## Documentation

The documentation stage is not a separate stage but accompanies all the 
other stages. Documentation is an important part of software development 
and aims to pass knowledge between the different participants in the 
development and support of a software product. Information is passed along 
between different stages as well as within a single stage. The development 
documentation is usually created by the developers (architects, program -
mers, QA engineers and others) and represents a combination of documents.

## Software Development Is More than Just Coding

As we saw, software development is much more than just coding (writing 
code), and it includes a number of other processes such as: requirements 
analysis, design, planning, testing and support, which require a wide variety 
of specialists called software engineers . Programming is just a small, but 
very essential part of software development. 
In this book we will focus solely on programming, because it is the only 
process, of the above, without which, we cannot develop software.