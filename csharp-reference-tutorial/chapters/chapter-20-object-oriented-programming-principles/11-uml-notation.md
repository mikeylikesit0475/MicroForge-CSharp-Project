# UML Notation

UML (Unified Modeling Language)  was mentioned in the section about 
inheritance where we discussed class diagrams. The UML notation defines 
several additional types of diagrams. Let’s check out some of them briefly.

## Use Case Diagrams

They are used when we elicit the requirements for the description of possible 
actions. Actors represent roles (types of users). 
Use cases describe interaction between the actors and the system. The use 
case model is a group of use cases – it provides a com plete description of a 
system’s functionality.

### Use Case Diagrams – Example

Here is how a use case diagram looks like: 

 
The actor (the “dwarf” in the diagram) is someone who interacts with the 
system (a user, external system or, for instance, an external e nvironment). 
The actor has a unique name  and, possibly, a description. I our case 
actors are the WatchUser and the WatchRepairPerson. 
A use case (the “egg” in the diagram) describes a single functionality of 
the system, a single action that can be performed by some actor . It has a 
unique name  and is related to actors . It can have input and output 
conditions. Most frequently, it contains a flow of operations (a process). It can 
also have other requirements. We have three use cases in the diagram above: 
ReadTime, SetTime and ChangeBattery. 
A package holds several logically related use cases. 
Lines connect actors to the use cases they perform. An actor can perform or 
be involved in one or several use cases.

## Sequence Diagrams

Sequence diagra ms are used when modeling the requirements of process 
specification and describing use case scenarios more extensively. They allow 
describing additional participants in the processes  and the sequence of the 
actions over the time . They are used in designing  the descriptions of system 
interfaces. 
Sequence diagrams describe what happens over the time, the interactions 
over the time, the dynamic view  over the system, a sequence of steps , 
just like an algorithm.

## Sequence Diagrams – Example

Here is how a sequence diagram looks like: 


 
Classes are depicted with columns  (lifelines). Messages (actions)  are 
depicted with arrows  and text above the arrows . Participants are depicted 
with wide rectangles. States are depicted with dashed lines.  The period of 
activity ( activation) of certain class during the time is depicted as narrow 
rectangles.

### Messages – Example

The direction of the arrow designates the sender and the recipient of a 
message (a method call in OOP). Horizontal dashed lines depict data flow:

## Statechart Diagrams

Statechart diagrams describe the possible states of certain process and the 
possible transitions between them  along with the conditions for the 
transitions. They represent finite-state automata (state machines). Below 
we have an example of statec hart diagram that illustrates the states and 
transitions of typical process of changing the current time of a wall clock  
which has two buttons and a screen:

## Activity Diagrams

Activity diagrams  are a special type of statechart diagrams  where 
conditions are actions. They show the flow of actions in a system: