# String Formatting

.NET Framework provides the developer with mechanisms for formatting 
strings, numbers and dates. We have already met some of them in the 
chapter "Console Input and Output ". Now we will extend our knowledge with 
methods for formatting and converting strings of the string class.

## The ToString(…) Method

One of the interesting concepts in .NET is that practically every object of a 
```csharp
class and primitive variables can be presented as text . This is done by the 
```
method ToString(…), which is present in all .NET objects. It is implicit in the 
definition of the object class – the base class that all .NET data types inherit 
directly or indirectly. Thus the definition of the method appears in each class 
and we can use it to bring the content of each object in some text form. 
The method ToString(…) is called automatically when we print objects from 
different classes to the console. For example, when printing dates  the 
submitted date is converted to text by calling the ToString(…): 
DateTime currentDate = DateTime.Now; 
```csharp
Console.WriteLine(currentDate); 
// Output: 01.02.2012 13:34:27 (depends on the culture settings) 
```
When we pass currentDate as a parameter of the WriteLine(…) method, we 
don’t have an accurate statement that handles dates. The method has a 
particular implementation for all primitive types and strings. For all other 
objects WriteLine(…) calls their ToString(…) method, which first converts 
them to text and then displays the resul ting text content. In fact, the sample 
code above is equivalent to the following: 
DateTime currentDate = DateTime.Now; 
```csharp
Console.WriteLine(currentDate.ToString()); 
The default implementation of the ToString(…) method in the object class 
```
returns the full nam e of the class. All classes that do not explicitly redefine 
the behavior of the ToString(…) are using this implementation. Most classes 
in C# have their own implementation of the method, which represents 
readable and understandable content in text form. Fo r example, converting a 
number to text is using the standard format for numbers in the current 

 
culture. Converting a date to text is also using the standard format for dates 
in the current culture.

## Using of String.Format(…)

String.Format(…) is a static met hod by which we can format text and 
other data through a template  (formatting string). The templates contain 
text and declared parameters ( placeholders) and are used to obtain 
formatted text after replacing the parameters with specific values. You can 
```csharp
make a direct association with the Console.WriteLine(…) method, which 
also formats a string through a template: 
Console.WriteLine("This is a template from {0}", "David"); 
```
How to use the String.Format(…) method? Consider an ex ample in order to 
clarify this: 
DateTime date = DateTime.Now; 
string name = "David Scott"; 
string task = "Introduction to C# book"; 
string location = "his office"; 
 
string formattedText = String.Format( 
 "Today is {0:MM/dd/yyyy} and {1} is working on {2} in {3}.", 
 date, name, task, location); 
```csharp
Console.WriteLine(formattedText); 
 
// Output: Today is 01.02.2012 and David Scott is working on 
// Introduction to C# book in his office. 
```
As it is seen from the example, formatting with String.Format() uses 
placeholders (parameters like {0}, {1}, etc.) and accepts formatting strings 
(such as :dd.MM.yyyy). It accepts as first parameter a formatting string 
containing text with parameters, followed by values for each parameter and 
returns the formatted text as a result. More information about formatting 
strings can be found on the Internet and in the Composite Formatting 
article in MSDN ( http://msdn.microsoft.com/en-us/library/txafckwd.aspx). 
Note that the exact formatting of the output could slightly vary depending on 
your default culture and internationalization.

## Parsing Data

The reverse operation of data formatting is data parsing. Parsing of data 
(data parsing)  means to obtain a value of a given type from the text 
representation of this value in a specific format , i.e. converting from text to 
some other data type , the opposite of ToString(). For example, from the 

text "10/22/2010" we can get an instance of DateTime type, containing the 
relevant date. 
Often working with applications with graphical user interface requires the user 
input to be  passed in variables of type string. This way we can work well 
with numbers and characters as well as text and dates, formatted in a user ’s 
preferred way. It is up to the developer ’s experience to represent the 
expected input data into the right way for th e user. The data are then 
converted to a specific data type and processed. For example, numbers 
can be converted to int or double variables and then participate in 
mathematical expressions for calculations. 
 
When converting types, we should not rely only on trusting 
the user. Always check the correctness of the input user 
data! Otherwise there could be an exception that could 
change the normal program logic.

### Parsing Numeric Types

To parse a string to a number we can use the Parse(…) method of the 
primitive types. Let’s see an example of parsing a string to an integer value: 
string text = "53"; 
int intValue = int.Parse(text); 
// intValue = 53 
We can also parse variables of Boolean type: 
string text = "True"; 
bool boolValue = bool.Parse(text); 
// boolValue = true 
Return value is true, when the passed parameter is initialized (not an object 
with null value), and its content is " true" regardless of the casing of letters 
in it. For example, any text such as " true", "True" or "tRUe" will set the 
variable boolValue to true. If the parameter’s content is "false", no matter 
the casing of letters, the return value will be false. In all other cases it throws 
FormatException. 
In case the passed to the Parse(…) method value is invalid for the type (e.g. 
we pass "John!" when parsing a number), an exception is thrown.

### Parsing Dates

Parsing to a date is similar to parsing to a numeric type, but it is 
recommended to set a specific date format. Here is an example of how this 
can happen: 


 
string text = "11/11/2001"; 
DateTime parsedDate = DateTime.Parse(text); 
```csharp
Console.WriteLine(parsedDate); 
// 11-Nov-01 0:00:00 AM 
```
Whether the date will be parsed successfully and in what format exactly it will 
be printed on the console depends strongly on the current culture of Windows. 
In the example, a modified version of the U.S. culture (en-US) is used. If we 
want to set a format explicitly, which does not depend on the culture, we can 
use the method DateTime.ParseExact(…) and specify particular formatting 
pattern of our choice: 
string text = "11/12/2001"; 
string format = "MM/dd/yyyy"; 
DateTime parsedDate = DateTime.ParseExact( 
 text, format, CultureInfo.InvariantCulture); 
```csharp
Console.WriteLine("Day: {0}\nMonth: {1}\nYear: {2}", 
 parsedDate.Day, parsedDate.Month, parsedDate.Year); 
// Day: 12 
// Month: 11 
// Year: 2001 
When parsing with an explicitly set format, it is required to pass a specific 
```
culture from which to take information about date format and separators 
between days and years. Since we want the parsing not to depend on a 
particular culture, we explicitly spe cify the neutral culture to be used: 
CultureInfo.InvariantCulture. To use the class CultureInfo, we must 
include the namespace System.Globalization in the beginning of our C# 
source code.