# Constructing Strings: the StringBuilder Class

As explained above, strings in C# are immutable. This means that any 
adjustments applied to an existing string do not change it but return a new 
string. For example, using methods like Replace(…), ToUpper(…), Trim(…) do 
not change the string, which they are called for. They allocate a new area in 
the memory  where the new content is saved. This behavior has many 
advantages but in some cases can cause performance problems.

## Strings Concatenation in a Loop: Never Do This!

Serious performance problems  may be encountered when trying to 
concatenate strings in a loop. The problem is directly related to the strings 
handling and dynamic memory, which is used to store them. To understand 
why we have poor performance when concatenating strings in a loop , 
we must first consider what happens when using operator "+" for strings.

### How Does the String Concatenation Works?

We already got familiar with the ways to do string concatenation in C#. Let ’s 
now examine what happens in memory when concatenating strings . 
Consider two variables str1 and str2 of type string, which have values of 
"Super" and "Star". There are two areas in the heap (dynamic memory) in 
which the values are stored. The task of str1 and str2 is to keep a reference 
to the memory addresses where our dat a is stored. Let ’s create a variable 
result and give it a value of the other two strings by concatenation. A code 
fragment for creating and defining the three variables would look like this:  
string str1 = "Super"; 
string str2 = "Star"; 
string result = str1 + str2; 
What will happen with the memory? Creating the variable result will allocate 
a new area in dynamic memory, which will record the outcome of the str1 + 
str2, which is "SuperStar". Then the variable itself will keep the address of 
the allocated area. As a result we will have three areas in memory and three 
references to them. This is convenient, but allocating a new area, recording a 
value, creating a new variable and referencing it in the memory is time -
consuming process that would be  a problem when repeated many times , 
typically inside a loop. 
Unlike other programming languages, in C# is not necessary to manually 
dispose the objects stored in memory. There is a special mechanism called a 
garbage collector (memory cleaning system) , whi ch takes care of 
clearing the unused memory and resources. The garbage collector is 

 
responsible for disposing of objects in dynamic memory when they are no 
longer used. Creation of many objects containing multiple references in 
dynamic memory is bad, becau se it fills memory and then the garbage 
collector is automatically enforced to start execution. It takes quite some time 
and slows the overall performance of the process . Furthermore, 
transferring characters from one place to another in memory (when string  
concatenation is executed) is slow, especially if the strings are long.

### Why Concatenating Strings in a Loop is a Bad Practice?

Assume that we have a task to store the numbers from 1 to 20 ,000 
consecutively to each other in a variable of type string. How c an we solve 
the problem with our already existing knowledge? One of the easiest ways for 
implementation is to create a variable that stores the numbers and execute a 
loop from 1 to 20 ,000 in which each number is concatenated to the variable. 
Implemented in C#, the solution would look like this: 
string collector = "Numbers: "; 
for (int index = 1; index <= 20000; index++) 
```csharp
{ 
 collector += index; 
} 
```
Execution of the above code will loop 20 ,000 times and after each iteration  
will add the current index to the collector variable. collector’s value after 
implementation would be: "Numbers: 12345678910111213141516 …" (the 
numbers from 17 to 20,000 are replaced with dots because we don ’t have the 
space to write something that long here). 
Probably you have not noticed  the delay in the fragment’s execution. Indeed, 
```csharp
using concatenation in the loop has delayed significantly  the normal 
```
calculation process. On an average PC (as of January 2012) the loop iteration 
takes 1-2 seconds . The user of our program would be very skeptical if he 
has to wait a few seconds for something so simple such as concatenating the 
numbers from 1 to 20 ,000. Moreover, in this case 20 ,000 is just an example 
endpoint. What will be the delay if instead of 20 ,000 the user needs to 
concatenate numbers to 200,000? Try it!

### Concatenating in Loop of 200,000 Iterations – Example

Let’s develop further the example above. First, we will change the endpoint of 
the loop from 20 ,000 to 200,000. Second, in order to account properly the 
execution time, we will di splay on the console the current date and time 
before and after execution of the loop. Third, to see whether the variable 
contains the desired value, we will also display part of it on the console. If you 
want to make sure that the whole value is stored, y ou can remove the 
method Substring(…), but the print itself in this case will take a long time. 
The final version of the example would look like this: 

```csharp
class SlowNumbersConcatenator 
{ 
 static void Main() 
 { 
  Console.WriteLine(DateTime.Now); 
 
  string collector = "Numbers: "; 
  for (int index = 1; index <= 200000; index++) 
  { 
   collector += index; 
  } 
 
  Console.WriteLine(collector.Substring(0, 1024)); 
  Console.WriteLine(DateTime.Now); 
 } 
} 
```
When executing the example implementation on the console, the program 
starting date and time, the first 1024 characters of the variable and program 
completion date and time are displayed on the console. The reason to show 
only the first 1024 characters is that we want to measure only the calculation 
time without the time for printing the results. Printing the whole result will be 
time consuming. Let’s see sample output from the execution: 
 
Program start is marked with a green line and its end – with red. Note the 
execution time – about 5-6 minutes (on our compute r from January 2012) ! 
Such a delay is unacceptable for such a task and will not only make the user 
nervous but will make him stop the program without waiting for it to end.

### Processing Strings in the Memory

The problem with time -consuming Loop processing is  related to the way 
strings work in memory. Each iteration creates a new object in the heap and 
point the reference to it. This process requires a certain physical time. 
Several things happen at each step: 
1. An area of memory is allocated for recording the n ext number 
concatenation result. This memory is used only temporarily while 
concatenating, and is called a buffer. 
2. The old string is moved into the new buffer. If the string is long (say 
500 KB, 5 MB or 50 MB), it can be quite slow! 
3. Next number is concatenated to the buffer. 
4. The buffer is converted to a string. 
5. The old string and the temporary buffer become unused. Later they are 
destroyed by the  garbage collector . This may also be a slow 
operation. 
Much more elegant and appropriate way to concatenate strings in a Loop is 
using the StringBuilder class. Let’s see how it works.

## Building and Changing Strings with StringBuilder

StringBuilder is a class that serves to build and change strings. It 
overcomes the performance problems  that arise when concatenating 
strings of type string. The class is built in the form of an array of characters 
and what we need to know about it is that the information in it can be freely 
changed. Changes that are required in the  variables of type StringBuilder, 
are carried out in the same area of memory (buffer), which saves time and 
resources. Changing the content does not create a new object but simply 
changes the current. 
Let’s rewrite the code above in which we concatenated strings in a loop. If you 
remember, the operation previously took 5 minutes. Let ’s measure how long 
will take the same operation if we use StringBuilder: 
```csharp
class ElegantNumbersConcatenator 
{ 
 static void Main() 
 { 
  Console.WriteLine(DateTime.Now); 
 
  StringBuilder sb = new();
  sb.Append("Numbers: "); 
 
  for (int index = 1; index <= 200000; index++) 

  { 
   sb.Append(index); 
  } 
 
  Console.WriteLine(sb.ToString().Substring(0, 1024)); 
  Console.WriteLine(DateTime.Now); 
 } 
} 
```
This example is  based on the previous one, with only minor adjustments. 
Return value is the same, but what about the execution time? 
 
The time required to concatenate 200 ,000 characters with StringBuilder is 
now less than a second (perhaps few milliseconds)!

### Reversing a String – Example

Consider another example: we want to reverse an existing string 
(backwards). For example, if we have the string "abcd", the returned result 
should be "dcba". We get the original string, iterate it backwards character by 
character and add each character to a variable of type StringBuilder: 
```csharp
public class WordReverser 
{ 
 static void Main() 
 { 
  string text = "EM edit"; 
  string reversed = ReverseText(text); 


 
  Console.WriteLine(reversed); 
 
  // Console output: 
  // tide ME 
 } 
 
 static string ReverseText(string text) 
 { 
  StringBuilder sb = new();
  for (int i = text.Length - 1; i >= 0; i--) 
  { 
   sb.Append(text[i]); 
  } 
  return sb.ToString(); 
 } 
} 
```
In this example we have a variable text, which contains the value "EM edit". 
We pass the variable to the ReverseText(…) method and set the new value in 
a variable named reversed. The method, in turn, iterates the charac ters of 
the variable in reverse order and stores them in a new variable of type 
StringBuilder, but now back ordered. Ultimately, the result is "tide ME".

## How Does the StringBuilder Class Work?

The StringBuilder class is an implementation of a string in C#, but different 
than the class String. Unlike the already familiar for us strings, the objects of 
the StringBuilder class are not immutable, namely edit operations do not 
require creating a new object  in the memory. This reduces the 
unnecessary transfer of data in memory when performing basic operations 
such as string concatenation. 
StringBuilder keeps a buffer with a certain capacity  (16 characters by 
default). The buffer is implemented as an array of characters that is provided 
to the developer by a user-friendly interface – methods that quickly and easily 
add and edit elements o f the string. At any moment part of the characters in 
the buffer are used and the rest stay in reserve. This allows the addition to 
work very quickly. Other operations also operate faster than the class string, 
because the changes do not create a new object. 
Once the internal buffer of the StringBuilder is full, it automatically is 
doubled (the internal buffer is resized to increase its capacity while its content 
is kept unchanged). Resizing is a slow operation  but is happens rarely so 
the total performance  is good. We will discuss this in more details in the 
chapter about "Algorithms Complexity". 

Let’s create an object of the StringBuilder class with 15 characters long 
buffer. We add the string: "Hello, C#!" to it and we get the following code: 
StringBuilder sb = new StringBuilder(15); 
sb.Append("Hello, C#!"); 
After creating the object and storing the value in it, the StringBuilder will 
look as follows: 
 
Colored elements are the filled with our content part of the buffer. Normally, 
adding a new character to the variable does not create a new object in the 
memory but use the already allocated and unused space. If the entire 
capacity of the buffer is filled,  then the buffer is doubled as we already 
explained.

## StringBuilder – More Important Methods

The StringBuilder class provides us with a set of methods that help us to 
easily and efficiently edit text data and construct text. We met some of them 
in the examples. The most important are: 
- StringBuilder(int capacity)  – constructor with an initial capacity 
parameter. It may be used to set the buffer size in advance if we have 
estimates of the number of iterations and concatenations, which will be 
performed. This way we can save unnecessary dynamic memory 
allocations. 
- Capacity – returns the buffer size (total number of used and unused 
positions in the buffer). 
- Length – returns length of string saved in the variable (number of used 
positions in the buffer) 
- Indexer [int index] – return the character stored in given position. 
- Append(…) – appends string, number or other value after the last 
character in the buffer. 
- Clear(…) – removes all characters from the buffer (deletes it). 
- Remove(int startIndex, int length) – removes (deletes) string from 
the buffer with a given start position and length. 


 
- Insert(int offset, string str) – inserts a string in a given start 
position (offset). 
- Replace(string oldValue, string newValue) – replaces all occurren -
ces of a given substring with another substring. 
- ToString() – returns the StringBuilder object content as a string 
object.

## Extracting All Capital Letters from a Text – Example

The next task is to extract all capital letters from a text . We can 
implement it in different ways – using an array, counter and filling the array 
with all capital l etters found; creating an object of type string and 
concatenate capitals one by one to it; using the class StringBuilder. 
Turning to the option of using an array, we have a problem: we do not know 
what will be array size, as we have no idea in advance how many are the 
capital letters in the text. We can create an array as large as the text, but 
thus wasting unnecessary space in memory and we must also maintain a 
counter that keeps where the array is full to. 
Another option is to use a variable of type string. As we will iterate the 
whole text and concatenate all capital letters to the variable, probably we will 
lose efficiency again due to the strings concatenation.

### StringBuilder: the Right Solution

The most viable solution to the task again is to use StringBuilder. We can 
start with an empty StringBuilder, iterate the letters of the given text 
character by character, verify that the current character is uppercase and 
concatenate the character at the end of our StringBuilder. Finally, we can 
return the final result by calling the ToString() method. Below is a sample 
implementation: 
```csharp
public static string ExtractCapitals(string str) 
{ 
 StringBuilder result = new();
 for (int i = 0; i < str.Length; i++) 
 { 
  char ch = str[i]; 
  if (char.IsUpper(ch)) 
  { 
   result.Append(ch); 
  } 
 } 
 return result.ToString(); 
} 

```
Calling ExtractCapitals(…) method and passing a specified text as a 
parameter to it, the return value is a string of all capital letters in the text, 
namely the initial string without all characters that are not capitalized. To 
check whether a character is uppercase we are using char.IsUpper(…) – a 
method from the standard .NET classes. You can view the char class 
documentation, because it offers other useful methods for handling 
characters.

> [!TIP]
> **Modern C# Note: String Interpolation**
> In C# 6.0, **String Interpolation** was introduced. Instead of using `string.Format` or manual string concatenation, you can prefix the string with `$` and put variables directly in curly braces:
> ```csharp
> string name = "John";
> int age = 30;
> Console.WriteLine($"Hello, my name is {name} and I am {age} years old.");
> ```
