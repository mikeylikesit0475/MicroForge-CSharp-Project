# Lambda Expressions

Lambda expressions are anonymous functions  that contain expressions 
or sequence of operators. All lambda expressions use the lambda operator =>, 
which can be read as “goes to”. The idea of the lambda expressions in C# is 
borrowed from the functional programming languages (e.g. Haskell, Lisp, 
Scheme, F# and others). The left side of the lambda operator specifies the 
input parameters and the right side holds a n expression or a code block 
that works with the entry parameters and conceivably returns some result. 
Usually lambda expressions are used as predicates or instead of delegates 
(a type that references a method  instance), which can be applied on 
collections, processing their elements and/or returning a certain result.

## Lambda Expressions – Examples

As an example, let ’s take the extension method FindAll(…), which can be 
used to filter the necessary elements. It works on a certain collection by 
applying a given predicate on it that checks if an element matches a certain 
requirement. In order to use it we have to add a reference to the assembly 
System.Core.dll (if it is not already added) and include the namespace  
System.Linq, because the extension methods for the collections are there. 
For example, if we want to take only the even numbers from a collection of 
integers, we can use the method FindAll(…) on that collection, passing a 
lambda method to it that checks if a certain number is even: 

List<int> list = new List<int>() { 1, 2, 3, 4, 5, 6 }; 
List<int> evenNumbers = list.FindAll(x => (x % 2) == 0); 
foreach (var num in evenNumbers) 
```csharp
{ 
 Console.Write("{0} ", num); 
} 
Console.WriteLine(); 
```
The result is: 
2 4 6 
The example above loops through the whole collection of numbers and for 
each element (named x) a check, if the number is multiple of 2, is made 
(through the Boolean expression (x % 2) == 0). 
Let’s now focus on an example in which trough an extension method and a 
lambda expression  we will create a collection, containing data from a 
certain class. In the example, from the class Dog (with properties Name and 
Age), we want to get a list that contains all dogs’ names. We can do that with 
the extension method Select(…) (defined in the namespace System.Linq) 
by assigning to it to turn each dog ( x) into dog ’s name (x.Name) and writing 
that result in the variable names. With the keyword var, we tell the compiler 
to define the type of the variable according to the result that we assign on the 
right side of the equals sign. 
```csharp
class Dog 
{ 
 public string Name { get; set; } 
 public int Age { get; set; } 
} 
 
static void Main() 
{ 
 List<Dog> dogs = new List<Dog>() { 
  new Dog { Name = "Rex", Age = 4 }, 
  new Dog { Name = "Sean", Age = 0 }, 
  new Dog { Name = "Stacy", Age = 3 } 
 }; 
 var names = dogs.Select(x => x.Name); 
 foreach (var name in names) 
 { 
  Console.WriteLine(name); 
 } 

} 
```
The result is: 
Rex 
Sean 
Stacy

## Using Lambda Expressions with Anonymous Types

We can create collections of anonymous types from a collection with some 
elements by using lambda expressions . Let ’s take the collection dogs, 
containing elements of type Dog, and create new collection consisting of 
elements of an anonymous type, having two properties – age and the initial 
letter of the dog’s name: 
var newDogsList = dogs.Select( 
 x => new { Age = x.Age, FirstLetter = x.Name[0] }); 
foreach (var item in newDogsList) 
```csharp
{ 
 Console.WriteLine(item); 
} 
```
The result is: 
{ Age = 4, FirstLetter = R } 
{ Age = 0, FirstLetter = S } 
{ Age = 3, FirstLetter = S } 
As it is obvious from the example above, the newly created collection 
newDogsList has elements of an anonymous type, taking the properties Age 
and FirstLetter as parameters. The first line of the example can be read as 
follows: "Create a variable of undefined (at this point) type, name it 
newDogsList and create a new element of an anonymous type for each 
element x of the dogs collection with two properties: Age that is equal to the 
property Age of the element x, and the property FirstLetter that is equal to 
the first character of the string x.Name".

## Sorting with Lambda Expressions

If we want to sort the elements in a certain collection, we can use the 
extension methods OrderBy(…) and OrderByDescending(…), by defining the 
way of sorting in a lambda function. An example on our collection dogs: 
var sortedDogs = dogs.OrderByDescending(x => x.Age); 

foreach (var dog in sortedDogs) 
```csharp
{ 
 Console.WriteLine(string.Format( 
  "Dog {0} is {1} years old.", dog.Name, dog.Age)); 
} 
```
The result is: 
Dog Rex is 4 years old. 
Dog Stacy is 3 years old. 
Dog Sean is 0 years old.

## Statements in Lambda Expressions

Lambda functions can also have a body. So far we have used lambda 
functions with only one statement. Now we will pay more attention to lambda 
functions that have a body. Let ’s return to the example with the even 
numbers. Suppose we want to print to the console the values of all numbers, 
to which our lambda f unction is applied to and to return the result if they are 
even or not. We can do it the following way: 
List<int> list = new List<int>() { 20, 1, 4, 8, 9, 44 }; 
// Process each argument with code statements 
var evenNumbers = list.FindAll((i) => 
```csharp
{ 
 Console.WriteLine("Value of i is: {0}", i); 
 return (i % 2) == 0; 
}); 
```
The result from the above code is: 
Value of i is: 20 
Value of i is: 1 
Value of i is: 4 
Value of i is: 8 
Value of i is: 9 
Value of i is: 44

## Lambda Expressions as Delegates

Lambda functions can be written in delegates. Delegates are such a type of 
variables that contains functions  (methods). Some standard delegate  types in 
.NET are: Action, Action<in T>, Action<in T1, in T2> , and so on and 
Func<out TResult>, Func<in T, out TR esult>, Func<in T1, in T2, 
out TResult> and so on. The types Func and Action are generic and 

contain the types of the return value, and the types of the parameters of the 
functions. The variables of such types are references to functions. Below is an 
example for using and assigning values to these types: 
Func<bool> boolFunc = () => true; 
Func<int, bool> intFunc = (x) => x < 10; 
if (boolFunc() && intFunc(5)) 
```csharp
{ 
 Console.WriteLine("5 < 10"); 
} 
```
The result is: 
5 < 10 
In the example above we define two delegates. The first one – boolFunc is 
a function that has no input parameters and returns a Boolean result. We 
have given an anonymous lambda function that does nothing and always 
returns true as a value to that function. The second delegate  intFunc takes 
as an argument an int variable and returns a Boolean value – true when x is 
less than ten, and false otherwise. At the end, in the if statement, we call 
these two delegates as we give to the second one value of 5 as an argument, 
and the result from their invocation is true, as we can see.