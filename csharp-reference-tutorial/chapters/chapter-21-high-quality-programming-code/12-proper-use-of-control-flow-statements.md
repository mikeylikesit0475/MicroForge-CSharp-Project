# Proper Use of Control Flow Statements

Control flow statements are represented by loops and conditional statements. 
We are going to review the good practices for using them properly.

## With or Without Curly Brackets?

Loops and conditional statements allow their body to not be surrounded by 
brackets, in case the body consists only of a single statement. This can be 
dangerous. Consider the following example: 
```csharp
static void Main() 
{ 
 int two = 2; 
 if (two == 1) 
  Console.WriteLine("This is the ..."); 
  Console.WriteLine("... number one."); 
 Console.WriteLine( 
  "Example of an if-clause without curly brackets."); 
} 
```
We are expecting to see only the last sentence, aren ’t we? The result is a bit 
unexpected: 
... number one. 
Example of an if-clause without curly brackets. 


That is because an if-statement without curly brackets only takes the first 
statement as its body, regardless of the indentation, which  makes matters 
confusing. 
 
Always enclose the body of loops and conditional statements 
in curly brackets – { and }.

## Proper Usage of Conditional Statements

Conditional statements in C# are represented by the if-else and the 
switch-case statements. 
if (condition) 
```csharp
{ 
 
} 
else 
{ 
 
}
```

### Deep Nesting of Conditional Statements

Deep nesting of if statements is a bad practice because it obstructs the 
comprehensibility of the code: 
1 
2 
3 
4 
5 
6 
7 
8 
9 
10 
11 
12 
13 
14 
15 
16 
17 
18 
19 
```csharp
private int Max(int a, int b, int c, int d) 
{ 
 if (a < b) 
 { 
  if (b < c) 
  { 
   if (c < d) 
   { 
    return d; 
   } 
   else 
   { 
    return c; 
   } 
  } 
  else if (b > d) 
  { 
   return b; 
  } 


```
20 
21 
22 
23 
24 
25 
26 
27 
28 
29 
30 
31 
32 
33 
34 
35 
36 
37 
38 
39 
40 
41 
42 
43 
44 
  else 
```csharp
  { 
   return d; 
  } 
 } 
 else if (a < c) 
 { 
  if (c < d) 
  { 
   return d; 
  } 
  else 
  { 
   return c; 
  } 
 } 
 else if (a > d) 
 { 
  return a; 
 } 
 else 
 { 
  return d; 
 } 
} 
```
This code is hardly readable because of the deep nesting. In order to improve 
it, we could introduce a few more methods  where parts of the logic are 
exported and isolated. Here is how we could do that: 
1 
2 
3 
4 
5 
6 
7 
8 
9 
10 
11 
12 
13 
14 
15 
```csharp
private int Max(int a, int b) 
{ 
 if (a < b) 
 { 
  return b; 
 } 
 else 
 { 
  return a; 
 } 
} 
 
private int Max(int a, int b, int c) 
{ 
 if (a < b) 

```
16 
17 
18 
19 
20 
21 
22 
23 
24 
25 
26 
27 
28 
29 
30 
31 
32 
33 
34 
35 
```csharp
 { 
  return Max(b, c); 
 } 
 else 
 { 
  return Max(a, c); 
 } 
} 
 
private int Max(int a, int b, int c, int d) 
{ 
 if (a < b) 
 { 
  return Max(b, c, d); 
 } 
 else 
 { 
  return Max(a, c, d); 
 } 
} 
```
Extracting parts of the code into separate methods is the easiest and most 
efficient way to reduce the level of nesting of a group of conditional 
statements, while preserving their logic. 
The refactored method is split into a few smaller ones. The overall length of 
the code has been decreased by 9 lines. Each of the new methods is simpler 
and easier to read. As a side benefit, we get two methods that can be easily 
reused for other purposes.

## Proper Use of Loops

Proper use of the different looping constructs is very important to the creation 
of quality software. In the next paragraphs we outline some of the principles, 
which help us decide when, and how to use a particular loop construct.

### Choosing an Appropriate Looping Construct

If we are not able to decide whether to use for, while or do-while loop, we 
can easily pick up one, adhering to the following principles. 
If we need a loop that will execute a fixed number of times, a for-loop is a 
good fit. This kind of l oop is used in the most basic situations where 
interrupting the control is not necessary. The initialization, the check of the 
condition and the incrementing are all in the for-construct and the loop body 
does not care about that. The value of the counter should not be altered 
within the body. 

If it is necessary to check some conditions in order to stop  the execution 
of the loop, then it is probably better to pick a while loop. A while loop is 
suitable in cases where the exact number of iterations is not kn own. The 
execution there continues until the exit condition has been encountered. If the 
prerequisites for using a while loop are in place, but the loop body must 
unconditionally execute at least once , a do-while loop should be used 
instead.

### Do Not Nest Too Many Loops

As with conditional statements, deep nesting of loops is a bad practice . 
Deep nesting usually happens because of a large number of loops and 
conditional statements residing in one another. This makes the code hard to 
read and maintain. Such code can easily be improved by moving away parts 
of it into separate methods. Modern d evelopment environments can do such 
refactoring automatically (we talk about that in the code refactoring section).