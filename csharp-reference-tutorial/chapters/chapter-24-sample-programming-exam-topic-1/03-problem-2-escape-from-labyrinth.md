# Problem 2: Escape from Labyrinth

We are given a labyrinth, which consists of N x N squares  and each of it 
can be passable (0) or not (x). Our hero Jack is in one of the squares (*): 
x x x x x x 
0 x 0 0 0 x 
x * 0 x 0 x 
x x x x 0 x 
0 0 0 0 0 x 
0 x x x 0 x 
Two of the squares are neighboring, if they have a common wall. In one 
step Jack can pass from one passable square to its neighboring passable 
square. If Jack steps in a cell, which is on the edge of the labyrinth, he can go 
out from the labyrinth with one step. 
Write a program, which by a given labyrinth prints the minimal number of 
steps, which Jack needs, to go out from the labyrinth  or -1 if there is no 
way out. 
The input data is read from a text file named Problem2.in. On the first line of 
the file is the number N (2 < N < 100). On the each of next N lines there are 
N characters, each of them is either  "0" or "x" or " *". The output is one 
number and must be in the file Problem2.out. 
Sample input – Problem2.in: 
6 
xxxxxx 
0x000x 
x*0x0x 
xxxx0x 
00000x 
0xxx0x 
Sample output – Problem2.out: 
9

## Figure Out an Idea for a Solution

We have a labyrinth and we should find the shortest path in it. This is not 
an easy task and we should think a lot or we should read somewhere how to 
solve such kinds of tasks. 
Our algorithm will begin its movement from the initial point we are given. We 
know we can move to a neighboring cell horiz ontally or vertically, but not 
diagonally. Our algorithm must traverse the labyrinth in some way, to find the 
shortest path in it. How to traverse the cells in the labyrinth? 
One possible decision is the following: we start from the initial cell. Move to 
one of its neighboring cells, after this in a neighboring cell of the current 
(which is passable and still unvisited), after this in a neighboring cell of the 
last visited (which is passable and still unvisited) and we go on forward 
recursively until we rea ch an exit of the labyrinth, or we reach a place where 
we can’t continue (there is no neighboring cell which is free or unvisited). In 
this moment we go back from the recursion (to the previous cell) and visit 
another neighboring cell for the previous cell. If we can’t continue, we go back 
again. The described recursive process  is the process of traversing the 
labyrinth in depth (remember the chapter "Recursion" and DFS traversal). 
The question “Is it needed to walk through one cell more than once” occurs to 
us? If we walk through one cell at most once, we can walk through the whole 
labyrinth faster and if there is an exit, we will find it. But will this be the 
minimal pat h? If we dr aw the whole process on a paper, we will find out 
quickly the path will not be the minimal. 
If we mark the cell we leave on the way back of the recursion as free, this will 
allow us to reach each cell repeatedly, coming from a different path. The full 
recursive walk of the labyrinth will find all possible paths  from the 
initial cell to any other cell. From all the found paths we can choose the 
shortest path to a cell on the bound of the labyrinth (exit) and that ’s how we 
will find a solution for the problem.

### Verification of the Idea

It seems we have an idea for solving the problem: with recursive walk we 
find all the possible paths  in the labyrinth from the initial cell to a cell on 
the bounds of the labyrinth and from all these paths we choose the shortest 
one. Let’s check the idea. 
We take a sheet of paper and make one example of the labyrinth. We try the 
algorithm. It’s obvious it finds all the paths from the initial cell to the one of 
the exits and it travels a lot forwards -backwards. As a result it finds  all exits 
and among all paths it can be chosen the shortest one. 
Does the idea work if there is no exit ? We create a second labyrinth, which 
is without exit. We try out the algorithm on it, again on a sheet of paper. We 
see after long circulation forwards -backwards that the algorithm does not find 
an exit and finishes. 

It looks we have a correct idea for solving the problem. Let ’s move forward 
and think for the data structures.

## What Data Structures to Use?

First, we have to decide how to store the labyrinth. It’s natural to use a 
matrix of characters, just as the one on the figure. We will consider that one 
cell is passable and we can enter it, if it has a character, different from the 
character 'x'. We can store the labyrinth in a matrix of numbers and Boolean 
values, but the difference is not significant. The matrix of characters is 
comfortable for printing, and this will help us while debugging. There are not 
many options. We will store the labyrinth in a matrix of characters. 
After this, we have to decide in what structure to keep the visited  through 
the recursion (current path) cells. We always need the last visited cell. This 
leads us to a structure, which is “last in, first out”, i.e. stack. We can use 
Stack<Cell>, where Cell is a class, containing the coordinates of one cell 
(number of row and number of column). It remains to think where to keep 
the found paths, to find the shortest of them. If we think of it, it is not 
necessary to keep all the paths. It is enough to keep the current path and the 
shortest till this moment. It’s not even necessary to keep the shortest path till 
this moment but only its length. Every time we find a path to an exit of the 
labyrinth we can take its length and if i t is shorter than the shortest path to 
this moment to keep it. 
It seems we found efficient data structures. According to our recommen-
dations for problem solving, it is early to write the code of the program, 
because we have to think of the efficiency of the algorithm.

## Think About the Efficiency

Let’s check our idea against efficiency. What are we doing? We find all the 
possible paths and we take the shortest. There ’s no argument the algorithm 
will not work, but if the labyrinth is way bigger, will it work fast? 
To answer this question, we should think how much paths there are . If we 
take an empty labyrinth, on the each step of the recursion we will have an 
average number of 3 free cells to go (without the cell we are coming from). 
If we have for example a l abyrinth 10x10, the path could be 100 cells and 
while we travel on each step we will have 3 neighboring cells. It seems the 
numbers of paths are sort of 3 to the power of 100. It ’s obvious the 
algorithm will slow down the computer very much and very fast. 
We found a serious problem with the algorithm. It will work very slowly , 
even with small labyrinths, and with bigger ones it will not work at all! The 
good news is that we haven’t written a single line of code and the general 
change of our approach to the problem will not cost us much time.

## Think of Another Idea

We found that walking through all the paths in the labyrinth is wrong 
approach, so we have to think of another. 
Let’s start with the initial cell and walk through all its neighboring cells and 
mark them as visited. For each visited cell we can keep a number equal 
to the number of cells, which we have travelled to reach it  (the length 
of the minimal path from the initial cell to the current cell). 
For the initial cell the length of the path is 0. For its neighboring cells it should 
be 1, because we can reach them from the initial cell with one move. For the 
neighboring cells for the neighbors of the initial cell the length  of the path is 
2. We can continue this way and we will get to the following algorithm: 
1. Write the length of the path 0 for the initial cell. Mark it as visited. 
2. For each neighboring cell to the initial we mark the length of the path is 
1. Mark these cells as visited. 
3. For each cell, which is,  neighboring to a cell with length of the path 1 
and it is not visited, write the length of the path is 2. Mark the cells as 
visited. 
4. Continuing analogous, on the N step we find all the still unvisited cells, 
which are on a distance of N moves from the initial cell and mark them 
as visited.

### Check the New Idea

To check whether the new idea for solving the “Escape from the Labyrinth” 
problem is correct w e can visualize the process. We take another labyrinth  
to test our idea  in a better way. At each step k our goal is to fill with the 
number k all cells that can be reached in k steps. If at step 0 we fill the initial 
cell with 0, at step 1 we fill all cells reachable in 1 step from the initial cell, at 
step 2 we fill all cell s reachable in 2 steps, etc. we will be sure that when we 
fill a cell with a number, this number reflects the minimal number of steps 
to reach this cell starting from the initial cell, right? 
Step 0 – mark the distance from the initial cell to itself with 0 (mark the free 
cells with "-"): 
x x x x x x 
- x - - - x 
x 0 - x - x 
x - - x - x 
x - - - - x 
- x x x - x 
Step 1– mark with 1 all the neighbors to cells with a value of 0: 

x x x x x x 
- x - - - x 
x 0 1 x - x 
x 1 - x - x 
x - - - - x 
- x x x - x 
Step 2 – mark with 2 all the passable neighbors to cells with value 1: 
x x x x x x 
- x 2 - - x 
x 0 1 x - x 
x 1 2 x - x 
x 2 - - - x 
- x x x - x 
Step 3 – mark with 3 all passable neighbors to cells with value of 2: 
x x x x x x 
- x 2 3 - x 
x 0 1 x - x 
x 1 2 x - x 
x 2 3 - - x 
- x x x - x 
Continuing this way, in a moment either we will reach a cell at the edge of the 
labyrinth (an exit) or we will find such a cell is unreachable.  It seems like our 
algorithm works correctly. It will either find an exit or will find that there is 
no reachable exit.  If at some step an exit is found, the path to it will be 
guaranteed to be the shortest possible  (otherwise the exit should already 
be found at some of the earlier steps).

## Breaking the Problem into Subproblems

Having invented the idea for solving the labyrinth escaping problem, it will be 
easy to break it into subproblems. The main subproblems could be: reading 
the input  labyrinth, finding the shortest path  to some of its exits and 
printing the resul ts. The path finding subproblem  could be further 
divided into subproblems (steps) which we discussed in the previous section.

## Checking the Performance of the New Algorithm

Because we never visit a cell more than once, t he number of steps, which this 
algorithm does, should not be big. For example, if we have a labyrinth with 
size 100 x 100, it will have 10,000 cells, we will visit each of the cells at most 
once and for each of them we will check every neighbor if it is fr ee, i.e. we 
will check 4 times each cell. At the end we will do at most 40,000 checks and 
we will  visit at most 10 ,000 cells. We will do a total amount of 50 ,000 
operations. This means the algorithm will work instantly.

## Check If the New Algorithm Is Correct

It seems this time we don ’t have a problem with the performance. We have a 
fast algorithm. 
Let’s check if it  is correct. For this purpose we draw a bigger and more 
complex example on a sheet of paper, which has many exits and a lot of 
paths, and we begin to perform the algorithm. After this we try with a 
labyrinth with no exit. It seems the algorithm ends, but does not find an exit 
so it’s working. We try another 2 -3 examples and convince ourselves this 
algorithm always finds the shortest path to an exit and always work s 
fast, because it visits each of the cells of the labyrinth at most once.

## What Data Structures to Use?

With the new algorithm we walk consequently through all neighboring cells to 
the initial cell. We can put them into a data structure , for example in an 
array or better a list (or list of lists), because we can’t add in the array. 
Then we take the list of the reached cells on the last step  and we add 
their neighbors in another list. 
That’s how if we index the lists we have list0, which contains the initial cell, 
list1, which contains passable neighboring cells to the initial , after this 
list2, which contains passable neighbors to list 1 and so on. At the N step 
we have the listn, which contains all the cells, which we can reach in exactly 
N steps, i.e. which are at a distance of n from the initial cell. 
It seems we can use a list of lists, to keep the ce lls on each step. If we think 
about it, to get the n list, we need the ( n-1)-list. So it seems we don ’t need 
list of lists but only the list from the last step. 
We can make general conclusion: cells are processed in the order of entry: 
when the cells of step k are finished, then we process the cells from step k+1, 
and just after them – the cells from step k+2, and so on. The process seems 
like a queue: earlier acc essed cells are processed earlier . If we dig a bit 
inside, we will conclude, that we have just re-invented the Breadth-First-
Search algorithm (read about BFS in Wikipedia). 
To implement the BFS algorithm we can use a queue of cells . For this 
purpose we have to define class  Cell, which contains the coordinates of 

given cell (row and column). We can keep the distance from each cell to the 
initial cell in a matrix. If the distance is not calculated yet, we store -1. 
If we think a little more, the distance from the initial cell can be kept in the 
cell itself (in the class Cell) instead of creating a special matrix for the 
distances. That way we will save memory. 
Now we are clear about the data str uctures. Now we have to implement the 
algorithm step by step.

## Step 1 – The Class Cell

We can begin with the definition of the Cell class. We need it to save the 
initial cell, from which begins the searching of the path. We will use auto -
implemented properties to make the code shorter and more readable. Here is 
the Cell class: 
```csharp
public class Cell 
{ 
 public int Row { get; set; } 
 public int Column { get; set; } 
 public int Distance { get; set; } 
} 
```
We can add a constructor to simplify the way we use this class: 
```csharp
public Cell(int row, int column, int distance) 
{ 
 this.Row = row; 
 this.Column = column; 
 this.Distance = distance; 
} 
```
Generally it is a good idea to test the code after each step , but the above 
code is too simple to be tested. We will test is later as part of some more 
complex piece of code.

## Step 2 – Reading the Input File

We will read the input file line by line using the well -known class 
StreamReader. On the each of the lines we will analyze the characters and we 
will write them in a matrix of characters. When we reach the character "*" we 
will keep its coordinates in an instance of class Cell to know where to start 
the searching of the shortest path for getting out of the labyrinth. 
We can define a class Maze and keep the matrix of the labyrinth and t he 
initial cell in it: 

Maze.cs 
```csharp
public class Maze 
{ 
 private char[,] maze; 
 private int size; 
 private Cell startCell = null; 
 
 public void ReadFromFile(string fileName) 
 { 
  using (StreamReader reader = new StreamReader(fileName)) 
  { 
   // Read the maze size and create the maze 
   this.size = int.Parse(reader.ReadLine()); 
   this.maze = new char[this.size, this.size]; 
 
   // Read the maze cells from the file 
   for (int row = 0; row < this.size; row++) 
   { 
    string line = reader.ReadLine(); 
    for (int col = 0; col < this.size; col++) 
    { 
     this.maze[row, col] = line[col]; 
     if (line[col] == '*') 
     { 
      this.startCell = new Cell(row, col, 0); 
     } 
    } 
   } 
  } 
 } 
} 
For simplicity we will skip processing the errors while reading and writing in a 
```
file. When an exception occurs we will skip to catch it in the main method 
and thus we will leave the CLR to print it on the console.

### Testing the Input File Reading Code

We already have the class Maze and appropriate representation of data of the 
input file. To be sure the written so far is correct we should test . We can 
check if the matrix is truly filled as we print it on the console. The other 
possibility is to view the values of the fields in the class Maze through the 
debugger of Visual Studio.  We add a Main() method which invokes the maze 
reading method and we test it: 

```csharp
static void Main() 
{ 
 Maze maze = new();
 maze.ReadFromFile("Problem2.in"); 
} 
Through the Visual Studio debugger we get convinced that the input file is 
```
correctly read from the input file:

## Step 3 – Finding the Shortest Path

We can implement the algorithm  directly from what we already discussed. 
We must define a queue and put in it s beginning the initial cell. Afterwards we 
must take the cell in turn from the queue and add all of its passable unvisited 
neighbors in a loop. At each step there is a chance to enter in a cell, which is 
at the border of the labyrinth,  and we see we have found an exit and the 
searching ends. We repeat the loop until the queue is empty. At each 
visitation of a given cell we check if it is free and if it is, we mark it as 
impassable. This way we avoid repeatedly visiting the same cell. 
Here is how the implementation of the algorithm looks like: 
```csharp
public int FindShortestPath() 
{ 
 // Queue for traversing the cells in the maze 
 Queue<Cell> visitedCells = new();
```
 VisitCell(visitedCells, this.startCell.Row, 
  this.startCell.Column, 0); 
 
 // Perform Breadth-First Search (BFS) 
 while (visitedCells.Count > 0) 
```csharp
 { 
  Cell currentCell = visitedCells.Dequeue(); 
  int row = currentCell.Row; 


  int column = currentCell.Column; 
  int distance = currentCell.Distance; 
  if ((row == 0) || (row == size - 1) 
   || (column == 0) || (column == size - 1)) 
  { 
   // We are at the maze border 
   return distance + 1; 
  } 
  VisitCell(visitedCells, row, column + 1, distance + 1); 
  VisitCell(visitedCells, row, column - 1, distance + 1); 
  VisitCell(visitedCells, row + 1, column, distance + 1); 
  VisitCell(visitedCells, row - 1, column, distance + 1); 
 } 
 
 // We didn't reach any cell at the maze border -> no path 
 return -1; 
} 
 
private void VisitCell(Queue<Cell> visitedCells, 
 int row, int column, int distance) 
{ 
 if (this.maze[row, column] != 'x') 
 { 
  // The cell is free --> visit it 
  maze[row, column] = 'x'; 
  Cell cell = new(row, column, distance);
  visitedCells.Enqueue(cell); 
 } 
}
```

### Checking after Step 3

Before the next step, we must test, to check our algorithm . We must try 
the normal case and the border cases, when there is no exit, when we step 
on an exit, when the input file doesn ’t exist or the square matrix is with size 
of 0. Only then can we start doing the next step.  Let’s start with testing the 
normal (typical) case. We create the following code to quickly test it: 
```csharp
static void Main() 
{ 
 Maze maze = new();
 maze.ReadFromFile("Problem2.in"); 
 Console.WriteLine(maze.FindShortestPath()); 
} 

```
We run the above code over the sample input file from the problem 
description and it works . The code correctly returns the length of the 
shortest path to the nearest exit: 
9 
Now let’s test the border cases, e.g. a labyrinth of size 0 . Unfortunately 
we get the following result: 
Unhandled Exception: System.NullReferenceException: Object 
reference not set to an instance of an object. 
 at Maze.FindShortestPath() 
We’ve made a mistake. The problem is when the variable, in which we keep 
the initial cell, is initialized with null. This can happen in many scenarios. If 
the labyrinth has no cells ( e.g. size of 0) or the initial cell is missing, the 
result that the program should return is -1, but not an exception. 
To fix the bug  we just found  we can add a check in the beginning of the 
FindShortestPath() method: 
```csharp
public int FindShortestPath() 
{ 
 if (this.startCell == null) 
 { 
  // Start cell is missing -> no path 
  return -1; 
 } 
```
 … 
We retest the code  with the typical and the border cases . After the fix  it 
seems the algorithm works correctly now.

## Step 4 – Writing the Result to a File

It remains to write the result of the FindShortestPath() to the output file. 
This is a trivial problem: 
```csharp
public void SaveResult(String fileName, int result) 
{ 
 using (StreamWriter writer = new StreamWriter(fileName)) 
 { 
  writer.WriteLine("The shortest way is: " + result); 
 } 
} 
```
Here is how the complete source code of the solution looks: 

Maze.cs 
```csharp
using System; 
using System.IO; 
using System.Collections.Generic; 
 
public class Maze 
{ 
 private const string InputFileName = "Problem2.in"; 
 private const string OutputFileName = "Problem2.out"; 
 
 public class Cell 
 { 
  public int Row { get; set; } 
  public int Column { get; set; } 
  public int Distance { get; set; } 
 
  public Cell(int row, int column, int distance) 
  { 
   this.Row = row; 
   this.Column = column; 
   this.Distance = distance; 
  } 
 } 
 
 private char[,] maze; 
 private int size; 
 private Cell startCell = null; 
 
 public void ReadFromFile(string fileName) 
 { 
  using (StreamReader reader = new StreamReader(fileName)) 
  { 
   // Read maze size and create maze 
   this.size = int.Parse(reader.ReadLine()); 
   this.maze = new char[this.size, this.size]; 
 
   // Read the maze cells from the file 
   for (int row = 0; row < this.size; row++) 
   { 
    string line = reader.ReadLine(); 
    for (int col = 0; col < this.size; col++) 
    { 
     this.maze[row, col] = line[col]; 

     if (line[col] == '*') 
     { 
      this.startCell = new Cell(row, col, 0); 
     } 
    } 
   } 
  } 
 } 
 
 public int FindShortestPath() 
 { 
  if (this.startCell == null) 
  { 
   // Start cell is missing -> no path 
   return -1; 
  } 
 
  // Queue for traversing the cells in the maze 
  Queue<Cell> visitedCells = new();
```
  VisitCell(visitedCells, this.startCell.Row, 
   this.startCell.Column, 0); 
 
  // Perform Breadth-First Search (BFS) 
  while (visitedCells.Count > 0) 
```csharp
  { 
   Cell currentCell = visitedCells.Dequeue(); 
   int row = currentCell.Row; 
   int column = currentCell.Column; 
   int distance = currentCell.Distance; 
   if ((row == 0) || (row == size - 1) 
    || (column == 0) || (column == size - 1)) 
   { 
    // We are at the maze border 
    return distance + 1; 
   } 
 
   VisitCell(visitedCells, row, column + 1, distance + 1); 
   VisitCell(visitedCells, row, column - 1, distance + 1); 
   VisitCell(visitedCells, row + 1, column, distance + 1); 
   VisitCell(visitedCells, row - 1, column, distance + 1); 
  } 
 
  // We didn't reach any cell at the maze border -> no path 
  return -1; 

 } 
 
 private void VisitCell(Queue<Cell> visitedCells, 
  int row, int column, int distance) 
 { 
  if (this.maze[row, column] != 'x') 
  { 
   // The cell is free --> visit it 
   maze[row, column] = 'x'; 
   Cell cell = new(row, column, distance);
   visitedCells.Enqueue(cell); 
  } 
 } 
 
 public void SaveResult(string fileName, int result) 
 { 
  using (StreamWriter writer = new StreamWriter(fileName)) 
  { 
   writer.WriteLine(result); 
  } 
 } 
 
 static void Main() 
 { 
  Maze maze = new();
  maze.ReadFromFile(InputFileName); 
  int pathLength = maze.FindShortestPath(); 
  maze.SaveResult(OutputFileName, pathLength); 
 } 
}
```

## Testing the Complete Solution of the Problem

After we have a solution of the problem we must test it . We have already 
tested the typical case and the border cases (like missing exit or when 
the initial position stays at the labyrinth edge ). We will execute these 
tests again to get convinced that the algorithm behaves correctly: 
Input Output Input Output Input Output Input Output 
0 -1 2 
00 
xx 
-1 3 
0x0 
x*x 
0x0 
-1 3 
000 
000 
00* 
1 

The algorithm works correctly. The output for each of the test is correct. 
It remains to test with a large labyrinth  (performance test) , for example  
1000 x 1000. We can make such a labyrinth very easy – with copy / paste. 
We perform the test and we convince ourselves the program is working 
correctly for the big test and works extremely fast – there is no delay. 
While testing we should try every way to break our solution . We run a 
few more difficult examples  (for example a labyrinth with passable cells in 
the form of spiral). We can put large labyrinth with a lot of paths, but without 
exit. We can try whatever else we wish. 
At the end we make sure, that we have a correct solution  and we pass to 
the next problem from the exam.