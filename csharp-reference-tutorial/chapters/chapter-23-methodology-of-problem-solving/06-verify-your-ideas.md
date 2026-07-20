# Verify Your Ideas!

It seems that we have figured out everything. We have an idea. It seems to 
work properly. The only thing for us to do is to check if our idea is correct or it 
is only correct in our minds. After that we can start with the implementation . 
How to verify an idea?  Usually this happens with the help of some 
examples. We should choose examples that fully cover all different cases, 
which our algorithm should be able to pass. The sample examples should not 
be too easy for your algorithm, but also they should not be so hard to be 
sketched. We call these certain types of examples " good representatives of 
the common case". 

E.g. if our task is to sort an array in ascending order, then a suitable example 
would be an array with 5-6 elements. Two of the numbers in the array 
should be equal and the other – different. The numbers should be randomly  
placed in the array. This is a good example, because it covers most of the 
common cases, in which our algorithm should work. 
There are many inappropriate examples  for the sorting numbers problem 
that could not help you test your idea properly. For example  if you use an 
array of only two elements. Your solution could work correctly with it, but 
your core idea could be completely wrong. Another inappropriate example is 
an array of equal numbers. Every sorting algorithm would work correctly with 
it. And anoth er bad example – we can use an array that is already sorted. 
Algorithm could also work correctly and yet the idea could be wrong. 
 
When verifying your ideas, choose your examples carefully. 
They should be simple and easy enough for you to be able to 
sketch them down by hand in a minute and at the same time 
they should represent most general case in which your idea 
should work. Your examples should be good representatives 
of the common case and cover as much cases as possible 
without being too big and complicated.

## "Cards Shuffle" Problem: Verifying the Idea

Let’s think of one sample example for our "Cards Shuffle" task. Let ’s say we 
have 6 cards. In order our example to be good, our deck of cards should not 
be too small  (e.g. 2-3 cards), because in this way  our example might 
become very easy. Also, if we want to easily check our idea with the deck, it 
should not be too big. Initially it is a good idea to get six cards and order 
them in the deck. In this way it would be easier for us to see if the cards are 
well shuffled or partially shuffled or not shuffled at all. So one of the smartest 
things to do is to choose 6 cards regardless of their suit and order them by 
value. 
Now we already have one example, which is a good representative of the 
common case of our problem. Let ’s now sketch it down on a piece of paper 
and check our algorithm on it. We should split the deck into two parts, at a 
random position 6 times and then swap them. Our cards are ordered by 
value. At the end we expect them to be randomly shuffled. 
Let’s see what is going to happen: 


 
There is no need to do 6 swaps. After only 3 swaps we came back to the 
starting position. This is probably not an accident. W hat happened? We have 
just found an error in our algorithm . When we reflect on the problem we 
can see that with every swap at a random position we rotate the deck to left 
and after N times it goes to the starting position. So it was a good thing that 
we tested our idea before even started writing some code, wasn’t it?

## Sorting Numbers: Verifying the Idea

It is time to check our first idea  considering the sorting numbers problem. 
We can easily see if it is right or wrong. We start with an array of N elements 
and we find the smallest number, print it and then delete it from the array N 
times. Even if we do not sketch the idea, it seems faultless. Still let ’s think of 
one example and see what is going to happen. We take 5 numbers, two of 
them are equal: 3, 2, 6, 1, 2. We have 5 steps to do: 
1) 3, 2, 6, 1, 2 → 1 
2) 3, 2, 6, 2 → 2 
3) 3, 6, 2 → 2 
4) 3, 6 → 3 
5) 6 → 6 
Seems like our algorithm works properly . Our result is correct and we do 
not have a reason to think that our idea will not work with any other example.