# What Does Quality Programming Code Mean?

The quality of a program encompasses two aspects: the quality perceived by 
the user (called external quality), and the quality in regard to the internal 
organization (called internal quality). 
The external quality is largely determined by the operational correctness 
of the particular program  (absence of defects) . Things like usability and 
intuitiveness of the user interface  (UI) do greatly influence the external 
quality as well. Performance, a term which includes operational speed, 
memory usage and resource utilization, also plays in the equation, whenever 
these things matter. 
Internal quality , on the other hand, is determined by how well th e 
program is built . It depends on whether the employed design and 
architecture are suitable and sufficiently simple, and whether it is easy to 
make a change or to add new functionality  (maintainability). The 
comprehensibility of the implementation and the readability of the code are 
vital as well. In general, internal quality mostly has to do with the code of the 
program and its internal work.

## Characteristics of Quality Code

Quality code is easy to read and understand. It is maintained easily and 
straightforwardly. It must withstand any kind of input without breaking or 
behaving strangely, and be well tested. The design and the architecture 
must be suitable and not over -engineered. Documentation should be at a 
decent level, or at least the code should be self-documenting. Formatting 
should be adequately chosen and applied consistently throughout the whole 
project. 
At all levels (modules, classes, methods) there should be a strong relation and 
a high focus of the responsibilities ( strong cohesion) – that means, a piece 
of code should only do one particular thing. 
Functional independence (or more precisely, loose coupling ) between 
modules, classes and methods is crucially important. Suitable and consistent 
naming of all program identifiers is a must. Documentation should be 
embedded in the code itself.