# LINQ Performance

LINQ Performance 
As a rule using LINQ and extension methods is slower than using direct 
operations over a collection of elements, so beware of using LINQ when 
processing large collections or the performance is critical. 
Let’s compare the speed of adding 50,000,000 elements to a list through 
extension methods and directly with a for-loop: 
List<int> l1 = new List<int>(); 
DateTime startTime = DateTime.Now; 
l1.AddRange(Enumerable.Range(1, 50000000)); 
```csharp
Console.WriteLine("Ext.method:\t{0}", DateTime.Now - startTime); 
 
startTime = DateTime.Now; 
List<int> l2 = new();
for (int i = 0; i < 50000000; i++) l2.Add(i); 
Console.WriteLine("For-loop:\t{0}", DateTime.Now - startTime); 
```
The result might be as follows (depends on the computer’s CPU speed): 
Ext.method:     00:00:01.6430939 
For-loop:       00:00:00.9120522 

LINQ technology and extension methods work through the concept of 
expression trees . Each LINQ query is translated by the compiler to an 
expression tree and is executed when its results are actually accessed (not 
earlier). For example let’s consider the following code: 
List<int> list = new List<int>(); 
list.AddRange(Enumerable.Range(1, 100000)); 
 
DateTime start = DateTime.Now; 
for (int i = 0; i < 10000; i++) 
```csharp
{ 
 var elements = list.Where(e => e > 20000); 
} 
Console.WriteLine("No execution:\t{0}", DateTime.Now - start); 
 
start = DateTime.Now; 
for (int i = 0; i < 10000; i++) 
{ 
 var element = list.Where(e => e > 20000).First(); 
} 
Console.WriteLine("Execution:\t{0}", DateTime.Now - start); 
```
The result might be as follows (depends on the computer’s CPU speed): 
No execution:   00:00:00.0070004 
Execution:      00:00:02.7231558 
This shows that if we call a .Where(…) filter (or where clause in LINQ) it is not 
actually executed until its result is actually needed. The elements get filtered 
on demand, at the time they are really required . In our case this is when we 
invoke First() method. Moreover, if we get the first element of a sequence, 
the rest elements are not processes until needed. Thus if we use change the 
filtering lambda function from “e => e > 20000” to “e => e > 500000”, the 
filtering becomes times slower because more elements are processed until the 
first matching the filtering condition is found: 
No execution:   00:00:00.0060004 
Execution:      00:00:06.3663641 
Standard .NET Framework collection classes like List<T>, HashSet<T> and 
Dictionary<K,V> are optimized to work fast with LINQ. Most operations with 
LINQ work almost as fast as if we run them directly. Let’s check this example: 
HashSet<Guid> set = new HashSet<Guid>(); 
for (int i = 0; i < 50000; i++) 
```csharp
{ 

 set.Add(Guid.NewGuid()); // Add random GUID 
} 
 
Guid keyForSearching = new();
DateTime start = DateTime.Now; 
for (int i = 0; i < 50000; i++) 
{ 
 // Use HashSet.Contains(…) 
 bool found = set.Contains(keyForSearching); 
} 
Console.WriteLine("HashSet: {0}", DateTime.Now - start); 
 
start = DateTime.Now; 
for (int i = 0; i < 50000; i++) 
{ 
 // Use IEnumerable<Guid>.Contains(…) extension method 
 bool found = set.Contains<Guid>(keyForSearching); 
} 
Console.WriteLine("Contains: {0}", DateTime.Now - start); 
 
start = DateTime.Now; 
for (int i = 0; i < 50000; i++) 
{ 
 // Use IEnumerable<Guid>.Where(…) extension method 
 bool found = set.Where(g => g==keyForSearching).Count() > 0; 
} 
Console.WriteLine("Where: {0}", DateTime.Now - start); 
```
The result is as follows (though it depends on the computer’s CPU speed): 
HashSet: 00:00:00.0030002 
Contains: 00:00:00.0040003 
Where: 00:02:49.9717218 
Seems like .NET Framework takes into account the  capability to search in 
constant time O(1) in a HashSet<T>, so searching though the native method 
Contains(…) and though the extension methods  IEnumerable.Contains(…) 
both run in time  O(1). By contrast, the IEnumerable.Where(…) method is 
dramatically slower and runs in linear time O(n). This is expected, because 
the Where(…) method checks certain condition for each element in a collection 
and it is expected to process all elements one by one. By contrast the 
Contains(…) method just searches for single element which is fast operation. 
In case you do not remember about the asym ptotic notation O(1) and O(n), 
please check the chapter “Data Structures and Algorithm Complexity”. 

In the above example we use the system structure Guid. This is a global 
unique identifier often used in computer technologies to identify an object. It 
may look like the following: 8668f585-faf8-4685-8025-6a8d1d2aba0a. If 
you want to generate a global unique (world -wide) identifier, you might 
benefit from the method Guid.NewGuid(), like we do in the code above.