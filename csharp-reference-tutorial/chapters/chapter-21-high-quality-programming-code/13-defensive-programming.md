# Defensive Programming

Defensive programming is a term denoting a practice towards defending the 
code fro m incorrect data . Defensive programming keeps the code from 
errors that nobody expects. It is implemented by checking the validity of 
all input data . This is the data coming from external sources, input 
parameters of methods, configuration files and settin gs, input from the user, 
and even the data from another local method. 
The main idea behind defensive programming is that methods should check 
their input parameters  (and other input data) and inform the caller when 
the object’s internal state or the input parameters are incorrect.  
Defensive programming requires that all data is checked , even if it is 
coming from a trusted source. If this trusted source happens to have a bug, 
the bug will be found earlier and more easily. 
Defensive programming is implemented through assertions, exceptions and 
other means of error handling.

## Assertions

Assertions are special conditions that should always be met . If not met, 
they throw an error message and the program terminates. 
A quick example of assertion in C# is shown below: 
void LoadTemplates(string fileName) 
```csharp
{ 
 bool templatesFileExist = File.Exists(fileName); 
```
 Debug.Assert(templatesFileExist, 
  "Can't load templates file: " + fileName); 
}

## Assertions vs. Exceptions

Exceptions are announcements for an error or for an unexpected event. 
They inform the programmer using the code for an error. Exceptions can be 
caught and program execution can still continue. 
Assertions produce fatal errors . They cannot be caught or handled, 
because they are meant to indica te a bug in the code. A failed assertion 
causes the program to terminate. 
Assertions can be turned off . The concept is to have them turned on only 
at the time of developing, in order to find as many bugs as possible. When 
turned off, the conditions are no longer checked. Turning off the assertions is 
plausible when the software goes to production, since these checks are 
affecting the performance and the messages are not always meaningful to the 
end user. 
If a particular check should continue to exist when t he software goes to 
production (for example, checking the input that comes from the user), it 
should not be implemented as an assertion in the first place. Exceptions 
should be used in such cases instead. 
 
Assertions should only be used for conditions that, if not 
met, it is due to a bug in the program.

## Defensive Programming with Exceptions

Exceptions provide a powerful mechanism for centralized handling of 
errors and unusual conditions . Th ey are covered in detail s in the 
“Exception Handling” chapter. 
Exceptions allow problematic situations to be handled at many levels. They 
ease the writing and the maintenance of reliable program code. 
Another difference between exceptions and assertions is that, in defensive 
programming, exceptions are mainly used for protecting the public interface 
of a class or component. This provides for a fail-safe mechanism. 
If the Archive method described above was a part of the public interface of 
an archiving component rather than an internal method, it would have to be 
implemented as follows: 
```csharp
public int Archive(PersonData user, bool persistent) 
{ 
 if (user == null) 
 { 
  throw new StorageException("null parameter"); 
 } 
 
 // Do some processing 


 int resultFromProcessing = … 
 
 Debug.Assert(resultFromProcessing >= 0, 
  "resultFromProcessing is negative. There is a bug!"); 
 
 return resultFromProcessing; 
} 
```
The Assert still remains because it is validating a variable created within the 
method itself. 
Exceptions should be used to inform other parts of the code for problems that 
should not be ignored. Throwing an exception  is reasonable only in 
situations when an abnormal condition has occurred . For more 
information on the situations consi dered exception al, refer to the “Exception 
Handling” chapter. 
If a particular problem can be handled locally , the handling should be 
performed in the method itself and no exceptions should be thrown. If a 
problem cannot be  handled locally, the exception should be thrown to the 
caller. 
The thrown exceptions should be at an ap propriate level of abstraction.  For 
example GetEmployeeInfo() could throw EmployeeException, but not 
FileNotFoundException. The last example throws StorageException rather 
than NullReferenceException.