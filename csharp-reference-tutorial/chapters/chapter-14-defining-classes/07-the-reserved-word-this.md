# The Reserved Word "this"

The Reserved Word "this" 
The reserved word this in C# is used to reference the current object , 
when one is used from method  in the same class . This is the object, which 
method or constructor is called. The reserved word can be deemed as an 

address (reference), given priory from the language authors, with which we 
access the elements (fields, methods, constructor) of the own class: 
this.myField; // access a field in the class 
this.DoMyMethod(); // access a method in the class 
this(3, 4); // access a constructor with two int parameters 
Currently, we will not e xplain the given code above. La ter, we will do it in 
other sections of this chapter, dedicated to the elements of the cl ass (fields, 
methods, constructors) and as well related to the reserved word this.