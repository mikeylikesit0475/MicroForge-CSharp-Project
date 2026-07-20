# Identifier Naming

Identifiers are the names of classes, interfaces, structures, enumerations, 
properties, methods , parameters  and variables. In C# and in many other 
languages, names are chosen by the developer . Names should n ot be 
random. They should be composed in such a way that they carry meaningful 
information about their purpose and their role in the code. This makes the 
code easier to read. 
When naming an identifier, it is good to ask yourself these questions: What 
does this class do? What is the purpose of this variable? What is that method 
being used for? What information does this parameter hold? 
Some good names  are: FactorialCalculator, studentsCount, Math.PI, 
configFileName, CreateReport. 
Some bad names are: k, k2, k3, junk, f33, KJJ, button1, variable, temp, 
tmp, temp_var, something, someValue. 
It is especially bad to have a class or a method called Problem12. Some 
beginner programmers would give such a name to their solution of Problem 
12 from the exercises. What wi ll the name Problem12 tell you in a week or a 
month? If the problem is about finding a path in a labyrinth, name it 
PathInLabyrinth. Three months later you may encounter a similar problem 
and you will be able to find the labyrinth problem. How would you find it if you 

have named it inappropriately? Do not give a name that contains digits – this 
is an indication for bad naming. 
 
The name of an identifier should describe its purpose. The 
solution of problem 12 from the exercises should NOT be 
called Problem12. That is a huge mistake!

## Avoid Abbreviations

Abbreviations should be avoided because they can be confusing. What does 
the class name GrBxPnl tell you? Isn’t GroupBoxPanel clearer? Exceptions are 
made for acronyms, which are more popular than their full form, for example 
HTML or URL. In that sense, HTMLParser is recommended over the excessive 
long name HyperTextMarkupLanguageParser.

## Use English

One of the most basic rules is to always use English. Would you be able to 
understand the code of a foreigner who names variables and methods in his 
own language? The one and only human language, which all programmers 
should know, is English. 
 
English is a de facto standard in writing software. Always use 
English for naming the identifiers in the code (variables, 
methods, classes, etc.). Use English for comments as well. 
Let’s see how we pick appropriate identifiers in different cases.

## Consistency in Naming

Naming should be consistent. What does this mean? 
In a group of methods called LoadFile(), LoadSettings(), LoadFont(), 
LoadImageFromFile() and LoadLibrary() it is inappropriate to have a 
method ReadTextFile(). The word Read is not consistent with Load. 
Opposite activities should be symmetrically named (you should be able to 
guess the name of the opposite activity just by knowing the complementing 
one): LoadLibrary() goes with UnloadLibrary(), but does not go with 
FreeHandle(). OpenFile() goes with CloseFile(), but does not go with 
DeallocateResource(). It  is unnatural to have AssignName next to a 
GetName / SetName pair. 
Notice that in .NET Framework class library , big groups of classes have 
consistent naming : collections (the namespace and all classes use the 
words like Collection and List, and never their  synonyms), streams are 
always Streams, etc. 


 
Use consistent names : use the same words for the same 
situations, do not use synonyms. Name opposite things 
symmetrically.

## Names of Classes, Interfaces and Other Types

From the chapter “Principles of Object -Oriented Programming ” we know that 
classes describe real -world objects. Class names should consist of a noun 
(denominative or substantive) and possibly a number of adjectives  (before 
or after the noun). For example, a class describing an African lion should be 
called AfricanLion. 
The recommended casing of the letters (small / capital letters)  for naming 
types in C# is Pascal Case: the first letter of every word in the name is 
always uppercase and the rest of the letters are lowercase. This way it is 
easier to read the identifier ’s name (compare the lowercase name 
idatagridcolumnstyleeditingnotificationservice to its Pascal Case 
version IDataGridColumnStyleEditingNotificationService). The latter is 
the public class with probably the longest name in the .NET Framework (46 
characters, from System.Windows.Forms). 
Let’s give a few more examples. We are to write a class, which  finds the 
prime numbers in a given range. A good name for that class is PrimeNumbers 
or PrimeNumbersFinder, or maybe PrimeNumbersScanner. Bad names  
would be FindPrimeNumber (a verb should not be used in the name of a 
```csharp
class) or Numbers (it is not clear what the numbers are and what we are doing 
with them) or Prime (a class name should not be an adjective).
```

### How Long Should Class Names Be?

In the common case, class names should not exceed 20 characters , but 
sometimes this rule is not adhered to if a real -world object is described which 
contains numerous lon ger words. As we saw above, it is possible to have a 
```csharp
class with a name that is 46 characters long. Although the name is long, it is 
very clear what this class does. Because of this, the recommendation for class 
```
names being less than 20 characters is only a dvisory, not mandatory. If you 
are about to choose between a class name that is short and clear and another 
one which is longer and as clear as the short one, prefer the short name. 
A bad advice would be to abbreviate names in order to keep them short. Are  
the following class names clear enough: CustSuppNotifSrvc, FNFException? 
Obviously they are not easy readable. Names like FileNotFoundException 
and CustomerSupportNotificationService are much clearer, although 
being longer.

### Naming Interfaces and Other Types

Interface names  should follow the same convention as class names: they 
are written in Pascal Case  and consist of a noun and possibly a few 


adjectives. In order to distinguish them from the rest of the types, the 
convention suggests prefixing them with an “I”. 
Some good examples are: IEnumerable, IFormattable, IDataReader, 
IList, IHttpModule, ICommandExecutor. 
Bad examples  would be: List, FindUsers, IFast, IMemoryOptimize, 
Optimizer, FastFindInDatabase, CheckBox. 
In .NET there is one more notation for nam ing interfaces: naming them so 
that they end in " able": ICloneable, IEnumerable, IFormattable. These 
are interfaces that most often augment the basic role of an object. Most 
interfaces, however, do not follow this notation, such as the IList and 
ICollection interfaces.

### Names of Enumeration Types

A few formats are allowed  for naming enumerations: [Noun] or [Verb] or 
[Adjective]. Names can be in singular or plural form. Every member of an 
enumerated type should be named in the same manner.  The below examples 
show correctly named enumerations: 
enum Days 
```csharp
{ 
```
 Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday 
}; 
 
enum Color 
```csharp
{ 
```
 Black, Red, Green, Blue, Yellow, Orange, Pink, Gray, White 
};

### Attribute Names

Attribute names in C# should be suffixed with Attribute. For example: 
WebServiceAttribute. Attributes are special annotations (metadata) for a 
```csharp
class / method or other piece of code which specify a special instruction for 
```
the compiler or the runtime. For more information see the  documentation in 
MSDN: http://msdn.microsoft.com/en-us/library/z0w1kczw(v=vs.110).aspx.

### Exception Names

The convention for naming exception classes suggests that exceptions end 
with Exception. The name should be informative  and Pascal case should be 
used just like when naming classes . A good example  of correctly named 
exception class would be FileNotFoundException. A bad example  for 
exception class is FileNotFoundError.

### Delegate Names

Delegates in C# and .NET Framework should be suffixed with Delegate or 
EventHandler. Thus DownloadFinishedDelegate would be a good example 
while WakeUpNotification would not adhere to the convention.  A delegate 
is a data type which holds a reference to method with compatible signature. 
For more information about delegates see the official documentation in MSDN: 
http://msdn.microsoft.com/en-us/library/ms173171(v=vs.110).aspx.

### Naming Namespaces

Namespaces, covered in details in the “Creating and Using Objects” chapter, 
should use Pascal Case like class names. The following forms are preferable: 
- Company.Product.Component… 
- Product.Component… 
Good example for naming a namespace is: Telerik.WinControls.GridView. 
Bad examples for naming namespaces are : Classes, TELERIK.CONSTANTS 
and Telerik_WinControlsGridView.

### Assembly Names

Assembly name s should match the name of the base namespace  which 
they hold. Good examples of correctly named assemblies are: 
- Telerik.WinControls.GridView.dll 
- Oracle.DataAccess.dll 
- Interop.CAPICOM.dll 
Improper (bad) assembly names are the following: 
- Telerik_WinControlsGridView.dll 
- OracleDataAccess.dll 
- Oracle.dll

## Method Names

Method names should be PascalCase, e.g. each separate word starts with an 
uppercase letter. 
Method names  should be constructed according to the following pattern: 
<verb> + <object>, for example PrintReport(), LoadSettings() or 
SetUserName(). The object can be a noun or a noun and an adjective: 
ShowAnswer(), ConnectToRandomTorrentServer() or FindMaxValue(). 
A method name should address what that method does. If you are not able 
to come up with a g ood name, you most probably have to review the method 
itself and whether it is decently written. 

Some bad examples for method names are: DoWork() (what kind of work?), 
Printer() (no verb), Find2() (why not Find7() then?), ChkErr() 
(abbreviations are not recommended), NextPosition() (no verb). 
Sometimes a single verb is a good name for a method, as long as it becomes 
clear what the particular method does and what objects it operates on. For 
example, within a Task class, the methods Start(), Stop() and Cancel() 
are well -named because it is clear that they start, stop or cancel a task. In 
other cases a single verb is inappropriate. For example, within an Utils class, 
methods called Evaluate(), Create() and Stop() are inadequate because 
the context is not entirely clear.

### Methods that Return a Value

Names of methods that return a value should describe the returned value  
in some way, e.g. GetNumberOfProcessors(), FindMinPath(), GetPrice(), 
GetRowsCount(), CreateNewInstance(). 
Bad examples for names of methods that return a value are the following: 
ShowReport() (it is not clear what the method returns), Value() (should be 
either GetValue() or HasValue()), Student() (no verb), Empty() (should be 
IsEmpty()). 
Whenever a value is returned, the measuring unit  should be clear: 
MeasureFontInPixels(…), instead of MeasureFont(…).

### Single Purpose of a Method

A method that does more than one thing is hard to be appropriately named: 
how would you call a method that does an annual income report, downloads  
software updates from the web and scans the system for viruses? Maybe 
CreateAnnualIncomesReportDownloadUpdatesAndScanForViruses? 
 
Methods should have one purpose only, solving only one 
task, not multiple tasks at the same time! 
Methods solving multiple tasks (weak cohesion ) cannot and should not be 
named properly. They must be refactored.

### Cohesion and Naming

A name should describe everything that the method does . If a suitable 
name cannot be found, it most probably means that the cohesion is weak, i.e. 
the method does many things and should be split up into separate methods. 
Here is an example: we have a method that sends an e -mail, prints a report 
and calculates the distance between two points in 3D Euclidean space. How 
would you call it? Maybe SendEmailAndPrintReportAndCalc3DDistance()? 
It is obvious that something is wrong with this method  – we should 
refactor it instead of striving to find a better name. It is even worse if that 
method is simply called SendEmail(). This way we are misleading other 


programmers that this method only sends email, while in reality it does many 
other things. The last is very, very bad practice. 
 
Naming a method misleadingly is even worse than naming it 
method1(). If a method calculates a cosine and we name it 
sqrt(), we will likely enrage other colleagues that are willing 
to use our code.

### How Long Should Method Names Be?

The same recommendations apply here as for classes – you should not 
abbreviate unless it is clear.  The names should be meaningful  and this is 
more important than its length. If the method name is too long (e.g. more 
than 50 characters), check whether it does a single task. 
Good method names  are: LoadCustomerSupportNotificationService(), 
Math.Sqrt(), CreateMonthlyAndAnnualIncomesReport(). 
Bad method names are LoadCustSuppSrvc(), CreateMonthIncReport().

### Method Parameters

Parameters should be named in the following form: [Noun] or [Adjective] 
+ [Noun] . Every word of the name should start with an uppercase letter, 
except for the first wo rd. This notation is called camelCase. As with any other 
code element, parameter naming should be meaningful and should carry 
useful information. 
Good examples of parameter names are the following : firstName, report, 
fontSizeInPixels, speedKmH, font, usersList. 
Bad examples of parameter names are : p, p1, p2, populate, LastName, 
last_name, convertImage.

## Property Names

Property names start with an uppercase letter  (PascalCase, like methods), 
but do not contain a verb (like variables). A property name consists of an 
[Adjective] + [Noun] or just [Noun]. 
In the presence of a property called X, it is not a good practice to have a 
method called GetX() – it will be confusing. 
If the property is of type  enumeration, you could think about naming the 
property like the enumeration type itself. In the presence of an enumeration 
called CacheLevel, the property would as well be called CacheLevel. 
Using the same name for the property and its type is allowed and is usual 
in .NET Framework class library. For example the property Cursor of the class 
Button in Windows Forms is of type Cursor.

## Variable Names

Variable names ( local variables in a method) and member -variables (fields in 
a class) should adhere to the camelCase notation, according to Microsoft. 
Variables should have a good name , as all other identifiers in the code 
should. A good variable name clearly and precisely describes the object that 
the variable holds. Good variable names are: account, blockSize and 
customerDiscount. Bad names are: r18pq, __hip, rcfd, val1, val2. 
A name should address the problem that the variable solves . It should 
answer the question “What?”, not “How?”.  In this sense, good names are 
employeeSalary, employees. Bad names are the ones that are irrelevant to 
the solved problem: myArray, customerFile, customerHashTable. 
 
Prefer names from the business domain in which the 
software operates , not from the technical names that come 
from the programming language: use CompanyNames rather 
than StringArray. 
The optimal length of a variable name is from 10 to 16 characters . The 
length of the name depends on the scope – variables with wider scope and a 
longer lifetime should have a more descriptive name: 
protected Account[] customerAccounts; 
Variables with a narrower scope and a shorter lifetime could have shorter 
length: 
for (int i=0; i < customers.Length; i++) { … } 
Variable names should be instantly understandable. Because of this it is not 
a good idea to remove vowels from the name in order to abbreviate it – 
btnDfltSvRzlts is not quite understandable. 
The most important thing is: whatever naming rules are chosen for variables, 
they should be applied consistently throughout the code – in all the modules 
of the project and by the whole team. An inconsistently applied rule is worse 
than not having a rule at all.

## Names of Boolean Identifiers

Parameters, properties and variables can be of a Boolean type. In this point 
we describe the specifics of these identifiers. 
Their names should be a prerequisite for either truth or falsehood. For 
example, names like canRead, available, isOpen and valid are good. 
Examples of inadequate names for Boolean variables are: student, read, 
reader. 


It would be useful if Boolean identifiers start with is, has or can (with an 
uppercase letter for properties), but only if this adds for clarity. 
Negated names should not be used (avoid prefixing with “ not”), because the 
following oddities may occur: 
if (! notFound) { … } 
Good examples: configFileLoaded, hasPendingPayment, customerFound, 
validAddress, positiveBalance, isPrime. 
Bad examples: notFound, fsafdashghg, run, programStop, player, list, 
findCustomerById, isUnsuccessfull.

## Named Constants

Like we already know from the chapter “ Defining Classes” constants in C# are 
something like static immutable variables and are defined as follows: 
```csharp
public struct Int32 
{ 
 public const int MaxValue = 2147483647; 
} 
```
Names of constants should be written in Pascal Case  or entirely in 
uppercase, with underscores between words (ALL_CAPS): 
```csharp
public static class Math 
{ 
 public const double PI = 3.14159265359; 
 public const double GoldenRatio = 1.61803398875; 
} 
```
Named constants should clearly describe  what the purpose  of the 
particular number, string or whatever value is, rather than the value itself. A 
constant named number314159 is useless and confusing. 
The official recommendation from Microsoft for naming the constants (const 
and readonly identifiers) is to use Pascal Case but some developers prefer 
the ALL_CAPS style which is widely used in C++ and Java.

## Naming of Specific Data Types

Names of variables used as counters are recommended to contain a word 
that specifies that, for example usersCount, rolesCount, filesCount. 
Variables that represent the state of an object should be named accordingly. 
A few examples: threadState, transactionState. 

Temporary variables should most often have common and short names , 
which make obvious that they are temporary, with a very short lifetime. Good 
examples are index, oldValue, count. Inappropriate names are a, aa, 
tmpvar1, tmpvar2. Although using names like tmp and temp is acceptable it is 
better to choose more meaningful name like oldValue and lastIndex.

## Naming by Prefixing or Suffixing

Prefix and suffix naming conventions do exist in older languages such as C. 
A very popular notation during many year s has been the Hungarian 
notation. Hungarian notation is a prefix naming notation in which every 
variable comes with a prefix that indicates its type and purpose. For example, 
in Win32 API, the name lpcstrUserName would mean a variable that is a 
pointer to  an array of characters, which  ends in 0, and is interpreted as a 
non-Unicode string. 
In C#, .NET Framework, Java and all modern programming languages , similar 
conventions have never gained popularity because the development 
environments are able to show the type of any variable. Do not use 
Hungarian notation in C#! Exceptions are made by some graphics libraries, 
to a certain extent.