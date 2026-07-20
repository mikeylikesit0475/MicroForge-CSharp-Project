# When to Use a Particular Data Structure?

Let’s skim through all the structures in the table above and explain in what 
situations we should use them  as well as how their complexities are 
evaluated.

## Array (T[])

The arrays are collections of fixed number of elements from a given type 
(for example numbers)  where the elements preserved their order . Each 

element can be accessed through its index. The arrays are memory areas, 
which have a predefined size. 
Adding a new element in an array is a slow operation. To do this we have 
to allocate a memory with the same size plus one and copy all the data from 
the original array to the new one. 
Searching in an array takes time  because we have to compare every 
element to the searched value. It takes N/2 comparisons in the average case. 
Removing an element  from an array is a slow operation . We have to 
allocate a memory with the same size minus one and copy all the old 
elements except the removed one. 
Accessing by index is direct, and thus, a fast operation. 
The arrays should be used only when we have to p rocess a fixed number of 
elements to which we need a quick access by index. For example, if we 
have to sort some numbers, we can keep them in an array and then apply 
some of the well -known sorting algorithms. If we h ave to change the 
elements' count, the array is not the correct data structure we should use. 
 
Use arrays when you have to process a fixed number of 
elements to which you need an access through index.

## Singly / Doubly Linked List (LinkedList<T>)

Singly and doubly linked lists hold collection of elements , which preserve 
their order . Their representation in the memory is dynamic, pointer -based. 
They are linked sequences of element. 
Adding is a fast operation but it is a bit slower than adding to a List<T> 
because every time when we add an element to a linked list we allocate a new 
memory area. The memory allocation works at speed, which cannot be easily 
predicted. 
Searching in a linked list is a slow operation because we have to traverse 
through all of its elements. 
Accessing an element by index  is a slow operation because there is no 
indexing in singly and doubly linked lists. You have to go through all the 
elements from the start one by one instead. 
Removing an element at a specified index is a slow operation  because 
reaching the element through its index is a slow operation. Removing an 
element with a specified value is a slow operation too, because it involves 
searching. 
Linked list can quickly add and remov e elements  (with a constant 
complexity) at its two ends  (head and tail). Hence, it is very handy for an 
implementation of stacks, queues and similar data structures. 


Linked lists are rarely used  in practice because the dynamic arrays 
(List<T>) can do almost exact same operations LinkedList does, plus for 
the most of them it works faster and more comfortable. 
When you need a linked list, use List<T> instead of LinkedList<T>, because 
it doesn ’t work slower and it gives you better speed and flexibility. Use 
LinkedList when you have to add and remove elements at b oth ends of 
the data structure. 
 
When you need to add and remove elements at both ends of 
the list, use LinkedList<T>. Otherwise use List<T>.

## Dynamic Array (List<T>)

Dynamic array (List<T>) is one of the most popular data structures  used 
in programming . It does not have fixed size like arrays, and allows direct 
access through index, unlike linked lists (LinkedList<T>). The dynamic array 
is also known as "array list", "resizable array" and "dynamic array". 
List<T> holds its elements in an array, which has a bigger size than the 
count of the stored elements. Usually when we add an element , there is an 
empty cell in the list ’s inner array. Therefore this operation takes a constant 
time. Occasionally the array has been filled and it has to expand. This takes 
linear time, but it rarely happens. If we have a large amount of additions, the 
average-case complexity of adding an element to List<T> will a constant – 
O(1). If we sum the steps needed for adding 100,000 elements (for both 
cases – "fast add" and "add with expand") and divide by 100,000, we will 
obtain a constant which will be nearly the same like for adding 1,000,000 
elements. 
This statistically-averaged complexity calculated for large enoug h amount of 
operations is called amortized complexity . Amortized linear complexity 
means that if we add 10 ,000 elements consecutively, the overall count of 
steps will be of the order of 10 ,000. In most cases  add it will execute in a 
constant time, while very rarely adding will execute in linear time. 
Searching in List<T> is a slow operation  because you have to traverse 
through all the elements. 
Removing by index  or value executes in a linear time. It is a slow 
operation because we have to move all the elements after the deleted one 
with one position to the left. 
The indexed access in List<T> is instant, in a  constant time , since the 
elements are internally stored in an array. 
Practically List<T> combines the best of arrays and lists, for which it is a 
preferred data structure in many situations. For example if we have to 
process a text file and to extract from it all words  (with duplicates) , which 


match a regular expression, the most suitable data structure in which w e can 
accumulate them is List<T>, because we need a list, the length of which is 
unknown in advance and can grow dynamically. 
The dynamic array ( List<T>) is appropriate, when we have to add elements 
frequently as well as keeping their order of addition and access them through 
index. If we often  we have to search or delete elements, List<T> is not the 
right data structure. 
 
Use List<T>, when you have to add elements quickly and 
access them through index.

## Stack

Stack is a linear data structure in which ther e are 3 operations defined: 
adding an element at the top of the stack ( push), removing an element from 
the top of the stack ( pop) and inspect the element from the top without 
removing it (peek). All these operations are very fast – it takes a constant 
time to execute them. The stack does not support the operations search and 
access through index. 
The stack is a data structure, which has a LIFO behavior (last in, first out). 
It is used when we have to model such a behavior – for example, if we have 
to keep the path to the current position in a recursive search. 
 
Use a stack when you have to implement the behavior "last 
in, first out" (LIFO).

## Queue

Queue is a linear data structure in which there are two operations defined: 
adding an element to the tail (enqueue) and extract the front -positioned 
element from the head  (dequeue). These two operations take a constant 
time to execute, because the queue is usually implemented with a linked list. 
We remind that the linked list can quickly add and remove elements fr om its 
both ends. 
The queue’s behavior is FIFO (first in, first out) . The operations searching 
and accessing through index are not supported. Queue can naturally model a 
list of waiting people, tasks or other objects, which have to be processed in 
the same order as they were added (enqueued). 
As an example of using a queue we can point out the implementation of the 
BFS (breadth -first search) algorithm , in which we start from an initial 
element and all its neighbors are added to a queue. After that they are 
processed in the order they were added and their neighbors are added to the 
queue too. This operation is repeated until we reach the element we are 
looking for or we process all elements. 


 
Use a queue when you have to implement the behavior "first 
in, first out" (FIFO).

## Dictionary, Implemented with a Hash-Table (Dictionary<K, T>)

Dictionary, Implemented with a Hash-Table 
(Dictionary<K, T>) 
The data structure "dictionary" suggests storing key -value pairs  and 
provides a quick search by key . The implementation with a hash table (the 
```csharp
class Dictionary<K,T> in .NET Fr amework) has a very fast add, search 
```
and remove of elements – constant complexity at the average case. The 
operation access through index is not available, because the elements in the 
hash-table have no order, i.e. an almost random order. 
Dictionary<K,T> keeps internally the elements in an array and puts every 
element at the position calculated by the hash -function. Thus the array is 
partially filled – in some cells there is a value, others are empty. If more than 
one element should be placed in a single  cell, elements are stored in a linked 
list. It is called chaining. This is one of the few ways to resolve the collision 
problem. When the load factor exceeds 75%, the size is doubled and all the 
elements occupy new positions. This operation has a linear c omplexity, but it 
is executed so rarely, that the amortized complexity remains a constant. 
Hash-table has one peculiarity: if we choose a bad hash -function causing 
many collisions, the basic operations can become very inefficient and reach 
linear complexit y. In practice, however, this hardly happens. Hash -table is 
considered to be the fastest data structure, which provides adding and 
searching by key. 
Hash-table in .NET Framework permits each key to be put only once. If we 
add two elements with the same key  consecutively, the last will replace the 
first and we will eventually lose an element. This important feature should be 
considered. 
From time to time one key will have to keep multiple values. This is not 
standardly supported but we can store the values m atching this key in a  
List<T> as a sequence of elements. For example if we need a hash -table 
Dictionary<int, string>, in which to accumulate pairs {integer, string} 
with duplicates, we can use Dictionary<int, List<string>>. Some external 
libraries have ready to use data structure called MultiDictionary<K,V>. 
Hash-table is recommended to be used every time we need fast addition 
and fast search by key . For example if we have to count how many times 
each word is encountered in a set of words in a text file, we  can use 
Dictionary<string, int> – the key will be a particular word, the value – 
how many times we have seen it. 
 
Use a hash -table, when you want to add and search by key 
very fast. 


A lot of programmers (mostly beginners) live with the delusion the main  
advantage of using a hash -table is the comfort of searching a value by its 
key. Actually this is wrong. We can implement searching a key with an array, 
a list or even a stack. There is no problem, everyone can build it. We can 
define a class Entry, which holds a key-value pair and after that we will work 
with an array or a list with Entry elements. We can implement the search but 
by any circumstances it will work slowly. This is the big problem with lists and 
arrays – they do not offer a fast searc h. Unlike them the hash -table can 
search and add new elements very fast. 
 
The main advantage of the hash -table over the other data 
structures is a very quick searching and addition. The 
comfort for the developers is a secondary factor.

## Dictionary, Implemented with a Balanced Tree (SortedDictionary<K,T>)

Dictionary, Implemented with a Balanced Tree 
(SortedDictionary<K,T>) 
The implementation of the data structure "dictionary" as a red -black tree 
(the class SortedDictionary<K,T>) is a structure storing key -value pairs 
where keys are ordered  increasingly (sorted). The structure provides a fast 
execution of basic operations (add an element, search by key and remove an 
element). The complexity of these operations is logarithmic – O(log(N)). 
Thus, it will take 10 steps for add / search / remove when the dictionary holds 
1,000 elements and 20 steps in case of 1,000,000 elements. 
Unlike hash -tables, where we can reach linear complexity if we pick a bad 
hash-function, in SortedDictionary<K,T> the count of the steps of the basic 
operations in the average and worst cas e are the same – log2(N). When we 
work with balanced trees, there is no hashing, no collisions and no risk of 
```csharp
using a bad hash-function. 
```
Again, as in the hash -tables, one key can be stored at most once in the 
structure. If we want to associate several values with one key, we should use 
some kind of a list for the values, for example List<T>. 
SortedDictionary<K,T> holds internally its values  in a red-black balanced 
tree ordered by key. This means if we traverse the structure (using its 
iterator or foreach loop in C#) we will get the elements sorted in ascending 
order by key. Sometimes this can be very useful property. 
Use SortedDictionary<K,T> when you need a structure which can add, 
search and remove an element fast and you also need to extract the elements 
sorted in ascending order. In general Dictionary<K,T> works a bit faster 
than SortedDictionary<K,T> and is preferable. 
As an example of u sing a SortedDictionary<K,T>, we can give the following 
task: find all the words in a text file, which occur exactly 10 times, and 
print them alphabetically . This is a task that we can solve as successful 
with Dictionary<K,T> too, but we will have to do an  additional sorting at the 


end. For the solution of this task we can use SortedDictionary<string, 
int> and to traverse through all the words in the text file. For each word we 
will keep in the sorted dictionary how many times we have encountered it. 
After that we can go through all the elements in the dictionary and print those 
words, which have been encountered exactly 10 times. They will be 
alphabetically ordered, since this is the natural internal order of the sorted 
dictionary data structure. 
 
Use SortedDictionary<K,T> when you want fast addition of 
elements and searching by key as well as the elements to be 
sorted by key.

## Set, Implemented with a Hash-Table (HashSet<T>)

The data structure "set" is a collection of elements with no duplicates . 
The basic operations are adding an element to the set, checking if an element 
belongs to the set (searching) and removing an element from the set. The 
operation searching through index is not supported, i.e. we do not have a 
direct access to the elements via orderin g number, because in this structure 
there is not any order. 
Set, implemented with a hash-table (the class HashSet<T>) is a special case 
of a hash-table, in which we have only keys. The values associated  with these 
keys do not matter. 
As in the hash -table, the basic operations in the data structure HashSet<T> 
are implemented with a constant complexity O(1). Another similarity to 
hash-table is if we choose a bad hash -function, we can reach a linear 
complexity executing the basic operatio ns. Fortunately in practice this almost 
never happens. 
As an example of using a HashSet<T>, we can point out the task of finding all 
the different words in a text file. 
 
Use HashSet<T>, when you have to quickly add elements to a 
set and check whether a given element belongs to a set.

## Set, Implemented with a Balanced Tree (SortedSet<T>)

Set, Implemented with a Balanced Tree 
(SortedSet<T>) 
The data structure set, implemented with a red-black tree, is a special case of 
SortedDictionary<K,T> in which keys and values coincide. 
Similar to SortedDictionary<K,T>, the basic operations in SortedSet<T> are 
executed with logarithmic complexity O(log(N)), which is the same in the 
average and worst case. 


As an example of using a SortedSet<T> we can point out the task of finding 
all the different words in a given text file  and printing them alphabetically 
ordered. 
 
Use SortedSet<T>, when you have to quickly add an element 
to a set and check whether given element belongs to the set 
as well as need all the elements sorted in ascending order.