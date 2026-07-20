# Multidimensional Arrays

The one-dimensional arrays  are known also as vectors in mathematics. 
Often we need arrays with more than one dimension. For example we can 
easily represent the standard chess board as a two -dimensional array with 
size 8 by 8 (8 cells in a horizontal direction and 8 cells in a vertical direction).

## What Is a Multidimensional Array? What Are Matrices?

What Is a Multidimensional Array? What Are 
Matrices? 
Every valid type in C# can be used for a type of an array. So, we can have an 
array of arrays, which we will discuss later. 
We declare a one-dimensional array of integer numbers using int[], and we 
declare a two-dimensional with int[,]. This example shows that: 
int[,] twoDimensionalArray; 

Those arrays we will call two-dimensional, because they have two 
dimensions. They are also known as matrices (it is mathematical term). In 
general arrays with more than one dimension we will call multidimensional. 
This way we can declare three -dimensional arrays as we add one more 
dimension: 
int[,,] threeDimensionalArray; 
In theory there is no limit for  an array dimensions , but in practice we do 
not use much arrays with more than two dimensions therefore we will focus 
on two-dimensional arrays.

## Multidimensional Array Declaration and Allocation

We declare multidimensional arrays in a way similar to one -dimensional 
arrays. Each dimension except the first is marked with comma: 
int[,] intMatrix; 
float[,] floatMatrix; 
string[,,] strCube; 
In the example above we create two-dimensional and three-dimensional 
arrays. Each dimension is represented by a comma in the square brackets []. 
We are allocating memory for multidimensional arrays by using the keyword 
new and for each dimension we set a length in the brackets as shown: 
int[,] intMatrix = new int[3, 4]; 
float[,] floatMatrix = new float[8, 2]; 
string[,,] stringCube = new string[5, 5, 5]; 
In this example intMatrix is a two-dimensional array with 3 elements of type 
int[] and each of those 3 elements has a length of 4. Two -dimensional 
arrays are difficult to understand explained that way. Therefore we c an 
imagine them as two-dimensional matrices, which have rows and columns 
for the dimensions: 
 
The rows and the columns of the square matrices are numbered with indices 
from 0 to n -1. If a two -dimensional array has a size of m by n, there are 
exactly m*n elements. 
1 3 6 2
8 5 9 1
4 7 3 0
0
1
0
2
1 2 3

## Two-Dimensional Array Initialization

We initialize two -dimensional arrays in the same way as we initialize one -
dimensional arrays. We can list the element values straight after the 
declaration: 
int[,] matrix = 
```csharp
{ 
 {1, 2, 3, 4}, // row 0 values 
 {5, 6, 7, 8}, // row 1 values 
}; 
// The matrix size is 2 x 4 (2 rows, 4 cols) 
```
In the example above we initialize a two -dimensional array  of type 
integer with size of 2 rows and 4 columns. In the outer brackets we place the 
elements of the first dimension, i.e. the rows of the array. Each row contains 
one dimensional array, which we know how to initialize.

## Accessing the Elements of a Multidimensional Array

Matrices have two dimensions and respectively we access each element by 
```csharp
using two indices: one for the rows and one for the columns. Multidimensional 
arrays have different indices for each dimension. 
 
```
Each dimension in a multidimensional array starts at index 0. 
Let’s examine the next example: 
int[,] matrix = 
```csharp
{ 
 {1, 2, 3, 4}, 
 {5, 6, 7, 8}, 
}; 
```
The array matrix has 8 elements, stored in 2 rows and 4 columns. Each 
element can be accessed in the following way: 
matrix[0, 0]  matrix[0, 1]  matrix[0, 2]  matrix[0, 3] 
matrix[1, 0]  matrix[1, 1]  matrix[1, 2]  matrix[1, 3] 
In this example we can access each element using indices. If we assign the 
index for rows to row, and the index for columns to col, then we can access 
any element as shown: 
matrix[row, col] 


When we use multidimensional arrays each element is unique and can be 
identified with indices from the array: 
nDimensionalArray[index1, …, indexN]

## Length of Multidimensional Arrays

Each dimension of a multidimensional array has its own length, which can be 
accessed during the execution of the program. Let’s look at an example for a 
two-dimensional array: 
int[,] matrix = 
```csharp
{ 
 {1, 2, 3, 4}, 
 {5, 6, 7, 8}, 
}; 
We can get the number of the rows of this two -dimensional array by using 
```
matrix.GetLength(0) and the number of all columns per row with 
matrix.GetLength(1). So, in this case matrix.GetLength(0) returns 2 and 
matrix.GetLength(1) returns 4.

## Printing Matrices – Example

In the next example we will demonstrate how we can print two -dimensional 
arrays to the console: 
// Declare and initialize a matrix of size 2 x 4 
int[,] matrix = 
```csharp
{ 
 {1, 2, 3, 4}, // row 0 values 
 {5, 6, 7, 8}, // row 1 value 
}; 
 
// Print the matrix on the console 
for (int row = 0; row < matrix.GetLength(0); row++) 
{ 
 for (int col = 0; col < matrix.GetLength(1); col++) 
 { 
  Console.Write(matrix[row, col]); 
 } 
 Console.WriteLine(); 
} 
```
First we declare and initialize an array, which we want to iterate through and 
print to the console. The array is two -dimensional, therefore we use a for-

loop which will iterate through the rows and a nested for loop which for each 
row will iterate through  the columns. At each iteration  we will print the 
current element using the appropriate method to access this element by using 
its two indices (row and column). Finally, if we execute this piece of code we 
will get the following result: 
1 2 3 4 
5 6 7 8

## Reading Matrices from the Console – Example

In this example we will learn how to read a two -dimensional array  from 
the console. First, we read the values (lengths) of the two -dimensions and 
then by using two nested loops we assign the value of each element ( and in 
the end we print out the values of the array): 
```csharp
Console.Write("Enter the number of the rows: "); 
int rows = int.Parse(Console.ReadLine()); 
 
Console.Write("Enter the number of the columns: "); 
int cols = int.Parse(Console.ReadLine()); 
 
int[,] matrix = new int[rows, cols]; 
 
Console.WriteLine("Enter the cells of the matrix:"); 
 
for (int row = 0; row < rows; row++) 
{ 
 for (int col = 0; col < cols; col++) 
 { 
  Console.Write("matrix[{0},{1}] = ",row, col); 
  matrix[row, col] = int.Parse(Console.ReadLine()); 
 } 
} 
 
for (int row = 0; row < matrix.GetLength(0); row++) 
{ 
 for (int col = 0; col < matrix.GetLength(1); col++) 
 { 
  Console.Write(" " + matrix[row, col]); 
 } 
 Console.WriteLine(); 
} 
```
The program output when we execute it (in this case the array consists of 
three rows and two columns) is: 

Enter the number of the rows: 3 
Enter the number of the columns: 2 
Enter the cells of the matrix: 
matrix[0,0] = 2 
matrix[0,1] = 3 
matrix[1,0] = 5 
matrix[1,1] = 10 
matrix[2,0] = 8 
matrix[2,1] = 9 
 2 3 
 5 10 
 8 9

## Maximal Platform in a Matrix – Example

In the next example we will solve another interesting problem: we are given a 
two-dimensional rectangular array (matrix) of integers and our task is to find 
the sub-matrix of size of 2 by 2 with maximum sum of its elements and 
to print it to the console. 
One solution to the problem might be the following: 
MaxPlatform2x2.cs 
```csharp
class MaxPlatform2x2 
{ 
 static void Main() 
 { 
  // Declare and initialize the matrix 
  int[,] matrix = { 
   { 0, 2, 4, 0, 9, 5 }, 
   { 7, 1, 3, 3, 2, 1 }, 
   { 1, 3, 9, 8, 5, 6 }, 
   { 4, 6, 7, 9, 1, 0 } 
  }; 
 
  // Find the maximal sum platform of size 2 x 2 
  long bestSum = long.MinValue; 
  int bestRow = 0; 
  int bestCol = 0; 
 
  for (int row = 0; row < matrix.GetLength(0) - 1; row++) 
  { 
   for (int col = 0; col < matrix.GetLength(1) - 1; col++) 
   { 

    long sum = matrix[row, col] + matrix[row, col + 1] + 
     matrix[row + 1, col] + matrix[row + 1, col + 1]; 
    if (sum > bestSum) 
    { 
     bestSum = sum; 
     bestRow = row; 
     bestCol = col; 
    } 
   } 
  } 
 
  // Print the result 
  Console.WriteLine("The best platform is:"); 
  Console.WriteLine("  {0} {1}", 
```
   matrix[bestRow, bestCol], 
   matrix[bestRow, bestCol + 1]); 
```csharp
  Console.WriteLine("  {0} {1}", 
```
   matrix[bestRow + 1, bestCol], 
   matrix[bestRow + 1, bestCol + 1]); 
```csharp
  Console.WriteLine("The maximal sum is: {0}", bestSum); 
 } 
} 
```
If we execute the program, we will see that it works properly: 
The best platform is: 
  9 8 
  7 9 
The maximal sum is: 33 
We will explain the algorithm. First we create a two -dimensional array, which 
contains integer numbers. We declare our auxiliary variables bestSum, 
bestRow, bestCol and we initialize bestSum with the minimal value of typ e 
long (so any other value is greater than this one).  Note that sum of 4 
integers may not fit in int, so we use long. 
In the variable bestSum we keep the current maximal sum and in bestRow 
and bestCol we keep the current best sub -matrix. This means the current 
row and current column describe the start element for the sub -matrix of size 
2 x 2, which is currently found to have the maximal sum of its elements. 
To access all elements of a sub-array with a size of 2 by 2 we need the indices 
of the first element. Having them we can easily access the rest 3 elements: 
matrix[row, col] 
matrix[row, col + 1] 

matrix[row + 1, col] 
matrix[row + 1, col + 1] 
In this example row and col are the indices of the first element of the sub -
matrix with a size of 2 by 2, which is part of the array matrix. 
After we know how to access all four elements of the matrix with a size of 2 
by 2, starting from a particular row and column, we can look at the algorithm, 
which we will use to find the maximal sub-matrix. 
We need to iterate through each  2 x 2 platform in the matrix until we reach 
the platform with the best sum. We will do this using two nested for-loops 
and two variables row and col. Note that we are not iterating through the 
entire matrix, because if we try to access index row + 1  or col + 1 , as we 
are at the last row or column we will go out of the range of the matrix, 
respectively System.IndexOutOfRangeException will be thrown. 
We access the neighbor elements of each current element of the sub -matrix 
and we sum them. Then we check if  our current sum is bigger than our 
current highest sum for the moment. If it is so, our current sum becomes our 
best sum and our current indices will update bestRow and bestCol. So, after 
the entire iteration through the main matrix we will find the maximal sum and 
the first element of the sub-matrix of size 2 by 2 and its indices. 
If there is more than one sub-matrix with the same maximal sum, we will find 
the one, which appears first. 
At the end of the example we are printing to the console the requested  sub-
matrix of size 2 x 2 and its sum of elements in an appropriate way.