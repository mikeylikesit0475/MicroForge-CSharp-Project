# Decompose the Task into Smaller Subtasks

Complex tasks can always be divided into smaller more manageable 
subtasks. We will show this with some examples below. There is not a single 
complex problem in this world that has been solved with one try. The correct 
formula for solving such a task is to split it into smaller simpler tasks , 
which have to be independent and different from one another. If these smaller 
subtasks prove to be complicated, we should split them again. This technique 
is called " divide and conquer " and it is in use since the time of the Roman 
Empire. 
The division of the problem into smaller units is easier said than done. The 
essence of solving algorithmic problems is in the good technique of division of 
the given task into simpler subproblems and, of course, the invention of good 
ideas that can be achieved with gaining more experience. 
 
Complex tasks can always be divided into smaller more 
manageable subta sks. When you have to solve big 
complicated tasks, you should always try to divide it into 
simpler problems, which are easier to solve.

## "Cards Shuffle" Problem – Example

Let’s give the following example: we have one ordered deck of cards  and 
we have to shuffle it in random order . Let ’s assume that the deck is 
represented as an array or list of N objects (every card is an object). These 
types of tasks require multiple repeating steps (series of removal, placing, 
replacing and realignment of elements). Each of these steps itself is simpler, 
easier and more manageable subtask , than the "Cards Shuffle" task as a 
whole. If we succeed in decomposing the complex task into smaller subtasks, 
we will basically find the right way to solve the problem. Exactly this is the 
essence of the algorithmic thinking : the ability to decompose complex 
problems into smaller ones and then find the correct solutions for them. Of 
course, this principle can be applied not only to programming problems, but 
also to ones from other scientific disciplines like math and physics. In fact this 
algorithmic thinking is the reason why the mathematicians and the physicists 
show a rapid progress when they begin to learn computer programming. 


Now let’s go back to the given task and think about how to find the simple 
subtasks, which are needed in order to meet the requirements to randomly 
shuffle the cards. 
If we take one deck of cards in our hands or try to sketch something on paper 
(e.g. series of rect angular cells, each of them representing one card), some 
ideas instantly come up , for example we need to change or realign 
elements from the deck. 
Thinking like this, we can easily reach the conclusion we need to make more 
than one swap of one or more card s. If we make only one swap, the deck of 
cards would not be completely random. Therefore we need many simpler 
operations for a single swap (exchange). 
We reached the point where we do the first decomposition into smaller 
subtasks: we need series of swaps , which can be considered as smaller 
tasks, a part of the bigger problem.

### First Subtask: a Single Swap

How do we make a single swap of cards in the deck? We can answer this 
question in many ways and take the first idea that come to our mind. If it is 
any good, we will use it. Otherwise we will think of something else. 
Our first idea can be: if we have a deck of cards, we can split it at random 
card and then separate and swap the two parts. Now do we have an idea for a 
single swap? Yes, we have. The next thing  to do is to check if our solution is 
working properly (we will demonstrate this after a while). 
Now let’s go back to the base task : after applying our idea, we need the 
deck of cards to be randomly shuffled. Now we split and swap it many times 
and check the result. It seems that our algorithm works fine and the subtask 
"single swap" will do the work.

### Second Subtask: Choosing a Random Number

How to generate a random number and use it to split the deck? If we have N 
cards, we need a random number between 1 and N-1, don’t we? 
In order to solve this problem we might need an additional help. If we know 
that in .NET Framework this task is already solved, we can simply use the 
integrated random number generator. 
Otherwise we have to think of a solution e.g. we ca n read one line from the 
keyboard and then measure the time span between the start of the program 
and the pressing of the button [Enter]. Since the time of every input is 
different (especially, if we report with accuracy to nanoseconds), we have a 
way to calculate a random number. The only problem now is to find a way 
to place this number in the interval [1…N-1] and probably most of us will 
remember that we can use the remainder of its division by (N-1). 
We can see that even the simplest subtasks can be div ided into smaller 
tasks, which  sometimes can be already solved for us. When we find a 

suitable solution for the current subtask, we need to go back to the base 
problem and test everything and see if it is working correctly put together. 
Let’s do that now, shall we?

### Third Subtask: Combining Swaps

Let’s go back to the main task. We have reached the conclusion we have to 
make as many "single swap" operations as needed to ensure the deck of 
cards will be correctly shuffled. This idea seems right and we should try it. 
Now this raises the question how many operations "single swap" are 
enough? Are 100 enough? Aren’t they too many? And what about 5 times? In 
order to give a good answer to this question, we need to think for a while. 
How many cards do we have? If we have several cards in the deck, we need 
fewer swaps. And if we have many cards, we need much more swaps, right? 
Therefore the number of swaps depends on the number of cards in the deck. 
To see how many swaps are enough, we can take one standard deck of car ds. 
How many cards are there in one standard deck? Most of us know there are 
52 cards in it. Well then try to figure out how many "single swap" operations 
are needed to randomly shuffle one deck of 52 cards. Are 52 enough? It 
seems enough because if we swap 52 times at random position it is likely that 
we will split the deck at every card (this conclusion is clear even if we do not 
know anything about Probability and Statistics). 52 "single swap" 
operations seem too much, isn ’t it? Let ’s think of even small er number. 
What about the half of the number 52? It seems fine as well, but it would be 
more difficult to explain why. 
Some of you probably think that the best way to find the correct number is to 
use complex formulas from the probability theory, but does it make any 
sense? The number 52 is small enough  and there is no need to look for 
other number. One loop of 52 iterations is fast enough. The cards in the deck 
would not be billions, would they? Therefore we do not have to think in that 
direction. We assum e that the correct number of "single swap" equals the 
number of the cards in the deck – neither too big nor too small. And this is 
the end of the current subtask.

## Another Example: Sorting Numbers

Let’s think of another example. We are given an array of numbers and our 
task is to sort it in ascending order . There is an abundance of algorithms 
for this problem and some of them conceptually different from one another. 
Even you could think of some ideas to solve this problem, some of them 
would be right and others – not quite. 
So we have to solve this task and we are not allowed to use built-in .NET 
Framework sorting methods. The first obvious thing to do is to take a pen and 
piece of paper and to think of one example and then to reflect on the task. 
Thus we can invent multiple and very different ideas like: 

- First idea: we can find the smallest number, print it and then remove it 
from the array of numbers. The next thing to do is to repeat the same 
action until the array is empty. Thinking like this, we can de compose 
this task into simpler tasks: finding the smallest number in array; 
deleting a number from array; printing a number. 
- Next idea : we can find the smallest number and put it at the first 
position of the array (swap operation). Then we can do the same action 
for the rest of the array. Since we have already placed number on the 
first position, we go to the next one. If we repeat this k times, we will 
have the first k smallest numbers from the array at the first k positions. 
This approach takes us natural ly to a task, which  can be very easily 
divided into smaller subtasks: finding the number with the smallest 
value in a part of the array and exchanging the positions of two 
numbers from the array. The second subtask can be divided one more 
time: removing an  element from a given position and placing an 
element at a given position. 
- Another idea , which uses a method, conceptually different from the 
previous two solutions: we split the array into two subarrays with 
approximately the same number of elements. Then  we sort them 
individually and finally we merge them into one. We can do this action 
recursively with every subarray until every one of them holds exactly 
one element. Array with one element is a sorted one. Here, like in the 
previous two ideas, we can div ide the complex problem into smaller 
more manageable problems: splitting one array into two parts with 
approximately equal number of elements; merging two arrays into one 
big array. 
There is no need to continue, right? It is obvious that every one of you c an 
think of several different solutions or you can read about the subject in a book 
about algorithms. We demonstrated that every complicated problem can 
be divided into smaller simpler problems . These is a correct approach to 
solving computer programming p roblems – to think of the big task like it is a 
collection of smaller easier subtasks. This technique may be hard to learn, but 
in time you will get used to it.