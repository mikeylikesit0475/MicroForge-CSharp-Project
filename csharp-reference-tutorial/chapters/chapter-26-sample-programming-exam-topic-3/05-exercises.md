# Exercises

Exercises 
1. Write a program, which  prints a square spiral matrix  beginning from 
the number 1 in the upper right corner and moving clockwise. Examples 
for N=3 and N=4: 
 
4567
314158
213169
1121110
4567
314158
213169
1121110
345
296
187
345
296
187

2. Write a program, which counts the phrases in a text file . Any 
sequence of characters could be given as phra se for counting, even 
sequences containing separators. For instance in the text "I am a student 
in Sofia" the phrases "s", "stu", "a" and "I am" are found respectively 2, 
1, 3 and 1 times. 
3. Model with OOP the file system of a computer running Windows . We 
have devices, directories and files. The devices are for instance floppy 
disk, HDD, CD-ROM, etc. They have a name and a tree of directories and 
files. Each directory has a name, date of last change and list of files and 
directories, which it holds. Each file has a name, date of creation, date of 
last change and content. Each file is placed in one of the directories. Each 
file can be text or binary. Text files  contain text ( string), and the 
binary ones – sequence of bytes ( byte[]). Create a class, which tests 
the other classes and demonstrates how we can build a model for 
devices, directories and files in the computer. 
4. Using the classes from the previous task  write a program which takes 
the real file  system from your computer and  loads it in your 
classes (just the names of the devices, directories and files, without the 
content of the files because you will run out of memory).