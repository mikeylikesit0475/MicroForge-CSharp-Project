# Parameters in Methods

Often to solve certain problem, the method may need additional information, 
which depends on the environment in what the method executes. 


So if there is a method, that has to find the area of a square, in its body there 
must be the algorithm that finds that area (equation S  = a2). Since the area 
depends on the square side length, to calculate that equation for each square, 
the method will nee d to pass a value for the square side length. That is why 
we have to pass somehow that value, and for this purpose we use 
parameters.

## Declaring Methods with Parameters

To pass information necessary for our method we use the parameters list. 
As was already mentioned, we must place it between the brackets following 
the method name, in method the declaration: 
```csharp
static <return_type> <method_name>(<parameters_list>) 
{ 
 // Method's body 
} 
```
The parameters list <parameters_list> is a list with zero or more 
declarations of variables, separated by a comma, so that they will be used 
for the implementation of the method’s logic: 
<parameters_list> = [<type1> <name1>[, <typei> <namei>]], 
where i = 2, 3, … 
When we create a method, and we need certain information  to develop the 
particular algorithm, we choose that variable from the list , which is of type 
<typei> and so we use it by its name <namei>. 
The parameters from the list can be of any type. They can be primitive types 
(int, double, …)  or object types (for example string or array – int[], 
double[], string[], …).

### Method to Display a Company Logo – Example

To make the mentioned above more clear, we will change the example that 
shows the logo of "Microsoft": 
```csharp
static void PrintLogo(string logo) 
{ 
 Console.WriteLine(logo); 
} 
```
Now, executing our method, we can display the logo of other companies, not 
only of "Microsoft". This is possible because we used a parameter of type 
string to pass the company name. The example shows how to use the 
information given in the parameters list – the variable logo, which is defined 

in the parameters list, is used in the method ’s body by the name given in the 
definition.

### Method to Calculate the Sum of Prices of Books – Example

We mentioned above, that whenever it is necessary we can use arrays as 
parameters for a certain method (int[], double[], string[], …).  So let ’s 
take a look at another example to illustrate this. 
Imagine we are in a bookstore and we want to calculate the amount of money 
we must pay for all the books we bought. We will create a method that gets 
the prices of all the books as an array of type decimal[], and then returns 
the total amount we must pay: 
```csharp
static void PrintTotalAmountForBooks(decimal[] prices) 
{ 
 decimal totalAmount = 0; 
 foreach (decimal singleBookPrice in prices) 
 { 
  totalAmount += singleBookPrice; 
 } 
 Console.WriteLine("The total amount for all books is:" + 
  totalAmount); 
}
```

### Method Behavior According to Its Input

When a method with parameters is declared, our purpose is that every time 
we invoke the method, its result changes according to its input. Said with 
another word, the algorithm is the same, but due to input change, the result 
changes too. 
 
When a method has parameters, its behavior depends upon 
parameters values.

### Method to Show whether a Number is Positive – Example

To clarify the way method execution depends upon its input let ’s take look at 
another example. The method gets as input a number of type int, and 
according to it returns to the console "Positive", "Negative" or "Zero": 
```csharp
static void PrintSign(int number) 
{ 
 if (number > 0) 
 { 
  Console.WriteLine("Positive"); 
 } 
 else if (number < 0) 


 { 
  Console.WriteLine("Negative"); 
 } 
 else 
 { 
  Console.WriteLine("Zero"); 
 } 
}
```

### Method with Multiple Parameters

So far we had some examples for methods with parameter lists that consist of 
a single parameter . When a method is declared, however, it can have as 
multiple parameters as the method needs. 
If we are  asking for maximal of two values, for example, the method needs 
two parameters: 
```csharp
static void PrintMax(float number1, float number2) 
{ 
 float max = number1; 
 
 if (number2 > max) 
 { 
  max = number2; 
 } 
 Console.WriteLine("Maximal number: " + max); 
}
```

### Difference in Declaration of Methods with Multiple Parameters

When a method with multiple parameters is declared, we must note that even 
if the parameters are of the same type, usage of short way of variable 
declaration is not allowed. So the line below in  the methods declaration is 
invalid and will produce compiler error: 
float var1, var2; 
Type of the parameters has to be explicitly written before each parameter, no 
matter if some of its neighbors are of the same type. 
Hence, declaration like one shown below is not valid: 
static void PrintMax(float var1, var2) 
Correct way to do so is: 

static void PrintMax(float var1, float var2)

## Invoking Methods with Parameters

Invocation of a method with one or several parameters is done in the same 
way as invocation of methods without parameters. The difference is that 
between the brackets following the method name, we place values. These 
values (called arguments) will be assigned to the appropriate parameters 
form the declaration and will be used when method is executed. 
Several examples for methods with parameters are show below: 
PrintSign(-5); 
PrintSign(balance); 
 
PrintMax(100.0f, 200.0f);

### Difference between Parameters and Arguments of a Method

Before we continue with this chapter, we must learn how to distinguish 
between parameters naming in the parameters list in the method s declaration 
and the values that we pass when invoking a method. 
To clarify, when we declare a method, any of the elements from the 
parameters list we will call parameters (in other literature sources they can 
be named as formal parameters). 
When we call a method the values we use to assign to its parameters are 
named as arguments. 
In other words, the elements in the parameters list  (var1 and varr2) are 
called parameters: 
static void PrintMax(float var1, float var2) 
Accordingly, the values by the method invocation  (-23.5 and 100) are called 
arguments: 
PrintMax(100.0f, -23.5f);

### Passing Arguments of a Primitive Type

As just was explained, in C# when a variable is passed as a method 
argument, its value is copied to the parameter from the declaration of the 
method. After that, the copy will be used in the method body. 
There is, however, one thing we should be aware of. If the declared 
parameter is of a primitive type , the usage of the arguments does not 

change the argument itself, i.e. the argument value will not change for the 
code after the method has been invoked. 
So if we have piece of code like that below: 
```csharp
static void PrintNumber(int numberParam) 
{ 
 // Modifying the primitive-type parameter 
 numberParam = 5; 
 
 Console.WriteLine("in PrintNumber() method, after " + 
  "modification, numberParam is: {0}", numberParam); 
} 
```
Invocation of the method from Main(): 
```csharp
static void Main() 
{ 
 int numberArg = 3; 
 
 // Copying the value 3 of the argument numberArg to the 
 // parameter numberParam 
 PrintNumber(numberArg); 
 
 Console.WriteLine("in the Main() method numberArg is: " + 
  numberArg); 
} 
```
The value 3 of numberArg, is copied into the parameter numberParam. After 
the method PrintNumber() is invoked, to numberParam is assigned value 5. 
This does not affect the value of variable numberArg, because by invocation of 
that method, the variable numberParam keeps a copy of the argument value. 
That is why the method PrintNumber() prints the number  5. Hence, after 
invocation of method PrintNumber() in the method Main() what is printed is 
the value of numberArg and as it can be seen that value is not changed. The 
result from the above line is printed below: 
in PrintNumber() method, after modification, numberParam is: 5 
in the Main() method numberArg is: 3

### Passing Arguments of Reference Type

When we need to declare (and so to invoke) a method, that has parameters 
of reference type (as arrays), we must be very careful. 
Before explaining the reason for the above consideration, we have to remind 
ourselves something from chapter "Arrays". An array, as any other reference 

type, consists of a variable -pointer (object reference) and a value – the 
real information kept in the computer ’s memory (we call it an object). In our 
case the object is the real array of elements. The address of this object, 
however, is kept in the variable (i.e. the address where the array elements 
are placed in the memory): 
 
So whenever we operate with arrays in C#, we always access them by that 
variable (the address  / pointer / reference ) we used to declare the particular 
array. This is the principle for any other reference type. Hence, whenever an 
argument of a reference type is passed to a method, the method ’s parameter 
receives the reference itself. But what happens with the object then (the real 
array)? Is it also copied or no? 
To explain this, let ’s have the following example: assume we have method 
ModifyArray(), that modifies the first element of an array that is passed as a 
parameter, so it is reinitialized the first element with value 5 and then prints 
the elements of the array, surrounded by square brackets and separated by 
commas: 
```csharp
static void ModifyArray(int[] arrParam) 
{ 
 arrParam[0] = 5; 
 Console.Write("In ModifyArray() the param is: "); 
 PrintArray(arrParam); 
} 
 
static void PrintArray(int[] arrParam) 
{ 
 Console.Write("["); 
 int length = arrParam.Length; 
 if (length > 0) 
 { 
  Console.Write(arrParam[0].ToString()); 
  for (int i = 1; i < length; i++) 
  { 
   Console.Write(", {0}", arrParam[i]); 
  } 
 } 
 Console.WriteLine("]"); 
} 
arrArg: int[]
```
1 2 31 2 3[I@e48e1b
variable object
arrArg: int[]
1 2 31 2 31 2 31 2 3

Let’s also declare a method Main(), from which we invoke the newly created 
method ModifyArray(): 
```csharp
static void Main() 
{ 
 int[] arrArg = new int[] { 1, 2, 3 }; 
 
 Console.Write("Before ModifyArray() the argument is: "); 
 PrintArray(arrArg); 
 
 // Modifying the array's argument 
 ModifyArray(arrArg); 
 
 Console.Write("After ModifyArray() the argument is: "); 
 PrintArray(arrArg); 
} 
```
What would be the result of the code execution? Let’s take a look: 
Before ModifyArray() the argument is: [1, 2, 3] 
In ModifyArray() the param is: [5, 2, 3] 
After ModifyArray() the argument is: [5, 2, 3] 
It is apparent that after execution of the method ModifyArray(), the array to 
which the variable arrArg refer, does not consists of [1,2,3], but [5,2,3] 
instead. What does this mean? 
The reason for such result is the fact that by passing arguments of reference 
type, only the value of the variable that kee ps the address to the object is 
copied. Note that this does not copy the object itself. 
 
By passing the argument that are of reference type, the only 
thing that is copied is the variable that keeps the reference 
to the object, but not the object data. 
Let’s try to illustrate what just was explained. We will use few drawings for 
the example we used above. By invocation of the method ModifyArray(), the 
value of the parameter arrParam is not defined and it does not keep a 
reference to any particular object (not a real array): 
 
arrParam: int[]
arrArg: int[]
[I@e48e1b 321 321

By the time of ModifyArray() invocation, the value that is kept in the 
argument arrArg is copied to the parameter arrParam: 
 
This way, copying the reference to the elements of the array in the memory 
from the argument into the param eter, we tell the parameter to point to the 
same object, to which the argument points: 
 
This actually is where we have to be very careful. If the invoked method 
modifies the object , to which a reference is passed, this may affect the 
execution of the code  after the method invocation (as we have seen in the 
example – the method PrintArray() does not print the array, that was 
initially passed). 
The difference between dealing with arguments of primitive and reference 
type is in the way they are passed: primitive types are passed by their 
values, the objects, however, are passed by reference.

### Passing of Expressions as Method Arguments

When a method is invoked, we can pass a whole expression  instead of 
arguments. By doing so, C# calculates the values for those e xpressions and 
by the time of code execution (if it is possible this is done at compile time) 
replaces the expression with its result, when the method is invoked. The 
following code shows methods invocation, by passing expressions as 
method arguments: 
PrintSign(2 + 3); 
 
float oldQuantity = 3; 
float quantity = 2; 
PrintMax(oldQuantity * 5, quantity * 2); 
The result of those methods execution is: 
arrParam: int[]
arrArg: int[]
[I@e48e1b 321 321
(copy)
arrParam: int[]
[I@e48e1b
arrParam: int[]
[I@e48e1b
arrArg: int[]
[I@e48e1b 321 321

Positive 
Maximal number: 15.0 
When a method with parameters is invoked, we must be aware of some 
specific rules, which will be explained in the next few subsections.

### Passing of Arguments Compatible with the Parameter Type

We must know that we can pass only arguments that are of type compatible 
with the related parameter, declared in the method’s parameters list. 
For example, if the parameter that the method expects in its declaration is of 
type float, by invocation of the method we can pass a value that is of type 
int. It will be conv erted by the compiler to a value of type float and then 
will be passed to the method for its execution: 
```csharp
static void PrintNumber(float number) 
{ 
 Console.WriteLine("The float number is: {0}", number); 
} 
 
static void Main() 
{ 
 PrintNumber(5); 
} 
```
In the example, by invocation of PrintNumber() in the method Main(), first 
the integer literal 5 (that implicitly is of type int) is converted to the related 
floating point value 5.0f. Then the so converted value is passed to the 
method PrintNumber(). 
As can be expected, the result of that code execution is: 
The float number is: 5.0

### Compatibility of the Method Parameter and the Passed Value

The result from the calculation of an expression, passed as argument, must 
be of the same type , as the type of the d eclared parameter is, or 
compatible with that type (refer to the passage above). 
So if a parameter of type float is required, we can pass the value calculated 
by an expression that is of a type int. E.g. in the example above, if instead of 
PrintNumber(5), we called the method, with 5 replaced by the expression 
2+3, the result of the calculation of that expression must be of type float 
(one that the method expects), or of a type that can be converted to float 
with no loss (in our case this is int). So let ’s modify a little the method 
Main() from the passage above, to illustrate what just was explained: 

```csharp
static void Main() 
{ 
 PrintNumber(2 + 3); 
} 
```
In this example first the summing will be executed. Then the integer result 5 
will be converted to its floating point equivalent 5.0f. When this is done the 
method PrintNumber(…) will be invoked with argument 5.0f. The result 
again will be: 
The float number is: 5.0

### Keeping the Declaration Sequence of the Arguments Types

Values, that are passed to the method, in the time of its invocation, must be 
in the same order as the parameters are declared in the parameters list. This 
is due to the method signature, mentioned above. 
To clarify, let ’s discuss the following example: we have a method 
PrintNameAndAge(), in which method declaration is a parameters list, with 
parameters of type’s string and int, ordered as shown below: 
Person.cs 
```csharp
class Person 
{ 
 static void PrintNameAndAge(string name, int age) 
 { 
  Console.WriteLine("I am {0}, {1} year(s) old.", name, age); 
 } 
} 
```
Let’s add a method Main() to our class, in that method we will invoke the 
PrintNameAndAge() method. Now let ’s try to pass parameters in reverse (as 
types) order, so instead "John" and 25, we will use 25 and "John": 
```csharp
static void Main() 
{ 
 // Wrong sequence of arguments 
 Person.PrintNameAndAge(25, "John"); 
} 
```
The compiler in this case will not be able to find a method that is called 
PrintNameAndAge, which accepts parameters in the sequence int and 
string. That is why, the compiler will notify for an error: 

The best overloaded method match for 
'Person.PrintNameAndAge(string, int)' has some invalid arguments

## Variable Number of Arguments (var-args)

So f ar, we examined declaration of methods for which the parameters list 
coincides with the count of the arguments we pass to that method, by its 
invocation. 
Now we will see how to declare methods that allow the count of arguments to 
be different any time the method is invoked, so to meet the needs of the 
invoking code. Such methods are often called methods with a variable 
number of arguments. 
Let’s we look at the example, that calculates the sum of a given array of book 
prices, the one that already was explain ed above. In that example, as a 
parameter we passed an array of type decimal that consists of the prices of 
the chosen books: 
```csharp
static void PrintTotalAmountForBooks(decimal[] prices) 
{ 
 decimal totalAmount = 0; 
 
 foreach (decimal singleBookPrice in prices) 
 { 
  totalAmount += singleBookPrice; 
 } 
 Console.WriteLine( 
  "The total amount of all books is:" + totalAmount); 
} 
```
Defined in this way, the method suppose, that always before its invocation, 
we will have created an array with numbers of type decimal and they will be 
initialized with certain values. 
After we created a C# method that accepts variable number of parameters, is 
possible, whenever a list of parameters from the same type  must be 
passed, instead of passing the array that consists of those values, to pass 
them directly, as arguments, separated by comma. 
In our case with the books, we need to create a new array, especially for that 
method invocation: 
decimal[] prices = new decimal[] { 3m, 2.5m }; 
PrintTotalAmountForBooks(prices); 

However, if we add some code ( we will  see it in a moment) to the method 
declaration, we will be able to directly pass list with the books prices, as 
method arguments: 
PrintTotalAmountForBooks(3m, 2.5m); 
PrintTotalAmountForBooks(3m, 5.1m, 10m, 4.5m); 
Such invocation is possible only if we have declared the method in a way, so it 
accepts variable number of arguments (var-args).

### How to Declare Method with Variable Number of Arguments

Formally the declaration of a method with variable number of arguments is 
the same as the declaration of any other method: 
```csharp
static <return_type> <method_name>(<parameters_list>) 
{ 
 // Method's body 
} 
```
The difference is that the <parameters_list> is declared with the key word 
params in the way shown below: 
<parameters_list> = 
 [<type1> <name1>[, <typei> <namei>], params <var_type>[] 
<var_name>] 
where i= 2, 3, … 
The last element from the list declaration – <params>, is the one that 
allows passing of random count of arguments of type <var_type>, for each 
invocation of the method. 
In the declaration of that element, before its type <var_type> we must add 
params: "params <var_type>[]". The type <var_type> can be either  
primitive or by reference. 
Rules and special characteristics for the other elements from the method ’s 
parameters list, that precede the var-args parameter <var_name>, are the 
same, as those we discussed in the section "Method Parameters". 
To clarify what was explained so far, we will discuss an example for 
declaration and invocation of a method with variable number if arguments: 
```csharp
static long CalcSum(params int[] elements) 
{ 
 long sum = 0; 
 foreach (int element in elements) 
 { 

  sum += element; 
 } 
 return sum; 
} 
 
static void Main() 
{ 
 long sum = CalcSum(2, 5); 
 Console.WriteLine(sum); 
 
 long sum2 = CalcSum(4, 0, -2, 12); 
 Console.WriteLine(sum2); 
 
 long sum3 = CalcSum(); 
 Console.WriteLine(sum3); 
} 
```
The example sums the numbers, as their count is not known in advance. The 
method can be invoked with one, two or more parameters, as well as with no 
parameters at all. If we execute the example we will get the following result: 
7 
14 
0

### Variable Number of Arguments: Arrays vs. "params"

From the formal definition, given above, of parameter that allows passing of 
variable number of arguments  by the method invocation – <var_name>, is 
actually a name of an array of type <var_type>. By the met hod invocation, 
the arguments of type <var_type> or compatible type that we pass to the 
method (with no care for their count) will be kept into this array. Then they 
will be used in the method body. The access and dealing with these 
parameters is in the same way we do when we work with arrays. 
To make it clearer we will modify the method that calculates the sum of the 
prices of chosen books, to get variable number of arguments: 
```csharp
static void PrintTotalAmountForBooks(params decimal[] prices) 
{ 
 decimal totalAmount = 0; 
 
 foreach (decimal singleBookPrice in prices) 
 { 
  totalAmount += singleBookPrice; 
 } 

 Console.WriteLine("The total amount of all books is:" + 
  totalAmount); 
} 
```
As we can see the only change is to change the declaration of the array 
prices with adding params before decimal[]. In the body of our method, 
"prices" is still an array of type decimal, so we use it in the same way as 
before. 
Now we can invoke our method, with no need to declare in advance an array 
of number and pass it as an argument: 
```csharp
static void Main() 
{ 
 PrintTotalAmountForBooks(3m, 2.5m); 
 PrintTotalAmountForBooks(1m, 2m, 3.5m, 7.5m); 
} 
```
The result of the two invocations will be: 
The total amount of all books is: 5.5 
The total amount of all books is: 14.0 
Since prices is an array, it can be assumed that we can declare and initialize 
an array before invocation of our method. Then to pass that array as an 
argument: 
```csharp
static void Main() 
{ 
 decimal[] pricesArr = new decimal[] { 3m, 2.5m }; 
 
 // Passing initialized array as var-arg: 
 PrintTotalAmountForBooks(pricesArr); 
} 
```
The above is legal invocation, and the result from that code execution is the 
following: 
The total amount of all books is: 5.5

### Position and Declaration of a Method with Variable Arguments

A method, that ha s a variable number of its arguments, can also have other 
parameters in its parameters list. 
The following code, for example, has as a first parameter an element of type 
string, and right after it there can be one or more parameters of type int: 

```csharp
static void DoSomething(string strParam, params int[] x) 
{ 
} 
```
The one thing that we must consider is that the element from the parameters 
list in the method ’s definition, that allows passing of a variable number of 
arguments, must always be placed at the end of the parameters list. 
 
The element of the parameters list, that allows passing of 
variable number of arguments by invocation of a method, 
must always be declared at the end of the method ’s 
parameters list. 
So, if we try to put the declaration of the var-args parameter x, shown in the 
last example, not at the last place, like so: 
```csharp
static void DoSomething(params int[] x, string strParam) 
{ 
} 
The compiler will return the following error message: 
```
A parameter array must be the last parameter in a formal 
parameter list

### Limitations on the Count for the Variable Arguments

Another limitation, for the methods with variable number of arguments, is 
that the method cannot have in its declaration more than one parameter that 
allows passing of variable numbers of arguments. So if we try to compile a 
method declared in the following way: 
```csharp
static void DoSomething(params int[] x, params string[] z) 
{ 
} 
The compiler will return the already known error message: 
```
A parameter array must be the last parameter in a formal 
parameter list 
This rule can be taken as a special case of the rule for the var-args position, 
i.e. the related parameter to be at the end of the parameters list.

### Specifics of Empty Parameter List

After we got familiar with the declaration and invocation of  methods with 
variable number of arguments, one more question arises. What would happen 
if we invoke such method, but with no parameters? 
For example, what would be the result of the invocation of our method that 
calculates the sum of books prices, in a case we did not liked any book: 
```csharp
static void Main() 
{ 
 PrintTotalAmountForBooks(); 
} 
```
As can be seen this code is compiled with no errors and after its execution the 
result is as follow: 
The total amount of all books is: 0 
This happens because, although, we did not pass any value to our method, by 
its invocation, the array decimal[] prices is created, but it is empty (i.e. it 
does not consists of any elements). 
This has to be remembered, because even if we did not initialize the array, 
C# takes care to do so for the array that has to keep the parameters.

### Method with Variable Number of Arguments – Example

Bearing in mind how we define methods with variable number of arguments, 
we can write the Main() method of a C# program in the following way: 
```csharp
static void Main(params string[] args) 
{ 
 // Method body comes here 
} 
```
The definition above is valid and is accepted without any errors by the 
compiler.

## Optional Parameters and Named Arguments

Named arguments and optional parameters are two different functionalities of 
the C# language. However, they often are used together. These parameters 
are introduced in C#, version 4.0. Optional parameters  allow some 
parameters to be skipped when a method is invoked. Named arguments on 
their side, allow method parameter values to be set by their name, instead of 
their exact position in the parameters list. These two features in the C# 
language syntax are very useful in cases, when we invoke a method with a 
different combination of its parameters. 

Declaration of optional parameters can be done just by using a default value 
in the way shown below: 
```csharp
static void SomeMethod(int x, int y = 5, int z = 7) 
{ 
} 
```
In the example above y and z are optional and can be skipped upon method ’s 
invocation: 
```csharp
static void Main() 
{ 
 // Normal call of SomeMethod 
 SomeMethod(1, 2, 3); 
 // Omitting z - equivalent to SomeMethod(1, 2, 7) 
 SomeMethod(1, 2); 
 // Omitting both y and z – equivalent to SomeMethod(1, 5, 7) 
 SomeMethod(1); 
} 
```
We can pass a value by a particular parameter name , by setting  the 
parameter’s name, followed  by a colon and  the value of the  parameter. An 
example of using named arguments is shown below: 
```csharp
static void Main() 
{ 
 // Passing z by name and x by position 
 SomeMethod(1, z: 3); 
 // Passing both x and z by name 
 SomeMethod(x: 1, z: 3); 
 // Reversing the order of the arguments passed by name 
 SomeMethod(z: 3, x: 1); 
} 
```
All invocations in the sample above are equivalent to each other – parameter 
y is skipped, but x and z are set to 1 and 3. The only difference between the 
second and third call is that the parameter values are calculated in the same 
order they are passed to the method, in the last invocation 3 will be 
calculated before 1. In this example all parameters are constants and their 
purpose is only to clarify the idea of named and optional parameters . 
However, the mentioned consideration may lead to some unexpected behavior 
when the order of parameters calculation matters.

## Method Overloading

When in a class a method is declared and its name coincides with the name of 
another method, but their signatures differ by their parameters list (count 
of the method’s parameters or the way they are arranged), we say that there 
are different variations / overloads of that method  (method 
overloading). 
As an example, let ’s assume that we have to write a program that draws 
letters and digits to the screen. We also can assume that our program has 
methods for drawing strings DrawString(string str) , integers – 
DrawInt(int number), and floating point digits – DrawFloat(float number) 
and so on: 
```csharp
static void DrawString(string str) 
{ 
 // Draw string 
} 
 
static void DrawInt(int number) 
{ 
 // Draw integer 
} 
 
static void DrawFloat(float number) 
{ 
 // Draw float number 
} 
```
As we can see the C# language allows us to create variations of the same 
method Draw(…), called overloads. The method below gets combinations of 
different parameters, depending of what we want to write on the screen: 
```csharp
static void Draw(string str) 
{ 
 // Draw string 
} 
 
static void Draw(int number) 
{ 
 // Draw integer 
} 
 
static void Draw(float number) 
{ 
 // Draw float number 

} 
```
The definitions of the methods above are valid and will compile without error 
messages. The method Draw(…) is also called overloaded.

### Method Parameters and Method Signature

As mentioned above, there are only two things required in C# to specify a 
method signature: the parameter type  and the order in which the 
parameters are listed . The names of the method ’s parameters are not 
significant for the method’s declaration. 
 
The most important aspect of creating an unambiguous 
declaration of a method in C# is the definition of its 
signature and the type of the method ’s parameters in 
particular. 
For example in C#, the following two declarations  are actually declarations of 
one and the same method. That’s because the parameter type in each of their 
parameters is the same – int and float. So the names of the variables we 
are using – param1 and param2 or p1 and p2, are not significant: 
// These two lines will cause an error 
```csharp
static void DoSomething(int param1, float param2) { } 
static void DoSomething(int p1, float p2) { }  
```
If we declare two or more methods in one class, in the way shown above, the 
compiler will show an error message, which will look something like the one 
below: 
Type '<the_name_of_your_class>' already defines a member called 
'DoSomething' with the same parameter types. 
If we change the parameter type from a given position of the parameter 
list to a different type , in C# th ey will count as two absolutely different 
methods, or more precisely said, different variations of a method with 
the same name. 
For example if in the second method, the second parameter from the 
parameter list of any of the methods – float p2, is declared not as float, 
but as int for example , we will have two different methods with two 
different signatures – DoSomething(int, float) and DoSomething(int, 
int). Now the second element from their signature – parameter list , is 
different, due to difference of their second element type: 
```csharp
static void DoSomething(int p1, float p2) { } 
static void DoSomething(int param1, int param2) { } 


In this case even if we type the same name for the parameters, the compiler 
```
will accept this declaration, because they are practically different methods: 
```csharp
static void DoSomething(int param1, float param2) { } 
static void DoSomething(int param1, int param2) { } 
The compiler will accept the code again if we declare two variations of the 
```
method, but this time we are going to change the order of the parameters 
instead of their type. 
```csharp
static void DoSomething(int param1, float param2) { } 
static void DoSomething(float param2, int param1) { } 
```
In the example above the order of the parameter types  is different and 
this makes the signature different too. Since the parameter lists are different, 
it plays no role that the name (DoSomething) is the same for both methods. 
We still have different signatures for both methods.

### Overloaded Methods Invocation

Since we have declared methods with  the same name and different 
signatures, we can invoke each of them as any other method – just by using 
their name and arguments. Here is an example: 
```csharp
static void PrintNumbers(int intValue, float floatValue) 
{ 
 Console.WriteLine(intValue + "; " + floatValue); 
} 
 
static void PrintNumbers(float floatValue, int intValue) 
{ 
 Console.WriteLine(floatValue + "; " + intValue); 
} 
 
static void Main() 
{ 
 PrintNumbers(2.71f, 2); 
 PrintNumbers(5, 3.14159f); 
} 
```
When the code executes, we will see, that the first invocation refers to the 
second method, and the second invocation refers to the first method. Which 
method will be invoked depends on the type of the used parameters. The 
result after executing the code above is: 
2.71; 2 
5; 3.14159 

The lines below, however, will not compile and execute: 
```csharp
static void Main() 
{ 
 PrintNumbers(2, 3); 
} 
The reason for this not to work is that the compiler tries to convert both 
```
integer numbers to suitable types before passing them to any of the methods 
named PrintNumbers. In this case, however, these conversions are not equal. 
There are two possible options – either to convert the first parameter to 
float and call the method PrintNumbers(float, int) or to convert the 
second parameter to float and call the method PrintNumbers(int, float). 
This ambiguity has to be manually resolved, and one way to do so  is shown in 
the example below: 
```csharp
static void Main() 
{ 
 PrintNumbers((float)2, (short)3); 
} 
```
The code above will be compiled without errors, because after the arguments 
are transformed, it is clearly decided which method we refer to – 
PrintNumbers(float, int).

### Methods with Coinciding Signatures

We will  discuss some other interesting examples that show how to use 
methods. Let ’s take a look at an example of an incorrect redefinition 
(overload) of methods: 
```csharp
static int Sum(int a, int b) 
{ 
 return a + b; 
} 
 
static long Sum(int a, int b) 
{ 
 return a + b; 
} 
 
static void Main() 
{ 
 Console.WriteLine(Sum(2, 3)); 
} 

```
The code from the example will show an error message upon compilation 
process, because there are two methods with same parameters lists (i.e. with 
same signature) which return results of different types. This makes the 
method invocation ambiguous, so it is not allowed by the compiler.

## Triangles with Different Size – Example

It would be a good time now to give a little bit more complex example, since 
we know now how to declare methods with parameters, how to invoke them 
as well as how to get result b ack from those methods. Let ’s assume we want 
to write a program, which prints triangles on the console, as those shown 
below: 
n = 5 
1 
1 2 
1 2 3 
1 2 3 4 
1 2 3 4 5 
1 2 3 4 
1 2 3 
1 2 
1 
 
n = 6 
1 
1 2 
1 2 3 
1 2 3 4 
1 2 3 4 5 
1 2 3 4 5 6 
1 2 3 4 5 
1 2 3 4 
1 2 3 
1 2 
1 
 
A possible solution of this task is given below: 
Triangle.cs 
```csharp
using System; 
 
class Triangle 
{ 

 static void Main() 
 { 
  // Entering the value of the variable n 
  Console.Write("n = "); 
  int n = int.Parse(Console.ReadLine()); 
  Console.WriteLine(); 
 
  // Printing the upper part of the triangle 
  for (int line = 1; line <= n; line++) 
  { 
   PrintLine(1, line); 
  } 
 
  // Printing the bottom part of the triangle 
  // that is under the longest line 
  for (int line = n - 1; line >= 1; line--) 
  { 
   PrintLine(1, line); 
  } 
 } 
 
 static void PrintLine(int start, int end) 
 { 
  for (int i = start; i <= end; i++) 
  { 
   Console.Write(i + " "); 
  } 
  Console.WriteLine(); 
 } 
} 
```
Let’s discuss how the example code works. We should think of the triangles as 
sequences of numbers, placed on separate lines, since we can print each line 
directly on the console. In order to print each line of the triangle on the 
console we need a tool. For th is purpose we created the method 
PrintLine(…). 
In this method, by using a for-loop, we print a line of consequent numbers. 
The first number from this sequence is the first parameter from the method ’s 
parameter list (the variable start). The last element of the sequence is the 
number, passed to the method, as second parameter (the variable end). 
Notice that since the numbers are sequential, the length (count of the 
numbers) of each line corresponds to the difference between the second 
parameter end and the first one – start, from the methods parameters list 
(this will be useful later, when we build the triangles). 

Then we implement an algorithm that prints the triangles, as whole figures, in 
the Main() method. With another method int.Parse, we get the n variable 
and print the empty line. 
Now with two sequential for-loops we build the triangle according to the 
entered n. With the first loop we print all the lines that draw the upper part of 
the triangle and the middle (longest) line inclusively. With the second loop, we 
print the rest of the triangle’s lines that lie below the middle line. 
As we mentioned above, the line number, corresponds to the element count 
placed on the appropriate line. And since we always start from 1, the line 
number will always be equal to the last element in the sequence, which has to 
be printed on that line. So, we can use this when we call PrintLine(…), as it 
requires exactly that for its parameters. 
Note that, the count of the elements on each next line, increases with one and 
so the last element of each sequent line must be greater (one is added) than 
the last element of the preceding line. That ’s why at each loop iteration of the 
first for-loop, we pass to the PrintLine(…) method, as first parameter 1, and 
as a second – the current value of the variable line. Since, on each execution 
of the body of the loop, line increases with one, at each iteration  
PrintLine(…) the method prints a line that has more than one element than 
the preceding line. 
With the second loo p, that draws the part under the middle triangle line, we 
follow the reverse logic. The downward we print lines, the shorter lines we 
print. Each line decreases with one element according to its preceding line. 
Hence, we set the initial value for the varia ble line in the second loop: line = 
n-1. After each iteration  of the loop line decreases with one and pass it as 
second parameter to the PrintLine(…). 
We can improve the program, as we take the logic that prints the triangle, in 
a separate method. It can b e noticed that, logically, the triangle print is 
clearly defined, that is why we can declare a method with one parameter (the 
value that we get from the keyboard) and to invoke it from the Main() 
method: 
```csharp
static void Main() 
{ 
 Console.Write("n = "); 
 int n = int.Parse(Console.ReadLine()); 
 Console.WriteLine(); 
 
 PrintTriangle(n); 
} 
 
static void PrintTriangle(int n) 
{ 
 // Printing the upper part of the triangle 

 for (int line = 1; line <= n; line++) 
 { 
  PrintLine(1, line); 
 } 
 
 // Printing the bottom part of the triangle 
 // that is under the longest line 
 for (int line = n - 1; line >= 1; line--) 
 { 
  PrintLine(1, line); 
 } 
} 
If we execute the program and enter for n the value 3, we will get the 
```
following result: 
n = 3 
 
1 
1 2 
1 2 3 
1 2 
1