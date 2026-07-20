# Solutions and Guidelines

Solutions and Guidelines 
1. First find a sheet of paper , on which y ou can draw the coordinate 
system and the points. In this way you can easily think up a solution  and 
consider some tests. Consider different solutions and compare them in 
terms of quality, performance and time needed to be implemented.  Hint: 
you may sort the points by X or Y and find the lines with a linear scan. 
2. Again take a pencil and a sheet. Write down many examples and consider 
them. What ideas do you come up with? Do you need more examples? 
Think over your ideas, write them down on a sheet of paper, i f you are 
sure they are correct – implement them. Think for examples that your 
program may not handle. It is always a good idea to think up specific 
examples and then to implement them. Think how your solution will work 
with different values of K and different values and count of elements in S. 
3. Use the described methodology for creating solutions to programming 
problems! The problem is difficult and you will have to spend more time 
on it. First draw the map on a sheet of paper. Try to think up a correct 
algorithm for finding the shortest path yourself. Then search for "shortest 
path algorithm" in the Internet. Most likely you will be able to find some 
article about the problem quickly. 
Check whether the algorithm is correct. Test with different cases. 
In what data structure are you going to keep the map of the city? Think 
about which are the operations your algorithm is going to use. Probably 
you will come up with idea to keep a list of the roads for every 
crossroad and the crossroads in a hash table. 
Think about the performance. Will your algorithm handle 1 ,000 
crossings and 5,000 roads fast enough? 
Write the code  step by step . First write the method, which reads the  
input file. In this case the data is entered from a file and therefore you 
can consider  it as your first step. If the data was entered from the 
console, we would have to leave it as our last step. Then implement the 
algorithm for finding the minimum path  length. If you can, break up the 
implementation into smaller steps . For example you coul d firstly find 
the length of the minimal path without the path itself, because it is easier. 
Then implement the finding of the shortest path itself (as a list of 
crossroads). Think what could happen if there are several shortest 
paths. Finally implement the output of the result. 
Test your solution! Test with an empty map. Test with a map with one 
crossroad. Test with an example, in which there are no roads between the 
crossroads. Test with a big map (1 ,000 crossings and 5 ,000 roads). You 

can generate such a  case with a small program. For the names of the 
crossroads you have to use string instead of char, right? If you use 
char, it will not be possible to have 1,000 crossings? Is your solution fast? 
Does it work correctly? 
Be careful with the input and output  data formats. Comply with the 
described format requirements in the problem statement! 
4. If you are not very good with analytic geometry, you may not come up 
with a correct solution by yourself. Search in the Internet for “convex 
hull algorithm”. Knowing that the fence is called a “convex hull” of a set 
of points in the plane, you will be able to find hundreds of articles – even 
some with source code in C#. Don’t copy others’ mistakes, especially the 
source code! Think! Study how the algorithm works. 
Check whether the algorithm is correct. Test with different examples  
(firstly on a sheet of paper). What could happen if there are a couple of 
points, lying on a line in the plane? Do you need to include all of them? 
Think what could happen if there is a c ouple of convex hulls. From which 
point will the hull start? Will you move clockwise or counterclockwise? Is 
there a requirement in the description of the task how must the points be 
ordered? What if some of the points are the same? What should happen if 
we have no points or just one point in the field? 
In what data structure will you keep the points? In what data structure 
will you keep the convex hull? 
Think about the performance. Will your algorithm work for 1,000 points? 
Write your program step by step . First implement the reading of the 
input data. Implement your convex hull finding algorithm. If it is possible, 
break your algorithm into smaller parts. Finally implement the output 
of the result in the described format. 
Test your solution!  What will happ en if we have 0 points? Try with 1 
point. Try with 2 points. Try with 5 point, which lie on a line. Does your 
algorithm work? What will happen if we have 10 points and another 10, 
which match the first 10? What will happen if we have 10 points, which lie 
one over another? What will happen if you have many points , i.e. 1,000? 
Does your algorithm work fast? What will happen if the coordinates of the 
points are large numbers (100 ,000,000; 200,000,000)? Does this affect 
your algorithm? Can you face precision errors in the calculations? 
Be careful with the input and output data. Consider the format in the 
проблем description! Do not invent your own format of the input and 
output data. The formats are defined and they must be respected. 
If you want, you could impl ement visualization of the points  and the 
convex hull using Windows Forms  or in WPF. You may i mplement a 
generator of random tests  and test your solution many times by 
observing the visualization of the hull – does it really wrap the points.