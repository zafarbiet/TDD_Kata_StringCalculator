using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDKataStringCalculator.Tests
{
    public class ConsoleOutput : IDisposable
    {
        private StringWriter _stringWriter;
        private TextWriter _originalOutput;
        private TextReader _originalInput;
        public void Dispose()
        {
            if (_originalOutput != null)
                Console.SetOut(_originalOutput);
            else
                Console.SetIn(_originalInput);
            _stringWriter.Dispose();
        }
        public ConsoleOutput()
        {
            _stringWriter = new StringWriter();
            _originalOutput = Console.Out;
            Console.SetOut(_stringWriter);

        }
        public ConsoleOutput(StringBuilder message)
        {
            _stringWriter = new StringWriter();
           
        }
        public string GetOutput()
        {
            return _stringWriter.ToString();
        }

        public void GetInput(string message)
        {
            //_originalInput = Console.In;
            Console.SetIn(new StringReader(message));
        }
    }
}
