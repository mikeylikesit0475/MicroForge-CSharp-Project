# Exercises

Exercises 
1. Describe the strings in C# . What is typical for the string type? 
Explain which the most important methods of the string class are. 
2. Write a program that reads a string, reverse it and prints it to the 
console. For example: "introduction"  "noitcudortni". 
3. Write a program that checks whether the parentheses are pla ced 
correctly in an arithmetic expression. Example of expression with 
correctly placed brackets: ((a+b)/5-d). Example of an incorrect 
expression: )(a+b)). 
4. How many backslashes you must specify as an argument to the method 
Split(…) in order to split the text by a backslash? 
Example: one\two\three. 

Note: In C# backslash is an escaping character. 
5. Write a program that detects how many times a substring is contained in 
the text. For example, let’s look for the substring "in" in the text: 
We are living in a yellow submarine. We don't have anything 
else. Inside the submarine is very tight. So we are drinking 
all the day. We will move out of it in 5 days. 
The result is 9 occurrences. 
6. A text is given. Write a program that modifies the casing  of letters to 
uppercase at all places in the text surrounded by <upcase> and 
</upcase> tags. Tags cannot be nested. 
Example: 
We are living in a <upcase>yellow submarine</upcase>. We 
don't have <upcase>anything</upcase> else. 
Result: 
We are living in a YELLOW SUBMARINE. We don't have ANYTHING 
else. 
7. Write a program that reads a string from the console (20 characters 
maximum) and if shorter complements it right with "*" to 20 characters. 
8. Write a program that converts a given string into the form of array of 
Unicode escape sequences in the format used in the C# language. 
Sample input: "Test". Result: "\u0054\u0065\u0073\u0074". 
9. Write a program that encrypts a text  by applying XOR (excluding or) 
operation between the given source characters and given cipher code. 
The encryption should be done by applying XOR between the first letter 
of the text and the first letter of the code, the second letter of the text 
and the second letter of the code, etc. until the last letter of the code, 
then goes back to the first letter of the code  and the next letter of the 
text. Print the result as a series of Unicode escape characters \xxxx. 
Sample source text: "Test". Sample cipher code: "ab". The result should 
be the following: "\u0035\u0007\u0012\u0016". 
10. Write a program that extracts from a text all sentences that contain 
a particular word . We accept that the sentences are separated from 
each other by the character "." and the words are separated from one 
another by a character which is not a letter. Sample text: 
We are living in a yellow submarine. We don't have anything 
else. Inside the submarine is very tight. So we are drinking 

 
all the day. We will move out of it in 5 days. 
Sample result: 
We are living in a yellow submarine. 
We will move out of it in 5 days. 
11. A string is given, composed of several "forbidden" words separated by 
commas. Also a text is given, containing those words. Write a program 
that replaces the forbidden words with asterisks. Sample text: 
Microsoft announced its next generation C# compiler today. 
It uses advanced parser and special optimizer for the 
Microsoft CLR. 
Sample string containing the forbidden words: "C#,CLR,Microsoft". 
Sample result: 
********* announced its next generation ** compiler today. 
It uses advanced parser and special optimizer for the 
********* ***. 
12. Write a program that reads a number from console and prints it in 15-
character field, aligned right  in several ways: as a decimal number, 
hexadecimal number, percentage, currency and exponential (scientific) 
notation. 
13. Write a program that parses an URL in following format: 
[protocol]://[server]/[resource] 
It should extract from the URL the protocol, server and resource parts. 
For example, when http://www.cnn.com/video is passed, the result is: 
[protocol]="http" 
[server]="www.cnn.com" 
[resource]="/video" 
14. Write a program that reverses the words in a given sentence  without 
changing punctuation and spaces. For example: " C# is not C++ and 
PHP is not Delphi"  "Delphi not is PHP and C++ not is C#". 
15. A dictionary is given, which consists of several lines of text. Each line 
consists of a word and its explanation, separated by a hyphen: 
.NET – platform for applications from Microsoft 

CLR – managed execution environment for .NET 
```csharp
namespace – hierarchical organization of classes 
```
Write a program that parses the dictionary and then reads words from 
the console in a loop, gives an explanation  for it or writes a message 
on the console that the word is not into the dictionary. 
16. Write a program that replaces all hyperlinks  in a HTML document 
consisting of <a href="…">…</a> and hyperlinks in "forum" style, which 
look like [URL=…]…[/URL]. 
Sample text: 
<p>Please visit <a href="http://softuni.org">our site</a> to 
choose a training course. Also visit <a href= 
"http://forum.softuni.org">our forum</a> to discuss the 
courses.</p> 
Sample result: 
<p>Please visit [URL=http://softuni.org]our site[/URL] to 
choose a training course. Also visit [URL= 
http://forum.softuni.org]our forum[/URL] to discuss the 
courses.</p> 
17. Write a program that reads two dates  entered in the format 
"day.month.year" and calculates the number of days between them. 
Enter the first date: 27.02.2006 
Enter the second date: 3.03.2006 
Distance: 4 days 
18. Write a program that reads the date and time entered in the format 
"day.month.year hour:minutes:seconds" and prints the date and time 
after 6 hours and 30 minutes in the same format. 
19. Write a program that extracts all e-mail addresses from a text. These 
are all substrings that are limited on both sides by text end or separator 
between words and match the shape <sender>@<host>…<domain>. 
Sample text: 
Please contact us by phone (+001 222 222 222) or by email at 
example@gmail.com or at test.user@yahoo.co.uk. This is not 
email: test@test. This also: @gmail.com. Neither this: 
a@a.b. 
Extracted e-mail addresses from the sample text: 

 
example@gmail.com 
test.user@yahoo.co.uk 
20. Write a program that extracts from a text all dat es written in format 
DD.MM.YYYY and prints them on the console in the standard format for 
Canada. Sample text: 
I was born at 14.06.1980. My sister was born at 3.7.1984. In 
5/1999 I graduated my high school. The law says (see section 
7.3.12) that we are allowed to do this (section 7.4.2.9). 
Extracted dates from the sample text: 
14.06.1980 
3.7.1984 
21. Write a program that extracts from a text all words which are 
palindromes, such as ABBA", "lamal", "exe". 
22. Write a program that reads a string from the console and prints in 
alphabetical order all letters from the input string and how many 
times each one of them occurs in the string. 
23. Write a program that reads a string from the console and prints in 
alphabetical order all words from the input string  and how many 
times each one of them occurs in the string. 
24. Write a program that reads a string from the console and replaces every 
sequence of identical letters in it with a single letter (the repeating 
letter). Example: "aaaaabbbbbcdddeeeedssaa"  "abcdedsa". 
25. Write a program that reads a list of words separated by commas from the 
console and prints them in alphabetical order (after sorting). 
26. Write a program that extracts all the text without any tags and 
attribute values from an HTML document. 
Sample text: 
<html> 
 <head><title>News</title></head> 
 <body><p><a href="http://softuni.org">Software 
  University</a>aims to provide free real-world practical 
  training for young people who want to turn into 
  skillful software engineers.</p></body> 
</html> 
Sample result: 

News 
Software University aims to provide free real-world 
practical training for young people who want to turn into 
skillful software engineers.