# Modifiers and Access Levels (Visibility)

Let’s revise, from  the chapter "Methods” we know that a modifier is a 
reserved word and with the help of it we add additional information for the 
compiler and the code related to the modifier. 
In C# there are four access modifiers : public, private, protected and 
internal. The access modifiers can be used only in  front the following 
elements of the class: class declaration, fields, properties and methods.

## Modifiers and Access Levels

As we explained, in C# there are four access modifiers – public, private, 
protected and internal. Based on them we control the access  (visibility) to 
the elements of the class, in front of which they are used. The levels of access 


in .NET are public, protected, internal, protected internal  and 
private. In this chapter we will review in details only public, private and 
internal. More about protected and protected internal we will learn in 
"Object-Oriented Programming Principles".

## Access Level "public"

When we use the modifier public in front of some element, we are telling the 
compiler, that this element can be accessed from every class , no matter 
from the current project (assembly), from the current namespace. The access 
level public defines the miss of restrictions regarding the vi sibility. This 
access level is the least restricted access level in C#.

## Access Level "private"

The access level private is the one, which defines the most restrictive 
level of visibility of the class and its elements. The modifier private is used 
to indicate, that the element, to which is issued, cannot be accessed from 
any other class  (except the class, in which it is defined), even if this class 
exists in the same namespace. This is the default access level, i.e. it is used 
when there is no access level m odifier in front of the respective element of a 
class (this is true only for elements inside a class).

## Access Level "internal"

The modifier internal is used to limit the access to the elements of the class 
only to files from the same assembly, i.e. the same project in Visual Studio. 
When we create several projects in Visual Studio, the classes from will be 
compiled in different assemblies.

## Assembly

.NET assemblies  are collections of compiled types (classes and other 
types) and resources, which form  a logical unit. Assemblies are stored in a 
binary file of type .exe or .dll. All types in C# and as general in .NET 
Framework can reside only inside assemblies. By every compilation of  a .NET 
application one or several assemblies are created by the C# compiler and  
each assembly is stored inside an .exe or .dll file.