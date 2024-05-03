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


            Console.WriteLine(string.Join(" ", tokens));

            Console.ReadLine();

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
                    CheckForPrecedence(operators, currentToken);

                    operators.Push(currentToken);
                }
                else if(currentToken == '(')
                {
                    operators.Push(currentToken);
                }
            }
        }


        static bool CheckForPrecedence(Stack<char> operators, char currentOperator)
        {
            var stackOperator = operators.Peek();

            if(currentOperator == '+')
            {

            }


            return true;

        }
    }
}
