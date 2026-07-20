# Writing to a Text File

Text files are very convenient for storing various types of information. For 
example, we can record the resu lts of a program. We can use text files to 
make something like a journal (log) for the program – a convenient way to 
monitor it at runtime. 
Again, as with reading a text file, we will use a similar  to the Console class 
when writing, called StreamWriter.

## The StreamWriter Class

The class StreamWriter is part of the System.IO namespace and is used 
exclusively for working with text data. It resembles the class  StreamReader, 
but instead of methods for reading, it offers similar methods for writing to a 
text file. Unlike other streams, before writing data to the desired destination, 
StreamWriter turns it into bytes. StreamWriter enables us to set a preferred 
character encoding at the time it is created . We can create an instance of the 
```csharp
class the following way: 
StreamWriter writer = new("test.txt");
In the constructor of the class can pass as a parameter a file path, as well as 
```
an existing stream, to which we will write, or a n encoding scheme. The 
StreamWriter class has several predefined constructo rs, depending on 
whether we will write to a file or a stream. In the examples, we will use the 
constructor with the parameter – file path. Example of the usage of the 
StreamWriter class constructor with more than one parameter is: 
StreamWriter writer = new StreamWriter("test.txt", 
 false, Encoding.GetEncoding("Windows-1251")); 
In this example, we pass a file path as the first parameter. As a second 
parameter, we pass a Boolean variable that indicates whether to overwrite the 
file or to append the data at the end of the file. As a third parameter, we pass 
an encoding scheme (charset). 

The example lines of code could trigger an exception, but the handling of 
input / output exceptions will be discussed later in this chapter.

## Printing the Numbers [1…20] in a Text File – Example

Once we know how to create a  StreamWriter class, we will use it as 
intended. Our goal is to enroll in a text file the numbers from 1 to 20, each 
number on a separate line. We can do this the following way: 
```csharp
class FileWriter 
{ 
 static void Main() 
 { 
  // Create a StreamWriter instance 
  StreamWriter writer = new("numbers.txt");
 
  // Ensure the writer will be closed when no longer used 
  using(writer) 
  { 
   // Loop through the numbers from 1 to 20 and write them 
   for (int i = 1; i <= 20; i++) 
   { 
    writer.WriteLine(i); 
   } 
  } 
 } 
} 
```
We start by creating an instance of StreamWriter in the already well -known 
way from the examples. 
To list the numbers from 1 to 20 we will use a for-loop. Inside the loop, we 
use the method WriteLine(…), which again we know from our previous  work 
with the console, to record the current number on a new line in the file. You 
need not worry if a file with the chosen name does not exist. If such the case, 
it will automatically be created in the folder of the project and if it already 
exists, it will be overwritten (old content will be deleted). The outcome is: 
numbers.txt 
1 
2 
3 
… 
20 

To make sure that after the end of the file it will be closed, we should use the 
```csharp
using construct. 
 
Be sure to close the stream after you finish using it! The best 
way to dispose any unused resources is with the using 
```
construct in C#. 
When you want to print text in Cyrillic and are unsure what encoding to use, 
prefer the UTF -8 encoding. It is universal and not only supports Cyrillic, but 
all widespread international alphabets: Greek, Arabic, Chinese, Japanese, etc.