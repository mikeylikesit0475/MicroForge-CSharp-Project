# Anonymous Types

In object-oriented languages (such as C#), it is common to define small 
classes that will be used only once. Typical example is the class  Point that 
has only two fields – the coordinates of a point. Creating a simple class with 
the idea of using it just once is inconvenient and time consuming for the 
programmer, especially when the stand ard operations for each class: 
ToString(), Equals() and GetHashCode() have to be predefined. 
In C# there is a built -in way to create single-use types, called anonymous 
types. Objects of such type are created almost the same way as other 
objects in C#. The thing with them is that we don ’t need to define data type 
for the variable in advance. The keyword var indicates to the compiler that 
the type of the variable will be automatically detected by the expression, after 
the equals sign. We actually don ’t have a choice here, since we can ’t tell the 
specific type of the variable, because it is defined as one of an anonymous 
type. After that, we specify name f or the object, followed by the " =" operator 
and the keyword new. In curly braces we enumerate the names and the 
values of the properties of the anonymous type.

## Anonymous Types – Example

Here is an example of creating an anonymous type that describes a car: 
var myCar = new { Color = "Red", Brand = "BMW", Speed = 180 }; 
During compilation, the compiler will create a class with a unique name  
(something like <>f__AnonymousType0) and will generate properties for it 
(with getter and setter). In the example above , the compiler will guess by its 
own, that the properties Color and Brand are of type string and Speed will 
be set as int. Right after the initialization, the object of the anonymous type 
can be used as one of an ordinary type with its three properties: 

```csharp
Console.WriteLine("My car is a {0} {1}.", 
 myCar.Color, myCar.Brand); 
Console.WriteLine("It runs {0} km/h.", myCar.Speed); 
```
The output of the code above will be as follows: 
My car is a Red BMW. 
It runs 180 km/h.

## More about Anonymous Types

As any other typ e in .NET, the anonymous ones inherit the class System. 
Object. During compilation, the compiler will automatically redefine the 
methods ToString(), Equals() and GetHashCode() for us. 
```csharp
Console.WriteLine("ToString: {0}", myCar.ToString()); 
Console.WriteLine("Hash code: {0}", 
 myCar.GetHashCode().ToString()); 
Console.WriteLine("Equals? {0}", myCar.Equals( 
 new { Color = "Red", Brand = "BMW", Speed = 180 } 
)); 
Console.WriteLine("Type name: {0}", myCar.GetType().ToString()); 
```
The output of the code above will be the following: 
ToString: { Color = Red, Brand = BMW, Speed = 180 } 
Hash code: 1572002086 
Equals? True 
Type name: 
<>f__AnonymousType0`3[System.String,System.String,System.Int32] 
As we can see from the result, the method ToString() is redefined, so that 
it can list the properties of the anonymous type in the order of their definition 
in the initialization of the object (in our case myCar). The method 
GetHashCode() is wrote in such a way, that it uses all fields and on their 
basis it calculates a hash functi on with a small number of collisions. The 
redefined by the compiler method  Equals(…) compares the objects field by 
field. As we can notice from the example, we have created a new object that 
has exactly the same properties as myCar, and returns a result stating that 
the newly created object and the old one have equal values.

## Arrays of Anonymous Types

The anonymous types, like ordinary ones, can be used as elements of 
arrays. We can initialize them with the keyword new, followed by square 
brackets. The values of the elements of the array are listed the same way, as 

the values assigned to the anonymous types. The values in the array should 
be homogeneous, i.e. it is not possible to have different anonymous types in 
the same array. An example of defining an a rray of anonymous types with 
two properties (X and Y): 
```csharp
var arr = new[] { 
 new { X = 3, Y = 5 }, 
 new { X = 1, Y = 2 }, 
 new { X = 0, Y = 7 } 
}; 
foreach (var item in arr) 
{ 
 Console.WriteLine(item.ToString()); 
} 
```
The result of the execution of the code above will be the following: 
{ X = 3, Y = 5 } 
{ X = 1, Y = 2 } 
{ X = 0, Y = 7 }