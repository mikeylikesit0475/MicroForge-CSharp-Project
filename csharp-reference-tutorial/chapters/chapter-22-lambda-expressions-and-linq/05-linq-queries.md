# LINQ Queries

LINQ (Language -Integrated Query)  is a set of extensions of the .NET 
Framework, that includes language integrated queries and operations on the 
elements of a certain data source (most often arrays or collectio ns). LINQ is 
a very powerful tool, similar to most SQL languages by logic and syntax. It 
actually works with collections in the same way as SQL languages work with 
table rows in databases. It is part of the syntax of C# and Visual Basic .NET  
and consists o f few special keywords like from, in and select. In order to 
use LINQ queries in C#, we have to include a reference to System.Core.dll 
and to include the namespace System.Linq in the beginning of the C# 
program.

## Data Sources with LINQ

To define the data source (collection, array and so on), we have to use the 
keywords from and in and a variable for the iteration of the collection (the 
iteration is similar to the one with the foreach operator). For example, a 
query that starts like this: 
from culture 
in CultureInfo.GetCultures(CultureTypes.AllCultures) 

can be read as follows: "for each element of the collection CultureInfo. 
GetCultures(CultureTypes.AllCultures) assign the variable culture and 
use it to refer to these items further in the query".

## Data Filtering with LINQ

The keyword where can be used to set conditions, that should be kept by each 
item of the collection, in order to continue with the execution of the query. 
The expression af ter where is always of a Boolean type. We can say that 
where works as a filter for the elements . For example, if we want to see 
only those cultures, whose name begins with the lowercase Latin letter b, we 
can continue the query from our last example like this: 
where culture.Name.StartsWith("b") 
As we can notice, after where … in, we use only the name we gave for the 
iteration of the variables in the collection. The keyword where is compiled up 
to the invoking of the extension method Where(). 
where culture.Name.StartsWith("b")

## Results of LINQ Queries

To choose the output data  for the query, we can use the keyword  
select. The result is an object of an existing class or an anonymous type. 
The result can also be a property of the objects, the query runs through or the 
objects themselves. The select statement and everything following it is 
placed always at the end of the query. The four keywords: from, in, where 
and select, are completely enough to create a simple LINQ query. Here is an 
example: 
List<int> numbers = new List<int>() { 
 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 
}; 
var evenNumbers = 
 from num in numbers 
 where num % 2 == 0 
 select num; 
foreach (var item in evenNumbers) 
```csharp
{ 
 Console.Write(item + " "); 
} 
```
The result is: 
2 4 6 8 10  

The example above runs a query over a collection of integers  called 
numbers and filters only the even ones in a new collection. The query can be 
read as follows: "for each number num from numbers check if it is multiple of 
2, and if so, add it to the new collection".

## Sorting Data with LINQ

Sorting with LINQ queries  is done through the keyword orderby. The 
conditions, used for sorting the elements, are placed after it. For each 
condition the order of arrangement can be indicated: ascending (using the 
keyword ascending) and descending (with the keyword descending), as by 
default the elements are ordered  in ascending order. If we want to sort an 
array of strings by their length in descending order, for example, we can write 
the following query: 
string[] words = { "cherry", "apple", "blueberry" }; 
var wordsSortedByLength = 
 from word in words 
 orderby word.Length descending 
 select word; 
foreach (var word in wordsSortedByLength) 
```csharp
{ 
 Console.WriteLine(word); 
} 
```
The result is: 
blueberry 
cherry 
apple 
If no instruction for the order is given (i.e. the keyword orderby is missing 
from the query) the items are printed in the way they would be processed, if 
the foreach operator was used.

## Grouping Results with LINQ

To group the results by some criteria the keyword group should be used. The 
pattern is as follows: 
group [variable name] by [grouping condition] into [group name] 
The result of grouping is a new collection of a special type  that can be 
used further in the query. After the grouping, however, the query stops 
working with its initial variable. This means that in th e select statement, we 
can use only the group. An example of grouping: 

int[] numbers = 
 { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0, 10, 11, 12, 13 }; 
int divisor = 5; 
 
var numberGroups = 
 from number in numbers 
 group number by number % divisor into group 
 select new { Remainder = group.Key, Numbers = group }; 
 
foreach (var group in numberGroups) 
```csharp
{ 
 Console.WriteLine( 
 "Numbers with a remainder of {0} when divided by {1}:", 
  group.Remainder, divisor); 
 foreach (var number in group.Numbers) 
 { 
  Console.WriteLine(number); 
 } 
} 
```
The result is: 
Numbers with a remainder of 0 when divided by 5: 
5 
0 
10 
Numbers with a remainder of 4 when divided by 5: 
4 
9 
Numbers with a remainder of 1 when divided by 5: 
1 
6 
11 
Numbers with a remainder of 3 when divided by 5: 
3 
8 
13 
Numbers with a remainder of 2 when divided by 5: 
7 
2 
12 
As we can see from the example above, the numbers printed to the console 
are grouped by their remainders of the division by 5. In the query, for each 
number number % divisor is calculated, and for each different result a new 

group is formed. Further in the query, the select operator works on the list 
of created groups, and for each group creates an anonymous type with two 
properties: Remainder and Numbers. To the property Remainder the key of 
the group is assigned (in our case the remainder of the division by the 
divisor of the number). And to the property Numbers the collection group is 
assigned, that contains all the elements in the group. Notice that select is 
executed only over the list of groups. The variable number cannot be used 
there. Further in the example of two nested foreach statements, the 
remainders (the groups) and the numbers that have the remainder (located in 
the group) are printed.

## Joining Data with LINQ

The join statement is a bit more complicated than the other LINQ 
statements. It joins collections by certain matching criteria and extracts the 
needed data. Its syntax is as follows: 
from [variable name from collection 1] in [collection 1] join 
[variable name from collection 2] in [collection 2] on [part of 
the compare condition from collection 1] equals [part of the 
compare condition from collection 2] 
Further in the query (e.g. in the select part), both, the name of the variable 
from collection 1, and the name of the variable from collection 2, can be used. 
Example: 
```csharp
public class Product 
{ 
 public string Name { get; set; } 
 public int CategoryID { get; set; } 
} 
 
public class Category 
{ 
 public int ID { get; set; } 
 public string Name { get; set; } 
} 
```
The code that illustrates how to use LINQ joins: 
List<Category> categories = new List<Category>() 
```csharp
{ 
 new Category() { ID = 1, Name = "Fruit" }, 
 new Category() { ID = 2, Name = "Food" }, 
 new Category() { ID = 3, Name = "Shoe" }, 
 new Category() { ID = 4, Name = "Juice" }, 

}; 
List<Product> products = new List<Product>() 
{ 
 new Product() { Name = "Strawberry", CategoryID = 1 }, 
 new Product() { Name = "Banana", CategoryID = 1 }, 
 new Product() { Name = "Chicken meat", CategoryID = 2 }, 
 new Product() { Name = "Apple Juice", CategoryID = 4 }, 
 new Product() { Name = "Fish", CategoryID = 2 }, 
 new Product() { Name = "Orange Juice", CategoryID = 4 }, 
 new Product() { Name = "Sandal", CategoryID = 3 }, 
}; 
var productsWithCategories = 
```
 from product in products 
 join category in categories 
  on product.CategoryID equals category.ID 
 select new {  Name = product.Name, 
       Category = category.Name }; 
foreach (var item in productsWithCategories) 
```csharp
{ 
 Console.WriteLine(item); 
} 
```
The result is: 
{ Name = Strawberry, Category = Fruit } 
{ Name = Banana, Category = Fruit } 
{ Name = Chicken meat, Category = Food } 
{ Name = Apple Juice, Category = Juice } 
{ Name = Fish, Category = Food } 
{ Name = Orange Juice, Category = Juice } 
{ Name = Sandal, Category = Shoe } 
In the example above, we create two classes and an imaginary relationship 
between them. To each product some category CategoryID (represented by 
a number)  corresponds, that matches the number ID from the class  
Category in the collection categories. If we want to use this relation and to 
create a new anonymous type, where to store the products and their names 
and category, we can write the above LINQ query. It joins the collection of 
elements of type Category with the one of type Product by the mentioned 
criteria (match between ID from Category and CategoryID from Products). 
In the select part of the query, we use both names category and product 
to construct an anonymous type with the name of the product  and the name 
of the category.