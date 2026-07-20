# Hash-Tables

Now let’s get familiar with the data structure hash-table, which implements 
the abstract data structure dictionary in a very efficient way. We  well 
explain in details how hash-tables actually work and why they are so efficient.

## Dictionary Implementation with Hash-Table

With a hash-table implementation, the time for accessing the elements in 
the dictionary is theoretically independent from their count. This is a very 
important advantage. 
Let’s make a comparison  between list and hash-table in the speed of 
searching. We take a list of randomly ordered elements . We want to check if 
a certain element is in the list. The worst case scenario is to check every 
element in the list, so as to give an explicit answer to the question “Does this 
list contain the element or n ot”. It’s obvious that the number of checks would 
depend (linear) of the number of elements. 
With hash-tables, if we have a key, the number of compa risons that we 
would need to do to find out if there is a key with this value, is constant and 
it does not d epend on the number of elements . How exactly we are 
achieving such efficiency, we will explain in more details below.

### What is a Hash-Table?

The data structure hash-table is usually implemented internally with an 
array. It consists of numerated elements (cells), each either holding a 
key-value pair or is empty (null). This at first sight, look like as if the 
elements were randomly placed in the array. At the positions that we don ’t 
have an ordered pair, we have an empty element ( null). The figure below 
illustrates how a hash-table might look like: 


 
The size of the internal storage array of the hash-table is called capacity. The 
load factor is a real number between 0 and 1, which stands for the ratio 
between the occupied elements and the current capacity. At the figure we 
have a hash -table with 3 elements and capacity m. The load factor for this 
hash-table would be 3/m. 
When adding or searching  for elements, a method for hashing the key  (hash 
function) is executed hash(key), that returns a number we call a hash-
code. When we take the division remainder of this hash -code and the 
capacity m we get a number between 0 and m-1: 
index = hash(key) % m 
At the figure there is a hash -table T with capacity m and hash -function 
hash(key): 
 
This value hash(k) gives us the position in the array at  which we search or 
add a certain key-value pair having this k. If the hash -function distributes 
the keys uniformly, in most cases  for every key a different hash value  will be 
assigned. In this way every cell of the array will have at most one key . 
Ultimately we get an extremely fast search and insertion of the elements : just 
calculate the hash function and obtain the cell assigned for the key. Of 
course it may occur that different keys would have the same hash code. We 
will examine this special case in more details later. 
i = hash(key)
0  i < m
0 1 2 3 4 5 m-1
... ... ... ...T
hash(key)
... ... ...

 
Use implementation of dictionary based on hash-table, when 
you need to find values by key with a maximum speed. 
The internal table’s capacity is increased  when the number of elements in 
the hash -table becomes greater or equal to a certain constant called fill 
factor (load factor, the maximal degree of filling). When increasing the 
capacity (usually doubling it), all of the elements are reordered by the  hash 
code of their keys and their assigned cell is calculated according to the new 
capacity. The load factor is significantly decreased after the reordering. This 
operation is time -consuming, but it is executed relatively rare, so it will not 
impact the overall performance of the "add" operation. 
Before we go further with the theory of hash -tables, let’s review how hash -
tables are implemented in C# and .NET Framework.

### Class Dictionary <K, V>

The class Dictionary<K, V> is a standard implementation of a dictionary 
based on hash-table in .NET Framework . Let ’s take a look at its main 
features. We will  examine a specific example that illustrates the use of this 
```csharp
class and its methods.
```

### Class Dictionary<K, V> – Main Operations

Creating a hash -table is done by calling some of the constructors of 
Dictionary<K, V> . Through them we can assign an initial value for the 
capacity and load factor. It ’s good if we know in advance the expected 
number of elements,  which would be added in our hash -table, so as to set it 
at the creation of the hash-table. This way we will  avoid the unneeded 
expansions of the hash-table and we will  achieve better performance. By 
default the value of the initial capacity is 16, and the load factor is 0.75. 
Let’s review the methods in the class Dictionary<K, V>: 
- void Add(K, V)  adds a new pair (key and a value)  to the hash -table. 
Throws an exception in the case that the key exists. This operation is 
extremely fast. 
- bool TryGetValue(K, out V) returns an element of type V via the out 
parameter for the given key or null, if there is no such key. The result 
of this operation will be true if such an element is found. The operation 
is very fast, because the algorithm for searching an element by key in 
the hash-table is with complexity about O(1) 
- bool Remove(K)  removes the element with this key. This operation 
works very fast. 
- void Clear() removes all the elements from the dictionary. 
- bool ContainsKey(K) check if there is an ordered pair with this key in 
the dictionary. This operation works extremely fast. 


- bool ContainsValue(V) checks if there is one or more ordered pairs 
with this value. Thi s operation is slow because it checks every element 
of the hash-table (like searching in a list). 
- int Count returns the number of ordered pairs within the dictionary. 
- Other operations – extracting all the keys, values or ordered pairs into a 
structure that could be iterated through using a loop.

### Students and Marks – Example

We will illustrate how to use some of the above described operations with an 
example. We have some students, and every one of them could have only one 
mark. We want to store the marks in a structure that would allow us to 
perform a fast search by the student’s name. 
For this task we create a hash-table with initial capacity of 6. It will use the 
student names for keys, and their marks for values. We will  add 6 sample 
students, and then we will check what’s happening when we print their data 
on the console. Here is how the code for this example should look like: 
```csharp
using System; 
using System.Collections.Generic; 
 
class StudentsExample 
{ 
 static void Main() 
 { 
  IDictionary<string, double> studentMarks = 
   new Dictionary<string, double>(6); 
 
  studentMarks["Alan"] = 3.00; 
  studentMarks["Helen"] = 4.50; 
  studentMarks["Tom"] = 5.50; 
  studentMarks["James"] = 3.50; 
  studentMarks["Mary"] = 4.00; 
  studentMarks["Nerdy"] = 6.00; 
 
  double marysMark = studentMarks["Mary"]; 
  Console.WriteLine("Mary's mark: {0:0.00}", marysMark); 
   studentMarks.Remove("Mary"); 
 
  Console.WriteLine("Mary's mark removed."); 
 
  Console.WriteLine("Is Mary in the dictionary: {0}", 
   studentMarks.ContainsKey("Mary") ? "Yes!": "No!"); 
 
  Console.WriteLine("Nerdy's mark is {0:0.00}.", 

   studentMarks["Nerdy"]); 
  studentMarks["Nerdy"] = 3.25; 
 
  Console.WriteLine( 
   "But we all know he deserves no more than {0:0.00}.", 
   studentMarks["Nerdy"]); 
 
  double annasMark; 
  bool findAnna = studentMarks.TryGetValue("Anna", 
   out annasMark); 
 
  Console.WriteLine( 
   "Is Anna's mark in the dictionary? {0}", 
   findAnna ? "Yes!": "No!"); 
 
  studentMarks["Anna"] = 6.00; 
  findAnna = studentMarks.TryGetValue("Anna", 
   out annasMark); 
 
  Console.WriteLine( 
   "Let's try again: {0}. Anna's mark is {1}", 
   findAnna ? "Yes!" : "No!", annasMark); 
 
  Console.WriteLine("Students and marks:"); 
 
  foreach (KeyValuePair<string, double> studentMark 
```
   in studentMarks) 
```csharp
  { 
   Console.WriteLine("{0} has {1:0.00}", 
    studentMark.Key, studentMark.Value); 
  } 
 
  Console.WriteLine( 
   "There are {0} students in the dictionary", 
   studentMarks.Count); 
  studentMarks.Clear(); 
  Console.WriteLine("Students dictionary cleared."); 
  Console.WriteLine("Is dictionary empty: {0}", 
   studentMarks.Count == 0); 
 } 
} 
```
The output of the program execution will be: 

Mary's mark: 4.00 
Mary's mark removed. 
Is Mary in the dictionary: No! 
Nerdy's mark is 6.00. 
But we all know he deserves no more than 3.25. 
Is Anna's mark in the dictionary? No! 
Let's try again: Yes!. Anna's mark is 6 
Students and marks: 
Alan has 3.00 
Helen has 4.50 
Tom has 5.50 
James has 3.50 
Anna has 6.00 
Nerdy has 3.25 
There are 6 students in the dictionary 
Students dictionary cleared. 
Is dictionary empty: True 
We can see that the students are not ordered when printed. This is because in 
hash-tables (unlike balanced trees) the elements are not kept sorted. 
Even if the current table capacity is changed while working with it, it is also 
highly possible that the ord er of the pairs could be changed as well. We will 
analyze the reason for this behavior later on. 
It is important to remember, that with hash -tables, we cannot rely on the 
elements being in order. If we need them ordered, we could sort the elements 
before printing. Another option would be using SortedDictionary<K, V>.

## Hashing and Hash-Functions

Now we will explain in more details the concept of hash-code used earlier. The 
hash-code is a number returned by the hash-function, used for the 
hashing the key . This number should be different for every key, or at least 
there should be a high chance for that.

### Hash-Functions

There is the concept of the perfect hash -function. One hash -function is 
called perfect, if for example you have N keys, and for each of them the 
function would add a different number in a reasonable interval (for example 
from 0 to N-1). 
Finding such a function in the common case is a very hard, almost 
impossible task. It ’s worth to use such functions when using sets of keys 
with predefined elements or when the set of keys is rarely changed. 
In practice there are also other, not so "perfect" hash-functions. 

Now we will take a look at a few examples for hash -functions, which are used 
directly with .NET libraries.

### The Method GetHashCode() in .NET Framework

Every .NET class has a method called GetHashCode() that returns a value of 
type int. This met hod is inherited by the class Object, which is the root 
member in the hierarchy of .NET classes. 
The implementation in the class Object of the method GetHashCode() does 
not guarantee  the unique value of the result. This means that the 
descendent classes ne ed to ensure that GetHashCode() is implemented in 
order to use it for a key in a hash-table. 
Another example for a hash -function that is directly built in .NET is used by 
the class int, byte and short (integer numbers). In this case the value of 
the number itself is used for the hash -code. For more complex types like 
strings all their elements (or at least the first few of them) are involved into 
calculation of their hash code. 
One more complex example for hash -function is the implementation of 
GetHashCode() in the class System.String: 
```csharp
public override unsafe int GetHashCode() 
{ 
 fixed (char* str = ((char*)this)) 
 { 
  char* chPtr = str; 
  int num = 352654597; 
  int num2 = num; 
  int* numPtr = (int*)chPtr; 
  for (int i = this.Length; i > 0; i -= 4) 
  { 
   num = (((num << 5) + num) + (num >> 27)) ^ numPtr[0]; 
   if (i <= 2) 
   { 
    break; 
   } 
   num2 = (((num2 << 5) + num2) + (num2 >> 27)) ^ numPtr[1]; 
   numPtr += 2; 
  } 
  return (num + (num2 * 1566083941)); 
 } 
} 
```
This implementation is complicated, but what we need to remember is that it 
tries to guarantee the uniqueness of the result : different hash code for 
different input strings . Note that the complexity of the algorithm for 

calculating the hash -code of string is proportional to Length / 4 or O(n), 
which means that the longer the string is the slower its hash -code would be 
calculated. Authors of the above code  use a small trick ( unsafe code) to 
directly work with the low-level representation of the string in the memory. 
We leave to the reader to take a look at other implementations of the method 
GetHashCode() in some of the most commonly used .NET types like int, 
DateTime, long, float and double. This can be done through a decompiler 
like JustDecompile. 
Now let ’s answer the question of how to implement ourselves this hash 
function for our classes. We already explained that leaving the 
implementation that is already built in the class object, is not an acceptable 
solution. Another very simple implementation is that we always return a fixed 
constant, for example: 
```csharp
public override int GetHashCode() 
{ 
 return 42; 
} 
If in a hash -table we use objects for keys from a class, that has the above 
```
implementation of GetHashCode(), it will  have very poor performance, 
because every time, when we add a new element in the table, we would have 
to insert it at the same place . Every time we search the hash -table, we will 
encounter the same element. 
In order to avoid the described behavior, we need the hash -function to 
distribute the keys evenly amongst the possible hash-code values.

### Collisions with Hash-Functions

The situation where two different keys have the same hash-code is called 
collision. A good example of collision is shown below: 
 
We will  look in more detail s how to solve the problem with collisions in the 
next paragraph. The simplest solution is obvious: order the pairs that have 
keys with the same hash -codes in a list or other data structure . Thus we 
don't solve the collisions but we accept them and we just put several key -
value-pairs in the same element in the underlying array in the hash -table. 
This approach for collision resolution is known as chaining: 
h("Pesho ) = 4
h("Kiro") = 2
h("Mimi") = 1
h("Ivan") = 2
h("Lili") = 12
h("Alan") = 4
h("Peter")= 2
h("Tom") = 1
h("Mary") = 2
h("Anna") = 12
collisioncollision

 
Therefore when using a constant 42 for hash -code our hash-table turns into a 
linear list and it becomes very inefficient.

### Implementing the Method GetHashCode()

We will give a standard algorithm for implementing GetHashCode(), when this 
is necessarily: 
First we need to choose which fields of the class will take part in the 
implementation of the Equals(object) method. This is necessary, because 
every time when Equals() returns true, the result from GetHashCode() 
should always return the same value. 
This way the fields that do not take part in Equals(), should not take part in 
GetHashCode() as well. 
After we choose which fields will take part for the calculation of 
GetHashCode(), we need to receive values from them (of type int). Here is a 
sample scheme: 
- If the field is bool, for true we take 1, and for false we take 0 (or 
directly call method GetHashCode() on bool). 
- If the field is of type int, byte, short, char, we can convert it to int, 
with the cast operator (int) (or we could directly call GetHashCode()). 
- If the field is type long, float or double, we could use the result from 
their own implementations of GetHashCode(). 
- If the field is not a primitive type, we could call the method 
GetHashCode() of this object. If the field value is null, we can return 0. 
- If the field is an array or a collection, we take the hash -code from every 
element of this collection. 
h("Pesho") = 4
h("Kiro") = 2
h("Mimi") = 1
h("Ivan") = 2
h("Lili") = m-1
h("Alan") = 4
h("Peter") 2
h("Tom") = 1
h("Mary") = 2
h("Anna") = m-1
collisioncollision
nullT
0 1 2 3 4 5 m-1
null ...null
Peter
Mary
null
Tom
null
Anna
null
Alan
null
= 

In the end we sum all the received int values, and before each addition we 
multiply the temporary result with a prime number (for example 83), while 
ignoring the eventual overflow of type int. For example, if we have 3 fields 
and their hash codes are f1, f2 and f3, our hash function could combine 
them though the formula hash = (((f1 * 83) + f2) * 83) + f3. 
At the end we obtain a hash-code, which is very well distributed in the range 
of all 32-bit values. We can expect, that with a hash -code calculated this way, 
the collisions would be rare , because every change in some of the fields 
taking part in GetHashCode() leads to a major  change in the hash code  and 
thus reduces the chance for collision.

### Implementing GetHashCode() – Example

Let’s illustrate the above algorithm with an example. We have a class whose 
objects are presented as points in the three -dimensional space. The point w ill 
be represented with its coordinates in the three dimensional space x, y and z: 
Point3D.cs 
```csharp
public class Point3D 
{ 
 public double X { get; set; } 
 public double Y { get; set; } 
 public double Z { get; set; } 
 
 public Point3D(double x, double y, double z) 
 { 
  this.X = x; 
  this.Y = y; 
  this.Z = z; 
 } 
 
 public override string ToString() 
 { 
  return String.Format("({0}, {1}, {2})",  
   this.X, this.Y, this.Z); 
 } 
} 
We can implement GetHashCode() easily using the above described algorithm 
```
that combines the hash values of the separate object fields: 
```csharp
public override bool Equals(object obj) 
{ 
 if (this == obj) 
  return true; 

 
 Point3D other = obj as Point3D; 
 
 if (other == null) 
  return false; 
 
 if (!this.X.Equals(other.X)) 
  return false; 
 
 if (!this.Y.Equals(other.Y)) 
  return false; 
 
 if (!this.Z.Equals(other.Z)) 
  return false; 
 
 return true; 
} 
 
public override int GetHashCode() 
{ 
 int prime = 83; 
 int result = 1; 
```
 unchecked 
```csharp
 { 
  result = result * prime + X.GetHashCode(); 
  result = result * prime + Y.GetHashCode(); 
  result = result * prime + Z.GetHashCode(); 
 } 
 
 return result; 
} 
```
This implementation is incomparably better, than returning a constant  or just 
one of the fields or their sum . Although the collisions might still happen, 
they would occur very rarely.

### Interface IEqualityComparer<T>

One of the most important things that we  have learned so far is that in order 
to use instances of a class as keys for a dictionary, the class needs to properly 
implement GetHashCode() and Equals(…). But what should we do if we want 
to use a class, that we cannot inherit or change? In this case the interface 
IEqualityComparer<T> comes to our aid. 
It defines the following two operations: 

- bool Equals(T obj1, T obj2) – returns true if obj1 and obj2 are 
equal 
- int GetHashCode(T obj) – returns the hash-code of given object 
As you might have already guessed, the dictionaries in .NET can use an 
instance of IEqualityComparer<T>, instead of using the corresponding 
methods of the given class that should be assigned for a key. This way the 
developers could use practically any class for a key of the dictionary, if they 
could assure IEqualityComparer<T> is implemented. Even more – when we 
pass IEqualityComparer<T> to a dictionary, we could change the way 
GetHashCode() and Equals(…) are calculated for every type, even for those 
built-in .NET Framework. This is because the dictionary uses interface 
methods instead of the corresponding m ethods of the class that is used for 
key. Here is an example of an implementation of IEqualityComparer for the 
```csharp
class Point3D that we looked earlier: 
public class Point3DEqualityComparer : IEqualityComparer<Point3D> 
{ 
 public bool Equals(Point3D point1, Point3D point2) 
 { 
  if (point1 == point2) 
   return true; 
 
  if (point1 == null || point2 == null) 
   return false; 
 
  if (!point1.X.Equals(point2.X)) 
   return false; 
 
  if (!point1.Y.Equals(point2.Y)) 
   return false; 
 
  if (!point1.Z.Equals(point2.Z)) 
   return false; 
 
  return true; 
 } 
 
 public int GetHashCode(Point3D obj) 
 { 
  Point3D point = obj as Point3D; 
  if (point == null) 
  { 
   return 0; 
  } 
 
  int prime = 83; 

  int result = 1; 
```
  unchecked 
```csharp
  { 
   result = result * prime + point.X.GetHashCode(); 
   result = result * prime + point.Y.GetHashCode(); 
   result = result * prime + point.Z.GetHashCode(); 
  } 
  return result; 
 } 
} 
```
Note that we implement both Equals(…) and GetHashCode(), not just 
GetHashCode() method. 
 
Remember that the keys in hash -tables need to have 
correctly defined Equals(…) and GetHashCode() to work 
properly. This is not required for the values, just for the keys. 
Always define both Equals(…) and GetHashCode(), never only 
one of them! 
In order to use Point3DEqualityComparer, it ’s enough to pass it as 
argument to our dictionary’s constructor. Here is an example: 
```csharp
static void Main() 
{ 
 IEqualityComparer<Point3D> comparer = 
  new Point3DEqualityComparer(); 
 Dictionary<Point3D, int> dict = 
  new Dictionary<Point3D, int>(comparer); 
 
 dict[new Point3D(4, 2, 5)] = 5; 
 dict[new Point3D(1, 2, 3)] = 1; 
 dict[new Point3D(3, 1, -1)] = 3; 
 dict[new Point3D(1, 2, 3)] = 10; 
 foreach (var entry in dict) 
 { 
  Console.WriteLine("{0} --> {1}", entry.Key, entry.Value); 
 } 
} 
```
The result from the above code is: 
(4, 2, 5) --> 5 
(1, 2, 3) --> 10 
(3, 1, -1) --> 3 


We have 3 unique keys in the dictionary and the key (1, 2, 3) is used twice.

## Resolving the Collision Problem

In practice, collisions happen almost always, excluding some rare and 
specific cases. That is why we need to live with the idea of the collisions 
presence in our hash -tables and take them into account. Let ’s have a look at 
several strategies for dealing with collisions.

### Chaining in a List

The most widespread method to resolve collisions  problem is called 
chaining. Its major concept consists of storing in a list all the pairs (key, 
value), which have the same hash-code for the key.

## Implementation of a Dictionary with Hash-Table and Chaining

Implementation of a Dictionary with Hash-Table and 
Chaining 
Let’s have the task to implement a dictionary data structure with a 
hash-table and to resolve the collisions by chaining . With the example 
below we will show how it could be done. First, we are going to define a class, 
describing the pair {key, value}: 
KeyValuePair.cs 
/// <summary>A structure holding a pair {key, value}</summary> 
/// <typeparam name="TKey">the type of the keys</typeparam> 
/// <typeparam name="TValue">the type of the values</typeparam> 
```csharp
public struct KeyValuePair<TKey, TValue> 
{ 
 /// <summary>Holds the key of the key-value pair</summary> 
 public TKey Key { get; private set; } 
 
 /// <summary>Holds the value of the key-value pair</summary> 
 public TValue Value { get; private set; } 
 
 /// <summary>Constructs a pair by given key + value</summary> 
 public KeyValuePair(TKey key, TValue value) : this() 
 { 
  this.Key = key; 
  this.Value = value; 
 } 
 
 /// <summary>Converts the key-value pair to a printable text. 
 /// </summary> 
 public override string ToString() 
 { 

  StringBuilder builder = new();
  builder.Append('['); 
  if (this.Key != null) 
  { 
   builder.Append(this.Key.ToString()); 
  } 
  builder.Append(", "); 
  if (this.Value != null) 
  { 
   builder.Append(this.Value.ToString()); 
  } 
  builder.Append(']'); 
  return builder.ToString(); 
 } 
} 
The class constructor has two parameters: key of type TKey and value of type 
```
TValue. There are defined two properties: one to access the key ( Key) and 
another to access the value ( Value). Note that these properties can only 
access the related members. There is no public functionality to change the 
key or value. This makes the class non -changeable (immutable). It is a good 
idea, because the objects, which will be kept inside the dictionar y 
implementation, will be the same as these we will return as a result of a 
method for taking all the ordered pairs in the dictionary, for instance. 
We have redefined the ToString() method in order to be able to easily print 
key-value pairs on the standard console output or in a text file. 
Following is an example of a generic dictionary interface, which defines the 
most common operations of the data structure "dictionary": 
IDictionary.cs 
/// <summary>Interface that defines basic methods needed for a 
/// "dictionary" class which maps keys to values</summary> 
/// <typeparam name="K">Key type</typeparam> 
/// <typeparam name="V">Value type</typeparam> 
public interface IDictionary<K, V> : 
 IEnumerable<KeyValuePair<K, V>> 
```csharp
{ 
 ///<summary>Finds the value mapped to the given key</summary> 
 /// <param name="key">the key to be searched</param> 
 /// <returns>value for the specified key if it presents, 
 /// or null if there is no value with such key</returns> 
 V Get(K key); 
 

 /// <summary>Assigns the specified value to the specified key 
 /// in the dictionary. If the key already exists, its value is 
 /// replaced with the new value and the old value is returned 
 /// </summary> 
 /// <param name="key">Key for the new value</param> 
 /// <param name="value">Value to be mapped to that key</param> 
 /// <returns>the old (replaced) value for the specified key 
 /// or null if the key does not exist</returns> 
 V Set(K key, V value); 
 
 /// <summary>Gets or sets the value of the entry in the 
 /// dictionary identified by the key specified</summary> 
 /// <remarks>A new entry will be created if the value is set 
 /// for a key not currently in the dictionary</remarks> 
 /// <param name="key">the key to identify the entry</param> 
 /// <returns>the value of the entry in the dictionary 
 /// identified by the provided key</returns> 
 V this[K key] { get; set; } 
 
 /// <summary>Removes an element in the dictionary identified 
 /// by a specified key</summary> 
 /// <param name="key">the key identifying the element to be 
 /// removed</param> 
 /// <returns>whether the element was removed or not</returns> 
 bool Remove(K key); 
 
 /// <summary>Returns the number of entries in the dictionary 
 /// </summary> 
 int Count { get; } 
 
 /// <summary>Removes all the elements from the dictionary 
 /// </summary> 
 void Clear(); 
} 
In the above defined  interface as well as in the previous class, we use 
generics (template types) , by which we define the parameters for the keys 
```
(K) and values ( V). Such implementation allows us to use various data types 
for keys and values inside our dictionary. As we already know, the only 
requirement is to have proper definitions for Equals() and GetHashCode() 
methods inside the data type used for the keys. 
Our interface IDictionary<K, V> looks much like the .NET standard interface 
System.Collections.Generic.IDictionary<K, V>, but it is simplified and 
describes only the most important operations of the "dictionary" data 
structure. It inherits the system interface  IEnumerable<DictionaryEntry<K, 

V>>, as doing so, the dictionary can be easily traversed by a simple foreach 
loop. 
Following is an example of a dictionary implementation that uses chaining 
to handle collisions. 
HashDictionary.cs 
/// <summary> 
/// Implementation of <see cref="IDictionary"/> interface 
/// using a hash table. Collisions are resolved by chaining. 
/// </summary> 
/// <typeparam name="K">Type of the keys. Keys are required 
/// to correctly implement Equals() and GetHashCode() 
/// </typeparam> 
/// <typeparam name="V">Type of the values</typeparam> 
```csharp
public class HashDictionary<K, V> : IDictionary<K, V>, 
```
 IEnumerable<KeyValuePair<K, V>> 
```csharp
{ 
 private const int DEFAULT_CAPACITY = 16; 
 private const float DEFAULT_LOAD_FACTOR = 0.75f; 
 private List<KeyValuePair<K, V>>[] table; 
 private float loadFactor; 
 private int threshold; 
 private int size; 
 private int initialCapacity; 
 
 /// <summary>Creates an empty hash table with the 
 /// default capacity and load factor</summary> 
 public HashDictionary() 
```
  : this(DEFAULT_CAPACITY, DEFAULT_LOAD_FACTOR) 
 { } 
 
 /// <summary>Creates an empty hash table with given 
 /// capacity and load factor</summary> 
```csharp
 public HashDictionary(int capacity, float loadFactor) 
 { 
  this.initialCapacity = capacity; 
  this.table = new List<KeyValuePair<K, V>>[capacity]; 
  this.loadFactor = loadFactor; 
  this.threshold = (int)(capacity * this.loadFactor); 
 } 
 
 /// <summary>Finds the chain of elements corresponding 
 /// internally to given key (by its hash code)</summary> 
 /// <param name="createIfMissing">creates an empty list 

 /// of elements if the chain still does not exist</param> 
 /// <returns>a list of elements in the chain or null</returns> 
 private List<KeyValuePair<K, V>> FindChain( 
  K key, bool createIfMissing) 
 { 
  int index = key.GetHashCode(); 
  index = index & 0x7FFFFFFF; // clear the negative bit 
  index = index % this.table.Length; 
  if (this.table[index] == null && createIfMissing) 
  { 
   this.table[index] = new List<KeyValuePair<K, V>>(); 
  } 
  return this.table[index] as List<KeyValuePair<K, V>>; 
 } 
 
 /// <summary>Finds the value assigned to given key 
 /// (works extremely fast)</summary> 
 /// <returns>the value found or null when not found</returns> 
 public V Get(K key) 
 { 
  List<KeyValuePair<K, V>> chain = this.FindChain(key, false); 
  if (chain != null) 
  { 
   foreach (KeyValuePair<K, V> entry in chain) 
   { 
    if (entry.Key.Equals(key)) 
    { 
     return entry.Value; 
    } 
   } 
  } 
 
  return default(V); 
 } 
 
 /// <summary>Assigns a value to certain key. If the key 
 /// exists, its value is replaced. If the key does not 
 /// exist, it is first created. Works very fast</summary> 
 /// <returns>the old (replaced) value or null</returns> 
 public V Set(K key, V value) 
 { 
  if (this.size >= this.threshold) 
  { 
   this.Expand(); 

  } 
 
  List<KeyValuePair<K, V>> chain = this.FindChain(key, true); 
  for (int i = 0; i < chain.Count; i++) 
  { 
   KeyValuePair<K, V> entry = chain[i]; 
   if (entry.Key.Equals(key)) 
   { 
    // Key found -> replace its value with the new value 
    KeyValuePair<K, V> newEntry = 
     new KeyValuePair<K, V>(key, value); 
    chain[i] = newEntry; 
    return entry.Value; 
   } 
  } 
  chain.Add(new KeyValuePair<K, V>(key, value)); 
  this.size++; 
 
  return default(V); 
 } 
 
 /// <summary>Gets / sets the value by given key. Get returns 
 /// null when the key is not found. Set replaces the existing 
 /// value or creates a new key-value pair if the key does not 
 /// exist. Works very fast</summary> 
 public V this[K key] 
 { 
  get 
  { 
   return this.Get(key); 
  } 
  set 
  { 
   this.Set(key, value); 
  } 
 } 
 
 /// <summary>Removes a key-value pair specified 
 /// by certain key from the hash table.</summary> 
 /// <returns>true if the pair was found removed 
 /// or false if the key was not found</returns> 
 public bool Remove(K key) 
 { 
  List<KeyValuePair<K, V>> chain = this.FindChain(key, false); 

 
  if (chain != null) 
  { 
   for (int i = 0; i < chain.Count; i++) 
   { 
    KeyValuePair<K, V> entry = chain[i]; 
    if (entry.Key.Equals(key)) 
    { 
     // Key found -> remove it 
     chain.RemoveAt(i); 
     this.size--; 
     return true; 
    } 
   } 
  } 
  return false; 
 } 
 
 /// <summary>Returns the number of key-value pairs 
 /// in the hash table (its size)</summary> 
 public int Count 
 { 
  get 
  { 
   return this.size; 
  } 
 } 
 
 /// <summary>Clears all ements of the hash table</summary> 
 public void Clear() 
 { 
  this.table =  
   new List<KeyValuePair<K, V>>[this.initialCapacity]; 
  this.size = 0; 
 } 
 
 /// <summary>Expands the underlying hash-table. Creates 2 
 /// times bigger table and transfers the old elements 
 /// into it. This is a slow (linear) operation</summary> 
 private void Expand() 
 { 
  int newCapacity = 2 * this.table.Length; 
  List<KeyValuePair<K, V>>[] oldTable = this.table; 
  this.table = new List<KeyValuePair<K, V>>[newCapacity]; 

  this.threshold = (int)(newCapacity * this.loadFactor); 
  foreach (List<KeyValuePair<K, V>> oldChain in oldTable) 
  { 
   if (oldChain != null) 
   { 
    foreach (KeyValuePair<K, V> keyValuePair in oldChain) 
    { 
     List<KeyValuePair<K, V>> chain = 
      FindChain(keyValuePair.Key, true); 
     chain.Add(keyValuePair); 
    } 
   } 
  } 
 } 
 
 /// <summary>Implements the IEnumerable<KeyValuePair<K, V>> 
 /// to allow iterating over the key-value pairs in the hash 
 /// table in foreach-loops</summary> 
```
 IEnumerator<KeyValuePair<K, V>> 
  IEnumerable<KeyValuePair<K, V>>.GetEnumerator() 
```csharp
 { 
  foreach (List<KeyValuePair<K, V>> chain in this.table) 
  { 
   if (chain != null) 
   { 
    foreach (KeyValuePair<K, V> entry in chain) 
    { 
     yield return entry; 
    } 
   } 
  } 
 } 
 
 /// <summary>Implements IEnumerable (non-generic) 
 /// as part of IEnumerable<KeyValuePair<K, V>></summary> 
```
 IEnumerator IEnumerable.GetEnumerator() 
```csharp
 { 
  return ((IEnumerable<KeyValuePair<K, V>>)this). 
   GetEnumerator(); 
 } 
} 
```
We will pay attention to the most important points in this code. Let’s begin 
with the constructor. The public parameterless constructor inside itself it 

invokes another constructor, by passing some predefined values for capacity 
and load factor, which are used when the hash table is created. 
Next thing, we pay attention to, is the actual implementation of the hash 
table with chaining. At th e instantiation of the hash -table, inside the 
constructor we initialize an array of lists, which will contain any of our objects 
of type KeyValuePair<K,V>. We have created a private method FindChain(), 
for internal usage only, which calculates the hash -code of the key by 
calling GetHashCode() method and taking the modulus of the returned hash-
value to the length of the table ( capacity). Additionally the most -left bit is 
cleared to ensure the index is always a positive number. In that way the 
index of the current key in the internal table is calculated . The list of all the 
elements with the same hash -code is hold inside the internal table  for this 
index. If the list is empty , it may have null as a value. Otherwise, at the 
specific index position there is a list of the elements for the specified key. 
A special parameter is passed to the FindChain() method. This parameter 
indicates whether to create an empty list, if for the specific key there is no list 
of elements. It gives a kind of convenience for the meth ods of adding 
elements and resizing the hash-table. 
The next thing, we pay attention to, is the Expand() method, which resizes 
the current internal table when the maximal allowed filling is reached. For this 
purpose we create a new table (array), with size  twice as the current. Then 
we calculate a new value for the maximal allowed filling (the field threshold). 
Next coming is the most important part. We have extended the table and in 
this way we changed the value of this.table.Length. If we search for an 
element, which we have added already, the FindChain(K key) method will 
not return the correct chain at all, in which to search for it. That is why, we 
need to transfer all the elements of the old table, by not just copying the 
chains, but adding again all th e KeyValuePair<K,V> objects into the newly 
created internal table of chains. 
In order to implement the ability for iteration over the hash-table elements in 
foreach-loops, we have implemented the IEnumerable<KeyValuePair<K, 
V>> interface, which has GetEnumerator() method, returning an iterator 
(IEnumerator) of the elements of the hash -table. We simply iterate over the 
elements in the internal table and return them one at a time using the yield 
return C# keyword (it’s is a complex concept explained in details in MSDN). 
Now let’s give an example of how we can use our implementation of 
hash-table and its iterator . We want to test whether the hash table copes 
correctly with coll isions and with expanding, so we intentionally change the 
initial capacity of 3 and load factor of 0.9 when creating the hash table to 
ensure it will resize soon  after few elements are put inside it . We first put 
an element, then read it, then overwrite it s value, then read it again, then 
add a new element that causes a collision, then read it, then read the first 
element, then add an element causing the hash table to expand its internal 
array, etc. The code is given below and it is highly recommended to tr ace it 

through the Visual Studio debugger and check at each step how the internal 
state of the hash table changes: 
```csharp
class PlayWithHashDictionary 
{ 
 static void Main() 
 { 
  HashDictionary<Point3D, int> dict = 
   new HashDictionary<Point3D, int>(3, 0.9f); 
 
  dict[new Point3D(1, 2, 3)] = 1; // Put a key-value pair 
  Console.WriteLine(dict[new Point3D(1, 2, 3)]); // Get value 
 
  // Overwrite the previous value for the same key 
  dict[new Point3D(1, 2, 3)] += 1; 
  Console.WriteLine(dict[new Point3D(1, 2, 3)]); 
 
  // Now this point will cause a collision with the 
  // previous one and the elements will be chained 
  dict[new Point3D(3, 2, 2)] = 42; 
 
  Console.WriteLine(dict[new Point3D(3, 2, 2)]); 
 
  // Test if the chaining works as expected, i.e. 
  // elements with equal hash-codes are not overwritten 
  Console.WriteLine(dict[new Point3D(1, 2, 3)]); 
 
  // Creation of another entry in the internal table 
  // This will cause the internal table to expand 
  dict[new Point3D(4, 5, 6)] = 1111; 
  Console.WriteLine(dict[new Point3D(4, 5, 6)]); 
 
  // Delete an existing by its key 
  dict.Remove(new Point3D(3, 2, 2)); 
 
  // Iterate through the dictionary entries and print them 
  foreach (KeyValuePair<Point3D, int> entry in dict) 
  { 
   Console.WriteLine( 
    "Key: " + entry.Key + "; Value: " + entry.Value); 
  } 
 } 
} 
```
As we could expect, the result of the program execution is the following: 

1 
2 
42 
2 
1111 
Key: (1, 2, 3); Value: 2 
Key: (4, 5, 6); Value: 1111

## Open Addressing Methods for Collision Resolution

Now let’s look over the methods for collision resolution , alternative to 
chaining in a list. In general, the idea is, in case of collision we try to put the 
new pair in a table position, which is free. These methods differentiate from 
each other in the way they choose where to lo ok for a free position for the 
new pair. Moreover, the new pair must be easily located at its new place. 
Main drawback of this group of methods, compared to chaining in a list, is 
that they are inefficient at high rates of the load factor (close to 1).

### Linear Probing

This is one of easiest methods for implementation. Linear probing , in 
general, can be presented with the following sample code: 
int newPosition = (oldPosition + i) % capacity; 
Here capacity is the internal table capacity, oldPostion is the position 
where collision occurs and i is a number for the next probing. If the new 
position is free, then we place the new pair there. Otherwise we try again 
(probing), incrementing i. Probing can be either forward or backwards. 
Backward probing is  when instead of adding, we are subtracting i from the 
position we have collision for. 
The advantage of this method is the relatively quick way to find of a new 
position. Unfortunately, if there was a collision at a certain place, there is an 
extremely high probability collision to occur again at the same place. So this, 
in practice, leads to a high inefficiency. 
 
Using linear probing as a method for collision resolution in 
hash tables is inefficient and has to be avoided.

### Quadratic Probing

Quadratic probing is a classic method for collision resolution. The main 
difference between quadratic probing and linear probing is that it uses a 
quadratic function of i (the number of the next probing) to find new position. 
Possible quadratic probing function is shown below: 


int newPosition = (oldPosition + c1*i + c2*i*i) % capacity; 
The given example uses two constants: c1 and c2, such that c2 must not be 
0, otherwise we are going back to linear probing. 
By choosing c1 and c2 we define the position w e are going to probe, 
compared to the starting position. For instance, if c1 and c2 are equal to 1, 
we are going to probe consequently oldPosition, oldPosition + 2 , 
oldPosition + 6, … For a hash-table with capacity of the kind 2n, the best is 
to choose c1 and c2 equal to 0.5. 
Quadratic probing is more efficient than linear the linear probing.

### Double Hashing

As the name implies,  the double hashing method uses two different hash 
functions. The main concept is that, the second hash function is used for the 
elements that fall into a collision . This method is better than the linear and 
quadratic probing, because all the next probing depends of the value of the 
key and not of the table position inside the hash -table. It makes sense, 
because the position of a given  key depends on the current capacity of the 
hash-table.

### Cuckoo Hashing

Cuckoo hashing is a relatively new method for collision resolution, using an 
open addressing. It was firstly presented by R. Pagh and F. Rodler in 2001. Its 
name comes from the behavior, observed with some kinds of cuckoos. The 
mother cuckoos push out the eggs and/or the nests out of other birds, in 
order to put their own eggs there and the other birds mistakenly care for the 
cuckoos' eggs in that way. (Also for the nests, after the incubation) 
The main idea of this method is the use of two hash-functions instead of 
one. In this way, we have not one, but two positions to place the 
element inside the hash -table. If one of the positions is free, the n we just 
put the element there. If both are taken, then we put the new element in one 
of them and it "kicks out" the element, which was already there. In turn, the 
"kicked" element is going to his alternative position and "kicks" another 
element out, if n ecessary. The new "kicked out" is repeating the procedure, 
and in that way until reaching a free position or we fall into a loop. In the last 
case, the whole hash table is built again with greater size and new hash -
functions. 
On the figure bellow it is sho wn an example scheme of a hash -table using 
cuckoo hashing. Every position, containing an element, has a link to the 
alternative position for the key inside. Now, let’s play out different situations 
of adding an element. 
If, at least one of the two hash fun ctions result is a free cell, there is no 
problem. We put the element in one of them. Let both hash functions result is 
a taken cell and we randomly have been choosing one of them. 

 
Let’s assume that this is the cell, containing element A. The new element  
"kicks out" A from his place, A in turn goes to its alternative position and 
"kicks out" B from his place. The alternative position  of B is free, so the 
adding is successfully completed. 
Let’s assume, that the cell, the new element is trying to " kick out " an 
element, is the cell containing H. Then we have a loop, formed by H and W. In 
this case, a rebuild must be done using greater size, and new hash-functions. 
In its simplest version this method has a constant access to its elements, 
even in the worst case, but this is valid with the constraint that the load factor 
is less than 0.5. 
The use of three different hash -functions instead of two could result in an 
efficient upper limit of the load factor above 0.9. 
Some researches show, the cuckoo hashing and its modifications could 
be much more efficient than the widely spread today chaining in a list  
and open addressing methods. Nevertheless, this method is still not well 
adopted in the industry  and not used internally in .NET Framework . The main 
stopper is the need of two hash func tions, which means that the class 
System.Object should introduce two GetHashCode() methods.