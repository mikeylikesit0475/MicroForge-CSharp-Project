# Input / Output Exception Handling

If you have followed the examples so far, you have probably noticed that 
many of the operations, related to  files, can cause exceptional situations. The 
basic principles and approaches for their capture and processing are already 
familiar to you from the chapter " Handling Exceptions ". Now we will 
concentrate on the specific errors when working with files  and best 
practices for their handling.

## Intercepting Exceptions when Working with Files

Perhaps the most common exception when working with files is the 
FileNotFoundException (its name infers that the desired file was not found). 
It can occur when creating StreamReader. 
When setting a specified encoding by the creation of a StreamReader or a 
StreamWriter object, an ArgumentException can be thrown. This means, 
that the encoding we have chosen is not supported. 
Another common mistake is  IOException. This is the base class for all IO 
errors when working with streams. 
The standard approach for handling exceptions when working with files is the 
following: declare variables of class StreamReader or StreamWriter in try-
catch block. Initialize them with the necessary values in the block and handle 
the potential exceptions properly. To close the stream, we use the structure 
using. To illustrate what we just said, will give an example.

## Catching an Exception when Opening a File – Example

Here’s how we can catch exceptions that occur when working with files: 
```csharp
class HandlingExceptions 
{ 
 static void Main() 
 { 
  string fileName = @"somedir\somefile.txt"; 
```
  try 


```csharp
  { 
   StreamReader reader = new(fileName);
   Console.WriteLine( 
    "File {0} successfully opened.", fileName); 
   Console.WriteLine("File contents:"); 
   using (reader) 
   { 
    Console.WriteLine(reader.ReadToEnd()); 
   } 
  } 
```
  catch (FileNotFoundException) 
```csharp
  { 
```
   Console.Error.WriteLine( 
    "Can not find file {0}.", fileName); 
  } 
  catch (DirectoryNotFoundException) 
```csharp
  { 
```
   Console.Error.WriteLine( 
    "Invalid directory in the file path."); 
  } 
  catch (IOException) 
```csharp
  { 
```
   Console.Error.WriteLine( 
    "Can not open the file {0}", fileName); 
  } 
 } 
} 
The example demonstrates reading a file and printing its contents to the 
console. If we accidentally have confused the name of the file or have deleted 
it, an exception of type FileNotFoundException will be thrown. In the catch 
block we intercept this sort of exception and if such occurs, we will process it 
properly and print a message to the console, saying that this file cannot be 
found. The same will happen if there were no directory named "somedir". 
Finally, for better security, we have also added a catch block for  
IOExceptions. There all other IO exceptions, that might occur when working 
with files, will be intercepted.