# Graphs

The graphs are very useful and fairly common data structures. They are used 
to describe a wide variety of relationships between objects and in practice 
can be related to almost everything. As we will see later, trees are a subset o f 
the graphs and also lists are special cases of trees and thus of graphs , i.e. the 
graphs represent a generalized structure that allows modeling of very large 
set of real-world situations. 
Frequent use of graphs in practice has led to extensive research i n "graph 
theory", in which there is a large number of known problems for graphs and 
for most of them there are well-known solutions.

## Graphs – Basic Concepts

In this section we will introduce some of the important concepts and 
definitions. Some of them are similar to those in troduced about the tree data 
structure, but as we shall see, there are very serious differences, because 
trees are just special cases of graphs. 
Let’s consider the following sample graph (which we would later call a finite 
and oriented). Again, like with trees,  we have number ed the graph, as it is 
easier to talk about any of them specifically: 
 
The circles of this scheme we will call vertices (nodes)  and the arrows 
connecting them we will call directed edges. The vertex of which the arrow 
comes out we will call predecessor of that the arrow points. For example 
“19” is a predecessor of “1”. In this case, “1” is a successor of “19”. Unlike 
the structure tree, here each vertex can have more than one pr edecessor. 
Like “21”, it has three – “19”, “1” and “7”. If two of the vertices are connected 
with edge, then we say these two vertices are adjacent through this edge. 


Next follows the definition of finite directed graph. 
Finite directed graph is called the couple (V, E), in which V is a finite set of 
vertices and E is a finite set of directed edges. Each edge e that belongs to 
E is an ordered couple of vertices u and v or e = (u, v), which are defining it 
in a unique way. 
For better understanding of this de finition we are strongly recommending to 
the reader to think of the vertices as they are cities, and the directed edges 
as one-way roads. That way, if one of the vertices is Sofia and the other is 
Paris, the one-way path (edge) will be called Sofia – Paris. In fact this is one 
of the classic examples for the use of the graphs – in tasks with paths. 
If instead of arrows, the vertices are connected with segments, then the 
segments will be called undirected edges , and the graph – undirected. 
Practically we can imagine that an undirected edge from vertex A to vertex B 
is two-way edge and equivalent to two opposite directed edges between the 
same two vertices: 
 
Two vertices connected with an edge are called neighbors (adjacent). 
For the edges a weight function can be assigned, that associates each edge 
to a real number. These numbers we will call weights (costs). For examples 
of the weights we can mention some distance between neighboring cities, or 
the length of the directed connections between two neighboring c ities, or the 
crossing function of a pipe, etc. A graph that has weights on the edges is 
called weighted. Here is how it is illustrated a weighted graph. 
 
A B
A B
7
19
21
14
1
12
31
67
3
3
16
2
4
12 13
14
23
0

Path in a graph  is a sequence of vertices v1, v2, …, vn,, such as there is an 
edge from vi to v i+1 for every i from 1 to n -1. In our example path is the 
sequence "1", "12", "19", "21". "7", "21" and "1" is not a path because there 
is no edge starting from "21" and ending in "1". 
Length of path  is the number of edges connecting vertices in the sequence 
of the vertices in the path. This number is equal to the number of vertices in 
the path minus one. The length of our example for path "1", "12", "19", "21" 
is three. 
Cost of path in a weighted graph, we call the sum of the weights (costs) of 
the edges involv ed in the path. In real life the road from Sofia to Madrid, for 
example, is equal to the length of the road from Sofia to Paris plus the length 
of the road from Madrid to Paris. In our example, the length of the path "1", 
"12", "19" and "21" is equal to 3 + 16 + 2 = 21. 
Loop is a path in which the initial and the final vertex of the path match. 
Example of vertices forming loop are "1", "12" and "19".  In the same time  
"1", "7" and "21" do not form a loop. 
Looping edge we will call an edge, which starts and ends in the same vertex. 
In our example the vertex "14" is looped. 
A connected undirected graph we call an undirected graph in which there 
is a path from each node to each other. For example, the following graph is 
not connected because there is no path from "1" to "7". 
 
So we already have enough knowledge to define the concept tree in other 
way, as a special kind of graph: 
Tree – undirected connected graph without loops. 
As a small exercise we let the reader show why all definitions of tree we gave 
in this chapter are equivalent.

## Graphs – Presentations

There are a lot of different ways to present a graph in the computer 
programming. Different representation s have different properties and what 
exactly should be selected depends on the particular algorithm that we want 
to apply. In other words – we present the graph in a way, so that the 
1 2
13 7
31

operations that our algorithm does on it to be as fast as possible. Witho ut 
falling into greater details we will set out some of the most common 
representations of graphs. 
- List of successors  – in this representation for each vertex v a list of 
successor vertices is kept (like the tree’s child nodes). Here again, if the 
graph is  weighted, then to each element of the list of successors an 
additional field is added indicating the weight of the edge to it. 
- Adjacency matrix  – the graph is represented as a square matrix 
g[N][N], where if there is an edge from vi to vj, then the position 
g[i][j] is contains the value 1. If such an edge does not exist, the field 
g[i][j] is contains the value 0. If the graph is weighted, in the position 
g[i][j] we record weight of the edge, and matrix is called a matrix of 
weights. If between two nodes in this matrix there is no edge, then it is 
recorded a special value meaning infinity. If the graph is undirected, the 
adjacency matrix will be symmetrical. 
- List of the edges  – it is represented through the list of ordered pairs 
(vi, vj), where there is an edge from vi to vj. If the graph is weighted, 
instead ordered pair we have ordered triple, and its third element shows 
what the weight of the edge is. 
- Matrix of incidence between vertices and edges  – in this case, 
again we are using a matrix  but with dimensions g[M][N], where N is 
the number of vertices, and M is the number of edges. Each column 
represents one edge, and each row a vertex. Then the column 
corresponding to the edge (vi, v j) will contain 1  only at position i and 
position j, and other items in this column will contain 0. If the edge is a 
loop, i.e. is (vi, vi), then on position i we record 2. If the graph we want 
to represent is oriented and we want to introduce edge from vi to vj, 
then to position i we write 1 and to the position j we write -1. 
The most commonly used representation of graphs is the list of successors.

## Graphs – Basic Operations

The basic operations in a graph are: 
- Creating a graph 
- Adding / removing a vertex / edge 
- Check whether an edge exists between two vertices 
- Finding the successors of given vertex 
We will offer a sample implementation of the graph representation with 
a list of successors and we will show how to perform most of the 
operations. This kind of implementation is good when the most often 
operation we need is to get the list of all successors (child nodes) for a certain 
vertex. This graph representation needs a memory of order N +  M where N 
is the number of vertices and M is the number of edges in the graph. 

In essence the vertices are numbered from 0 to N-1 and our Graph class holds 
for each vertex a list of the numbers of all its child vertices. It does not work 
with the nodes, b ut with their numbers in the range [0...N -1]. Let’s explore 
the source code of our sample graph: 
```csharp
using System; 
using System.Collections.Generic; 
 
/// <summary>Represents a directed unweighted graph structure 
/// </summary> 
public class Graph 
{ 
 // Contains the child nodes for each vertex of the graph 
 // assuming that the vertices are numbered 0 ... Size-1 
 private List<int>[] childNodes; 
 
 /// <summary>Constructs an empty graph of given size</summary> 
 /// <param name="size">number of vertices</param> 
 public Graph(int size) 
 { 
  this.childNodes = new List<int>[size]; 
  for (int i = 0; i < size; i++) 
  { 
   // Assing an empty list of adjacents for each vertex 
   this.childNodes[i] = new List<int>(); 
  } 
 } 
 
 /// <summary>Constructs a graph by given list of 
 /// child nodes (successors) for each vertex</summary> 
 /// <param name="childNodes">children for each node</param> 
 public Graph(List<int>[] childNodes) 
 { 
  this.childNodes = childNodes; 
 } 
 
 /// <summary> 
 /// Returns the size of the graph (number of vertices) 
 /// </summary> 
 public int Size 
 { 
  get { return this.childNodes.Length; } 
 } 
 
 /// <summary>Adds new edge from u to v</summary> 

 /// <param name="u">the starting vertex</param> 
 /// <param name="v">the ending vertex</param> 
 public void AddEdge(int u, int v) 
 { 
  childNodes[u].Add(v); 
 } 
 
 /// <summary>Removes the edge from u to v if such exists 
 /// </summary> 
 /// <param name="u">the starting vertex</param> 
 /// <param name="v">the ending vertex</param> 
 public void RemoveEdge(int u, int v) 
 { 
  childNodes[u].Remove(v); 
 } 
 
 /// <summary> 
 /// Checks whether there is an edge between vertex u and v 
 /// </summary> 
 /// <param name="u">the starting vertex</param> 
 /// <param name="v">the ending vertex</param> 
 /// <returns>true if there is an edge between 
 /// vertex u and vertex v</returns> 
 public bool HasEdge(int u, int v) 
 { 
  bool hasEdge = childNodes[u].Contains(v); 
  return hasEdge; 
 } 
 
 /// <summary>Returns the successors of a given vertex 
 /// </summary> 
 /// <param name="v">the vertex</param> 
 /// <returns>list of all successors of vertex v</returns> 
 public IList<int> GetSuccessors(int v) 
 { 
  return childNodes[v]; 
 } 
} 
```
To illustrate how our graph data structure  works, we will create small 
program that creates a graph and traverses it by the DFS algorithm . To 
play a bit with graphs, the goal of our graph traversal algorithm will be to 
count how many connected components the graph has. 
By definition in undirected graph if a path exists between two nodes, they 
belong to the same connected component  and if no path exists  between 

two nodes, they belong to different connected components . For example 
consider the following undirected graph: 
 
It has 3 connected components: {0, 4}, {1, 2, 6, 3} and {5}. 
The code below creates a gra ph corresponding to the figure above and by 
DFS traversal finds all its connected components . This is straightforward: 
pass through all vertices and once unvisited vertex is found, all connected to 
it vertices (directly or indirectly via some a path) are f ound by DFS traversal, 
each of them is printed and marked as visited. Below is the code: 
```csharp
class GraphComponents 
{ 
 static Graph graph = new Graph(new List<int>[] { 
  new List<int>() {4},       // successors of vertice 0 
  new List<int>() {1, 2, 6}, // successors of vertice 1 
  new List<int>() {1, 6},    // successors of vertice 2 
  new List<int>() {6},       // successors of vertice 3 
  new List<int>() {0},       // successors of vertice 4 
  new List<int>() {},        // successors of vertice 5 
  new List<int>() {1, 2, 3}  // successors of vertice 6 
 }); 
 
 static bool[] visited = new bool[graph.Size]; 
 
 static void TraverseDFS(int v) 
 { 
  if (!visited[v]) 
  { 
   Console.Write(v + " "); 
   visited[v] = true; 
   foreach (int child in graph.GetSuccessors(v)) 
   { 
    TraverseDFS(child); 
   } 
  } 
 } 
 
 static void Main() 
 { 
```
2
63
0 4
1
5

```csharp
  Console.WriteLine("Connected graph components: "); 
  for (int v = 0; v < graph.Size; v++) 
  { 
   if (!visited[v]) 
   { 
    TraverseDFS(v); 
    Console.WriteLine(); 
   } 
  } 
 } 
} 
If we run the above code, we will get the following output (the connected 
```
components of our sample graph shown above): 
Connected graph components: 
0 4 
1 2 6 3 
5

## Common Graph Applications

Graphs are used to model many situations of reality, and tasks on graphs 
model multiple real problems that often need to be resolved. We will give just 
a few examples: 
- Map of a city  can be modeled by a weighted oriented graph . On 
each street, edge is compared with a length, corresponding to the length 
of the street, and direction – the direction of movement. If the street is 
a two-way, it can be compared to two edges in both directions. At each 
intersection there is a node. In  such a model there are natural tasks 
such as searching for the shortest path between two intersections, 
checking whether there is a road between two intersections, checking 
for a loop (if we can turn and go back to the starting position) searching 
for a path with a minimum number of turns, etc. 
- Computer network can be modeled by an undirected graph, whose 
vertices correspond to the computers in the network, and the edges 
correspond to the communication channels between the computers. To 
the edges different numbers can be compared, such as channel capacity 
or speed of the exchange, etc. Typical tasks for such models of a 
network are checking for connectivity between two computers, 
checking for double-connectivity between two points (existence of 
double-secured channel, which remains active after the failure of any 
computer), finding a minimal spanning tree  (MST), etc. In particular, 
the Internet can be modeled as a graph , in which are solved 

problems for routing packets, which are modeled as classical graph 
problems. 
- The river system in a given region can be modeled by a weighted 
directed graph, where each river is composed of one or more edges, 
and each node represents the place where two or more rivers flow into 
another one. On the edges can be set values, r elated to the amount of 
water that goes through them. Naturally with this model there are tasks 
such as calculating the volume of water, passing through each vertex 
and anticipate of possible flood in increasing quantities. 
You can see that the graphs can be used to solve many real-world 
problems. Hundreds of books and research papers are written about graphs, 
graph theory and graph algorithms . There are dozens of classic tasks for 
graphs, for which there are known solutions or it is known that there is no 
efficient solution. The scope of this chapter does not allow mentioning all of 
them, but we hope that through the short presentation we have awaken your 
interest in graphs, graph algorithms and their application s and spur you 
to take enough time to solve the tasks about graphs in the exercises.