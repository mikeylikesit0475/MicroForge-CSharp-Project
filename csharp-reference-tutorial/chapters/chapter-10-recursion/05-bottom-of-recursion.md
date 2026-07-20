# Bottom of Recursion

Bottom of Recursion 
When using recursion, we have to be totally sure that after a certain count of 
steps we get a concrete result. For this reason we should have one or more 
cases in which the solution could be found directly, without a recursive call. 
These cases are called bottom of recursion. 
In the example with Fibonacci numbers the bottom of recursion is when n is 
less than or equal to 2. In this base case we can directly return result without 
making recursive calls, because by definition the first two elemen ts of the 
sequence of Fibonacci are equal to 1. 
If a recursive method has no base case, i.e. bottom, it will become infinite 
and the result will be StackOverflowException.