# Problem 2: Counting Words in a Text File

We are given a text file words.txt, which contains several words, one per 
each line . Each word consists of Latin letters only . Write a program , which 
retrieves the number of matches  of each of the given words as a 
substring in the file text.txt. The counting is case insensitive. The result 
should be written into a text file named result.txt in the following format 
(the words should appear in the same order as given in the input file 
words.txt): 
<word1> --> <number of matches> 
<word2> --> <number of matches> 
… 
Sample input file words.txt: 
for 
academy 
student 
Java 
develop 

CAD 
Sample input file text.txt: 
The Telerik Academy for software development engineers is a 
famous center for free professional training of .NET experts. 
Telerik Academy offers courses designed to develop practical 
computer programming skills. Students graduated the Academy are 
guaranteed to have a job as a software developers in Telerik. 
Sample result file result.txt: 
for --> 2 
academy --> 3 
student --> 1 
Java --> 0 
develop --> 3 
CAD --> 3 
Below are the locations of the matched words from the above example: 
The Telerik Academy for software development engineers is a 
famous center for free professional training of .NET experts. 
Telerik Academy offers courses designed to develop practical 
computer programming skills. Students graduated the Academy are 
guaranteed to have a job as a software developers in Telerik.

## Start Thinking on the Problem

The emphasis of the given problem seems not so much o n the algorithm , 
but on its technical implementation. In order to write the solution we must 
be familiar with working with files in C# and with the basic data structures , as 
well as string processing in .NET Framework.

## Inventing an Idea for a Solution

We get a piece of paper, write few examples and we come up with the 
following idea: we read the word s file, scan through the text  and check 
each word  from the text for matches with the preliminary given  list of 
words and increase the counter for each matched word.

## Checking the Idea

The above idea for solving the task is trivial but we can still check it  by 
writing down on a piece of paper the sample input (words and text) and the 
expected result. We just scan through the text word by word in our paper 

example and when we find a match  with some of the preliminary given words 
(as a substring)  we increment the counter for the matched word.  The idea 
works in our example. 
Now let’s think of counterexamples. In the same time we might also come 
with few questions regarding the implementation: 
- How do we scan the text  and search for matches ? We can scan the 
text character by character  or line by line  or we can read the 
entire text in the memory and then scan it in the memory (by string 
matching or by a regular ex pression). All of these approaches might 
work correctly but the performance could vary, right?  We will think 
about the performance a bit later. 
- How do we extract the words  from the text? Maybe we can read the 
text and split it by all any non -letter characters? Where shall we take 
these non-letter characters from? Or we can read the text char by char 
and once we find a non-letter character we will have the next word from 
the text?  The second idea seems faster and will require less memory 
because we don’t need to read all the text at once. We should think 
about this, right? 
- How do we match two words?  This is a good question.  Very good 
question. Suppose we have a word from the text a nd we want to match 
it with the words from the file words.txt. For example, we have 
“Academy” in the text and we should find whether it matches as 
substring the “ CAD” word from the list of words. This will require 
searching each word from the list as a sub string in each word from the 
text. Also can we have some word appearing several times inside 
another? This is possible, right? 
From all the above questions we can conclude that we don’t need to read 
the text word by word. We need to match substrings, not words! The title 
of the problem is misleading. It says “Counting Words in a Text File” but it 
should be “Counting Substrings in a Text File”. 
It is really good that we found we have to match substrings  (instead of 
words), before we have implemented the code for the above idea, right?

## Inventing a Better Idea

Now, considering the requirement for substring matching, we come with few 
new and probably better ideas about solving the problem: 
- Scan the text line by line  and for each line from the text and each 
word check how many times the word appears as substring in the 
line. The last can be counted with String.IndexOf(…) method in a 
loop. We already have solved this subproblem in the chapter “Strings 
and Te xt Processing”  (see the section “Finding All Occurrences of a 
Substring”). 

- Read the entire text and count the occurrences of each word in it 
(as a substring) . This idea is very similar to the previo us idea but it 
will require much memory to read the entire text. Maybe this will not be 
efficient. We gain nothing, but potentially we will run “out of memory”. 
- Scan the text char by char  and store the read characters in a buffer. 
After each character read  we check if the text in the buffer ends 
with some of the words  from the list. We will not need to search the 
words in the buffer because we check for each word after each character 
is read.  We could also clear the buffer when we read any non -letter 
character (because the list of words for matching should contain letters 
only). Thus the memory consumption will be very low. 
The first and the last idea  seem to be good. Which of them to implemen t? 
Maybe we could implement both of them and choose the faster one. Having 
two solutions will also improve the testing because we should get identical 
results with both of the solutions on all test cases.

## Checking the New Ideas

We have two good ideas and w e need to check them for correctness  
before thinking about implementation. How to check the ideas? We can invent 
a good test case on a piece of paper and try the ideas on it. 
Let’s have the following list of words: 
Word 
S 
MissingWord 
DS 
aa 
We might be interested to find the number of occurrences of the above words 
in the following text: 
Word? We have few words: first word, second word, third word. 
Some passwords: PASSWORD123, @PaSsWoRd!456, AAaA, !PASSWORD 
The expected result is as follows: 
Word --> 9 
S --> 13 
MissingWord --> 0 
DS --> 2 
aa --> 3 
In the above example we have many different special cases : whole-word 
matching, matching as a substring, matching in different casing, match es in 
the start / end of the text, several matches inside the same word, overlapping 

matches, etc. This example is a very good representative of the common 
case for this problem. It is important to have such short but 
comprehensive test case  when solving pro gramming problems. It is 
important to have it early , when checking the ideas, before any code is 
written. This avoids mistakes, catches incorrect algorithms and saves time!

### Checking the Line by Line Algorithm

Now let’s check the first algorithm : read the two lines of text and check 
how many times each of the words from the given list occurs in each line 
ignoring the character casing. At the first line we find as substrings (ignoring 
the case) “word” 5 times, “s” 3 times, “MissingWord”  0 times, “aa” 0 times 
and “ds” – 1 time. At the second line we find as substrings (ignoring the case) 
“word” 4 times, “s” 10 times, “MissingWord” 0 times, “aa” 3 times and “ds” – 
1 time. We sum the occurrences and we find that the result is correct. 
We try to find counterexamples, but we can’t. The algorithm may not work 
with words spanning multiple lines . This is not possible by definition . It may 
also have issues with the overlapping matches like finding “aa” in “AAaA”. 
This will be definitely checked after the algorithm is implemented.

### Checking the Char by Char Algorithm

Let’s check the other algorithm : scan through the text char by char, 
holding the characters in a buffer. After each character if the buffer ends with 
some of the words (ignoring the character casin g), the occurrences of the 
matched word are increased. If a non-letter is occurred, the buffer is cleaned. 
We start from empty buffer and append the first char from the text “W” to 
the buffer. None of the words match the end of the buffer . We append 
“o” and the buffer holds “Wo”. No matches. Then we append “r”. The buffer 
holds “Wor”. Again no matches are found with any of the words. We append 
the next  char “d” and the buffer holds “Word”. We have found a match  
with the word form a list: “word”. We increase  the number of occurrences of 
the matched word from zero to one. The next char is “?” and we clean the 
buffer, because it is not a letter. The next char is “ ” (space). We again clean 
the buffer. The next char is “W”. We append it to the buffer. No matches  with 
any of the words. We continue further and further… After the last character is 
processed, the algorithm finishes and the results are correct. 
We try to find counterexamples, but we can’t. The algorithm may not work 
with words spanning multiple lines, but this is not possible by definition.

## Decompose the Problem into Subproblems

Now let’s try to divide the problem into subproblems. This should be done 
separately for the both algorithms we want to try because they differ 
significantly.

### Line by Line Algorithm Decomposed into Subproblems

Let’s decompose the line by line algorithm into subproblems (sub-steps): 

1. Read the input words . We can read the file words.txt by using 
File.ReadAllLines(…). It reads a text file in a string[] array of lines. 
2. Process the li nes of the text  one by one to count the occurrences of 
each word in it. Initially assign zero occurrences for each word. Read the 
input file text.txt line by line. For each line  from the text and  for 
each word check the number of its occurrences (this is a  separate 
subproblem) and increase the counters for each match.  The occurrences 
counting should be case-insensitive. 
3. Count the number of occurrences of certain substring in certain 
text. This is a separate subproblem. We find the leftmost occurrence of 
the substring in the text though string.IndexOf(…). If the returned 
index > -1 (the substring exists), we increase the counter and find the 
next occurrence of the substring on the right from the last found index. 
We perform this in a loop until we find -1 as a result which means that 
there are no more matches.  To perform case -insensitive searching  we 
can pass a special parameter StringComparison.OrdinalIgnoreCase 
to the IndexOf() method. 
4. Print the results . Process all words and for each word print it along 
with its counter holding its occurrences in the output file result.txt.

### Char by Char Algorithm Decomposed into Subproblems

Let’s decompose the char by char algorithm into subproblems (sub-steps): 
1. Read the input words . We can read the file words.txt by using 
File.ReadAllLines(…). It reads a text file in a string[] array of lines. 
The original words can be saved and a copy of them in lowercase can be 
made to simplify the matching with ignoring the character casing. 
2. Process the text char by char . Read the input f ile text.txt and 
append the letters into a buffer  (StringBuilder). After each letter 
appended check whether the text in the buffer ends with some of the 
words in the input list of words (this check is a separate subproblem). If 
so, increase the number occurrences of the matched word. If a non -
letter character is found, clean the buffer.  Letters are converted to 
lowercase before added in the buffer. 
3. Check whether a certain text (StringBuilder) ends by a certain 
string. In case the string has length n lower than the length of the text, 
the result is false. Otherwise the n letters of the string should be 
compared one by one with the last n letters of the text. If a mismatch is 
found, the result is false. If all checks pass, the result is true. 
4. Print the results . Process all words and for each word print it along 
with its counter holding its occurrences in the output file result.txt.

## Think about the Data Structures

In the line by line algorithm  we don’t have any need of special data 
structures. We can keep the words in an array or list of strings . We can 
keep the number of occurrences for each word in array of integer values . 
The text lines we can keep in strings. 
In the char by char algorithm  the situation is similar. We don’t need any 
special data structures. We can keep the words in an array or list of 
strings. We can keep the number of occurrences for each word in array of 
integer values . The buffer for the characters we can implement by 
StringBuilder (because we need to append chars many times).

## Think about the Performance

Following the guidelines for problem solving from the chapter “Methodology of 
Problem Solving” we should think about the efficiency and performance  
before writing any code. 
The line by line algorithm  will process the entire text line by line and for 
each text line it will search for all of the words. Thus if the text has a total size  
of t characters and the number o f words are w, the algorithm will totally 
perform w string searches in t characters. Each search for a word in the text 
will pass through the entire text  (at least once, but maybe not always) . If we 
assume that searching for a word in a text is a linear ti me operation, we will 
have w scans through the entire text , so  the excepted running time in 
quadratic: O(w*t). If we search in MSDN or in Internet, we will be unable to 
find any information about how exactly String.IndexOf(…) works internally 
and whether it runs in linear time or it is slower. This method calls a Win32 
API function so it cannot be decompiled. Thus the best way to check its 
performance is by measuring. 
The char by char algorithm will process the entire text cha r by char and for 
each character it will perform a string matching for each of the words.  
Suppose the text has t characters and the number of the words is w. In the 
average case the string matching will run in constant time (it will require just 
one check if the first letter is not matching, two checks if the first letter 
matches, etc.). In the worst case the string matching will require n 
comparisons where n is the length of the word being matched. Thus in the 
average case the expected running time of the algorithm will be 
quadratic: O(w*t). In the worst case it will be significantly slower. 
It seems like the line by line algorithm is expected to run faster  but we 
are uncertain about how fast is string.IndexOf(…), so this cannot be 
definitely stated. If we are at an exam, we will probably choose to implement 
the line by line algorithm. Just for the experiment, let’s implement both of 
them and compare their performance.

## Implementation: Step by Step

If w e directly follow the steps, which  we have already identified we can 
write the code with ease. Of course it is better to implement the algorithms 
step-by-step, to find and fix the bugs early.

## Line by Line Algorithm: Step by Step Implementation

We can start implementing the line by line algorithm for word countin g in a 
text file from the method that counts how many times a substring 
appears in a text. It should look like the following: 
```csharp
static int CountOccurrences( string substring, string text) 
{ 
 int count = 0; 
 int index = 0; 
 while (true) 
 { 
  index = text.IndexOf(substring, index); 
  if (index == -1) 
  { 
   // No more matches 
   break; 
  } 
  count++; 
 } 
 return count; 
} 
```
Let’s test it before going further: 
```csharp
Console.WriteLine( 
 CountOccurrences("hello", "Hello World Hello")); 
```
The result is 0 – wrong! It seems like we have forgotten to ignore the 
character casing. Let’s fix this . We need to change the name of the method 
as well and add the StringComparison.OrdinalIgnoreCase option when 
searching for the given substring: 
```csharp
static int CountOccurrencesIgnoreCase( 
 string substring, string text) 
{ 
 int count = 0; 
 int index = 0; 
 while (true) 
 { 

  index = text.IndexOf(substring, index, 
   StringComparison.OrdinalIgnoreCase); 
  if (index == -1) 
  { 
   // No more matches 
   break; 
  } 
  count++; 
 } 
 return count; 
} 
```
Let’s test again with the same example. The program hangs ! What 
happens? We step through the code using the debugger and we find that the 
variable index takes the first occurrence at position 0 and at the next 
iteration it takes the same occurrence again at position 0 and the program 
enters into an endless loop . This is easy to fix. Just start searching from 
position index+1 (the next position on the right), not from index: 
```csharp
static int CountOccurrencesIgnoreCase( 
 string substring, string text) 
{ 
 int count = 0; 
 int index = 0; 
 while (true) 
 { 
  index = text.IndexOf(substring, index + 1, 
   StringComparison.OrdinalIgnoreCase); 
  if (index == -1) 
  { 
   // No more matches 
   break; 
  } 
  count++; 
 } 
 return count; 
} 
```
We run the fixed code with the same test. Now the result is incorrect  (1 
occurrence instead of 2). We again trace the program with the debugger and 
we find that the first match is at position 12. Immediately we find out why 
this happens: initially we start  from position 1 ( index + 1 when index is 0) 
and we skip the start of the text (position 0). 
This is easy to fix: 

```csharp
static int CountOccurrencesIgnoreCase( 
 string substring, string text) 
{ 
 int count = 0; 
 int index = -1; 
 while (true) 
 { 
  index = text.IndexOf(substring, index + 1, 
   StringComparison.OrdinalIgnoreCase); 
  if (index == -1) 
  { 
   // No more matches 
   break; 
  } 
  count++; 
 } 
 return count; 
} 
```
We test again with the same example and finally the result is correct . We 
take another, more complex test: 
```csharp
Console.WriteLine(CountOccurrencesIgnoreCase( 
```
 "Word", "Word? We have few words: first word, second word," + 
 "third word. Passwords: PASSWORD123, @PaSsWoRd, !PASSWORD")); 
The result is again correct (9 matches). We test with missing wo rd and the 
result is again correct (0 matches). This is enough. We assume the method 
works correctly. Now let’s continue with the next step: read the words. 
string[] words = File.ReadAllLines("words.txt"); 
There is no need to test this code. It is too simple to have bugs . We will 
test it when we test the entire solution. Let’s not write the main logic of the 
program which reads the text line by line and counts the occurrences of 
each of the input words in each of the lines: 
int[] occurrences = new int[words.Length]; 
```csharp
using (StreamReader text = File.OpenText("text.txt")) 
{ 
 string line; 
 while ((line = text.ReadLine()) != null) 
 { 
  for (int i = 0; i < words.Length; i++) 
  { 

   string word = words[i]; 
   int wordOccurrences = 
    CountOccurrencesIgnoreCase(word, line); 
   occurrences[i] += wordOccurrences; 
  } 
 } 
} 
```
This code definitely should be tested  but it will be easier to write the code 
which prints the results to simplify testing. Let’s do this: 
```csharp
using (StreamWriter result = File.CreateText("result.txt")) 
{ 
 for (int i = 0; i < words.Length; i++) 
 { 
  result.WriteLine("{0} --> {1}", words[i], occurrences[i]); 
 } 
} 
The complete implementation  of the line by line string occurrences 
```
counting algorithms looks as follows: 
CountSubstringsLineByLine.cs 
```csharp
using System; 
using System.IO; 
 
public class CountSubstringsLineByLine 
{ 
 static void Main() 
 { 
  // Read the input list of words 
  string[] words = File.ReadAllLines("words.txt"); 
 
  // Process the file line by line 
  int[] occurrences = new int[words.Length]; 
  using (StreamReader text = File.OpenText("text.txt")) 
  { 
   string line; 
   while ((line = text.ReadLine()) != null) 
   { 
    for (int i = 0; i < words.Length; i++) 
    { 
     string word = words[i]; 
     int wordOccurrences = 

      CountOccurrencesIgnoreCase(word, line); 
     occurrences[i] += wordOccurrences; 
    } 
   } 
  } 
 
  // Print the result 
  using (StreamWriter result = File.CreateText("result.txt")) 
  { 
   for (int i = 0; i < words.Length; i++) 
   { 
    result.WriteLine("{0} --> {1}", 
     words[i], occurrences[i]); 
   } 
  } 
 } 
 
 static int CountOccurrencesIgnoreCase( 
  string substring, string text) 
 { 
  int count = 0; 
  int index = -1; 
  while (true) 
  { 
   index = text.IndexOf(substring, index + 1, 
    StringComparison.OrdinalIgnoreCase); 
   if (index == -1) 
   { 
    // No more matches 
    break; 
   } 
   count++; 
  } 
  return count; 
 } 
}
```

## Testing the Line by Line Algorithm

Now let’s test the entire code of the program . We try our test and it 
works as expected! 
text.txt 

Word? We have few words: first word, second word, third word. 
Some passwords: PASSWORD123, @PaSsWoRd!456, AAaA, !PASSWORD 
words.txt 
Word 
S 
MissingWord 
DS 
aa 
result.txt 
Word --> 9 
S --> 13 
MissingWord --> 0 
DS --> 2 
aa --> 3 
We also try the sample test from the problem description  and it also 
works correctly. We try few other tests and all they work correctly. We try 
also few border cases  like empty text and empty list of words. All these 
cases are handled correctly. It seems like our line by line word counting 
algorithm and its implementation correctly solve the problem. 
We need to conduct only a performance test  but let’s first implement the 
other algorithm to be able to compare which is faster.

## Char by Char Algorithm: Step by Step Implementation

Let’s now implement the char by char string occurrences counting 
algorithm. We will need a StringBuilder to hold the characters we read 
and a method to check for a match at the end of the StringBuilder. Let’s 
define this method first.  For more flexibility it can be implemented as 
extension method  to the StringBuilder class (recall how extension 
methods work from the chapter “Lambda Expressions and LINQ”): 
```csharp
static bool EndsWith(this StringBuilder buffer, string str) 
{ 
 for (int bufIndex = buffer.Length-str.Length, strIndex = 0; 
  strIndex < str.Length; 
```
  bufIndex++, strIndex++) 
```csharp
 { 
  if (buffer[bufIndex] != str[strIndex]) 
  { 
   return false; 

  } 
 } 
 return true; 
} 
```
Let’s test the method with a sample text and its ending: 
```csharp
Console.WriteLine( 
 new StringBuilder("say hello").EndsWith("hello")); 
```
This test produces a correct result: True. Let’s test the negative case: 
```csharp
Console.WriteLine(new StringBuilder("abc").EndsWith("xx")); 
This test produces a correct result: False. Let’s test what will happen if the 
```
ending is longer than the test: 
```csharp
Console.WriteLine(new StringBuilder("a").EndsWith("abcdef")); 
We get IndexOutOfRangeException. We found a bug! It is easy to fix – we 
can return false if the ending string is longer than the text where it should 
```
be found: 
```csharp
static bool EndsWith(this StringBuilder buffer, string str) 
{ 
 if (buffer.Length < str.Length) 
 { 
  return false; 
 } 
 for (int bufIndex = buffer.Length - str.Length, strIndex = 0; 
  strIndex < str.Length; 
```
  bufIndex++, strIndex++) 
```csharp
 { 
  if (buffer[bufIndex] != str[strIndex]) 
  { 
   return false; 
  } 
 } 
 return true; 
} 
```
We run all the tests again  and all of them pass . We assume the  above 
method is correctly implemented. 
Now let’s continue with the step -by-step implementation. Let’s implement the 
reading of the words: 

string[] wordsOriginal = File.ReadAllLines("words.txt"); 
This is the same code from the line by line algorithm and it should work. 
Let’s now implement the main program logic  which reads the text char by 
char in a buffer of characters and after each letter checks all input words for 
matches at the ending of the buffer: 
int[] occurrences = new int[words.Length]; 
```csharp
using (StreamReader text = File.OpenText("text.txt")) 
{ 
 StringBuilder buffer = new();
 int nextChar; 
 while ((nextChar = text.Read()) != -1) 
 { 
  char ch = (char)nextChar; 
  if (char.IsLetter(ch)) 
  { 
   // A letter is found --> check all words for matches 
   buffer.Append(ch); 
   for (int i = 0; i < words.Length; i++) 
   { 
    string word = words[i]; 
    if (buffer.EndsWith(word)) 
    { 
     occurrences[i]++; 
    } 
   } 
  } 
  else 
  { 
   // A non-letter character is found --> clean the buffer 
   buffer.Clear(); 
  } 
 } 
} 
```
To test the code we will need few lines of code to print the output: 
```csharp
using (StreamWriter result = File.CreateText("result.txt")) 
{ 
 for (int i = 0; i < words.Length; i++) 
 { 
  result.WriteLine("{0} --> {1}", 
   words[i], occurrences[i]); 

 } 
} 
```
Now the program is completed and we should test it.

## Testing the Char by Char Algorithm

Let’s test the entire code of the program . We try our test and it fails. The 
produced result is incorrect: 
Word --> 1 
S --> 6 
MissingWord --> 0 
DS --> 0 
aa --> 0 
What’s wrong ? Maybe the character casing ? Do we compare the 
characters in case-insensitive fashion? No. We found the problem. 
How to fix the character casing? Maybe we need to fix the EndsWith(…) 
method. We search in MSDN and in Internet a nd we cannot find a method to 
compare case-insensitively characters. We can do something like this: 
if (char.ToLower(ch1) != char.ToLower(ch2)) … 
The above code will work but it will convert the characters to lowercase many 
times, at each character compar ison. This may be slow  so it is better to 
lowercase the words and the text preliminary  before comparing. If we 
lowercase the words, they will be printed in lowercase at the output and this 
will be incorrect. So we need to remember the original words and to make a 
copy of them in lowercase. Let’s try it . We can use the built -in extension 
methods from System.Linq to perform the lowercase conversion: 
string[] wordsOriginal = File.ReadAllLines("words.txt"); 
string[] wordsLowercase = 
 wordsOriginal.Select(w => w.ToLower()).ToArray(); 
We need to apply few other fixes and finally we get the following full source 
code of the char by char algorithm for counting the occurrences of a list of 
substrings in given text: 
CountSubstringsCharByChar.cs 
```csharp
using System.IO; 
using System.Linq; 
using System.Text; 
 

public static class CountSubstringsCharByChar 
{ 
 static void Main() 
 { 
  // Read the input list of words 
  string[] wordsOriginal = File.ReadAllLines("words.txt"); 
  string[] wordsLowercase = 
   wordsOriginal.Select(w => w.ToLower()).ToArray(); 
 
  // Process the file char by char 
  int[] occurrences = new int[wordsLowercase.Length]; 
  StringBuilder buffer = new();
  using (StreamReader text = File.OpenText("text.txt")) 
  { 
   int nextChar; 
   while ((nextChar = text.Read()) != -1) 
   { 
    char ch = (char)nextChar; 
    if (char.IsLetter(ch)) 
    { 
     // A letter is found --> check all words for matches 
     ch = char.ToLower(ch); 
     buffer.Append(ch); 
     for (int i = 0; i < wordsLowercase.Length; i++) 
     { 
      string word = wordsLowercase[i]; 
      if (buffer.EndsWith(word)) 
      { 
       occurrences[i]++; 
      } 
     } 
    } 
    else 
    { 
     // A non-letter is found --> clean the buffer 
     buffer.Clear(); 
    } 
   } 
  } 
 
  // Print the result 
  using (StreamWriter result = File.CreateText("result.txt")) 
  { 
   for (int i = 0; i < wordsOriginal.Length; i++) 

   { 
    result.WriteLine("{0} --> {1}", 
     wordsOriginal[i], occurrences[i]); 
   } 
  } 
 } 
 
 static bool EndsWith(this StringBuilder buffer, string str) 
 { 
  if (buffer.Length < str.Length) 
  { 
   return false; 
  } 
  for (int bufIndex = buffer.Length-str.Length, strIndex = 0; 
   strIndex < str.Length; 
```
   bufIndex++, strIndex++) 
```csharp
  { 
   if (buffer[bufIndex] != str[strIndex]) 
   { 
    return false; 
   } 
  } 
  return true; 
 } 
} 
```
We need to test again  with our example. Now the program works. The 
result is correct: 
Word --> 9 
S --> 13 
MissingWord --> 0 
DS --> 2 
aa --> 3 
We test with all other tests we have (the test from the problem statement, 
the border cases, etc.) and all of them pass correctly.

## Testing for Performance

Now it is time to test for performance  both our solutions. We need a big 
test. We can do it with copy-paste. It is easy to c opy-paste the text from 
our text example 10,000 times  and its words 100 times.  The repeating 
words might cause inaccuracies in performance measuring so we manually 
replace the last 26 words with the letters from “a” to “z”. We also play a bit 
with the rectangular selection in Visual Studio  ([Alt] + mouse selection) 

and we insert the alphabet  as a vertical column  in few other places.  All this 
will result in 20,000 lines of text (1.2 MB) and 500 words (3 KB). 
To measure the execution time  we add two lines of code – before the first 
line of the Main() method and after the last line of the Main() method: 
```csharp
static void Main() 
{ 
 DateTime startTime = DateTime.Now; 
 // The original code goes here 
 Console.WriteLine(DateTime.Now - startTime); 
} 
```
Now we execute first the line by line algorithm and it seems not very fast. 
On average computer from 2008 it prints the following result: 
00:01:33.6393559 
After that we execute the char by char algorithm. It produces the 
following output: 
00:00:18.1080357 
Unbelievable! Our char by char processing algorithm is more than 5 times 
faster than the line by line processing algorithm!  But … it still is slow ! 18 
seconds for 1 MB file is not fast. How about processing 500 MB input and 
search for 10,000 words?

## Invent a Better Idea (Again)

If we are at exam, we could decide whether to take the risk to submit the 
char by char solution  or spend more time to think of faster algorithm . 
This depends on how muc h time we have to the end of the exam and how 
much problems we have already solved, how hard are the unsolved problems, 
etc. Suppose we have enough time and we want to think more. 
What makes our solution slow?  If we have 500 words, we check for each 
of them at each character. We do 500 * length(text) string comparisons. 
The text is scanned only once (char by char). This cannot be improved, right? 
If we do not scan the entire text, we will be unable to find all occurrences. So 
if we want to improve the perfo rmance, we should look how to check the 
words faster after each character is read, right? For 500 words we perform 
500 checks after each character is read. This is slow! Can’t we do it faster? 
In fact we perform a kind of searching for a matching word  in a  list of 
words? From the data structures we know that this takes linear time . Also, 
from the data structures we know that the fastest data structure for 
searching is the hash -table. OK, can’t we use a hash table? Instead of 

searching the words by trying ea ch of them one by one, can’t we directly find 
the word we need through a hast-table lookup? 
We take a sheet of paper  and the pencil and we start making sketches and 
thinking. Suppose we have the text  “passwords” and the word “ s”. We can 
check the word that we obtain when we append the letters one after another: 
p, pa, pas, pass, passw, passwo, passwor, password, passwords 
In this case we will not match the word “ s”, right. In fact, when we find a 
word in the text, we should check all its substrings in the hash table. For 
example if the text is “password”, all its substrings are: 
p, pa, a, pas, as, s, pass, ass, ss, s, passw, assw, ssw, sw, w, passwo, 
asswo, sswo, swo, wo, o, passwor, asswor, sswor, swor, wor, or, r, 
password, assword, ssword, sword, word, ord, rd, d, passwords, 
asswords, sswords, swords, words, ords, rds, ds, s 
There are 45 substrings of the word “password”. In a word of n characters we 
have n*(n+1)/2 substrings. This will work well with short words (e.g. 3 -4 
characters) and will be slow for the long words (e.g. 15-20 characters). 
We get into another idea? This multi-pattern matching problem should have 
a standard solution. Why don’t we search for it in Internet? We try to search 
for “multi-pattern matching algorithm” in Google and after exploring the 
first few results we learn about the “Aho-Corasick string matching algorithm ”. 
Once we know the algorithm name we search for “Aho Corasick C#”  and we 
find a nice C# implementation: https://github.com/tupunco/Tup.AhoCorasick. 
The theory says that after we have a new idea, we should check it for 
correctness. The best way to check this idea is by putting the code we found 
in action. In fact we do not implement the algorithm. We just try to adopt it to 
solve the problem we have.

## Counting Substrings with the Aho-Corasick Algorithm

From the open -source implementation of the Aho-Corasick multi-pattern 
string matching algorithm mentioned above we can take the class 
AhoCorasickSearch and put it in action. We write a new solution of the 
substring counting problem based on what we have learned from the previous 
solutions. We find all matches of all words by  the SearchAll(…) method of 
the AhoCorasickSearch class. Then we use a hash-table to count the number 
of occurrences for each of the words. To ensure we ignore the character 
casing we convert the text and the words into lowercase. This is the code: 
CountSubstringsAhoCorasick.cs 
```csharp
using System; 
using System.Collections.Generic; 

using System.Linq; 
using System.IO; 
 
class CountSubstringsAhoCorasick 
{ 
 static void Main() 
 { 
  DateTime startTime = DateTime.Now; 
 
  // Read the input list of words 
  string[] wordsOriginal = File.ReadAllLines("words.txt"); 
  string[] wordsLowercase = 
   wordsOriginal.Select(w => w.ToLower()).ToArray(); 
 
  // Read the text 
  string text = File.ReadAllText("text.txt").ToLower(); 
 
  // Find all word matches and count them 
  var search = new AhoCorasickSearch(); 
  var matches = search.SearchAll(text, wordsLowercase); 
  Dictionary<string, int> occurrences = 
   new Dictionary<string, int>(); 
  foreach (string word in wordsLowercase) 
  { 
   occurrences[word] = 0; 
  } 
  foreach (var match in matches) 
  { 
   string word = match.Match; 
   occurrences[word]++; 
  } 
 
  // Print the result 
  using (StreamWriter result = File.CreateText("result.txt")) 
  { 
   foreach (string word in wordsOriginal) 
   { 
    result.WriteLine("{0} --> {1}", word, 
     occurrences[word.ToLower()]); 
   } 
  } 
 
  Console.WriteLine(DateTime.Now - startTime); 
 } 

} 
```
We test the above code with all tests we already have and it seems to work 
correctly. We try the performance test and this time we can be amazed 
by its speed: 
00:00:00.6540374 
It runs really fast . This is the solution we needed and if we are allowed to 
use Internet at the exam, the best way to start when we have a standard 
well-known problem is to look for a well-known solution.