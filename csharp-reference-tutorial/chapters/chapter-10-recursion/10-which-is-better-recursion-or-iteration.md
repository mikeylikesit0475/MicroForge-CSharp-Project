# Which is Better: Recursion or Iteration?

If the algorithm solving of the problem is recursive, the implementation of 
recursive solution can be much more readable and elegant than iterative 
solution to the same problem. 

Sometimes defining equivalent algorithm is considerably more difficult and it 
is not easy to be proven that the two algorithms are equivalent. 
In certain cases by using recursion we can accomplish much simpler, 
shorter and easy to understand solutions. 
On the other hand, recursive calls can consume much more resources (CPU 
time and memory ). On each recursive call in the stack new memory is set 
aside for arguments, local variables and returned results. If there are too 
many recursive calls, a stack overflow could happen because of lack of 
memory. 
In certain situa tions the recursive solutions can be much more difficult to 
understand and follow than the relevant iterative solutions. 
Recursion is powerful programming technique , but we have to think 
carefully before using it. If used incorrectly, it can lead to inefficient and 
tough to understand and maintain solutions. 
 
If by using recursion we reach a simpler, shorter and easier 
for understanding solution, not causing inefficiency and 
other side effects, then we can prefer recursive solution. 
Otherwise, it is better to think of iteration.

## Fibonacci Numbers – Inefficient Recursion

Let’s go back to the example with finding the n th Fibonacci number  and 
look more carefully at the recursive solution: 
```csharp
static long Fib(int n) 
{ 
 if (n <= 2) 
 { 
  return 1; 
 } 
 return Fib(n - 1) + Fib(n - 2); 
} 
```
This solution is intuitive, short and easy to understand. At first sight it seems 
that this is a great example for applying recursion. The truth is that this is one 
of the classical examples of inappropriate usage of recursion. Let’s run the 
following application: 
RecursiveFibonacci.cs 
```csharp
using System; 
 
class RecursiveFibonacci 


{ 
 static void Main() 
 { 
  Console.Write("n = "); 
  int n = int.Parse(Console.ReadLine()); 
 
  long result = Fib(n); 
  Console.WriteLine("fib({0}) = {1}", n, result); 
 } 
 
 static long Fib(int n) 
 { 
  if (n <= 2) 
  { 
   return 1; 
  } 
  return Fib(n - 1) + Fib(n - 2); 
 } 
} 
If we set the value of n = 100, the calculations would take so much time that 
```
no one would wait to see the result. The reason is that similar implementation 
is extremely inefficient. Each recursive call leads to two more calls  and each 
of these calls cause s two more calls and so on. That's  why the tree of calls 
grows exponentially as shown on the figure below. 
The count of steps for computing of fib(100) is of the order of 1.6 raised to 
the power 100 (this could be mathematically proven), whereas, if the solution 
is linear, the count of steps would be only 100. 
The problem comes from the fact that there are a lot of excessive 
calculations. You can notice that fib(2) appears below many times on the 
Fibonacci tree:

## Fibonacci Numbers – Efficient Recursion

We can optimize the recursive method  for calculating the Fibonacci 
numbers by remembering (saving) the already calculated numbers in an array 
and making r ecursive call only if the number we are trying to calculate has 
not been calculated yet. Thanks to this small optimization technique (also 
known in computer science and dynamic optimization as memoization (not 
to be confused with memorization) the recursiv e solution would work for 
linear count of steps. Here is a sample implementation: 
RecursiveFibonacciMemoization.cs 
```csharp
using System; 
 
class RecursiveFibonacciMemoization 
{ 
 static long[] numbers; 
 
 static void Main() 
 { 
  Console.Write("n = "); 
  int n = int.Parse(Console.ReadLine()); 
 
  numbers = new long[n + 2]; 
  numbers[1] = 1; 
  numbers[2] = 1; 
 
  long result = Fib(n); 
  Console.WriteLine("fib({0}) = {1}", n, result); 
 } 
 
 static long Fib(int n) 
 { 
  if (0 == numbers[n]) 
  { 
   numbers[n] = Fib(n - 1) + Fib(n - 2); 
  } 
 
  return numbers[n]; 
 } 
} 
Do you notice the difference? While with the initial version if n = 100 it seems 
like the computation goes on forever, with the optimized solution we get an 
```
answer instantly. As we will learn later in chapter " Algorithm Complexity", the 
first solution runs in exponential time while the second is linear. 

n = 100 
fib(100) = 3736710778780434371

## Fibonacci Numbers – Iterative Solution

It is not hard to notice that we can solve the problem without using recursion, 
by calculating the Fibonacci numbers consecutively. For this purpose we are 
going to keep only the last two calculated elements of the sequence and use 
them to get the next element. Bellow you can see an implementation of the 
iterative Fibonacci numbers calculation algorithm: 
IterativeFibonacci.cs 
```csharp
using System; 
 
class IterativeFibonacci 
{ 
 static void Main() 
 { 
  Console.Write("n = "); 
  int n = int.Parse(Console.ReadLine()); 
 
  long result = Fib(n); 
  Console.WriteLine("fib({0}) = {1}", n, result); 
 } 
 
 static long Fib(int n) 
 { 
  long fn = 0; 
  long fnMinus1 = 1; 
  long fnMinus2 = 1; 
 
  for (int i = 2; i < n; i++) 
  { 
   fn = fnMinus1 + fnMinus2; 
 
   fnMinus2 = fnMinus1; 
   fnMinus1 = fn; 
  } 
 
  return fn; 
 } 
} 

This solution is as short and elegant, but does not hide risks of using 
```
recursion. Besides, it is efficient and does not require extra memory. 
Concluding the previous examples we can give you the next recommendation: 
 
Avoid recursion, unless you are certain about how it works 
and what has to happen behind the scenes . Recursion is a 
great and powerful weapon, with which you can easily shoot 
yourself in the leg. Use it carefully! 
If you follow this rule, you considerably will reduce the possibility of incorrect 
usage of recursion and the consequences, created by it.

## More about Recursion and Iteration

Generally, when we have a linear computational process , we do not have 
to use recursion, because iteration can be constructed easily and leads to 
simple and efficient calculations. An example of linear computational 
process is the calculation of  factorial. In it we calculate the elements of the 
sequence in which every next element depends only on the previous ones. 
What is distinctive about the linear computational processes is that on each 
step of the calculating recursion is called only once , only in one direction. 
Schematically, a linear computational process we can describe as follows: 
void Recursion(parameters) 
```csharp
{ 
 do some calculations; 
 Recursion(some parameters); 
 do some calculations; 
} 
```
In such a process, when we have only one recursive call in the body of the 
recursive method, it is not necessary to use recursion, because the iteration 
is obvious. 
Sometimes, however, we have a branched computational process  (like a 
tree). For example, th e imitation of N nested loops cannot be easily replaced 
with iteration. You have probably noticed that our iterative algorithm, which 
imitates nested loops, works in a completely different principle. Try to 
implement the same without recursion and you will see it is not easy. 
Ordinarily each recursion could boil down to iteration by using a stack of 
the calls (which is created through program execution), but this is 
complicated and there is no benefit from doing this.  Recursion has to be used 
when it provides simple, easy -to-understand and efficient solution to a 
problem, for which we have no obvious iterative solution. 
In tree-like (branched) computational processes  on each step of the 
recursion a couple of recursive calls are made and the scheme of calculations 


could be visualized as a tree (and not as a list like in linear calculations). For 
example, we saw what the tree of recursive calls would be like when we 
calculate the Fibonacci numbers. 
A typical scheme of a t ree computational process could be described with a 
pseudo-code in the following way: 
void Recursion(parameters) 
```csharp
{ 
 do some calculations; 
 Recursion(some parameters); 
```
 … 
 Recursion(some other parameters); 
 do some calculations; 
} 
Tree computational processes could not be directly boiled down to 
recursive (unlike the linear processes). The case of Fibonacci is simple, 
because each next number is calculated via the previous, which we can 
calculate in advance. Sometimes, however, each next number is cal culated 
not only via the previous, but via the next, and the recursive dependence is 
not so simple. In this case recursion turns out very efficient, if implemented 
correctly by avoiding duplicated calculations through memoization. 
 
Use recursion for branched recursive calculations (and 
ensure each value is calculated only once). For linear 
recursive calculations prefer using iteration. 
We are going to demonstrate the last statement with one classic example.

## Searching for Paths in a Labyrinth – Example

We are given a labyrinth with a rectangular shape, consisting of N*M 
squares. Each square is either passable or impassable. An adventurer enters 
the labyrinth from its top left corner (there is the entrance) and has to reach 
the bottom right corner of th e labyrinth (there is the exit). At each turn the 
adventurer can move up, down, left or right with one position and he has no 
right to go outside the binderies of the labyrinth, or step on impassable 
square. Passing through one and the same position is als o forbidden (it is 
considered that the adventurer is lost if after a several turns he goes back to 
a position he has already been). 
Write a computer program, which prints all possible paths  from the 
beginning of the labyrinth to the exit. 
This is a typical  example of a problem, which can be easily solved with 
recursion, while with iteration the solution will be more complex and harder to 
implement. 


Let’s first draw an example in order to illustrate the problem and think about 
finding a solution: 
s       
       
       
       
      e 
You can see that there are 3 different paths from the starting position to the 
end, which meets the requirements of the task (movement only on passable 
squares and not passing twice through any of the squares). Here you can see 
how these three paths look like: 
s 1 2     
  3     
6 5 4     
7       
8 9 10 11 12 13 14 
 
s 1 2  8 9 10 
  3  7  11 
  4 5 6  12 
      13 
      14 
 
s 1 2     
  3     
  4 5 6 7 8 
      9 
      10 
 
On the figure above with numbers from 1 to 14 are marked the numbers of 
the corresponding turns of the paths.

## Paths in a Labyrinth – Recursive Algorithm

How can we solve the problem? We can consider searching from a position in 
the labyrinth to the end of the labyrinth as a recursive process as follows: 
- Let the current position in the labyrinth be (row, col). In the beginn ing 
we go from the starting position (0, 0). 
- If the current position is the searched position (N -1, M-1), then we have 
found a path and we should print it. 
- If the current position is impassable, we go back (we have no right to 
step on it). 
- If the current position is already visited, we go back (we have no right 
to step on it twice. 
- Otherwise, we look for a path in four possible directions . We search 
recursively (with the same algorithm) a path to the exit from the 
labyrinth by trying to go in all possible directions: 
- We try left: position (row, col-1). 
- We try up: position (row-1, col). 
- We try right: position (row, col+1). 
- We try down: position (row+1, col). 

In order to reach this algorithmic solution we think recursively. We have the 
problem "searching for a path from given position to the exit". It can be boiled 
down to the following four sub problems: 
- searching for a path from the position on the left from the current 
position to the exit; 
- searching for a path from the position above the current position to the 
exit; 
- searching for a path from the position on the right from the current 
position to the exit; 
- searching for a path from the position below the current position to the 
exit. 
If from each possible position, which we reach, we check th e four possible 
directions and do not move in a circle (avoid passing through positions, on 
which we have already stepped on), we should find a path to the exit sooner 
or later (if such exists). 
This time the recursion is not as simple as in the previous p roblems. On each 
step we have to check whether we have reached the exit and whether we are 
on a forbidden position; after that we should mark the position as visited and 
recursively call searching in the four directions. After returning from the 
recursive calls we have to mark as unvisited the starting point. In informatics 
such crawl is known as searching with backtracking.

## Paths in a Labyrinth – Implementation

For the implementation of the algorithm we need to represent the labyrinth in 
a suitable way. We are going to use a two-dimensional array of characters, as 
in it we are going to mark with the character ' ' (space) the passable 
positions, with ' e' the exit from the labyrinth and with ' *' the impassable 
positions. The starting position is marked as pas sable position. The positions 
we have already visited we are going to mark with the character 's'. Here is 
how the definition of the labyrinth is going to look like for our example: 
```csharp
static char[,] lab = 
{ 
 {' ', ' ', ' ', '*', ' ', ' ', ' '}, 
 {'*', '*', ' ', '*', ' ', '*', ' '}, 
 {' ', ' ', ' ', ' ', ' ', ' ', ' '}, 
 {' ', '*', '*', '*', '*', '*', ' '}, 
 {' ', ' ', ' ', ' ', ' ', ' ', 'e'}, 
}; 
Let’s try to implement the recursive method for searching in a labyrinth. It 
```
should be something like this: 

```csharp
static char[,] lab = 
{ 
 {' ', ' ', ' ', '*', ' ', ' ', ' '}, 
 {'*', '*', ' ', '*', ' ', '*', ' '}, 
 {' ', ' ', ' ', ' ', ' ', ' ', ' '}, 
 {' ', '*', '*', '*', '*', '*', ' '}, 
 {' ', ' ', ' ', ' ', ' ', ' ', 'e'}, 
}; 
 
static void FindPath(int row, int col) 
{ 
 if ((col < 0) || (row < 0) || 
  (col >= lab.GetLength(1)) || (row >= lab.GetLength(0))) 
 { 
  // We are out of the labyrinth 
  return; 
 } 
 
 // Check if we have found the exit 
 if (lab[row, col] == 'e') 
 { 
  Console.WriteLine("Found the exit!"); 
 } 
 
 if (lab[row, col] != ' ') 
 { 
  // The current cell is not free 
  return; 
 } 
 
 // Mark the current cell as visited 
 lab[row, col] = 's'; 
 
 // Invoke recursion to explore all possible directions 
 FindPath(row, col - 1); // left 
 FindPath(row - 1, col); // up 
 FindPath(row, col + 1); // right 
 FindPath(row + 1, col); // down 
 
 // Mark back the current cell as free 
 lab[row, col] = ' '; 
} 
 
static void Main() 

{ 
 FindPath(0, 0); 
} 
```
The implementation strictly follows the description from the above. In this 
case the size of the labyrinth is not stored in variables N and M, but is derived 
from the two -dimensional array lab, which stores the labyrinth: the count of 
the columns is lab.GetLength(1), and the count of the rows is 
lab.GetLength(0). 
When entering the recursive method for searching, firstly we check if we go 
outside the labyrinth. In this case the searching is terminated, because going 
outside the boundaries of the labyrinth is forbidden. 
After that we check whether we have found the exit . If we have, we print 
an appropriate message and the searching from the current position onward is 
terminated. 
Next, we check if the current square is available. The square is available if 
the position is passable and we have not been on it on some of the previous 
steps (if it is not part of the current path from the starting position to the 
current cell of the labyrinth). 
If the cell is available, we step on it.  This is performed by marking it as 
visited (with the character 's'). After that we recursively search for a path in 
the four possible directions.  After returning from the recursive search of the 
four possible directions, we step back from the current cell and mark it as 
available. 
The marking back of the current position as available when leaving the 
current position is substantial because, when we go back, it is not a part of 
the current path. If we skip this action, not all paths to the exit would be 
found, but only some of them. 
This is how the recursive method for searching for the exit from the labyrinth 
looks like. We should now only call the method from the Main() method, 
beginning the search from the starting position (0, 0). 
If we run the program, we are going to see the following result: 
Found the exit! 
Found the exit! 
Found the exit! 
You can see that the exit has been found exactly three times. It seems that 
the algorithm works correctly. However, we are missing the printing of the 
path as a sequence of positions.

## Paths in a Labyrinth – Saving the Paths

In order to print the paths we have found by our recursive algorithm, we can 
use an array, in which at every step we keep the direction  taken (L – left, U – 
up, R – right, D – down). This array will keep in every moment the current 
path from the start of the labyrinth to the current position. 
We are going to need an array of characters  and a counter for the steps 
we have taken. The counter will keep how many times we have mov ed to the 
next position recursively, i.e. the current depth of recursion. 
In order to work correctly, our program has to increment the counter when 
entering recursion and save the direction we have taken in the position in the 
array. When returning from a recursion, the counter should be reduced by 1. 
When an exit I found, the path can be printed (it consists of all the characters 
in the array from 0 to the position pointed by the counter). 
What should be the size of the array ? The answer to this question i s easy; 
since we can enter one cell at most once, than the path would never be longer 
than the count of all cells (N*M). In our case the size of the maze is 7*5, i.e. 
the size of the array has to be 35. 
Note: if you know the List<T> data structure is might be more appropriate to 
use List<char> instead of the array of chars. We will learn about lists in the 
chapter "Linear Data Structures". 
This is an example implementation of the described idea: 
```csharp
static char[,] lab = 
{ 
 {' ', ' ', ' ', '*', ' ', ' ', ' '}, 
 {'*', '*', ' ', '*', ' ', '*', ' '}, 
 {' ', ' ', ' ', ' ', ' ', ' ', ' '}, 
 {' ', '*', '*', '*', '*', '*', ' '}, 
 {' ', ' ', ' ', ' ', ' ', ' ', 'e'}, 
}; 
 
static char[] path = 
 new char[lab.GetLength(0) * lab.GetLength(1)]; 
static int position = 0; 
 
static void FindPath(int row, int col, char direction) 
{ 
 if ((col < 0) || (row < 0) || 
  (col >= lab.GetLength(1)) || (row >= lab.GetLength(0))) 
 { 
  // We are out of the labyrinth 
  return; 
 } 

 
 // Append the direction to the path 
 path[position] = direction; 
 position++; 
 
 // Check if we have found the exit 
 if (lab[row, col] == 'e') 
 { 
  PrintPath(path, 1, position - 1); 
 } 
 
 if (lab[row, col] == ' ') 
 { 
  // The current cell is free. Mark it as visited 
  lab[row, col] = 's'; 
 
  // Invoke recursion to explore all possible directions 
  FindPath(row, col - 1, 'L'); // left 
  FindPath(row - 1, col, 'U'); // up 
  FindPath(row, col + 1, 'R'); // right 
  FindPath(row + 1, col, 'D'); // down 
 
  // Mark back the current cell as free 
  lab[row, col] = ' '; 
 } 
 
 // Remove the last direction from the path 
 position--; 
} 
 
static void PrintPath(char[] path, int startPos, int endPos) 
{ 
 Console.Write("Found path to the exit: "); 
 for (int pos = startPos; pos <= endPos; pos++) 
 { 
  Console.Write(path[pos]); 
 } 
 Console.WriteLine(); 
} 
 
static void Main() 
{ 
 FindPath(0, 0, 'S'); 
} 

To make it easier we added one more parameter to the recursive method for 
```
searching path to the exit of the labyrinth: the direction we have taken to in 
order to reach the current position. This parameter has no meaning when 
going from the starting position. For this reason in the beginning we put a 
meaningless value 'S'. After that, when printing, we skip the first element of 
the path. 
If we start the program, we are going to get the three possible paths from the 
beginning to the end of the labyrinth: 
Found path to the exit: RRDDLLDDRRRRRR 
Found path to the exit: RRDDRRUURRDDDD 
Found path to the exit: RRDDRRRRDD

## Paths in a Labyrinth – Testing the Program

It seems like the algorithm works properly. It remains to test it with some 
more examples in order to make sure we have not made a stupid mistake. We 
can test the program with an empty labyrinth with size 1x1, with an empty 
labyrinth with size 3x3, or for instance with a labyrinth in which there is no 
path to the exit, and in the end with an enormous labyrinth, where there are 
a lot of paths. 
If we run the tests, we are going to be convinced that in each case th e 
program is working correctly. 
Example input (labyrinth 1 x 1): 
```csharp
static char[,] lab = 
{ 
 {'e'}, 
}; 
```
Example output: 
Found path to the exit: 
You can see that the output is correct, but the path is empty (with length 0), 
because the starting position coincides with the exit. We could improve the 
visualization in this case (for example print " Empty path "). Example input 
(empty labyrinth 3x3): 
```csharp
static char[,] lab = 
{ 
 {' ', ' ', ' '}, 
 {' ', ' ', ' '}, 
 {' ', ' ', 'e'}, 
}; 

Example output for the above labyrinth: 
```
Found path to the exit: RRDLLDRR 
Found path to the exit: RRDLDR 
Found path to the exit: RRDD 
Found path to the exit: RDLDRR 
Found path to the exit: RDRD 
Found path to the exit: RDDR 
Found path to the exit: DRURDD 
Found path to the exit: DRRD 
Found path to the exit: DRDR 
Found path to the exit: DDRUURDD 
Found path to the exit: DDRURD 
Found path to the exit: DDRR 
You can check that the output is correct – these are all the paths to the exit. 
Let’s try another example input (labyrinth 5x3 without a path to the exit): 
```csharp
static char[,] lab = 
{ 
 {' ', '*', '*', ' ', ' '}, 
 {' ', ' ', ' ', '*', ' '}, 
 {'*', ' ', ' ', '*', 'e'}, 
}; 
```
Example output: 
(there is no output) 
You can see that the output is correct, but again we could add a more friendly 
message (for example "No exit!"), instead of any output. 
Now we have to check what would happen when we have an enormously big 
labyrinth. Here is a sample input (labyrinth with size 15x9): 
```csharp
static char[,] lab = 
{{' ','*',' ',' ',' ',' ','*',' ',' ',' ',' ','*','*',' ',' '}, 
 {' ',' ','*',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '}, 
 {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '}, 
 {' ',' ',' ',' ',' ',' ','*',' ',' ',' ',' ',' ',' ',' ',' '}, 
 {' ',' ',' ',' ',' ','*',' ',' ',' ',' ',' ',' ',' ',' ',' '}, 
 {' ',' ',' ',' ',' ','*',' ',' ',' ',' ',' ',' ',' ',' ',' '}, 
 {' ','*','*','*',' ','*',' ',' ',' ',' ',' ','*','*','*','*'}, 
 {' ',' ',' ',' ',' ','*',' ',' ',' ',' ',' ',' ',' ',' ',' '}, 
 {' ',' ',' ',' ',' ','*',' ',' ',' ',' ',' ',' ',' ',' ','e'}}; 

```
We run the program and it starts typing paths to the exit, but it does not 
end because there are too many paths . Here is how a small part of the 
output looks like: 
Found path to the exit: 
DRDLDRRURUURRDLDRRURURRRDLLDLDRRURRURRURDDLLDLLDLLLDRRDLDRDRRURDRR 
Found path to the exit: 
DRDLDRRURUURRDLDRRURURRRDLLDLDRRURRURRURDDLLDLLDLLLDRRDLDRDRRRURRD 
Found path to the exit: 
DRDLDRRURUURRDLDRRURURRRDLLDLDRRURRURRURDDLLDLLDLLLDRRDLDRDRRRURDR 
… 
Now, let’s try one last example – labyrinth with big size (15x9), in which there 
is no path to the exit: 
```csharp
static char[,] lab = 
{ 
 {' ','*',' ',' ',' ',' ','*',' ',' ',' ',' ','*','*',' ',' '}, 
 {' ',' ','*',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '}, 
 {' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' '}, 
 {' ',' ',' ',' ',' ',' ','*',' ',' ',' ',' ',' ',' ',' ',' '}, 
 {' ',' ',' ',' ',' ','*',' ',' ',' ',' ',' ',' ',' ',' ',' '}, 
 {' ',' ',' ',' ',' ','*',' ',' ',' ',' ',' ',' ',' ',' ',' '}, 
 {' ','*','*','*',' ','*',' ',' ',' ',' ',' ','*','*','*','*'}, 
 {' ',' ',' ',' ',' ','*',' ',' ',' ',' ',' ','*','*',' ',' '}, 
 {' ',' ',' ',' ',' ','*',' ',' ',' ',' ',' ',' ',' ','*','e'}, 
}; 
```
We run the program and it hangs, without printing anything . It actually 
works very long for us to wait for it. It seems like there is a problem. 
What is the problem? The problem is that the possible paths, analyzed by the 
algorithm are too many and their research takes too much time. Let’s think 
how many these paths a re. If a path to the exit is average 20 steps long and 
on each step there are 4 possible directions to be take, then 4 20 paths have to 
be researched, which is a very big number. This evaluation of the count of 
possibilities is very inaccurate, but it gives  orientation on the approximate 
order of possibilities. 
What is the conclusion? The backtracking method does not work, when the 
variants are too many, and the fact they are too many can be easily 
concluded. 
We are not going to torture you by making you fin d solution to the task. The 
problem of searching all paths in a labyrinth has no efficient solution 
for big labyrinths. 
The problem has an efficient solution if it is formulated in a slightly different 
way: find at least one exit from the labyrinth. This task is far easier and 

can be solved with one very small correction in the sample code: when 
escaping the recursion, we do not mark the current cell as available. This 
means to delete the following lines from the code: 
// Mark back the current cell as free 
lab[row, col] = ' '; 
We can convince ourselves that after this change the program finds out very 
quickly if there is no path to the exit, and if there is, it very quickly finds one 
of them. It is not the shortest or longest, just the first path found.