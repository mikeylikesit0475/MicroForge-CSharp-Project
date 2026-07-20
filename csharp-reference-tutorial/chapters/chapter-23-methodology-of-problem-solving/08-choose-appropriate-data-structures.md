# Choose Appropriate Data Structures!

If we already have a correct and working idea for the solution of the problem, 
the next thing to do is to write the program code. We have missed something, 
right? What have we missed? Have we done everything necessary to be able 
to write fast, easy and trouble-free implementation of our solution? 
The thing that we have missed is the manner in which our idea (which we 
have checked on a sheet of paper) is going to be implemented as a computer 
program. The implementation is not always a simple task and sometimes 
it requires additional ideas. This is the next major step: to think of our ideas 
in terms of the computer programming. This means to think for specific data 
structures and not for abstract ones like "card"  and "deck". We should choose 
the right data structures, which are going to help us build a correct solution.  
 
Before you even start with the implementation of your idea, 
you should choose the proper data structures. It may turn 
out that your current idea is not as good as it seems. The 
solution could be inefficient or difficult to implement. It is 
better to figure  this out before you write any programming 
code! 
In our case we have spoken of swapping one card from the deck with another, 
but in terms of programming this means to swap two elements from 
specific data structure (i.e. array, list or something else). We have reached 
the moment where we have to choose one data structure and show you how 
it is done.

## What Kind of Data Structure Should We Use?

The first question that comes to our mind is: What kind of data structure 
should we use? We may have all kinds of different ideas for data structures, 
but not all of them can do the work. Let ’s reflect for a while, shall we? We 


have a collection of cards and the way in which the cards are ordered matters. 
That’s why we need a dat a structure that can hold a collection of elements 
and keep their order.

### Can We Use an Array?

The first thing we can think of is using the structure "array" . The array 
structure is the simplest data structure, which  can hold a collection of 
elements. The array also keeps the order of the elements (first, second, third 
and so on) and we can reach each element by index. The array has a fixed 
number of elements and we cannot change its size during the execution of the 
program. 
Is the array the correct data str ucture for us? To answer this question we 
have to know what kind of operations we are going to apply on the deck, 
represented as an array, and whether they are feasible and efficient. 
What kind of operations are we going to apply in order to implement our 
algorithm? Let’s enumerate them: 
- Choosing a random card. Since we can access every element from the 
array by index we can easily pick a random position k between t he 
interval [1…N-1]. 
- Swapping the first card with the k -positioned one  (single swap). 
After choosing the random card, we should swap it with the first one. 
Again this operation seems easy enough. We can do the swap with three 
simple steps and one temporary variable. 
- More operations  that we might use: initialization of the deck; 
traversing the deck ; printing the deck. All these operations seem trivial 
when applied on array. 
It seems that one simple data structure like the array can represent a deck of 
cards quite well.

### Can We Use Another Data Structure?

It is normal to ask ourselves whether an array  is the best data structure for 
our problem. It seems that every operation that we use in our algorithm can 
be applied efficiently to the array. 
But still, let ’s try and think of an even better data structure for the deck of 
cards than the array. What other options do we have? 
- Linked list – we do not have an indexer and it will be difficult for us to 
access element at a random position. 
- Array with a non -defined size ( List<T>) – this structure seems to 
have all the benefits of the arrays and we can apply every operation to it 
as well. If we use List<T>, we increase our comfort – we can easily 
remove and add elements, which may help us to initialize the deck 
faster and do some other helpful operations. 

- Stack / queue – the deck of cards does not have a behavior of FIFO or 
LIFO, so these structures are not appropriate for our algorithm. 
- Sets (TreeSet<T> / HashSet<T>) – with the use of sets we lose the 
original order of the elements  which is a major obstacle. The use of sets 
is inappropriate. 
- Hash table – the structure card deck is not from the type key -value, so 
the structure hash table cannot store the deck efficiently. Also it does 
not allow us to keep the original order of the elements. 
Generally speaking, we have just covered the basic data structures, which 
can hold a collection of elements. We have reached the conclusion that either 
array or List<T> will be suitable for the job. List<T> is more flexible than the 
ordinary array, so we decide to use List<T> to represent our deck of cards. 
 
The choice of data structure begins with the consideration of 
all key operations that we are going to perform on it. Next 
we analyze all suitable structures and choose the one that 
will be the most efficient and easiest to use. And sometimes 
we should make a compro mise between efficiency and the 
simplicity.

### How to Represent the Other Data Objects?

We have already decided how to represent our deck of cards  and now we 
should do the same with the other objects that we are going to use in our 
algorithm. If we think ab out it, it seems that beside the two objects a " card" 
and "deck", which we use in our algorithm, we do not use other data objects. 
The next question that arises is how to represent a single card ? We can 
represent it as a string, number or class, which has two fields – face and suit. 
There are, of course, other variants, which have their advantages and 
disadvantages. 
Before we even start considering which of these representations of one card is 
"the best", we should go back to the requirements  of the task. I t suggests 
that we are given a deck of cards (as an array or list) and our task is to 
shuffle it. How a card is represented is not of importance in the task. So it 
does not matter what we shuffle, we could shuffle cards, chess figures, boxes 
of tomatoes or  other objects. We have an ordered collection of elements 
and we need to randomly shuffle it . The fact that we shuffle cards is not 
significant for our task, that ’s why we do not need to waste time to choose 
the best way to represent one card. Let ’s use the first thing that come to our 
mind, i.e. we will define a class Card with 2 fields – Face and Suit. Even if we 
use a number between 1 and 52 to represent one card, it still does not 
change anything. We shall not discuss this any further.

## Sorting Numbers: Choosing a Data Structures

Let’s go back to the sorting numbers problem and choose an appropriate 
data structures for it too. We choose to use the simplest algorithm that we 
could think of: to pick the smallest number until we can, print it and after that  
delete it. This solution can be easily sketched on a piece of paper and checked 
for errors. 
Again, in order to answer this question we need to figure out what kind of 
operations we are going to use in our algorithm. The operations are as 
follows: 
- Searching for the smallest number in the structure. 
- Removing of the previously found smallest number. 
Obviously, the use of an array is not reasonable, because we need the 
operation "remove". The use of List<T> seems better, because both 
operations can be simply and easily implemented. Data structures like stack 
or queue have a little use for us, because we do not have a LIFO or FIFO 
behavior. There is not much sense to use a hash table, because the "search 
by v alue" operation is not fast, despite the fact that the removal of an 
element should be very efficient. 
Let’s talk about the two sets – HashSet<T> and TreeSet<T>. The two sets 
have one major problem. They cannot contain elements with an equal value. 
Despite that let ’s see what they can do. The HashSet<T> is not of any 
interest, because like the hash tables it does not support efficient way to find 
the element with the smallest value. The data structure TreeSet<T>, 
however, looks very promising. Let’s take a look, shall we? 
The TreeSet<T> class is a balanced search tree by design, so it supports 
the operation "finding the smallest element". That ’s interesting, isn’t it? Now 
we have a new solution for the task, we put all the input elements in a 
TreeSet<T> and then we get the smallest from the set until it remains empty. 
Easy, simple and very efficient. The two operations, which we want,  are 
internally implemented (searching for the smallest number and deleting it).  
While we skim through the documentatio n, we figure out something very 
interesting: the TreeSet<T> stores its elements ordered by value. And this 
is the solution of our problem, right? Therefore if we keep all the input 
elements in a TreeSet<T> and then traverse the ordered set (with the help of 
the built -in enumeration), we will have all the elements ord ered by value. 
Problem solved! 
We are now very happy, we found one very nice way to solve our task, but 
soon we discover one major problem: TreeSet<T> does not store two 
elements with the same v alue. I.e. if we add the number 5 several times, 
at the end there will be only one entry with a value 5. Eventually we will lose 
some of the input elements irreversibly. 

Naturally we want this problem fixed. If there was a way to store how many 
times one n umber occurs in a set that would solve our problem. Then we 
think of the SortedDictionary<K,T>. This class can store ordered keys, 
which have a value. We can store the number of occurrences of a key in its 
corresponding value. We can traverse all the eleme nts and then store the 
number of occurrences in the SortedDictionary<K,T>. Although it seems our 
problem is solved, it is not going to be implemented as elegant and simple as 
with List<T> or TreeSet<T>. 
If we read the documentation of the SortedDictionary<K,T> carefully, we 
will find that this class internally uses a red–black tree and some day we can 
implement that this type of sorting is very famous and it is called a Binary 
Tree Sorting (http://en.wikipedia.org/wiki/Binary_tree_sort). 
With this little demonstration we showed you how when you put some 
thoughts into the selection of the best data structures , you can come up 
with some new solutions for the problem. We start with an algorithm, whic h 
leads us to a new, better one. This is normal to happen during the process of 
consideration of our algorithm and not after we have written 300 lines of 
code, which we will then have to be redone. This is another proof it is better 
to firstly think of the  best data structures and then to start writing the 
programming code.