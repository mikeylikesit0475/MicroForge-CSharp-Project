# Choosing a Data Structure – Examples

We are going to show several problems, where the choice of an appropriate 
data structure is crucial to the efficiency of their solution. The purpose of this 
is to show you typical situations, in which the reviewed data structures are 
used and to teach you in what scenarios what data structures you should use.

## Generating Subsets

It is given a set of strings S. For example S = { ocean, beer, money, 
happiness}. The task is to write a program, which prints all subsets of S. 
The problem has many and dif ferent solutions, but we are going to focus on 
the following one: We start from the empty set (with 0 elements): 
{} 
We add to it every element of S and we get a collection of subsets with one 
element: 
{ocean}, {beer}, {money}, {happiness} 
To each of the one-elemental subsets we add every element from S , which 
has not been added yet to the corresponding subset and now we have all two -
elemental subsets: 
{ocean, beer}, {ocean, money}, {ocen, happiness}, {beer, money}, 
{beer, happiness}, {money, happiness} 
If we keep on the same way, we will get all 3 -elemental subsets and after 
that all 4-elemental etc. to the N-elemental subsets. 
How to implement this algorithm? We have to choose appropriate data 
structures, right? 
We can start with the data structure keepi ng the initial set of elements S. It 
can be an array, linked list, dynamic array  (List<string>) or set, imple-
mented as SortedSet<string> or HashSet<string>. To answer the question 
which structure is the most appropriate, let’s think of which are the operations 
we are going to do on this structure. We can think of only one operation – 


traversing through all the elements of S. This operation can be implemented 
efficiently with any of these structures. We choose an array because it is the 
simplest data structure of all and it is easy to work with. 
The next step is to pick a structure  in which we will store one of the 
subsets we generate, for example { ocean, happiness}. Again we ask 
ourselves the question what are the operations we execute on this subset of 
words. The operations are a check whether an element exists and an addition 
of an element, right? Which data structure quickly implements both opera -
tions? The arrays and lists do not search quickly, dictionaries store key -value 
pairs, which is not our case. Almost no options are left, so we are going to see 
what the data structure set offers. It supports a quick searching and addition. 
Which implementation to choose – SortedSet<string> or HashSet<string>? 
We do not have a requirem ent to sort the words in alphabetical order, so we 
choose the faster implementation – HashSet<string>. 
Lastly, we will choose one more data structure in which we are going to keep 
the collection of the subsets of words, for example: 
{ocean, beer}, {sea, money}, {sea, happiness}, {beer, money}, 
{beer, happiness}, {money, happiness} 
Using this structure we have to be able to add as well as traverse through all 
its elements consecutively. The following structures meet the requirements:  
list, stack, queue and set . In each of them we can add quickly and go 
through its elements. If we examine the algorithm for generating subsets, we 
will notice each is processed in style: " first generated, first processed ". 
The subset, which had been firstly generated, has be en firstly processed and 
subsets with one more element have been generated from it, right? Therefore 
our algorithm will most accurately fit the data structure queue. We can 
describe the algorithm as follows: 
1. We start with a queue, containing the empty set {}. 
2. We dequeue an element called subset and try to add each element from 
S which subset does not contain. The result is a set, which we enqueue. 
3. We repeat step 2 until the queue becomes empty. 
You can see how a few thoughts brought us to the classical algo rithm 
"breadth-first search " (BFS). Once we know what data structures we 
should use, implementation is quick and easy. Here is how it might look:  
string[] words = {"ocean", "beer", "money", "happiness"}; 
Queue<HashSet<string>> subsetsQueue = 
 new Queue<HashSet<string>>(); 
HashSet<string> emptySet = new HashSet<string>(); 
subsetsQueue.Enqueue(emptySet); 
while (subsetsQueue.Count > 0) 

```csharp
{ 
 HashSet<String> subset = subsetsQueue.Dequeue(); 
 
 // Print current subset 
 Console.Write("{ "); 
 foreach (string word in subset) 
 { 
  Console.Write("{0} ", word); 
 } 
 Console.WriteLine("}"); 
 
 // Generate and enqueue all possible child subsets 
 foreach (string element in words) 
 { 
  if (! subset.Contains(element)) 
  { 
   HashSet<string> newSubset = new();
   newSubset.UnionWith(subset); 
   newSubset.Add(element); 
   subsetsQueue.Enqueue(newSubset); 
  } 
 } 
} 
```
If we execute the code above, we will see that it successfully generates all 
subsets of S, but some of them are generated twice. 
{ } 
{ ocean } 
{ beer } 
{ money } 
{ happiness } 
{ ocean beer } 
{ ocean money } 
{ ocean happiness } 
{ beer ocean } 
… 
In the example the subsets { ocean beer } and { beer ocean } are actually 
one and the same subset. It seems we have not thought of duplicates, which 
occur when we  mix the order of elements in the same subset. How can we 
avoid duplicates? 
Let’s associate the words by their indices. 
ocean  0 

beer  1 
money  2 
happiness  3 
Since the subsets {1, 2, 3} and {2, 1, 3} are actually one and a same subset, 
in order to avoid duplicates, we are going to impose a requirement to 
generate only subsets, in which the indices are in ascending order. Instead 
of subsets of words we can keep subsets of indices, right? In these subsets of 
indices we need two operations: adding an index and getting the biggest 
index so we can add only indices bigger than it. Obviously we do not need 
HashSet<T> anymore, but we can successfully use List<T>, in whi ch the 
elements are ordered in ascending order by index and the biggest element is 
naturally placed last. 
Finally, our algorithm looks something like this: 
1. Let N be the number of elements in S. We start with a queue, 
containing the empty set {}. 
2. We dequeue an element called subset. Let start be the biggest index 
in subset. We add to subset all indices, which are bigger than start 
and smaller than N. As a result we get several new subsets, which we 
enqueue. 
3. Repeat step 2 until the queue is empty. 
Here is how the implementation of the new algorithm looks like: 
```csharp
using System; 
using System.Collections.Generic; 
 
public class Subsets 
{ 
 static string[] words = { "ocean", "beer", "money", 
  "happiness" }; 
 
 static void Main() 
 { 
  Queue<List<int>> subsetsQueue = new();
  List<int> emptySet = new();
  subsetsQueue.Enqueue(emptySet); 
  while (subsetsQueue.Count > 0) 
  { 
   List<int> subset = subsetsQueue.Dequeue(); 
   Print(subset); 
   int start = -1; 
   if (subset.Count > 0) 
   { 
    start = subset[subset.Count - 1]; 

   } 
   for (int i = start + 1; i < words.Length; i++) 
   { 
    List<int> newSubset = new();
    newSubset.AddRange(subset); 
    newSubset.Add(i); 
    subsetsQueue.Enqueue(newSubset); 
   } 
  } 
 } 
 
 static void Print(List<int> subset) { 
  Console.Write("[ "); 
  for (int i=0; i<subset.Count; i++) { 
   int index = subset[i]; 
   Console.Write("{0} ", words[index]); 
  } 
  Console.WriteLine("]"); 
 } 
} 
If we run the program we will get the following correct result: 
```
[ ] 
[ ocean ] 
[ beer ] 
[ money ] 
[ happiness ] 
[ ocean beer ] 
[ ocean money ] 
[ ocean happiness ] 
[ beer money ] 
[ beer happiness ] 
[ money happiness ] 
[ ocean beer money ] 
[ ocean beer happiness ] 
[ ocean money happiness ] 
[ beer money happiness ] 
[ ocean beer money happiness ]

## Sorting Students

It is given a text file , containing the data of a group of students and 
courses which they attend, separated by |. The file looks like this: 

Chris | Jones | C# 
Mia | Smith | PHP 
Chris | Jones | Java 
Peter | Jones | C# 
Sophia | Wilson | Java 
Mia | Wilson | C# 
Mia | Smith | C# 
Write a program printing all courses and the students , who have joined 
them, ordered by last name, and then by first name (if the last names 
match). 
We can implement the problem using a hash-table, which will hold a list of 
students by a course name. We are choosing a hash -table, because we can 
quickly search by course name in it. 
In order to meet the requirements for an order by name and surname, we are 
going to sort the particular list of student s from each course, before we 
print it. Another option is to use SortedSet<T> for the students attending 
each course (because it is internally sorted), but since one can have students 
with the same name, we have to use SortedSet<List<String>>. It becomes 
too complicated. We choose the eas ier way – using List<Student> and 
sorting it before we print it. 
In any case we will have to implement the IComparable interface so we can 
define the order of the elements  of type Student according to the task 
requirements. It is important to firstly compare the family names and if they 
are the same to compare the first names. We remind that in order to sort the 
elements of a given class it is explicitly necessary to define the logic of their 
order. In .NET Framework this is  done by the IComparable<T> interface (or 
through lambda functions like we shall see in the chapter “Lambda 
Expressions and LINQ” ). Let’s define the class Student and implement 
IComparable<Student>. We get something like this: 
```csharp
public class Student : IComparable<Student> 
{ 
 private string firstName; 
 private string lastName; 
 
 public Student(string firstName, string lastName) 
 { 
  this.firstName = firstName; 
  this.lastName = lastName; 
 } 
 
 public int CompareTo(Student student) 
 { 

  int result = lastName.CompareTo(student.lastName); 
  if (result == 0) 
  { 
   result = firstName.CompareTo(student.firstName); 
  } 
  return result; 
 } 
 
 public override String ToString() 
 { 
  return firstName + " " + lastName; 
 } 
} 
```
Now we are able to write the code, which reads the students and their 
courses and stores them in a hash -table, which keeps a list of students 
by a course name ( Dictionary<string, List<Student>>). And then it is 
easy – we iterate over the courses, sort the students and print them: 
// Read the file and build the hash-table of courses 
Dictionary<string, List<Student>> courses = 
 new Dictionary<string, List<Student>>(); 
StreamReader reader = new StreamReader("Students.txt"); 
```csharp
using (reader) 
{ 
 while (true) 
 { 
  string line = reader.ReadLine(); 
  if (line == null) 
  { 
   break; 
  } 
  string[] entry = line.Split(new char[] { '|' }); 
  string firstName = entry[0].Trim(); 
  string lastName = entry[1].Trim(); 
  string course = entry[2].Trim(); 
  List<Student> students; 
  if (! courses.TryGetValue(course, out students)) 
  { 
   // New course -> create a list of students for it 
   students = new List<Student>(); 
   courses.Add(course, students); 
  } 
  Student student = new(firstName, lastName);
  students.Add(student); 

 } 
} 
 
// Print the courses and their students 
foreach (string course in courses.Keys) 
{ 
 Console.WriteLine("Course " + course + ":"); 
 List<Student> students = courses[course]; 
 students.Sort(); 
 foreach (Student student in students) 
 { 
  Console.WriteLine("\t{0}", student); 
 } 
} 
```
The above code first parses all the lines consecutively by splitting them by a 
vertical bar "|". Secondly it cleans the spaces from the beginning and the end. 
After storing every student ’s information, it is checked in the  hash-table 
whether its course exists. If the course has been found, the student is added 
to the list of the students of this course. Otherwise, a new list is created and 
the student is added to it. Then the list is adde d in the hash -table using the 
course name as a key. 
Printing the courses  and students is not difficult. All keys are extracted 
from the hash-table. These are the names of the courses. For each course its 
students' list is extracted, sorted and printed . The sorting is made by the 
built-in method Sort() using the comparison method CompareTo(…) from the 
interface IComparable<T> as defined in the class Student (comparison firstly 
by family name, and if they are the same, comparison by first name). At the 
end th e sorted students are printed by the overridden virtual method 
ToString(). Here is how the output of the upper program looks: 
Course C#: 
        Chris Jones 
        Peter Jones 
        Mia Smith 
        Mia Wilson 
Course PHP: 
        Mia Smith 
Course Java: 
        Chris Jones 
        Sophia Wilson

## Sorting a Phone Book

It is given a text file, containing people ’s names, their city names and 
phone numbers. The file looks like this: 
Kenneth | Virginia Beach | 1-541-754-3010 
Paul    | San Antonio    | 1-535-675-6745 
Mary    | Portland       | 1-234-765-1983 
Laura   | San Antonio    | 1-454-345-2345 
Donna   | Virginia Beach | 1-387-387-2389 
Write a program which prints all the city names in an alphabetical order  
and for each one of them prints all people’s names in alphabetical order  
and their corresponding phone number. 
The problem can be solved in many ways, for example we sort by two criteria: 
firstly by city name and secondly by person name and then we print the 
phone book. 
However, let’s solve the p roblem without sorting, but by using the standard 
data structures in .NET Framework. We want the city names to be sorted. This 
means that it is best to use a data structure, which  internally keeps the 
elements sorted. Such as, for example, a balanced search tree – 
SortedSet<T> or SortedDictionary<K,T>. Since every record from the 
phone book contains beside a city name – other data, it is more convenient to 
have a SortedDictionary<K,T>, which keeps a list of people ’s names and 
their phone numbers. We wa nt the list of the people ’s names from every city 
to be sorted in alphabetical order by name. Hence, we can use the data 
structure SortedDictionary<K,T> again. The key will be the name of the 
person and its value will be his phone number. 
At the end we get  the nested structure SortedDictionary<string, 
SortedDictionary<string, string>> . Here is a sample implementation, 
which shows how we can solve the problem using this structure: 
// Read the file and build the phone book 
SortedDictionary<string, SortedDictionary<string, string>> 
 phonesByTown = new SortedDictionary<string, 
  SortedDictionary<string, string>>(); 
StreamReader reader = new StreamReader("PhoneBook.txt"); 
```csharp
using (reader) 
{ 
 while (true) 
 { 
  string line = reader.ReadLine(); 
  if (line == null) 
  { 
   break; 

  } 
  string[] entry = line.Split(new char[]{'|'}); 
  string name = entry[0].Trim(); 
  string town = entry[1].Trim(); 
  string phone = entry[2].Trim(); 
 
  SortedDictionary<string, string> phoneBook; 
  if (! phonesByTown.TryGetValue(town, out phoneBook)) 
  { 
   // This town is new. Create a phone book for it 
   phoneBook = new SortedDictionary<string, string>(); 
   phonesByTown.Add(town, phoneBook); 
  } 
  phoneBook.Add(name, phone); 
 } 
} 
 
// Print the phone book by towns 
foreach (string town in phonesByTown.Keys) 
{ 
 Console.WriteLine("Town " + town + ":"); 
 SortedDictionary<string, string> phoneBook = 
  phonesByTown[town]; 
 foreach (var entry in phoneBook) 
 { 
  string name = entry.Key; 
  string phone = entry.Value; 
  Console.WriteLine("\t{0} - {1}", name, phone); 
 } 
} 
```
If we execute this sample code with input – the sample phone book, we will 
get the expected result: 
Town Portland: 
        Mary - 1-234-765-1983 
Town San Antonio: 
        Laura - 1-454-345-2345 
        Paul - 1-535-675-6745 
Town Virginia Beach: 
        Donna - 1-387-387-2389 
        Kenneth - 1-541-754-3010

## Searching in a Phone Book

Here is another problem, so we can strengthen the way, in which we think in 
order to choose appropriate data structures . A phone book is stored in a 
text file, containing names of people, their city names and phone 
numbers. People ’s names can be in the format first name or nickname or 
first name + last name or first name + surname + last  name. The file could 
have the following look: 
Kevin Clark       | Virginia beach | 1-454-345-2345 
Skiller           | San Antonio    | 1-566-533-2789 
Kevin Clark Jones | Portland       | 1-432-556-6533 
Linda Johnson     | San Antonio    | 1-123-345-2456 
Kevin             | Phoenix        | 1-564-254-4352 
Kevin Garcia      | Virginia Beach | 1-445-456-6732 
Kevin             | Phoenix        | 1-134-654-7424 
It is possible several people to be given under the same name or even the 
same city. It is possible someone to have several phone numbers . In this 
case he is given several times in the input file. Phone book could be huge  
(up to 1,000,000 records). 
A file holding a sequence of queries is given. The queries are two types: 
- Search by name / nickname / surname / last name . The query 
looks like this: list(name). 
- Search by name / nickname / surname / last name + city name . 
The query looks like this: find(name, town). 
Here is a sample query file: 
list(Kevin) 
find(Angel, San Antonio) 
list(Linda) 
list(Clark) 
find(Jones, Phoenix) 
list(Grandma) 
Write a program, which by given phone book and query file executes and 
respond to all the queries . For each query a list of records in the phone 
book has to be printed or the message " Not found", if the query cannot find 
anything. Queries could be a large number, for example 50,000. 
This problem is not as easy as the previous ones. One easy to implement 
solution could be to scan the entire phone book  for every query and 
extract all records in which there is a match with the searched information. 
But this will work slowly, because the records and queries could be a lot. It 
is necessary to find a way for a quick search without sca nning the entire 
phone book every time. 

In paper phone books the numbers are given by the people’s name, sorted 
in alphabetical order. Sorting will not help us, because someone can search 
by first name, other – by last name, third – by nickname and city na me. We 
have to search by any of the above at the same time. The question is how do 
we do it? 
If we think a bit it will occur to us that the problem requires searching by 
any of the words, which can be seen at the first column of the phone book 
and eventually by the combination of a word from the first column and a town 
from the second one. We know that the fastest search is implemented with a 
hash-table. So far so good, but what are we going to keep as a key and 
value in the hash-table? 
What if we use several hash-tables: one for searching a word from the first 
column, another for searching in the second column, third for searching by 
city and so on? If we think a bit more, we will ask ourselves the question – 
why do we need several hash -tables? Can ’t we search in one common 
hash-table? If we have a name "Peter Jones", we will store his phone 
number under the keys "Peter" and "Jones". If someone searches by any of 
these keys, he will find Peter’s phone number. 
So far so good, but how do we search by both first name and city name , 
for example "Peter from Virginia Beach"? It is possible firstly to find all with a 
name "Peter" and then to print those who are from Virginia Beach. This will 
work, but if there are a lot of people named Peter, searching will work slo wly. 
Then why don ’t we make a hash-table with a key name of a person and 
value another hash -table, which by city name will return a list of phone 
numbers? This could work. We have done something similar in the previous 
task, haven’t we? 
Can we come up with  something smarter ? Can ’t we just put the phone 
numbers of all the people named Peter from Virginia Beach under a key 
"Peter from Virginia Beach " in the main hash -table in the phone book? It 
seems this could solve our problem and we will use only one hash -table for all 
the queries. 
Using the last idea, we could invent the following algorithm: we read line by 
line from the phone book and for each word from the name of a person  
d1, d2, …, dk and for each city name  t we make new records  in the 
phonebook hash-table by the following keys: d1, d2, …, dk, "d1 from t", 
"d2 from t", …, "dk from t". Now it is guaranteed we could search by any of 
a person ’s names as well as name and town. In order to search without 
bothering about letter case we can transform the  words to lowercase in 
advance. After that the searching is trivial – we just search in the hash -table 
by a given word d or if a town t is given "d from t ". Since we could have 
many phone numbers under the same key, for a value in the hash -table we 
should use a list of strings (List<string>). 
Let’s skim through an implementation of the described algorithm: 

```csharp
class PhoneBookFinder 
{ 
 const string PhoneBookFileName = "PhoneBook.txt"; 
 const string QueriesFileName = "Queries.txt"; 
 
 static Dictionary<string, List<string>> phoneBook = 
  new Dictionary<string, List<string>>(); 
 
 static void Main() 
 { 
  ReadPhoneBook(); 
  ProcessQueries(); 
 } 
 
 static void ReadPhoneBook() 
 { 
  StreamReader reader = new(PhoneBookFileName);
  using (reader) 
  { 
   while (true) 
   { 
    string line = reader.ReadLine(); 
    if (line == null) 
    { 
     break; 
    } 
    string[] entry = line.Split(new char[]{'|'}); 
    string names = entry[0].Trim(); 
    string town = entry[1].Trim(); 
 
    string[] nameTokens = 
     names.Split(new char[] {' ', '\t'} ); 
    foreach (string name in nameTokens) 
    { 
     AddToPhoneBook(name, line); 
     string nameAndTown = CombineNameAndTown(town, name); 
     AddToPhoneBook(nameAndTown, line); 
    } 
   } 
  } 
 } 
 
 static string CombineNameAndTown( string town, string name) 
 { 

  return name + " from " + town; 
 } 
 
 static void AddToPhoneBook(string name, string entry) 
 { 
  name = name.ToLower(); 
  List<string> entries; 
  if (! phoneBook.TryGetValue(name, out entries)) 
  { 
   entries = new List<string>(); 
   phoneBook.Add(name, entries); 
  } 
  entries.Add(entry); 
 } 
 
 static void ProcessQueries() 
 { 
  StreamReader reader = new(QueriesFileName);
  using (reader) 
  { 
   while (true) 
   { 
    string query = reader.ReadLine(); 
    if (query == null) 
    { 
     break; 
    } 
    ProcessQuery(query); 
   } 
  } 
 } 
 
 static void ProcessQuery(string query) 
 { 
  if (query.StartsWith("list(")) 
  { 
   int listLen = "list(".Length; 
   string name = query.Substring( 
    listLen, query.Length-listLen-1); 
   name = name.Trim().ToLower(); 
   PrintAllMatches(name); 
  } 
  else if (query.StartsWith("find(")) 
  { 

   string[] queryParams = query.Split( 
    new char[] { '(', ' ', ',', ')' }, 
    StringSplitOptions.RemoveEmptyEntries); 
   string name = queryParams[1]; 
   name = name.Trim().ToLower(); 
   string town = queryParams[2]; 
   town = town.Trim().ToLower(); 
   string nameAndTown = 
    CombineNameAndTown(town, name); 
   PrintAllMatches(nameAndTown); 
  } 
  else 
  { 
   Console.WriteLine( 
    query + " is invalid command!"); 
  } 
 } 
 
 static void PrintAllMatches(string key) 
 { 
  List<string> allMatches; 
  if (phoneBook.TryGetValue(key, out allMatches)) 
  { 
   foreach (string entry in allMatches) 
   { 
    Console.WriteLine(entry); 
   } 
  } 
  else 
  { 
   Console.WriteLine("Not found!"); 
  } 
  Console.WriteLine(); 
 } 
} 
```
While reading the phone book line by line and splitting by the vertical bar "|", 
we extract the three columns (names, city name and phone number). After 
that the names are split and each word is added in the hash -table. 
Additionally we add each word, combined with the city name (so that we can 
search by name + city name). 
The second part of the algorithm is the command execution . In this part 
each line from the query file is read and processed. The process includes 
parsing the command, extracting the name or name and city name and 

searching. The search is directly done by using the hash-table, which is 
created after reading the phone book file. 
To be able to ignore the difference between lowercase and uppercase, all keys 
in the hash -table are added as lowercase. When we search, we do it 
lowercase too.

## Choosing a Data Structure – Conclusions

By the many examples it is clear that the choice of an appropriate data 
structure is highly dependable on the specific task . Sometimes data 
structures have to be combined  or we have to use several of them 
simultaneously. 
What data structure should we pick mostly depends on the operations we 
will perform , so always ask yourselves "what operations should the 
structure, I need, perform efficiently". If you are familiar with the operations, 
you can easily conform which structure does them most efficiently and at the 
same time is easy and handy. 
In order to efficiently choose an appropriate data structure, you should firstly 
invent the algorithm, which you are going to implement, and then look for 
an appropriate data structures for it. 
 
Always go from the algorithm to the data structures, never 
backwards.