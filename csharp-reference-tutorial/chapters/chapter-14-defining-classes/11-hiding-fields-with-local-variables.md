# Hiding Fields with Local Variables

Hiding Fields with Local Variables 
From the section " Declaring Fields " above, we know that the scope of one 
field starts from the line where the declaration is made to the closing curly 
bracket of the class. For example let's see the OverlappingScopeTest class: 
```csharp
public class OverlappingScopeTest 
{ 
 int myValue = 3; 
 
 void PrintMyValue() 
 { 
  Console.WriteLine("My value is: " + myValue); 
 } 
 
 static void Main() 
 { 
  OverlappingScopeTest instance = new();
  instance.PrintMyValue(); 
 } 
} 
```
This code will have the following result on the console: 
My value is: 3 
On the other hand, when we implement the body of one method we have to 
declare local variables which we will use for the work of the method. As we 
know, the scope of a local variable begins from the line where it is declared 
to the closing bracket of the body of the method. For example, let’s add this 
method to the class OverlappingScopeTest: 
Int CalculateNewValue(int newValue) 
```csharp
{ 
 int result = myValue + newValue; 
 return result; 


} 
In this case, the local variable, which we will use to calculate the new value, is 
```
result. 
Sometimes the name of the local variable can overlap with the name of some 
field. In this case there is a collision. 
Let’s first look at one example, before we expla in what it is about. Let’s 
modify the method PrintMyValue() in the following way: 
void PrintMyValue() 
```csharp
{ 
 int myValue = 5; 
 Console.WriteLine("My value is: " + myValue); 
} 
```
If we declare in this way the method, could it be possible to compile this 
code? And if it is compiled, is it possible to execute it? If it is compiled and 
executed which value will be printed – the one of the field or the one of the 
local variable? 
After the execution of the Main() method, the result will be: 
My value is: 5 
This is so, because C# allows defining local variables, which names 
match with fields of the class . If this happens, we say that the scope of 
the local variable overlays the field variable (scope overlapping). 
Therefore the scope of the local variable myValue with value 5 overlapped the 
scope of the field variable in the class. Then, when we print we will get the 
local variable value. 
Despite this, sometimes it is required use the field instead the local variable 
with the same name. In this case, to ret rieve the value of the field, we use 
the reserved word this. For this purpose we access the field by using the 
"dot" operator, applied to the reserved word this. In this way, we say 
deliberately that we want to use the field of the class, and not the local  
variable with the same name. 
Let’s take a look again at our example relate to the printing of the value 
myValue: 
void PrintMyValue() 
```csharp
{ 
 int myValue = 5; 
 Console.WriteLine("My value is: " + this.myValue); 
} 

```
This time, after we applied the changes, the result from the call of the method 
is different: 
My value is: 3