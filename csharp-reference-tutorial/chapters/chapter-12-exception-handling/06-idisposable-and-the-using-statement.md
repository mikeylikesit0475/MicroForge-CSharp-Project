# IDisposable and the "using" Statement

It is time to present a new short er and simplified  way to release some kinds 
of resources in C#. We will demonstrate which resources can use this special 
programming construct and how it looks like.

## IDisposable

The main use of IDisposable interface is to release resources . In .NET 
such resources are window handles, files, streams and others. We will talk 
about interfaces in “ OOP Principles” chapter. Now we may consider interface 
as an indication that given type of objects (for example streams for reading 
files) support a certain number of operations (for example closing the stream 
and releasing related resources). 
We will not go into details how to implement  IDisposable since we have to  
go much deeper and explain how the garbage collector works, how to use 
destructors, unmanaged resources and so on. 
The important method in IDisposable interface is Dispose(). The main thing 
we need to know about the method is that it releases the resources of the 
```csharp
class that implements it. In cases when resources are streams, readers or 
files releasing resources can be done using the Dispose() method from 
```
IDisposable interface, which calls their Close() method. This method closes 
them and releases their resources. So to close a stream we can do the 
following: 
StreamReader reader = new StreamReader(fileName); 
try 
```csharp
{ 
 // Use the reader here 
} 
```
finally 
```csharp
{ 

 if (reader != null) 
 { 
  reader.Dispose(); 
 } 
}
```

## The Keyword "using"

The previous example can be written in shorter form with the help of the 
```csharp
using keyword in C#, as shown in the following example: 
using (StreamReader reader = new StreamReader(fileName)) 
{ 
 // Use the reader here 
} 
```
The above simplified form of the "dispose pattern" is simple to write, 
simple to use and simple to read  and is guaranteed to release correctly the 
allocated resources specified in the brackets of the using statement. 
It is not necessary to have try-finally or to exp licitly call any method to 
release the resources. The compiler takes care to automatically put try-
finally block and the used resources are released by calling the Dispose() 
method after leaving the using block. 
Later in chapter " Text Files" we will extensively use the using statement to 
correctly read and write text files.

## Nested "using" Statements

The using statements can be nested one within another: 
```csharp
using (ResourceType r1 = …) 
 using (ResourceType r2 = …) 
```
  … 
```csharp
   using (ResourceType rN = …) 
    statements; 
```
The previous example can be written like this: 
```csharp
using (ResourceType r1 = …, r2 = …, …, rN = …) 
{ 
 statements; 
} 

It is important to mention that using statement is not related to exception  
```
handling. Its only purp ose is to release the resources no matter whether 
exceptions are thrown or not. It does not handle exception.

## When to Use the "using" Statement?

There is a simple rule when to use using with .NET classes: 
 
Use the using statement with all classes that implement the 
IDisposable interface. Look for IDisposable in MSDN. 
When a class implements IDisposable interface this means that the creator 
of this class expects it can be used with the using statement and the class 
contains some expensive resource that should not be left unreleased. 
Implementing IDisposable also means that it should be released 
immediately after we finish using the class and the easiest way to do this in 
C# is with using statement.