# Comparison between Basic Data Structures

Comparison between Basic Data Structures 
After you have been introduced to the term algorithm complexity, we are now 
ready to make a comparison between the basic data structures , which 
we know from the last few chapters , and to estimate with what complexity 
each of them performs the basic operati ons like addition, searching, 
deletion and access by index  (when applicable) . In that way we could 
easily judge according to the operations  we expect to need, which structure 
would be the most appropriate . The complexities of the basic operations 
on the basic data structures, which we have reviewed in the previous 
chapters, are given in the table below: 
Data structure Addition Search Deletion Access 
by index 
Array (T[]) O(N) O(N) O(N) O(1) 
Linked list (LinkedList<T>) O(1) O(N) O(N) O(N) 
Dynamic array (List<T>) O(1) O(N) O(N) O(1) 
Stack (Stack<T>) O(1) - O(1) - 
Queue (Queue<T>) O(1) - O(1) - 
Dictionary, implemented 
with a hash-table 
(Dictionary<K, T>) 
O(1) O(1) O(1) - 
Dictionary, implemented 
with a balanced search tree 
(SortedDictionary<K, T>) 
O(log(N)) O(log(N)) O(log(N)) - 
Set, implemented with a 
hash-table (HashSet<T>) O(1) O(1) O(1) - 
set, implemented with a 
balanced search tree 
(SortedSet<T>) 
O(log(N)) O(log(N)) O(log(N)) - 
We let the reader to think about how these complexities were estimated.