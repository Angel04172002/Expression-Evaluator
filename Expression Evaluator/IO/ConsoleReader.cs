using Calculator.IO.Interfaces;

namespace Calculator.IO
{
    public class ConsoleReader : IReader
    {
      
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
