# Text Files – More Examples

We hope the theoretical explanations and examples so far have helped you 
get into the subtleties when working with text files. Now we will take a look at 
some more complex examples , so as to review the gained knowledge and 
to illustrate how to use them in solving practical problems.

## Occurrences of a Substring in a File – Example

Here is how to implement a simple program that counts how many times a 
substring occurs in a text file. In the example, let’s look for the substring "C#" 
in a text file as follows: 
sample.txt 
This is our "Intro to Programming in C#" book. 
In it you will learn the basics of C# programming. 
You will find out how nice C# is. 
We can implement the counting as follows: will read the file line by line and 
each time we meet the desired word  inside the last read line, we will increase 
the value of a variable (counter). We will process the possible exceptional 
situations to enable users to receive adequate information in case of errors. 
Here is a sample implementation: 
CountingWordOccurrences.cs 
```csharp
static void Main() 
{ 
 string fileName = @"..\..\sample.txt"; 
 string word = "C#"; 
```
 try 
```csharp
 { 
  StreamReader reader = new(fileName);
  using (reader) 
  { 
   int occurrences = 0; 
   string line = reader.ReadLine(); 
   while (line != null) 
   { 
    int index = line.IndexOf(word); 
    while (index != -1) 
    { 
     occurrences++; 
     index = line.IndexOf(word, (index + 1)); 
    } 
    line = reader.ReadLine(); 
   } 
   Console.WriteLine( 
    "The word {0} occurs {1} times.", word, occurrences); 
  } 
 } 
```
 catch (FileNotFoundException) 

```csharp
 { 
```
  Console.Error.WriteLine( 
   "Can not find file {0}.", fileName); 
 } 
 catch (IOException) 
```csharp
 { 
```
  Console.Error.WriteLine( 
   "Cannot read the file {0}.", fileName); 
 } 
} 
For simplicity of the example , the word we seek is hardcoded. You can 
implement the program to search a word entered by the user. 
You can see that the example is not very different from the previous ones. We 
initialize the variables outside of the try-catch block. Again, we use a while-
loop to read the lines of the text file one by one. Inside its body, there is 
another while-loop, which counts how many times the searched word occurs 
in the given line, and then increases the number of occurrences. This is done 
```csharp
using the method IndexOf(…) of the class String (remember what it does in 
```
case you have forgotten). We do not forget to ensure the closing of the 
StreamReader object using the using structure. All that remains is to print 
the results on to the console. 
For our example, the result is the following: 
The word C# occurs 3 times.

## Editing a Subtitles File – Example

Now we will look at a more complex example, in which we at the same time 
read from a file and record to another. This program fixes a subtitles file 
for a movie. 
Our goal will  be to read a file with subtitles, that are incorrect and do not 
appear at the right time, and to shift the times in an appropriate manner, so 
that they can appear correctly. One such file generally contains the time of 
the on-screen duration and the text,  that should appear in the defin ed time 
interval. Here is how typical subtitles files look like: 
GORA.sub 
{1029}{1122}{Y:i}Captain, systems are|at the ready. 
{1123}{1270}{Y:i}The preassure is stable.|Prepare for landing. 
{1343}{1468}{Y:i}Please,fasten your seatbelts|and take your 
places. 
{1509}{1610}{Y:i}Coordinates 5.6|- Five, Five, Six, dot com. 

{1632}{1718}{Y:i}Where did the coordinates|go to? 
{1756}{1820}Commander Logar,|everyone is speaking in English. 
{1821}{1938}Can't we switch|to Turkish from the beginning? 
{1942}{1992}Yes, we can! 
{3104}{3228}{Y:b}G.O.R.A.|a movie about the cosmos 
… 
 
StarWars.sub 
{1029}{1122}{Y:i}I'll never join you. 
{1123}{1270}{Y:i}If you only knew | the power of the dark side. 
{1343}{1468}{Y:i}Obi One never told you what happened to your 
father! 
{1509}{1610}{Y:i}He told me enough! | He told me you killed him. 
{1632}{1718}{Y:i}No... I am your father! 
{1756}{1820}(dramatic music playing)... 
{1821}{1938}No, no that's not true... | That's impossible! 
{1942}{1992}Search your feelings,| you know it's true. 
{3104}{3228}{Y:b}Nooo… 
… 
To fix the subtitles, we usually just need to make an adjustment in the time 
for displaying the subtitles. Such an adjustment may be offsetting the start / 
end time for each subtitle (by addition or subtraction of a constant) or 
changing the speed (multiplying by a factor, say 1.05). 
Here is sample code that can implement such a program: 
FixingSubtitles.cs 
```csharp
using System; 
using System.IO; 
 
class FixingSubtitles 
{ 
 const double COEFFICIENT = 1.05; 
 const int ADDITION = 5000; 
 const string INPUT_FILE = @"..\..\source.sub"; 
 const string OUTPUT_FILE = @"..\..\fixed.sub"; 
 
 static void Main() 
 { 
```
  try 
```csharp
  { 

   // Create reader 
   StreamReader streamReader = new(INPUT_FILE);
 
   // Create writer 
   StreamWriter streamWriter = 
    new StreamWriter(OUTPUT_FILE, false); 
 
   using (streamReader) 
   { 
    using (streamWriter) 
    { 
     string line; 
     while ((line = streamReader.ReadLine()) != null) 
     { 
      streamWriter.WriteLine(FixLine(line)); 
     } 
    } 
   } 
  } 
```
  catch (IOException exc) 
```csharp
  { 
   Console.WriteLine("Error: {0}.", exc.Message); 
  } 
 } 
 
 static string FixLine(string line) 
 { 
  // Find closing brace 
  int bracketFromIndex = line.IndexOf('}'); 
 
  // Extract 'from' time 
  string fromTime = line.Substring(1, bracketFromIndex - 1); 
 
  // Calculate new 'from' time 
  int newFromTime = (int) (Convert.ToInt32(fromTime) * 
   COEFFICIENT + ADDITION); 
 
  // Find the following closing brace 
  int bracketToIndex = line.IndexOf('}', 
   bracketFromIndex + 1); 
 
  // Extract 'to' time 
  string toTime = line.Substring(bracketFromIndex + 2, 
   bracketToIndex - bracketFromIndex - 2); 

 
  // Calculate new 'to' time 
  int newToTime = (int) (Convert.ToInt32(toTime) * 
   COEFFICIENT + ADDITION); 
 
  // Create a new line using the new 'from' and 'to' times 
  string fixedLine = "{" + newFromTime + "}" + "{" + 
   newToTime + "}" + line.Substring(bracketToIndex + 1); 
 
  return fixedLine; 
 } 
} 
```
Again, we use the already familiar method for reading a file line by line . 
The difference this time is, that in the body of the loop, we write every line of 
the file with already corrected subtitles, after we have fixed them with the 
method FixLine(string) (this method is not the subject of our discussion, 
since it can be implemented in many different ways depending on what you 
want to adjust). Because we use the using block for both files, we can 
guarantee that they will be closed even if an exception occu rs during 
processing (this may happen, for example if one of the lines in the file is not 
in the expected format).