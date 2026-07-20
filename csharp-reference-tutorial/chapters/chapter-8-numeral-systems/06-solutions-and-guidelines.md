# Solutions and Guidelines

Solutions and Guidelines 
1. Use the methods for conversion from one numeral system to 
another. You can check your results with the help of the Windows built -
in calculator, which supports numeral systems in "Programmer" mode. 
The results are: 10010111, 100011, 101011, 11111011, 1111111111 and 
10000000000. 
2. Like the previous exercise. Result: F59E(16) and 62878(10). 
3. Like the previous exercise.  The results are : FA(16) = 250(10) = 
11111010(2), 2A3E(16) = 10814(10) = 10101000111110(2), FFFF(16) = 
65535(10) = 1111111111111111(2) and 5A0E9(16) = 368873(10) = 
1011010000011101001(2). 
4. The rule is " divide by 2 and concatenate the remainders in 
reversed order". For division with a remainder we use the % operator. 
You can cheat by invoking Convert.ToString(numDecimal, 2). 
5. Start with a sum of 0. Multiply the right-most bit with 1 and add it to 
the sum. Multiply the next bit on the left by 2 and add it to the sum. 
Multiply the next bit on the left by 4, the next by 8 and so on. You can 
cheat by invoking Convert.ToInt32(binaryNumAsString, 2). 
6. The rule is " divide by the base of the system (16) and concatenate 
the remainders in reverse d order". A logic that gets a hexadecimal  
digit (0…F) by decimal number ( 0…15) should also be implemented.  You 
can cheat by invoking num.ToString("X"). 
7. Start with a sum of 0. Multiply the right-most digit with 1 and add it to 
the sum. Multiply the next digit to the left by 16 and add it to the sum. 

Multiply the next digit by 16*16, the next by 16*16*16 and so on. You 
can cheat by invoking Convert.ToInt32(hexNumAsString, 16). 
8. Use the fast method for transitioning  between hexadecimal and binary 
numeral system (each hexadecimal digit turns to 4 binary bits). 
9. Use the fast method for transitioning from binary to hexadecimal numeral 
system (each 4 binary bits correspond to a hexadecimal digit). 
10. Directly apply the Horner scheme. 
11. Scan the digits of the Roman number from left to right and add them 
up to a sum, which is initialized with a 0. When processing each Roman 
digit, take it with a positive or negative sign, depending on the digit 
after it (whether it has a bigger or smaller decimal value). 
12. Take a look at the numbers from 1 to 9 and their corresponding Roman 
representation with the digits "I", "V" and "X": 
1 -> I 
2 -> II 
3 -> III 
4 -> IV 
5 -> V 
6 -> VI 
7 -> VII 
8 -> VIII 
9 -> IX 
We have exactly the same correspondence for the numbers 10, 20, …, 
90 with their Roman representation "X", "L" and "C". The same is valid 
for the numbers 100, 200, …, 900 and their Roman representation with 
"C", "D" and "M" and so on. 
We are now ready to convert the number N into the Roman numeral 
system. It must be in the range [1 …3999], otherwise we should report 
an error. First we separate the thousands (N / 1000) and replace them 
with their Roman counterpart. After that we separate the hundreds (N / 
100) % 10) and separate them with their Roman counterpart and so on. 
13. You can convert first from S-based system  to decimal number  and 
then from decimal number to D-based system. 
14. If you execute the calculations correctly, you will get 32.00 (for float), 
49.9999999657788 (for double) and 50.00 (for decimal) respectively. 
The differences come from the fact that 0.000001 has no exact 
representation as float and double. You may notice also that addi ng 
decimal values is at least 10 times slower than adding double values. 
15. Use the special method for conversion of single precision floating -point 
numbers to a  sequence of 4 bytes:  System.BitConverter.GetBytes( 
<float>). Then use bitwise operations  (shifting and bit masks)  to 
extract the sign, mantissa and exponent following the IEEE 754 standard.