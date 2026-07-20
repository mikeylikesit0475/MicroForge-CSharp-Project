# Dictionary Data Structure

In the last few chapters we got familiar with some classic and very important 
data structures – arrays, lists, trees and graphs. In this chapter we will  get 
familiar with the so called " dictionaries", which are extremely useful and 
widely used in the programming. 
The dictionaries are also known as associative arrays or maps. In this book 
we are going to use the terminology "dictionary". Every element in the 
dictionary has a key and an associated value for this key. Both the key and 
the value represent a pair. The analogy with the real world dictionary comes 
from the fact, that in every dictionary, for every f or word (key), we also have 
a description related to this word (value). 
 
As well as the data (values), that the dictionary holds, there 
is also a key that is used for searching and finding the 
required values. The elements of the dictionary are 
represented by pairs (key, value), where the key is used for 
searching.

## Dictionary Data Structure – Example

We are going to illustrate what exactly the data structure dictionary means 
```csharp
using an everyday, real world example. 


```
When you go to a theatre, opera or a conc ert, there is usually a place where 
you can leave your outdoor clothing. The employee than takes your jacket 
and gives you a number. When the event is over, on your way out, you give 
them back the same number. The employee uses this number to search and 
find your jacket to give back to you. 
Thanks to this example we can see that the idea for using a key (the number 
that the employee gives you) to store a value (your jacket), and later having 
the option to access it, is not so abstract. Actually this is a me thod that is 
often widely used not only in programming, but also in many other practical 
areas. 
When using the ADT dictionary, the key may not just be a number, but any 
other type of object. In the case, when we have a key (number), we could 
implement this type of structure as a regular array. In this scenario the set of 
keys is already known – these are the numbers from 0 to n, where n 
represents the size of the array (when n is within the allowed limits). The idea 
of the dictionaries is to provide us with more flexibility regarding the set of 
the keys. 
When using dictionaries, the set of keys usually is a randomly chosen set of  
values like real numbers or strings. The only restriction is that we can 
distinguish one key from the other. Later we will take a look at some 
additional requirements for the keys that are needed for the different kinds of 
implementations. 
For every key in the dictionary , there is a corresponding value . One key 
can hold only one value. The aggregation of all the pairs (key, value)  
represents the dictionary. 
Here is the first example for using a dictionary in .NET: 
IDictionary<string, double> studentMarks =  
 new dictionary<string, double>(); 
 
studentMarks["Paul"] = 3.00; 
```csharp
Console.WriteLine("Paul 's mark: {0:0.00}", 
 studentMarks["Paul"]); 
```
Later in this chapter , we will find out the result from the execution of the 
example above.

## The Abstract Data Structure “Dictionary” (Associative Array, Map)

The Abstract Data Structure “Dictionary” (Associative 
Array, Map) 
In programming the abstract data structure "dictionary" is represented 
by many aggregated pairs (key, value) along with predefined methods for 
accessing the values by a given key. Alternatively this data stru cture can also 
be called a "map" or "associative array". 

Described below are the required operations, defined by this data structure: 
- void Add(K key, V value)  – adds given key-value pair in the 
dictionary. With most implementations of this class in .NET, when 
adding a key that already exists, an exception is thrown. 
- V Get(K key)  – returns the value by the specified key. If there is no 
pair with this key, the method returns null or throws an exception 
depending on the specific dictionary implementation. 
- bool Remove(key) – removes the value, associated with the specified 
key and returns a Boolean value, indicating if the operation was 
successful. 
Here are some additional methods, which are supported by the ADT. 
- bool Contains(key) – returns true if the dictionary has a pair with 
the selected key 
- int Count – returns the number of elements (key value pairs) in the 
dictionary 
Other operations that are usually supported are: extracting all of the keys, 
values or key value pairs and importing them int o another structure (array, 
list). This way they can easily be traversed using a loop. 
 
For the comfort of .NET developers, the IDictionary<K, V> 
interface holds an indexing property V this[K] { get; set; }, 
which is usually implemented by calling the met hods V 
Get(K), Add(K, V). 
Bear in mind that the access method (accessor) get of the 
property V this[K] of the class  Dictionary<K, V> in .NET 
throws an exception if the given key K does not exist in the 
dictionary. In order to access the value of a certain key, 
without having to worry about exceptions, use the method 
bool TryGetValue(K key, out V value).

### The Interface IDictionary<K, V>

In .NET there is a standard interface IDictionary<K, V> where K defines the 
type of the key, and V type of the value. It defines all of the basic operations 
that the dictionaries should implement. IDictionary<K, V> corresponds to 
the abstract data structure "dictionary" and defines the operati ons, mentioned 
above, but without supplying an actual implementation of them. This interface 
is defined in assembly mscorelib, namespace System.Collections.Generic. 
In .NET interfaces represent specifications of methods for a certain class. 
They define met hods without implementation, which should be implemented 
by the classes that inherit them. How the interfaces and inheritance work we 
will discuss in more detail s in the chapter "Principles of the Object-Oriented 
Programming". For the moment all you need to know is that interfaces define 


which methods and fields should be implemented in the classes that inherit 
the interface. 
In this chapter we will  take a look at the two most popular dictionary 
implementations – with a balanced tree  and a hash-table. It’s extremely 
important for you to know how they differ from one another, and which are 
the main principles related to them. Otherwise you risk using them improperly 
and inefficiently. 
In .NET Framework there are two major implementations of the interface 
IDictionary<K, V> – Dictionary<K, V> and SortedDictionary<K, V>. 
SortedDictionary is an implementation by a balanced (red -black) tree, and 
Dictionary – by a hash-table. 
 
Except for IDictionary<K, V> in .NET there is one more 
interface – IDictionary, along with the classes implementing 
it: Hashtable, ListDictionary and HybridDictionary. They are 
heritage from the first version of .NET. These classes need to 
be used only on special occasions. Much more  preferable is 
the use of Dictionary<K, V> or SortedDictionary<K, V>. 
In this and  the next chapter  we will  analyze when the different 
implementations of dictionaries are used.

## Implementation of Dictionary with Red-Black Tree

Because the implementation of a dictionary with a balanced tree  is very 
extensive and complex task, we will not examine it in source code. Instead we 
will analyze the class SortedDictionary<K, V> , that comes with the 
standard .NET library. We strongly recommend the curious readers to look at 
the decompiled code of the SortedDictionary class using some of the 
decompilation tools mentioned in the chapter "Introduction to Programming " 
like JustDecompile. 
As we mentioned in the  previous chapter, a red-black tree is an ordered 
binary balanced search tree, that’s used for searching. This is why one of 
the important requirements for the set of keys used by SortedDictionary<K, 
V> is comparability. This means that, if we have two keys, either one of 
them should be bigger, or they should be equal.  The keys used i n 
SortedDictionary<K, V> should implement IComparable<K>. 
The usage of the binary search tree gives us a great advantage: the keys 
in the dictionary are stored ordered. Thanks to this feature, if we need 
the data ordered by keys, we don ’t need to perform any additional sorting. 
Actually, this is the only advantage of this dictionary implementation 
compared to the hash-table. 
A thing that should be mentioned is that keeping the keys ordered comes with 
its price. Searching for the elements u sing in an ordered balanced tree is 
slower (typically takes log(n) steps) than using a hash-table (typical takes 


fixed number of steps ). Because of this, if there is no requirement for the 
keys to be ordered, it’s better to use Dictionary<K, V>. 
 
Use a balanced tree dictionary only when you need your pairs 
(key, value) to be ordered by key. Bear in mind that the 
balanced tree comes with the complexity of the algorithm 
log(n), for searching, adding and deleting elements. 
Compared to this, the complexity use d in hash -table may 
reach a linear value.

### The Class SortedDictionary<K, V>

The class SortedDictionary<K, V> is a dictionary implementation, which 
uses a red-black tree . This class implements all the standard operations 
defined in the interface IDictionary<K, V>.

### Using SortedDictionary Class – Example

Now we will  solve a practical problem, where using the class 
SortedDictionary is a good idea. Let ’s say we have arbitrary text. Our task 
would be to find all the different words in the text , and the number of 
occurrences of these words. Additionally we should print all the words found 
in alphabetical order. 
For this task using a dictionary is a really good idea . We can use the 
different words in the text for keys, and the value for each key would be the 
number of occurrences for each word in our text. 
The algorithm for counting the words  is the following: we read the text 
word by word. For each word we check if it already exists in the dictionary. If 
the answer is no, we add a new element in the dictionary with a value of 1. If 
the answer is yes – we increase the old value of the element by one, so as to 
count the last occurrence. 
The elements of the ordered dictionary SortedDictionary<string, int> will 
be ordered by their key during the iteration process. This w ay we met the 
additional requirement for the words to be ordered alphabetically. Below is a 
sample implementation of the described algorithm: 
WordCountingWithSortedDictionary.cs 
```csharp
using System; 
using System.Collections.Generic; 
 
class WordCountingWithSortedDictionary 
{ 
 private static readonly string Text = 
```
  "Mary had a little lamb " + 
  "little Lamb, little Lamb, " + 


  "Mary had a Little lamb, " + 
  "whose fleece were white as snow."; 
 
```csharp
 static void Main() 
 { 
  IDictionary<String, int> wordOccurrenceMap = 
   GetWordOccurrenceMap(Text); 
  PrintWordOccurrenceCount(wordOccurrenceMap); 
 } 
 
 private static IDictionary<string, int> GetWordOccurrenceMap( 
  string text) 
 { 
  string[] tokens = 
   text.Split(' ', '.', ',', '-', '?', '!'); 
 
  IDictionary<string, int> words = 
   new SortedDictionary<string, int>(); 
 
  foreach (string word in tokens) 
  { 
   if (!string.IsNullOrEmpty(word.Trim())) 
   { 
    int count; 
    if (!words.TryGetValue(word, out count)) 
    { 
     count = 0; 
    } 
    words[word] = count + 1; 
   } 
  } 
  return words; 
 } 
 
 private static void PrintWordOccurrenceCount( 
  IDictionary<string, int> wordOccurenceMap) 
 { 
  foreach (var wordEntry in wordOccurenceMap) 
  { 
   Console.WriteLine( 
    "Word '{0}' occurs {1} time(s) in the text", 
     wordEntry.Key, wordEntry.Value); 
  } 
 } 

} 
```
The output from executing this code is the following: 
Word 'a' occurs 2 time(s) in the text 
Word 'as' occurs 1 time(s) in the text 
Word 'fleece' occurs 1 time(s) in the text 
Word 'had' occurs 2 time(s) in the text 
Word 'lamb' occurs 2 time(s) in the text 
Word 'Lamb' occurs 2 time(s) in the text 
Word 'little' occurs 3 time(s) in the text 
Word 'Little' occurs 1 time(s) in the text 
Word 'mary' occurs 2 time(s) in the text 
Word 'snow' occurs 1 time(s) in the text 
Word 'was' occurs 1 time(s) in the text 
Word 'white' occurs 1 time(s) in the text 
Word 'whose' occurs 1 time(s) in the text 
Note that we are counting the words "little" and "lamb" starting with both 
lowercase and uppercase characters as different. 
In this example, we demonstrated for the first time how to traverse a 
dictionary using the method PrintWordOccurrenceCount(IDictionary 
<string, int>) . We used a foreach loop. When iterating through the 
elements of dictionaries, we need to take i nto account that the elements of 
this ADT are ordered pairs (key and value), not just single objects. Because 
IDictionary<K, V>  implements the interface IEnumerable<KeyValuePair 
<K, V>>, this means that the foreach loop should iterate through objects of 
type KeyValuePair<K, V> . For simplicity we use the var-syntax in the 
foreach loop.

### IComparable<K> Interface

When using SortedDictionary<K, V>  the keys are required to be  
comparable. In our example we use objects of type string. 
The class string implements the interface IComparable, and the comparison 
between the elements is done lexicographically. What does that mean? By 
default the strings in .NET are case sensitive (the compiler distinguishes 
uppercase from lowercas e letters). Words like "Length" and "length" are 
considered different. This means that words that start with a lowercase letter 
will be before the ones with an uppercase letter. This definition comes from 
the implementation of the method CompareTo(object), through which the 
string class implements the interface IComparable.

### IComparer<T> Interface

What should we do when we are not happy with the default implementation of 
comparison? For example, what should we do when we want uppercase and 
lowercase characters to be treated as equal? 
One option we have is to transform the word into a capital, or non -capital 
string, but sometimes the situation is more complicated than that. This is why 
we will  offer another solution, which works for every class that does not 
implement the IComparable<T> interface, or it does implement it, but we 
want to change its behavior. 
For the comparison of objects with an exclusively defined order in 
SortedDictionary<K, V> in .NET, we will use the interface IComparer<T>. It 
defines a comparison function int Compare(T x, T y) that is an alternative 
to the already defined order. Let’s take a better look at this interface. 
When we create an object of type SortedDictionary<K, V> we can pass to 
its constructor a reference to IComparer<K> so that it can use it for the key 
comparison (key elements should be objects of type K). 
Here is a sample implementation of IComparer<K> that changes the behavior 
when comparing strings, so that they are not distinguished by uppercase and 
lowercase characters: 
```csharp
class CaseInsensitiveComparer : IComparer<string> 
{ 
 public int Compare(string s1, string s2) 
 { 
  return string.Compare(s1, s2, true); 
 } 
} 
Let’s use this interface IComparer<E> when creating the dictionary: 
IDictionary<string, int> words =  
 new SortedDictionary<string, int>( 
  new CaseInsensitiveComparer()); 
```
After changing this in the code, the result from the program execution will be: 
Word 'a' occurs 2 time(s) in the text 
Word 'as' occurs 1 time(s) in the text 
Word 'fleece' occurs 1 time(s) in the text 
Word 'had' occurs 2 time(s) in the text 
Word 'lamb' occurs 4 time(s) in the text 
Word 'little' occurs 4 time(s) in the text 
… 

The first time a word is found, it becomes a key  in the dictionary . This is 
because after calling the words[word] = count + 1  only the value is 
changed, and not the key itself. 
After using IComparer<E> we changed the definition for ordering keys in our 
dictionary. If, for a key, we used a class, defined by us, for example – 
Student, that implements IComparable<E>, we would get the same result if 
we were to alter the method CompareTo(Student). There is also one 
additional requirement, when implementing IComparable<K>: 
 
When two objects are equal ( Equals(object) returns true), 
CompareTo(E) should return 0. 
Meeting this requirement would allow us to use the objects of a custom class 
as keys, just as in the implementation with a balanced tree 
(SortedDictionary<K,V>, constructed without Comparer), as well with a 
hash-table (Dictionary<K,V>).