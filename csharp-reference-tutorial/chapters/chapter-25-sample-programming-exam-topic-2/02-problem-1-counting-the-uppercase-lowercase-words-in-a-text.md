# Problem 1: Counting the Uppercase / Lowercase Words in a Text

Problem 1: Counting the Uppercase / Lowercase 
Words in a Text 
Write a program that counts the words in a text  entered from the console. 
The program must output the total number of words , the number of words 
written in uppercase and the number of words written in lowercase. If a 
word appears more than once in the text, each repetition counts as a new 
occurrence. Every character that is not a letter counts as a word separator. 
Sample input: 
Welcome to your first programming exam! Can you think of a 
solution to this problem and write it down? GOOD LUCK! 
Sample output: 
Word count: 21 
Upper case words: 2 
Lower case words: 17

## Coming Up with an Appropriate Idea for a Solution

Intuitively, it comes to mind, that the problem may be solved by splitting 
the text up into separate words and counting  those that meet the 
specified conditions. 
Obviously, this approach is correct, but far too general, and it doesn ’t lead to 
a particular method for solving the problem. Let ’s try to be more specific , 
and see if by doing so, we could imple ment an algorithm that will lead to a 

solution. It might turn out that the implementation is difficult, or that the 
complexity of the solution is too great for the program to complete its 
execution even with today’s powerful computers. If that is the case,  we would 
have to find another solution to the problem.

## Breaking Down the Problem into Subproblems

A useful approach for solving algorithmic problems is to try breaking them 
down into smaller problems  that are easier and quicker to solve. Let ’s try 
defining the necessary steps for solving this problem. 
First of all, we have to split the text up into separate words . This, in and 
of itself, is not a simple task, but it is the first step towards breaking down the 
problem into smaller, although still complicated, subproblems. 
Then we need to count the words that concern us. This is the second major 
problem we have to solve. Let ’s take a look at both problems separately and 
try breaking them down even further.

### How Do We Split the Text Up into Separate Words?

In order to split the text up into separate words, we need to find a way to 
identify them first. According to the problem specifications every non -letter 
character functions as a word separator. Therefore, we must first identify 
these separators and use them to split the text in tokens. 
So far, we have formulated two subproblems – finding the separators  and 
partitioning (splitting) the text in accordance with the characters found. 
We can implement their solutions right away. This was in fact our goal from 
the start – breaking down complicated problems into smaller and easier 
subproblems. 
In order to find the separators , all we need to do is iterate through all 
characters and extract those that aren’t letters. 
Once we have identified the separators, we can im plement the text 
partitioning by invoking the Split(…) method of the String class.

### How Do We Count the Words?

Let’s assume we already have a list of all words from the text. We want to 
find the total word count, the number of words in uppercase and the 
number of words in lowercase. 
To do this, we can go through each and every word from the list and check if 
it meets either of the necessary conditions. At each step we increment the 
total word count. We check if the current word is in uppercase and, if so, we 
increment the number of words in uppercase . Likewise, we check if the 
word consists only of lowercase letters  and increment the lowercase word 
counter. 
Thus, we have defined another two subproblems  – recognizing uppercase 
and lowercase words. These app ear to be very easy. It might even turn out 

that the string class provides such functionality. After we check, it turns out 
this is not the case. Yet we notice that there are methods that allow us to 
convert a string to an uppercase or a lowercase string. This might be of use. 
To check if a word consists only of uppercase letters, all we have to do is 
compare it to the string resulting after converting the word to uppercase. If 
the two are equal, then the comparison returns true. Performing the check for 
lowercase words is done likewise.

## Verifying the Idea

It seems our idea is a good one. We ’ve broken down the problem into 
subproblems and we know how to solve each of them. Should we continue 
towards the implementation? Haven’t we overlooked something? 
Shouldn’t we have verified the idea by writing down a few examples on 
paper? Perhaps we  would come across something we have missed. We could 
start with the example given in the problem statement: 
The separators would be: spaces, ? and !. The words that have come up 
are the following: Welcome, to, your, first, programming, exam, Can, 
you, think, of, a, solution, to, this, problem, and, write, it, down, 
GOOD, LUCK. 
Counting the words we acquire the correct result. It seems the idea is 
adequate, and it works. Now we can proceed towards implementing it. We 
will do this step by step and at each step we will implement one subproblem.

## Let’s Consider the Data Structures

The problem is simple and doesn’t need complex data structures. 
We can use the char data type for storing each separator. During the process 
of finding the separator characters we add each of them to a list. We can use 
either char[] or List<char>. In this case, we will choose the latter. 
As for the words in the text, we can use an array of strings string[] or 
List<string>.

## Let’s Consider the Efficiency

Are there any performance requirements? How long can the text be? 
Since the text will be entered from the console, it ’s unlikely to be very long. 
No one is going to type 1MB of text into the console. We can assume that the 
solution’s performance is not critical. 
Welcome to your first programming exam! Can you think of a 
solution to this problem and write it down? GOOD LUCK!

## Let’s Write Down the Solution

It’s very good practice to write the solution down o n a piece of paper  
before typing it on the computer. This helps uncover drawbacks in our idea or 
implementation beforehand. In addition, implementing the solution will be 
considerably quicker, because of the outlines we can provide and because we 
would then have a better grasp of both the problem and the solution.

## Step 1: Finding the Separators in the Text

We will  define a method that extracts all non -letter characters from the 
text and return them as an array of characters. Then we will use that array 
for splitting the text up into separate words.  We will use List<char> to keep 
the separators we find when passing through the text: 
```csharp
private static char[] ExtractSeparators(string text) 
{ 
 List<char> separators = new();
 foreach (char character in text) 
 { 
  // If the character is not a letter, 
  // then by definition it is a separator 
  if (!char.IsLetter(character)) 
  { 
   separators.Add(character); 
  } 
 } 
 return separators.ToArray(); 
} 
We use a loop to iterate through all of the characters in the text. We check if 
```
the current character is a letter by invoking the IsLetter() method of the 
primitive data type char. If it ’s not, we add the character to the separators.  
Finally, our method returns an array containing the separators.

### Testing the ExtractSeparators(…) Method

Before we go any further, it’s advisable to test if extracting the separators 
is working correctly. For this purpose, we will write two additional methods. 
The first of these is TestExtractSeparators() which will test the executi on 
of ExtractSeparators(…) and the second – GetTestData() – will return 
different texts, which will allow us to test our solution: 
```csharp
private static void TestExtractSeparators() 
{ 
 List<string> testData = GetTestData(); 
 foreach (string testCase in testData) 

 { 
  Console.WriteLine( 
   "Test Case:{0}{1}", Environment.NewLine, testCase); 
  Console.WriteLine("Result:"); 
  foreach (char separator in ExtractSeparators(testCase)) 
  { 
   Console.Write("{0} ", separator); 
  } 
  Console.WriteLine(); 
 } 
} 
 
private static List<string> GetTestData() 
{ 
 List<string> testData = new();
```
 testData.Add("This is wonderful!!! All separators like " + 
  "these ,.(? and these /* are recognized. It works."); 
 testData.Add("SingleWord"); 
 testData.Add(string.Empty); 
 testData.Add(">?!>?#@?"); 
 return testData; 
} 
 
```csharp
static void Main() 
{ 
 TestExtractSeparators(); 
} 
We start the program and check if the separators have been correctly 
```
identified. The first test’s result is as follows: 
Test Case: 
This is wonderful!!! All separators like these ,.(? and these /* 
are recognized. 
 It works. 
Result: 
    ! ! !           , . ( ?       / *     .     . 
Test Case: 
SingleWord 
Result: 
 
Test Case: 
 
Result: 

 
Test Case: 
>?!>?#@? 
Result: 
> ? ! > ? # @ ? 
We might think of the above output as partially correct . In fact it does 
extract correctly the separators between the words but most of them are 
duplicated several times . We need all the separators without duplications, 
right?

### Correcting the ExtractSeparators(…) Method

To correct the method for extracting the separators between the words in the 
text, we can use a different data structure to keep them. We know that sets 
keep elements without duplications. So we could use HashSet<char> instead 
of List<char> to hold the separator characters we find in the text: 
```csharp
private static char[] ExtractSeparators(string text) 
{ 
 HashSet<char> separators = new();
 foreach (char character in text) 
 { 
  // If the character is not a letter, 
  // then by definition it is a separator 
  if (!char.IsLetter(character)) 
  { 
   separators.Add(character); 
  } 
 } 
 return separators.ToArray(); 
} 
The code is almost the same, but we use a set instead of list to avoid 
duplicated separators. We might need to include the System.Linq namespace 
in the start of the program to use the ToArray() extension method for 
converting a hash set to an array.
```

### Testing Again after the Fix

We test the above method with the same testing code and we find it now 
works correctly. The separators are extracted correctly with no duplicates: 
Test Case: 
This is wonderful!!! All separators like these ,.(? and these 
/* are recognized. 
 It works. 

Result: 
  ! , . ( ? / * 
Test Case: 
SingleWord 
Result: 
 
Test Case: 
 
Result: 
 
Test Case: 
>?!>?#@? 
Result: 
> ? ! # @ 
We test also with some borderline cases – text consisting of a single word 
without separators; text consisting of separators only; an empty string. We ’ve 
already included such tests in our GetTestData() method. It seems that the 
method works fine and we can proceed to the next step.

## Step 2: Splitting Up the Text in Separate Words

We will  use string’s Split(…) method with the specified separators for 
splitting up the text by the separators and extracting the words from it . This 
is how our method looks like: 
```csharp
private static string[] ExtractWords(string text) 
{ 
 char[] separators = ExtractSeparators(text); 
 string[] words = text.Split(separators, 
  StringSplitOptions.RemoveEmptyEntries); 
 return words; 
}
```

### Testing the Word Extracting Method

Before we carry on to the next step, we have to see if the method works 
correctly. To do this, we will reuse the GetTestData() for the input test data 
and we will test the new ExtractWords(…) method: 
```csharp
private static void TestExtractWords() 
{ 
 List<string> testData = GetTestData(); 
 foreach (string testCase in testData) 
 { 

  Console.WriteLine("\nTest Case: {0}", testCase); 
  string[] words = ExtractWords(testCase); 
  Console.WriteLine("Result: {0}", string.Join(" ", words)); 
 } 
} 
 
static void Main() 
{ 
 TestExtractWords(); 
} 
```
The result from the above test looks correct: 
Test Case: This is wonderful!!! All separators like these ,.(? 
and these /* are 
recognized. It works. 
Result: This is wonderful All separators like these and these 
are recognized It 
works 
 
Test Case: SingleWord 
Result: SingleWord 
 
Test Case: 
Result: 
 
Test Case: >?!>?#@? 
Result: 
We check the results from the other test cases. We verify that they are 
correct and that our algorithm is accurate (till this stop).

## Step 3: Determining Whether a Word Is in Uppercase or Lowercase

Step 3: Determining Whether a Word Is in Uppercase 
or Lowercase 
We already have an idea how to implement the  uppercase / lowercase 
checks, and we can write the corresponding methods directly: 
```csharp
private static bool IsUpperCase(string word) 
{ 
 bool result = word.Equals(word.ToUpper()); 
 return result; 
} 
 
private static bool IsLowerCase(string word) 

{ 
 bool result = word.Equals(word.ToLower()); 
 return result; 
} 
```
We test the above methods  by passing words in uppercase, lowercase and 
mixed case. The results are correct.

## Step 4: Counting the Words

Now we can proceed to solving the problem itself – counting the words. All 
we have to do is iterate through the list of words and depending on the word ’s 
type to increment the corresponding counters. Then we print the result: 
```csharp
private static void CountWords(string[] words) 
{ 
 int allUpperCaseWordsCount = 0; 
 int allLowerCaseWordsCount = 0; 
 foreach (string word in words) 
 { 
  if (IsUpperCase(word)) 
  { 
   allUpperCaseWordsCount++; 
  } 
  else if (IsLowerCase(word)) 
  { 
   allLowerCaseWordsCount++; 
  } 
 } 
 
 Console.WriteLine("Total words count: {0}", words.Length); 
 Console.WriteLine("Upper case words count: {0}", 
  allUpperCaseWordsCount); 
 Console.WriteLine("Lower case words count: {0}", 
  allLowerCaseWordsCount); 
}
```

### Testing the Word Counting Method

Let’s check if we count the words correctly . We will  write another test 
method using the data from the GetTestData() method and the previously 
written and tested ExtractWords(…) method: 
```csharp
private static void TestCountWords() 
{ 

 List<string> testData = GetTestData(); 
 foreach (string testCase in testData) 
 { 
  Console.WriteLine("Test Case: {0}", testCase); 
  Console.WriteLine("Result: "); 
  CountWords(ExtractWords(testCase)); 
  Console.WriteLine(); 
 } 
} 
 
static void Main() 
{ 
 TestCountWords(); 
} 
```
Executing the application, we obtain the correct result: 
Test Case: This is wonderful!!! All separators like these ,.(? and these /* are 
recognized. It works. 
Result: 
Total words count: 13 
Upper case words count: 0 
Lower case words count: 10 
 
Test Case: SingleWord 
Result: 
Total words count: 1 
Upper case words count: 0 
Lower case words count: 0 
 
Test Case: 
Result: 
Total words count: 0 
Upper case words count: 0 
Lower case words count: 0 
 
Test Case: >?!>?#@? 
Result: 
Total words count: 0 
Upper case words count: 0 
Lower case words count: 0 
The above results are correct  (the typical case  and a few borderline 
cases). We perform few other borderline tests, e.g. when the list contains 
words in uppercase or lowercase only, or when the list is empty.  All of them  
work correctly. 

Note that it is a good idea to use unit testing  instead of these semi -
automated tests. Recall how we write unit tests in Visual Studio  (in the 
chapter “High-Quality Code”) and try to convert our test methods to unit tests  
for the Visual Studio Team Test (VSTT) framework.

## Step 5: Console Input

All that’s left to implement is the final step – allowing the user to input text: 
```csharp
private static string ReadText() 
{ 
 Console.WriteLine("Enter text:"); 
 return Console.ReadLine(); 
} 
```
Note that as a rule unless the input comes from a text file or is very short 
(e.g. just one number or few characters) it should be read as a final step . 
Otherwise we will need to enter the input data each time when we start the 
program and this will waste a lot of time and can lead to errors.

## Step 6: Putting All Together

Now after all subproblems have been solved , we can proceed to the 
complete solution to the probl em. We need to add a Main(…) method, which 
will combine together the different parts of the solution: 
```csharp
static void Main() 
{ 
 string text = ReadText(); 
 string[] words = ExtractWords(text); 
 CountWords(words); 
}
```

## Testing the Solution

While implementing the solution, we wrote test methods for every 
method, integrating them with each other gradually. For the moment, we are 
certain they interact correctly; there ’s nothing we have overlooked and there 
is no method that does unnecessary work or that returns incorrect results. 
If we would like  to test the solution with more data , we would only need 
to add it to the GetTestData(…) method. If we want, we may even rewrite 
the GetTestData(…) method so that it reads the test data from an extern al 
source, e.g. from a text file. 
Here’s how the final solution looks like at the end: 
WordsCounter.cs 

```csharp
using System; 
using System.Collections.Generic; 
using System.Linq; 
 
public class WordsCounter 
{ 
 static void Main() 
 { 
  string text = ReadText(); 
  string[] words = ExtractWords(text); 
  CountWords(words); 
 } 
 
 private static string ReadText() 
 { 
  Console.WriteLine("Enter text:"); 
  return Console.ReadLine(); 
 } 
 
 private static char[] ExtractSeparators(string text) 
 { 
  HashSet<char> separators = new();
  foreach (char character in text) 
  { 
   // If the character is not a letter, 
   // then by definition it is a separator 
   if (!char.IsLetter(character)) 
   { 
    separators.Add(character); 
   } 
  } 
  return separators.ToArray(); 
 } 
 
 private static string[] ExtractWords(string text) 
 { 
  char[] separators = ExtractSeparators(text); 
  string[] words = text.Split(separators, 
   StringSplitOptions.RemoveEmptyEntries); 
  return words; 
 } 
 
 private static bool IsUpperCase(string word) 
 { 

  bool result = word.Equals(word.ToUpper()); 
  return result; 
 } 
 
 private static bool IsLowerCase(string word) 
 { 
  bool result = word.Equals(word.ToLower()); 
  return result; 
 } 
 
 private static void CountWords(string[] words) 
 { 
  int allUpperCaseWordsCount = 0; 
  int allLowerCaseWordsCount = 0; 
 
  foreach (string word in words) 
  { 
   if (IsUpperCase(word)) 
   { 
    allUpperCaseWordsCount++; 
   } 
   else if (IsLowerCase(word)) 
   { 
    allLowerCaseWordsCount++; 
   } 
  } 
 
  Console.WriteLine("Total words count: {0}", words.Length); 
  Console.WriteLine("Upper case words count: {0}", 
   allUpperCaseWordsCount); 
  Console.WriteLine("Lower case words count: {0}", 
   allLowerCaseWordsCount); 
 } 
} 
```
We removed the testing methods from our code to simplify it. The best 
practice is instead of removing the tests to create a separate testing 
project and put all the tests in a testing class. This is best achieved though 
the Visual Studio’s unit testing framework , as it was shown in the chapter 
“High-Quality Code”.

## A Word on Performance

Since there are no explicit performance requirements , we will only make 
a suggestion for dealing with the situation when the algorithm turns out to be 
slow. Splitting the text with separators assumes that the entire text will be 

loaded into memory . The list of words, after partitioning the text, will also 
be written to memory. Therefore, if the input text is large, the program will 
also consume a large amount of memory . For example, if the input text is 
200MB long, then the program will consume at least 800MB of memory, 
because each word is stored as 2 bytes for every character (.NET uses UTF-16 
character encoding for the strings in memory). 
If we want to avoid high memory consumption then the words must not 
be stored in memory all at once. We can c ome up with another algorithm: 
scanning the text char by char  and storing the letters in to a buffer (such 
as StringBuilder). If at a certain moment a separator is encountered, then 
the buffer contains the most recent word. We can analyze its casing and the n 
empty the buffer. We can repeat this until the end of the file is reached. This 
appears to be more efficient, doesn’t it? 
A more efficient lower  / upper case checker would be to iterate through 
all letters using a loop and to examine them  char by char . That way we can 
skip a lower / upper case conversion, which allocates extra memory for every 
word. After the word has been processed, the memory will be freed, which 
would eventually lead to extra CPU utilization (for the .NET garbage collector). 
Obviously, the latter solution is more efficient. The question is if we should 
scrap the original solution and write a completely different one. It all 
depends on the performance requirements . The problem description 
doesn’t hint at an input text measuring  in the hundreds of megabytes. 
Therefore the current solution, although not optimal, is still correct and will 
suffice. We suggest the reader to implement the proposed fast solution  
and to compare how faster it is, e.g. by processing an input of 100 MB.