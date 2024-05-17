using Calculator.Core.Interfaces;
using Calculator.IO.Interfaces;

namespace Calculator.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        public Engine(IReader _reader, IWriter _writer)
        {
            reader = _reader;
            writer = _writer;
        }

        public void Run()
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


            string expression = reader.ReadLine();
            string[] tokens = GetCharArrayFromString(expression);


            Calculate(tokens, operatorsStack, numbers);
            MoveOperatorsToQueue(operatorsStack, numbers);


            //int result = CalculateResult(numbers);


            writer.WriteLine(string.Join(" ", numbers));



            reader.ReadLine();
        }


        private static int CalculateResult(Queue<string> numbers)
        {
            Stack<int> results = new Stack<int>();

            while(numbers.Count > 0)
            {
                string currentItem = numbers.Dequeue();
                bool hasParsed = int.TryParse(currentItem, out int currentNumber);

                if(hasParsed)
                {
                    results.Push(currentNumber);
                    continue;
                }

                int number1 = int.Parse(numbers.Dequeue());
                int number2 = int.Parse(numbers.Dequeue());

                int result = Evaluator.Evaluate(number2, number1, currentItem);

                results.Push(result);
            }

            int finalResult = results.Pop();
            return finalResult;
        }

        private static void MoveOperatorsToQueue(Stack<string> operatorsStack, Queue<string> numbers)
        {
            string operat = operatorsStack.Pop();

            while (operatorsStack.Count > 0)
            {
                numbers.Enqueue(operat);
                operat = operatorsStack.Pop();
            }

            numbers.Enqueue(operat);
        }

        private static string[] GetCharArrayFromString(string text)
        {


            List<string> tokens = new List<string>();

            string currentToken = "";

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] >= '0' && text[i] <= '9')
                {
                    currentToken += text[i];
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

            if (currentToken != string.Empty)
            {
                tokens.Add(currentToken);
            }

            return tokens.ToArray();
        }

        private static void Calculate(string[] tokens, Stack<string> operators, Queue<string> numbers)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                var currentToken = tokens[i];

                if (int.TryParse(currentToken, out int result) == true)
                {
                    numbers.Enqueue(currentToken);
                }

                else if (currentToken == "+" || currentToken == "-" || currentToken == "*" || currentToken == "/")
                {
                    if (CheckForPrecedence(operators, currentToken) == false)
                    {
                        OrderOperators(operators, numbers, currentToken);
                    }

                    operators.Push(currentToken);
                }
                else if (currentToken == "(")
                {
                    operators.Push(currentToken);
                }
                else if (currentToken == ")")
                {
                    OrderBrackets(operators, numbers, currentToken);
                }
            }
        }

        private static void OrderBrackets(Stack<string> operators, Queue<string> numbers, string currentToken)
        {
            string currentOperator = operators.Peek();

            while (currentOperator != "(")
            {
                numbers.Enqueue(currentOperator);

                if (operators.Count > 0)
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

        private static void OrderOperators(Stack<string> operators, Queue<string> symbols, string currentToken)
        {

            string currentOperator = operators.Peek();

            while (currentOperator != "+" && currentOperator != "-")
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

        private static bool CheckForPrecedence(Stack<string> operators, string currentOperator)
        {
            if (operators.Count == 0)
            {
                return true;
            }

            var stackOperator = operators.Peek();

            if (currentOperator == "+" || currentOperator == "-")
            {
                if (stackOperator == "*" || stackOperator == "/")
                {
                    return false;
                }

            }
            else if (currentOperator == "*" || currentOperator == "/")
            {
                if (stackOperator == "+" || stackOperator == "-")
                {
                    return true;
                }
            }

            return true;
        }
    }
}
