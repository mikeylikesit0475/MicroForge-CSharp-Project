# Enumerations

Earlier in this chapter  we discussed what constants are, how to declare and 
use them. In this connection we will now consider a part of the C# language, 
in which a variety of logically connected constants can be linked by means of 
language. These language constructs are the so-called enumerated types.

## Declaration of Enumerations

Enumeration is a structure, which  resembles a class but differs from it in 
that in the class body we can declare only constants . Enumerations can 
take values only from the constants listed in the type. An enumerated variable 
can have as a value one of the listed in the type constants but cannot have 
value null. 
Formally speaking, the enumerations can be declared using the reserved word 
enum instead of class: 
[<modifiers>] enum <enum_name> 
```csharp
{ 
```
 constant1 [, constant2 [, [, … [, constantN]] 
} 
Under <modifiers> we understand the access modifiers public, internal 
and private. The identifier <enum_name> follows the rules for class names in 
C#. Constants separated by commas are declared in the enumeration block. 
Consider an example. Let’s define an enumeration for the days of the week 
(we will call it Days). As we can guess, the constants that will appear in this 
particular enumeration are the names of the week days: 
Days.cs 
enum Days 
```csharp
{ 
```
 Mon, Tue, Wed, Thu, Fri, Sat, Sun 
} 
Naming of constants in one particular enumeration follows the same principles 
of naming of which we already explained in the "Naming Constants" section. 
Note that each of the constants lis ted in the enumeration is of type this 
enumeration, i.e. in our case Mon belongs to type Days, as well as each of the 
other constants. 
In other words, if we execute the following line: 
```csharp
Console.WriteLine(Days.Mon is Days); 

```
This will be printed as a result: 
True 
Let’s repeat again: 
 
The enumerations are a set of constants of type – this listed 
type.

## Nature of Enumerations

Each constant, which is declared in one enumeration,  is being associated with 
a certain integer. By default, for this hidden integer representation of 
constants in one enumeration int is being used. 
To show “the integer nature” of constants in the listed types let ’s try to 
figure out what ’s the numerical re presentation of the constant, which  
corresponds to “Monday” from the example of the previous subsection: 
int mondayValue = (int)Days.Mon; 
```csharp
Console.WriteLine(mondayValue); 
```
After we execute it, the result will be: 
0 
The values, associated with constants of a particular enumerated type by 
default are the indices in the list of constants of this type, i.e. numbers from 0 
to the number of constants in the type less 1. In this way, if we consider the 
example with the enum eration type for the week days, used in the previous 
subsection, the constant Mon is associated with the numerical value 0, the 
constant Tue with the integer value 1, Wed – with 2, etc. 
 
Each constant in one enumeration is actually a textual 
representation of an integer. By default this number is the 
constant’s index in the list of constants of a particular 
enumeration type. 
Despite the integer nature of constants in a particular enumeration, when we 
try to print a particular constant, its te xtual representation at the time of the 
constant’s declaration will be printed: 
```csharp
Console.WriteLine(Days.Mon); 
After we execute the code above we will get the following result: 
```
Mon

## Hidden Numerical Value of Constants in Enumeration

As we can guess it is possible to change the numerical value of constants 
in an enumeration . This is done when we assign the values we prefer to 
each of the constants at the time of declaration. 
[<modifiers>] enum <enum_name> 
```csharp
{ 
 constant1[=value1] [, constant2[=value2] [, … ]] 
} 
```
Accordingly value1, value2, etc. must be integers. 
To get a clearer idea of the given definition consider the following example: 
let’s have a class Coffee, which represents a cup of coffee that customers 
order in a coffee shop: 
Coffee.cs 
```csharp
public class Coffee 
{ 
 public Coffee() 
 { 
 } 
} 
```
In this facility customers can order different amounts of coffee, as the coffee 
machine has predefined values “small” – 100 ml, “normal” – 150 ml and 
“double” – 300 ml. Therefore, we can declare one enumeration CoffeeSize, 
which has respectively three cons tants – Small, Normal and Double, the 
correspondent qualities of which will be assigned: 
CoffeeSize.cs 
```csharp
public enum CoffeeSize 
{ 
 Small=100, Normal=150, Double=300 
} 
Now we can add a field and property to the class Coffee, which reflect the 
```
type of coffee the customer has ordered: 
Coffee.cs 
```csharp
public class Coffee 
{ 
 public CoffeeSize size; 

 
 public Coffee(CoffeeSize size) 
 { 
  this.size = size; 
 } 
 
 public CoffeeSize Size 
 { 
  get { return size; } 
 } 
} 
Let’s try to print the values of the coffee quantity for a normal and for one 
double coffee: 
static void Main() 
{ 
 Coffee normalCoffee = new(CoffeeSize.Normal);
 Coffee doubleCoffee = new(CoffeeSize.Double);
 
 Console.WriteLine("The {0} coffee is {1} ml.", 
  normalCoffee.Size, (int)normalCoffee.Size); 
 Console.WriteLine("The {0} coffee is {1} ml.", 
  doubleCoffee.Size, (int)doubleCoffee.Size); 
} 
```
As we compile and execute this method, the following will be printed:  
The Normal coffee is 150 ml. 
The Double coffee is 300 ml.

## Use of Enumerations

The main purpose of the enumerations is to replace the numeric values, 
which we would use, if there were no enumeration types. In this way the code 
becomes simpler and easier to read. 
Another very important app lication of the enumerations is the pressure 
exercised by the compiler to use constants from the enumerations and not 
just numbers. Thus we minimize future errors in the code. For example, if we 
use an int variable instead of a variable from enumerations a nd a set of 
constants for the valid values, nothing prevents us from assigning the variable 
any value, e.g. -6723. 
To make this clearer, consider the following example: create a class  "coffee 
price calculator ", which is calculating the price of each type o f coffee, 
offered in the coffee shop: 

PriceCalculator.cs 
```csharp
public class PriceCalculator 
{ 
 public const int SmallCoffeeQuantity = 100; 
 public const int NormalCoffeeQuantity = 150; 
 public const int DoubleCoffeeQuantity = 300; 
 
 public CashMachine() { } 
 
 public double CalcPrice(int quantity) 
 { 
```
  switch (quantity) 
```csharp
  { 
```
   case SmallCoffeeQuantity: 
    return 0.20; 
   case NormalCoffeeQuantity: 
    return 0.30; 
   case DoubleCoffeeQuantity: 
    return 0.60; 
   default: 
    throw new InvalidOperationException( 
     "Unsupported coffee quantity: " + quantity); 
  } 
 } 
} 
We have three constants for the capacity of the coffee cups in the coffee 
shop, respectively 100, 150 and 300 ml. Furthermore, we expect that users 
of our class will diligently use the defined constants, instead of numbers – 
SmallCoffeeQuantity, NormalCoffeeQuantity and DoubleCoffeeQuantity. 
The method CalcPrice(int) returns the respective price, calculating it by 
the submitted amount. 
The problem lies in the fact that someone may decide not to use the 
constants defined by us and may submit an invalid number as a parameter of 
our method, for example: -1 or 101. In this case, if the method does not 
check for invalid quantity, it will likely return a wrong price, which is incorrect 
behavior. 
To avoid this problem we will use one feature of these enumerations, namely 
constants in the enumeration type can be used in switch-case structures. 
They can be submitted as values of the operator switch and accordingly – as 
operands of the operator case. 

 
The constants of enumerations can be used in switch -case 
structures. 
Let’s rework the method, which  calculates the price for a cup of coffee, 
depending on the capacity of the cup. This time we will use the enumeration 
type CoffeeSize, which we declared in previous examples: 
```csharp
public double CalcPrice(CoffeeSize coffeeSize) 
{ 
```
 switch (coffeeSize) 
```csharp
 { 
```
  case CoffeeSize.Small: 
   return 0.20; 
  case CoffeeSize.Normal: 
   return 0.40; 
  case CoffeeSize.Double: 
   return 0.60; 
  default: 
   throw new InvalidOperationException( 
    "Unsupported coffee quantity: " + (int)coffeeSize); 
 } 
} 
As we can see in this example, the possibility for the users of our method to 
provoke unexpected behavior of the method is negligible, because we force 
them to use specific values which to be used as arguments, namely constants 
of enumerated CoffeeSize type. This is one of the advantages of constants, 
which are declared in enumeration types to constants declared in any class. 
 
Whenever possible, use enumerations instead of set of  
constants declared in a class. 
Before we finish with the enumeration section we should mention that the 
enumerations are to be used with caution when working with the switch-
case construct. For example, if one day the owner of the coffee shop buys 
many big cups (mugs) for coffee, we wil l need to add a new constant in the 
constant list of the enumeration CoffeeSize, which may be called, for 
example, Overwhelming: 
CoffeeSize.cs 
```csharp
public enum CoffeeSize 
{ 
 Small=100, Normal=150, Double=300, Overwhelming=600 
} 


When we try to calculate the coffee price with the new quantity, the method, 
```
which calculates the price,  will throw an exception, informing the user that 
such amount of coffee is not available in the coffee shop. 
What we should do to solve this problem is to add a new case-condition, 
which reflects the new constant in the enumerated CoffeeSize type. 
 
When we modify the list of constants in an existing 
enumeration, we should be careful not to break the logic of 
the code that already exists and uses the constants, declared 
so far.