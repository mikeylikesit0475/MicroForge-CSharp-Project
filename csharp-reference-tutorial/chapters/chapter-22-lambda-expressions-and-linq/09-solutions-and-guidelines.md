# Solutions and Guidelines

Solutions and Guidelines 
1. Follow the syntax explained in the section “ Extension Met hods”. You 
may create a new StringBuilder and to write in it all the characters with 
indices, starting from index and with length length, from the object that 
the extension method will work on. 

2. For generic implementation of the Min() and Max() methods for any 
generic type T you can add a restriction to the passed type T to be 
comparable, i.e. you should have something like this: 
public static T Min<T>(this IEnumerable<T> elements) 
 where T : IComparable<T> 
{  … } 
Since not all data types have predefined operators + and /, it will not be 
possible to apply the functions Sum() and Average() to all types directly. 
There are no interfaces ISummable<T> and IDividable<T> in .NET. One 
way to work around  this problem is to convert all input object s to  
decimal and then to calculate sum / average and return decimal as result. 
For the conversion you can use the static method Convert.ToDecimal(…). 
Another interesting approach is to use the dynamic data type  in C# to 
hold the arguments and results and to execute the operations over them at 
runtime (due to the dynamic evaluation capabilities in C#): 
```csharp
public static dynamic Min<T>(this IEnumerable<T> elements) 
{ … } 
```
This is easier to implement and works better but could have performance 
issues and some special cases to be handled. 
3. Review the keywords from, where and select from the "LINQ Queries" 
section in this chapter. 
4. Write a LINQ query to select the described students in  an anonymous 
type that contains only two properties – FirstName and LastName. 
5. For the LINQ query use from, orderby, descending and select. For the 
implementation with the lambda expressions, you can use the methods 
OrderByDescending(…) and ThenByDescending(…). 
6. It is enough to check if the numbers are multiples of 21, instead of writing 
two where conditions. 
7. Use the method ToTitleCase(…) of the property TextInfo in the culture 
en-US in the following way: 
new CultureInfo("en-US", false).TextInfo.ToTitleCase(text); 
8. See the examples at the end of the section “ LINQ Performance”. You can 
use Dictionary<string, List<string>> to hold the phone book.  You 
may explain the difference in the execution speed by trying to explain 
how searching works internally and by the assumption that searching in a 
hash-table takes time O(1) and searching in a collection element by 
element runs in linear time O(n).