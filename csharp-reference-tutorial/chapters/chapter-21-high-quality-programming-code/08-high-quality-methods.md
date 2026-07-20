# High-Quality Methods

The quality of our methods is of significant importance to creating hi gh-quality 
software and its maintenance. They contribute to more readable and more 
comprehensible programs. Methods do help us reduce the complexity  of 
our software, in order to make it more flexible and easier to modify. 
It is up to us, to what extent we will benefit from these advantages. The 
higher the quality of our methods, the more we gain from their usage. In the 
next paragraphs we are introducing some of the basic principles for creating 
quality methods.

## Why Should We Use Methods?

Before talking abo ut good method names, let’s spend some time and 
summarize the reasons for using methods. 
A method solves a small problem . Many individual methods solve many 
small problems. Taken together, they solve a bigger problem – this is an 
illustration of the old Ro man principle “Divide and conquer”, which, in this 
case allows us to tackle smaller problems more easily. 
With methods, the overall complexity of a task is reduced : complex 
problems are being split up into simpler ones, additional layers of abstraction 
are added, implementation details are hidden, and the risk of failure is 
lowered. Code duplication is avoided  as well. Complex sequences of 
actions are hidden. 
Since methods are the smallest reusable unit of code, their biggest advantage 
is the ability they give us to reuse code. In fact, that ’s exactly how methods 
emerged.

## What Should a Method Do?

A method should do the work described by its name, and nothing more . 
If a method does not do what its name suggests, then either its name is 
wrong, or it does many  things at the same time, or the method simply is 
incorrectly implemented. In any of the se three cases, the method does not 
meet the requirements for code quality and should be refactored accordingly. 
A method should either do its expected job, or should inform for an error 
and terminate. In .NET, informing for errors is done by throwing an 
exception. In case of invalid input, it is unacceptable for a method to return 
a wrong result. Instead, the method should inform the caller that it cannot do 
its job bec ause the necessary preconditions are not met (such as invalid 
parameters being supplied, or an unexpected internal object state, etc.). 

For example, suppose we have a method for reading the contents of a file. It 
should be called ReadFileContents() and should return byte[] or string, 
depending on whether we are treating the contents as binary or text. If the 
file does not exist or cannot be opened for whatever reason, the method 
should throw an exception rather than return an empty string or null. 
Returning a neutral value  (such as null) instead of an error message is 
generally not recommended , except in cases where that value does not 
collide with an error condition, such as a Find() method returning null 
because nothing was found. Otherwise, the caller los es its ability to handle 
the error, and the cause of the error is lost because of the lack of a richly 
informative exception. 
 
A public method should either correctly accomplish exactly 
what its name suggests, or should inform the caller for an 
error by throwing an exception. 
Any other behavior is incorrect! 
The above rule has some exceptions when private methods are concerned. 
Unlike public methods, which  should either work correctly or throw an 
exception, a compromise can be made for private methods. Since only the 
author of the class is supposed to call them, he should be aware of the 
validity of the passed arguments. Therefore, error conditions need not be 
handled because they can be predicted and prevented in the first place. But 
do not forget – this is still a compromise. 
Two examples of high-quality methods: 
long Sum(int[] elements) 
```csharp
{ 
 long sum = 0; 
 foreach (int element in elements) 
 { 
  sum = sum + element; 
 } 
 return sum; 
} 
 
double CalcTriangleArea(double a, double b, double c) 
{ 
 if (a <= 0 || b <= 0 || c <= 0) 
 { 
  throw new ArgumentException("Sides should be positive."); 
 } 
 double s = (a + b + c) / 2; 
 double area = Math.Sqrt(s * (s - a) * (s - b) * (s - c)); 
 return area; 


}
```

## Strong Cohesion and Loose Coupling

The rules regarding the logical relatedness of the responsibilities ( strong 
cohesion) and the functional independence through a minimal amount of 
interaction with other methods and classes ( loose coupling) are of a major 
importance when methods are concerned. 
As we already ex plained, a method should solve only one problem , not 
many. A method should not solve numerous unrelated problems and should 
not have side effects. Otherwise, coming up with a precise and descriptive 
name is hard. This means that all of our methods should h ave strong 
cohesion, i.e. be concerned towards solving a single problem. 
Methods should depend as little as possible on the rest of the methods in their 
```csharp
class and on the methods / properties / fields in other classes. This concept is 
```
called loose coupling. 
In the best -case scenario, a method should depend only on its parameters 
and not use any other data as its input or output. Such methods can be easily 
pulled out and reused in another project , because they are unbound to 
the environment in which they execute. 
Sometimes methods depend on private variables declared within their class, 
or they alter the state of the object they belong to. This is not wrong and is 
entirely OK. In such a case we are talking about coupling between the 
method and its class . Such coupling is not problematic because the  class 
and its internal data and logic are encapsulated: the whole class can still be 
moved into another project and reused without any modifications. 
Most of the classes from .NET Common Type System  (CTS) and .NET 
Framework define methods that depend only on the data within their class 
and the passed arguments. In standard libraries, the methods dependencies 
from external classes are minimal and that is why they are easy to reuse . 
The .NET Framework class library strongly follows the idea of loose coupling. 
Whenever a method reads or modifies global data  and depends on 10 
additional objects , which must be initialized within the instance of its own 
```csharp
class, it is considered  a coupled to its environment  and to all of these 
```
objects. This means that it functions in an overly complex way and is affected 
by too many external conditions, therefore the probability for an error is high. 
Methods that depend on too many external conditions are hard to read , 
understand and maintain. Strong functional coupling is bad  and should 
be avoided as much as possible, because it often leads to spaghetti code. 
Look at the same two methods  like at our previous example . They are slightly 
modified and no longer fulfill the requirements of loose coupling and strong 
cohesion. Do you spot errors? 

long Sum(int[] elements) 
```csharp
{ 
 long sum = 0; 
 for (int i = 0; i < elements.Length; i++) 
 { 
  sum = sum + elements[i]; 
  elements[i] = 0; // Hidden side effect 
 } 
 return sum; 
} 
 
double CalcTriangleArea(double a, double b, double c) 
{ 
 if (a <= 0 || b <= 0 || c <= 0) 
 { 
  return 0; // Incorrect result 
 } 
 double s = (a + b + c) / 2; 
 double area = Math.Sqrt(s * (s - a) * (s - b) * (s - c)); 
 return area; 
}
```

## How Long Should a Method Be?

Throughout the years, research has been done regarding the optimal length of 
methods, but after all, a universal formula has not been found. 
The p ractice shows that, in general, shorter methods (not longer than a 
single screen) should be preferred.  Such methods are visible on the screen 
without scrolling and this simplifies their reading and understanding  and the 
probability for making mistakes. 
The longer a method, the more complex it becomes. Consequent modifica -
tions become considerably harder and more time-consuming than with shorter 
methods. These factors lead towards errors and harder maintenance. 
The recommended length of a method is not more than a single screen, but 
this recommendation is only advisory. If a method fits on the screen, it is 
easier to read because scrolling is not needed. If a method is longer than one 
screen, we should think whether we can split it up  into a few simpler 
methods. Since splitting is not always possible to be done in a meaningful 
way, the recommendation about method length is only advisory. 
Although longer methods are not preferred, the latter should not be an 
absolute excuse for splitting up a method only to make it shorter. Methods 
should be as long as necessary. 

 
Strong cohesion of methods is much more important than  
their length. 
If we are implementing a complex algorithm and consequently come up with a 
longer, meaningful method, which does one thing  and does it well, the length 
is not a problem. 
In any case, we should at least consider splitting up a longer method  into 
smaller methods solving particular subtasks, whenever the method becomes 
too long.

## Method Parameters

One of the basic rules for ordering method parameters is that the primary 
one(s) should precede the rest. For example: 
```csharp
public void Archive(PersonData person, bool persistent) { … } 
```
The opposite would be much more confusing: 
```csharp
public void Archive(bool persistent, PersonData person) { … } 
```
Another rule is to have meaningful parameter names. A common mistake 
is to tie the parameter names to their type: 
```csharp
public void Archive(PersonData personData) { … } 
```
Instead of the meaningless personData (which carries information only 
about the type), we can use a better name so that it becomes clear what kind 
of an object we are archiving: 
```csharp
public void Archive(PersonData loggedUser) { … } 
```
If there are other methods with similar parameters, their ordering should 
be consistent: 
```csharp
public void Archive(PersonData person, bool persistent) { … } 
 
public void Retrieve(PersonData person, bool persistent) { … } 
```
It is important that no parameters are left unused. Unused parameters can 
only mislead the person who uses the code. 
Parameters should not be used as local variables , that is, they should 
not be modified. Modifying method parameters makes the code harder to read 
and the logic becomes more convoluted. You can always declare a new 
variable instead of modifying a parameter. Conserving memory is not an 
excuse in such a scenario. 


Implicit assumptions should be documented . An example would be to 
specify the measurement unit of a parameter to a method that computes the 
cosine of an angle – whether the angle is in radians or degrees, in case the 
name does not make it obvious. 
The parameter count should not exceed 7 . Seven is a special, magic 
number. It is proven in the psychology that the human mind cannot trace 
more than 7  (+/- 2) things simultaneously. As with parameter count, this 
recommendation is only advisory. Sometimes y ou need to pass more 
parameters. If that is the case, think about passing them as an object that 
represents a class with many fields. For example, instead of having an 
AddStudent(…) method with 15 parameters (name, address, contacts, etc.), 
you can reduce them by grouping logically related parameters into separate 
objects: AddStudent(personalData, contacts, universityDetails). This 
way, each of the three parameters will contain a few fields inside, and the 
same information will be passed to the method, but in an easier to understand 
form. 
Sometimes it is more appropriate, from a logical standpoint, to pass only one 
or a few of the fields  of an object, rather than the whole object . This 
mostly depends on whether the method should be aware of the existence of 
this object or not. Suppose we have a method that calculates the final grade 
of a given student – CalcFinalGrade(Student s). Because the final grade 
depends only on the previous grades and the rest of the student ’s data does 
not matter, it would be better if only the list of grades is passed – 
CalcFinalGrade(IList<Grade>), instead of a Student object.