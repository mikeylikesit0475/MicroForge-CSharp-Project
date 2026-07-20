# Exercises

Exercises 
1. You are given an input file  mails.txt, which contains names of users 
and their email addresses. Each line of the file looks like this: 
<first name> <last name> <username>@<host>.<domain> 
There is a requirement for email addresses – <username> can be a 
sequence of Latin letters ( a-z, A-Z) and underscore ( _), <host> is a 
sequence of lower Latin letters ( a-z), and <domain> has a limit of 2 to 4 
lower Latin letters ( a-z). Following the guidelines for problem solving  
write a program, which finds the valid email addresses and writes 
them together with the names of the users  (in the same format as in the 
input) to an output file valid-mails.txt. 
Sample input file (mails.txt): 
Steve Smith steven_smith@yahoo.com 
Peter Miller pm<5.gmail.com 
Svetlana Green svetlana_green@hotmail.com 
Mike Johnson mike*j@888.com 
Larry Cutts larry.cutts@gmail.com 
Angela Hurd angel&7@freemail.hut.fi 
Output file (valid-mails.txt): 
Steve Smith steven_smith@yahoo.com 
Svetlana Green svetlana_green@hotmail.com 
Larry Cutts larry.cutts@gmail.com 
2. You are given a labyrinth, which consists of N x N squares, and each of 
them can be passable (0) or not (x). 
In one of the squ ares our hero Jack ( *) is positioned. Two squares are 
neighbors, if they have a common wall. At one step Jack can pass from 
one passable square to its neighboring passable square. Write a program, 
which prints the number of possible exits  from given labyrinth. At the 
figure below we have 7 possible exits, reachable from the start position. 

x x x 0 x x 
0 x 0 0 0  
0 * 0 x 0 0 
x x x x 0 x 
0 0 0 0 0 x 
0 x 0 x x 0 
The input data is read from a text file named Labyrinth.in. At the first 
line in the file is the number N (2 < N < 1000). At the next N lines there 
are N characters, each either "0" or " x" or " *". The output is a single  
number and should be printed in the file Labyrinth.out. 
3. You are given a labyrinth, which consists of N x N squares , each of it 
can be passable or not. Passable cells consist of lower Latin letter 
between "a" and "z", and the non-passable – '#'. In one of the squares is 
Jack. It is marked with "*". 
Two squares are neighbors, if they have common wall. At one step Jack 
can pass from one passable square to its neighboring passable square. 
When Jack passes through passable squares, he writes down the letters 
from each square. At each exit he gets a word . Write a program, which 
from a given labyrinth prints t he words, which Jack gets from all the 
possible exits.  At the example below Jack can get 10 different words  
corresponding to its 10 possible paths he could find to some of the exits: 
a, az, aza, madk, madkm, madam, madamk, dir, did, difid. 
a # # k m # 
z # a d a # 
a * m # # # 
# d # # # # 
r i f i d # 
# d # d # t 
The input data is read from a text file named  Labyrinth.in. At the first 
line in the file there is the number N (2 < N < 10). At each of the next N 
lines there are N characters, each of them is either Latin letter between 
"a" and " z" or " #" (impassable wall)  or "*" (Jack). The output must be 
printed in the file Labyrinth.out. 
4. A company plans to create a system for managing of a sound 
recording company . The sound recording company has a name, 
address, owner and performers. Each performer has name, nickname 
and created albums. Albums are described with name, genre, year of 
creation, number of sold copies and list of songs. The songs are 
described with na me and duration. Design a set of classes with 

relationships between each other, which models the data of the record 
company. Implement a test class, which demonstrates the work of rest of 
the classes. 
5. A company plans on creating of a system for managing a company 
for real estates. The company has name, owner, tax ID, employees and 
has a list of estates for sale. Employees are described with name, work 
position and experience. The company sells several types of estates : 
apartments, houses, undeveloped areas and shops. All of them are 
characterized with area, price of square meters and location. For some of 
them there is additional information. For the apartments there is data 
about the number of the floor, whether there is an elevator in the block, 
and if it is furnished. For the houses the data is – square meters for the 
undeveloped area and for the developed (yard), how many floors it has 
and whether it is furnished. Design a set of classes with relationships 
between them, which model the data for the compan y. Implement a 
test class, which demonstrates the work of the rest of the classes.