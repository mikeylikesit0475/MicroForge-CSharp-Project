# Solutions and Guidelines

Solutions and Guidelines 
1. Traverse the tree recursively  in depth (using DFS) and count the 
occurrences of the given number. 
2. Traverse the tree recursively  in depth (using DFS) and check for each 
node the given condition . For each node the number of nodes in its 
subtree is: 1 + the sum of the nodes of each of its child subtrees. 
3. You can solve the problem by traversing the tree in depth recursively. 

4. Use traversing in depth  or breadth and when shifting from one node 
to another keep its level (depth). Knowing the levels of the nodes at 
each step, the wanted amount can be easily calculated. 
5. You can solve the problem by recursively traversing the tree  in depth 
and by checking the given condition. 
6. By recursive traversal in depth  (DFS) for every node of the tree 
calculate the depths of its left and right sub-trees. Then check 
immediately whether the condition of the definition for perfectly balanced 
tree is executed  (check the difference between the left and right sub -
tree’s depths). 
7. Use the algorithm o f traversing in breadth  (BFS) as a base. In the 
queue put every node always along with its predecessor. This will help 
you to restore the path between the nodes (in reverse order). 
8. Use traversing in depth or in breadth . Mark every node, if already 
visited. If at any time you reach to a node, which  has already been 
visited, then you have found loop. 
Think about how you can find and print the loop itself . Here is an 
idea: while traversing every node keep its predecessor . If at any 
moment you reach a node that has already been visited, you should have 
a path to the initial node. The current path in the recursion stack is also a 
path to the wanted node. So at some point we have two different paths 
from one node to the initial node. By merging the two paths  you can 
easily find the loop. 
9. Use the DFS algorithm. Testing can be done with few example graphs. 
10. Use the BFS algorithm. Instead of putting the vertices of the graph in the 
queue, put their numbers (0 … N-1). This will simplify the algorithm. 
11. Use traversing in depth and System.IO.Directory class. 
12. Use the example of the tree data structure  given in this chapter . Each 
directory from the tree should two arrays (or lists)  of descendants: 
subdirectories and files. 
13. Use the solution of problem 8, but modify it so it does not stop when it 
finds a loop, but continues. For each loop you should check if you have 
already found it. This problem is more complex than you may expect! 
14. Use the algorithms for traversing in breadth or depth as a base. 
15. Use the Dijkstra’s algorithm (find it on the Internet). 
16. The requested order is called " topological sorting of a directed graph". 
It can be implemented in two ways: 
For every task t we should know how many others tasks P(t) it depends 
on. We find task t0, which is independent, i.e. P(t0)=0 and we execute it. 
We reduce P(t) for every task, which depends from task t0. Again we 
look for a task, which is independent and we execute it. We repeat until 

the tasks end or until we find a moment when there is no task tk having 
P(tk)=0. In the last case no solution exists due to a cyclic dependency. 
We can solve the task with traversing the graph in depth  and printing 
every node just before leaving it. That means that at any time of pri nting 
of a task, all the tasks that depend on it should have already been  
printed. The topological sorting will be produced in reversed order. 
17. The graph must be connected and the degree of each of its nodes 
must be even  in order an Eulerian cycle in a graph  to exits (can you 
prove this?) . With series of DFS traversals  you can find cycles in the 
graph and to remove the edges involved in them. Finally, by joining the 
cycles you will get the Eulerian cycle. See more about Eulerian paths and 
cycles at http://en.wikipedia.org/wiki/Eulerian_path. 
18. If you write a true solution of the problem, check whether it works for a 
graph with 200 nodes. Do not try to solve the problem so it could work 
with a large number of nodes ! If someone manages to solve it for large 
numbers of nodes, he will remain permanently in history!  See also the 
Wikipedia article http://en.wikipedia.org/wiki/Hamiltonian_path_problem. 
You might try some recursive algorithm for generating all paths  but 
accept that it will be slow. Techniques like backtracking and branch and 
bound could help a bit but generally this problem is NP-complete and 
thus no efficient solution is known to exist for it.