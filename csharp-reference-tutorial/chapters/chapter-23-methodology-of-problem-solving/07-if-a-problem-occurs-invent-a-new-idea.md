# If a Problem Occurs, Invent a New Idea!

If a Problem Occurs, Invent a New Idea! 
When you find your idea is incorrect, the obvious thing to do is to invent a 
new, better idea . We can do this in two ways: we can either try to fix our 
old idea or create a completely new one. Let ’s see how this works with our 
cards shuffle problem, shall we? 
6
♥
7
♠
5
♣
4
♦
3
♣
2
♠
6
♥
7
♠
5
♣
4
♦
3
♣
2
♠
3
♣
2
♠
3
♣
2
♠
6
♥
7
♠
5
♣
4
♦
6
♥
7
♠
5
♣
4
♦
4
♦
5
♣
6
♥
7
♠
3
♣
2
♠
4
♦
5
♣
6
♥
7
♠
3
♣
2
♠
4
♦
5
♣
6
♥
4
♦
5
♣
6
♥
3
♣
2
♠
7
♠
3
♣
2
♠
7
♠
7
♠
2
♠
3
♣
4
♦
6
♥
5
♣
7
♠
2
♠
3
♣
4
♦
6
♥
5
♣
7
♠
7
♠
6
♥
5
♣
4
♦
3
♣
2
♠
6
♥
5
♣
4
♦
3
♣
2
♠
2
♠
3
♣
4
♦
5
♣
7
♠
6
♥
2
♠
3
♣
4
♦
5
♣
7
♠
6
♥
…

 
The creating of a solution for a computer programming 
problem is an iterative process, which consists of inventing 
ideas, verifying them and sometimes, when problem occurs, 
inventing new one s. Sometimes the first idea that comes to 
our mind is the right one, but most of the times we need to 
go through many different ideas until we reach the best one. 
Let’s go back to our card shuffle problem . Firstly let’s see why our premier 
idea is wrong and is it possible to fix it? The problem here is easily 
recognized: the continuous splitting and card swapping does not shuffle them 
randomly; it simply rotates them to left. 
How to fix this algorithm?  We need to think of a new and better way to 
make a "single swap" operation, don ’t we? Our new idea for one single swap 
is: randomly choose two cards from the deck and swap their places. If we do 
this N number of times, we would probably get randomly shuffled deck. This 
idea looks better than the previous one and maybe it would work correctly  
this time. We already know that before we even start thinking of 
implementing our new algorithm it is better to check it  and see if it is 
working properly. We can verify our idea by using pen and paper and the 
example with the 6 cards that we used above. 
In this moment we think of an even better idea, instead of choosing 2 random 
cards from the deck, why not just pick one random card and swap it with 
the first card from the deck ? Isn ’t this idea simpler and easier to 
implement? The result should be random too. Let ’s start by choosing a 
random card at position k 1 and swap it with the first card. Now we have a 
random card at the first position and the first card is at the k 1 position. On the 
next step of the algorithm we pick another card at random position k2 and 
then swap it with the card from the first position (previously the card from the 
position k1). It is apparent that with only 2 steps we have changed the place 
of the first, the k 1-st and the k 2-nd cards from the deck with random cards. It 
seems that at every step one card changes its position with a random one. 
After N number of steps we can expect that every card from the deck has 
changed its position averagely one time. Hence ou r solution is working and 
the cards should be well shuffled. 
Now we should test our new idea . Does it work properly? Let ’s make sure 
that what has happened last time will not happen again, shall we? Let ’s 
thoroughly check this idea as well. Again, we can t ake the 6 cards example, 
which represents most of the general cases  of the card shuffle problem 
(good representative of the common case). Then use the new algorithm and 
shuffle them. We should do this 6 times in a row. This is the result: 


 
From the example above we can see that the result is correct  – we have 
randomly shuffled six cards. If our algorithm works well with 6 cards, it should 
work with decks with different number of cards as well. If we are not sure in 
that, we should think of a nother more complicated example and then test the 
algorithm again. 
Otherwise we could avoid drawing new examples and continue with our task.  
Let’s summarize what we have done so far  and how with consecutive 
actions we have figured out a solution for our pr oblem. As we have gone 
through every step, we have done so far the following steps: 
- We have used a sheet of paper and pen  to sketch a deck of cards. 
We have visually represented the deck of cards as an array of boxes. 
- As we already have a visual feedback, we could easily think of some 
sample ideas: firstly we should make some kind of a single swap 
operation and secondly we do this N number of times. 
- We had decided that our "single swap" operation  was going to be 
splitting the deck at random position into le ft and right part and then 
swap them. 
- We have decided that we should do this "single swap" as much times as 
the number of cards in the given deck. 
- We have considered the problem of choosing a random number , but 
have finally decided to use a ready solution for the job. 
6
♥
7
♠
5
♣
4
♦
3
♣
2
♠
6
♥
7
♠
5
♣
4
♦
3
♣
2
♠
2
♠
7
♠
5
♣
4
♦
3
♣
6
♥
2
♠
7
♠
5
♣
4
♦
3
♣
6
♥
2
♠
7
♠
5
♣
4
♦
3
♣
6
♥
2
♠
7
♠
5
♣
4
♦
3
♣
6
♥
2
♠
7
♠
5
♣
4
♦
6
♥
3
♣
2
♠
7
♠
5
♣
4
♦
6
♥
3
♣
2
♠
7
♠
5
♣
4
♦
6
♥
3
♣
2
♠
7
♠
5
♣
4
♦
6
♥
3
♣
2
♠
3
♣
5
♣
4
♦
6
♥
7
♠
2
♠
3
♣
5
♣
4
♦
6
♥
7
♠
2
♠
3
♣
5
♣
4
♦
6
♥
7
♠
2
♠
3
♣
5
♣
4
♦
6
♥
7
♠
2
♠
3
♣
7
♠
4
♦
6
♥
5
♣
2
♠
3
♣
7
♠
4
♦
6
♥
5
♣
2
♠
3
♣
7
♠
4
♦
6
♥
5
♣
2
♠
3
♣
7
♠
4
♦
6
♥
5
♣
5
♣
3
♣
7
♠
4
♦
6
♥
2
♠
5
♣
3
♣
7
♠
4
♦
6
♥
2
♠
5
♣
3
♣
7
♠
4
♦
6
♥
2
♠
5
♣
3
♣
7
♠
4
♦
6
♥
2
♠
5
♣
3
♣
2
♠
4
♦
6
♥
7
♠
5
♣
3
♣
2
♠
4
♦
6
♥
7
♠

- We have decomposed the main problem  into three smaller subtasks: 
"single swap" operation; choosing a random split point; combining a 
sequence of "single swap" operations. 
- We have checked our idea for mistakes and found one. It was a good 
thing to check it when we did, because it was not too late to fix it. 
- We have thought of a new, more reliable solution  of the single swap 
operation. 
- We have checked our new idea with an appropriate example  and 
we assured ourselves that this time the solution was right. 
Now we finally have a working idea, backed up with good examples. This is 
the most important thing to do in order to solve a given problem – inventing 
of the algorithm. The easier part remains – the implementation of our 
idea. Let’s see how this can be done.