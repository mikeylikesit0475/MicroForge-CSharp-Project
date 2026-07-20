# Strings Operations

After getting familiar with the strings seman tics and how we can create and 
print them, next comes to learn how to deal with them and how to process 

 
them. The C# language gives us a number of operations ready for use, which 
we will use for manipulating the strings.

## Comparing Strings in Alphabetical Order

There are many ways to compare strings  and depending on what exactly 
we need in the particular case, we can take advantage of the various features 
of the string class.

### Comparison for Equality

If the requirements are to compare the two strings  in order to determine 
whether their values are equal or not , the most convenient method is the 
Equals(…), which works equivalently to the operator ==. It returns a 
Boolean result with either true value, if the strings have the same values, or 
false value, if the y are different. The method Equals(…) checks letter by 
letter for equality of string values, as it makes distinction between small and 
capital letters, i.e. comparing the "c#" and "C#" with the Equals(…) method 
will return the value false. Consider the following example: 
string word1 = "C#"; 
string word2 = "c#"; 
```csharp
Console.WriteLine(word1.Equals("C#")); 
Console.WriteLine(word1.Equals(word2)); 
Console.WriteLine(word1 == "C#"); 
Console.WriteLine(word1 == word2); 
 
// Console output: 
// True 
// False 
// True 
// False 
```
In practice, we often are interested of  only the actual text content when 
comparing two strings, regardless of the character casing (uppercase / 
lowercase). To ignore the difference between small and capital letters in string 
comparison we can use  the method  Equals(…) with the parameter 
StringComparison.CurrentCultureIgnoreCase. So now in the same 
example of comparing "C#" with "c#" the method will return the value true: 
```csharp
Console.WriteLine(word1.Equals(word2, 
 StringComparison.CurrentCultureIgnoreCase)); 
// True 
```
StringComparison.CurrentCultureIgnoreCase is a constant of the 
enumerated type StringComparison. What is enumerated type and how we 
can use it, we will learn in the chapter "Defining Classes".

### Comparing Strings in Alphabetical Order

It has become clear how we compare strings for equality, but how we are 
going to establish the lexicographical order of several strings? If we try to 
use the operators < and > which work great for comparing numbers, we find 
out that they cannot be used for strings. 
If you want to compare two words and get information which one of them is 
before the other according to th eir alphabetical order of letters, here comes 
the method CompareTo(…). It allows us to compare the values of two strings 
in order to determine their lexicographical order. In order two strings to have 
the same values, they must have the same length (number  of characters) and 
the all their characters should match accordingly. For example, the strings 
"give" and "given" are different because they differ in their lengths, and 
"near" and "fear" differ in their first character. 
The method CompareTo(…) from the String class returns a negative value, 0 
or positive value depending on the lexical order of the two compared strings. 
A negative value means that the first string is lexicographically before the 
second, zero means that the two strings are equal and positiv e value means 
that the second string is lexicographically before the first. To clarify better 
how to compare strings lexicographically, let’s go through a few examples: 
string score = "sCore"; 
string scary = "scary"; 
 
```csharp
Console.WriteLine(score.CompareTo(scary)); 
Console.WriteLine(scary.CompareTo(score)); 
Console.WriteLine(scary.CompareTo(scary)); 
 
// Console output: 
// 1 
// -1 
// 0 
The first experiment is called the method CompareTo(…) of the string score, 
```
as passed parameter is the variable scary. The first digit returns equal sign. 
Because the method does not ignore the casing of small and capital letters, 
it finds mismatch in the second character (in the first string it is "C", while in 
the second it is  "c"). This is enough to determine the arrangement of strings 
and CompareTo(…) returns +1. Calling the same method with swapped places 
of the strings returns -1, because then the starting point is the string scary. 
His final call returns a logical 0, because we compare scary with itself. 
If we have to compare the strings lexicographically , namely to ignore 
the letters cas ing, then we could use string.Compare(string strA, 
string strB, bool ignoreCase). This is a static method, which works in the 
same way as CompareTo(…), but it has an ignoreCase option for ignoring the 
casing of capital and small letters. Let’s look at the method in action: 

 
string alpha = "alpha"; 
string score1 = "sCorE"; 
string score2 = "score"; 
 
```csharp
Console.WriteLine(string.Compare(alpha, score1, false)); 
Console.WriteLine(string.Compare(score1, score2, false)); 
Console.WriteLine(string.Compare(score1, score2, true)); 
Console.WriteLine(string.Compare(score1, score2, 
 StringComparison.CurrentCultureIgnoreCase)); 
// Console output: 
// -1 
// 1 
// 0 
// 0 
```
In the last example the method Compare(…) takes as a third parameter 
StringComparison.CurrentCultureIgnoreCase – already well -known from 
the method Equals(…) through which we can also compare strings, without 
having to register the difference between the small and capital letters. 
Please note that according to the methods  Compare(…) and CompareTo(…) 
the small letters are lexicographically before the capital on es. The 
correctness of this rule is quite controversial as in the Unicode table the 
capital letters are before the small ones. For example due to the standard 
Unicode, the letter “ A” has a code 65, which is smaller than the code of the 
letter “a” (97). 
 
When you want just to consider whether the values of two 
strings are equal or not, please use the method Equals(…) or 
the operator ==. The methods CompareTo(…) and string. 
Compare(…) are designed to be used when the lexicographical 
order is needed. 
Therefore, you should consider that the lexicographical comparison does 
not follow the letter arrangement in the Unicode table . Other 
abnormalities can also be caused by special features of the current culture. 
For some languages like German the characters "ss" and "ß" are considered 
equal. For example the words "Straße" and "Strasse" are considered the same 
by CompareTo(…) and equal when compared through the == operator: 
string first = "Straße"; 
string second = "Strasse"; 
 
```csharp
Console.WriteLine(first == second); // False 
Console.WriteLine(first.CompareTo(second)); // 0 – equal strings
```

### The == and != Operators

In the C# language the operators == and =! work for strings through an 
internal calling of Equals(…). We will go through some examples for using 
those two operators with variables from the string type: 
string str1 = "Hello"; 
string str2 = str1; 
 
```csharp
Console.WriteLine(str1 == str2); 
// Console output: 
// True 
```
The comparison of matching strings str1 and str2 returns true. This is a 
fully expected result, since the target variable str2 is pointed to the dynamic 
memory that is reserved for the variable str1. Thus, both variables have the 
same address and the check for equality returns true. Presented is how the 
memory looks like with the two variables: 
 
Let’s look at another example: 
string hel = "Hel"; 
string hello = "Hello"; 
string copy = hel + "lo"; 
 
```csharp
Console.WriteLine(copy == hello); 
// True 
```
Pay attention to the comparison between the strings hello and copy. The 
first variable takes directly the value "Hello". The second takes its value as a 
result of joining a variable with literal, and the final result is equivalent to the 
value of the first variable. At this stage the two variables point to different 
areas of memory,  but the contents of the memory blocks are identical. The 
comparison made with the operator == returns a result true, although both 
variables point to different areas of memory. 
Here is how the memory looks like at this point: 
HeapStack
string@8a4fe6
str1
Hello
string@8a4fe6
str2

### Memory Optimization for Strings (Interning)

Let’s consider the following example: 
string hello = "Hello"; 
string same = "Hello"; 
Let’s create a variable with value " Hello". We also create a second variable 
assigning it a value the same literal. It is logical when creating the variable 
hello, to allocate space in the heap, to write its value and the variable to 
point to that location. When creating the same a new place to record should be 
allocated too, the value should be written and the reference to the memory 
should be directed. 
But the truth is that there is an optimization in the C# compiler and in CLR, 
which saves the memory from creating duplicated strings . This 
optimization is called strings interning and thanks to it the two variables in 
the memory will be pointed to the same common block of memory. This 
reduces the memory space usage and optimizes certain operations such as 
comparing two completely matching strings.  They are written in the memory 
in the following way: 
 
When we initialize a variable of type string with a string literal, the memory 
checks invisibly for us whether this value already exists. If the value already 
exists, the new variable is simply pointe d to it. If not, a new block of memory 
is allocated, the value is stored in it and the reference is changed to point to 
HeapStack
string@6e278a
hel
Hel
string@2fa8fc
hello
Hello
string@a7b46e
copy
Hello
HeapStack
string@a8fe24
hello
Hello
string@a8fe24
same

the new block. The string interning in .NET is possible because strings are 
immutable by design and it is not likely that the memory block referenced 
by several string variables will simultaneously be changed by someone. 
When not initializing the strings with literals, no interning is used. However, if 
we want to use interning specifically, we can m ake it through the use of the 
method Intern(…): 
string declared = "Intern pool"; 
string built = new StringBuilder("Intern pool").ToString(); 
string interned = string.Intern(built); 
 
```csharp
Console.WriteLine(object.ReferenceEquals(declared, built)); 
Console.WriteLine(object.ReferenceEquals(declared, interned)); 
// Console output: 
// False 
// True 
```
Here is the memory situation at this moment: 
 
In the example we used the static method Object.ReferenceEquals(…), 
which compares two objects in memory and returns whether they point to the 
same memory block. We used the class StringBuilder, which serves to 
efficiently build strings. When and how to use StringBuilder we will explain 
in detail s shortly, but now let ’s get familiar with the basic operations on 
strings.

## Operations for Manipulating Strings

Once we got familiar with the fundamentals of strings and their structure, the 
next thing to explore a re the tools for their processing. We will review string 
concatenation, searching in a string , extracting substrings, change the 
character casing, splitting a string by separator and other string operations 
that will help us solve various problems from the everyday practice. 
HeapStack
string@6e278a
declared
Intern pool
string@6e278a
interned
string@a7b46e
built
Intern pool

 
 
Strings are immutable! Any change of a variable of type 
string creates a new string in which the result is stored. 
Therefore, operations that apply to strings return as a result 
a reference to the result. 
It is possible to process strings without creating new objects in the memory 
every time a modification is made but for this purpose the class 
StringBuilder should be used. We will introduce it a bit later.

## Strings Concatenation

Gluing two strings and obtaining a new one as a result is called 
concatenation. It could be done in several ways: through the method 
Concat(…) or with the operators + and +=. 
Example of using the method Concat(…): 
string greet = "Hello, "; 
string name = "reader!"; 
string result = string.Concat(greet, name); 
By calling the method, we will concatenate the  string variable name, which is 
passed as an argument, to the string variable greet. The result string will be  
the text "Hello, reader!". 
The second way for concatenation is via the operators + and +=. Then the 
above example can be implemented in the following way: 
string greet = "Hello, "; 
string name = "reader!"; 
string result = greet + name; 
In both cases those variables will be presented in the memory as follows: 
 
HeapStack
0x00122F680
greet
Hello, 
0x003456FF
name
reader!
0x00AD4934
result
Hello, reader!

Please note that string concatenation does not change the existing strings 
but returns a new string as a result. If we try to concatenate two strings 
without storing them in a variable, the changes would not be saved. Here is a 
typical mistake: 
string greet = "Hello, "; 
string name = "reader!"; 
string.Concat(greet, name); 
In the given example the two v ariables are concatenated but the result of it 
has not been saved anywhere, so it is lost: 
If we want to add a value to an existing variable, for example the variable 
result, we can do it with the well-known code: 
result = result + " How are you?"; 
In order to avoid the double writing of the above declared variable, we can 
use the operator +=: 
result += " How are you?"; 
The result will be the same in both cases: "Hello, reader! How are you?". 
We can concatenate other data with strings. Any data,  which can be 
presented in a text form , can be appended to a string . Concatenation is 
possible with numbers, characters, dates, etc. Here is an example: 
string message = "The number of the beast is: "; 
int beastNum = 666; 
string result = message + beastNum; 
// The number of the beast is: 666 
As we understood from the above example, there is no problem in 
concatenating strings with other data, which is not from a string type. Let’s 
have another full example for string concatenation: 
```csharp
public class DisplayUserInfo 
{ 
 static void Main() 
 { 
  string firstName = "John"; 
  string lastName = "Smith"; 
  string fullName = firstName + " " + lastName; 
 
  int age = 28; 
  string nameAndAge = "Name: " + fullName + "\nAge: " + age; 

 
 
  Console.WriteLine(nameAndAge); 
 } 
} 
// Console output: 
// Name: John Smith 
// Age: 28
```

## Switching to Uppercase and Lowercase Letters

Sometimes we need to change the casing of a string  so that all the 
characters in it to be entirely uppercase or lowercase . The two methods 
that would work best in this case are ToLower(…) and ToUpper(…). The first 
converts all capital letters to small ones: 
string text = "All Kind OF LeTTeRs"; 
 
```csharp
Console.WriteLine(text.ToLower()); 
// all kind of letters 
```
The example shows that all capital letters of the text change their casing and 
the entire text goes in lowercase. Such a shift to lower case is convenient for 
storing usernames in various online systems. Upon registration the users may 
use a mixture of uppe rcase and lowercase letters, but the system can then 
make them all small to unify them and to avoid matches on points with 
differences in the casing. 
Here is another example. We want to compare entered by the user input but 
we are not sure exactly how it w as written – in small or capital letters or 
mixed. One possible approach is to standardize capitalization and compare it 
with the constant defined by us. Thus, we make no distinction of small 
and capital letters . For example, if we have a user input panel where we 
enter name and password and it does not matter if the password is written 
with capital letters or small, we can make a similar check on the password: 
string pass1 = "PasswoRd"; 
string pass2 = "PaSSwoRD"; 
string pass3 = "password"; 
 
```csharp
Console.WriteLine(pass1.ToUpper() == "PASSWORD"); 
Console.WriteLine(pass2.ToUpper() == "PASSWORD"); 
Console.WriteLine(pass3.ToUpper() == "PASSWORD"); 
 
// Console output: 
// True 

// True 
// True 
```
In the example we are comparing three passwords with the same content but 
with a different casing. When checking their contents, always verify if it 
equals to the string " PASSWORD" (letter by letter). Of course, we could do 
the above verification and by th e method Equals(…) in the version with 
ignoring the character casing, which we already discussed.

## Searching for a String within Another String

When we have a string with a specified content, it is often necessary  to 
process only a part of its value. The .NET platform provides us with two 
methods to search a string within another string : IndexOf(…) and 
LastIndexOf(…). They search into the string and check whether the passed 
as a parameter substring occurs in its co ntent. The result of those methods is 
an integer. If the result is not a negative value, then this is the position where 
the first character of the substring is found. If the method returns value of -1, 
it means that the substring was not found. Remember t hat in C# index ing 
into strings start from 0. 
The methods IndexOf(…) and LastIndexOf(…) search the contents of the 
text sequence, but in a different direction. The search with the first method 
starts from the beginning of the string towards the end, while the second 
method – the search is done backwards. If we are interested in the first 
encountered match, then we use IndexOf(…). If we want to search the string 
from its end (for example to detect the last dot in a file name or the last slash 
in an URL address), then we use LastIndexOf(…). 
When calling IndexOf(…) and LastIndexOf(…) a second parameter could be 
passed, which will specify the position, which the searching should start from. 
This is useful if we want to search part of a string, not the entire string.

### Searching into a String – Example

Let’s consider an example with the IndexOf(…) method: 
string book = "Introduction to C# book"; 
int index = book.IndexOf("C#"); 
 
```csharp
Console.WriteLine(index); 
// index = 16 
```
In the example, the variable book has a value " Introduction to C# book ". 
The search for the substring " C" in this variable will return the value 16, 
because the substring will be found and the first character "C" of the searched 
word is in 16th position.

### Searching with IndexOf(…) – Example

Let’s look into great details one more example for searching for a separate 
characters or strings in a text: 
string str = "C# Programming Course"; 
 
int index = str.IndexOf("C#"); // index = 0 
index = str.IndexOf("Course"); // index = 15 
index = str.IndexOf("COURSE"); // index = -1 
index = str.IndexOf("ram");    // index = 7 
index = str.IndexOf("r");      // index = 4 
index = str.IndexOf("r", 5);   // index = 7 
index = str.IndexOf("r", 10);  // index = 18 
Look how the string we are searching looks like in the memory: 
 
If we look at the results of the third search, we will note that the search for 
the word " COURSE" in the text returned a result of -1, i.e. no match has 
been found. Although the word is in the text, it has been written in a different 
case of letters. The methods IndexOf(…) and LastIndexOf(…) distinguish 
between uppercase and lowercase letters. If we want to ignore this difference, 
we can write text in a new variable and turn it to a text with entirely lower or 
entirely uppercase, and then we can perform the search in it, independently 
from the letters casing.

### Finding All Occurrences of a Substring – Example

Sometimes we want to find all occurrences of a particular substring 
within another string . Using both methods with only one searched string 
passed as an argument would not work for us, because it will always return 
only the first occurrence of the substring. We can pass a second parameter for 
an index that indicates the starting position from which the searching should 
begin. Of course, we need to loop through it in order to move from the first 
occurrence of the searched string to the next, to the ne xt, and the next, etc., 
until the last one. 
Here is an example how we can use the method IndexOf(…) by a given word 
and start index: finding all occurrences of the word "C#" in a given text: 
string quote = "The main intent of the \"Intro C#\"" + 
 " book is to introduce the C# programming to newbies."; 
HeapStack
string@821a48
str
C # P r o g r a m m i n g C o u r s e
0  1  2  3  4 5  6  7  8  9 10 11 12 13 14 15 16 17 18 20 21

string keyword = "C#"; 
int index = quote.IndexOf(keyword); 
 
while (index != -1) 
```csharp
{ 
 Console.WriteLine("{0} found at index: {1}", keyword, index); 
 index = quote.IndexOf(keyword, index + 1); 
} 
The first step is to make a search for the keyword "C#". If the word is found 
```
in the text (i.e. the returned value is different than -1), it prints it on the 
console and we continue our search rightwards, starting from the position on 
which we have found t he word plus one. We repeat this operation until 
IndexOf(…) returns value -1. 
Note: If we miss setting an initial index, then the search will always start from 
the beginning and will return one and the same value. This will lead to 
hanging of the program. If we search directly from the index without 
adding plus one each time, we will come across again and again to the last 
result, whose index we have already found. Therefore, proper search of the 
next result should start from a starting position index + 1.

## Extracting a Portion of a String

For now we know how to check whether a substring occurs in a text and 
which are the occurrence positions. But how can we extract a portion of a 
string in a separate variable? 
The solution of this problem is the method Substring(…). By using it, we can 
extract a part of the string (substring)  by a given starting position in the 
text and its length. If the length is omitted, a portion from the text will be 
extracted, starting from the initial position to the string’s end. 
Presented is an example of extracting a substring from a string: 
string path = "C:\\Pics\\CoolPic.jpg"; 
string fileName = path.Substring(8, 7); 
// fileName = "CoolPic" 
We manipulate the variable path. It contains the path to a file from our file 
system. To assign the file name to a new variable, we use Substring(8, 7) 
and take a sequence of 7 characters starting from the 8 th position, i.e. 
character positions from 8 to 14 inclusively. 
 
Calling the method Substring(startIndex, length), extracts a 
substring from a string, which is located between startIndex 
and (startIndex + length – 1) inclusively. The character at  


 
the position startIndex + length is not taken into considera -
tion! For example, if we point Substring(8, 3), the characters 
between index 8 and 10 inclusively will be extracted. 
Here are presented the characters, which form the text from which we extract 
a substring: 
0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 
C : \ P i c s \ C o o l P i c . j p g 
Sticking to the scheme, the method that has been called must write the 
characters from the positions 8 to 14 (as the last index is not included), 
namely "CoolPic".

### Extracting a File Name and File Extension – Example

Let’s consider a more interesting task. How can we print the filename and 
its extension from given full path to a file in Windows -based file system? As 
we know how the path is recorded in the file system, we can proceed with the 
following plan: 
- Looking for the last backslash in the text; 
- Keeping the position of the last backslash; 
- Extracting the substring starting from the obtained position +1; 
Let’s consider again the example of the well -known file path. If we have no 
information about the exact contents of the  variable, but we know that it 
contains a file path, we can stick to the above scheme: 
string path = "C:\\Pics\\CoolPic.jpg"; 
int index = path.LastIndexOf("\\"); 
// index = 7 
string fullName = path.Substring(index + 1); 
// fullName = "CoolPic.jpg"

## Splitting the String by a Separator

One of the most flexible methods for working with strings is Split(…). It 
allows us to split a string by a separator or an array of possible separators. 
For example, we can process a variable, which has the following content: 
string listOfBeers = "Amstel, Heineken, Tuborg, Becks"; 
How can we split each beer in a separate variable or extract all beers in an 
array? At first glance it may seem difficult – we must seek with IndexOf(…) 
for a special character, then to extract a substring with Substring(…), to 

iterate all this in a loop and to write the result in a variable. Since the splitting 
of a string by a separator is a main task of text processing, ready to use 
methods for it can be found in .NET Framework.

### Splitting Strings by Multiple Separators – Example

The easiest and more flexible method for resolving this issue is the following:  
char[] separators = new char[] {' ', ',', '.'}; 
string[] beersArr = listOfBeers.Split(separators); 
Using the built-in functionality of the method Split(…) from the class String, 
we will split the contents of a given string by array of characters  – separators, 
which are passed as an argument of the method. All substrings among which 
are space, comma or dot will be removed and stored in the beersArr array. 
If we iterate the array and print its elements one by one, the result will be: 
"Amstel", "", "Heineken", "", "Tuborg", "" and "Becks".  We get 7 results, 
instead of the expected 4. The reason is that during the text splitting, three 
substrings are found which contain two separator characters one next to the 
other (for example a comma, followed by a space). In this case the empty 
string between the two separators is also part of the returned result.

### How to Remove the Empty Elements after Splitting?

If we want to ignore the empty strings  from the splitting results , one possible 
solution is to make checks on their printing: 
foreach (string beer in beersArr) 
```csharp
{ 
 if (beer != "") 
 { 
  Console.WriteLine(beer); 
 } 
} 
```
But this approach does not remove the empty strings from the array. It just 
does not print them. So we can change the arguments we are passing to the 
method Split(…), by passing a special option: 
string[] beersArr = listOfBeers.Split( 
 separators, StringSplitOptions.RemoveEmptyEntries); 
After this change, the beersArr array will contain 4 elements – the 4 words 
from the listOfBeers variable. 
 
When splitting strings and adding as a  second parameter the 
constant StringSplitOptions.RemoveEmptyEntries we instruct 


 
the method Split(…) to work in the following way: “Return 
all substrings from the variable that are split by given list of 
separators. If you meet two or more neighboring separators, 
consider them as one.”

## Replacing a Substring

The text processing in .NET Framework provides ready methods for replacing 
a substring with another. For example, if we have made one and the same 
technical mistake when typing the email address of a user in an official 
document, we can replace it by using the method Replace(…): 
string doc = "Hello, some@gmail.com, " + 
 "you have been using some@gmail.com in your registration."; 
string fixedDoc = 
 doc.Replace("some@gmail.com", "john@smith.com"); 
```csharp
Console.WriteLine(fixedDoc); 
 
// Console output: 
// Hello, john@smith.com, you have been using 
// john@smith.com in your registration. 
```
As it can be seen from the example, the method Replace(…) replaces all 
occurrences of a given substring with another substring, not just the first.

## Regular Expressions

The regular expressions are a powerful tool for text processing and allow 
searching matches by a pattern. An example for a pattern is [A-Z0-9]+, 
which means not an empty series of capital Latin letters and numbers. 
Regular expressions make text processing easier and more accurate : 
extracting some resources from texts, searching for phone numbers, finding 
email addresses in a text, splitting all the words in a sentence, data 
validation, etc.

### Regular Expressions – Example

If we have an official document that is used only in the office and it contains a 
lot of personal data, then we should censor it before sending it to the client. 
For example, we can censor all mobile numbers and replace them with 
asterisks. By using regular expressions, this could be done as follows: 
string doc = "Smith's number: 0898880022\nFranky can be " + 
 "found at 0888445566.\nSteven's mobile number: 0887654321"; 
string replacedDoc = Regex.Replace( 
 doc, "(08)[0-9]{8}", "$1********"); 

```csharp
Console.WriteLine(replacedDoc); 
// Console output: 
// Smith's number: 08******** 
// Franky can be found at 08********. 
// Steven' mobile number: 08********
```

### Explaining the Arguments of Regex.Replace(…)

In the above code fragment by using a regular expression, we find all the 
phone numbers specified in the text and replace them by a pattern. We use 
the class System.Text.RegularExpressions.Regex, which is intended for 
use with regular expressions in .NET F ramework. The variable, which imitates 
the document text, is doc. Several names of customers are recorded there. If 
we want to protect the contacts from an improper use and wish to censor the 
phone numbers, then we can replace all mobile phones with asteri sks. 
Assuming that the phones are saved in the following format: " 08 + 8 digits", 
the method Regex.Replace(…) finds all matches by a given format and 
replaces them with: "08********". 
The regular expression that finds all of the numbers is the following: "(08)[0-
9]{8}". It finds all substrings in the text, constructed by the constant " 08" 
and followed exactly by 8 characters ranging from 0 to 9. The example can be 
further improved by selecting the numbers only from a given mobile operator, 
for phones on for eign networks, etc., but in this case we used the simplified 
version. 
The literal " 08" is surrounded by parentheses. They serve for forming a 
separate group in the regular expression. The groups can be used for handling 
only a certain part of the expressio n instead of the entire expression. In our 
example, the group is used in the substitution. Through it, the founded 
matches are replaced by the pattern " $1********", i.e. the text which was 
found in the first group of the regular expression ( $1) + 8 consecu tive 
asterisks for censorship. As the defined group is always a constant (08), so 
the text replaced will always be: 08 ********. 
This chapter is not intended to explain in details how to use regular 
expressions in .NET Framework, as it is a huge and complex field, but only 
to turn the reader’s attention that the regular expressions exist and they are a 
powerful tool for text processing. Anyone who wants to learn more, can 
search for articles, books and tutorials in order to learn how to construct 
regular expressions, how to look for matches, how validation is made, how to 
make substitutions by patterns, etc. In particular, we recommend you to visit 
the websites http://www.regular-expressions.info and http://regexlib.com. 
More information about the classes in .NET Framework for working with 
regular expressions can be found at: http://msdn.microsoft.com/en-
us/library/system.text.regularexpressions.regex%28VS.100%29.aspx. 

 
Removing Unnecessary Characters at the Beginning

## Removing Unnecessary Characters at the Beginning and at the End of a String

and at the End of a String 
When entering text in a file or to the console, you can find some times some 
"parasitic" spaces (white-space) at the beginning or at the end of the text – 
some other space or a tab that cannot be observed at first glance. This may 
not be essential but if we do not validate the user data, there would be a 
problem in terms  of checking the contents of the input information. In order 
to solve this problem we can use the method Trim(). It is responsible for 
eliminating (trimming) the white spaces at the beginning or at the end of a 
string. The white spaces can be spaces, tabs, line breaks etc. 
Let’s assume in the variable fileData we have read the contents of a file 
where is written a name of a student. There may have emerged parasitic 
spaces when writing the text or reversing it from one format to another. In 
that case the variable will look the following way: 
string fileData = "   \n\n     David Allen      "; 
If we print the contents to the console, we get two blank lines followed by 
some spaces, the requested name and some additional spaces at the end. We 
can reduce the information just to the required name, in the following way: 
string reduced = fileData.Trim(); 
When we print the information to the console for the second time, the content 
will be "David Allen", without any unwanted white spaces.

### Removing Unnecessary Characters by a Given List

The method Trim(…) can accept an array of characters, which  we want to 
remove from the string. We can make it in the following way: 
string fileData = "   111 $  %    David Allen  ### s   "; 
char[] trimChars = new char[] {' ', '1', '$', '%', '#', 's'}; 
string reduced = fileData.Trim(trimChars); 
// reduced = "David Allen" 
Again, we get the desired result "David Allen". 
 
Please note that we must list all the characters we want to 
eliminate, including the empty spaces (spaces, tabs, new 
line, etc.). Without a ' '  in the array trimChars, we would 
not get the desired result! 
If we want to remove the white spaces only at the beginning or in end of the 
string, we can use the methods TrimStart(…) and TrimEnd(…): 


string reduced = fileData.TrimEnd(trimChars); 
// reduced = "   111 $  %    David Allen"

> [!TIP]
> **Modern C# Note: String Interpolation**
> In C# 6.0, **String Interpolation** was introduced. Instead of using `string.Format` or manual string concatenation, you can prefix the string with `$` and put variables directly in curly braces:
> ```csharp
> string name = "John";
> int age = 30;
> Console.WriteLine($"Hello, my name is {name} and I am {age} years old.");
> ```
