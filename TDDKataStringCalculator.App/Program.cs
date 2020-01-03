using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDDKataStringCalculator.Code;

namespace TDDKataStringCalculator.App
{
    class Program
    {
        static void Main(string[] args)
        {

            var _stringCalculator = new StringCalculator(new Logger(),new Webservice());
            _stringCalculator.Add("12,3");
        }
    }
}
