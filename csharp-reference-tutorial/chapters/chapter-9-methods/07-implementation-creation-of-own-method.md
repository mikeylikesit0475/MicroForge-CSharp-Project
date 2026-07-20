# Implementation (Creation) of Own Method

After a method had been declared, we must write its implementation. As we 
already explained above, implementation (body) of the method consists of 
the code, which will be executed by calling the method. That code must be 
placed in the method’s body and it represents the method’s logic.

## The Body of a Method

Method body we call the piece of code, that is placed in between the curly 
brackets "{" and "}", that directly follow the method declaration. 
```csharp
static <return_type> <method_name>(<parameters_list>) 
{ 
 // … code goes here – in the method's body … 
} 
```
The real job, done by the method, is placed exactly in the method body. So, 
the algorithm used in the method to solve the particular task is placed in the 
method body. 
So far we have seen many examples of method body however, we will show 
one more with the code below: 
```csharp
static void PrintLogo() 
{ // Method's body starts here 
 Console.WriteLine("Microsoft"); 
 Console.WriteLine("www.microsoft.com"); 
} // … And finishes here 
```
Let’s consider one more time one rule about method declaration: 
 
Method can NOT be declared inside the body of another 
method.

## Local Variables

Whenever we declare a variable inside the body of a method, we call that 
variable local variable for the method. To name a variable we should follow 
the identifiers rules in C# (refer to chapter "Primitive Types and Variables"). 
The area where a local variable exists, and can be used, begins from the line 
where the variable is declared and ends at the closing curly bracket " }" of the 
method body. This is the so -called area of visibility of the vari able 
(variable scope ). If we try to declare variable, after we have already 
declared a variable with the same name, the code will not compile due to an 
error. Let’s look at the example below: 


```csharp
static void Main() 
{ 
 int x = 3; 
 int x = 4; 
} 
Compiler will not let’s use the name x for two different variables, and will 
return a message similar to the one below: 
```
A local variable named 'x' is already defined in this scope. 
A block of code  we call a code that is placed between opening and closing 
curly brackets "{" and "}". 
If a variable is declared within a block, it is also called local (for this block) . 
Its area of visibility begins from the line where the variable is declared, and 
ends at the line where block’s closing bracket is.