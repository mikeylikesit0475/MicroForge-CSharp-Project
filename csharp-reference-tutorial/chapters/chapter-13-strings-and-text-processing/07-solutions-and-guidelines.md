# Solutions and Guidelines

Solutions and Guidelines 
1. Read in MSDN or refer to the start of this chapter. 
2. Use StringBuilder and for (or foreach) loop. 
3. Use counting of the brackets : For an opening bracket increase the 
counter by 1 and for closing bracket decrease it by 1. Watch the counter 
not to become a negative number and always ends with 0. 
4. If you do not know how many slashes you must use, try Split(…) with 
an increasing number of slashes until you reach the desired result. 
5. Reverse the casing of letters in text to small and search the given 
substring in a loop. Remember to use IndexOf(…) with a start index in 
order to avoid infinite loop. 
6. Use regular expressions or IndexOf(…) method for opening and closing 
tag. Calculate the start and end index of the text. Change the text in all 
capital letters and repl ace the entire substring opening tag + text + 
closing tag with the text in uppercase. 
7. Use the PadRight(…) method from the String class. 
8. Use format string " \u{0:x4}" for the Unicode character code for each 
character of the input string (you can get it by co nverting char to 
ushort). 
9. Let the cipher cipher consists of cipher.Length letters. Iterate through 
all letters in the text and encrypt the letter at position index in the text 
with cipher[index % cipher.Length]. If you have a letter from the 
text and letter from the cipher, we can perform XOR operation between 
them by transforming in advance the two letters into numbers of type 
ushort. We can print the result with "\u{0:x4}" format string. 
10. First split the senten ces from each other by using the Split(…) 
method. Then make sure that each sentence contains the searched 
word by searching for it as a substring with IndexOf(…) and if you find it 
check whether there is a separator ( character, which is not a letter or 
start / end of the string) on the left and on the right of the found 
substring. 
11. First, split the forbidden words  with the method Split(…) in order to 
get them as an array. For each forbidden word, iterate through the text 
and search for an occurrence . If a fo rbidden word is found, replace it 
with as many asterisks as letters contained in the forbidden word. 

 
Another, easier approach is to use RegEx.Replace(…) with a suitable 
regular expression and a suitable MatchEvaluator method. 
12. Use appropriate formatting strings. 
13. Use a regular expression  or search for the respective splitters – two 
slashes for a protocol and one slash as a separator between the server 
and the resource. Test the special cases like missing parts of the URL. 
14. You can solve the problem in two s teps: reverse the input string ; 
reverse each word in the result string. 
Another interesting approach is to split the input text by punctuation  
marks between words, in order to get just the words of the text and then 
split by the letters  to get the punctuat ion marks of the text. Thus, 
given a list of words and a list of punctuation marks between them, you 
can easily reverse the words, preserving the punctuation marks. 
15. You can parse the text  by splitting it by the new line character, then a 
second time by the "-" character. The most appropriate way to record the 
dictionary is in a hash table ( Dictionary<string, string>), which will 
provide a quick search for a given word. Read on the Internet for hash -
tables and the Dictionary<K,T> class. You might also check the chapter 
“Dictionaries, hash-Tables and Sets”. 
16. Using a regular expression is the easiest way to solve the task. 
If you still choose not to use regular expressions, you can find all 
substrings t hat start with " <a href= " and end with " </a>" and within 
them to replace "<a href="" with "[URL=", then the first found "">" with 
"]" and then "</a>" with "[/URL]". 
17. Use the methods in the DateTime structure. For parsing the dates you 
can use splitting by " ." or parsing with the DateTime.ParseExact(…) 
method. 
18. Use the DateTime.ToString() and DateTime.ParseExact() methods 
with suitable formatting strings. 
19. Use RegEx.Match(…) with an appropriate regular expression. 
If you want to solve the task without regular expressions, you will need 
to process the text letter by letter from start to finish and process the 
next character, depending on the current mode, which can be one of 
OutsideOfEmail, ProcessingSender or ProcessingHostOrDomain. If a 
separator or the end of the text is reached and host or domain is 
processed (mode ProcessingHostOrDomain), then you have found an e -
mail, otherwise potentially a new e -mail is starting and mode must be 
changed to ProcessingSender. If  @ character is reached in 
ProcessingSender mode, ProcessingSender is switched on. When 
meeting letters or dot in ProcessingSender or ProcessingHostOrDomain 
mode, they ate accumulated in a buffer. You can look at all possible 

groups of characters encountered respectively in each of the three modes 
and process them appropriately. We come to something like a final 
automaton (state machine), which detects e -mail addresses. All found e -
mail addresses must be checked whether they have nonempty recipient, 
host, and domain with a length between 2 and 4 letters, as well as not 
beginning or ending with a dot. 
Another easier approach to this problem is to split the text by all 
characters that are not letters and dots and to verify that the extracted 
"words" are valid e-mail addresses. Check can be done through an 
attempt to split them to nonempty parts: <sender>, <host>, <domain>, 
meeting the listed conditions. 
20. Use RegEx.Match(…) with an appropriate regular expression . 
Alternative option is to implement a state -machine that has several 
states OutOfDate, ProcessingDay, ProcessingMonth, ProcessingYear 
and while processing the text letter by letter to move between states 
according to the current letter which you are processing. As in the 
previous task, you can extract all "words" from the text in advance and 
then check which ones correspond to the date template. 
21. Split the text into words and check whether each word is a palindrome. 
22. Use an array of integers int[65536], which will keep how many times 
each letter occurs . Initi ally, all array elements are zeros. After 
processing the input string letter by letter you can write in the array how 
many times each letter occurs. For example, if you meet the letter ' A', 
the number of occurrences in the array index of 65 (Unicode code ' A') will 
increase by one. Finally, all non -zero elements (convert array index to 
char, to get the letter) and their number of occurrences can be printed 
with one scan of the array. 
23. Use a hash table ( Dictionary<string, int>) which keeps how many 
times each word occurs in the input string. Read on the Internet for class 
System.Collections.Generic.Dictionary<K,T>. With iteration through 
words you can accumulate information for each word occurrences in the 
hash table and with hash table iteration you can print the result. 
24. You can scan text from left to right and when the current letter is 
identical with the previous one, miss it, but otherwise concatenate it in 
StringBuilder. 
25. Use the static method Array.Sort(…) after parsing the input text into 
array of strings. 
26. Scan the text letter by letter  and at all times keep in a variable 
whether currently there is an opening tag which has not been closed or 
not. If you have "<", enter in "opening tag" mode. If you have ">", exit 
the "opening tag" mode. If you have a letter, add it to the result only if 
the program is not in "opening tag". After closing a tag you can add a 
space in order not to "stick" the text before and after the tag.