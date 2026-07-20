# Expressions

Much of the program’s work is the calculation of e xpressions. Expressions 
are sequences of operators, literals and variables  that are calculated to 
a value of some type (number, string, object or other type). Here are some 
examples of expressions: 
int r = (150-20) / 2 + 5; 
 
// Expression for calculating the surface of the circle 
double surface = Math.PI * r * r; 
 
// Expression for calculating the perimeter of the circle 
double perimeter = 2 * Math.PI * r; 
 
```csharp
Console.WriteLine(r); 
Console.WriteLine(surface); 
Console.WriteLine(perimeter); 
```
In the example three expressions are defined. The first expression calculates 
the radius of a circle. The second calculates the area of a circle, and the last 
one finds the perimeter. Here is the result from the fragment above: 
70 
15393.80400259 
439.822971502571

## Side Effects of Expressions

The calculation of the expression can have side effects , because the 
expression can contain embedded assignment operators, can cause increasing 
or decreasing of the value and calling methods. Here is an example of such a 
side effect: 
int a = 5; 
int b = ++a; 
 
```csharp
Console.WriteLine(a); // 6 
Console.WriteLine(b); // 6
```

## Expressions, Data Types and Operator Priorities

When writing expressions , the data types and the behavior of the used 
operators should be considered. Ignoring this can lead to unexpected results. 
Here are some simple examples: 
// First example 
double d = 1 / 2; 
```csharp
Console.WriteLine(d); // 0, not 0.5 
 
// Second example 
double half = (double)1 / 2; 
Console.WriteLine(half); // 0.5 
```
In the first example, an expression divides two integers (written this way, 1 
and two are integers) and assigns the result to a variable of type double. The 
result may be unexpected for some people, b ut that is because they are 
ignoring the fact that in this case the operator "/" works over integers and the 
result is an integer obtained by cutting the fractional part. 
The second example shows that if we want to do division with fractions in the 
result, it is necessary to convert to float or double at least one of the 
operands. In this scenario the division is no longer integer and the result is 
correct.

## Division by Zero

Another interesting example is division by 0. Most programmers think that 
division b y 0 is an invalid operation and causes an error at runtime 
(exception) but this is actually true only for integer division by 0. Here is an 
example, which shows that fractional division by 0 is Infinity or NaN: 
int num = 1; 
double denum = 0; // The value is 0.0 (real number) 
int zeroInt = (int) denum; // The value is 0 (integer number) 
```csharp
Console.WriteLine(num / denum); // Infinity 
Console.WriteLine(denum / denum); // NaN 
Console.WriteLine(zeroInt / zeroInt); // DivideByZeroException
```

## Using Brackets to Make the Code Clear

When working with expressions it is important to use brackets  whenever 
there is the slightest doubt about the priorities of the operations. Here is an 
example that shows how useful the brackets are: 
double incorrect = (double)((1 + 2) / 4); 
```csharp
Console.WriteLine(incorrect); // 0 

 
double correct = ((double)(1 + 2)) / 4; 
Console.WriteLine(correct); // 0.75 
 
Console.WriteLine("2 + 3 = " + 2 + 3); // 2 + 3 = 23 
Console.WriteLine("2 + 3 = " + (2 + 3)); // 2 + 3 = 5
```