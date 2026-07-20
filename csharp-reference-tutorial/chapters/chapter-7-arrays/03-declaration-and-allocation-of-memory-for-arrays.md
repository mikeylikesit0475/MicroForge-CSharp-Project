# Declaration and Allocation of Memory for Arrays

In C# the arrays have fixed length, which is set at the time of their 
instantiation and determines the total number of elements. Once the length of 
an array is set we cannot change it anymore. 
0  1  2  3  4
… … … … …
Array of 5 
elements
Element 
index
Element of 
an array

## Declaring an Array

We declare an array in C# in the following way: 
int[] myArray; 
In this example the variable myArray is the name of the array, which is of 
integer type (int[]). This means that we declared an array of integer 
numbers. With [] we indicate, that the variable, which we are declaring, is an 
array of elements, not a single element. 
When we declare an array type variable, it is a reference, which does not 
have a value (it points to null). This is because the memory for the elements 
is not allocated yet. 
The figure below shows how a declared array variable looks, when the 
memory for elements of the array is not allocated yet: 
 
In the program’s execution stack the variable with the name myArray is 
created and its value is set to null (meaning it holds no value).

## Creation of an Array – the Operator "new"

In C# we create an array  with the help of the keyword new, which is used to 
allocate memory: 
int[] myArray = new int[6]; 
In this example we allocate an array with length of 6 elements of type int. 
This means that in the dynamic memory (heap) an area of 6 integer numbers 
is allocated and they all are initialized with the value 0: 
 
HeapStack
myArray
HeapStack
myArray
.
.
.
0 0 0 0 0 0
0 1 2 3 4 5

The figure shows, that after the allocation of memory for the array the 
variable myArray points to an address in the dynamic memory, where the 
values are. In C#, the elements of an array are always stored in the dynamic 
memory (called also heap). 
During the allocation of the memory for an array we set the total number of 
the elements in the br ackets (a non -negative integer number), defining its 
length. The type of the elements is written after the reserved word new, so we 
indicate what type of elements are going to be allocated in the memory.

## Array Initialization and Default Values

Before we can use an element of a given array, it has to be initialized or to 
have a default value. In some programming languages there are no default 
values and then if we try to access an element, which is not initialized, this 
may cause an error. In C# all variables, including the elements of arrays have 
a default initial value . This value is either 0 for the numeral types or its 
equivalent for the non -primitive types (for example null for a reference type 
and false for the bool type). 
Of course we can set initial values explicitly. We can do this in different ways. 
Here is one of them: 
int[] myArray = { 1, 2, 3, 4, 5, 6 }; 
In this case we create and initialize the elements of the array at the time of 
the declaration. On the figure below we see how the array is allocated in the 
memory when its values are initialized at the moment of its declaration: 
 
With this syntax we use curly brackets instead of the operator new. Between 
the brackets we list the initial valu es of the array, separated by commas. 
Their count defines the length of the array.

## Declaration and Initialization of an Array – Example

Here is one more example how to declare and initialize an array: 
string[] daysOfWeek = 
 { "Monday", "Tuesday", "Wednesday","Thursday", "Friday", 
 "Saturday", "Sunday" }; 
HeapStack
myArray
.
.
.
1 2 3 4 5 6
0 1 2 3 4 5

In this case we allocate an array of seven elements of type string. The type 
string is a reference type (object) and its values are stored in the dynamic 
memory. The variable daysOfWeek is allocat ed in the stack memory, and 
points to a section of the dynamic memory containing the elements of the 
array. The type of each of these seven elements is string, which itself points 
to a different section of the dynamic memory, where the real value is stored. 
On this figure we see how the array is allocated in the memory:

## Boundaries of an Array

Arrays are by default zero-based, which means the enumeration of the 
elements starts from 0. The first element has the index 0, the second – 1, 
etc. In an array of N elements, the last element has the index N-1.

> [!TIP]
> **Modern C# Note: Collection Expressions**
> In C# 12, collection initialization was simplified using **Collection Expressions** (square brackets `[]` instead of curly braces `{}` or `new` arrays):
> ```csharp
> int[] numbers = [1, 2, 3, 4, 5];
> List<string> names = ["Alice", "Bob", "Charlie"];
> ```
