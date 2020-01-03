using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDKataStringCalculator.Code
{
    public class Logger : ILogger
    {
        public void Write(int numberSum)
        {
            Console.Write(numberSum);
        }
    }
}
