# The try-finally Construct

Every try block could contain a respective finally block. The code within the 
finally block is always executed, no matter how the program flow leaves the 
try block. This guarantees that the finally block will be executed even if an 
exception is thrown or a return statement is executed within the try block. 
 
The code in the finally block will not be executed if while 
executing the try block, CL R is unexpectedly terminated , e.g. 
if we stop the program through Windows Task Manager. 
The basic form of the finally block is given below: 
try 
```csharp
{ 
 // Some code that could or could not cause an exception 
} 
```
finally 
```csharp
{ 
 // Code here will always execute 
} 
```
Every try block may have zero or more catch blocks and at most one 
finally block. It is possible to have multiple catch blocks and a finally 
block in the same try-catch-finally construct. 
try 
```csharp
{ 
```
 some code 
} 
catch (…) 
```csharp
{ 
 // Code handling an exception 
} 
```
catch (…) 
```csharp
{ 
 // Code handling another exception 
} 
```
finally 
```csharp
{ 
 // This code will always execute 
}
```

## When Should We Use try-finally?

In many applications we have to work with external for our programs 
resources. Examples for external resources  include files, network 
connections, graphical elements, pipes  and streams to or from different 
hardware devices (like printers, card readers and others). When we deal with 
such external resources, it is critically important to free up the resources as 
early as possible when the resource is no longer needed. For example, when 
we open a file to read its contents (let ’s say to load a JPG image), we must 
close the file right after we have read the contents. If we leave the file open, 
the operating system will prevent other users and applications to make 
certain operations on the file. Perhaps you faced such a situation when you 
could not delete some directory or a file because it is being used by a running 
process. 
The finally block is priceless when we need to  free an external resource or 
make any other cleanup. The finally block guarantees that the cleanup 
operations will not be accidentally skipped beca use of an unexpected 
exception or because of execution of return, continue or break. 
Because proper resource  management is an important concept in 
programming, we will look at it in some more details.

## Resource Cleanup – Defining the Problem

In our example, we want to read a file. To accomplish this, we have a reader 
that must be closed when the file has been rea d. The best way to do this is to 
surround the lines using the reader in a try-finally block. Here is a refresh 
of how our example looks like: 
```csharp
static void ReadFile(string fileName) 
{ 
 TextReader reader = new StreamReader(fileName); 
 string line = reader.ReadLine(); 
 Console.WriteLine(line); 
 reader.Close(); 
} 
```
What is the problem with this code ? Well, what the code is supposed to do 
is to open up a file reader, read the data and then close the reader before the 
method returns. This last part is a problem because the method could finish in 
one of several ways: 
- An exception could be thrown when the reader is initialized (say if the 
file is missing). 
- During reading the file, an exception could arise (imagine a file on a 
remote network device which goes offline during file reading). 

- A return statement could be executed before the reader is closed (in 
our trivial example this would be obvious but it is not always as 
apparent). 
- Everything goes as expected and the method is executed normally. 
So our method as written in the example above has a critical flaw : it will 
close the reader only in the last scenario. In all of the other cases, the code 
closing the reader will not be executed. And if this code is within a loop, 
things get even more complex  as continue and break operators must be 
considered too.

## Resource Cleanup – Solving the Problem

In the previous section we explained the fundamental flaw of the solution 
'open the file  read  close'. If an error occurs during opening or reading 
the file, we will leave the file open. 
To solve this, we can use the try-finally construct. We will first discuss the 
case in which we have one resou rce to clean-up (in this case a file). Then we 
will give an example when we have two or more resources. 
Closing a file stream could be done using the following pattern: 
```csharp
static void ReadFile(string fileName) 
{ 
 TextReader reader = null; 
```
 try 
```csharp
 { 
  reader = new StreamReader(fileName); 
  string line = reader.ReadLine(); 
  Console.WriteLine(line); 
 } 
```
 finally 
```csharp
 { 
  // Always close "reader" (if it was opened) 
  if (reader != null) 
  { 
   reader.Close(); 
  } 
 } 
} 
```
In this example we first declare the reader variable, and then initialize the 
TextReader in a try block. Then in the finally block we close the reader. 
Whatever happens during TextReader’s initialization or during reading, it is 
guaranteed that the file will be closed. If there is a problem initializing the 
reader (say the file is missing), then reader will remain null and this is why 
we do a check for null in the finally block before calling Close(). If the value 

is indeed null, then the reader has not been initialized and there is no need 
to close it. The code above guarantees that if the file has been opened, then it 
will be closed no matter how the method exits. 
The example above should, in principle, properly handle all exceptions related 
to opening and initialization  of the reader  (like FileNotFoundException). In 
our example, these exceptions are not handled and are simply propagated to 
the caller. 
We have chosen file streams for our example for freeing resources up but the 
same principle applies to all resources that require proper cleanup. These 
could be remote connections, operating system resources, database 
connections and so on.

## Resource Cleanup – Better Solution

While the above solution is correct, it is unnecessary complex. Let ’s look at a 
simplified version: 
```csharp
static void ReadFile(string fileName) 
{ 
 TextReader reader = new StreamReader(fileName); 
```
 try 
```csharp
 { 
  string line = reader.ReadLine(); 
  Console.WriteLine(line); 
 } 
```
 finally 
```csharp
 { 
  reader.Close(); 
 } 
} 
```
This code has the advantage of being simpler and shorter . We avoid the 
preliminary declaration of the reader variable and the check for null in the 
finally block. The null check is now not necessary because the initialization of 
the reader is outside of the try block and if an exception occurs during the 
initialization, the finally block will not be executed at all. 
This code is cleaner, shorter and clearer and is known as " dispose pattern". 
However, note that this way the exception will go up to the method calling 
ReadFile(…).

## Multiple Resources Cleanup

Sometimes we need to free more than one resource. It is a good practice to 
free the resources in in reverse order in respect to their allocation. 

We can use the same approach outlined above, nesting the try-finally 
blocks inside each other: 
```csharp
static void ReadFile(string filename) 
{ 
 Resource r1 = new Resource1(); 
```
 try 
```csharp
 { 
  Resource r2 = new Resource2(); 
```
  try 
```csharp
  { 
   // Use r1 and r2 
  } 
```
  finally 
```csharp
  { 
   r2.Release(); 
  } 
 } 
```
 finally 
```csharp
 { 
  r1.Release(); 
 } 
} 
```
Another option is to declare all of the resources in advance and then make the 
cleanup in a single finally block with respective null checks: 
```csharp
static void ReadFile(string filename) 
{ 
 Resource r1 = null; 
 Resource r2 = null; 
```
 try 
```csharp
 { 
  Resource r1 = new Resource1(); 
  Resource r2 = new Resource2(); 
 
  // Use r1 and r2 
 } 
```
 finally 
```csharp
 { 
  if (r1 != null) 
  { 
   r1.Release(); 
  } 
  if (r2 != null) 

  { 
   r2.Release(); 
  } 
 } 
} 
```
Both of these options are correct and both are applied depending on the 
situation and programmer ’s preference. The second approach is a little bit 
riskier as if an exception occurs in the finally block, some of the resources 
will not be cleaned up. In the example above, if an exception is thrown during 
r1.Release(), r2 will not be cleaned up. If we use the first option, there is 
no such problem but the code is a bit longer.