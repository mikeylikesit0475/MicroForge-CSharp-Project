# Nested LINQ Queries

Nested LINQ Queries 
LINQ also supports nested queries . For example our last query can be 
written by nesting two queries in the following way (the result is exactly the 
same as the one with join): 
var productsWithCategories = 
 from product in products 
 select new { 
  Name = product.Name, 
  Category = 
   (from category in categories 
   where category.ID == product.CategoryID 
   select category.Name).First() 
 }; 
Since each query in LINQ returns a collection of items (irrespective of whether 
the result from it is of 0, 1 or more elements), we need to use the extension 
method First() over the result of the nested query. The method First() 
returns the first element ( in our case the only one) of the collection it is 
applied on. In this way we get the name of the category only by its ID 
number.