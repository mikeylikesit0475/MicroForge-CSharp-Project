# Console Input and Output – Examples

We will consider few more examples of console input and output that will 
show us some interesting techniques.

## Printing a Letter

Next is a practical example representing console input and formatted text in 
the form of a letter: 
PrintingLetter.cs 
```csharp
class PrintingLetter 
{ 
 static void Main() 
 { 
  Console.Write("Enter person name: "); 
  string person = Console.ReadLine(); 
 
  Console.Write("Enter book name: "); 
  string book = Console.ReadLine(); 
 
  string from = "Authors Team"; 
 
  Console.WriteLine("  Dear {0},", person); 
  Console.Write("We are pleased to inform " + 
   "you that \"{1}\" is the best Bulgarian book. {2}" + 
   "The authors of the book wish you good luck {0}!{2}", 
   person, book, Environment.NewLine); 
 
  Console.WriteLine("  Yours,"); 
  Console.WriteLine("  {0}", from); 
 } 
} 
```
The result of the execution of the above program could be the following: 
Enter person name: Readers 
Enter book name: Introduction to programming with C# 
  Dear Readers, 
We are pleased to inform you that "Introduction to programming 

with C#" is the best Bulgarian book. 
The authors of the book wish you good luck Readers! 
  Yours, 
  Authors Team 
In this example we have a letter template. The program "asks" a few 
questions to the user and reads from the console information needed to print 
the letter by replacing the formatting specifiers with the data filled in by the 
user.

## Area of a Rectangle or a Triangle

We will consider another example: calculating of an area of a rectangle or a 
triangle. 
CalculatingArea.cs 
```csharp
class CalculatingArea 
{ 
 static void Main() 
 { 
  Console.WriteLine("This program calculates " + 
  "the area of a rectangle or a triangle"); 
 
  Console.WriteLine("Enter a and b (for rectangle) " + 
   "or a and h (for triangle): "); 
 
  int a = int.Parse(Console.ReadLine()); 
  int b = int.Parse(Console.ReadLine()); 
 
  Console.WriteLine("Enter 1 for a rectangle or " + 
    "2 for a triangle: "); 
 
  int choice = int.Parse(Console.ReadLine()); 
  double area = (double) (a * b) / choice; 
  Console.WriteLine("The area of your figure is " + area); 
 } 
} 
```
The result of the above example’s execution is as follows: 
This program calculates the area of a rectangle or a triangle 
Enter a and b (for rectangle) or a and h (for triangle): 
5 
4 

Enter 1 for a rectangle or 2 for a triangle: 
2 
The area of your figure is 10