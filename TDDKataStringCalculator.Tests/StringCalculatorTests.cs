using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDDKataStringCalculator.Code;

namespace TDDKataStringCalculator.Tests
{
    [TestFixture]
    public class StringCalculatorTests
    {
        private ILogger _logger;
        private Mock<IWebservice> _webService;
        [SetUp]
        public void SetupTest()
        {
            _logger = new Logger();
            _webService = new Mock<IWebservice>();

        }
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
            var currentConsoleOut = Console.Out;
            var calculator = new StringCalculator(_logger, _webService.Object);
            using (var consoleOut = new ConsoleOutput())
            {
                var actualResult = calculator.Add(number);
                Assert.AreEqual(actualResult.ToString(), consoleOut.GetOutput());
                Assert.AreEqual(expected, actualResult);
                _webService.Verify(c => c.RecordExceptions(It.IsAny<string>()), Times.Never);
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }
        [Test]
        public void TestInput_StringAdd_ReturnsNumbers()
        {
            
            var calculator = new StringCalculator(_logger, _webService.Object);
            var message = new StringBuilder();
            message.Append("1,2");
            using (var consoleIn = new ConsoleOutput())
            {
                var currentConsoleIn = Console.In;
                consoleIn.GetInput(message.ToString());
                //var actualResult = calculator.Add(currentConsoleIn.ReadToEnd());
                //Assert.AreEqual(consoleIn.GetOutput(), actualResult.ToString());
                //Assert.AreEqual(expected, actualResult);
                _webService.Verify(c => c.RecordExceptions(It.IsAny<string>()), Times.Never);
            }

        }

        private TextReader GetNewInput()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void TestStringAdd_ReturnsError()
        {
            var number = "//;\n-1;-2";
            var calculator = new StringCalculator(_logger, _webService.Object);
            var exception = Assert.Throws<Exception>(() => calculator.Add(number));
            Assert.AreEqual("negatives not allowed -1 -2", exception.Message);
            _webService.Verify(c => c.RecordExceptions(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public void CallAddMethod_LogThrowsEx_LogToService()
        {
            var number = "//;\n1;2";
            var _customLogger = new Mock<ILogger>();
            _customLogger.Setup(c => c.Write(It.IsAny<int>())).Throws<Exception>();
            var calculator = new StringCalculator(_customLogger.Object, _webService.Object);
            var actual = Assert.Throws<Exception>(() => calculator.Add(number));
            _webService.Verify(c => c.RecordExceptions(It.IsAny<string>()));

        }
    }
}
