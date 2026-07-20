# Using Recursion – Conclusions

Using Recursion – Conclusions 
The general conclusion from the problem searching a path in a labyrinth is 
already formulated: if you do not understand how recursion works, 
avoid using it! 
Be careful when you write recursive methods. Recursion is a powerful 
programming technique for solving combinatorial problems (problems in 
which we have to go through all variants), but it is not for everyone . We 
can easily make mistakes when using recursion. You may make the program 
"hang", or cause stack overflow wit h bottomless recursion. Always look for 
iterative solutions, unless you deeply understand how to use recursion. 
As to the problem searching shortest path in a labyrinth you can solve it 
elegantly without recursion with the so called  BFS (breadth-first search), 
also known as  the wavefront algorithm, which is elementary implemented 
with a queue. You can read more about the " BFS" algorithm in this article in 
Wikipedia: http://en.wikipedia.org/wiki/Breadth-first_search.