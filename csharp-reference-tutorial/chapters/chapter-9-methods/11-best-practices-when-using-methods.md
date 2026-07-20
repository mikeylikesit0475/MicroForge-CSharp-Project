# Best Practices when Using Methods

Best Practices when Using Methods 
In the chapter "High-Quality Programming Code " we will explain in detail s 
about the good practices for writing methods. None the less, we will look at 
some of them right now, so we can start applying the good pract ices and start 
developing a good programming style: 
- Each method must resolve a distinct, well defined task . This feature 
is also known as strong cohesion, i.e. to give a focus onto one single 
task, not to several tasks no strongly related logically. A single method 
should perform a single task, its code should be well structured, easy to 
understand, and easy to be maintained. One method must NOT solve 
several tasks! 
- A method has to have a good name, i.e. name that is descriptive and 
from which becomes clear what the method does . As an example: a 
method that sorts numbers should be named SortNumbers(), but 
should not  be named Number() or Processing() or Method2(). If it 

cannot be given a good name, this may indicate that the method solves 
more than one task and, hence, it must be separated into sub-methods. 
- Method names should describe an action , so they should contain a 
verb or a verb + noun  (possibly with an adjective to supplement the 
noun). For example  good method names are  FindSmallestElement(), 
Sort(int[] arr) and ReadInputData(). 
- It is assumed that all the method names in C# will start with capital 
letter. PascalCase rule is used, i.e. each new word that is concatenated 
to the end of the method name must start with capital letter. For 
example: SendEmail(…), but not sendEmail(…) or send_email(…). 
- A method must do whatever is described with its name, or it must 
return an error (throws an exception). It is not correct that the methods 
return wrong or unusual result when it has received invalid input data . 
The method resolves the task it is created for, or returns an 
error. Any other behavior is incorrect. We will discuss this principle in 
"High-Quality Programming Code", section "What a Method Should Do". 
- A method must have minimum dependency to the class in which the 
method is declared  and to other methods and classes . This feature of 
the methods is also known as loose coupling . This means that the 
method must do its job by using the data that passed to it as 
parameters, but not data that can be accessed from other places. 
Methods should not have side effects  (for example to change some 
global variable or print something on the console in the meantime). 
- It is recommended that the methods must be short . Methods that are 
longer than a computer screen must be avoided. To do so, the logic 
implemented in the method is divided by functionality, to several 
smaller sub -methods. These sub -methods are then called from the 
original place they were cut off. 
- To improve the readability of a method and the code structure, it is good 
idea a functionality that is well detached logically, to be placed in a 
separate method. For example if we have a me thod that calculates the 
volume of a dam lake, the process of calculating the volume of a 
parallelepiped can be defined in a separate method. Then that new 
method can be invoked as many times as necessary. Hence, the sub-
task is separated from the main tas k. Since the dam lake can be 
taken as set of many different parallelepipeds, calculating the volume of 
each one of them is logical detached functionality. 
- The last but most important rule is that a method should either do 
what it name says or throw an exce ption. If a method cannot 
perform its job (e.g. due to incorrect input), it should throw an 
exception, not return invalid or neutral result. How to throw an 
exception will be explained in the chapter “Except ion Handling”, but for 
now you should remember that returning an incorrect result or 
having a side effect are bad practices. If a method cannot do its job, 

it should inform its caller about this by throwing appropriate exception. 
Methods should never return wrong result!