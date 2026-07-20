# Solutions and Guidelines

Solutions and Guidelines 
1. Use the examples discussed in this chapter. Use the using structure to 
ensure proper closing of the input and the resulting stream. 
2. You will have to first read the input file line by line  and save i t in the 
resulting file in overwrite mode. Then you must open the second input 
file and save it line by line in the result file in append mode. To create a 
StreamWriter in overwrite / use mode use the appropriate constructor 
(find it in MSDN). 
An alternati ve way is to read both files in a string with ReadToEnd(), 
store them in memory and save them in the resulting file. This approach 
does not work for large files (the likes of several gigabytes). 
3. Follow the examples in this chapter. Think of how you would solve the 
task if the file were large (several gigabytes). 
4. Follow the examples in this chapter . You will have to open both files 
simultaneously and read them line by line in  a loop. If you reach the end 
of the (read null) file, that does not match the other ’s, that means that 
both files have different number of rows and an exception should be 
thrown. 
5. Read the first line of the file and create a matrix with the specified size. 
After that read the following lines, line by line and separate the numbers. 
Then save them in the corresponding (row, column) in the matrix. Finally, 
find the sub-matrix using two nested loops. 
6. Write each read name in a list (List<string>), then sort it properly (look 
for information on the method Sort()) and then print it in the result file. 
7. Read the file line by line and use the methods of the class String. If you 
load the entire file into memory, instead of reading it line by line, 
problems when loading large files might occur. 
8. For every occurrence of “start”, check if that is the whole word or just a 
part of it. 
9. Look at the examples in this chapter. 
10. Read the input file character by character. When you encounter a " <", 
then this is a starting tag, but when you encounter a ">", that means a 
closing tag. All characters you encounter outside of the tags build up the 
text that must be extracted. You can store it in StringBuilder and print 
its contents when you encounter "<" or reach the end of the file. 
11. Read the file line by line  and replace words that start with " test" with 
an empty string. Use Regex.Replace(…) with an appropriate regular 
expression. Alternatively, you can search in the line the substring " test" 
and every time you find it, get all of its neig hboring letters to the left and 
right. This way you find the word in which the string " test" is part of and 
you can delete it if it begins with "test". 

12. The task is similar to the previous one . You can read the text line by 
line and replace each of the give n words with an empty string. Test 
whether your program properly handles exceptions by simulating different 
scenarios (e.g. no file, lack of rights for reading and writing, etc.). 
13. Create a hash table with keys – the words from words.txt and 
value number of  occurrences of each word ( Dictionary<string, 
int>). Firstly, save to the hash table that all words are found 0 times. 
Then read the file line by line and split each line into words. Check 
whether each obtained word can be found in the hash table, and if s o, 
add 1 to the number of occurrences. Finally, save all the words and their 
number of occurrences in an array of type  KeyValuePair<string, int>. 
Sort the array with a suitable comparison function like so: 
Array.Sort<KeyValuePair<string, int>>( 
 arr, (a, b) => a.Value.CompareTo(b.Value));