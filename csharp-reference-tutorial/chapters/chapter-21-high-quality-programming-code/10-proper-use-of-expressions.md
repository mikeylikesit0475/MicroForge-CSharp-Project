# Proper Use of Expressions

Proper Use of Expressions 
When using expressions, the simple rule is: avoid complex expressions!  A 
complex expression is one that performs more than one thing: 
for (int i = 0; i < xCoord.Length; i++) 
```csharp
{ 
 for (int j = 0; j < yCoord.Length; j++) 
 { 
  matrix[i][j] = 
```
   matrix[xCoord[FindMax(i) + 1]][yCoord[FindMin(i) + 1]] * 
   matrix[yCoord[FindMax(i) + 1]][xCoord[FindMin(i) + 1]]; 
 } 
} 
In the above sample we have a complex calculation, which  fills a given 
matrix based on a computation over some coordinates. It is i n fact very hard 
to understand what exactly is going on, because the used expressions are 
overly complex. 
There are many reasons to avoid the use of complex expressions as in the 
above example. Let’s mention a few: 
- Code becomes hard to read . Therefore, tra cing what is going on and 
whether the code is correct becomes hard, too. 
- Code is hard to maintain – think about the effort involved in fixing an 
error, in case the code does not work as expected. 
- Code is hard to fix in case of defects . If the above code th rows 
IndexOutOfRangeException, how would we know exactly which array 
has been involved? It could be xCoord or yCoord or matrix, occurrences 
of which are all scattered within the expressions. 


- Code is hard to debug. In case of an error, it would be much harder to 
debug a complex expression because it stays on a single line, and 
debuggers step through the code in terms of lines. 
All of these reasons suggest that writing complex expressions is harmful  
and should be av oided. Instead of a single complex expression, we can write 
a few less complex ones and save them in variables with descriptive names. 
In this way the code becomes simpler, easier to read and understand and 
easier to maintain, debug and fix. 
Let’s rewrite the above code without using complex expressions: 
for (int i = 0; i < xCoord.Length; i++) 
```csharp
{ 
 for (int j = 0; j < yCoord.Length; j++) 
 { 
  int maxStartIndex = FindMax(i) + 1; 
  int minStartIndex = FindMax(i) - 1; 
  int minXcoord = xCoord[minStartIndex]; 
  int maxXcoord = xCoord[maxStartIndex]; 
  int minYcoord = yCoord[minStartIndex]; 
  int maxYcoord = yCoord[maxStartIndex]; 
  matrix[i][j] = 
```
   matrix[maxXcoord][minYcoord] * 
   matrix[maxYcoord][minXcoord]; 
 } 
} 
Notice how simple and readable the code has become. Without knowing the 
exact calculation that this code is supposed to do, it is still hard to understand 
it, but at least we can debug it in case of an exception and find which line is 
causing it, and eventually fix it. 
 
Do not write complex expressions. Only one operation should 
be performed at one line, otherwise the code becomes hard 
to read, maintain, debug and modify.