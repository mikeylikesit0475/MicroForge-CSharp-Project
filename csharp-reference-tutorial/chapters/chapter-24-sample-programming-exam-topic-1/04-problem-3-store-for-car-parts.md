# Problem 3: Store for Car Parts

A company is planning to create a system for managing a store for auto 
parts. A single part can be used for different car models and it has following 
characteristics: code, name, category (e.g. suspension, t ires and wheels, 
engine, accessories and etc.), purchase price , sale price , list of car 
models, with which it is compatible (each car is described with brand, model  
and year of manufacture, e. g. Mercedes C320, 2008)  and manufacturing 
company. Manufacturing companies  are described with name, country, 
address, phone and fax. 
Design a set of classes  with relationships between them, which model the 
data for the store. Write a demonstration program , which demonstrates 
the classes and their all functionality work correctly with some sample data.

## Inventing an Idea for Solution

We have a non -algorithmic problem which is intended to check whether the 
students at the exam know how to use object-oriented programming  
(OOP), how to design classes and relationships betwe en them to model real -
world objects ( object-oriented analysis and design) and how to use 
appropriate data structures to hold collections of objects. 
We are required to create an aggregation of classes and relationships between 
them, which have to describe the data of the store. We have to find which 
nouns are important for solving the problem. They are objects from the real 
world, which correspond to classes. 
Which are these nouns that interest us? We have a store, car parts, cars 
and manufacturing companies. We have to create a class defining a store. 
It could be named Shop. Other classes are Part, Car and Manufacturer. In 
the requirements of the problem there are other nouns too, like code for one 
part or year of manuf acturing of given car. For these nouns we are not 
creating individual classes, but instead these will be fields in the already 
created classes. For example in the Part class there will be let ’s say a field 
code of string type. 

We already know which will be our classes, and fields to describe them . 
We have to identify the relationships between the objects.

### Checking the Idea

We will not check the idea  because there is nothing to be proven with 
examples and counterexamples or checked whether it will work. We n eed to 
write few classes to model a real-world situation: a store for car parts.

### What Data Structures to Use to Describe the Relationship between Two Classes?

What Data Structures to Use to Describe the Relationship 
between Two Classes? 
The data structures, needed for this problem, are of two main groups: 
classes and relationships b etween the classes . The interesting part is 
how to describe relationships. 
To describe a relationship (link) between two classes we can use an array. 
With an array we have access of its elements by index, but once it is created 
we can’t change its length. This makes it uncomfortable for our problem , 
because we don ’t know how many parts we will have in the store and more 
parts can be delivered or somebody can buy parts so we have to delete or 
change the data. List<T> is more comfortable. It has the advantages of an 
array and also is with variable length and it is easy to add or delete elements. 
So far it seems List<T> is the most appropriate for holding aggregations of 
objects inside another object . To be convinced we will analyze a few mo re 
data structures. For example hash-table – it is not appropriate in this case, 
because the structure “parts” is not of the key -value type. It would be 
appropriate if each of the parts in the store has unique number ( e.g. barcode) 
and we needed to search them by this unique number. Structures like stack 
and queue are inappropriate. 
The structure “set” and its implementation HashSet<T> is used when we have 
uniqueness for given key. It would be good sometimes to use this structure 
to avoid duplicates. We mus t recall that HashSet<T> requires the methods 
GetHashCode() and Equals(…) to be correctly defined by the T type. 
Our final decision is to use List<T> for the aggregations and  HashSet<T> for 
the aggregations which require uniqueness.

## Dividing the Task into Subtasks

Now we have to think from where to start writing the code . If we start to 
write the Shop class, we will  need the Part class. This reminds us we will  
have to start with a class, which does not depend on others. We will divide 
the writing of each class to а subtask, and we will start from the independent 
classes: 
- Class describing a car – Car 
- Class describing manufacturer of parts – Manufacturer 
- Class or enumeration for the categories of the parts – PartCategory 

- Class describing part for a car – Part 
- Class for the store – Shop 
- Class for testing rest of the classes with sample data – TestShop

## Implementation: Step by Step

We start writing classes, which we described in our idea. We will create them 
in the same sequence as in the list above.

### Step 1: The Class Car

We start solving the problem by defining the class Car. In the definition we 
have three fields, which keep the manufacturer, the model and the year of 
manufacturing of the car and the standard method ToString(), which returns 
a human-readable string holding the information about the car. We define the 
```csharp
class Car in the following way: 
```
Car.cs 
```csharp
public class Car 
{ 
 private string brand; 
 private string model; 
 private int productionYear; 
 
 public Car(string brand, string model, int productionYear) 
 { 
  this.brand = brand; 
  this.model = model; 
  this.productionYear = productionYear; 
 } 
 
 public override string ToString() 
 { 
  return "<" + this.brand + "," + this.model + "," 
   + this.productionYear + ">"; 
 } 
} 
Note that the class Car is designed to be immutable. This means that once 
```
created, the car’s properties cannot be later modified. This design is not 
always the best choice. Sometimes we want the class properties to be freely 
modifiable; sometimes. For our case the immutable design will work well.

### Testing the Class Car

Once we have the class Car, we could test it by the following code: 

Car bmw316i = new Car("BMW", "316i", 1994); 
```csharp
Console.WriteLine(bmw316i); 
```
The result is as expected: 
<BMW,316i,1994> 
We are convinced the class Car is correct so far and we can continue with the 
other classes.

### Step 2: The Class Manufacturer

We have to implement the definition of the class  Manufacturer, which 
describes the manufacturer for given part. It will have five fields – name, 
country, address, phone number and fax. The class will be immutable, 
because we will not need to change its members after creation. We also 
define the standard method ToString() for representing the object as  
human-readable string. 
Manufacturer.cs 
```csharp
public class Manufacturer 
{ 
 private string name; 
 private string country; 
 private string address; 
 private string phoneNumber; 
 private string fax; 
 
 public Manufacturer(string name, string country, 
  string address, string phoneNumber, string fax) 
 { 
  this.name = name; 
  this.country = country; 
  this.address = address; 
  this.phoneNumber = phoneNumber; 
  this.fax = fax; 
 } 
 
 public override string ToString() 
 { 
  return this.name + " <" + this.country + "," + this.address 
   + "," + this.phoneNumber + "," + this.fax + ">"; 
 } 
}
```

### Testing the Class Manufacturer

We test the class Manufacturer just like we tested the class Car. It works.

### Step 3: The Part Category Enumeration

Part categories are fixes set of values  and do not have additional details 
(like name, code and description). This makes them perfect to be modeled as 
enumeration: 
PartCategory.cs 
```csharp
public enum PartCategory 
{ 
```
 Engine, 
 Tires, 
 Exhaust, 
 Suspention, 
 Brakes 
}

### Step 4: The Class Part

Now we have to define the class  Part. Its  definition will include the 
following fields : name, code, category, list with cars, where we can use the 
given part, starting and closing price and manufacturer. Here we will use the 
data structure HashSet<Car> to hold all compatible cars. 
The field that keeps the manufacturer of the part will be of Manufacturer 
```csharp
class, because the task requires us to keep additional information about the 
```
manufacturer. If it was required to keep only the name of the manufacturer 
(as in the case with class Car) this class should not be necessary. We would 
have a field of string type. 
We need a method for adding a car (object of type Car) to the list of cars (in 
HashSet<Car>). It will be named AddSupportedCar(Car car). 
Below is the code of the class Part which is also designed as  set of  
immutable fields (except that it accepts adding cars): 
Part.cs 
```csharp
public class Part 
{ 
 private string name; 
 private string code; 
 private PartCategory category; 
 private HashSet<Car> supportedCars; 
 private decimal buyPrice; 

 private decimal sellPrice; 
 private Manufacturer manufacturer; 
 
 public Part(string name, decimal buyPrice, decimal sellPrice, 
  Manufacturer manufacturer, string code, 
```
  PartCategory category) 
```csharp
 { 
  this.name = name; 
  this.buyPrice = buyPrice; 
  this.sellPrice = sellPrice; 
  this.manufacturer = manufacturer; 
  this.code = code; 
  this.category = category; 
  this.supportedCars = new HashSet<Car>(); 
 } 
 
 public void AddSupportedCar(Car car) 
 { 
  this.supportedCars.Add(car); 
 } 
 
 public override string ToString() 
 { 
  StringBuilder result = new();
  result.Append("Part: " + this.name + "\n"); 
  result.Append("-code: " + this.code + "\n"); 
  result.Append("-category: " + this.category + "\n"); 
  result.Append("-buyPrice: " + this.buyPrice + "\n"); 
  result.Append("-sellPrice: " + this.sellPrice + "\n"); 
  result.Append("-manufacturer: " + this.manufacturer +"\n"); 
  result.Append("---Supported cars---" + "\n"); 
  foreach (Car car in this.supportedCars) 
  { 
   result.Append(car); 
   result.Append("\n"); 
  } 
  result.Append("----------------------\n"); 
  return result.ToString(); 
 } 
} 
In the class Part we use HashSet<Car> so it is necessary to redefine the 
methods Equals(…) and GetHashCode() for the class Car: 

// The Equals(…) and GetHashCode() methods for the class Car 
 
public override bool Equals(object obj) 
{ 
 Car otherCar = obj as Car; 
 if (otherCar == null) 
 { 
  return false; 
 } 
 bool equals = 
```
  object.Equals(this.brand, otherCar.brand) && 
  object.Equals(this.model, otherCar.model) && 
  object.Equals(this.productionYear,otherCar.productionYear); 
 return equals; 
} 
 
```csharp
public override int GetHashCode() 
{ 
 const int prime = 31; 
 int result = 1; 
 result = prime * result + ((this.brand == null) ? 0 : 
  this.brand.GetHashCode()); 
 result = prime * result + ((this.model == null) ? 0 : 
  this.model.GetHashCode()); 
 result = prime * result + this.productionYear; 
 return result; 
}
```

### Testing the Class Part

We test the class Part. It is a bit more complicated than when testing the 
classes Car and Manufacturer, because Part it is more complex class. We 
can create a part, assign all its properties and print it: 
Manufacturer bmw = new Manufacturer("BWM", 
 "Germany", "Bavaria", "665544", "876666"); 
Part partEngineOil = new Part("BMW Engine Oil", 
 633.17m, 670.0m, bmw, "Oil431", PartCategory.Engine); 
Car bmw316i = new Car("BMW", "316i", 1994); 
partEngineOil.AddSupportedCar(bmw316i); 
Car mazdaMX5 = new Car("Mazda", "MX5", 1999); 
partEngineOil.AddSupportedCar(mazdaMX5); 
```csharp
Console.WriteLine(partEngineOil); 
```
Seems like the result is correct: 

Part: BMW Engine Oil 
-code: Oil431 
-category: Engine 
-buyPrice: 633.17 
-sellPrice: 670.0 
-manufacturer: BWM <Germany,Bavaria,665544,876666> 
---Supported cars--- 
<BMW,316i,1994> 
<Mazda,MX5,1999> 
---------------------- 
Before we  can continue with the next class , we could test for duplicated 
cars in the set of supported cars for certain part . Duplicates are not allowed 
by design and we should check whether this is enforced: 
Manufacturer bmw = new Manufacturer("BWM", 
 "Germany", "Bavaria", "665544", "876666"); 
Part partEngineOil = new Part("BMW Engine Oil", 
 633.17m, 670.0m, bmw, "Oil431", PartCategory.Engine); 
partEngineOil.AddSupportedCar(new Car("BMW", "316i", 1994)); 
partEngineOil.AddSupportedCar(new Car("BMW", "X5", 2006)); 
partEngineOil.AddSupportedCar(new Car("BMW", "X5", 2007)); 
partEngineOil.AddSupportedCar(new Car("BMW", "X5", 2006)); 
partEngineOil.AddSupportedCar(new Car("BMW", "316i", 1994)); 
```csharp
Console.WriteLine(partEngineOil); 
```
The result is correct. The duplicated cars are taken into account only once: 
Part: BMW Engine Oil 
-code: Oil431 
-category: Engine 
-buyPrice: 633.17 
-sellPrice: 670.0 
-manufacturer: BWM <Germany,Bavaria,665544,876666> 
---Supported cars--- 
<BMW,316i,1994> 
<BMW,X5,2006> 
<BMW,X5,2007> 
----------------------

### Step 5: The Class Shop

We already have all needed classes  for creating the class Shop. It will have 
two fields : name and list of parts, which are for sale. The list will be 
List<Part>. We will add the method AddPart(Part part) , with which we 

will add new parts. With a redefined ToString() we will print the name of the 
shop and the parts in it. 
Here is an example of implementation  of our class Shop holding the catalog of 
auto parts (its name is immutable but it can add parts): 
Shop.cs 
```csharp
public class Shop 
{ 
 private string name; 
 private List<Part> parts; 
 
 public Shop(string name) 
 { 
  this.name = name; 
  this.parts = new List<Part>(); 
 } 
 
 public void AddPart(Part part) 
 { 
  this.parts.Add(part); 
 } 
 
 public override string ToString() 
 { 
  StringBuilder result = new();
  result.Append("Shop: " + this.name + "\n\n"); 
  foreach (Part part in this.parts) 
  { 
   result.Append(part); 
   result.Append("\n"); 
  } 
  return result.ToString(); 
 } 
} 
```
It might be a subject of discussion whether we should use List<Part> 
or Set<Part> for the parts in the car shop. The set data structure  has an 
advantage that it avoids any duplicates . Thus if we have for example few 
tires of certain model, they will be found only once i n the set. To use set we 
need to be sure the parts are uniquely identified by their code or by some 
other unique identifier. In our case we assume we could have parts with 
exactly the same code, name, etc. which come at different buy and sell prices 
(e.g. if the prices change over the time). So we need to allow duplicated parts 

and thus using a set will not be appropriate . Parts in the shop will be kept 
in List<Part>. 
We will test the class Shop though the especially written class TestShop.

### Step 6: The Class TestShop

We created all classes we need. We have to create one more, with which we 
will have to demonstrate the usage of the rest of the classes . It will be 
named TestShop. In the Main() method we will  create two manufacturers 
and a few cars. W e will add them to two parts. We will add the parts to the 
Shop. At the end we will print everything on the console. 
TestShop.cs 
```csharp
public class TestShop 
{ 
 static void Main() 
 { 
  Manufacturer bmw = new Manufacturer("BWM", 
   "Germany", "Bavaria", "665544", "876666"); 
  Manufacturer lada = new Manufacturer("Lada", 
   "Russia", "Moscow", "653443", "893321"); 
 
  Car bmw316i = new("BMW", "316i", 1994);
  Car ladaSamara = new("Lada", "Samara", 1987);
  Car mazdaMX5 = new("Mazda", "MX5", 1999);
  Car mercedesC500 = new("Mercedes", "C500", 2008);
  Car trabant = new("Trabant", "super", 1966);
  Car opelAstra = new("Opel", "Astra", 1997);
 
  Part cheapPart = new Part("Tires 165/50/R13", 302.36m, 
   345.58m, lada, "T332", PartCategory.Tires); 
  cheapPart.AddSupportedCar(ladaSamara); 
  cheapPart.AddSupportedCar(trabant); 
 
  Part expensivePart = new Part("Universal Car Engine", 
   6733.17m, 6800.0m, bmw, "EU33", PartCategory.Engine); 
  expensivePart.AddSupportedCar(bmw316i); 
  expensivePart.AddSupportedCar(mazdaMX5); 
  expensivePart.AddSupportedCar(mercedesC500); 
  expensivePart.AddSupportedCar(opelAstra); 
 
  Shop newShop = new("Tuning Pro Shop");
  newShop.AddPart(cheapPart); 
  newShop.AddPart(expensivePart); 

 
  Console.WriteLine(newShop); 
 } 
} 
```
This is the result of the execution of the above code: 
Shop: Tuning Pro Shop 
 
Part: Tires 165/50/R13 
-code: T332 
-category: Tires 
-buyPrice: 302.36 
-sellPrice: 345.58 
-manufacturer: Lada <Russia,Moscow,653443,893321> 
---Supported cars--- 
<Lada,Samara,1987> 
<Trabant,super,1966> 
---------------------- 
 
Part: Universal Car Engine 
-code: EU33 
-category: Engine 
-buyPrice: 6733.17 
-sellPrice: 6800.0 
-manufacturer: BWM <Germany,Bavaria,665544,876666> 
---Supported cars--- 
<BMW,316i,1994> 
<Mazda,MX5,1999> 
<Mercedes,C500,2008> 
<Opel,Astra,1997> 
----------------------

## Testing the Solution

At the end we need to test our code. In fact we have done this in the class 
TestShop. This doesn ’t mean that we have tested entirely our problem. We 
have to check the border cases , for example when some of the lists are 
empty. Let’s make a little change of the code in Main() method, to start the 
program with an empty list: 
```csharp
static void Main() 
{ 
 Shop emptyShop = new("Empty Shop");
 Console.WriteLine(emptyShop); 

 
 Manufacturer lada = new Manufacturer("Lada", 
  "Russia", "Moscow", "653443", "893321"); 
 Part tires = new Part("Tires 165/50/R13", 302.36m, 
  345.58m, lada, "T332", PartCategory.Tires); 
 
 Manufacturer bmw = new Manufacturer("BWM", 
  "Germany", "Bavaria", "665544", "876666"); 
 Part engineOil = new Part("BMW Engine Oil", 
  633.17m, 670.0m, bmw, "Oil431", PartCategory.Engine); 
 engineOil.AddSupportedCar(new Car("BMW", "316i", 1994)); 
   
 Shop ultraTuningShop = new("Ultra Tuning Shop");
 ultraTuningShop.AddPart(tires); 
 ultraTuningShop.AddPart(engineOil); 
 
 Console.WriteLine(ultraTuningShop); 
} 
```
The result of this test is: 
Shop: Empty Shop 
 
 
Shop: Ultra Tuning Shop 
 
Part: Tires 165/50/R13 
-code: T332 
-category: Tires 
-buyPrice: 302.36 
-sellPrice: 345.58 
-manufacturer: Lada <Russia,Moscow,653443,893321> 
---Supported cars--- 
---------------------- 
 
Part: BMW Engine Oil 
-code: Oil431 
-category: Engine 
-buyPrice: 633.17 
-sellPrice: 670.0 
-manufacturer: BWM <Germany,Bavaria,665544,876666> 
---Supported cars--- 
<BMW,316i,1994> 
---------------------- 

From the result it seems the first shop is empty and in the second shop the 
list of cars for the first part is empty. This is the correct output. Therefore 
our program works correctly with the border case of empty lists. 
We can continue testing with other border cases  (e.g. missing part name, 
missing price, missing manufacturer, etc.), as well as with some kind of 
performance test  (e.g. shop with 300,000 parts for 5,000 cars and 200 
manufacturers). We will leave this for the readers.