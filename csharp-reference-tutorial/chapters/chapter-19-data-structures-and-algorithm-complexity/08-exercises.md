# Exercises

Exercises 
1. Hash-tables do not allow storing more than one value in a key . How can 
we get around this restriction?  Define a class to h old multiple values in a 
hash-table. 
2. Implement a data structure, which can quickly do the following two 
operations: add an element  and extract the smallest element . The 
structure should accept adding duplicated elements. 
3. It is given a text file students.txt containing information about students 
and their specialty in the following format: 
Steven Davis | Computer Science 
Joseph Johnson | Software Engeneering 
Helen Mitchell | Public Relations 
Nicolas Carter | Computer Science 
Susan Green | Public Relations 
William Johnson | Software Engeneering 
Using SortedDictionary<K,T> print on the console the specialties in an 
alphabetical order and for each of them print the names of the students, 
firstly sorted by family name and secondly – by first name, as shown: 
Computer Sciences: Nicolas Carter, Steven Davis 
Public Relations: Susan Green, Helen Mitchell 

Software Engeneering: Joseph Johnson, William Johnson 
4. Implement a class BiDictionary<K1,K2,T>, which allows adding triplets 
{key1, key2, value} and quickly search by either of the keys key1, key2 
as well as searching by combination of the both keys. Note: Adding many 
elements with the same keys is allowed. 
5. A big chain of supermarkets sell millions of products. Each of them has a 
unique number (barcode), prod ucer, name and price. What data structure 
could we use in order to quickly find all products, which cost between 
5 and 10 dollars? 
6. A timetable of a conference hall is a list of events in a format [starting 
date and time; ending date and time; event ’s name]. What data 
structure could we to be able to quickly add events and quickly check 
whether the hall is available in a given interval  [starting date and 
time; ending date and time]? 
7. Implement the data structure PriorityQueue<T>, which offers quick 
execution of the following operations: adding an element , extracting 
the smallest element. 
8. Imagine you develop a search engine, which  gathers all the 
advertisements for used cars in ten websites for the last few years. After 
that the search engine allows a quick search by one or several criteria: a 
brand, model, color, year of production and price. You are not allowed to 
use database management system (like SQL Server, MySQL or MongoDB) 
and you must implement your own indexing in the memory, without storing 
it to the hard disk and without using LINQ. When one searches by price 
minimal and maximal price is given. When one searches by year  of 
production a starting and ending year s are given. What data structures 
would you use in order to ensure fast searching by one or several criteria?