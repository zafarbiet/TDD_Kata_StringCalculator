using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDDKataStringCalculator.Code;

namespace TDDKataStringCalculator.Tests
{
    [TestFixture]
    public class StringCalculatorTests
    {
        [TestCase(0, "")]
        [TestCase(1, "1")]
        [TestCase(3, "1,2")]
        [TestCase(15, "1,2,3,4,5")]
        [TestCase(6, "1\n2,3")]
        [TestCase(3, "//;\n1;2")]
        [TestCase(2, "1001,2")]
        [TestCase(6, "//***\n1***2***3")]
        [TestCase(6, "//*%\n1*2%3")]
        public void Test_StringAdd_ReturnsNumbers(int expected, string number)
        {
            var calculator = new StringCalculator();
            Assert.AreEqual(expected, calculator.Add(number));
        }
        [Test]
        public void TestStringAdd_ReturnsError()
        {
            var number = "//;\n-1;-2";
            var calculator = new StringCalculator();
            var exception = Assert.Throws<Exception>(() => calculator.Add(number));
            Assert.AreEqual("negatives not allowed -1 -2", exception.Message);
        }
    }
}
