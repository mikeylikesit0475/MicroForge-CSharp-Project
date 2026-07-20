# Algorithm Complexity

We cannot talk about efficiency of algorithms and data structures  
without explaining the term "algorithm complexity", which we have already 
mentioned several times in one form or another. We will avoid the 
mathematical definitions and we are going to give a simple explanation of 
what the term means. 
Algorithm complexity is a measure which evaluates the order of the count 
of operations, performed by a given or algorithm as a function of the size of 
the input data. To put this simpler, complexity is a rough approximation of 
the number of steps necessary to execute an algorithm. When we evaluate 
complexity we speak of order of operation count, not of their exact count. For 
example if we have an order of N 2 operations to process N elements, then 
N2/2 and 3*N2 are of one and the same quadratic order. 
Algorithm complexity is commonly represented with the O(f) notation, also 
known as asymptotic notation or “Big O notation”, where f is the function 
of the size of the input data.  The asymptotic computational complexity O(f) 
measures the order of the consumed res ources (CPU time, memory, etc.) by 
certain algorithm expressed as function of the input data size. 
Complexity can be constant, logarithmic, linear, n*log(n), quadratic, 
cubic, exponential, etc. This is r espectively the order of constant, 
logarithmic, linear and so on, number of steps, are executed to solve a given 
problem. For simplicity, sometime instead of “ algorithms complexity ” or 
just “complexity” we use the term “running time”. 
 
Algorithm complexity is a rough approximation of the 
number of steps, which will be executed depending on the 
size of the input data. Complexity gives the order of steps 
count, not their exact count.

## Typical Algorithm Complexities

This table will explain what every type of complexity (running time) means: 
Complexity Running Time Description 
constant O(1) 
It takes a constant number of steps for 
performing a given operation (for example 
1, 5, 10 or other number) and this count 
does not depend on the size of the input 
data. 
logarithmic O(log(N)) 
It takes the order of log(N) steps, where 
the base of the logarithm is most often 2, 
for performing a given operation on N 
elements. For example, if N = 1,000,000, 
an algorithm with a complexity O(log(N)) 


would do about 20 steps (with a constant 
precision). Since the base of the logarithm 
is not of a vital importance for the order of 
the operation count, it is usually omitted. 
linear O(N) 
It takes nearly the same amount of 
steps as the number of elements for 
performing an operation on N elements. 
For example, if we have 1,000 elements, it 
takes about 1,000 steps. Linear 
complexity means that the number of 
elements and the number of steps are 
linearly dependent, for example the 
number of steps for N elements can be 
N/2 or 3*N. 
 O(n*log(n)) 
It takes N*log(N) steps for performing a 
given operation on N elements. For 
example, if you have 1,000 elements, it 
will take about 10,000 steps. 
quadratic O(n2) 
It takes the order of N2 number of steps, 
where the N is the size of the input data, 
for performing a given operation. For 
example if N = 100, it takes about 10,000 
steps. Actually we have a quadratic 
complexity when the number of steps is in 
quadratic relation with the size of the 
input data. For example for N elements 
the steps can be of the order of 3*N2/2. 
cubic O(n3) 
It takes the order of N3 steps, where N is 
the size of the input data, for performing 
an operation on N elements. For example, 
if we have 100 elements, it takes about 
1,000,000 steps. 
exponential O(2n), O(N!), 
O(nk), … 
It takes a number of steps, which is with 
an exponential dependability with the 
size of the input data, to perform an 
operation on N elements. For example, if 
N = 10, the exponential function 2N has a 
value of 1024, if N = 20, it has a value of 
1 048 576, and if N = 100, it has a value 
of a number with about 30 digits. The 
exponential function N! grows even faster: 
for N = 5 it has a value of 120, for N = 10 
it has a value of 3,628,800 and for N = 20 
– 2,432,90,008,176,640,000. 

When evaluating complexity, constants are not taken into account , 
because they do not significantly affect t he count of operations. Therefore an 
algorithm which does N steps and algorithms which do N/2 or 3*N 
respectively are considered linear and approximately equally efficient, 
because they perform a number of operations which is of the same order.

## Complexity and Execution Time

The execution speed  of a program depends on the complexity of the 
algorithm, which is executed. If this complexity is low, the program will 
execute fast even for a big number of elements. If the complexity is high, the 
program will execu te slow ly or will not even work (it will hang) for a big 
number of elements. 
If we take an average computer from 2008, we can assume that it can 
perform about 50,000,000 elementary operations per second . This 
number is a rough approximation, of course. The  different processors work 
with a different speed and the different elementary operations are performed 
with a different speed, and also the computer technology constantly evolves. 
Still, if we accept we use an average home computer from 2008, we can make 
the following conclusions about the speed of execution  of a given program 
depending on the algorithm complexity and size of the input data. 
Algorithm 10 20 50 100 1,000 10,000 100,000 
O(1) < 1 
sec. 
< 1 
sec. 
< 1 
sec. 
< 1 
sec. 
< 1 
sec. < 1 sec. < 1 sec. 
O(log(n)) < 1 
sec. 
< 1 
sec. 
< 1 
sec. 
< 1 
sec. 
< 1 
sec. < 1 sec. < 1 sec. 
O(n) < 1 
sec. 
< 1 
sec. 
< 1 
sec. 
< 1 
sec. 
< 1 
sec. < 1 sec. < 1 sec. 
O(n*log(n)) < 1 
sec. 
< 1 
sec. 
< 1 
sec. 
< 1 
sec. 
< 1 
sec. < 1 sec. < 1 sec. 
O(n2) < 1 
sec. 
< 1 
sec. 
< 1 
sec. 
< 1 
sec. 
< 1 
sec. 2 sec. 3-4 min. 
O(n3) < 1 
sec. 
< 1 
sec. 
< 1 
sec. 
< 1 
sec. 
20 
sec. 
5.55 
hours 
231.5 
days 
O(2n) < 1 
sec. 
< 1 
sec. 
260 
days hangs hangs hangs hangs 
O(n!) < 1 
sec. hangs hangs hangs hangs hangs hangs 
O(nn) 3-4 
min. hangs hangs hangs hangs hangs hangs 
We can draw many conclusions from the above table: 

- Algorithms with a constant, logarithmic or linear complexity are so 
fast that we cannot feel any delay, even with a relatively big size of the 
input data. 
- Complexity O(n*log(n)) is similar to the linear and works nearly as 
fast as linear, so it will be very difficult to feel any delay. 
- Quadratic algorithms work very well up to several thousand elements. 
- Cubic algorithms work well if the elements are not more than 1,000. 
- Generally these so calle d polynomial algorithms  (any, which are not 
exponential) are considered to be fast and working well for thousands of 
elements. 
- Generally the exponential algorithms do not work well  and we 
should avoid them (when possible). If we have an exponential solutio n 
to a task, maybe we actually do not have a solution, because it will work 
only if the number of the elements is below 10 -20. Modern cryptography 
is based exactly on this – there are not any fast (no n-exponential) 
algorithms for finding the secret keys used for data encryption. 
 
If you solve a given problem with an exponential complexity 
this means that you have solved it for a small amount of 
input data and generally your solution does not work. 
The data in the table is just for orientation, of course. Sometimes a linear 
algorithm could work slower than a quadratic one or a cubic algorithm 
could work faster than O(n*log(n)). The reasons for this could be many: 
- It is possible the constants in an algorithm with a low complexity to be 
big and this could eve ntually make the algorithm slow. For example, if 
we have an algorithm, which makes 50*n steps  and another one, 
which makes 1/100*n*n steps, for elements up to 5000 the quadratic 
algorithm will be faster than the linear. 
- Since the complexity evaluation is m ade in the worst case scenario, it 
is possible a quadratic algorithm to work better than O(n*log(n)) in 99% 
of the cases. We can give an example with the algorithm QuickSort 
(the standard sorting algorithm in .NET Framework), which in the 
average case works a bit better than MergeSort, but in the worst case 
QuickSort can make the order of n2 steps, while MergeSort does 
always O(n*log(n)) steps. 
- It is possible an algorithm, which is evaluated to execute with a linear 
complexity, to not work so fast, bec ause of an inaccurate complexity 
evaluation. For example if we search for a given word in an array  of 
words, the complexity is linear, but at every step string comparison 
is performed, which is not an elementary operation and can take much 
more time than p erforming simple elementary operation (for example 
comparison of two integers).

## Complexity by Several Variables

Complexity can depend on several input variables at once. For example, if we 
look for an element in a rectangular matrix with sizes M and N , the  
searching speed depends on M and N. Since in the worst case we have to 
traverse the entire matrix, we will do M*N number of steps at most. Therefore 
the complexity is O(M*N).

## Best, Worst and Average Case

Complexity of algorithms is usually evaluated in th e worst case  (most 
unfavorable scenario). This means in the average case they can work faster, 
but in the worst case they work with the evaluated complexity and not slower.  
Let’s take an example: searching in array . To find the searched key in the 
worst case, we have to check all the elements in the array. In the best case 
we will have luck and we will find the element at first position. In the average 
case we can expect to check half the elements in the array until we find the 
one we are looking for. Hence  in the worst case the complexity is O(N) – 
linear. In the average case the complexity is O(N/2) = O(N) – linear, because 
when evaluating complexity one does not take into account the constants. In 
the best case we have a constant complexity O(1), because we make only one 
step and directly find the element.

## Roughly Estimated Complexity

Sometimes it is hard to evaluate the exact complexity  of a given 
algorithm, because it performs operations and it is not known  exactly how 
much time they will take and how ma ny operations will be done  internally. 
Let’s take the example of searching a given word in an array of strings  
(texts). The task is easy: we have to traverse the array and search in every 
text with Substring() or with a regular expression for the given word. We 
can ask ourselves the question: if we had 10 ,000 texts, would this work fast? 
What if the texts were 100 ,000? If we carefully think about it, we will 
implement that in order to evaluate adequately, we ha ve to know how big 
are the texts , because there is a difference between searching in people ’s 
names (which are up to 50-100 characters) and searching in scientific articles 
(which are roughly composed by 20 ,000 – 30,000 characters). However, we 
can evaluate the complexity using the length of the texts, through which we 
are searching: it is at least O(L), where L is the sum of the lengths of all 
texts. This is a pretty rough evaluation, but it is much more accurate than 
complexity O(N), where N is the number  of the texts, right? We should think 
whether we take into account all situations, which could occur. Does it matter 
how long the searched word is? Probably searching of long words is slower 
than searching of short words. In fact things are slightly differ ent. If we 
search for "aaaaaaa" in the text "aaaaaabaaaaacaaaaaabaaaaacaaaaab", this 
will be slower than if we search for "xxx" in the same text, because in the first 
case we will get more sequential matches than in the second case. Therefore, 

in some spec ial situations, searching seriously depends on the length of the 
word we search and the complexity O(L) could be underestimated.

## Complexity by Memory

Besides the number of steps using a function of the input data, one can 
measure other resources , which an algorithm uses, for example memory, 
count of disk operations, etc. For some algorithms the execution speed is not 
as important as the memory they use . For example if a given algorithm is 
linear but it uses RAM in the order of N 2, it will be probably short age of 
memory if N = 100 ,000 (then it will need memory in  order of 9 GB RAM), 
despite the fact that it should work very fast.

## Estimating Complexity – Examples

We are going to give several examples, which show how you can estimate 
the complexity of your algo rithms, and decide whether the code written by 
you will work fast: 
If we have a single loop from 1 to N, its complexity is linear – O(N): 
int FindMaxElement(int[] array) 
```csharp
{ 
 int max = int.MinValue; 
 for (int i = 1; i < array.Length; i++) 
 { 
  if (array[i] > max) 
  { 
   max = array[i]; 
  } 
 } 
 return max; 
} 
```
This code will work well even if the number of elements is huge. 
If we have two of nested loops from 1 to N , their complexity is quadratic 
– O(N2). Example: 
int FindInversions(int[] array) 
```csharp
{ 
 int inversions = 0; 
 for (int i = 0; i < array.Length - 1; i++) 
 { 
  for (int j = i + 1; j < array.Length; j++) 
  { 
   if (array[i] > array[j]) 
   { 

    inversions++; 
   } 
  } 
 } 
 return inversions; 
} 
```
If the elements are no more than several thousand or tens  of thousands, this 
code will work well. 
If we have tree nested loops from 1 to N , their complexity is cubic – 
O(N3). Example: 
long Sum3(int n) 
```csharp
{ 
 long sum = 0; 
 for (int a = 1; a < n; a++) 
 { 
  for (int b = 1; b < n; b++) 
  { 
   for (int c = 1; c < n; c++) 
   { 
    sum += a * b * c; 
   } 
  } 
 } 
 return sum; 
} 
```
This code will work well if the number of elements is below 1,000. 
If we have two nested loops from 1 to N and from 1 to M  respectively, 
their complexity will be quadratic – O(N*M). Example: 
long SumMN(int n, int m) 
```csharp
{ 
 long sum = 0; 
 for (int x = 1; x <= n; x++) 
 { 
  for (int y = 1; y <= m; y++) 
  { 
   sum += x * y; 
  } 
 } 
 return sum; 
} 

The speed of this code depends on two variables. The code will work well if 
```
M, N < 10,000 or if at least the one variable has a value small enough. 
We should pay attention to the fact that not always tree nested loops mean 
cubic complexity. Here is an example in which the complexity is O(N*M): 
long SumMN(int n, int m) 
```csharp
{ 
 long sum = 0; 
 for (int x = 1; x <= n; x++) 
 { 
  for (int y = 1; y <= m; y++) 
  { 
   if (x == y) 
   { 
    for (int i = 1; i <= n; i++) 
    { 
     sum += i * x * y; 
    } 
   } 
  } 
 } 
 return sum; 
} 
```
In this example the most inner loop executes exactly min(M, N) times and 
does not significantly affect  the algorithm speed. The outer code executes 
approximately N*M + min(M,N)*N steps, i.e. its complexity is quadratic. 
When using a recursion, the complexity is more difficult to be 
estimated. Here is an example: 
long Factorial(int n) 
```csharp
{ 
 if (n == 0) 
 { 
  return 1; 
 } 
 else 
 { 
  return n * Factorial(n - 1); 
 } 
} 
```
In this example the complexity is obviously linear – O(N), because the 
function Factorial() executes exactly once for each of the numbers 1 … n. 

Here is a recursive function for which it is very difficult to estimate  the 
complexity: 
long Fibonacci(int n) 
```csharp
{ 
 if (n == 0) 
 { 
  return 1; 
 } 
 else if (n == 1) 
 { 
  return 1; 
 } 
 else 
 { 
  return Fibonacci(n - 1) + Fibonacci(n - 2); 
 } 
} 
```
If we write down what really happens when the upper code executes, we will 
see that the function calls itself as many times as the Fibonacci ’s n+1 number 
is. We can roughly evaluate the complexity by another way too: since on 
every step of the function execution 2 recursi ve calls are done in average, the 
count of the recursive calls must be in order of 2 n, i.e. we have an 
exponential complexity . This automatically means that for values greater 
than 20-30 the function will "hang". You may check this yourself. 
The same funct ion for calculating the nth number of Fibonacci  can be 
written with a linear complexity in the following way: 
long Fibonacci(int n) 
```csharp
{ 
 long fn = 1; 
 long fn1 = 1; 
 long fn2 = 1; 
 for (int i = 2; i < n; i++) 
 { 
  fn = fn1 + fn2; 
  fn2 = fn1; 
  fn1 = fn; 
 } 
 return fn; 
} 
```
You see that the complexity estimation helps us to predict whether a given 
code will work slowly  before we have run it and it implies we should look 
for a more efficient solution.