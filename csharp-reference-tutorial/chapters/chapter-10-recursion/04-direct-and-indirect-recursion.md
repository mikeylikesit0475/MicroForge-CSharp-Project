# Direct and Indirect Recursion

Direct and Indirect Recursion 
When in the body of a method there is a call to the same method, we say that 
the method is directly recursive. 
If method A calls method B, method B calls method C, and method C calls 
method A we call the methods A, B and C indirectly recursive or mutually 
recursive. 
Chains of calls in indirect recursion can contain multiple methods, as well as 
branches, i.e. in the presence of one condition one method to be called, and 
provided a different condition another to be called.