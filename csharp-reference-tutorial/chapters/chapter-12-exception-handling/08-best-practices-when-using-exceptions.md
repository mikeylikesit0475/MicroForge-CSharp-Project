# Best Practices when Using Exceptions

In this section we will give some recommendations and best practices for 
correctly using exceptions for error handling and unexpected situations. 
These are important rules that should be remembered and followed.

## When to Rely on Exceptions?

To understand when it is good to rely on exceptions let ’s see the following 
example: we have a program that ope ns a file by given path and file name. 
While writing the user can write the file name wrong. This should rather be 
considered normal and not exceptional. 
We can be prepared and first check if the file exists before we try to open it: 
```csharp
static void ReadFile(string fileName) 
{ 
 if (!File.Exists(fileName)) 
 { 
  Console.WriteLine( 
   "The file '{0}' does not exist.", fileName); 
  return; 
 } 
 
 StreamReader reader = new(fileName);
 using (reader) 
 { 
  while (!reader.EndOfStream) 
  { 
   string line = reader.ReadLine(); 
   Console.WriteLine(line); 
  } 
 } 
} 
```
If we call the method and the file is missing we will see the following message 
in the console: 
The file 'WrongTextFile.txt' does not exist. 
The other way to implement this is the following: 
```csharp
static void ReadFile(string filename) 
{ 

 StreamReader reader = null; 
```
 try 
```csharp
 { 
  reader = new StreamReader(filename); 
  while (!reader.EndOfStream) 
  { 
   string line = reader.ReadLine(); 
   Console.WriteLine(line); 
  } 
  reader.Close(); 
 } 
```
 catch (FileNotFoundException) 
```csharp
 { 
  Console.WriteLine( 
   "The file '{0}' does not exist.", filename); 
 } 
```
 finally 
```csharp
 { 
  if (reader != null) 
  { 
   reader.Close(); 
  } 
 } 
} 
```
We can consider the second option as worse because exceptions should be 
used for unexpected situations and missing file is more or less usual. 
It is not a good practice to rely on exceptions for expected events for another 
reason: performance. Throwing an exception is time consuming operation. 
An object has to be created to hold the exception, t he stack trace has to be 
initialized and handler for this exception has to be found and so on. 
 
It is hard to define the exact border between expected and 
unexpected. In general expected event is something related 
to the program functionality. Input of wr ong file name for 
example. Power cut during the execution of the program, 
from the other hand, is unexpected event.

## Throw Exceptions to the End User?

Exceptions are confusing for most users. They give the impression of a 
poorly written program that “has bugs”. What will the user of our application 
entering invoices think if suddenly the program shows this dialogue? 


 
This dialogue is very suitable for a developers or administrators for example, 
but it is extremely inappropriate for the end users. 
Instead of this dialogue we can show another one, much more user friendly 
and understandable for the user: 
 


This is the good way to show the error message  to the end user . The 
message is easy to understand from the user and also contains technical 
details that can be used if required but is not visible at the beginning. 
It is recommended when exceptions are not caught by anyone (such 
exceptions can only be runtime errors) to be caught by a global exception 
handler which saves them on the disk and shows user friendly message such 
as “An error occurred, please try again later”. It is a good a practice to show 
not only a user friendly message but also technical information (stack trace) 
available on demand (e.g. through an additional button or link).

## Throw Exceptions at the Appropriate Level of Abstraction!

Throw Exceptions at the Appropriate Level of 
Abstraction! 
When we throw our own exceptions we must keep in mind the abstractions in 
the context our methods work. For example if our method works with arrays 
we can throw IndexOutOfRangeException or NullReferenceException 
because our method works at low level and directly operates with the memory 
and the array elements. But if our method is doing accumulating of interests 
at all accounts in a bank it should not throw IndexOutOfRangeException 
because this exception is not  from the business area of the banking sector. It 
would be normal accumulation of interests in a bank software to throw 
InvalidInterestException exception with an appropriate error message 
where the original IndexOutOfRangeException exception to be attached. 
Let’s give another example: we call a method that sorts an array of integers 
and throws an exception TransactionAbortedException. This is also an 
inappropriate exception just as NullReferenceException was in accumu -
lation of interests in the bank softwa re. That is why we should consider the 
abstraction level where our method works when we throw our exception.

## If Your Exception Has a Source, Use It!

When we catch an exception and throw a new one with a higher level of 
abstraction we should always attach the original exception to it. This way 
the user of our code will be able to easily find the exact reason for the error 
and the location where it occurred at the first place. 
This rule is a special case of more general rule: 
 
Each exception should carry detailed information about the 
problem. 
From the rule above many more rules come out: we should have a relevant 
error message, the erro r type should match the problem  and the exceptions 
should hold its source as inner exception.

## Give a Detailed Descriptive Error Message!

The error message that every exceptions holds is extremely important. In 
most cases it is enough to give us information what is the problem. If the 
message is not good enough the users of your methods will not  be able to 
quickly solve the problem. 
Let’s take the following example: we have a method that reads the 
applications settings from a file. For example size and position of all windows 
in the application and others. There is a problem while reading the settings 
file and we receive the following error message: 
Error. 
Is this enough to find the problem? Obviously not.  What should be the 
message so it is descriptive enough? Is this one better? 
Error reading settings file. 
Obviously the message above is better but it is still not good enough. It 
explains what the error is but does not tell us what causes it. Let ’s suppose 
we change the program so it gives the following error information: 
Error reading settings file: 
C:\Users\Administrator\MyApp\MyApp.settings 
This error message is better because it tells us which file caused the problem 
(something that would save us time, especially if we are not familiar with the 
application and don’t know where it keeps its settings files). 
The situation could be even worse – we may not have the s ource code of the 
application and don’t have the access to the stack trace (if we have compiled 
without debug information). That is why the error message should be even 
better. For example like the following: 
Error reading settings file: 
C:\Users\Administrator\MyApp\MyApp.settings. Number expected at 
line 17. 
This message fully describes the problem. Obviously we have an error on line 
17, in MyApp.settings file, which is in C:\Users\Administrator\MyApp 
folder. On this line a number is expected but is not p rovided. If we open the 
file we could quickly find the problem. 
 
Always give adequate, detailed and correct error message 
when throwing exceptions! The user of your code should be 
able to tell what and where is the problem and what caused 
it when reading the error message. 


Let’s give some examples: 
- We have a method that searches for an integer in an array. If it throws 
IndexOutOfRangeException it is important to mention the index that 
cannot be reached in the error message. For example index 18 when the 
length of the array is 7. If we don ’t know the position we will hardly 
understand why we are outside the array. 
- We have a method that reads int egers from a file. If in the file we have 
a row without an integer we should get an error, which explains that at 
row 17 for example an integer is expected instead of a string (and prints 
the string). 
- We have a method that calculates the sum of numeric exp ression. If we 
find an error in the expression the exception should say what error 
occurred and at what position. The code that causes the error may use 
String.Format(…) to build the error message. Here is an example  how 
to implement this: 
throw new FormatException( 
 string.Format("Invalid character at position {0}. " + 
 "Number expected but character '{1}' found.", index, ch));

## Error Messages with Wrong Content

Even worse than throwing an exception with not enough information is 
throwing one with wrong information. If in the last example we say the error 
is at row 3 instead of row 17 this will be misleading and will be worse than 
just showing an error and give no details. 
 
Be careful not to show messages with incorrect content!

## Use English for All Exception Messages

Use English for the error messages when throwing an exception . This rule 
is a sub-rule of the rule “use English in your entire source code. The reason : 
English is the only language that is understood by programmers ar ound the 
world. One day your code could be used by foreigners. If you live in France 
you probably won ’t be happy to get error messages in Chinese and vice -
versa, would you? 
Note that error messages shown to the end user could be in his native 
language, but the error messages in the exceptions should always be in 
English. The exceptions are for the developer. The developers around the 
world use English. The messages (errors / noti fications / warnings) for the 
end user are different story. These messages could be in the language which 
is best suited for the end -users and may be customized through localization  
techniques like resources, embedded resource files and resource strings  (see 


http://msdn.microsoft.com/en-us/magazine/cc163609.aspx for additional 
information).

## Never Ignore the Exceptions You Catch!

Never ignore the exceptions you catch without handling them . Here is an 
example what we should not do: 
try 
```csharp
{ 
 string fileName = "WrongTextFile.txt"; 
 ReadFile(fileName); 
} 
```
catch (Exception e) 
{ } 
In the example the exception is caught and ignored. This means that if the file 
is missing the program will not read anything and there will not be any error 
message. This gives the user wrong impression the file is read when it is in 
fact missing. Don't do this! 
If we ever need to ignore an exception on purpose we should add a comment, 
which will help us when reading the code later. Here is an example: 
int number = 0; 
try 
```csharp
{ 
 string line = Console.ReadLine(); 
 number = Int32.Parse(line); 
} 
```
catch (Exception) 
```csharp
{ 
 // Incorrect numbers are intentionally considered 0 
} 
Console.WriteLine("The number is: " + number); 
We can improve the code above by using Int32.TryParse(…) or by 
```
initializing the number variable with 0 in the catch block, not outside of it. In 
the second case the comment in the code and empty catch block are not 
necessary.

## Dump the Error Messages in Extreme Cases Only!

Let’s take our method, which is reading the application settings from a file. If 
an error occurs it could print it in the console but what will happen with the 
calling method? It will suppose that the settings are read correctly. 
There is an important concept in programming: 

 
A method should either do the work it is created for or throw 
an exception. Any other behavior is incorrect! 
This is a very important rule that is why we will repeat it and even extend it:  
 
A method should either do the work it is created for or throw 
an exception. In case of wrong input the method should 
throw an exception and should not return a wrong result! 
We can explain the rule in details: A method is created to do a certain job. 
What the method is doing should be clear from its name. If we cannot give an 
appropriate name to the method means that it is doing many things and we 
should split it so everything is in separate method. If the method cannot do 
the work it is created for it should throw an exception. For example if we have 
a method for sorting of an array of integers. If the array is empty the method 
should either return an empty array or return an error. Wrong input should 
cause an exception and not return a wrong result! For e xample if we try to 
take a substring from index 7 to 12 from a string with length 10, it should 
cause an exception and not return fewer characters. This is how the 
Substring() method in String works. 
We will give another example, which confirms the rule th at a method should 
do the work it is created for or throw an exception. Let ’s suppose we copy a 
big file from the local disk to an USB flash drive. It could happen so that the 
space on the flash drive is not enough and the file cannot be copied. Which of 
the following is correct and the program for coping files (for example 
Windows Explorer) should do? 
- The file is not copied and no error message is shown. 
- The file is partially copied and no error message is shown. 
- The file is partially copied and error message is shown. 
- The file is not copied and error message is shown. 
From the user point of view the only correct behavior of the program is the 
last one: if a problem occurs the file should not be copied partially and an 
error message should be shown. We should do the same if we have to write a 
method that copy files. It should fully copy the given file or throw an 
exception. At the same time it should not leave any traces – it should delete 
any partial result if such was created.

## Don’t Catch All Exceptions!

A very common mistake with exceptions is to catch all exceptions no matter 
what type they are. Here is an example where all exceptions are handled 
wrong: 
try 


```csharp
{ 
 ReadFile("CorrectTextFile.txt"); 
} 
```
catch (Exception) 
```csharp
{ 
 Console.WriteLine("File not found."); 
} 
```
In the code we suppose that there is a method ReadFile(), which reads a 
text file and returns the content as string. The catch block catches all 
exceptions (regardless of their type), not only FileNotFoundException, and 
in all cases prints that file is not found. There are unexpected situations such 
as when file is locked by another process in the operating system. In such 
case the CLR will generate UnauthorizedAccessException, but the message 
that the program will show to the user will be wrong and m isleading. The file 
exists but the program will claim it is not there. The same will happen when 
during the file opening we are out of memory and OurOfMemoryException is 
generated. The message will be incorrect again.

## Only Catch Exceptions You Know How to Process!

We should handle only errors that we expect and we are prepared for. We 
should leave the other errors (exceptions) so they are caught by another 
method that knows how to handle them. 
 
A method should not catch all exceptions – it should only 
catch the ones it can process correctly. 
This is a very important rule that should be followed. If you don’t know how to 
handle an exception do not catch it or wrap it with your exception and pass it 
on for additional handling.