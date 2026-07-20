# Conditional Statement "switch-case"

In the following section we will co ver the conditional statement switch. It is 
used for choosing among a list of possibilities.

## How Does the "switch-case" Statement Work?

The structure switch-case chooses which part of the programming code to 
execute based on the calculated value of a certa in expression (most often of 
integer type). The format of the structure for choosing an option is as follows: 
switch (integer_selector) 
```csharp
{ 
```
 case integer_value_1:  
  statements; 
  break; 
 case integer_value_2: 
  statements; 
  break; 
 // … 
 default: 
  statements; 
  break; 
} 
The selector is an expression returning a resulting value that can be 
compared, like a number or string. The switch operator compares the result 
of the selector to every value listed in the case labels in the body of the 
switch structure. If a match is found in a case label, the corresponding 
structure is executed (simple or complex). If no match is found, the default 
statement is executed (when such exists). The value of the selector must be 
calculated before comparing it to the values inside the switch structure. The 
labels should not have repeating values, they must be unique. 
As it can be seen from the definition above, every case ends with the 
operator break, which ends the body of the switch structure. The C# 
compiler requires the word break at the end of each case-section containing 
code. If no code is found after a case-statement, the break can be omitted 

and the execution passes to the next case-statement and continues until it 
finds a break operator. After the default structure break is obligatory. 
It is not necessary for the default clause to be last, but it ’s recommended to 
put it at the end, and not in the middle of the switch structure.

## Rules for Expressions in Switch

The switch statement is a clear way to implement selection among many 
options (namely, a choice among a few alternative ways for executing the 
code). It requires a selector, which is calculated to a certain value. The 
selector type could be an integer number, char, string or enum. If we want 
to use for example an array or a float as a selector, it will not work. For non -
integer data types, we should use a series of if statements.

## Using Multiple Labels

Using multiple labels is appropriate, when we want to execute the same 
structure in more than one case. Let’s look at the following example: 
int number = 6; 
switch (number) 
```csharp
{ 
```
 case 1: 
 case 4: 
 case 6: 
 case 8: 
 case 10: 
```csharp
  Console.WriteLine("The number is not prime!"); break; 
```
 case 2: 
 case 3: 
 case 5: 
 case 7: 
```csharp
  Console.WriteLine("The number is prime!"); break; 
```
 default: 
```csharp
  Console.WriteLine("Unknown number!"); break; 
} 
In the above example, we implement multiple labels by using case 
```
statements without break after them. In this case, first the integer value of 
the selector is calculated – that is 6, and then this value is compared to every 
integer value in the case statements. When a match is found, the code block 
after it is executed. If no match is found, t he default block is executed. The 
result of the example above is as follows: 
The number is not prime!

## Good Practices When Using "switch-case"

- A good practice when using the switch statement is to put the default 
statement at the end, in order to have easier to read code. 
- It’s good to place first the cases, which handle t he most common 
situations. Case statements, which handle situations occurring rarely, 
can be placed at the end of the structure. 
- If the values in the case labels are integer, it ’s recommended that they 
be arranged in ascending order. 
- If the values in the case labels are of character type, it’s recommended 
that the case labels are sorted alphabetically. 
- It’s advisable to always use a default block to handle situations that  
cannot be processed in the normal operation of the program. If in the 
normal operation of the program the default block should not be 
reachable, you could put in it a code reporting an error.