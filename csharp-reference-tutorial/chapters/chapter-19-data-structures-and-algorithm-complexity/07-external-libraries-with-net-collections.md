# External Libraries with .NET Collections

It is a well -known fact that the standard data structures in .NET Framework 
System.Collections.Generic have pretty poor functionality. It lacks 
implementations of basic concepts in data structures such as multi -sets, 
priority queues, for which there should be standard classes as well as basic 
system interfaces. 
When we have to use a special data structure, which  is not standardly 
implemented in .NET Framework, we have two options: 
- First option: we implement the data structure ourselves . This gives 
us flexibility, because the implementation will completely meet our 
needs, but it takes a lot of time and it has a gr eat chance of making 
mistakes. For example, if one has to qualitatively implement a balanced 
tree, this may take an experienced software developer several days 
(along with the tests). If the same is implemented by inexperienced 
software developer it will t ake a lot more time and most probably there 
will be errors in the implementation. 
- Second option (generally preferable): find an external library, which 
has a full implementation of the needed functionality. This approach has 
an advantage of saving us time and troubles, because in most cases the 
external libraries of data structures are well -tested. They have been 


used for years by thousands of software developers and this makes 
them mature and reliable.

## Power Collections for .NET

One of the most popular and richest libraries with efficient implementations of 
the fundamental data structures for C# and .NET software developers is the 
open-source project "Wintellect’s Power Collections for .NET " – 
http://powercollections.codeplex.com. It provides free, reliable, efficient, fast 
and handy implementations of the following commonly used data structures, 
which are missing or partly-implemented in .NET framework: 
- Set<T> – set of elements, implemented with a hash-table. It 
efficiently implements the basic operations over sets: adding, deleting 
and searching an element as well as union, intersection, difference 
between sets and many more. By functionality and way of work the 
class looks like the standard class HashSet<T> in .NET Framework. 
- Bag<T> – multi-set of elements  (set with duplicates), implemented 
with a hash-table. It efficiently implements all basic operations over 
multi-sets. 
- OrderedSet<T> – ordered set of elements  (without duplicates), 
implemented with a balanced search tree. It efficiently implements all 
basic operations over sets and when traversing through its elements it 
returns them in ascending order (according to the used comparer). It 
allows a fast extraction of subsets of values in a given interval. 
- OrderedBag<T> – ordered multi-set of elements, implemented with a 
balanced search tree. It efficiently implements all basic operations 
over multi-sets and when going through all its elements it returns them 
in ascending order (according to the used comparer). It allows a quick 
extraction of subsets of values in a given interval. 
- MultiDictionary<K,T> – it is a hash-table allowing key duplicates . 
For every key there is a collection of values stored, not one single value. 
- OrderedDictionary<K,T> – it represents a dictionary, implemented 
with a balanced search tree. It allows a fast search by key and when 
going through its elements it returns them in ascending order. It 
enables us to quickly extract the values from a given key range. By 
functionality and w ay of work the class looks like the standard class  
SortedDictionary<K,T> in .NET Framework. 
- Deque<T> – represents efficient implementation of a queue with two 
ends ( double ended queue ), which practically combines the data 
structures stack and queue. It all ows efficient addition, extraction and 
deletion of elements in both ends. 
- BagList<T> – list of elements, accessed through index , which 
allows a quick insertion and deletion  of an element from a particular 
position. The operations index accessing, adding, i nserting at position 

and removing an element from position have a complexity O(log N). The 
implementtation is with a balanced tree. The structure is a good 
alternative of List<T>, in which the insertion and removal of element at 
a particular position take s linear time because of the need of the 
replacement of linear number of elements to the left or right. 
We let the reader the opportunity to download the library " Power 
Collections for .NET " from its site and to experiment with it . It can be 
very useful when you solve some of the problems from the exercises.

## C5 Collections for .NET

Another very powerful library of data structures and collection classes is “ The 
C5 Generic Collection Library for C# and CLI ” (www.itu.dk/research/c5/). 
It provides standard interfaces and collection classes like lists, sets, bags, 
multi-sets, balanced trees  and hash tables , as well as non-traditional 
data structures  like “hashed linked list”, “wrapped arrays” and “interval 
heaps”. It also describes a set of collection-related algorithms and patterns, 
such as “read -only access”, “random selection”, “removing duplicates”, etc. 
The library comes with solid documentation  (a book of 250 pages). The C5 
collections and the book about them are the ultimate resource for data 
structure developers.