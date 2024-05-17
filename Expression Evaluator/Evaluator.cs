namespace Calculator
{
    public static class Evaluator
    {
        public static int Evaluate(int x, int y, string op)
        {
            switch(op)
            {
                case  "+":
                   return x + y;

                case "-":
                    return x - y;

                case "*":
                    return x * y;

                case "/":
                    return x / y;
            }

            return 0;
        }
    }
}
