using System;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
class CharStack    //Stack class for characters 
{
    //feilds
    private char[] stk; //char arrays for create Stack
    int top = -1;   //points to the last characters in stack

    //Methods
    public bool Full()  //Stack full check function
    {
        if (top == stk.Length - 1)  //If top points to the last characters of the stack (one less than the length), then the stack is full
            return true;
        else
            return false;
    }
    public bool Empty() //Stack ٍEmpty check function
    {
        if (top == -1)  //If top is equal to -1, it means the stack is empty
            return true;
        else
            return false;
    }
    public void Push(char c)    //Stack add function
    {
        if (Full()) //If the stack is full, it will give an error
        {
            Console.WriteLine("Stack Full");
            return;
        }
        else
            stk[++top] = c; //Otherwise, first increase the top by one and then add it to the stack
    }
    public char Pop()   //Pop function from stack
    {
        if (Empty())    //If the stack is empty, return zero
            return '\0';    
        else
            return stk[top--];  //Otherwise, remove the last object and then subtract one from the top
    }
    public char Last()  //Function to return the last character of the stack
    {
        if(Empty()) //If the stack is empty, return zero
            return '\0';
        else
            return stk[top];    //Otherwise, return the characters pointed to by top
    }


    public CharStack(int len)   //Constructor function
    {
        stk = new char[len];
    }
}
class StrStack  //Stack class for string
{
    //Feilds
    private string[] stk;    //string arrays for create Stack
    int top = -1;      //points to the last string in stack

    //Methods
    public bool Full()   //Stack full check function
    {
        if (top == stk.Length - 1)  //If top points to the last string of the stack (one less than the length), then the stack is full
            return true;
        else
            return false;
    }
    public bool Empty() //Stack ٍEmpty check function
    {
        if (top == -1)  //If top is equal to -1, it means the stack is empty
            return true;
        else
            return false;
    }
    public void Push(string s)  //Stack add function
    {
        if (Full()) //If the stack is full, it will give an error
        {
            Console.WriteLine("Stack Full");
            return;
        }
        else
            stk[++top] = s; //Otherwise, first increase the top by one and then add it to the stack
    }
    public string Pop() //Pop function from stack
    {
        if (Empty())     //If the stack is empty, return zero
            return "\0";
        else
            return stk[top--];   //Otherwise, remove the last object and then subtract one from the top
    }
    public string Last()    //Function to return the last string of the stack
    {
        if (Empty())     //If the stack is empty, return zero
            return "\0";
        else
            return stk[top];     //Otherwise, return the string pointed to by top
    }


    public StrStack(int len)    //Constructor function
    {
        stk = new string[len];
    }
}   
class Program
{
    public static int priority(char op, bool icp)   //Function to determine the priority of operators in the icp(Intracranial pressure) The second parameter specifies whether the operator is in the icp(true) or the stack(false)
    {
        if ((op == '(' || op == ')') && icp == true)
            return 0;
        else if (op == '*' || op == '/')
            return 2;
        else if (op == '-' || op == '+')
            return 3;
        else
            return 10;

    }   
    static public string cheked(char ch)    //function to check whether each character is an operator or an operand
    {
        if ((ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z') || (ch >= '0' && ch <= '9'))
            return "operand";
        else if (ch == '+' || ch == '-' || ch == '/' || ch == '*' || ch == '(' || ch == ')' )
            return "operator";
        else
            return "Invalid";
    }   
    public static string Reverse(string input)  //Function to reverse string
    {
        string reverse = "";
        for (int i = input.Length - 1; i >= 0; i--)
        {
            reverse += input[i];
        }
        return reverse;
    }

    static public string PostfixToInfix(string postfix) //Function convert postfix to infix
    {
        StrStack stk = new StrStack(postfix.Length);    //Create a string stack of string length 

        for (int i = 0; i < postfix.Length; i++)    //Scroll character by character of string
        {
            if (cheked(postfix[i]) == "operand")    //If it was an operand, add it to the stack
            {
                stk.Push(postfix[i].ToString());
            }
            else if (cheked(postfix[i]) == "operator")  //If it was an operator, the last two strings of the stack will be popped and the operator between these two will be Pushed
            {

                string s1 = stk.Pop();
                string s2 = stk.Pop();

                stk.Push("(" + s2 + postfix[i] + s1 + ")");

            }
            else if (postfix[i] == ' ') //If there was a space character, continue
            {
                continue;
            }
            else    //Otherwise, return an error in all these cases
            {
                return "Invalid Value!";
            }

        }
        return stk.Last();  //Return the last house of the stack that contains the infix expression
    }
    static public string PostfixToPrefix(string postfix)    //Function convert postfix to prefix
    {
        return InfixToPrefix(PostfixToInfix(postfix));  //By using nested functions, it converts the postfix phrase first into an infix and then into a prefix
    }
    static public string PrefixToInfix(string prefix)   //Function convert prefix to infix
    {
        StrStack stk =new StrStack(prefix.Length);  //Create a string stack of string length
        string reverse = Reverse(prefix);   //reverse string

        for (int i = 0; i < reverse.Length ; i++)    //Scroll character by character of string
        {
            if (cheked(reverse[i]) == "operand")     //If it was an operand, add it to the stack
            {
                stk.Push(reverse[i].ToString());
            }
            else if (cheked(reverse[i]) == "operator")  //If we have a visual operator, we have two modes
            {
                if (reverse[i] == ')')  //If we see ')' until we reach '(', we pop the last two strings of the stack and place the operator between the two strings and push it to the stack.
                {
                    while (reverse[i] != '(')
                    {
                        string sr1 = stk.Pop();
                        string sr2 = stk.Pop();

                        stk.Push(")" + sr2 + reverse[i] + sr1 + "(");   //Put '(' and ')' in reverse
                    }
                }
                else    //Otherwise, we pop the last two strings from the stack and place the operator between the two strings and push it to the stack.
                {
                    string s1 = stk.Pop();
                    string s2 = stk.Pop();

                    stk.Push(")" + s2 + reverse[i] + s1 + "("); //Put '(' and ')' in reverse
                }
            }
            else if (reverse[i] == ' ') //If there was a space character, continue
            {
                continue;
            }
            else    //Otherwise, return an error in all these cases
            {
                return "Invalid Value!";
            }
        }
        return Reverse(stk.Last()); //The last string of the stack is returned in reverse form, which is our prefix string
    }
    static public string PrefixToPostfix(string prefix)  //Function convert prefix to postfix
    {
        return InfixToPostfix(PrefixToInfix(prefix));   //By using nested functions, it converts the prefix phrase first into an infix and then into a postfix
    }
    static public string InfixToPrefix(string infix)    //Function convert infix to prefix
    {
        CharStack operators = new CharStack(infix.Length);  //Create a stock of character type to store operators 
        string reverse = Reverse(infix);    //reverse string
        string output = ""; //Output string

        for (int i = 0; i < reverse.Length; i++)    //Scroll character by character of string
        {
            if (cheked(reverse[i]) == "operand")    //If it was an operand, it will be added to the output string
            {
                output += reverse[i];
            }
            else if (cheked(reverse[i]) == "operator")  //If we have a visual operator, we have 3 modes
            {
                if (reverse[i] == '(') //If we see '(' in the string before seeing ')', any operator was in the stack, we pop it from the stack and add it to the output string.     
                {
                    while (operators.Last() != ')')
                    {
                        output += operators.Pop();
                        if (operators.Last() == ')')
                        {
                            operators.Pop();
                            break;
                        }
                    }
                }
                else if (priority(reverse[i], true) <= priority(operators.Last(), false))   //If the priority of the operator in the string is higher or equal to the priority of the operator in the stack, the operator is pushed to the stack
                {
                    operators.Push(reverse[i]);
                }
                else    //otherwise (the priority of the stack operator is higher than the string)
                {
                    //until it reaches an operator with a lower priority in the stack, it pop the operators and adds them to the output
                    while (priority(reverse[i], true) > priority(operators.Last(), false))
                    {
                        output += operators.Pop();
                    }
                    operators.Push(reverse[i]); //Finally, it push the operator to the stack
                }
            }
            else if (reverse[i] == ' ') //If there was a space character, continue
            {
                continue;
            }
            else    //Otherwise, return an error in all these cases
            {
                return "Invalid Value!";
            }
        }
        while (!operators.Empty())  //At the end, we take the remaining operators in the stack until the stack is empty and add them to the output string
        {
            output += operators.Pop();
        }
        return Reverse(output); //Finally, we reverse the output string so that the expression is prefixed
    }
    static public string InfixToPostfix(string infix)   //Function convert infix to postfix
    {
        CharStack operators = new CharStack(infix.Length);  //Create a stock of character type to store operators
        string output = ""; //Output string

        for (int i = 0; i < infix.Length ; i++) //Scroll character by character of string
        {
            if (cheked(infix[i]) == "operand")  //If it was an operand, it will be added to the output string
            {
                output += infix[i];
            }
            else if (cheked(infix[i]) == "operator")    //If we have a visual operator, we have 3 modes
            {
                if (infix[i] == ')')//If we see ')' in the string before seeing '(', any operator was in the stack, we pop it from the stack and add it to the output string.
                {
                    while (operators.Last() != '(')
                    {
                        output += operators.Pop();
                        if (operators.Last() == '(')
                        {
                            operators.Pop();
                            break;
                        }
                    }
                }
                else if (priority(infix[i],true) < priority(operators.Last(),false))    //If the priority of the operator in the string is higher to the priority of the operator in the stack, the operator is pushed to the stack
                {
                    operators.Push(infix[i]);
                }
                else   //Otherwise (if the priority of the stack operator is higher than or equal to the string operator)
                {
                    // Pops operators on the stack until it reaches an operator with less or equal priority and push them to the output.
                    while (priority(infix[i], true) >= priority(operators.Last(), false))
                    {
                        output += operators.Pop();
                        if (priority(infix[i], true) == priority(operators.Last(), false))
                            break;
                    }
                    operators.Push(infix[i]);   //Finally, it push the operator to the stack
                }
            }
            else if (infix[i] == ' ')   //If there was a space character, continue
            {
                continue;
            }
            else    //Otherwise, return an error in all these cases
            {
                return "Invalid Value!";
            }
        }
        while (!operators.Empty())  //At the end, we take the remaining operators in the stack until the stack is empty and add them to the output string
        {
            output += operators.Pop();
        }
        return output;  //At the end, the output string contains our postfix phrase
    }

    static void Main(string[] args)
    {
        Console.WriteLine("1- Prefix  -->  Infix and Postfix");
        Console.WriteLine("2- Infix   -->  Prefix and Postfix");
        Console.WriteLine("3- Postfix -->  Prefix and Infix");
        Console.Write("Enter Command Number: ");
        int num = Convert.ToInt32(Console.ReadLine());
        Console.Write("\nEnter a phrase:");
        string ph = Console.ReadLine();
        switch (num)
        {
            case 1:
                Console.WriteLine("Infix: " + PrefixToInfix(ph));
                Console.WriteLine("Postfix: " + PrefixToPostfix(ph));
                break;
            case 2:
                Console.WriteLine("Postfix: " + InfixToPostfix(ph));
                Console.WriteLine("Prefix: " + InfixToPrefix(ph));
                break;
            case 3:
                Console.WriteLine("Prefix: " + PostfixToPrefix(ph));
                Console.WriteLine("Infix: " + PostfixToInfix(ph));
                break;
            default:
                Console.WriteLine("Invalid Value!");
                break;
        }


    }
}