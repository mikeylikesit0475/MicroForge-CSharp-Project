# Solutions and Guidelines

Solutions and Guidelines 
1. The problem is similar to the first  problem from our sample exam. Again 
we can  read line by line the input file and with appropriate regular 
expression to check the  email addresses . Test the solution  carefully 
before you go to the next problem. 
2. Possible exits from the labyrinth are all the cells, which are positioned 
at the border of the labyrinth and are reachable from the initia l cell. The 
problem could be solved using BFS with just little modification of the 
solution of the “Escape from Labyrinth”. Test your solution carefully! 
3. The problem is similar to the previous  one, but all possible paths to the 
exit are required. You can do recursive search with backtracking (DFS) 
and keep in a StringBuilder the letters to the exit, to create the words, 
which you have to print. With bigger labyrinths the problem has no 
optimal solution (there is no way to print all the paths, without generating 
all of them, but they grow exponentially  to the labyrinth size ). Test 
carefully your solution and think of special cases that need special care. 
4. You must write the required classes : MusicCompany, Artist, Album, 
Song. Think of the links between classes and what data structures to use 
for them. For the printing redefine the method ToString() from 
System.Object. Test all methods and the border cases. 
5. The classes you must write are EstateCompany, Employee, Apartment, 
House, Shop and UndevelopedArea. Export all shared characteristics in 
separate abstract base class  Estate. Encapsulate all fields with 
properties. Override the method ToString(), which to collect the data of 
the corresponding class and print it to the console. Test all methods and 
special border cases (like missing property values).