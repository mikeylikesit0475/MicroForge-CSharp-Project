# Code Refactoring

The term “ refactoring” appears in 1993 and is popularized by  Martin 
Fowler in his book with the same name. This book reviews a lot of techniques 
for code refactoring (called refactoring patterns). We are going to mention 
a few of them. 
A program needs refactoring in case of code duplication. Code duplication is 
dangerous because a change in one place requires that all the other 
duplicated code be changed as well. The latter is error -prone and 
inconsistencies can arise therefore. Avoiding code duplication can be achieved 
by putting the particular piece of code in a m ethod, or by moving common 
functionality to base classes. 
Refactoring is necessary for methods, which  have grown over time. The 
excessive length of a method is a good reason to think about splitting it up 
logically into few smaller and simpler methods. 
Deeply nested constructs  are another reason for refactoring. They can be 
eliminated by taking out a block of code into a method. 
Classes that do not provide a sufficiently good level of abstraction  or ones 
that perform unrelated tasks (weak cohesion) are candidates for refactoring 
as well. 
Long parameter lists  and public fields  should also go to the fix -it list. 
Tightly coupled classes go in the same category.

## Refactoring at Data Level

A good practice is to avoid magic numbers  scattered throughout the code. 
They should be replaced by named constants. Variables with unclear names 
should be renamed. Long conditional expressions can be refactored into 
separate methods. Variables can be used to hold the intermediate results of 
expressions. A group of data that always appears together can be refactored 
into a separate class. Related constants should be grouped into enumerations.

## Refactoring at Method and Class Level

Within a longer method, all tasks that are unrelated to its main pur pose are 
better moved into separate methods . Similar tasks should be grouped in 
common classes, similar classes – in a common package. If a group of classes 
have common functionality, it should be moved into a base class. 
Circular dependencies between the classes should not exist, they should be 
removed. In most cases the more common class has a reference to the more 
specialized class (parent-child relationship).