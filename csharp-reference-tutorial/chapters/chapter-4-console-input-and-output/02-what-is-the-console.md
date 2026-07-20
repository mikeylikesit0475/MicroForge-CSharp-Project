# What Is the Console?

The Console is a window of the operating system through which users c an 
interact with system programs of the operating system or with other console 
applications. The interaction consists of text input from the standard input 
(usually keyboard) or text display on the standard output  (usually on the 
computer screen). These ac tions are also known as input-output 
operations. The text written on the console brings some information and is a 
sequence of characters sent by one or more programs. 
For each console application the operating system connects input and output 
devices. By d efault these are the keyboard and the screen but they can be 
redirected to a file or other devices.

## Communication between the User and the Program

A lot of programs communicate in some way with the user. This is necessary 
for the user in order to give inst ructions to them. Modern communication 
methods are many and various: they can be through graphical or web-
based interface , console or others. As we mentioned one of the tools for 
communication between programs and users is the console, which  is 
becoming le ss and less used. This is because the modern user interface 
concepts are more convenient and intuitive to work with, from a user ’s 
perspective.

## When to Use the Console?

In some cases the console remains an irreplaceable tool for communication 
with the user . One of these cases is when writing small and simple 
programs where it is necessary to focus the attention on the specific problem 
to be solved, rather than the elegant representation of the result to the user. 
Then a simple solution is used for entering or printing a result, such as input -
output console. Another use case is when we want to test a small piece of 
code for a larger application. Due to simplicity of the operation of the console 
application we can isolate this part of the code easily and comfo rtably without 
having to go through a complex user interface and a number of screens to get 
to the desired code for testing.

## How to Launch the Console?

Each operating system has its own way to launch the console. On Windows for 
example, it can be done in the following way: 
Start -> (All) Programs -> Accessories -> Command Prompt 
After starting the console a black screen  (this color can be changed)  like the 
following should appear: 
 
When starting the console the home directory of the current user (in this c ase 
the username is nakov) is used as a current directory and this is displayed as 
a guide for the user. 
 
Console can be launched through pressing the Start button 
and typing "cmd" in the search box and pressing [Enter] (on 
Windows Vista, Windows 7 and later). For Windows XP, go 
through the sequence Start -> Run… ->, type in "cmd" and 
press [Enter]. 
For simplified visualization of the results from now on in this chapter instead 
of a console screenshot we will use the form: 
Results from console

## More about Consoles

The system console is the black window shown above which displays text 
information. It can display text strings and has a cursor, which moves to the 
right after each character is printed. After the cursor passes through the last 
column of the console (usually it has 80 columns), it moves to the beginning 
of the next line. If the cursor passes through the last line, the console scrolls 
its content upwards and shows a new empty line below the last line. 
Programs in Windows can be console -based, desktop-based, Web-based and 
other. The console-based programs  use the console for their input and 
output. The desktop -based programs use graphical u ser interface (GUI). The 
Web-based programs have Web -based user interface. In this book we will 
write console-based programs almost all the time, so their input will be read 
from the keyboard and their output will be printed in the console. 
Some console-based programs expect the users to enter text, numbers and 
other data, and this is usually done through the keyboard. 
The console in Windows is often associated with the system command 
interpreter, also called the " Command Prompt " or " shell" or which is a 
console-based program in the operating system, which  provides access to 
system commands as well as a wide range of programs, which are part of the 
operating system or are additionally installed to it. 
The word "shell" means "wrap" and has a meaning of a wrap per between the 
user and the inside of the operating system. 
The so called operating system "shells" can be split into two main categories 
according to the type of interface they can provide to the operating system: 
- CLI – Command Line Interface – is a cons ole for commands (such as 
cmd.exe in Windows and bash in Linux). 
- GUI – Graphical User Interface – is a graphical work environment (such 
as Windows Explorer). 
For both types the main purpose of the shell is to run other programs  with 
which the user works although most of the interpreters also support some 
advanced features such as the opportunity to examine the content of 
directories with files. 
 
Each operating system has its own command interpreter that 
has its own commands. 
For example, when starting Windows console, we run the so -called Windows 
command interpreter  in it ( cmd.exe) that executes system programs and 
commands in interactive mode. For example, the command dir shows the 
files in the current directory:

## Basic Console Commands

We will take a look at some basic commands in the Windows standard 
command prompt, which is useful for finding and launching programs.

### Windows Console Commands

The command interpreter running in the console is also called " Command 
Prompt" or "MS-DOS Prompt" (in older versions of Windows). We will take a 
look at some basic commands for this interpreter: 
Command Description 
dir Displays the content of the current directory. 
cd <directory name> Changes the current directory. 
mkdir <directory name> Creates a new directory in the current one. 
rmdir <directory name> Deletes an existing directory. 
type <file name> Prints file content. 
copy <src file> 
<destination file> Copies one file into another. 
Here is an example of multiple commands executed in the Windows command 
shell. The result of the commands’ execution is displayed on the console: 
C:\Documents and Settings\User1>cd "D:\Project2009\C# Book" 
 
C:\Documents and Settings\User1>D: 


 
D:\Project2008\C# Book>dir 
 Volume in drive D has no label. 
 Volume Serial Number is B43A-B0D6 
 
 Directory of D:\Project2009\C# Book 
 
26.12.2009   12:24    <DIR>          . 
26.12.2009   12:24    <DIR>          .. 
26.12.2009   12:23           537 600 Chapter-4-Console-Input-
Output.doc 
26.12.2009   12:23    <DIR>          Test Folder 
26.12.2009   12:24                 0 Test.txt 
             2 File(s)        537 600 bytes 
             3 Dir(s)  24 154 062 848 bytes free 
 
D:\Project2009\C# Book>