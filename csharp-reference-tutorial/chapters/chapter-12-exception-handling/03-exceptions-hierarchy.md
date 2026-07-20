# Exceptions Hierarchy

There are two types of exceptions in .NET Framework: exceptions thrown by 
the applications we develop  (ApplicationException) and exceptions thrown 
by the runtime (SystemException). Each of these is a base class for a 
hierarchy of exception classes: 
 
As all of the se classes have different characteristics, we will examine them 
one by one.

## The Exception Class

In .NET Framework, Exception is the base class for all exceptions. Several 
classes inherit directly from it, including ApplicationException and 
SystemException. These two classes are base classes for almost all 
exceptions that occur during the program execution. 
The Exception class c ontains a copy of the call -stack at the time the 
exception instance was created. The class also has a (usually) short message 
describing the error (filled in by the method throwing the exception ). Every 
exception could have a  nested exception also sometime s called a n inner 
exception, wrapped exception or internal exception. 
The ability to wrap an exception with another exception is very useful in some 
cases and allows exceptions to be linked in the so called exception chain.

## Exception – Constructors, Methods and Properties

Here is how the System.Exception class looks like: 
[SerializableAttribute] 
[ComVisibleAttribute(true)] 
[ClassInterfaceAttribute(ClassInterfaceType.None)] 
```csharp
public class Exception : ISerializable, _Exception 
{ 
 public Exception(); 
 public Exception(string message); 
```
System 
Exception 
Exception 
Application 
Exception 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 
 

```csharp
 public Exception(string message, Exception innerException); 
 public virtual IDictionary Data { get; } 
 public virtual string HelpLink { get; set; } 
 protected int HResult { get; set; } 
 public Exception InnerException { get; } 
 public virtual string Message { get; } 
 public virtual string Source { get; set; } 
 public virtual string StackTrace { get; } 
 public MethodBase TargetSite { get; } 
 public virtual Exception GetBaseException(); 
} 
The full specification of the Exception class given above is complex to be 
```
explained, so we will discuss only its most important methods and properties 
as they are inherited by all exceptions in .NET Framework. 
- We have three constructors with different combinations for message and 
inner exception. 
- The Message property returns a text description of the exception. For 
example if the exception is FileNotFoundException, the message could 
provide information which file was not found. In most of the cases, the 
code throwing the exception passes the me ssage in the constructor. 
Once set, the Message property cannot be changed. 
- The InnerException property returns the inner (wrapped, nested) 
exception or null if such doesn’t exist. 
- The GetBaseException() returns the innermost exception from a given 
exception chain. By definition, calling this method for every exception 
within an exception chain will always yield the same result – the first 
exception that happened. 
- The StackTrace property returns information for the entire stack 
contained in the exception (w e have already seen how this information 
looks like).

## Application vs. System Exceptions

Exceptions in .NET are two types – system and application. System exceptions 
are defined in .NET libraries and are used by the framework, while application 
exceptions are defined by application developers and are used by the 
application software. When we, as developers, design our own exce ption 
classes, it is a good practice to inherit from ApplicationException and not 
directly from SystemException (or even worse – directly from Exception). 
SystemException should only be inherited internally within the .NET 
Framework. 

Some of the worst syst em exceptions include ExecutionEngineException 
(which is thrown on internal error within CLR), StackOverflowException 
(call-stack overflow, most probably due to infinite recursion) and 
OutOfMemoryException (insufficient memory). In all of these cases, our 
application could hardly recover or react in some reasonable manner. Most 
frequently, when such exception occurs, the application just crashes. 
Exceptions related to interaction with external components  (like COM 
components) inherit from ExternalException. Examples are COMException 
and Win32Exception.