# Methods

In chapter "Methods" we have discussed how to declare and use a method. 
In this section we will revise how we do this and we will focus on some 
additional features from the process of creating methods.  Till now we have 
used static methods only. Now it is time to  start using non -static (instance) 
methods.

## Declaring of Class Method

The declaration of methods is done in the following way: 
// Method definition 
[<modifiers>] [<return_type>] <method_name>([<parameters_list>]) 
```csharp
{ 
 // … Method's body … 
 [<return_statement>]; 
} 
The mandatory elements for declaration of a method are the type of the 
return value <return_type>, the name of the method <method_name> and 
```
the opening and the closing brackets – "(" and ")". 
The parameter list <params_list> is not mandatory. We use it to pass data 
to the method, which we declare, when this is required. 
We know, if the return type <return_type> is void, then 
<return_statement> can be declared without the return statement. If 
<return_type> is different from void, the method has to return a result with 
the help of the reserved word return and an expression, which is from the 
type <return_type> or a compatible one. 
The work, which the method has to do, is situated in the method body, 
enclosed in curly brackets – "{" and "}". 
We already discussed some of the access modifiers that can be used in the 
declaration of a method in the section "Visibility of Methods and Fields" we will 
review in details this again. 
The static modifier will be explained in depth in the section " Static Classes 
and Static Members".

### Example – Method Declaration

Let’s see the declaration of a method, which sums two values: 
int Add(int number1, int number2) 
```csharp
{ 
 int result = number1 + number2; 
 return result; 

} 
The name of the method is Add and the return value type is int. The 
```
parameter list consists of two elements – the variables number1 and number2. 
Accordingly, the return value is the sum of the two parameters as a result.