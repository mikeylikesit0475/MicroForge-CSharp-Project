# Exercises

Exercises 
1. Implement an extension method Substring(int index, int length) for 
the class StringBuilder that returns a new StringBuilder and has the 
same functionality as the method Substring(…) of the class String. 
2. Implement the following extension methods for the classes, implementing 
the interface IEnumerable<T>: Sum(), Min(), Max(), Average(). 
3. Write a class Student with the following properties: first name, last name 
and age. Write a method that for a given array of students finds those, 
whose first name is before their last one in alphabetical order. Use LINQ. 
4. Create a LINQ query that finds the first and the last name of all students, 
aged between 18 and 24 years including. Use the class Student from the 
previous exercise. 
5. By using the extension methods OrderBy(…) and ThenBy(…) with lambda 
expression, sort a list of students  by their first and last name in 
descending order. Rewrite the same functionality using a LINQ query. 
6. Write a program that prints to the consol e all numbers from a given array 
(or list), that are multiples of 7 and 3 at the same time . Use the built -
in extension methods  with lambda express ions and then rewrite the 
same using a LINQ query. 
7. Write an extension method for the class String that capitalizes all 
letters, which  are the beginning of a word in a sentence in English. For 
example: "this iS a Sample sentence. " should be converted to " This 
Is A Sample Sentence.". 
8. Create a hash -table to hold a phone book : a set  of person names and 
their phone numbers (e.g. Kate Wilson   +3592981981, +3598862536; 
Alex & Co.   1-800-ALEX; Steve Milton   +496023456). Fill enough 
random data (e.g. 50,000 key -value pairs). Measure how much time it 
takes to perform searching by key in the hash-table using its native search 
capabilities, using the extension methods IEnumerable.Contains(…) and 
IEnumerable.Where(…). Can you explain the difference?