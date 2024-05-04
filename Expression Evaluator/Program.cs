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

            Stack<char> operatorsStack = new Stack<char>();
            Queue<char> numbers = new Queue<char>();


            string expression = Console.ReadLine();
            char[] tokens = GetCharArrayFromString(expression);


            Calculate(tokens, operatorsStack, numbers);
            MoveOperatorsToQueue(operatorsStack, numbers);

            Console.WriteLine(string.Join(" ", numbers));

            Console.ReadLine();

        }

        static void MoveOperatorsToQueue(Stack<char> operatorsStack, Queue<char> numbers)
        {
            char operat = operatorsStack.Pop();

            while(operatorsStack.Count > 0)
            {
                numbers.Enqueue(operat);
                operat = operatorsStack.Pop();
            }

            numbers.Enqueue(operat);
        }

        static char[] GetCharArrayFromString(string text)
        {
            text = text.Replace(" ", "");

            char[] tokens = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                tokens[i] = text[i];
            }

            return tokens;
        }


        static void Calculate(char[] tokens, Stack<char> operators, Queue<char> numbers)
        {
            for(int i = 0; i < tokens.Length; i++)
            {
                var currentToken = tokens[i];
                var characterCode = (int)currentToken;

                if(currentToken >= '0' && currentToken <= '9')
                {
                    numbers.Enqueue(currentToken);
                }

                else if(currentToken == '+' || currentToken == '-' || currentToken == '*' || currentToken == '/')
                {
                    if(CheckForPrecedence(operators, currentToken) == false)
                    {
                        OrderOperators(operators, numbers, currentToken);
                    }

                    operators.Push(currentToken);
                }
                else if(currentToken == '(')
                {
                    operators.Push(currentToken);
                }
                else if(currentToken == ')')
                {
                    OrderBrackets(operators, numbers, currentToken);
                }
            }
        }

        static void OrderBrackets(Stack<char> operators, Queue<char> numbers, char currentToken)
        {
            char currentOperator = operators.Peek();

            while(currentOperator != '(')
            {
                numbers.Enqueue(currentOperator);

                if(operators.Count > 0) 
                {
                    operators.Pop();
                    currentOperator = operators.Peek();
                }
                else
                {
                    currentOperator = '(';
                }
            }


            operators.Pop();

        }

        static void OrderOperators(Stack<char> operators, Queue<char> symbols, char currentToken)
        {

            char currentOperator = operators.Peek();

            while(currentOperator != '+' && currentOperator != '-')
            {
                symbols.Enqueue(currentOperator);

                if (operators.Count > 0)
                {
                    operators.Pop();
                    currentOperator = operators.Peek();
                }
                else
                {
                    currentOperator = '+';
                }
            }

            operators.Push(currentToken);
        }

        static bool CheckForPrecedence(Stack<char> operators, char currentOperator)
        {
            if(operators.Count == 0)
            {
                return true;
            }

            var stackOperator = operators.Peek();

            if(currentOperator == '+' || currentOperator == '-')
            {
                if(stackOperator == '*' || stackOperator == '/')
                {
                    return false;
                }
             
            }
            else if(currentOperator == '*' || currentOperator == '/')
            {
                if(stackOperator == '+' || stackOperator == '-')
                {
                    return true;
                }
            }

            return true;
        }
    }
}
