# Write the Code Step by Step!

Have you written 200 -300 lines of code without even compiling or testing it? 
Do not do that! Do not write large lumps of code at one time, instead you 
should write small parts and then test them. 
How to write code step by step? This depends on the given task and the way, 
in which it is decomposed into smaller tasks. For example if the main task 
consists of 3 independent parts, we should write one of them, compile and 
test it with a proper input data until we are sure that it works correctly. After 
that we move to the second part  – write code, compile, test and then 
proceed with the third part  with the same approach and finally integrate 
the parts and test everything as a whole. 
Why to wri te code step by step? Because we reduce the amount of code 
that we have to concentrate on in any given moment . By treating the 
problem in parts, we decrease its complexity. Remember: the large and 
complicated task could always be divided  into several small er and simpler 
subtasks. And it is always easier to solve simple problems. 
When writing large chunks of code, without compiling it, we accumulate a 
great amount of errors , which could easily be avoided by a simple 
compilation. The modern programming enviro nments (like Visual Studio) try 
to recognize the syntactic errors automatically while we are writing the code. 
Use this function and fix the obvious coding errors as early as possible. Early 
troubleshooting takes less time and nerves . However if we delay t he 
troubleshooting, it could cost us a lot of efforts, sometimes even rewriting the 
whole programming code. 
When you write a huge amount of code, which is not tested, and decide to 
test it as a whole with some input data, you usually receive a lot of errors, 
which can be avoided if one just compiles . The larger the code is, more 
difficult it is to be fixed. Thes e problems could be caused by a variety of 
reasons: incorrect use of data structures; wrong algorithm; badly structured 
code; bad condition in the if-statement; wrongly implemented loop; going 
out of bounds of the array and many other problems that could h ave been 
fixed earlier. Do not wait for the last moment. Eliminate the mistakes 
as soon as possible! 
 
Write your program in parts, not at once! Take , write  and 
compile one logically independent part, fix the errors, test it 
and if it works fine, move to the next part.

## Writing Code Step by Step – Example

In order to demonstrate how to write code step by step , we should 
illustrate it with the "card-shuffle" algorithm that we invented previously.

### Step 1 – Defining the Class "Card"

Our task is to shuffle the card deck, so let’s start with the definition of the 
```csharp
class "card". If we do not have an idea of how to represent one single card, 
```
we could not have any idea how to represent a deck as well. Therefore it will 
not be possible to define a method for shuffling  the cards. We have already 
agreed the representation of one card does not matter, so any kind of them 
might work. 
We will define a class "card" with fields face and suit . We will use a string 
variable for the face of the card (with possible values: "2", " 3", "4", "5", "6", 
"7", "8", "9", "10", "J", "Q", "K" or "A") and enumerable variable for the suit of 
the card (possible values: "Club", "Diamond", "Heart", "Spade"). The class 
Card might look like the following code: 
Card.cs 
```csharp
class Card 
{ 
 public string Face { get; set; } 
 public Suit Suit { get; set; } 
 
 public override string ToString() 
 { 
  string card = "(" + this.Face + " " + this.Suit + ")"; 
  return card; 
 } 
} 
 
enum Suit 
{ 
```
 CLUB, DIAMOND, HEART, SPADE 
} 
For comfort we have overridden the method ToString() for the class Card. 
In this way we could easily print a single card on the console. We have 
defined enumerable type for the Suit.

### Testing of the Class "Card"

Some of us would probably proceed with writing the code, but if we follow the 
principle "Writing Code Step by Step", we should firstly compile and test 
how the class Card works. 
In order to do so, we can write a small simple program to initialize a single 
card (e.g. Ace of Clubs)  and print it on the console . This will check whether 
our class Card, its constructor and its ToString() method work correctly: 

```csharp
static void Main() 
{ 
 Card card = new Card() { Face="A", Suit=Suit.CLUB }; 
 Console.WriteLine(card); 
} 
We start the program and check if the card is printed correctly. We should see 
```
the following: 
(A CLUB)

### Step 2 – Creating and Printing a Deck of Cards

Before we proceed with the main task (randomly shuffling the deck of cards) 
we should try to initialize and print a whole deck of 52 cards . Thus we 
will be completely sure that the input data for the card -shuffle method is 
correct. Based on our previous analysis on the data structures, we should use 
List<Card> in order to represent the deck. Let ’s create and print a deck of 
five cards, shall we? Later we can try with a full deck of 52 cards. 
CardsShuffle.cs 
```csharp
class CardsShuffle 
{ 
 static void Main() 
 { 
  List<Card> cards = new();
  cards.Add(new Card() { Face = "7", Suit = Suit.HEART }); 
  cards.Add(new Card() { Face = "A", Suit = Suit.SPADE }); 
  cards.Add(new Card() { Face = "10", Suit = Suit.DIAMOND }); 
  cards.Add(new Card() { Face = "2", Suit = Suit.CLUB }); 
  cards.Add(new Card() { Face = "6", Suit = Suit.DIAMOND }); 
  cards.Add(new Card() { Face = "J", Suit = Suit.CLUB }); 
  PrintCards(cards); 
 } 
 
 static void PrintCards(List<Card> cards) 
 { 
  foreach (Card card in cards) 
  { 
   Console.Write(card); 
  } 
  Console.WriteLine(); 
 } 
}
```

### Printing the Deck – Testing the Code

Before we proceed  forward, let ’s start the program and verify the output 
result. It seems that there are no mistakes, the result is correct: 
(7 HEART)(A SPADE)(10 DIAMOND)(2 CLUB)(6 DIAMOND)(J CLUB)

### Step 3 – Single Swap

Let’s implement the next step to the task solution – the subtask "single 
swap". When we have a logically independent piece of code, the best thing to 
do is to extract it as a separate method. We should think of what is our input 
and output. Our input should be a single deck of cards ( List<Card>). As a 
result of its work the method should change the input deck List<card>. The 
method should not return any result, because it does not create a new 
List<Card>, it just operates with the already created and submitted list. 
What should be the name of the method? Following the recommendations for 
working with methods, we should give "descriptive" name (with 1 -2 words) 
what the method is for. Suitable name for it is: PerformSingleSwap(…). The 
name clearly describes what the method does: executes a single swap. 
Let’s firstly define the method and then write its body. This is a good practice, 
because before we proceed with the implementation of the method, we should 
be aware: what does it do; how does it work and what is its name. Here it is 
the definition of the method: 
```csharp
static void PerformSingleSwap(List<Card> cards) 
{ 
 // TODO: Implement the method body 
} 
```
The next thing to do is to write the body itself. Firstly let ’s recall the 
algorithm: we choose one random number k  in the interval between 1 and 
the length of the array minus 1 and then swap the element at the position 
k with the first element. Everything seems easy, but how do we generate a 
random number in a given interval with the language C#?

### Search in Google!

When we encounter a common problem, which we cannot solve, but we are 
sure that many people have faced it, the easiest way to cope with it is to 
search for information in Google. We should adequately structure our search. 
In our case we look for sample C# cod e, which returns as a result a 
random number in a given interval. We could try the following search: 
C# random number example 

Among the first results there is a C# program, which uses the class 
System.Random for generating a random number. Now we have a direction 
in which we look for a solution. We know that in .NET Framework there is a 
standard class called Random, which serves for generating random numbers. 
After that we could try to guess how this class works (most of the times it 
takes less time to guess instead of reading the documentation). We are trying 
to find an appropriate static method for generating a random number, but it 
seems there is none. Then we make an instance and search for a method, 
which could return a number in given  a diapason. We have luck, there is a 
method Next(minValue, maxValue), which returns what we need. 
Let’s try to write the whole code for the method. We have the following: 
```csharp
static void PerformSingleSwap(List<Card> cards) 
{ 
 Random rand = new();
 int randomIndex = rand.Next(1, cards.Count - 1); 
 Card firstCard = cards[1]; 
 Card randomCard = cards[randomIndex]; 
 cards[1] = randomCard; 
 cards[randomIndex] = firstCard; 
}
```

### Single Swap – Testing the Code

The next step is to test the code. Before proceeding forward, we have to be 
sure that the single swap ( exchange) operation works properly. We do not 
want to find an eventual problem just when we test the "card -shuffle" method 
with the entire deck? It is better when there is a problem to b e found 
immediately and when there is none, to continue forward with confidence. 
We act step by step – before going to the next step we should make sure that 
the current step is working fine. For this purpose we make a small test 
program, let’s say with 3 cards (2♣, 3♥ and 4♠): 
```csharp
static void Main() 
{ 
 List<Card> cards = new();
 cards.Add(new Card() { Face = "2", Suit = Suit.CLUB }); 
 cards.Add(new Card() { Face = "3", Suit = Suit.HEART }); 
 cards.Add(new Card() { Face = "4", Suit = Suit.SPADE }); 
 PerformSingleSwap(cards); 
 PrintCards(cards); 
} 
```
Let’s perform several times a single swap operation with our 3 cards . 
The first card (card 2 ♣) is supposed to be with one of the other two cards 
(cards 3♥ or 4♠). We execute the program several times in a sequence. We 

should expect the half of the obtained results to contain (3♥, 2♣, 4♠) and the 
other half – (4♠, 3♥, 2♣), shouldn’t we? Let’s see what is going to happen. We 
start the program and see the following results: 
(2 CLUB)(3 HEART)(4 SPADE) 
We start it again and again and the result is the same – no swap is made . 
How is that possible? What has just happened? Did we miss to execute the 
single swap before printing the cards? There is something wrong here. It 
seems that the program did not make even one  swap in the deck of cards. 
How did this happen?

### Single Swap – Correcting the Mistakes

It is obvious that there is a mistake. Let’s put a breakpoint and follow what 
is happening via the debugger of Visual Studio: 
 
It is clear that during the first execution the random position happens to be 
one. This is acceptable so we continue on. When we look the code we follow, 
we notice that we swap the random element  at index 1 with the element 
at position 1 i.e. with itself . We apparently did something wrong . And 
then we remember that indexing in List<T> is zero -based i.e. the first 
element is at position 0. We immediately change the code: 
```csharp
static void PerformSingleSwap(List<Card> cards) 
{ 
 Random rand = new();
 int randomIndex = rand.Next(1, cards.Count - 1); 
 Card firstCard = cards[0]; 
 Card randomCard = cards[randomIndex]; 
 cards[0] = randomCard; 
 cards[randomIndex] = firstCard; 
} 
We start the program several times and we get unexpected results, again: 
```
(3 HEART)(2 CLUB)(4 SPADE) 


(3 HEART)(2 CLUB)(4 SPADE) 
(3 HEART)(2 CLUB)(4 SPADE) 
It seems that the random number is not so random . What to do now? Do 
not rush to blame .NET Framework, CLR, Visual Studio and all other usual 
suspects! It is possible that the mistake is ours. Let’s look at the execution 
of the method Next(…). Since cards' count is 3, we always call Next(1, 2) 
and expect from it to return a number between one and two. It seems correct 
but if we read what the documentation says for the method Next(…), we will 
notice that the second parameter should be one unit bigger than the upper 
border we want to obtain. 
We were wrong about the diapason of the random number  that we 
selected. We correct the code and once again we test it to see how it works. 
After the second correction we get the following results: 
```csharp
static void PerformSingleSwap(List<Card> cards) 
{ 
 Random rand = new();
 int randomIndex = rand.Next(1, cards.Count); 
 Card firstCard = cards[0]; 
 Card randomCard = cards[randomIndex]; 
 cards[0] = randomCard; 
 cards[randomIndex] = firstCard; 
} 
```
Here are the possible results after several executions of the previous method: 
(3 HEART)(2 CLUB)(4 SPADE) 
(4 SPADE)(3 HEART)(2 CLUB) 
(4 SPADE)(3 HEART)(2 CLUB) 
(3 HEART)(2 CLUB)(4 SPADE) 
(4 SPADE)(3 HEART)(2 CLUB) 
(3 HEART)(2 CLUB)(4 SPADE) 
It seems that after enough executions the first card is replaced by each of the 
other two cards i.e. we have a random swap indeed and every card has the 
equal chance to be randomly chosen. We are finally ready with the method 
"single swap". It is better that we found these two mistakes now and not 
later when the whole program is supposed to start working, right?

### Step 4 – Card Shuffling

The last step is simple: we use the single-swap method N times: 
```csharp
static void ShuffleCards(List<Card> cards) 
{ 

 for (int i = 1; i <= cards.Count; i++) 
 { 
  PerformSingleSwap(cards); 
 } 
} 
```
We now can put it all together. We combine all the pieces of code  we 
already wrote, tested and we checked they work correctly. The entire code of 
our program looks like this: 
CardsShuffle.cs 
```csharp
using System; 
using System.Collections.Generic; 
 
class CardsShuffle 
{ 
 static void Main() 
 { 
  List<Card> cards = new();
  cards.Add(new Card() { Face = "2", Suit = Suit.CLUB }); 
  cards.Add(new Card() { Face = "6", Suit = Suit.DIAMOND }); 
  cards.Add(new Card() { Face = "7", Suit = Suit.HEART }); 
  cards.Add(new Card() { Face = "A", Suit = Suit.SPADE }); 
  cards.Add(new Card() { Face = "J", Suit = Suit.CLUB }); 
  cards.Add(new Card() { Face = "10", Suit = Suit.DIAMOND }); 
 
  Console.Write("Initial deck: "); 
  PrintCards(cards); 
 
  ShuffleCards(cards); 
  Console.Write("After shuffle: "); 
  PrintCards(cards); 
 } 
 
 static void PerformSingleSwap(List<Card> cards) 
 { 
  Random rand = new();
  int randomIndex = rand.Next(1, cards.Count); 
  Card firstCard = cards[0]; 
  Card randomCard = cards[randomIndex]; 
  cards[0] = randomCard; 
  cards[randomIndex] = firstCard; 
 } 
 

 static void ShuffleCards(List<Card> cards) 
 { 
  for (int i = 1; i <= cards.Count; i++) 
  { 
   PerformSingleSwap(cards); 
  } 
 } 
 
 static void PrintCards(List<Card> cards) 
 { 
  foreach (Card card in cards) 
  { 
   Console.Write(card); 
  } 
  Console.WriteLine(); 
 } 
}
```

### Card Shuffling – Testing

Now we only need to test whether the algorithm for shuffling a deck of cards  
works correctly. Here is the output of our program: 
Initial deck: (7 HEART)(A SPADE)(10 DIAMOND)(2 CLUB)(6 
DIAMOND)(J CLUB) 
After shuffle: (7 HEART)(A SPADE)(10 DIAMOND)(2 CLUB)(6 
DIAMOND)(J CLUB) 
As we can see, we encounter a problem: after the shuffle the cards did not 
change their order. We run the program several times and the result is the 
same. Did we forget to call the card-shuffling method ShuffleCards? 
We take a close look at our source code: everything looks fine . We decide 
to set a breakpoint after the call of the method PerformSingleSwap(…) in the 
body of the loop, responsible for the shuffling of the cards. We run our 
program in debugging mode by pressing the [F5] button. After the first 
stop of the debugger at our breakpoint everything seems good  – the first 
card is exchanged with another one, as it supposed to. After the second stop 
of the debugger everything is still all right – a random card is swapped with 
the first one. We continue tracing the program execution with the debugger 
and everything seems to work just fine. 
The card shuffling program works flawlessly when we run it step by step 
through the Visual Studio debugger: 

 
But why is the final result wrong? We decide to set another breakpoint in the 
body of ShuffleCards(…) at the end. The debugger stops and at this point 
and the result is still correct – the cards are randomly shuffled. We continue 
debugging and we reach the place where we print the deck. We go pass it and 
the cards are printed to the console in random orde r. Strangely: still the 
correct result. What is the problem? 
We start the program without debugging  it with [Ctrl+F5]. The result is 
wrong – the cards are not shuffled. We run our program in debugging mode 
again with the press of [F5]. The debugger once mo re stops at the 
breakpoints and the program yet again is working without any problem. It 
looks like that, when we run our program in debug mode the result is 
correct, but when we start it normally , without the debugger, the answer 
is wrong. Strange indeed! 
We decide to add a line of code, which is going to print the de ck after 
every single swap: 
```csharp
static void ShuffleCards(List<Card> cards) 
{ 
 for (int i = 1; i <= cards.Count; i++) 
 { 
  PerformSingleSwap(cards); 
  PrintCards(cards); 
 } 
} 
```
We run our program in debug mode (with [F5]), observe the execution step 
by step and we find that it works correctly: 
Initial deck: (7 HEART)(A SPADE)(10 DIAMOND)(2 CLUB)(6 
DIAMOND)(J CLUB) 
(A SPADE)(7 HEART)(10 DIAMOND)(2 CLUB)(6 DIAMOND)(J CLUB) 
(6 DIAMOND)(7 HEART)(10 DIAMOND)(2 CLUB)(A SPADE)(J CLUB) 
(J CLUB)(7 HEART)(10 DIAMOND)(2 CLUB)(A SPADE)(6 DIAMOND) 
(2 CLUB)(7 HEART)(10 DIAMOND)(J CLUB)(A SPADE)(6 DIAMOND) 


(A SPADE)(7 HEART)(10 DIAMOND)(J CLUB)(2 CLUB)(6 DIAMOND) 
(10 DIAMOND)(7 HEART)(A SPADE)(J CLUB)(2 CLUB)(6 DIAMOND) 
After shuffle: (10 DIAMOND)(7 HEART)(A SPADE)(J CLUB)(2 CLUB)(6 
DIAMOND) 
We run again our program in normal mode (with [Ctrl+F5]) and the answer 
is still incorrect. Yet again we try to find out why it is happening: 
Initial deck: (7 HEART)(A SPADE)(10 DIAMOND)(2 CLUB)(6 
DIAMOND)(J CLUB) 
(6 DIAMOND)(A SPADE)(10 DIAMOND)(2 CLUB)(7 HEART)(J CLUB) 
(7 HEART)(A SPADE)(10 DIAMOND)(2 CLUB)(6 DIAMOND)(J CLUB) 
(6 DIAMOND)(A SPADE)(10 DIAMOND)(2 CLUB)(7 HEART)(J CLUB) 
(7 HEART)(A SPADE)(10 DIAMOND)(2 CLUB)(6 DIAMOND)(J CLUB) 
(6 DIAMOND)(A SPADE)(10 DIAMOND)(2 CLUB)(7 HEART)(J CLUB) 
(7 HEART)(A SPADE)(10 DIAMOND)(2 CLUB)(6 DIAMOND)(J CLUB) 
After shuffle: (7 HEART)(A SPADE)(10 DIAMOND)(2 CLUB)(6 
DIAMOND)(J CLUB) 
We can clearly see that at every step, on which we expect to be done a 
single-swap, actually the same cards are swapped: 7 ♥ and 6♦. The only 
way this to happen is if every time the random number is the same . The 
conclusion is that we have a problem with the generation of random numbers.  
The random generator does not work correctly. 
We instantly think about taking a look at the documentation of the class 
System.Random(). On MSDN we can read, that by creating a new instance of 
the generator of pseudo -random numbers with the constructor Random(), the 
generator is initialized with a value, equal to the current system time . In 
the documentation we can further read that by creating two or more instances 
of the class Random in a relatively short time span, there is a great chance the 
numbers to be the same. It turns that the problem consists in the misuse of 
the class Random. 
Now being more familiar with the current problem, we could correct it by 
creating an instance of the class Random only once at the beginning of 
the program. After that, i f we need a random number, we are going to use 
the already created generator of pseudo -random numbers. After the 
correction, the code looks like this: 
```csharp
class CardsShuffle 
{ 
```
 … 
 
```csharp
 static Random rand = new Random(); 
 

 static void PerformSingleSwap(List<Card> cards) 
 { 
  int randomIndex = rand.Next(1, cards.Count); 
  Card firstCard = cards[0]; 
  Card randomCard = cards[randomIndex]; 
  cards[0] = randomCard; 
  cards[randomIndex] = firstCard; 
 } 
```
 … 
} 
It seems that the program finally works correctly . At every run the order 
of the cards is different and looks randomly: 
Initial deck: (7 HEART)(A SPADE)(10 DIAMOND)(2 CLUB)(6 
DIAMOND)(J CLUB) 
After shuffle: (2 CLUB)(A SPADE)(J CLUB)(10 DIAMOND)(7 HEART)(6 
DIAMOND) 
-------------------------------------------------------- 
Initial deck: (7 HEART)(A SPADE)(10 DIAMOND)(2 CLUB)(6 
DIAMOND)(J CLUB) 
After shuffle: (6 DIAMOND)(10 DIAMOND)(J CLUB)(2 CLUB)(A 
SPADE)(7 HEART) 
We try some other tests and the final result is still correct. Now we can say 
that we have a correct implementation of our algorithm, which we have 
designed earlier.

### Step 5 – Console Input

Now we only need to be able to read the deck from the console so that the 
user can enter the cards, which need to be shu ffled. Notice that we left this 
step as last. Why? The answer is pretty simple: we do not want to enter the 
same data at the start of our program just to check whether a piece of our 
code works correctly. By having the needed data hard -coded in the source 
code, we save a lot of time during the developing process. 
 
If the problem involves entering data from the console, leave 
this as last step and be sure that everything else works 
flawlessly before implementing the reading of the input. 
While writing the source code, do your tests with hard -coded 
examples so that you don ’t have to enter any data. This way 
you are going to save your time and your nerves. 
Data entry is a low -priority task , which everyone can handle. You only 
need to think of the format the cards are entered – are they entered one by 


one or at once; are the face and the color entered at once or separately. 
There is nothing difficult so we leave this to our readers.

## Sorting Numbers – Step by Step

Till this moment, we showed you how important i t is to write your 
program step by step and before proceeding to the next step you must be 
sure that the previous one is working correctly. 
Let’s take a look at the problem of ordering numbers in ascending order . 
The steps for solving this problem are the same. Once again the right 
approach for solving this task is to work step by step . Let ’s see shortly 
which the steps are. We are not going to write any code, but we are going to 
consider the main parts of the solution. Suppose that we use List<int>, in 
which we successively find the lowest number, print it and erase it from the 
list of numbers. These are the steps: 
Step 1. Think up a good test data (example). For example the numbers: 
7, 2, 4, 1, 8 and 2. We create a List<int> and fill it with the numbers 
from the example. We create a method, which outputs the numbers. 
We compile and test the piece of code we just wrote. 
Step 2. We create a method to find the lowest number  in the array and 
return its position. 
We test the method, responsible for the search of the lowest number. We 
try different sets of numbers to be sure that the search works correctly (we 
set the lowest element at the beginning, at the end, at the middle; we also 
consider a case when the lowest number occurs more than once). 
Step 3. We create a method to find the lowest number , print it and 
after that delete it. 
We test with our example  if the method works correctly. We must also 
try with other examples. 
Step 4. We create a method to sort the numbers. This method executes 
N times the previous one (where N is the count of the numbers). 
We must test whether everything works correctly. 
Step 5. If data input is required we implement it after everything else. 
You can see that the approach to break the main problem into smaller 
problems can work well for all tasks. Simply we just need to figure out which 
are the smaller problems, from the bigger one, and implement them. On 
every smaller problem implemented we need to test it. After every step we 
need to start our program to be sure that till this mome nt everything works 
flawlessly. In this way we are be able to find out errors quickly and debug 
them. It would be much faster than trying to debug them after we have 
written tons of code.