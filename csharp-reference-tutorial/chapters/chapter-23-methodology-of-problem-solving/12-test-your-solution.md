# Test Your Solution!

Does the following sound familiar to you: "I am ready with the first task  so I 
have to start the next one."? Everybody has thought of this on an exam. But 
in programming to be ready with a task means: 
1. I have understood well the description of the task. 
2. I have come up with an algorithm to solve the problem. 
3. I have tested my algorithm  on a piece of paper and I am sure that is 
correct. 
4. I have thought up for the data structures  we need and for the 
complexity of my algorithm. 
5. I have written a program, which implements my algorithm correctly. 
6. I have tested my program with suitable examples so that I can be 
sure that everything works flawlessly, even in unusual situations. 
Inexperienced software developers often forget about the last step . They 
think that testing is not their job, which is their biggest mistake.  It is like to 
think that Microsoft is not supposed to test Windows and let it crash every 
minute. 
 
Testing is an important part of the programmer ’s duties. 
Writing code without testing is like typing on the keyboard 
without looking at the screen – you think that the text, you 
have written, is correct, but most likely it is full of bugs. 
Experienced programmers know that untested code is not finished . In 
most software companies it is completely unacceptable not to test your work. 
In the software industry is widely spread the idea of unit testing  – 
automatic testing of every unit of code  (methods, classes and modules). 
Unit testing means, that for every program, we create another one, which to 
test our work. In some companies firstly they think up the testing scenario, 
build the tests and only then start working on the program. The things you 
should know about unit testing are quite many, but you will get more familiar 
with them as you get deeper in th e "software engineering". For now we are 
going to focus on the manual testing every programmer must do. Unit testing 
frameworks and test automation can be used to simplify the process.

## How to Test?

A program works correctly if it can handle every kind of input data. Testing is 
a process, which aims to find any type of bugs . It cannot detect whether a 
program works flawlessly, but it can help you to find most of the bugs, which 
cause incorrect results and other types of errors. 
Sadly you can ’t predict all cases and test them. Therefore you must think up 
examples, which cover most of the situations, which could happen. In this 


way you could with minimum efforts (i.e. with minimum count of simple tests) 
to check all common cases  of usage of the program. If no bugs are found 
after testing, this doesn ’t mean that the program works 100% correctly, but 
in this way we reduce the chance of the program to crash in a later phase. 
 
Testing can only find the exist ence of bugs. It can ’t prove 
that a program works flawlessly! Programs, which are 
carefully tested, have fewer bugs than untested or not 
carefully tested programs. 
It is good to start testing with a typical case  for our program. Often this is 
the same exa mple we have tested on a piece of paper and which our 
algorithm can handle correctly. 
Normally, after the code is written we only need to fix some minor bugs so 
that our program can pass the test correctly. After that we have to test our 
program with more difficult and bigger examples  to see how our program 
behaves in more complicated situations. We now have to test with borderline 
cases and test for performance. Depending on the complexity of the current 
task, we do from 1-2 to dozens of tests to cover the main cases of usage. 
With more complicated software, i.e. Microsoft Word, the number of tests can 
be thousands, even hundreds of thousands . If a program ’s functionality is 
not carefully tested you can ’t say that it works correctly. Testing during 
software development is as important as writing the code . In big software 
companies for every programmer there is a tester. For example in Microsoft 
for every programmer, who writes the code (software developer), there are 
two people hired to test the code  (software quality assurance engineers). 
Although these people do not  write the main software, they write testing 
software and therefore we call them software engineers.

## Testing with Good Examples of the Common Cases

As we already mentioned, it is good to start testing with a good example of a 
common situation. This is a test, which is enough simple to be written 
down on a piece of paper and accurate enough to cover the usual usage  
of the program excluding special cases. The steps are as follows: 
1. Think up a test, which is a good example of a common situation. 
2. Test this example on a piece of paper. 
3. Expect the program to work correctly for that test. 
4. Be sure that the example works correctly after the program is written 
and the errors in the development process are fixed. 
Sadly, many programmers end their testing at this moment . Some 
inexperienced programmers do something even worse: think up a stupid test 
case (which is a special case for the current program), don ’t write it down, 
write some code and, after the program passes the example, they continue. 
Don’t do like this!  This is like repairing a car and once you are finished, 


driving it downhill, thinking that the car is repaired  (but the car has no power 
to drive uphill).

## What Else to Test For?

Testing the case, drawn on a piece of paper, is only the first step. Next you 
need to do some additional tests to be sure that your program works 
correctly: 
- A hard common-case test. The goal is to check whether your program 
can handle a bigger and harder to compute example. For our task that 
kind of a test is to shuffle a deck of 52 cards. 
- Borderline tests . They check whether your program can handle an 
unusual case, which could happen. In our case this could be shuffling a 
deck, which contains only one card. 
- Performance tests . These tests put our program in extreme 
conditions. Usually these tests consist of large data, which needs to be 
inputted and processed. 
Let’s take a look at the groups of tests above one be one.

## A Hard Common-Case Test

We have already tested our program for one case , which we have written 
down on a piece of paper. Our program works correctly. This case covers a 
typical scenario of usage of our program. What more do we have to test for? 
It is possible our program to be incorrect, but accidentally to work for our 
test. 
How to think up a harder test? It depends on the task. It must consist of 
larger amounts of data and we must be able to see whether the output of our 
program is correct. 
In our case we have to test with a full deck – 52 cards. We can easily produce 
such a test with two nested loops. After the execution of our program we 
could also easily check whether the answer is correct – the cards must be 
randomly shuffled. It is necessary to check whether the cards are again 
randomly shuffled after two consecutive executions of this test. The code for 
this test looks like this: 
```csharp
static void TestShuffle52Cards() 
{ 
 List<Card> cards = new();
 string[] allFaces = new string[] { "2", "3", "4", "5", 
  "6", "7", "8", "9", "10", "J", "Q", "K", "A" }; 
 Suit[] allSuits = new Suit[] { Suit.CLUB, Suit.DIAMOND, 
  Suit.HEART, Suit.SPADE }; 
 foreach (string face in allFaces) 

 { 
  foreach (Suit suit in allSuits) 
  { 
   Card card = new Card() { Face = face, Suit = suit }; 
   cards.Add(card); 
  } 
 } 
 ShuffleCards(cards); 
 PrintCards(cards); 
} 
```
If we execute it, the returned result is: 
(4 DIAMOND)(2 DIAMOND)(6 HEART)(2 SPADE)(A SPADE)(7 SPADE)(3 
DIAMOND)(3 SPADE)(4 SPADE)(4 HEART)(6 CLUB)(K HEART)(5 CLUB)(5 
DIAMOND)(5 HEART)(A HEART)(9 CLUB)(10 CLUB)(A CLUB)(6 SPADE)(7 
CLUB)(7 DIAMOND)(3 CLUB)(9 HEART)(8 CLUB)(3 HEART)(9 SPADE)(4 
CLUB)(8 HEART)(9 DIAMOND)(5 SPADE)(8 DIAMOND)(J HEART)(10 
DIAMOND)(10 HEART)(10 SPADE)(Q HEART)(2 CLUB)(J CLUB)(J SPADE)(Q 
CLUB)(7 HEART)(2 HEART)(Q SPADE)(K CLUB)(J DIAMOND)(6 DIAMOND)(K 
SPADE)(8 SPADE)(A DIAMOND)(Q DIAMOND)(K DIAMOND) 
If we take a careful look we notice that most of the cards are at the same 
place. After the first 4 cards, the half of the cards didn’t change their 
place after the shuffle: 2♦ and 2♠. 
It is never late to find a bug in our program  and the only way we can do 
that is to test it with many tests, which cover practical situations. It was 
useful to test with a real deck of 52 cards, wasn ’t it? We came upon a serious 
bug, which cannot be left unfixed. 
How to fix the problem?  The first idea, we come up with, is to do more 
single-swaps (obviously N times are not enough). Another idea is at the Nth 
single-swap to swap the Nth card from the de ck with a random one 
instead of changing it with the first one . In this way we can guarantee 
that every card is going to be swapped. The second idea is better. Let ’s 
implement it. We have the following changes in the code: 
```csharp
static void PerformSingleSwap(List<Card> cards, int index) 
{ 
 int randomIndex = rand.Next(1, cards.Count); 
 Card firstCard = cards[index]; 
 Card randomCard = cards[randomIndex]; 
 cards[index] = randomCard; 
 cards[randomIndex] = firstCard; 
} 

 
static void ShuffleCards(List<Card> cards) 
{ 
 for (int i = 0; i < cards.Count; i++) 
 { 
  PerformSingleSwap(cards, i); 
 } 
} 
We execute the program and get a better shuffle of the deck of 52 cards: 
```
(9 HEART)(5 CLUB)(3 CLUB)(7 SPADE)(6 CLUB)(5 SPADE)(6 HEART)(4 
CLUB)(10 CLUB)(3 SPADE)(K DIAMOND)(10 HEART)(8 CLUB)(A CLUB)(J 
DIAMOND)(K SPADE)(9 SPADE)(7 CLUB)(10 DIAMOND)(9 DIAMOND)(8 
HEART)(6 DIAMOND)(8 SPADE)(5 DIAMOND)(4 HEART)(10 SPADE)(J 
CLUB)(Q SPADE)(9 CLUB)(J HEART)(K CLUB)(2 HEART)(7 HEART)(A 
HEART)(3 DIAMOND)(K HEART)(A SPADE)(8 DIAMOND)(4 SPADE)(3 
HEART)(5 HEART)(Q HEART)(4 DIAMOND)(2 SPADE)(A DIAMOND)(2 
DIAMOND)(J SPADE)(7 DIAMOND)(Q DIAMOND)(2 CLUB)(6 SPADE)(Q 
CLUB) 
It looks like the cards are finally randomly ordered  at every execution of 
our program. For now we don ’t see any bugs (i.e. re peating or missing cards, 
or cards, which are at the same place). The program is fast and it doesn ’t fall 
asleep. It looks like we have done well. 
Let’s take a look at the other sample task: sorting numbers. What would be 
a serious common -case test? Easy f or us would be to generate 100 or even 
1000 random numbers and sort them. It is easy to check whether the final 
answer is correct: the numbers must be sorted by size. Another good test 
would be to enter the numbers from 1000 to 1 in descending order . The 
output must also consist of 1000 numbers, but sorted in ascending order. We 
could say that the hardest test is to check whether our program can handle 
many numbers – then we could say that our program probably works 
correctly. 
Let’s take a look at some othe r types of tests, which we must consider when 
we solve programming problems.

## Borderline Cases

The step, we often miss in problem solving, is testing for borderline cases. 
Borderline situations occur when the input data is on the border between a 
normal sit uation and a situation, which most likely will not  happen. In this 
situation the program often crashes, because very large or very small 
amounts of data are not considered, although they are possible to be entered. 
This is clearly a programmer ’s fault, bec ause he has not thought that this 
could happen. 

How to think up borderline cases? We analyze all of the data, which is being 
entered, to our program and think up such extreme values , which are 
possible to be entered. These values could be extremely small, extremely 
large or just strange. If it is said that the upper limit is 52 cards, the values 
around 52 are also borderlines and they could cause errors.

### Borderline Case: Shuffling One Card

In our shuffling-cards problem a borderline case is to shuffle only one card. 
This case is absolutely valid (although it is quite unusual), but our program 
may not handle it correctly. Let ’s take a look what could happen if we enter a 
deck of one card. We could write the following little test: 
```csharp
static void TestShuffleOneCard() 
{ 
 List<Card> cards = new();
 cards.Add(new Card() { Face = "A", Suit = Suit.CLUB }); 
 CardsShuffle.ShuffleCards(cards); 
 CardsShuffle.PrintCards(cards); 
} 
We execute our program and get an unexpected result: 
```
Unhandled Exception: System.ArgumentOutOfRangeException: Index 
was out of range. Must be non-negative and less than the size of 
the collection. Parameter name: index 
 at 
System.ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArg
ument argument, ExceptionResource resource) 
 at System.ThrowHelper.ThrowArgumentOutOfRangeException() 
 at System.Collections.Generic.List`1.get_Item(Int32 index) 
 at CardsShuffle.PerformSingleSwap(List`1 cards, Int32 index) 
in D:\Projects\Cards\CardsShuffle.cs:line 61 
… 
The error occurred  because the arguments passed to the method, which 
generates random numbers, were invalid. Our program can handle a normal 
deck of cards, but it can’t handle a deck of one card . We found an easy fix  
for this bug, which we could miss lightly, if we skipped checking the borderline 
cases. After we have established the nature of the problem, we can fix it: 
```csharp
static void ShuffleCards(List<Card> cards) 
{ 
 if (cards.Count > 1) 
 { 
  for (int i = 0; i < cards.Count; i++) 
  { 

   PerformSingleSwap(cards, i); 
  } 
 } 
} 
```
We test again and we are sure that the bug is fixed.

### Borderline Case: Shuffling Two Cards

After there is a problem with one card, there may be also a problem with 
two cards. It sounds logical, does it? It doesn ’t bother us to test it. We run 
the program se veral times with only two cards to be shuffled and expect to 
get a different order on every run. Here is an example source code that will 
do the trick: 
```csharp
static void TestShuffleTwoCards() 
{ 
 List<Card> cards = new();
 cards.Add(new Card() { Face = "A", Suit = Suit.CLUB }); 
 cards.Add(new Card() { Face = "3", Suit = Suit.DIAMOND }); 
 CardsShuffle.ShuffleCards(cards); 
 CardsShuffle.PrintCards(cards); 
} 
```
We run the program several times and the output is always the same: 
(3 DIAMOND)(A CLUB) 
It seems something is still not right . If we take a look at the source code 
or run the debugger we notice that two swaps are made: this first card with 
the second and immediately after that the second with the first one. The 
result is one and the sa me. How to solve this problem? Instantly we can think 
up a few solutions: 
- We perform N+K single swaps, where K is a random number between 0 
and 1. 
- We assume that the random position can be also a zero position. 
- We consider a deck of 2 cards as a special ca se and write a separate 
method to handle this case. 
This second solution is the simplest to be implemented. Let ’s try it. Here is 
the source code: 
```csharp
static void PerformSingleSwap(List<Card> cards, int index) 
{ 
 int randomIndex = rand.Next(0, cards.Count); 
 Card firstCard = cards[index]; 

 Card randomCard = cards[randomIndex]; 
 cards[index] = randomCard; 
 cards[randomIndex] = firstCard; 
} 
```
We test again and it looks like the program works correctly : sometimes 
the cards are shuffled and sometimes not. 
If there is a problem with 2 cards, there may be also a problem with 3 cards, 
right? If we do a test with 3 cards we see that it works flawlessly. After a few 
runs we get every possible order of the cards (all permutations of the cards). 
This time we did not find any bugs and we do not need to edit the code.

### Borderline Case: Shuffling Zero Cards

What other tests do we need to do? Are there other unusual, borderline 
cases? Let’s think. What is going to happen if we sh uffle an empty list of 
cards? This really  is a bit strange, but a program must work correctly or it 
must properly alert for an error. Let’s look at result of our program. The result 
is an empty list. Is this correct? Yes, if we try to shuffle zero cards the answer 
should be again a deck of zero cards. Everything looks fine. 
 
With the input of invalid data the program must not return 
an incorrect result. It must return a correct result or alert 
that the input data is wrong. 
What do you think about the rule above? It is logical, right? Imagine a 
program, which displays images. What is going happen if we input a n image, 
which is actually an empty file? This is an unusual situation, which should not 
happen, but it may happen. If with the input of an empty file the program 
crashes or throws an unproc essed exception, this would be annoying for the 
user. Normally, instead of an empty file, a special image should be displayed 
or an alert should pop-up, which contains that the image is invalid. 
Think about how many borderline cases are there in Windows . W hat 
happens if we try to print an empty file? Does Windows crash or the “the blue 
screen of death” appears? What happens if in the Windows’ calculator we try 
divide by zero? What happens if we try to copy an empty file (with size of 0 
bytes) using Windows Explorer? What happens if try to save a file without a 
name in Notepad (with an empty string as a name)? You see that there are 
many borderline cases. Our duty as programmers is to fix the bugs before 
the product is released on the market. 
Let’s go back to  the card-shuffling problem. Thinking about other borderline 
and unusual cases, we consider a case with -1 cards to be shuffled? Because 
we cannot initialize an array of -1 elements, we consider it as incorrect. 
We don ’t have an upper bound, so we don ’t have any other special cases 
(similar to the case with 1 card). We stop searching for borderline cases 


having to do with the count of the cards. It looks like we considered all of the 
cases. 
Now we only need to check whether there are such values of the inpu t, 
which can cause errors – an invalid suit, a card with negative value (i.e. -1 
club). Actually our algorithm does not care about what is being shuffled (cards 
or books), so this should not be a problem. If we have any doubts, we could 
do a test and be su re, that even with an invalid deck of cards, the answer is 
correct. 
We look around for other borderline case in the input data, but we could not 
think up any. We only need to do a performance test, right? Actually we 
forgot to test our program again after the corrections.

## Regression Testing

While fixing bugs, we often create new bugs without to notice. For example if 
we fix a bug for two cards with editing the method, responsible for the 
shuffling of the cards, a new bug with 3 cards may occur. On every edi t of 
code, which concerns other cases, we must again do the same test, we have 
done earlier. That ’s why it is a good idea to save the tests as methods  
(naming them with a prefix Test) and be able to run them again. Re -testing 
with the tests already passed in the past is called “ regression testing”. We 
may also use the unit testing framework that comes with Visual Studio (see 
Unit Testing section in the “High-Quality Code” chapter) to simplify re-running 
the tests after making changes in the code. 
The idea of repeating the tests  is implicit in the concept of unit testing. As 
we mentioned earlier, this concept is for more advanced programmers. 
After the changes we need to try again shuffling 0 cards, 1 card, 2 cards, 3 
cards and 52 cards. 
 
When you have found and fixed in your code a bug, which 
concerns a specific test, make sure that no other bugs have 
been introduced after the code has been changed. It is a 
good idea to keep the tests saved for repeated execution.

## Performance Tests

It is normal to have some performance requirements about a module or the 
program at all. No one likes his computer to be slow, right? That ’s why you 
have to write software, which works slow only if there is a good reason. 
How to check the speed (performance) of our program? We must firstly 
consider whether we have any performance requirements. Then what are 
they? If we don’t have any, we should use some conventional criteria.

### Shuffling Cards – Performance Tests

Let’s take a look at our deck -shuffling program. What are the performance 
criteria? Do we have such? We don ’t have any requirements like: "the 
program must compute the answer in less than a second with an upper bound 
of 500 cards on a modern computer". After we don ’t have such explicit 
criteria, we should set our own. 
Because the data is a set of cards, we consider testing with a normal deck of 
52 cards. We already ran such a test and the answer was output immediately. 
It looks like our program works fast in normal conditions. 
It is normal to test also with a deck of many more cards (i.e. 52 ,000). In a 
very particular situation someone might like to shuffle a deck of 52 ,000 
cards and we could not allow our program to crash at this later phase. We 
can easily create such a test by adding 1000 times our 52 cards. Here is some 
example code: 
```csharp
static void TestShuffle52000Cards() 
{ 
 List<Card> cards = new();
 string[] allFaces = new string[] {"2", "3", "4", "5", 
  "6", "7", "8", "9", "10", "J", "Q", "K", "A"}; 
 Suit[] allSuits = new Suit[] { Suit.CLUB, Suit.DIAMOND, 
  Suit.HEART, Suit.SPADE}; 
 for (int i = 0; i < 1000; i++) 
 { 
  foreach (string face in allFaces) 
  { 
   foreach (Suit suit in allSuits) 
   { 
    Card card = new Card() { Face = face, Suit = suit }; 
    cards.Add(card); 
   } 
  } 
 } 
 ShuffleCards(cards); 
 PrintCards(cards); 
} 
We start the program and notice that it works without stopping for 5-10 
```
seconds. Of course with slower computers it is going to take more time. 
What is happening? The count of the operations with a deck of 52 ,000 cards is 
almost the same as with a deck of 52 cards for example. Why is the program 
so slow? Advanced programmers will immediately guess that the delay is 
because we output big amounts of data and printing to the console is a 
slow operation . If we comment the line, where the printing is done, as a 

comment, we will see how fast our program is, even with a deck of 52 ,000 
cards. Here’s how we can count the time: 
```csharp
static void TestShuffle52000Cards() 
{ 
```
 … 
 DateTime oldTime = DateTime.Now; 
 ShuffleCards(cards); 
 DateTime newTime = DateTime.Now; 
```csharp
 Console.WriteLine("Execution time: {0}", newTime - oldTime); 
 //PrintCards(cards); 
} 
We can check how long it takes for the method, responsible for the shuffling 
```
of the cards, to be executed: 
Execution time: 00:00:00.0156250 
One millisecond is absolutely acceptable. We don ’t have any performance 
problems.

### Sorting Numbers – Performance Tests

Let’s take a look at another one of our prob lems: sorting an array of 
numbers. Here performance might be a problem. We have thought up a 
simple solution: our algorithm finds the lowest number in the array and swaps 
it with the number at position 0. Then it finds the next lowest number and 
sets it at position 1. The algorithm continues until we reach the last position. 
We will not  comment the correctness of the algorithm. It is well known as 
"Selection sort" (http://en.wikipedia.org/wiki/Selection_sort). 
Let’s suppose that we have passed all the steps for solving a programming 
problem, except the last one. Therefore we will try to sort 10,000 random 
numbers: 
Sort10000Numbers.cs 
```csharp
using System; 
 
public class Sort10000Numbers 
{ 
 static void Main() 
 { 
  int[] numbers = new int[10000]; 
  Random rnd = new();
  for (int i = 0; i < numbers.Length; i++) 
  { 

   numbers[i] = rnd.Next(2 * numbers.Length); 
  } 
  SortNumbers(numbers); 
  PrintNumbers(numbers); 
 } 
 
 static void SortNumbers(int[] numbers) 
 { 
  for (int i = 0; i < numbers.Length - 1; i++) 
  { 
   int minIndex = i; 
   for (int j = i + 1; j < numbers.Length; j++) 
   { 
    if (numbers[j] < numbers[minIndex]) 
    { 
     minIndex = j; 
    } 
   } 
 
   int oldNumber = numbers[i]; 
   numbers[i] = numbers[minIndex]; 
   numbers[minIndex] = oldNumber; 
  } 
 } 
 
 static void PrintNumbers(int[] numbers) 
 { 
  Console.Write("["); 
  for (int i = 0; i < numbers.Length; i++) 
  { 
   Console.Write(numbers[i]); 
   if (i < numbers.Length - 1) 
   { 
    Console.Write(", "); 
   } 
  } 
  Console.WriteLine("]"); 
 } 
} 
```
We run our program and it looks like it finishes in less than a second  on a 
typical modern computer. The shortened result would be something like that: 
[0, 14, 19, 20, 20, 22, …, 19990, 19993, 19995, 19996] 

We do another test, this time with an array of 300,000 random numbers, 
and notice that our program falls asleep  or it is just too slow. This is a 
serious performance problem. 
Before we start fixing it , we must ask ourselves: is this a real situation. If we 
sort students’ marks, the elements will not be more than a couple of dozens. 
However if we sort the stocks’ currencies of a large software company for its 
being on the stock market, we have a lot of numbers to be sorted. This is 
because the price of the stocks can change every second. In about ten years 
the price can change hundreds of thousands times of times. In this case we 
should look for a better sorting algorithm. 
We can easily find information about sorting algorithms in many websites and 
books. In our case, it is most appropriate to use the non-comparative integer 
sorting algorithm "radix sort" (en.wikipedia.org/wiki/Radix_sort) which runs 
in linear time, but this discussion is beyond the objectives of this book. 
Let’s recall the efficiency rule: 
 
You must always make a compromise between the time, 
spent on writing the program, and the performance, which 
we want to achieve. Otherwise you might lose your time for 
solving a problem, that doesn ’t exist, or come up with a 
solution, which is inefficient. 
We must consider that for some problems there aren ’t any fast algorithms. 
For example there aren’t any fast solutions for finding all prime dividers of 
an integer (take a look at http://en.wikipedia.org/wiki/Integer_factorization). 
In some situations the input data is too small and we don ’t need a fast 
algorithm to process it. For example : sorting the students’ marks. It can be 
solved with every sorting algorithm, because the count of the students is 
small.