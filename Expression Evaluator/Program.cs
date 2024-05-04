using System.Linq.Expressions;

namespace Calculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var operators = new[]
            {
                '+',
                '-',
                '*',
                '/'
            };

            Stack<string> operatorsStack = new Stack<string>();
            Queue<string> numbers = new Queue<string>();


            string expression = Console.ReadLine();
            string[] tokens = GetCharArrayFromString(expression);


            Calculate(tokens, operatorsStack, numbers);
            MoveOperatorsToQueue(operatorsStack, numbers);

            Console.WriteLine(string.Join(" ", numbers));

            Console.ReadLine();

        }

        static void MoveOperatorsToQueue(Stack<string> operatorsStack, Queue<string> numbers)
        {
            string operat = operatorsStack.Pop();

            while(operatorsStack.Count > 0)
            {
                numbers.Enqueue(operat);
                operat = operatorsStack.Pop();
            }

            numbers.Enqueue(operat);
        }

        static string[] GetCharArrayFromString(string text)
        {


            List<string> tokens = new List<string>();

            string currentToken = "";

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] >= '0' && text[i] <= '9')
                {
                    currentToken += text[i];
                    continue;
                }
               
                else if (text[i] == '+' || text[i] == '-' || text[i] == '*' || text[i] == '/')
                {
                    tokens.Add(currentToken);
                    currentToken = "";
                    tokens.Add(text[i].ToString());
                    continue;
                }
                else
                {
                    if (currentToken != string.Empty)
                    {
                        tokens.Add(currentToken);
                        currentToken = "";
                    }

                    tokens.Add(text[i].ToString());
                }
      
            }

            return tokens.ToArray();
        }

        static void Calculate(string[] tokens, Stack<string> operators, Queue<string> numbers)
        {
            for(int i = 0; i < tokens.Length; i++)
            {
                var currentToken = tokens[i];

                if(int.TryParse(currentToken, out int result) == true)
                {
                    numbers.Enqueue(currentToken);
                }

                else if(currentToken == "+" || currentToken == "-" || currentToken == "*" || currentToken == "/")
                {
                    if(CheckForPrecedence(operators, currentToken) == false)
                    {
                        OrderOperators(operators, numbers, currentToken);
                    }

                    operators.Push(currentToken);
                }
                else if(currentToken == "(")
                {
                    operators.Push(currentToken);
                }
                else if(currentToken == ")")
                {
                    OrderBrackets(operators, numbers, currentToken);
                }
            }
        }

        static void OrderBrackets(Stack<string> operators, Queue<string> numbers, string currentToken)
        {
            string currentOperator = operators.Peek();

            while(currentOperator != "(")
            {
                numbers.Enqueue(currentOperator);

                if(operators.Count > 0) 
                {
                    operators.Pop();
                    currentOperator = operators.Peek();
                }
                else
                {
                    currentOperator = "(";
                }
            }


            operators.Pop();

        }

        static void OrderOperators(Stack<string> operators, Queue<string> symbols, string currentToken)
        {

            string currentOperator = operators.Peek();

            while(currentOperator != "+" && currentOperator != "-")
            {
                symbols.Enqueue(currentOperator);

                if (operators.Count > 0)
                {
                    operators.Pop();
                    currentOperator = operators.Peek();
                }
                else
                {
                    currentOperator = "+";
                }
            }

            operators.Push(currentToken);
        }

        static bool CheckForPrecedence(Stack<string> operators, string currentOperator)
        {
            if(operators.Count == 0)
            {
                return true;
            }

            var stackOperator = operators.Peek();

            if(currentOperator == "+" || currentOperator == "-")
            {
                if(stackOperator == "*" || stackOperator == "/")
                {
                    return false;
                }
             
            }
            else if(currentOperator == "*" || currentOperator == "/")
            {
                if(stackOperator == "+" || stackOperator == "-")
                {
                    return true;
                }
            }

            return true;
        }
    }
}
