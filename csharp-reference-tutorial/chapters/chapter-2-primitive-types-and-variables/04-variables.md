# Variables

After reviewing the main data types in C# let’s see how we can use them. In 
order to work with data we should use variables. We have already seen their 
usage in the examples, but now let’s look at them in more detail. 
A variable is a container of informati on, which can change its value. It 
provides means for: 
- storing information; 
- retrieving the stored information; 

- modifying the stored information. 
In C# programming, you will use variables to store and process information 
all the time.

## Characteristics of Variables

Variables are characterized by: 
- name (identifier), for example age; 
- type (of the information preserved in them), for example int; 
- value (stored information), for example 25. 
A variable is a  named area of memory , which stores a value from a 
particular data type, and that area of memory is accessible in the program by 
its name. Variables can be stored directly in the operational memory of the 
program (in the stack) or in the dynamic memory in which larger objects are 
stored (such as character strings and arrays). 
Primitive data types (numbers, char, bool) are called value types because 
they store their value directly in the program stack. 
Reference data types  (such as strings, objects and arrays) are an address, 
pointing to the dynamic memory where their value is stored. They can be 
dynamically allocated and released i.e. their size is not fixed in advance 
contrary to the case of value types. 
More information about the value and reference data types is provided in the 
section "Value and Reference Types".

## Naming Variables – Rules

When we want the compiler to allocate a memory area for some information 
which is used in our program we must provide a name for it. It works like an 
identifier and allows referring to the relevant memory area. 
The name of the variable can be any of our choice but must follow certain 
rules defined in the C# language specification: 
- Variable names can contain the letters a-z, A-Z, the digits 0-9 as well as 
the character '_'. 
- Variable names cannot start with a digit. 
- Variable names cannot coincide with a keyword of the C# language. 
For example, base, char, default, int, object, this, null and many 
others cannot be used as variable names. 
A list of the C# keywords can be found in the section " Keywords" in chapter 
"Introduction to Programming". If we want to name a variable like a keyword, 
we can ad d a prefix to the name – "@". For example, @char and @null are 
valid variable names while char and null are invalid.

## Naming Variables – Examples

Proper names: 
- name 
- first_Name 
- _name1 
Improper names (will lead to compilation error): 
- 1 (digit) 
- if (keyword) 
- 1name (starts with a digit)

## Naming Variables – Recommendations

We will provide some recommendations how to name your variables, since not 
all names, allowed by the compiler, are appropriate for the variables. 
- The names should be descriptive and explain what the variable is used 
for. For example, an appropriate name for a variable storing a person’s 
name is personName and inappropriate name is a37. 
- Only Latin characters  should be used. Although Cyrillic is allowed b y 
the compiler, it is not a good practice to use it in variable names or in 
the rest of the identifiers within the program. 
- In C# it is generally accepted that variable names should start with a 
small letter and include small letters, every new word, howev er, starts 
with a capital letter. For instance, the name firstName is correct and 
better to use than firstname or first_name. Usage of the character  _ 
in the variable names is considered a bad naming style. 
- Variable names should be neither too long nor too  short – they just 
need to clarify the purpose of the variable within its context. 
- Uppercase and lowercase letters should be used carefully as C# 
distinguishes them. For instance, age and Age are different variables. 
Here are some examples of well-named variables: 
- firstName 
- age 
- startIndex 
- lastNegativeNumberIndex 
And here are some examples for poorly named variables (although the names 
are correct from the C# compiler’s perspective): 
- _firstName (starts with _) 

- last_name (contains _) 
- AGE (is written with capital letters) 
- Start_Index (starts with capital letter and contains _) 
- lastNegativeNumber_Index (contains _) 
- a37 (the name is not descriptive and does not clearly provide the 
purpose of the variable) 
- fullName23, fullName24, etc. (it is not appropriate for a variable name 
to contain digits unless this improves the clarity of the variable used; if 
you need to have multiple variables with similar names ending in a 
different number, storing the same or similar type of data, it may be 
more appropriate to create a single collection or array variable and 
name it fullNamesList, for example). 
Variables should have names, which briefly explain their purpose . When a 
variable is named with an inappropriate name, it makes the program very 
difficult to read and modify later  (after a while, when we have forgotten how 
it works). For further explanation on the proper naming of variables refer to 
chapter "High-Quality Programming Code". 
 
Always try to use short and precise names when naming the 
variables. Follow the rule that the variable name should state 
what it is used for, e.g. the name should answer the question 
"what value is stored in this variable". When this condition is 
not fulfi lled then try to find a better name. Digits are not 
appropriate to be used in variable names.

## Declaring Variables

When you declare a variable, you perform the following steps: 
- specify its type (such as int); 
- specify its name (identifier, such as age); 
- optionally specify initial value (such as 25) but this is not obligatory. 
The syntax for declaring variables in C# is as follows: 
<data type> <identifier> [= <initialization>]; 
Here is an example of declaring variables: 
string name; 
int age;

## Assigning a Value

Assigning a value to a variable is the act of providing a value that must be 
stored in the variable. This operation is performed by the assignment operator 
"=". On the left side of the operator we put the variable name and on the right 
side – its new value. 
Here is an example of assigning values to variables: 
name = "John Smith"; 
age = 25;

## Initialization of Variables

The word initialization in programming means specifying an initial value. 
When setting value to variables at the time of their declarat ion we actually 
initialize them.

### Default Variable Values

Each data type in C# has a default value (default initialization) which is used 
when there is no explicitly set value for a given variable. We can use the 
following table to see the default values of  the types, which we already got 
familiar with: 
Data Type Default Value  Data Type Default Value 
sbyte 0 float 0.0f 
byte 0 double 0.0d 
short 0 decimal 0.0m 
ushort 0 bool false 
int 0 char '\u0000' 
uint 0u string null 
long 0L object null 
ulong 0u   
Let’s summarize how to declare variables, initialize them and assign values to 
them with the following example: 
// Declare and initialize some variables 
byte centuries = 20; 
ushort years = 2000; 
decimal decimalPI = 3.141592653589793238m; 
bool isEmpty = true; 
char ch = 'a'; 

string firstName = "John"; 
 
ch = (char)5; 
char secondChar; 
 
// Here we use an already initialized variable and reassign it 
secondChar = ch;