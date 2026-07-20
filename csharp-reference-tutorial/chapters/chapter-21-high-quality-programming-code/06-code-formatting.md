# Code Formatting

Formatting, along with naming is one of the most basic prerequisit es for 
readable code. Without proper formatting , the code is not going to be 
readable, whatever rules for naming and code structuring are chosen. 
Formatting has two objectives: easier to read code , and, as a consequence 
– code that is easy to maintain . Formatting that makes the code harder to 
read is not good. Every aspect of formatting (indentation, empty lines, 
alignment, etc.) can provide benefits as well as cause harm. Formatting 
should follow the logical structure of the program  so that the logical 
understanding is supported. 
 
The formatting of a program should represent its logical 
structure. All formatting rules are introduced towards 
improving code readability by exposing its logical structure. 
In Visual Studio, the code can be automatically formatted with the [Ctrl+K, 
Ctrl+F] key combination. Different standards can be applied whenever auto 
formatting is performed – the Microsoft conventions as well as user -defined 
standards are available.  Try it yourself: select a piece of code and press 
[Ctrl+K] followed by [Ctrl+F]. 
Now we are going to review the formatting rules according to the coding 
convention of Microsoft for C#.

## Why Does Code Need Formatting?

First let’s look at the below example: 
```csharp
public   const    string                    FILE_NAME 
="example.bin"  ;  static void Main   (             ){ 
FileStream   fs=     new FileStream(FILE_NAME,FileMode 
.   CreateNew)   // Create the writer      for data  . 
;BinaryWriter w=new BinaryWriter     (    fs      );// 
```
Write data to                               Test.data. 
for(  int i=0;i<11;i++){w.Write((int)i);}w   .Close(); 
fs   .   Close  (  ) // Create the reader    for data. 
;fs=new FileStream(FILE_NAME,FileMode.            Open 
,  FileAccess.Read)     ;BinaryReader                r 
= new BinaryReader(fs);  // Read data from  Test.data. 
for (int i = 0; i < 11; i++){ Console      .WriteLine 
(r.ReadInt32                                       ()) 
;}r       .    Close   (   );  fs .  Close  (  )  ;  } 
Is that enough as an answer?

## Block Formatting

Blocks are surrounded by “{” and “}”. In C# t hey should be on separate 
lines (unlike in Java and JavaScript) . The contents of the block should be 
indented to the right by a single tab: 
if (some condition) 
```csharp
{ 
 // Block contents indented by a single [Tab] 
 // Don't use spaces for indentation 
} 
This rule applies for namespaces, classes, methods, conditional statements, 
```
loops, etc. 
Nested blocks are indented  additionally. The body of the class here is 
indented relative to the body of the namespace, and the body of the method 
is indented additionally, as well as the conditional statement: 
```csharp
namespace Chapter_21_Quality_Code 
{ 
 public class IndentationExample 
 { 
  private int Zero() 
  { 
   if (true) 

   { 
    return 0; 
   } 
  } 
 } 
}
```

## Rules for Formatting a Method

According to the Microsoft ’s coding convention, some formatting rules when 
declaring methods should be adhered to.

### Formatting Multiple Method Declarations

Whenever a class has more than one method, their declarations should be 
separated by an empty line: 
IndentationExample.cs 
```csharp
public class IndentationExample 
{ 
 
 public static void FirstMethod() 
 { 
  // … 
 } // One blank line follows 
 
 public static void SecondMethod() 
 { 
  // … 
 } 
}
```

### How to Put Parentheses?

The Microsoft coding convention suggests that a space should be put  
between a keyword (for, while, if, switch) and an opening parenthesis: 
while (!EOF) 
```csharp
{ 
 // … Code … 
} 
```
This is made for the keywords to stand out. 
Next to a method name and before an opening parenthesis, no whitespace 
should be present: 

```csharp
public void CalculateCircumference(int radius) 
{ 
 return 2 * Math.PI * radius; 
} 
```
In this line of thought, between the name of the method and the opening 
parenthesis "(" there should not be any whitespace (spaces, tabs etc.): 
```csharp
public static void PrintLogo() 
{ 
 // … Code … 
}
```

### Formatting the Parameter List of Methods: Space after Commas

When a method has many parameters, we should put a space between the 
previous comma and the type of the next parameter, but not before the 
comma: 
public void CalcDistance(Point startPoint, Point endPoint) 
Similarly, the same rule is applied when calling a  method with more than one 
parameter. Before the arguments preceded by a comma, a space should be 
put: 
DoSmth(1, 2, 3);

## Rules for Formatting of Types

When classes, interfaces, structures and enumerations are created, a few 
recommendations should be followed for formatting the code inside.

### Rules for Ordering the Contents of a Class

As we know, the class name is declared on the first line, preceded by the 
```csharp
class keyword: 
public class Dog 
{ 
```
Constants follow next. They should be ordered according to thei r access 
modifier – public constants are first, then protected and then private: 
 // Static variables 
```csharp
 public const string SPECIES = "Canis Lupus Familiaris"; 

Then follow the non -static fields. Like static fields, those labeled public are 
first, then protected and finally private fields follow: 
 // Instance variables 
 private int age; 
After non-static class fields, constructor declarations follow: 
 // Constructors 
 public Dog(string name, int age) 
 { 
  this.Name = name; 
  this.age = age; 
 } 
```
After the constructors, properties are declared: 
 // Properties 
```csharp
 public string Name { get; set; } 
```
Finally, after the properties, the methods are declared. It is recommended 
that methods are grouped by functionality, not by access level or scope. For 
example, a met hod with a private access modifier could easily be between 
two methods with a public modifier in order to make reading and 
understanding the code easier. We end by putting a curly bracket for the end 
of the class: 
 // Methods 
```csharp
 public void Breath() 
 { 
  // TODO: breathing process 
 } 
 
 public void Bark() 
 { 
  Console.WriteLine("wow-wow"); 
 } 
}
```

## Formatting Rules for Loops and Conditional Statements

Formatting Rules for Loops and Conditional 
Statements 
Formatting of loops and conditional statements follows the same rules as 
methods and classes. The body of a conditional statement or a loop is always 
put in a block beginning with “{” and ending with “}”. The opening bracket is 
always on a new line, immedi ately after the condition of the loop or the 

conditional statement. The body of a loop or a conditional statement is always 
indented to the right by a single tabulation. If the condition is long and does 
not fit at a single line, it is carried over on a new line and then indented to the 
right by two tabs. Here is an example of a correctly formatted loop and a 
conditional statement: 
```csharp
static void Main() 
{ 
 Dictionary<int, string> bulgarianNumbersHashtable = 
  new Dictionary<int, string>(); 
 bulgarianNumbersHashtable.Add(1, "one"); 
 bulgarianNumbersHashtable.Add(2, "two"); 
 bulgarianNumbersHashtable.Add(3, "three"); 
 
 foreach (KeyValuePair<int, string> pair in 
```
  bulgarianNumbersHashtable.ToArray()) 
```csharp
 { 
  Console.WriteLine("Pair: [{0},{1}]", pair.Key, pair.Value); 
 } 
} 
```
It is especially wrong to indent the body of a loop or a conditional statement 
as follows: 
```csharp
foreach (Student s in students) { 
      Console.WriteLine(s.Name); 
      Console.WriteLine(s.Age); 
}
```

## Usage of Empty Lines

It is very common for beginner programmers to insert empty lines  in a 
chaotic manner. Really, when new lines do not harm, why shouldn ’t we put 
them wherever we want and why should we remove them since they do not 
affect the meaning of the code? The reason is very simple: empty lines are 
used for separating parts of the program, which  are not logically 
connected, much like new lines separate the end and the beginning of two 
paragraphs. 
Empty lines are used to separate two methods , to separate a group of 
member-variables from another group of member -variables with a different 
logical task, for separating a group of related statements from another group 
of related statements. 
Here is a bad example of two methods in which empty lines are used 
inappropriately and that hinders code readability: 

```csharp
public static void PrintList(IList<int> list) 
{ 
 Console.Write("{ "); 
 foreach (int item in list) 
 { 
  Console.Write(item); 
 
  Console.Write(" "); 
 
 
 } 
 Console.WriteLine("}"); 
} 
static void Main() 
{ 
 IList<int> firstList = new List<int>(); 
 firstList.Add(1); 
 
 firstList.Add(2); 
 firstList.Add(3); 
 firstList.Add(4); 
 firstList.Add(5); 
 Console.Write("firstList = "); 
 PrintList(firstList); 
 List<int> secondList = new();
 secondList.Add(2); 
 
 secondList.Add(4); 
 secondList.Add(6); 
 Console.Write("secondList = "); 
 PrintList(secondList); 
 List<int> unionList = new();
 unionList.AddRange(firstList); 
 Console.Write("union = "); 
 
 PrintList(unionList); 
} 
```
You see that the empty lines do not represent the logical structure of the 
program, and that is why they break the main rule in formatting. 
If we reformat the program so that empty lines are properly used  to 
separate logical ly related groups of statements, we will come up with much 
more readable code: 

```csharp
public static void PrintList(IList<int> list) 
{ 
 Console.Write("{ "); 
 foreach (int item in list) 
 { 
  Console.Write(item); 
  Console.Write(" "); 
 } 
 Console.WriteLine("}"); 
} 
 
static void Main() 
{ 
 IList<int> firstList = new List<int>(); 
 firstList.Add(1); 
 firstList.Add(2); 
 firstList.Add(3); 
 firstList.Add(4); 
 Console.Write("firstList = "); 
 PrintList(firstList); 
 
 List<int> secondList = new();
 secondList.Add(2); 
 secondList.Add(4); 
 secondList.Add(6); 
 Console.Write("secondList = "); 
 PrintList(secondList); 
 
 List<int> unionList = new();
 unionList.AddRange(firstList); 
 Console.Write("union = "); 
 PrintList(unionList); 
}
```

## Rules for Moving to the Next Line and Alignment

When a line is longer, split it up into  two or more lines and indent the lines 
after the first one by a single tab: 
Dictionary<int, string> egyptianNumbersHashtable = 
 new Dictionary<int, string>(); 
It is wrong to align similar statements  according to the longest of them, 
since that can obstruct the maintenance of the code: 

DateTime    date   = DateTime.Now.Date; 
int      count  = 0; 
Student      student  = new Strudent(); 
List<Student>  students  = new List<Student>(); 
Or 
matrix[x, y]         == 0; 
matrix[x + 1, y + 1]      == 0; 
matrix[2 * x + y, 2 * y + x]   == 0; 
matrix[x * y, x * y]      == 0; 
It is wrong to align arguments to the right, based on the opening parenthesis 
of a method call: 
```csharp
Console.WriteLine("word '{0}' is seen {1} times in the text", 
```
                  wordEntry.Key, 
                  wordEntry.Value); 
The above code should be properly formatted as follows (this is not the only 
proper way, though): 
```csharp
Console.WriteLine( 
 "word '{0}' is seen {1} times in the text", 
```
 wordEntry.Key, 
 wordEntry.Value);