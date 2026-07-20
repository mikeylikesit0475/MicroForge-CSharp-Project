# Object-Oriented Modeling (OOM)

Suppose we have a problem or task to solve. The problem usually comes from 
the real word. It exists in a reality we are going to call its surrounding 
environment. 
Object-oriented modeling (OOM ) is a process associated with OOP where 
all objects related to the problem we a re solving are brought out (a model is 
created). Only the classes' characteristics, which are important for solving this 
particular problem, are elicited. The rest are ignored. That way, we create a 
new reality, a simplified version of the original  one (its model), such that 
it allows us to solve the problem or task. 
For example, if we model a ticketing system , the important 
characteristics of a passenger could be their name, their age, whether they 
use a discount and whether they are male or female (if we sell sleeping 


berths). A passenger has many other not important characteristics we 
aren’t concerned about, such as the color of their eyes, what shoe size they 
wear, what books they like or what beer they drink. 
By modeling, a simplified model of reality i s created in order to solve a 
specific task. In object -oriented modeling, the model is created by means of 
OOP: via classes, class attributes, class methods, objects, relations between 
classes, etc. Let’s scrutinize this process.

## Steps in Object-Oriented Modeling

Object-oriented modeling is usually performed in these steps:

## Identification of Classes

- Identification of classes.

## Identification of Class Attributes

- Identification of class attributes.

## Identification of Operations on Classes

- Identification of operations on classes. 
- Identification of relations between classes. 
We will consider a short example through which we will demonstrate how to 
apply these steps. 
Identification of Classes 
Suppose we have the following excerpt from a system’s specification: 
The user must be able to describe each product by its characteristics, 
including name and product number.  If the barcode doesn ’t match the 
product, an error must be generated on the error screen. There has to be a 
daily report for all transactions specified in section 9.3. 
Here is how we identify key concepts: 
The user must be able to describe each product by its characteristics, 
including name and product number . If the barcode doesn’t match the 
product, an error must be generated on the error screen. There has to be 
a daily report for all transactions specified in section 9.3. 
We have just identified the classes we will need. The names of the classes 
are the nouns in the text , usually common nouns in singular like Student, 
Message, Lion. Avoid names that don ’t come from the text, such as: 
StrangeClass, AddressTheStudentHas. 
Sometimes it ’s difficult to dete rmine whether some subject or phenomena 
from the real world has to be a class. For example, the address can be 
defined as a class Address or a string . The better we explore the problem, 
the easier it will be to decide which entities must be represented as classes. 
When a class becomes large and complicated it has to be broken down into 
several smaller classes. 

Identification of Class Attributes 
Classes have attributes (characteristics) , for example the class Student 
has a name, institution and a list of cou rses. Not all characteristics are 
important for a software system. For example, as far as the class Student is 
concerned eye color is a non -essential characteristic. Only essential 
characteristics have to be modeled. 
Identification of Operations on Classes 
Each class must have clearly defined responsibilities  – what objects or 
processes from the real world it identifies and what tasks it performs. Each 
action in the program is performed by one or several methods in some class. 
The actions are modeled as operations (methods). 
A combination of verb + noun is used for the name of a method , e.g. 
PrintReport(), ConnectToDatabase(). We cannot define all methods of a 
given class immediately. Firstly, we define the most important methods – 
those that implement the basic responsibilities of the class. Over time 
additional methods appear.

## Identification of Relationships between Classes

If a student is from a faculty and this is important for the task we are solving, 
then student and faculty are related,  i.e. the Faculty class has a list of 
Students. These relations are called associations (remember the " Class 
Diagrams" section).