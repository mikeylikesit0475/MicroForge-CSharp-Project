# Solutions and Guidelines

Solutions and Guidelines 
1. Your program can read from the input file character by character. If the 
current character is a letter, append it to the b uffer, and if it  is a 
separator, analyze the buffer (since it holds the current word) and clear it. 
When the end of the input file is reached and the file does not end with a 
separator, analyze the last word in the buffer. Test the solution! 
2. First, estimate the count of prime numbers you will need. Then consider 
what the upper limit of the iterations for the Sieve of Eratosthenes  
should be, so that there are enough numbers to fill the matrix. You can 
come up with a formula by experimenting or you may use the formula 
from the section “Improved Performance: Sieve of Eratosthenes”. 
3. Taking into account that in math multiplication and division has a priority 
over addition and subt raction you can calculate all multiplications and 
divisions first, replace them with their result and then all the additions 
and subtractions. For example, let’s the expression is “2*5 -8/2+11”. You 
may first calculate all multiplications and divisions a nd replace them with 
the results of their execution: “2*5-8/2+11”  “10-4+11”. Then you may 
use the algorithm from the “ Evaluate an Arithmetic Expression” section . 
Did you consider division by zero? Test your code. Think of special cases. 
4. Floating point arithmetic can be implemented by allowing the use of the 
character "." and replacing int with double or decimal. Test your code! 
5. You can do the following: locate the first closing parenthesis and match 
it with its corresponding opening parenthesis . What remains between 
them is an arithmetic expression that can be evaluated with the same 
algorithm recursively. You can substitute the expression with its value and 

repeat the process until there a ren’t any more parentheses. Eventually, 
you will end up with an expression without parentheses. 
For example, if the expression " 2*((3+5)*(4-7*2))" is entered, you will 
substitute "(3+5)" with 8, and "(4-7*2)" with -10. Finally, you will replace 
"(8*-10)" with -80 and calculate 2*-80, thus getting the result -160. You 
will have to consider the arithmetic operations with negative numbers, i.e. 
adding negative numbers support when parsing the numbers. 
There is one more algorithm. It utilizes a stack and converts the expression 
to reverse Polish notation  (RPN). Look up the terms " postfix 
notation" and "shunting yard algorithm" on the Internet. 
To handle correctly the unary minus , you may consider two sit uations. 
The first is a leading unary minus (e.g. -3 + 5). The second is a minus 
after another operator or after a bracket, e.g. “3 * -2 + 4”. The minus can 
be applied to a number of to an expression in brackets. In both cases you 
may insert “0-” and put t he number or expression on the right in 
brackets. Examples: 
- “-3 + 5”  “(0-3) + 5” 
- “3 * -2 + 5”  “3 * (0-2) + 5” 
- “-(3+2)”  “(0-(3+2))” 
- “-(-1) * 3 - -1”  “(0-((0-1))) * 3 – (0-1)” 
6. If you evaluate the expression using reverse polish notation , you can 
expand your algorithm to check the expression for validity . Follow 
these rules: when you expect a digit, but the next token is not a digit, then 
the expression is invalid; when an arithmetic operation is expected, but the 
next token is not a valid operator , then the expression is invalid; when the 
parentheses do not match, the stack will either underflow or remain non -
empty at the end. Don ’t forget the special cases " -1", "-(2+4)", etc. Test 
thoroughly your code! There are many special cases to consider.