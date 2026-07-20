# Solutions and Guidelines

Solutions and Guidelines 
1. The task is analogical to the first task of the sample exam. You can 
modify the sample solution given above. 
2. You may read the text char by char and after each char to append it to 
the current buffer buf and check each of the searched word for a match 
with EndsWith() in the buffer’s end . Of course you cannot use efficiently 
hash-table and you will have a loop for each letter from the text, which is 
not the fastest solution.  This is a modification of the “ char by char 
algorithm for word counting”. 
Implementing a faster solution needs to adapt the Aho-Corasick 
algorithm. Try to play with it and modify the code from the section 
“Counting Substrings with the Aho-Corasick Algorithm”. 
3. The problem is analogical with the “School” problem  from the sample 
exam and it can be solved by using the same approach. Define classes 
Device, Directory, File, ComputerStorage and ComputerStorageTest. 
Think of what properties each of these classes has and what are the 
relationships between the classes . Create a base abstract class File 
and inherit it from TextFile and BinaryFile. Test your code  with 
sample hierarchy of devices, files and folders.  Note: a file can be listed in 
more than one directory at the same time (unlike in the file system). 
4. Use the class System.IO.Directory and its static methods GetFiles(), 
GetDirectories() and GetLogicalDrives(). Traverse the files system 
```csharp
using the BFS or DFS graph traversal algorithm. Load partially the 
```
content of long files (e.g. the first 128 bytes / chars) to save memory.