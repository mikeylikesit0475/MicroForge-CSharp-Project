# Why to Use Methods?

There are many reasons we should use methods. Some of them are listed 
below, and by gaining experience, you will assure yourself that methods are 
something that cannot be avoided for a serious task.

## Better Structured Program and More Readable Code

Whenever a program has been created, it is always a good practice to use 
methods, in a way to make y our code better structured and easy to 
read, hence to be maintained by other people. 
A good reason for this is the fact, that of the time that a program exists, only 
about 20% of the effort is spent on creating and testing the program. The rest 
is for maintenance and adding new features to the initial version. In most of 
the cases, once the code has been released, it is maintained not only from its 
creator, but by many other developers. That is why it is very important for the 
code to be as well structured and readable as possible.

## Avoid Duplicated Code

Another very important reason to use methods is that methods help us to 
avoid code repeating . This has a strong relationship to the idea of code 
reuse.

## Code Reuse

If a piece of code is used more than once in a program, it is good to separate 
it in a method, which can be called many times – thus enabling reuse of the 
same code, without rewriting it. This way we avoid code repeating, but this 
is not the only advantage. The program itself becomes more readable an d 
well structured. 
Repeating code may become very noxious and hazardous, because it impedes 
the maintenance of the program and leads to errors. Often, whenever change 
of repeating code is needed, the developer fixes only some of the blocks, but 
the problem s is still alive in the others, about which they forgot. So for 
example if a defect is found into a piece of 50 lines code, that is copied to 10 
different places over the program, to fix the defect, the repeated code must 
be fixed for the all 10 places. Th is, however, is not what really happens. 
Often, due to lack of concentration or some other reasons, the developer 
fixes only some of the pieces of code, but not all of them . For example, 

let’s say that in our case the developer has fixed 8 out of 10 blocks  of code. 
This eventually, will lead to unexpected behavior of our program, only in rare 
cases and, moreover, it will be very a difficult task to find out what is going 
wrong with the program.