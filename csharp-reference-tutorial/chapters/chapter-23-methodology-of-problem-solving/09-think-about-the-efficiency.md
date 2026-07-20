# Think about the Efficiency!

Again, it seems we should grab the keyboard and start writing a programming 
code. And again, it is better not to hurry . The thing is that we have not 
thought of something very important: the efficiency an d performance  of 
our algorithm. 
 
You should think of efficiency before writing even a line of a 
programming code. Otherwise, you risk to waste time 
implementing an algorithm, which is inefficient and slow.  
Let’s return to our "card-shuffle" problem. We have a working idea for solving 
the problem (we have invented the algorithm). The idea appears to be correct 
(we have checked the algorithm with examples). We should not have any 
problems implementing our idea (we are going to use List<Card> for the 
deck and class Card for a single card). Everything seems fine, but let ’s think 
about how many cards we are going to shuffle . Is our idea going to work 
fast enough when using the chosen data structures?

## How to Estimate the Performance of Given Algorithm?

How fast is our algorithm? To answer this question we should estimate how 
many operations it performs when shuffling one deck of 52 cards. 
For one deck of 52 cards our algorithm makes 52 "single swap" 
operations, do you agree? How many elementary operations cost one "single 


swap"? 4 operations: the choice of one random card; the placing of the first 
card in a temporary variable; the replacing of the first card with the random 
card; the replacing of the random card with the first card (from the temporary 
variable). How many operations does our algorithm do? They are 
approximately 52 * 4 = 208. 
Are 208 operations too much? Let’s do a loop with 208 iterations. Are they too 
much? Give it a try! We can assure you that one loop with 1 ,000,000 
iterations on a modern computer goes imperceptibly fast, and one with 208 – 
for an insignificant amount of time. Therefore we can easily conclu de that our 
algorithm has a good performance . Our algorithm is extremely fast when 
working with 52 cards. 
Despite the fact that in reality we rarely play cards with more than 1 – 2 
decks, let’s assume that we have 50,000 cards in the deck . Let’s estimate 
the performance of our algorithm with a large number of cards. We have 
50,000 single swap operations and each of them consists of 4 operations, 
which makes about 200 ,000 operations, which are going to be executed for a 
small amount of time as well.

## The Efficiency Is a Matter of Compromise

Finally we can conclude that our algorithm is efficient and will work well even 
with decks with large amount of cards. Here we had luck. Usually the things 
are not so simple and we must make a compromise with the performanc e and 
the efforts, which we put, when we implement our algorithm. For example if 
we sort numbers, we can solve this problem in minutes when we use some of 
the simplest sorting algorithms. We can also do this much more efficiently 
when we use some of the more complex algorithms, but that will waste 
more of our time (in searching and reading books and Internet). 
Is it worth it? We should consider that. If we have to sort 20 numbers, it does 
not matter which algorithm we are going to use. It will always be fast, even 
with the most naive algorithm. If we are going to sort 20 ,000 numbers, the 
algorithm matters, and if we need to sort 20 ,000,000, we should look at the 
task from a completely new angle . The efforts for solving efficiently the 
problem of sorting 2 0,000,000 numbers is far more than the efforts for 
writing a straightforward algorithm to sort 20 numbers. We should answer the 
question: is it worth it? 
 
The efficiency is a matter of compromise – sometimes it 
does not worth to complicate your algorithm and put time 
and effort to make it work faster. But occasionally the 
performance is crucial and we should pay serious attention 
to it.

## Sorting Numbers: Estimating the Performance

It is obvious that the performance depends on whether a particular task 
requires it. And now let ’s return to the sorting numbers problem, because we 
want to show you that the efficiency is directly related to the right 
choice of data structures. 
Let’s go back to the point where we have decided what kind of data structures 
to use for keeping the input data. Which is better: List<T> or 
SortedDictionary<K,T>? Shouldn ’t we use a data structure that we know 
well instead of some complex structure that we have never used? Do you 
know well red -black trees (the internal implementation of t he 
SortedDictionary<K,T>)? With what are they better than List<T>? In fact it 
may turn out that you do not need to answer this question after all. 
If we have to sort 20 numbers , does it matter what data structure are we 
going to use? We can choose the simp lest algorithm  and the first data 
structure that is actually suitable for the job and that ’s it. It does not matter 
how fast is the algorithm and the data structure, because the numbers are 
not so many. 
But if we have to sort 300,000 numbers, then everythi ng is different. We 
should carefully study how exactly the class SortedDictionary<K,T> 
behaves. We should figure out how fast is the "search" operation. How fast 
does this data structure add elements? How fast can you traverse through 
every element of the collection? If we read the documentation of the class we 
will see that the adding of an element takes on average  log2(N) steps, where 
N is the number of the elements in the structure. After few simple  
mathematical calculations (which require additional skills), we can roughly 
estimate that we need about 5 -6 million steps to sort all numbers. For 
300,000 numbers this number is reasonably small. 
Similarly we can prove that the search and delete operation s in List<T> with 
N elements take N steps. Therefore for 300 ,000 elements we will need 
roughly 2 * 300 ,000 * 300,000 steps. In fact this number is an approximate 
guess, because at the beginning we have one number in the list, not 300 ,000 
elements. Neverthe less this estimation is approximately right, maybe a bit 
rough but right. We can see that the number of steps needed in this case is 
extremely large , that is why here the simple algorithm will not work  
properly (the program might "hang"). 
And again we reac h a point where we need to choose between one simple 
and one complex algorithm. One of them can be very easily but slow when 
implemented. The other is more efficient, but very difficult to implement and 
we will probably need an additional reading of docume ntation and thick books 
in order to correctly estimate the performance. Everything is a matter of 
compromise. 
Naturally, at this point we can think of some of the other algorithms that we 
have considered previously. And precisely, to split the array into two parts 
then to sort them separately  (by a recursive call) and then merge the two 

parts into one sorted array. As we consider this algorithm we will find that this 
solution will work efficiently with such structures like the dynamic array 
(List<T>). This sorting algorithm has an average and worst -case perfor-
mance of n*log(n) steps, where n is the count of the elements in the array. 
This algorithm will work efficiently with 300 ,000 numbers. Let ’s not go any 
further, if you want more details about the algor ithm you should read more 
about MergeSort in Wikipedia (http://en.wikipedia.org/wiki/Merge_sort).