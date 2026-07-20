# Use of Constants

Well written code should not contain “ magic numbers ” and “magic 
strings”. Such constants are all the literals in a program having a value other 
than 0, 1, -1, "" and null (with little exceptions). 
In order to explain the concept why we need named constants , let’s 
examine the code below. In this code w e use the number 3.14159206 (π) 
three times (directly and in a formula), which introduces duplicated code. If, 
for example, we decide to increase the precision of this constant  or change it, 
we will need to modify the program at three different locations: 


```csharp
public class GeometryUtils 
{ 
 public static double CalcCircleArea(double radius) 
 { 
  double area = 3.14159206 * radius * radius; 
  return area; 
 } 
 
 public static double CalcCirclePerimeter(double radius) 
 { 
  double perimeter = 6.28318412 * radius; 
  return perimeter; 
 } 
 
 public static double CalcElipseArea( 
  double axis1, double axis2) 
 { 
  double area = 3.14159206 * axis1 * axis2; 
  return area; 
 } 
} 
```
It comes to mind that it is better to define the repeating values only once on 
the code. In .NET such values are declared as named constants as follows: 
```csharp
public const double PI = 3.14159206; 
```
After this declaration, the PI constant is accessible to the whole program and 
can be used an unlimited number of times. In case we need to change the 
value, we change it at one location only, and the changes are reflected 
everywhere. Here is how our GeometryUtils class looks after dec laring the 
number 3.14159206 as a named constant: 
```csharp
public class GeometryUtils 
{ 
 public const double PI = 3.14159206; 
 
 public static double CalcCircleArea(double radius) 
 { 
  double area = PI * radius * radius; 
  return area; 
 } 
 
 public static double CalcCirclePerimeter(double radius) 
 { 

  double perimeter = 2 * PI * radius; 
  return perimeter; 
 } 
 
 public static double CalcElipseArea( 
  double axis1, double axis2) 
 { 
  double area = PI * axis1 * axis2; 
  return area; 
 } 
}
```

## When to Use Constants?

The use of constants allows us to avoid the use of “magic numbers” and 
strings in our programs, and enables us to give names to the numbers and 
strings we use. In the above example not only we avoided code 
duplication, but we documented the fact that the n umber 3.14159206 is the 
well-known mathematical constant π. 
Constants should be used whenever we need to use numbers or strings 
whose origin and meaning are not obvious . Constants should generally 
be defined for every number or string that is used more than once in a 
program (with some exceptions). 
Here are a few typical cases in which named constants should be used: 
- For filenames the program operates on. They need to be frequently 
changed and it is convenient to have them as named constants at the 
beginning of the program. 
- For constants taking part in mathematical expressions . A good 
constant name improves the chance of understanding the formula. 
- For buffer sizes and sizes of memory blocks . These sizes often need 
to be changed and that is why it is convenient to have them declared as 
named constants. Apart from that, using a constant named 
READ_BUFFER_SIZE rather than the number 81 92 makes the code a lot 
more readable and comprehensible.

## When Not to Use Constants?

Although many books recommend that all numbers and strings except 0, -1, 
1, "" and null are best declared as named constants, there are a few 
exceptions in which declaring  constants can be harmful . Remember, 
declaring constants is made in order to improve the readability and the 
maintainability of the code. When a constant does not contribute to the 
readability of the code, you should avoid it. 

Here are a few situations in which using a named constant can be harmful: 
- Error messages and other messages intended for the user (“Enter your 
name”, for example). Making such strings named constants will actually 
hinder the readability. 
- SQL queries  in named constants are not recommen ded (in case you 
are using a database, queries are usually written in SQL, and that is 
usually a string in the terms of the programming language). 
- Button labels, dialog box titles, menu entries and captions of other UI 
components should not be declared as named constants. 
The .NET Framework provides libraries that facilitate internationalization and 
allow exporting all the messages, captions and labels from the UI in special 
resource files . These are not constants, however. This approach is 
encouraged if the program you are writing will have to be internationalized. 
 
Use named constants to avoid the usage and duplication of 
magic numbers and strings, and mostly to improve code 
readability. If the introduction of a named constant hinders 
the readability, better leave the hardcoded value in the code!