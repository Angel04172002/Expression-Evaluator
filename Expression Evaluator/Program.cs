using Calculator.Core;
using Calculator.Core.Interfaces;
using Calculator.IO;
using Calculator.IO.Interfaces;

namespace Calculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IEngine engine = new Engine(reader, writer);
            engine.Run();
        }
    }
}
