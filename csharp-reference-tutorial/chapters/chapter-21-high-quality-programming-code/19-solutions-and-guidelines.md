# Solutions and Guidelines

Solutions and Guidelines 
1. Use [Ctrl+K, Ctrl+F]  in Visual Studio to reformat the code  and see the 
differences. Then rename the variables, omit the unnecessary statements 
and variables, and make the output that is printed more meaningful. 
2. Pay special attention to the recommendations for quality code from this 
chapter. Remember your most frequent mistakes and try  to avoid them.  
The most often problem with the code written by inexperienced 
programmers is the naming. You can use the “ rename” feature in Visual 
Studio (shortcut [Ctrl+R, Ctrl+R]) to rename the identifiers in the code 
* * * *
* * *
*

when necessary. You may need to ref ormat your code through [Ctrl+K, 
Ctrl+F] in Visual Studio . You may need to extract pieces of code in 
separate method . This can be done through “Refactor”  “Extract 
Method …” feature in Visual Studio (shortcut [Ctrl+R, Ctrl+M]). 
3. Take some well-written software as an example (e.g. Wintellect Power 
Collections for .NET – http://powercollections.codeplex.com). You would 
probably find things that you would write in a different way, or things that 
this chapter suggests should be done differently. Deviations are possible 
and are completely normal. One of t he biggest differences between low -
quality and high-quality code is the consistency in following the rules . 
The rules in different projects may be different (e.g. different formatting 
style, different documentation style, different naming style, different 
project structure, etc.) but the general recommendations f or writing 
high-quality code will be followed. 
Take another example: bad code  that is hard to read, understand and 
maintain. You may find many examples in Internet but to save time you 
may look at the projects from the “High -Quality Code” course at Telerik  
Software Academy (May 2011): https://qualitycode.googlecode.com/svn/ 
trunk/2011/Exams/Final-Projects-19-May-2011/High-Quality-Code-2011-
Final-Projects.rar. There are C#, Java, C++ and PHP projects with low -
quality code that needs deep refactoring and quality improvement. 
4. The code from CTS is written by engineers with an extensive experience 
and you can rarely encounte r low-quality code there. Despite of that, 
anomalies such as using complex expressions and inappropriately named 
variables can still be seen . Try to find some examples of bad coding 
practices in CTS. Use JustDecompile or other decompilation tool because 
the source code of CTS is unavailable.  Keep in mind that local variable 
names and comments in the code are lost when the code is compiled and 
decompiled so the variable names might be incorrect. 
Instead of decompiling the .NET CTS you may look at the source code of 
Mono (the open -source .NET implementation for Linux) at GitHub: 
https://github.com/mono/mono/tree/master/mcs/class/corlib. An example 
of code that needs improvement is the Dictionary<K,T> implementation 
in Mono: Dictionary.cs. 
5. Just answer based on your personal experience. You may ask your 
colleagues whether the y use coding conventions. You may also read the 
official C# code conventions from Microsoft:  http://msdn.microsoft.com/ 
en-us/library/vstudio/ff926074.aspx. 
6. Review all the learne d concepts from this chapter and apply them to the 
code you are given . First understand how the code works  and then fix 
the bugs you discover. The best way to start is by reformatting the code 
and renaming the identifiers. Then you may write unit tests to enable 
refactoring without a risk to break something. Then step by step you may 
extract methods, remove the duplicated code , and rewrite pieces of 
the code which cannot be refactored. Be sure to test after each change.