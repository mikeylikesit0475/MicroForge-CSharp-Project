# Value and Reference Types

Data types in C# are two types: value and reference. 
Value types are stored in the program execution stack and directly contain 
their value. Value types are the primitive numeric types, the character type 
and the Boolean type: sbyte, byte, short, ushort, int, long, ulong, 
float, double, decimal, char, bool. The memory allocated for them is 
released when the program exits their range, i.e. when the block of code in 
which they are defined completes its execution. For example, a variable 
declared in the method Main() of the program is stored in the stack until t he 
program completes execution of this method, i.e. until it finishes (C# 
programs terminate after fully executing the Main() method). 
Reference types  keep a reference (address), in the program execution 
stack, and that reference points to the dynamic memo ry (heap), where 
their value is stored. The reference is a pointer (address of the memory cell) 
indicating the actual location of the value in the heap. An example of a value 
at address in the stack for execution is 0x00AD4934. The reference has a 
type. Th e reference can only point to objects of the same type, i.e. it is a 
strongly typed pointer. All reference types can hold a null value. This is a 
special service value, which means that there is no value. 
Reference types  allocate dynamic memory  for their c reation. They also 
release some dynamic memory for a memory cleaning  (garbage 
collector), when it is no longer used by the program. It is unknown exactly 
when a given reference variable will be released of the garbage collector as 
this depends on the memor y load and other factors. Since the allocation and 
release of memory is a slow operation, it can be said that the reference types 
are slower than the value ones. 
As reference data types  are allocated and released dynamically  during 
program execution, their size might not be known in advance. For example, a 
variable of type string can contain text data which varies in length. Actually 
the string text value is stored in the dynamic memory and can occupy a 
different volume (count of bytes) while the string variable stores the address 
of the text value. 
Reference types are all classes, arrays and interfaces such as the types: 
object, string, byte[]. We will learn about classes, objects, strings, arrays 
and interfaces in the next chapters of this book. For now, it is enough to know 

that all types, which are not value, are reference and their values are stored 
in the heap (the dynamically allocated memory).

## Value and Reference Types and the Memory

In this example we will illustrate how value and reference types are 
represented in memory . Consider the execution of the following 
programming code: 
int i = 42; 
char ch = 'A'; 
bool result = true; 
object obj = 42; 
string str = "Hello"; 
byte[] bytes = { 1, 2, 3 }; 
At this point the variables are located in the memory as follows: 
 
If we now execute the following code, which changes the values of the 
variables, we will see what happens to the memory  when changing the 
value and reference types: 
String@7cdaf2
Int32@9ae764
HeapStack
42
i
ch
result
obj
42
str
Hello
(4 bytes)
A (2 bytes)
true (1 byte)
int
(4 bytes)
string
byte[]@190d11
bytes
1 byte[]2 3

i = 0; 
ch = 'B'; 
result = false; 
obj = null; 
str = "Bye"; 
bytes[1] = 0; 
After these changes the variables and their values are located in the 
memory as follows: 
 
As you can see from the figure, a change in a value type (i = 0) changes its 
value directly into the stack . When changing a reference type , things are 
different: the value is changed in the heap  (bytes[1] = 0). The variable 
that keeps the array reference remains unchanged ( 0x00190D11). When 
assigning a null value in a reference type, that reference is disconnected 
from its value and the variable remains with no value (obj = null). 
When assigning new value to an object (a reference type variable) the new 
object is allocated in the heap (the dynamic memory) while the old object 
remains free (unreferenced). The reference is redirected to the new object 
(str = "Bye") while the old objects ("Hello") will be cleaned at some moment 
String@9a787b
null
HeapStack
0
i
ch
result
obj
42
str
Hello
(4 bytes)
B (2 bytes)
false (1 byte)
int
(4 bytes)
string
byte[]@190d11
bytes
1 byte[]0 3
Bye string

by the garbage collector (the .NET Framework ’s internal system for 
automatic memory cleaning) as they are not in use anymore.