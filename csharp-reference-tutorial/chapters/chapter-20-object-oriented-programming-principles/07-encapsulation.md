# Encapsulation

Encapsulation is one of the main concepts in OOP. It is also called 
"information hiding ". An object has to provide its user s only with the 
essential information for manipulation , without the internal details . A 
Secretary using a Laptop only knows about its screen, keyboard and mouse. 
Everything else is hidden  internally under the cover . She does not know 
about the inner working s of Laptop, because she doesn ’t need to, and if 
she does, she might make a mess. Therefore parts of the properties and 
methods remain hidden to her. 
The person writing the class has to decide what should be hidden and what 
not. When we program, we must de fine as private every method or field 
which other classes should not be able to access.

## Encapsulation – Examples

The example below shows how to hide methods that the class ’ user doesn’t 
have to be familiar with and are only used internally by the author of  the 
```csharp
class. First, we define an abstract class  Felidae, which defines the public 
```
operations of cats (regardless of the cat’s type): 

Felidae.cs 
```csharp
public class Felidae 
{ 
 public virtual void Walk() 
 { 
  // … 
 } 
 
 // … 
} 
This is how the class Lion looks like: 
```
Lion.cs 
```csharp
public class Lion : Felidae, Reproducible<Lion> 
{ 
 // … 
 
 private Paw frontLeft; 
 private Paw frontRight; 
 private Paw bottomLeft; 
 private Paw bottomRight; 
 
 private void MovePaw(Paw paw) { 
  // … 
 } 
 
 public override void Walk() 
 { 
  this.MovePaw(frontLeft); 
  this.MovePaw(frontRight); 
  this.MovePaw(bottomLeft); 
  this.MovePaw(bottomRight); 
 } 
 
 // … 
} 
The public method Walk() calls some other private method 4 times. That 
way the base class is short – it consists of a single method. The 
```
implementation, however, calls another of its methods, which is hidden from 
the users of the class. That way, Lion doesn’t publicly disclose 
information about its inner  workings (it encapsulates certain behavior) . 

At a later stage, this makes it possible to change its implementation without 
any of the other classes finding out and requiring changes.