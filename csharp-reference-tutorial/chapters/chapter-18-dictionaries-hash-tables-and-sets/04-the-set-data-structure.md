# The "Set" Data Structure

In this section we will look over the abstract data structure "set"  and two 
of its typical implementations. We will explain the ir advantages and disadvan-
tages and which of them should be preferred for different situations.

## The Abstract Data Structure "Set"

Sets are collections of unique elements  (without any repeating elements 
inside). In the .NET context, it means, for every set object, calling its  
Equals() method and passing another object from the set as an argument, 
will always result in false. Note that two different objects in .NET may be 
equal when compared by certain field and thus in the da ta structure "set" only 
one of them could be put. 
Some sets allow their elements to be null, while others do not allow. 
Besides not allowing the repetition of objects, another important thing, that 
distinguishes sets from lists and arrays, is that the set element has no 


index. The elements of the set cannot be accessed by any key, as it is with 
dictionaries. The elements themselves are the keys. 
The only way to access an object from a set is by having available either the 
object itself or another object, wh ich is equal to it. That is why, in practice we 
access all the elements of a given set at once, while iterating, by using the 
foreach loop construct.

## Set Implementations in .NET Framework

In .NET (version 4.0 and above) there is an interface ISet<T> representing 
the ADT "set" and it has two standard implementation classes: 
- HashSet<T> – hash-table based implementation of set. 
- SortedSet<T> – red-black tree based implementation of set. 
Let’s review both of them and see their strong and weak sides. 
The main operations, defined by the  ISet<T> interface (abstract data 
structure set), are the following: 
- bool Add(element)  – adding the element to the set and returning 
false if the element is already present inside the set, otherwise 
returning true. 
- bool Contains(e lement) – checks if the set already contains the 
element passed as an argument. If yes, returns true as a result, 
otherwise returns false. 
- bool Remove(element) – removes the element from the set. Returns 
Boolean if the element has been present inside the set. 
- void Clear() – removes all the elements from the set. 
- void IntersectWith(Set other) – inside the current set remain only 
the elements of the intersection of both sets – the result is a set, 
containing the elements, which are present in both sets at the  same 
time – the set, calling the method and the other, passed as parameter. 
- void UnionWith(Set other)  – inside the current set remain only the 
elements of the sets union – the result is a set, containing the elements 
of either one or the other, or both sets. 
- bool IsSubsetOf(Set other) – checks if the current set is  a subset of 
the other set. Returns true, if yes and false, if no. 
- bool IsSupersetOf(Set other)  – checks if the other set is a subset 
of the current one. Returns true, if yes and false, if no. 
- int Count – a property, which returns the current number of elements 
inside the set.

## Implementation with Hash-Table – HashSet<T>

As we already mentioned, the hash -table implementation of set in .NET is the 
HashSet<T> class. This class, like Dictionary<K, V>, has constructors, by 
which we might pass a list of elements, as well as an IEqualityComparer 
implementation, mentioned earlier. They have the same semantics, because 
here we use a hash-table again. 
Here is an example, which demonstrates the use of sets  and the already 
described, operations: union and intersection: 
```csharp
using System; 
using System.Collections.Generic; 
 
class StudentListSetsExample 
{ 
 static void Main() 
 { 
  HashSet<string> aspNetStudents = new();
  aspNetStudents.Add("S. Jobs"); 
  aspNetStudents.Add("B. Gates"); 
  aspNetStudents.Add("M. Dell"); 
 
  HashSet<string> silverlightStudents = 
   new HashSet<string>(); 
  silverlightStudents.Add("M. Zuckerberg"); 
  silverlightStudents.Add("M. Dell"); 
 
  HashSet<string> allStudents = new();
  allStudents.UnionWith(aspNetStudents); 
  allStudents.UnionWith(silverlightStudents); 
 
  HashSet<string> intersectStudents = 
   new HashSet<string>(aspNetStudents); 
  intersectStudents.IntersectWith(silverlightStudents); 
 
  Console.WriteLine("ASP.NET students: " + 
   string.Join(", ", aspNetStudents)); 
  Console.WriteLine("Silverlight students: " + 
   string.Join(", ", silverlightStudents)); 
  Console.WriteLine("All students: " + 
   string.Join(", ", allStudents)); 
  Console.WriteLine( 
```
   "Students in both ASP.NET and Silverlight: " + 
   string.Join(", ", intersectStudents)); 
 } 

} 
And the output from the above code is: 
ASP.NET students: S. Jobs, B. Gates, M. Dell 
Silverlight students: M. Zuckerberg, M. Dell 
All students: S. Jobs, B. Gates, M. Dell, M. Zuckerberg 
Students in both ASP.NET and Silverlight: M. Dell 
Pay attention that " M. Dell" is present in both sets, but inside the union it is 
present only once. That is, because, as we already explained, one element 
might be present at most once in a given set.

## Implementation with Red-Black Tree – SortedSet<T>

The standard .NET class SortedSet<T> is a set, implemented by a balanced 
search tree (red-black tree). Because of this, its elements are internally kept 
in an increasing order. For that reason we can only add elements, which are 
comparable. We remind that in .NET it typically means the objects are 
instances of a class, implementing IComparable<T>. We would demonstrate 
the use of the SortedSet<T> class by the following example: 
```csharp
using System; 
using System.Collections.Generic; 
 
class SortedSetsExample 
{ 
 static void Main() 
 { 
  SortedSet<string> bandsBradLikes = 
   new SortedSet<string>(new[] { 
```
    "Manowar", "Blind Guardian", "Dio", "Kiss", 
    "Dream Theater", "Megadeth", "Judas Priest", 
    "Kreator", "Iron Maiden", "Accept" 
   }); 
 
  SortedSet<string> bandsAngelinaLikes = 
```csharp
   new SortedSet<string>(new[] { 
```
    "Iron Maiden", "Dio", "Accept", "Manowar", "Slayer", 
    "Megadeth", "Running Wild", "Grave Gigger", "Metallica" 
   }); 
 
```csharp
  Console.Write("Brad Pitt likes these bands: "); 
  Console.WriteLine(string.Join(", ", bandsBradLikes)); 
 

  Console.Write("Angelina Jolie likes these bands: "); 
  Console.WriteLine(string.Join(", ", bandsAngelinaLikes)); 
 
  SortedSet<string> intersectBands = 
   new SortedSet<string>(bandsBradLikes); 
  intersectBands.IntersectWith(bandsAngelinaLikes); 
 
  Console.WriteLine(string.Format( 
   "Does Brad Pitt like Angelina Jolie? {0}", 
   intersectBands.Count >= 5 ? "Yes!" : "No!")); 
 
  Console.Write( 
   "Because Brad Pitt and Angelina Jolie both like: "); 
  Console.WriteLine(string.Join(", ", intersectBands)); 
 
  SortedSet<string> unionBands = 
   new SortedSet<string>(bandsBradLikes); 
  unionBands.UnionWith(bandsAngelinaLikes); 
 
  Console.Write( 
   "All bands that Brad Pitt or Angelina Jolie like: "); 
  Console.WriteLine(string.Join(", ", unionBands)); 
 } 
} 
```
And the output of the program execution is: 
Brad Pitt likes these bands: Accept, Blind Guardian, Dio, Dream 
Theater, Iron Maiden, Judas Priest, Kiss, Kreator, Manowar, 
Megadeth 
Angelina Jolie likes these bands: Accept, Dio, Grave Gigger, 
Iron Maiden, Manowar, Megadeth, Metallica, Running Wild, Slayer 
Does Brad Pitt like Angelina Jolie? Yes! 
Because Brad Pitt and Angelina Jolie both like: Accept, Dio, 
Iron Maiden, Manowar, Megadeth 
All bands that Brad Pitt or Angelina Jolie like: Accept, Blind 
Guardian, Dio, Dream Theater, Grave Gigger, Iron Maiden, Judas 
Priest, Kiss, Kreator, Manowar, Megadeth, Metallica, Running 
Wild, Slayer 
As we may note, the elements in all set are always ordered, in comparison 
with HashSet<T>.