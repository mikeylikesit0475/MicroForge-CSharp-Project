# Exercises

Exercises 
1. Write a program that reads a text file and prints its odd lines  on the 
console. 
2. Write a program that joins two text files  and records the results in a 
third file. 
3. Write a program that reads the contents of a text file and inserts the 
line numbers  at the beginning of each line, then rewrites the file 
contents. 
4. Write a program that compares two text files with the same number of 
rows line by line and prints the number of equal and the number of 
different lines. 
5. Write a program that reads a square matrix of integers from a file and 
finds the sub-matrix with size 2 × 2 that has the maximal sum and 
writes this sum to a separate te xt file. The first line of input file contains 
the size of the recorded matrix (N). The next N lines contain N integers 
separated by spaces. 
Sample input file: 
4 
2 3 3 4 

0 2 3 4 
3 7 1 2 
4 3 3 2 
Sample output: 17. 
6. Write a program that reads a list of names  from a text file, arranges 
them in alphabetical order, and writes them to another file. The lines 
are written one per row. 
7. Write a program that replaces every occurrence of the substring  
"start" with " finish" in a text file. Can you rewrite the program to  
replace whole words only? Does the program work for large files (e.g. 800 
MB)? 
8. Write the previous program so that it changes only the whole words  
(not parts of the word). 
9. Write a program that deletes all the odd lines of a text file. 
10. Write a program that extracts from an XML file the text only (without the 
tags). Sample input file: 
<?xml version="1.0"><student><name>Peter</name> 
<age>21</age><interests count="3"><interest> 
Games</interest><interest>C#</interest><interest> 
Java</interest></interests></student> 
Sample output: 
Peter 
21 
Games 
C# 
Java 
11. Write a program that deletes all words that begin with the word "test". 
The words will contain only the following chars: 0…9, a…z, A…Z. 
12. A text file words.txt is given, containing a list of words, one per line. 
Write a program that deletes in the file text.txt all the words that 
occur in the other file. Catch and handle all possible exceptions. 
13. Write a program that reads a list of words from a file called words.txt, 
counts how many times each of these words is found in another 
file text.txt, and records the results in a third file – result.txt, but 
before that, sorts them by the number of occurrences in descending 
order. Handle all possible exceptions.