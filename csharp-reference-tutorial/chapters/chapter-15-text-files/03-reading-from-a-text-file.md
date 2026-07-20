# Reading from a Text File

Text files provide the ideal solution for reading and writing data. If we want to 
enter some data automatically (instead by hand), we could read it from a text 

files. So now, we will take a look at how to read and write text files with the 
classes from .NET Framework and the C# language.

## StreamReader Class for Reading a Text File

C# provides several ways to read files but not all are easy and intuitive to 
use. This is why we will use the StreamReader class. The System.IO. 
StreamReader class provides the easiest way to read a text file, as it 
resembles reading from the console, which by now you have probably 
mastered to perfection. 
Having read everything until now, you  are probably a bit confused. We 
already explained that reading and writing to and from text files is only and 
exclusively possible with streams, but StreamReader did not appear anywhere 
in the above-mentioned streams and you are not sure whether it is actuall y a 
stream. Indeed, StreamReader is not a stream, but it can work with streams. 
It provides the easiest and comprehensive way to read from a text file.

### Opening a Text File for Reading

You can simply create a StreamReader from a filename (or full file path) , 
which greatly eases us and reduces the probability of an error. On its 
creation, we can specify the character encoding. Here is an example of how 
an object of the class StreamReader can be created: 
// Create a StreamReader connected to a file 
StreamReader reader = new StreamReader("test.txt"); 
 
// Read the file here … 
 
// Close the reader resource after you've finished using it 
reader.Close(); 
The first thing to do, when reading from a text file, is to create a variable of 
type StreamReader, which we can  associate with a specific file from the file 
system on our computer. To do this we need only pass the file path as a 
parameter to the constructor. Note that if the file is located in the folder 
where the compiled project (subdirectory bin\Debug) is, we can only provide 
its filename. Otherwise, we have to provide the full file path or relative path. 
The code in the above example that creates an object of type  StreamReader 
can cause an error. For now, simply pass a path to an existing file, and later 
on we will turn to the handling of errors when working with files.

### Full and Relative Paths

When working with files we can use full paths (e.g. C:\Temp\example.txt) 
or relative paths , to the directory from which the application was started 
(e.g. ..\..\example.txt). 

If you use full paths, where you pass the full path to a file, do not forget to 
apply escaping of slashes, which is used to separate the folders. In C# you 
can do this in two ways – with a double slash or with a quoted string 
beginning with @ before the string literal. For example, to enroll the path to 
the file "C:\Temp\work\test.txt" in a string we have two options: 
string fileName = "C:\\Temp\\work\\test.txt"; 
string theSamefileName = @"C:\Temp\work\test.txt"; 
Although the use of relative paths is more difficult because you have to take 
into account the directory structure of your project  which may change during 
the life of the project, it is highly recommended avoiding full paths. 
 
Avoid full file paths and work with relative paths! This makes 
your application portable and easy for installation and 
maintenance. 
Using the full path to a file (e.g.  C:\Temp\test.txt) is bad practice because 
it makes your application dependent on the environment  and also non -
transferable. If you transfer it to another computer, you will need to correct 
paths to the files, which it seeks, to work correctly. If you use a relative path 
to the current directory (e.g. ..\..\example.txt), your program will be 
easily portable. 
 
Remember that when you start the C# program, the current 
directory is the one, in which the executable (.exe) file is 
located. Most often this is the subdirectory bin\Debug or 
bin\Release directory to the root of the project. Therefore, to 
open the file example.txt from the root directory of your 
Visual Studio project, you should use a relative path 
..\..\example.txt.

### Universal Relative to Physical Path Resolver

If you want to write a portable application, you might benefit of Nakov’s 
universal path resolver:  http://www.nakov.com/blog/2009/07/14/universal-
relative-to-physical-path-resolver-for-console-wpf-and-aspnet-apps/. It  can 
automatically resolve a relative path to full (physical) file path in Web, 
desktop, console or other .NET application.  For example, if your application 
consists of an assembly App.exe and a file logo.gif and these files are 
located in the same directory,  at runtime you will be able to get the physical 
location of logo.gif through the following code: 
string logoPath = 
 UniversalFilePathResolver.ResolvePath(@"~\logo.gif");

### Reading a Text File Line by Line – Example

Now, we have learned how to create StreamReader. We can go further by 
trying to do something more complicated: to read the entire text file line 
by line and print the read text on to the console. Our advice is to create the 
text file in the Debug folder of the project (.\bin\Debug), so that it will be in 
the same directory in which your compiled application will be and you will not 
have to set the full path to it when opening the file. Let’s see what our file 
looks like: 
Sample.txt 
This is our first line. 
This is our second line. 
This is our third line. 
This is our fourth line. 
We have a text file from which to read. Now we must create an object of type 
StreamReader to read the file and loop though it line by line: 
FileReader.cs 
```csharp
class FileReader 
{ 
 static void Main() 
 { 
  // Create an instance of StreamReader to read from a file 
  StreamReader reader = new("Sample.txt");
 
  int lineNumber = 0; 
 
  // Read first line from the text file 
  string line = reader.ReadLine(); 
 
  // Read the other lines from the text file 
  while (line != null) 
  { 
   lineNumber++; 
   Console.WriteLine("Line {0}: {1}", lineNumber, line); 
   line = reader.ReadLine(); 
  } 
 
  // Close the resource after you've finished using it 
  reader.Close(); 
 } 
} 

```
There is nothing difficult to read text files. The first part of our program is 
already well known – create a variable of type StreamReader, to whose 
constructor we pass the file ’s name, which will be read. The parameter of the 
constructor is the path to the file, but since our file is found in the Debug 
directory of the project, we set only its name as a path. If our file were 
located in the project directory, then we would have su bmitted the string – 
"..\..\Sample.txt" as a path. 
After that, we create a variable – counter, whose purpose is to count and 
display on which row of the file we are currently located. 
Then, we create a variable that will store each read line. With its crea tion, we 
directly read the first line of text file. If the text file is empty, the method 
ReadLine() of the StreamReader object will return null. 
For the main part – reading the file line by line, we will use a while loop. The 
condition for the loop is : as long as there is something in the variable line, 
we should continue reading. In the body of the loop, our task is to increase 
the value of the counter variable by one and then print the current line in the 
format we like. Finally, again we use ReadLine() to read the next line in the 
file and write it in the variable  line. For printing, we use a method that is 
well known to us from the tasks, which required something to be printed on to 
the console – WriteLine(). 
Once we have read everything we need from th e file, we should not forget to 
close the object StreamReader, as to avoid loss of resources. For this, we use 
the method Close(). 
 
Always close the StreamReader instances after you finish 
working with them. Otherwise you risk losing system 
resources. Use the method Close() or the statement using. 
The result of the program should look like this: 
Line 1: This is our first line. 
Line 2: This is our second line. 
Line 3: This is our third line. 
Line 4: This is our fourth line.

## Automatic Closing of the Stream after Working with It

As noted in the previous example, having finished working with the object of 
type StreamReader, we called Close() and closed the stream  behind the 
StreamReader object. Very often, however, novice programmers forget to call 
the Close() method, thus blocking the file they use. Also in case of runtime 
exception when reading from a file, the file might be left open. This causes 
resource leakage and can lead to very unpleasant effects like program 
hanging, program misbehavior and strange errors. 


The correct way to handle the file closing is though the using keyword: 
```csharp
using (<stream object>) { … } 
```
The C# construct using(…) ensures that after leaving its body, the method  
Close() will automatically be called. This will happen even if an exception 
occurs when reading the file. 
Now let’s rework the previous example to benefit from the using construct: 
FileReader.cs 
```csharp
class FileReader 
{ 
 static void Main() 
 { 
  // Create an instance of StreamReader to read from a file 
  StreamReader reader = new("Sample.txt");
 
  using (reader) 
  { 
   int lineNumber = 0; 
 
   // Read first line from the text file 
   string line = reader.ReadLine(); 
 
   // Read the other lines from the text file 
   while (line != null) 
   { 
    lineNumber++; 
    Console.WriteLine("Line {0}: {1}", lineNumber, line); 
    line = reader.ReadLine(); 
   } 
  } 
 } 
} 
```
Now the code guarantees that once opened successfully, the text file will be 
closed correctly regardless of whether reading from it will succeed or fail. 
If you are wondering how it is best to take care of closing your program ’s 
streams and files, follow the following rule: 
 
Always use the using construct in C# in order to properly 
close files and streams!

## File Encodings. Reading in Cyrillic

Let’s now consider the problems that occur when reading  a file using an  
incorrect encoding, such as reading a file in Cyrillic.

### Character Encodings

You know that in memory everything is stored in binary form . This means 
that it is necessary for text files to be represented digitally, so that they can 
be stored in memory, as well as on the hard disk. This process is called 
encoding files or more correctly encoding the characters stored in text files. 
The encoding process consists of replacing the text characters (letters, 
digits, punctuation, etc.) with specific sequences of binary values. You can 
imagine this as a large table in which each character corresponds to a certain 
value (sequence of bytes). 
We already know the concept of character encodings  and few character 
encoding schemes like UTF-8 and Windows-1251 from the section " Encoding 
Schemes" of chapter " Numeral Systems and D ata Representation " and also 
from the section about " File Encodings in Visual Studio " of chapter "Defining 
Classes". Now we will extend this concept a bit and  will use character 
encodings to work correctly with text files. 
Character encodings specify the rules for converting from text to sequence 
of bytes and vice versa. An encoding scheme is a table of characters along 
with their numbers, but may also contain special rules. For example, the 
character "accent" (U + 0300) is special and sticks to the last character that 
precedes it. It is encoded as one or more bytes (depending on the character 
encoding scheme), and it does not correspond to any character, but to a part 
of the character. We will take a look at two encodings that are used most 
often when working with Cyrillic: UTF-8 and Windows-1251. 
UTF-8 is a universal encoding scheme, which supports al l languages and 
alphabets in the world. In UTF -8 the most commonly used characters (Latin 
alphabet, numerals and special characters) are encoded in one byte, rarely 
used Unicode characters (such as Cyrillic, Greek and Arabic) are encoded in 
two bytes and a ll other characters (Chinese, Japanese and many others) are 
encoded in 3 or 4 bytes. UTF -8 encoding can convert any Unicode text in 
binary form and back and support all of the 100 ,000 characters of Unicode 
standard. UTF-8 encoding is universal and suitable for any language alphabet. 
Another commonly used encoding is Windows-1251, which is usually used for 
encoding of Cyrillic texts  (such as messages sent by e -mail). It contains 
256 characters, including the Latin alphabet, Cyrillic alphabet and some 
commonly used signs. It uses one byte for each character, but at the expense 
of some characters that cannot be stored in it (as the Chinese alphabet 
characters), and are lost in an attempt of doing so. 
Other examples of encoding schemes (encodings or charsets) ar e ISO 8859-
1, Windows-1252, UTF-16, KOI8-R, etc. They are used in specific regions of 

the world and define their own sets of characters and rules for the transition 
from text to binary data and vice versa. 
For working with encoding s (charsets)  in .NET Fram ework, the class 
System.Text.Encoding is used, which is created the following way: 
Encoding win1251 = Encoding.GetEncoding("Windows-1251");

### Reading a Cyrillic Content

You probably already guessed that if we want to read from a file that contains 
characters from the Cyrillic alphabet , we must use the correct encoding 
that "understands" correctly these special characters. Typically, in a Windows 
environment, text files, containing Cyrillic  text, are stored in Windows-1251 
encoding. To use it, we sho uld set it as the encoding of the stream, which our 
StreamReader will process: 
Encoding win1251 = Encoding.GetEncoding("Windows-1251"); 
StreamReader reader = new StreamReader("test.txt", win1251); 
If you do not explicitly set the encoding scheme (encoding) for the file read,  
in .NET Framework, the default encoding UTF-8 will be used. 
You might wonder what happens if you use wrong encoding when reading or 
writing a file. There are several scenarios possible: 
- If you use read / write only Latin letters, everything will work normally. 
- If you write Cyrillic letters, to a files open with encoding, which does not 
support the Cyrillic alphabet (e.g. ASCII), Cyrillic letters will be 
permanently replaced by the character "?" (question mark). 
In any case, these are unpleasant problems, which cannot be immediately 
noticed. 
 
To avoid problems with incorrect encoding of files, always 
check the encoding explicitly. Otherwise, you may work 
incorrectly or break at a later stage.

### The Unicode Standard. Reading in Unicode

Unicode is an industry standard that allows computers and other electronic 
devices always to present and manipulate text, which was written in most of 
the world ’s literacies. It consists of over 100,000 characters, as well as 
various encoding schemes (encodings). The unification of different characters, 
which Unicode offers, leads to its greater distribution. As you know, 
characters in C# (types char and string) are also presented in Unicode. 
To read characters, stored in Unicod e, we must use one of the supported 
encoding schemes for this standard. The most popular and widely used is  
UTF-8. We can set it as a code scheme with an already familiar way: 


StreamReader reader = new StreamReader("test.txt", 
 Encoding.GetEncoding("UTF-8")); 
If you are wondering, whether to read a text file, encoded in Cyrillic, 
Windows-1251 or UTF-8, then this question has no clear answer. Both 
standards are widely used for the recording of non -Latin text. Both encoding 
schemes are allowed and can be used. You should only always follow the rule 
that a certain files should always be read and written using the same 
encoding.