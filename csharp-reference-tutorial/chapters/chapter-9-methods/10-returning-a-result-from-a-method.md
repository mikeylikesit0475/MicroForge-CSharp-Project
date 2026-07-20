# Returning a Result from a Method

So far, we always were given examples, in which the method does something 
like printing on the console, and nothing more. Methods, however, usually do 
not just execute a simple code sequence, but in addition they often return 
results. So let’s take a look at how this actually happens.

## Declaring a Method that Returns a Result

Let’s see again how to declare a method. 
static <return_type> <method_name>(<parameters_list>) 
Earlier we said that at the place of <return type> we will always put void. 
Now we will extend this definition, as we will see, that void is not the only 
choice. Instead of void we can return any type either primitive (int, float, 
double, …) or by reference (as string or array), depending on the type of 
the result that the method shall return after its execution. 
For example, take a method that calculates the area of a square and instead 
of printing it to the console returns it as a result. So, the declaration would 
look as follows: 

static double CalcSquareSurface(double sideLength) 
As can be seen the result of the calculation of the area is of type double.

## How to Use the Returned Value?

When the method is executed and returns a value, we can imagine that C# 
puts this value where this method has been invok ed from. Then the program 
continues work with that value. Respectively, that returned value, we can use 
for any purpose from the calling method.

### Assigning to a Variable

We can also assign the result of the method execution to a variable of an 
appropriate type: 
// GetCompanyLogo() returns a string 
string companyLogo = GetCompanyLogo();

### Usage in Expressions

After a method returns a result, it can be used then in expressions too. 
So for example, to find the total price for invoice calculation, we must get the 
single price and to multiply it by the quantity: 
float totalPrice = GetSinglePrice() * quantity;

### Using the Returned Value as Method Parameter

We can pass the result from the method execution as value in the parameters 
list from another method: 
```csharp
Console.WriteLine(GetCompanyLogo()); 
```
In this example, in the beginning we invoke the method GetCompanyLogo(), 
and write it as an argument of the method WriteLine(). Right after the 
GetCompanyLogo() method finishes its execution it will return a result . Let’s 
say that the result will be " Microsoft Corporation". Then C# will put the 
result returned by the method ’s execution in the method ’s place. So we can 
assume that this is represented in the code in the following way: 
```csharp
Console.WriteLine("Microsoft Corporation");
```

## Returned Value Type

As it was already explained above, the result that a method returns can be of 
any type – int, string, array and so on. When, however, instead of a type 

we use the keyword void instead of a type, this mean that method does not 
return value.

## The Operator "return"

To make a method return value, the keyword return must be placed in the 
method’s body, followed by an expression that will be returned as a result 
by the method: 
```csharp
static <return_type> <method_name>(<parameters_list>) 
{ 
 // Some code that is preparing the method's result comes here 
 return <method's_result>; 
} 
```
Respectively <method's_result>, is of type <return_type>. For example: 
```csharp
static long Multiply(int number1, int number2) 
{ 
 long result = number1 * number2; 
 return result; 
} 
In this method after the multiplication, by using the return the method will 
```
produce as a result of its execution the integer variable result.

### Compatibility of the Result and the Retuning Type

The result returned by the method, can be of a type that is compatible (the 
one that can be implicitly converted) with the type of the returned value 
<return_type>. 
For example, we can modify the following example, in which the type of the 
returned value to be of type float, but not int and to keep the following 
code in the shown way: 
```csharp
static float Multiply(int number1, int number2) 
{ 
 int result = number1 * number2; 
 return result; 
} 
```
In this case after the multiplication execution, the result will be of type int. 
Even though the type of the expression after the return keyword is not of 
type float, it can be returned, because it can be implicitly converted to 
float.

### Using an Expression after the Return Operator

It is allowed (whenever this will not make the code look complicated / ugly) to 
directly put some expression after the keyword return: 
```csharp
static int Multiply(int number1, int number2) 
{ 
 return number1 * number2; 
} 
```
In this situation, after the calculation of number1 * number2, the result that 
this expression produces will be replaced where the expression is, and hence 
will be returned by the return operator.

### Features of the Return Operator

The execution of return does two things: 
- Stops immediately the method execution. 
- Returns the result of the executed method to the calling method. 
In relation to the first feature of return operator, we must note that, since it 
stops the method ’s execution (and no code after it and before the method 
body’s closing bracket will be executed), we  should not put any code after the 
return operation. 
Though, if we do so, the compiler will show a warning message: 
```csharp
static int Add(int number1, int number2) 
{ 
 int result = number1 + number2; 
 return result; 
 
 // Let’s try to "clean" the result variable here: 
 result = 0; 
} 
In this example the compilation will be successful, but for the lines after 
return, the compiler will output a warning message like this: 
```
Unreachable code detected 
When the method has void for returned value type, then after return, there 
would be no expression to be returned. In that case return usage is only 
used to stop the method’s execution: 
```csharp
static void PrintPositiveNumber(int number) 
{ 

 if (number <= 0) 
 { 
  // If the number is NOT positive, terminate the method 
  return; 
 } 
 Console.WriteLine(number); 
}
```

### Multiple Return Statements

The last thing that must be said about the operator return is that it can be 
called from several places in the code of our method, but should be 
guaranteed that at least one of the operators return that we have used, will 
be reached while executing the method. 
So let’s take a look, at the example for a method tha t gets two numbers, and 
then upon their values return 1 if the first is greater than the second, 0 if both 
are equal, or -1 if the second is greater than the first: 
```csharp
static int CompareTo(int number1, int number2) 
{ 
 if (number1 > number2) 
 { 
  return 1; 
 } 
 else if (number1 == number2) 
 { 
  return 0; 
 } 
 else 
 { 
  return -1; 
 } 
} 
Having multiple return statements  is usual in programming and is typical 
for methods that check several cases, like the above.
```

## Why Is the Returned Value Type not a Part of the Method Signature?

Why Is the Returned Value Type not a Part of the 
Method Signature? 
In C# it is not allowed to have several methods that have equal name and 
parameters, but different type of returned value. This means that the 
following code will fail to compile: 
```csharp
static int Add(int number1, int number2) 

{ 
 return (number1 + number2); 
} 
 
static double Add(int number1, int number2) 
{ 
 return (number1 + number2); 
} 
The reason for this limitation is that the compiler doesn ’t know which of both 
```
methods must be invoked.  Both methods have the same signature  
(sequence of parameters along with their types). Note that the return value is 
not part of the method’s signature.  That is why on the declaration of the 
methods an error message like the one below will be returned: 
Type '<the_name_of_your_class>' already defines a member called 
'Add' with the same parameter types 
Where <the_name_of_your_class> is the name of the class in which we have 
tried to declare those methods.

## Fahrenheit to Celsius Conversion – Example

Now we have to write a program that for a given (by the user) body 
temperature, measured in Fahrenheit degrees, has to convert that 
temperature and output it in Celsius degrees, with the following message: 
"Your body temperature in Celsius degrees is X", where X is respectively 
the Celsius degrees. In addition if the measured temperature in Celsius is 
higher than 37 degrees, the program should warn the user that they are ill, 
with the following message "You are ill!". 
For starters, we can make fast research in Internet and find out that the 
Celsius to Fahrenheit formula  is like this one: °C = (°F - 32) * 5 / 9, 
where respectively with °C we mark the temperature measured in Celsius, 
and with °F – the temperature in Fahrenheit. 
After analysis of the current task, we can see that it can be divided to 
subtasks as follow: 
- Take the temperature measured in Fahrenheit degrees as an input from 
the console (the user must enter it). 
- Convert that number to its corresponding value, for temperature 
measured in Celsius. 
- Print a message for the converted temperature in Celsius. 
- If the temperature is found to be higher than 37 ºC , print a message 
that the user is ill. 

A sample implementation of the above described algorithm is given below  in 
the class TemperatureConverter: 
TemperatureConverter.cs 
```csharp
using System; 
 
class TemperatureConverter 
{ 
 static double ConvertFahrenheitToCelsius(double temperatureF) 
 { 
  double temperatureC = (temperatureF - 32) * 5 / 9; 
  return temperatureC; 
 } 
 
 static void Main() 
 { 
  Console.Write( 
   "Enter your body temperature in Fahrenheit degrees: "); 
  double temperature = double.Parse(Console.ReadLine()); 
 
  temperature =  ConvertFahrenheitToCelsius(temperature); 
 
  Console.WriteLine( 
   "Your body temperature in Celsius degrees is {0}.", 
   temperature); 
 
  if (temperature >= 37) 
  { 
   Console.WriteLine("You are ill!"); 
  } 
 } 
} 
The operations for input of the temperature and output of the messages are 
```
trivial, so we will skip their explanation, as we will focus on the approach to 
convert t he temperatures. As we can see this is a logical unit that can be 
separated in its own method. By doing so, not only the program source code 
will get clearer, but moreover, we will have the opportunity to reuse that 
piece of code, whenever we need it, so we just will use the same method. So 
we declare the method ConvertFahrenheitToCelsius(…), with list of one 
parameter with the name temperatureF that represents the measured value 
of the temperature in Fahrenheit. Then the method returns a result of type 
double, which represents the calculated body temperature in Celsius degrees. 
In the metho d’s body we use the formula we found on Internet (and write it 
according to the C# syntax). 

Since we are done with this step from our task solution, we have decided that 
the rest of the steps we will not need to be in separate methods, so we just 
implement them in the Main() method of the class. 
By the method double.Parse(…), we get the user ’s body temperature as we 
have previously asked him for it, by the following message: " Enter your 
body temperature in Fahrenheit degrees". 
Then we invoke the method ConvertFahrenheitToCelsius() and we store 
the returned result in the variable temperature. 
```csharp
By the method Console.WriteLine() we print the message " Your body 
```
temperature in Celsius degrees is X ", where X is replaced with the value 
of temperature. 
The last step we must make is to check whether the temperature is higher 
than 37 degrees in Celsius or no. This can be done by using  a conditional 
statement if. So if the temperature is higher than 37 degrees Celsius a 
message that the user is ill must be printed. 
Below is shown a possible output of the program: 
Enter your body temperature in Fahrenheit degrees: 100 
Your body temperature in Celsius degrees is 37,777778. 
You are ill!

## Difference between Two Months – Example

Let’s take a look at the following task: we have to write a program which, by 
given two numbers, that are between 1 and 12 (so to correspond to a 
particular month) prints the count of months between these months. The 
message that must be printed to the console must be " There is X months 
period from Y to Z.", where X is the count of the months, that we must 
calculate, and Y and Z, are respectively the names of the months that mark 
start and end of the period. 
By reading carefully the task we will try to divide it into subtasks, that can be 
more easily solved, and then by combining them to get the whole solution. 
We can see that we have to solve the following subtasks: 
- To enter the months numbers that mark beginning and end of the 
period. 
- To calculate the period between the input months. 
- To print the message. 
- In the message instead of the numbers we entered, for beginning and 
end of the period, we must write their corresponding month names in 
English. 
A possible solution of the given task is shown below: 

Months.cs 
```csharp
using System; 
 
class Months 
{ 
 static string GetMonthName(int month) 
 { 
  string monthName; 
```
  switch (month) 
```csharp
  { 
```
    case 1: 
      monthName = "January"; 
      break; 
    case 2: 
     monthName = "February"; 
     break; 
    case 3: 
     monthName = "March"; 
     break; 
    case 4: 
     monthName = "April"; 
     break; 
    case 5: 
     monthName = "May"; 
     break; 
    case 6: 
     monthName = "June"; 
     break; 
    case 7: 
     monthName = "July"; 
     break; 
    case 8: 
     monthName = "August"; 
     break; 
    case 9: 
     monthName = "September"; 
     break; 
    case 10: 
     monthName = "October"; 
     break; 
    case 11: 
     monthName = "November"; 
     break; 

    case 12: 
     monthName = "December"; 
     break; 
    default: 
```csharp
     Console.WriteLine("Invalid month!"); 
     return null; 
  } 
  return monthName; 
 } 
 
 static void SayPeriod(int startMonth, int endMonth) 
 { 
  int period = endMonth - startMonth; 
  if (period < 0) 
  { 
   // Fix negative distance 
   period = period + 12; 
  } 
  Console.WriteLine( 
   "There is {0} months period from {1} to {2}.", 
```
   period, GetMonthName(startMonth), 
   GetMonthName(endMonth)); 
 } 
 
```csharp
 static void Main() 
 { 
  Console.Write("First month (1-12): "); 
  int firstMonth = int.Parse(Console.ReadLine()); 
 
  Console.Write("Second month (1-12): "); 
  int secondMonth = int.Parse(Console.ReadLine()); 
 
  SayPeriod(firstMonth, secondMonth); 
 } 
} 
```
The first task solution is trivial. In the Main() method we will use 
int.Parse(…) so we get the months for the period, the length of which we 
aim to calculate. 
Then we see that period calculation and message printing can be logically 
separated as a subtask, so we create a method SayPeriod(…) that has two 
parameters – numbers representing month numbers that mark the beginning 
and the end of the period. This method will not return a value but it will 
calculate period and print the message,  described in the task, to the console, 
```csharp
by the standard output – Console.WriteLine(…). 

```
Apparently, to find the length of the period between two months, we have to 
subtract the number of the beginning month from that of the end month. We 
consider also, that  if the second month has number less that the number of 
the first month, then the user most probably has had the assumption that the 
second month is not in the current year, but in the next one. That is why, if 
the difference between the two months is nega tive, we must add 12 to it – 
the length of a year in months, and so to find the length of the given period. 
Then we must print the message, as for the months names we use the 
method GetMonthName(…). 
The method that gets the month ’s name by its number can b e easily created 
with conditional switch-case statement, in which we could get the months 
for each of the input numbers. If the value is not in the range of  [1…12], the 
program will report an error. Later in the chapter "Exception Handling" we 
will discuss in detail s how to notify for an error occurring. You will be shown 
how to catch and deal with the exceptions (error notifications). However, for 
now we just will print a n error message to the console . This is generally an 
incorrect behavior and we will learn how to avoid it in the chapter " High-
Quality Code", section "What Should a Method Do". 
At the end, in the Main() method we invoke the SayPeriod() method, by 
entered numbers for beginning and end of the period. By doing so, we have 
completely solved the task. 
A possible output, if the input was 2 and 6, is shown below: 
First month (1-12): 2 
Second month (1-12): 6 
There is 4 months period from February to June.

## Input Data Validation – Example

In this task we must write a program that asks the user what time it is, by 
printing on the console "What time is it? ". Then the user must enter two 
numbers – one for hours and one for minutes. If the input data represents a 
valid time, the program must output the message " The time is hh:mm now.", 
where hh respectively means the hours, and mm – the minutes. If the entered 
hours or minutes are not valid, the program must print the message 
"Incorrect time!". 
After we read the task carefully, we see that it can be divided into the 
following subtasks: 
- Get input data for hours and minutes. 
- Check if input data is valid (input validation). 
- Print the corresponding message – either an error message, or the valid 
time message. 

We consider that getting the input data and printing the output messages will 
not be a problem anymore, so we will focus on input data validation, i.e. 
validation the numbers for hours and minutes. We know that the hours are in 
the range from 0 to 23 inclusive, and the minutes respectively from 0 to 59 
inclusive. Since the data (for hours and for minutes) has not the same nature, 
we decide to create two separate methods. One of them will check the validity 
of hours, while the other will check the validity for minutes. 
A solution is shown below: 
DataValidation.cs 
```csharp
using System; 
 
class DataValidation 
{ 
 static void Main() 
 { 
  Console.WriteLine("What time is it?"); 
 
  Console.Write("Hours: "); 
  int hours = int.Parse(Console.ReadLine()); 
 
  Console.Write("Minutes: "); 
  int minutes = int.Parse(Console.ReadLine()); 
 
  bool isValidTime = 
   ValidateHours(hours) && ValidateMinutes(minutes); 
  if (isValidTime) 
  { 
   Console.WriteLine("The time is {0}:{1} now.", 
    hours, minutes); 
  } 
  else 
  { 
   Console.WriteLine("Incorrect time!"); 
  } 
 } 
 
 static bool ValidateHours(int hours) 
 { 
  bool result = (hours >= 0) && (hours < 24); 
  return result; 
 } 
 
 static bool ValidateMinutes(int minutes) 

 { 
  bool result = (minutes >= 0) && (minutes <= 59); 
  return result; 
 } 
} 
```
The method that checks the hours is named ValidateHours(), and it gets a 
number of type int for the hours, and returns result of type bool, i.e. true if 
the input number is a valid hour, otherwise – false: 
```csharp
static bool ValidateHours(int hours) 
{ 
 bool result = (hours >= 0) && (hours < 24); 
 return result; 
} 
```
We use simple logic to declare method, which checks the validity of the 
minutes. We named it ValidateMinutes(), since it gets a parameter that is 
integer value and represents the minutes, and returns a value of type bool. If 
the input number is a valid minute value, the method will return as result 
true, otherwise – false: 
```csharp
static bool ValidateMinutes(int minutes) 
{ 
 bool result = (minutes >= 0) && (minutes <= 59); 
 return result; 
} 
```
Since we are done with the most complicated part of the task, we declare the 
Main() method. In its body we print out the question according to the task – 
"What time is it?". Then by the method int.Parse(…), we read from the 
console the numbers for hours and minutes, then the results are kept in the 
integer variables hours and minutes: 
```csharp
Console.WriteLine("What time is it?"); 
 
Console.Write("Hours: "); 
int hours = int.Parse(Console.ReadLine()); 
 
Console.Write("Minutes: "); 
int minutes = int.Parse(Console.ReadLine()); 
The result from the validation is kept in a variable of type bool – 
```
isValidTime, as we sequentially invoke the methods we have already 
declared – ValidateHours() and ValidateMinutes(), as of course we pass 

the appropriate variables hours and minutes to each of them. To validate the 
input data as a whole, we unite the results from the methods invocation with 
the operator for logical "and" &&: 
bool isValidTime = 
 ValidateHours(hours) && ValidateMinutes(minutes); 
After we stored the result, telling us whether the input data is valid or not, in 
the variable isValidTime, we use the conditional statement if, cope with the 
last problem for the given task – Printing the information to the user, whether 
```csharp
the input is valid or not. With the method Console.WriteLine(…), if 
```
isValidTime is true, we print on the console " The time is hh:mm now." 
where hh is respectively the value of the variable hours, and mm – of the 
variable minutes. In the else part of the conditional statement we print that 
the input time was invalid – "Incorrect time!". 
A possible output of the program, with correct data, is shown below: 
What time is it? 
Hours: 17 
Minutes: 33 
The time is 17:33 now. 
And here’s how the program behaves, when the data is incorrect: 
What time is it? 
Hours: 33 
Minutes: -2 
Incorrect time!

## Sorting – Example

Let’s try to create a method that sorts (puts in order) a set of values in 
ascending order. The result will be a string with the sorted numbers. 
With this in mind, we suppose that the subtasks we have to cope with are 
two: 
- How to give the numbers to our method, so it could sort them 
- How to sort those numbers 
Our method has to take an array on numbers as a para meter, create a sort of 
that array and return it: 
```csharp
static int[] Sort(int[] numbers) 
{ 
 // The sorting logic comes here … 

 
 return numbers; 
} 
```
This solution seems to satisfy the task requirements. However, it seems that 
we could optimize it more, and instead  of the argument to be an integer 
array, we can declare it in such way that it could accept a variable count of 
integer parameters. 
This will save us the need to initialize the array in advance when we invoke 
the method with a small set of numbers. In case  of bigger sets of input 
numbers, as we saw in the subsection for method declaration with a variable 
number of arguments, we could directly pass an already initialized array of 
integers, instead of passing them as par ameters of the method. Hence, the 
initial declaration turns into: 
```csharp
static int[] Sort(params int[] numbers) 
{ 
 // The sorting logic comes here … 
 
 return numbers; 
} 
Now we must to decide how to sort our array. One of the easiest ways for this 
```
to be done is to use the so -called "selection sort" algorithm. This method 
considers the array as two parts – sorted and unsorted. The sorted part is in 
the left side of the array, while the unsorted is in the right. For each step of 
the algorithm, the sorted part expan ds to the right with one element and the 
unsorted shrinks with one element from its left part. 
Let’s take a look at an example. So assume we have the following unsorted 
array and we want to order its elements by selection sorting: 
 
On each step our algorithms must find the minimal element in the unsorted 
part of the array: 
 
Then the minimal element must swap with the first element from the unsorted 
part of the array: 
10 3 5 -1 0 12 810 3 5 -1 0 12 8
10 3 5 -1 0 12 8
min
10 3 5 -1 0 12 8
min

 
Then we look for the minimal element again, from the rest of the unsorted 
part of the array (all elements except the first one): 
 
That minimal element now exchanges with the first from the unsorted part: 
 
 
So this step is repeated until the unsorted part of the array reaches a length 
of 0, i.e. it is empty: 
 
 
As a result the array is sorted: 
 
This is a variant of a code, which implements the algorithm explained above 
(selection sort): 
```csharp
static int[] Sort(params int[] numbers) 
{ 
 // The sorting logic: 
```
10 3 5 -1 0 12 8
min
10 3 5 -1 0 12 810 3 5 -1 0 12 8
minmin
-1 3 5 10 0 12 8
min
-1 3 5 10 0 12 8
minmin
-1 3 5 10 0 12 8
min
-1 3 5 10 0 12 8
min
-1 3 5 10 0 12 8
minmin
-1 0 5 10 3 12 8
min
-1 0 5 10 3 12 8
minmin
-1 0 3 5 8 12 10
min
-1 0 3 5 8 12 10-1 0 3 5 8 12 10
minmin
-1 0 3 5 8 12 10
min
-1 0 3 5 8 12 10
min
-1 0 3 5 8 12 10-1 0 3 5 8 12 10
minmin
-1 0 3 5 8 10 12-1 0 3 5 8 10 12

 for (int i = 0; i < numbers.Length - 1; i++) 
```csharp
 { 
  // Loop operating over the unsorted part of the array 
  for (int j = i + 1; j < numbers.Length; j++) 
  { 
   // Swapping the values 
   if (numbers[i] > numbers[j]) 
   { 
    int temp = numbers[i]; 
    numbers[i] = numbers[j]; 
    numbers[j] = temp; 
   } 
  } 
 } // End of the sorting logic 
 return numbers; 
} 
Let’s declare a method PrintNumbers(params int[]) that outputs the list 
```
with numbers to the console, and then to test this example by writing a few 
lines directly into the Main(…) method: 
SortingEngine.cs 
```csharp
using System; 
 
class SortingEngine 
{ 
 static int[] Sort(params int[] numbers) 
 { 
  // The sorting logic: 
  for (int i = 0; i < numbers.Length - 1; i++) 
  { 
   // Loop that is operating over the un-sorted part of 
   // the array 
   for (int j = i + 1; j < numbers.Length; j++) 
   { 
    // Swapping the values 
    if (numbers[i] > numbers[j]) 
    { 
     int oldNum = numbers[i]; 
     numbers[i] = numbers[j]; 
     numbers[j] = oldNum; 
    } 
   } 

  } // End of the sorting logic 
  return numbers; 
 } 
 
 static void PrintNumbers(params int[] numbers) 
 { 
  for (int i = 0; i < numbers.Length; i++) 
  { 
   Console.Write("{0}", numbers[i]); 
   if (i < (numbers.Length - 1)) 
   { 
    Console.Write(", "); 
   } 
  } 
 } 
 
 static void Main() 
 { 
  int[] numbers = Sort(10, 3, 5, -1, 0, 12, 8); 
  PrintNumbers(numbers); 
 } 
} 
```
After this code is compiled and executed, the result is exactly as the one that 
was expected – the array is ordered ascending: 
-1, 0, 3, 5, 8, 10, 12