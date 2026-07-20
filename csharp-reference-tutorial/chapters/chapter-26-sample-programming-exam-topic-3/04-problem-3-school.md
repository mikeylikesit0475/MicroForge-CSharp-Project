# Problem 3: School

Students, which are studying in a  school, are separated in to groups. Each 
of the groups has a teacher. The following information is kept for the 
students: first name and last name.  The following information is kept for the 
groups: name, a list of students and teacher. The following information is kept 
for the teachers: first name, last name and a list of groups he is teaching. 
Each teacher can teach more than one group.  The following information is 
kept for the school: name, list of the teachers, list of th e groups and list of 
the students. Your task is to: 
1. Design a set of classes  and relationships between them to model the 
school, its students, teachers and groups. 
2. Implement functionality for add / edit / delete  teachers, students, 
groups and their properties. 
3. Implement functionality for printing in human -readable form  the 
school, the teachers, the students, the groups and their properties. 
4. Write a sample test program , which demonstrates the work of the 
implemented classes and methods. 
Example of school with teachers, students and groups: 
School "Freedom". Teachers: Tom Johnson, Elizabeth Hall. 
Group "English": David Russell, Nicholas Grant, Emma Fletcher, 
John Brown, Emily Cooper, teacher Elizabeth Hall. 
Group "French": Kevin Simmons, Ian Hayes, teacher Elizabeth 
Hall. 
Group "Informatics": Jessica Carter, Andrew Cooper, Ashley 
Moore, Olivia Adams, Jonathan Smith, teacher Tom Johnson.

## Start Thinking on the Problem

This is a good example of an exam assignment the purpose of which is to test 
your abilities to use object-oriented programming ( OOP) for modeling 

problems from the real life, design classes and relationships between them  
as well as working with collections. 
All we need to solve this problem is to use our object -oriented modeling 
skills that we have gained from chapter “Object -Oriented Programming 
Principles”, especially from the section “Object-Oriented Modeling (OOM)”.

## Inventing an Idea for Solution

In this task there is nothing complex to invent. It is not algorithmic and 
there is not anything to be thought up. We must define a class for each of 
the described in the problem description objects (students, teachers, 
school students, groups, school, etc.) and after that we should define in each 
```csharp
class properties to describe its characteristics and methods to implements 
the actions the class can do, e.g. printing in human-readable form. That’s all. 
```
Following the directions from the section “Object -Oriented Modeling (OOM)”  
we could identify the nouns  in the problem description. Some of them 
should be modeled as classes; some of them as properties ; and some of them 
may not be important and could be disregarded. 
Reading the text from the problem description and analyzing the nouns, w e 
could come to the idea to model the school through defining few interrelated 
classes: Student, Group, Teacher and School. For testing the classes we 
could create a class SchoolTest, which will create few objects of each class 
and will demonstrate their work in action.

## Checking the Idea

We will not check the idea  because there is nothing to be proven or 
checked. We need to write few classes to model a real -world situa tion: a 
school with students, teachers and groups.

## Dividing the Problem into Subproblems

The implementation of each of the c lasses we already identified can be 
considered a subproblem of the given school modeling problem . Thus we 
have the following subproblems: 
- Class for the students – Student. Students will have first name, last 
name and a method for printing in human-readable form – ToString(). 
- Class for the groups – Group. Groups will have a name, a teacher and a 
list of students.  It will also have а method for printing in human -
readable form. 
- Class for the teachers – Teacher. Teachers will have first name, last 
name and a list of groups, as well as а method for printing in human -
readable form. 
- Class for the school – School. It wil l have a name and will hold all 
students, all teachers and all groups. 

- Class for testing the other classes – SchoolTest. It will create a school 
with a few students, a few groups holding subsets of the students and a 
few teachers. It will assign one teache r per group and a few groups per 
teacher accordingly. Finally the class will print the school and all its 
teachers, groups and students.

## Think about the Data Structures

The data structures, needed for this problem, are of two main groups: 
classes and relationships between the classes . Classes will be classes. 
We have nothing to decide here. The interesting part is how to describe the 
relationships between the classes, e.g. when a group has a collection of 
students. 
To describe a relationship (link) between two classes we can use an array. 
With an array we have access to its elements by index, but once it is created 
we will not be able to add or delete items (arrays have a fixed size). This 
makes it uncomfortable for our problem , because we don’t know how 
many students we will have in the school and more students can be added or 
removed after the school is once created. 
List<T> seems more comfortable . It has the advantages of an array and 
also has a variable length – it is easy to add or delete elements. List<T> can 
hold lists of students (inside the school and inside a group), lists of teachers 
(inside a school) and lists of groups (inside a school and inside a teacher).  
So far it seems List<T> is the most appropriate for holding aggregations of 
objects inside  another object. To be convinced we will analyze a few more 
data structures. For example hash-table – it is not appropriate in this case, 
because the school, teachers, students and groups are not of a key -value 
type. A hash-table would be appropriate if we  need to search a student by its 
unique student ID, but this is not the case. Structures like stack and queue 
are inappropriate – we do not have LIFO or FIFO behavior. 
The structure “set” and its implementation HashSet<T> may be used when we 
need to have uniqueness for given key. It would be good sometimes to use 
this structure to avoid duplicates. We must recall that HashSet<T> requires 
the methods GetHashCode() and Equals(…) to be correctly defined by the T 
type. Shall we use sets  and where? To answer this question we need to 
recall the problem description. What is says? We need to design a set of 
classes to model the school, its students, teachers and groups and 
functionality for add / edit / delete teachers, students, groups and their 
properties. The easiest way to implement it is to hold a list of students in the 
school, a list of groups for each teacher, etc. Lists are easier to implement. 
Sets give uniqueness, but require Equals() and GetHashCode(). Sets need 
more effort to be used. So we may use lists to simplify our work. 
According to the requirements the school should allow add / edit / delete of 
students, teachers and groups. The easiest way to implement this is to expose 
the lists of students, teachers and groups as public properties. List<T> 

already have methods for add and delete of its elements and its elements are 
accessible by index and editable. It does the job. 
Finally we choose to use List<T> for all aggregations in our classes and we 
will expose all the class member s as properties with read and write access. 
We do not have a good reason to restrict the access to the members or 
implement immutable behavior.

## Implementation: Step by Step

It’s appropriate to start the implementation with the class Student because it 
does not depend on any of the other classes.

### Step 1: Class Student

In the problem definition we have only two fields representing the first name 
and the last name of a student. We may add a property Name, which returns 
a string with the full name of the student and a ToString() implementation 
to print the student in human -readable form . We might define the class 
Student as follows: 
Student.cs 
```csharp
public class Student 
{ 
 public string FirstName { get; set; } 
 public string LastName { get; set; } 
 
 public Student(string firstName, string lastName) 
 { 
  this.FirstName = firstName; 
  this.LastName = lastName; 
 } 
 
 public string Name 
 { 
  get 
  { 
   return this.FirstName + " " + this.LastName; 
  } 
 } 
 
 public override string ToString() 
 { 
  return "Student: " + this.Name; 
 } 
} 

We want to allow the class members to be editable so we define the 
```
FirstName and LastName as public read-write properties.

### Testing the Class Student

Before continuing forward we want to test the class Student to be sure it is 
correct. Let’s create a testing class with a Main() method and create a 
student in it and print the student: 
```csharp
class TestSchool 
{ 
 static void Main() 
 { 
  Student studentPeter = new("Peter", "Lee");
  Console.WriteLine(studentPeter); 
 } 
} 
We run the testing program and we get a correct result: 
```
Student: Peter Lee 
Now we can continue with the implementation of the other classes.

### Step 2: Class Group

The next class we can define is Group. We choose it because the only one 
required for its definition is the class Student. The properties, which we will 
define, are the name of the group , a list of the students, which belong to 
the group, and a teacher who teaches the group . To implement the list with 
of the  students we will use List<Student>. We will add a ToString() 
method to enable printing the group in a human -readable text form. Let’s see 
the implementation of the class Group: 
Group.cs 
```csharp
using System.Collections.Generic; 
 
public class Group 
{ 
 public string Name { get; set; } 
 public List<Student> Students { get; set; } 
 
 public Group(string name) 
 { 
  this.Name = name; 
  this.Students = new List<Student>(); 

 } 
 
 public override string ToString() 
 { 
  StringBuilder groupAsString = new();
  groupAsString.AppendLine("Group name: " + this.Name); 
```
  groupAsString.Append("Students in the group: " + 
   this.Students); 
  return groupAsString.ToString(); 
 } 
} 
It is important when we create a group to assign an empty list  of students 
to it. If we leave the list of students unassigned, it will be null and when we 
try to add a student, we will get an exception.

### Testing the Class Group

Let’s now test the  Group class. Let’s create a sample group, add few 
students to it and print the group at the console: 
```csharp
static void Main() 
{ 
 Student studentPeter = new("Peter", "Lee");
 Student studentMaria = new("Maria", "Steward");
 Group groupEnglish = new("English language course");
 groupEnglish.Students.Add(studentPeter); 
 groupEnglish.Students.Add(studentMaria); 
 Console.WriteLine(groupEnglish); 
} 
```
We run the above testing code and we find a bug: 
Group name: English language course 
Students in the group: 
System.Collections.Generic.List`1[Student] 
It seems like the list of students is  printed incorrectly. It is easy to find 
why. The List<T> class does not correctly implement ToString() and we 
need to use another way to print a list of students. We can do this with a for-
loop but let’s try something shorter and more elegant: 
```csharp
using System.Linq; 
```
… 
groupAsString.Append("Students in the group: " + 
 string.Join(", ", this.Students.Select(s => s.Name))); 

The above code uses an extension method and a lambda expression to 
select all students’ names as IEnumerable<string> and then combines them 
into a string using a comma as separator. Let’s test the Group class again 
after the fix: 
Group name: English language course 
Students in the group: Peter Lee, Maria Steward 
The group class now works correctly. 
Let’s think a bit: who is teaching the students in the group? We should have 
a teacher , right. Let’s try to add the simplest possible class Teacher and 
define a property of it in the Group class: 
```csharp
public class Teacher 
{ 
 public string FirstName { get; set; } 
 public string LastName { get; set; } 
 
 public string Name 
 { 
  get 
  { 
   return this.FirstName + ' ' + this.LastName; 
  } 
 } 
} 
 
public class Group 
{ 
 public string Name { get; set; } 
 public List<Student> Students { get; set; } 
 public Teacher Teacher { get; set; } 
 
 public Group(string name) 
 { 
  this.Name = name; 
  this.Students = new List<Student>(); 
 } 
 
 public override string ToString() 
 { 
  StringBuilder groupAsString = new();
  groupAsString.AppendLine("Group name: " + this.Name); 
```
  groupAsString.Append("Students in the group: " + 
   string.Join(", ", this.Students.Select(s => s.Name))); 

  groupAsString.Append("\nGroup teacher: " + 
   this.Teacher.Name); 
  return groupAsString.ToString(); 
 } 
} 
Let’s test again with our sample groups of two students studying English: 
Student studentPeter = new Student("Peter", "Lee"); 
Student studentMaria = new Student("Maria", "Steward"); 
Group groupEnglish = new Group("English language course"); 
groupEnglish.Students.Add(studentPeter); 
groupEnglish.Students.Add(studentMaria); 
```csharp
Console.WriteLine(groupEnglish); 
```
We find another bug: 
Unhandled Exception: System.NullReferenceException: Object 
reference not set to an instance of an object. 
   at Group.ToString() … 
We step through the debugger and we see that we try to print the teacher’s 
name but there is no teacher (it is null). This is easy to fix. We could check 
whether the teacher exists prior to printing it in the ToString() method: 
if (this.Teacher != null) 
```csharp
{ 
 groupAsString.Append("\nGroup teacher: " + this.Teacher.Name); 
} 
Let’s test again after the fix. Now we get the following correct result: 
```
Group name: English language course 
Students in the group: Peter Lee, Maria Steward 
Let’s now add a teacher to the testing group and check what happens: 
Student studentPeter = new Student("Peter", "Lee"); 
Student studentMaria = new Student("Maria", "Steward"); 
Group groupEnglish = new Group("English language course"); 
groupEnglish.Students.Add(studentPeter); 
groupEnglish.Students.Add(studentMaria); 
Teacher teacherNatasha = new Teacher() { 
 FirstName = "Natasha", LastName = "Walters" }; 
groupEnglish.Teacher = teacherNatasha; 

```csharp
Console.WriteLine(groupEnglish); 
```
The result is correct: 
Group name: English language course 
Students in the group: Peter Lee, Maria Steward 
Group teacher: Natasha Walters 
Now the Group class works correctly. We can continue with the next class.

### Step 3: Class Teacher

Let’s define the class Teacher. We already have so me piece of it, but let’s 
define it in a better way. The teacher should have first name, last name and a 
list of group he teaches and should be printable in human -readable form. We 
can define it directly repeating the logic in the Group class: 
Teacher.cs 
```csharp
public class Teacher 
{ 
 public string FirstName { get; set; } 
 public string LastName { get; set; } 
 public List<Group> Groups { get; set; } 
 
 public Teacher(string firstName, string lastName) 
 { 
  this.FirstName = firstName; 
  this.LastName = lastName; 
  this.Groups = new List<Group>(); 
 } 
 
 public string Name 
 { 
  get 
  { 
   return this.FirstName + " " + this.LastName; 
  } 
 } 
 
 public override string ToString() 
 { 
  StringBuilder teacherAsString = new();
  teacherAsString.AppendLine("Teacher name: " + this.Name); 
```
  teacherAsString.Append("Groups of this teacher: " + 

   string.Join(", ", this.Groups.Select(s => s.Name))); 
  return teacherAsString.ToString(); 
 } 
} 
Like in the class Group, it is important to create and empty list of groups 
instead of leaving the Groups property uninitialized.

### Testing the Class Teacher

Before going further, let’s test the class Teacher. We can create a teacher 
with a few groups and print it at the console: 
```csharp
static void Main() 
{ 
 Teacher teacherNatasha = new("Natasha", "Walters");
 Group groupEnglish = new("English language");
 Group groupFrench = new("French language");
 teacherNatasha.Groups.Add(groupEnglish); 
 teacherNatasha.Groups.Add(groupFrench); 
 Console.WriteLine(teacherNatasha); 
} 
```
The result is correct: 
Teacher name: Natasha Walters 
Groups of this teacher: English language, French language 
This was expected. We just repeated the same logic like in the Group class 
which was already tested and all bugs in it was fixed. We found once again 
how important is to write the code step by step with testing and bug -
fixing after each step , right? The bug with incorrectly printing the list of 
students would have been repeated when printing the list of groups, right?

### Step 4: Class School

We finish our object model with the definition of the class  School, which 
uses all of the classes we already defined. It should have a name and should 
hold a list of students, a list of teachers and a list of groups: 
```csharp
public class School 
{ 
 public string Name { get; set; } 
 public List<Teacher> Teachers { get; set; } 
 public List<Group> Groups { get; set; } 
 public List<Student> Students { get; set; } 
 

 public School(string name) 
 { 
  this.Name = name; 
  this.Teachers = new List<Teacher>(); 
  this.Groups = new List<Group>(); 
  this.Students = new List<Student>(); 
 } 
} 
Before testing the class, let’s think what the class School is expected to 
```
do. It should hold the students, teachers and groups and should be  printable 
at the console, right? If we print the school, what should be printed? Maybe 
we should print its name, all its students (with their inner details), all its 
teachers (with their inner details) and all its groups (with their inner details). 
Let’s try to define the ToString() method for the class School: 
```csharp
public override string ToString() 
{ 
 StringBuilder schoolAsString = new();
 schoolAsString.AppendLine("School name: " + this.Name); 
```
 schoolAsString.AppendLine("Teachers: " + 
  string.Join(", ", this.Teachers.Select(s => s.Name))); 
 schoolAsString.AppendLine("Students: " + 
  string.Join(", ", this.Students.Select(s => s.Name))); 
 schoolAsString.Append("Groups: " + 
  string.Join(", ", this.Groups.Select(s => s.Name))); 
 foreach (var teacher in this.Teachers) 
```csharp
 { 
  schoolAsString.Append("\n---\n"); 
  schoolAsString.Append(teacher); 
 } 
 foreach (var group in this.Groups) 
 { 
  schoolAsString.Append("\n---\n"); 
  schoolAsString.Append(group); 
 } 
 foreach (var student in this.Students) 
 { 
  schoolAsString.Append("\n---\n"); 
  schoolAsString.Append(student); 
 } 
 return schoolAsString.ToString(); 
} 

We shall not test the class School, because this will be the main purpose of 
```
our last class: SchoolTest.

### Step 5: Class SchoolTest

The final thing is the implementation of the class SchoolTest the purpose of 
which is to demonstrate all the classes we have defined  (Student, Group, 
Teacher and School) and their methods and properties.  This is our last 
subproblem. For the demonstration we create a sample school with a few 
students, a few teachers and a few groups and we print it: 
SchoolTest.cs 
```csharp
class TestSchool 
{ 
 static void Main() 
 { 
  // Create a few students 
  Student studentPeter = new("Peter", "Lee");
  Student studentGeorge = new("George", "Redwood");
  Student studentMaria = new("Maria", "Steward");
  Student studentMike = new("Michael", "Robinson");
 
  // Create a group and add a few students to it 
  Group groupEnglish = new("English language course");
  groupEnglish.Students.Add(studentPeter); 
  groupEnglish.Students.Add(studentMike); 
  groupEnglish.Students.Add(studentMaria); 
  groupEnglish.Students.Add(studentGeorge); 
 
  // Create a group and add a few students to it 
  Group groupJava = new("Java Programming course");
  groupJava.Students.Add(studentMaria); 
  groupJava.Students.Add(studentPeter); 
 
  // Create a teacher and assign it to few groups 
  Teacher teacherNatasha = new("Natasha", "Walters");
  teacherNatasha.Groups.Add(groupEnglish); 
  teacherNatasha.Groups.Add(groupJava); 
  groupEnglish.Teacher = teacherNatasha; 
  groupJava.Teacher = teacherNatasha; 
 
  // Create another teacher and a group he teaches 
  Teacher teacherSteve = new("Steve", "Porter");
  Group groupHTML = new("HTML course");
  groupHTML.Students.Add(studentMike); 

  groupHTML.Students.Add(studentMaria); 
  groupHTML.Teacher = teacherSteve; 
  teacherSteve.Groups.Add(groupHTML); 
 
  // Create a school with few students, groups and teachers 
  School school = new("Saint George High School");
  school.Students.Add(studentPeter); 
  school.Students.Add(studentGeorge); 
  school.Students.Add(studentMaria); 
  school.Students.Add(studentMike); 
  school.Groups.Add(groupEnglish); 
  school.Groups.Add(groupJava); 
  school.Groups.Add(groupHTML); 
  school.Teachers.Add(teacherNatasha); 
  school.Teachers.Add(teacherSteve); 
 
  // Modify some of the groups, student and teachers 
  groupEnglish.Name = "Advanced English"; 
  groupEnglish.Students.RemoveAt(0); 
  studentPeter.LastName = "White"; 
  teacherNatasha.LastName = "Hudson"; 
 
  // Print the school 
  Console.WriteLine(school); 
 } 
} 
We run the program and we get the expected result: 
```
School name: Saint George High School 
Teachers: Natasha Hudson, Steve Porter 
Students: Peter White, George Redwood, Maria Steward, Michael 
Robinson 
Groups: Advanced English, Java Programming course, HTML course 
--- 
Teacher name: Natasha Hudson 
Groups of this teacher: Advanced English, Java Programming 
course 
--- 
Teacher name: Steve Porter 
Groups of this teacher: HTML course 
--- 
Group name: Advanced English 
Students in the group: Michael Robinson, Maria Steward, George 

Redwood 
Group teacher: Natasha Hudson 
--- 
Group name: Java Programming course 
Students in the group: Maria Steward, Peter White 
Group teacher: Natasha Hudson 
--- 
Group name: HTML course 
Students in the group: Michael Robinson, Maria Steward 
Group teacher: Steve Porter 
--- 
Student: Peter White 
--- 
Student: George Redwood 
--- 
Student: Maria Steward 
--- 
Student: Michael Robinson 
Of course in real life programs do not start from the first time, but in this task 
the mistakes you could make are trivial so there ’s no point in discussing 
them. All classes are impl emented and tested . We are alm ost finished 
with this problem.

## Testing the Solution

As usually, it remains to test if the entire solution is working  correctly. 
We’ve already done this. We tested all the classes in their nominal case. 
We can do some tests with the border cases, for instance a group without 
students, empty school, etc.  It seems like these cases work correctly. We 
might test a student without a name, but it is unclear whether the class 
should keep itself of incorrect name s and what is a correct name. We can 
leave these classes without checks for the names. It will be a responsibility of 
their caller to put correct names though their constructors and properties. The 
problem description says nothing about this. 
It is interes ting how we delete a student . In our current implementation, if 
we delete a student, we will need to remove it from the school and to remove 
it from all groups he belongs to. The removal itself will require the student to 
have the Equals() method defined c orrectly or we should compare students 
by hand (property by property). It is unclear from the problem description 
how exactly the “delete student” operation should work. 
We assume we don’t have time and we submit the solution in its current state 
without efficient delete operation . Sometimes it takes too much time to 
fix something and it is better to leave it in not perfect form. Below is the full 
source code of the solution of the school modeling problem: 

School.cs 
```csharp
using System; 
using System.Collections.Generic; 
using System.Linq; 
using System.Text; 
 
public class Student 
{ 
 public string FirstName { get; set; } 
 public string LastName { get; set; } 
 
 public Student(string firstName, string lastName) 
 { 
  this.FirstName = firstName; 
  this.LastName = lastName; 
 } 
 
 public string Name 
 { 
  get 
  { 
   return this.FirstName + " " + this.LastName; 
  } 
 } 
 
 public override string ToString() 
 { 
  return "Student: " + this.Name; 
 } 
} 
 
 
public class Group 
{ 
 public string Name { get; set; } 
 public List<Student> Students { get; set; } 
 public Teacher Teacher { get; set; } 
 
 public Group(string name) 
 { 
  this.Name = name; 
  this.Students = new List<Student>(); 
 } 

 
 public override string ToString() 
 { 
  StringBuilder groupAsString = new();
  groupAsString.AppendLine("Group name: " + this.Name); 
```
  groupAsString.Append("Students in the group: " + 
   string.Join(", ", this.Students.Select(s => s.Name))); 
  if (this.Teacher != null) 
```csharp
  { 
```
   groupAsString.Append("\nGroup teacher: " + 
    this.Teacher.Name); 
  } 
  return groupAsString.ToString(); 
 } 
} 
 
```csharp
public class Teacher 
{ 
 public string FirstName { get; set; } 
 public string LastName { get; set; } 
 public List<Group> Groups { get; set; } 
 
 public Teacher(string firstName, string lastName) 
 { 
  this.FirstName = firstName; 
  this.LastName = lastName; 
  this.Groups = new List<Group>(); 
 } 
 
 public string Name 
 { 
  get 
  { 
   return this.FirstName + " " + this.LastName; 
  } 
 } 
 
 public override string ToString() 
 { 
  StringBuilder teacherAsString = new();
  teacherAsString.AppendLine("Teacher name: " + this.Name); 
```
  teacherAsString.Append("Groups of this teacher: " + 
   string.Join(", ", this.Groups.Select(s => s.Name))); 
  return teacherAsString.ToString(); 

 } 
} 
 
```csharp
public class School 
{ 
 public string Name { get; set; } 
 public List<Teacher> Teachers { get; set; } 
 public List<Group> Groups { get; set; } 
 public List<Student> Students { get; set; } 
 
 public School(string name) 
 { 
  this.Name = name; 
  this.Teachers = new List<Teacher>(); 
  this.Groups = new List<Group>(); 
  this.Students = new List<Student>(); 
 } 
 
 public override string ToString() 
 { 
  StringBuilder schoolAsString = new();
  schoolAsString.AppendLine("School name: " + this.Name); 
```
  schoolAsString.AppendLine("Teachers: " + 
   string.Join(", ", this.Teachers.Select(s => s.Name))); 
  schoolAsString.AppendLine("Students: " + 
   string.Join(", ", this.Students.Select(s => s.Name))); 
  schoolAsString.Append("Groups: " + 
   string.Join(", ", this.Groups.Select(s => s.Name))); 
  foreach (var teacher in this.Teachers) 
```csharp
  { 
   schoolAsString.Append("\n---\n"); 
   schoolAsString.Append(teacher); 
  } 
  foreach (var group in this.Groups) 
  { 
   schoolAsString.Append("\n---\n"); 
   schoolAsString.Append(group); 
  } 
  foreach (var student in this.Students) 
  { 
   schoolAsString.Append("\n---\n"); 
   schoolAsString.Append(student); 
  } 
  return schoolAsString.ToString(); 

 } 
} 
 
class TestSchool 
{ 
 static void Main() 
 { 
  // Create a few students 
  Student studentPeter = new("Peter", "Lee");
  Student studentGeorge = new("George", "Redwood");
  Student studentMaria = new("Maria", "Steward");
  Student studentMike = new("Michael", "Robinson");
 
  // Create a group and add a few students to it 
  Group groupEnglish = new("English language course");
  groupEnglish.Students.Add(studentPeter); 
  groupEnglish.Students.Add(studentMike); 
  groupEnglish.Students.Add(studentMaria); 
  groupEnglish.Students.Add(studentGeorge); 
 
  // Create a group and add a few students to it 
  Group groupJava = new("Java Programming course");
  groupJava.Students.Add(studentMaria); 
  groupJava.Students.Add(studentPeter); 
 
  // Create a teacher and assign it to few groups 
  Teacher teacherNatasha = new("Natasha", "Walters");
  teacherNatasha.Groups.Add(groupEnglish); 
  teacherNatasha.Groups.Add(groupJava); 
  groupEnglish.Teacher = teacherNatasha; 
  groupJava.Teacher = teacherNatasha; 
 
  // Create another teacher and a group he teaches 
  Teacher teacherSteve = new("Steve", "Porter");
  Group groupHTML = new("HTML course");
  groupHTML.Students.Add(studentMike); 
  groupHTML.Students.Add(studentMaria); 
  groupHTML.Teacher = teacherSteve; 
  teacherSteve.Groups.Add(groupHTML); 
 
  // Create a school with few students, groups and teachers 
  School school = new("Saint George High School");
  school.Students.Add(studentPeter); 
  school.Students.Add(studentGeorge); 

  school.Students.Add(studentMaria); 
  school.Students.Add(studentMike); 
  school.Groups.Add(groupEnglish); 
  school.Groups.Add(groupJava); 
  school.Groups.Add(groupHTML); 
  school.Teachers.Add(teacherNatasha); 
  school.Teachers.Add(teacherSteve); 
 
  // Modify some of the groups, student and teachers 
  groupEnglish.Name = "Advanced English"; 
  groupEnglish.Students.RemoveAt(0); 
  studentPeter.LastName = "White"; 
  teacherNatasha.LastName = "Hudson"; 
 
  // Print the school 
  Console.WriteLine(school); 
 } 
} 
```
We will not run performance tests  because the task is not of a 
computational nature which requires a fast algorithm . Operations that could 
be slow are deleting of elements  from a collection. Cr eating objects, 
assigning their properties and adding elements to their collections of child 
elements are all fast operations. Only the deletion could be slow . We could 
improve its performance by using HashSet<T> instead of List<T> in all 
aggregations. We leave this to the reader. 
Let’s make just one more note. Why we did not  notice the performance 
problem with deleting elements  earlier? Let’s recall how we proceeded 
with solving this problem. After thinking about the data structures we had to 
thing about the performance right? Did we do this step? We omitted this step 
and we found the problem too late. The conclusion is: follow the guidelines for 
problem solving. They are very wise.