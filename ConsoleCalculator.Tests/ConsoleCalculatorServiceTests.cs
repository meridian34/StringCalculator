using System;
using Moq;
using Xunit;
using StringCalculator.Services;
using ConsoleCalculator.Services;

namespace ConsoleCalculator.Tests
{
    public class ConsoleCalculatorServiceTests
    {
        private readonly Mock<ConsoleService> _consoleService;
        private readonly Mock<StringCalculatorService> _stringCalculatorService;
        private Mock<ConsoleCalculateService> _consoleCalculateService;

        public ConsoleCalculatorServiceTests()
        {
            
            _consoleService = new Mock<ConsoleService>();
            _stringCalculatorService = new Mock<StringCalculatorService>();
        }

        [Fact]
        public void Start_InvokeMethod_ShouldConsoleWriteDefaultData()
        {
            //arrange
            
            _consoleCalculateService = new Mock<ConsoleCalculateService>(_stringCalculatorService.Object, _consoleService.Object) { CallBase = true };

            //act
            _consoleCalculateService.Object.Start();

            //assert
            _consoleService.Verify(x => x.WriteLine(It.Is<string>(s => s == "Enter comma separated numbers (enter to exit):")));
        }

        [Fact]
        public void Start_InvokeMethod_ShouldPrintSum()
        {
            //arrange
            _consoleService.SetupSequence(x => x.ReadLine())
                .Returns("1,2");
            _stringCalculatorService.SetupSequence(x => x.Sum(It.Is<string>(s => s == "1,2")))
                .Returns(3);
            _consoleCalculateService = new Mock<ConsoleCalculateService>(_stringCalculatorService.Object, _consoleService.Object) { CallBase = true };

            //act
            _consoleCalculateService.Object.Start();

            //assert
            _consoleService.Verify(x => x.WriteLine(It.Is<string>(s => s == "Result is: 3")) );
        }

        [Fact]
        public void Start_InvokeMethodAndInputCalculateData_ShouldPrintQuestionAfrerPrintSum()
        {
            //arrange
            _consoleService.SetupSequence(x => x.ReadLine())
                .Returns("1,2");

            _stringCalculatorService.SetupSequence(x => x.Sum(It.Is<string>(s => s == "1,2")))
                .Returns(3);

            _consoleCalculateService = new Mock<ConsoleCalculateService>(_stringCalculatorService.Object, _consoleService.Object) { CallBase = true };

            //act
            _consoleCalculateService.Object.Start();

            //assert
            _consoleService.Verify(x => x.WriteLine(It.Is<string>(s => s == "Result is: 3")));
            _consoleService.Verify(x => x.WriteLine(It.Is<string>(s => s == "Result is: 9")));
        }

        [Fact]
        public void Start_InvokeMethodAndInputCalculateDataTwice_ShouldWithdrawSumTwice()
        {
            //arrange
            _consoleService.SetupSequence(x => x.ReadLine())
                .Returns("1,2")
                .Returns("4,5")
                .Returns("");

            _stringCalculatorService.SetupSequence(x => x.Sum(It.Is<string>(s => s == "1,2")))
                .Returns(3);
            _stringCalculatorService.SetupSequence(x => x.Sum(It.Is<string>(s => s == "4,5")))
                .Returns(9);

            _consoleCalculateService = new Mock<ConsoleCalculateService>(_stringCalculatorService.Object, _consoleService.Object) { CallBase = true };

            //act
            _consoleCalculateService.Object.Start();

            //assert
            _consoleService.Verify(x => x.WriteLine(It.Is<string>(s=> s == "Result is: 3")));
            _consoleService.Verify(x => x.WriteLine(It.Is<string>(s=> s == "Result is: 9")));
        }

        [Fact]
        public void Start_InvokeMethod_ShouldArgumentException()
        {
            //arrange
            var expectedMessage = "negatives not allowed: -3 -5";

            _consoleService.SetupSequence(x => x.ReadLine())
                .Returns("-4,-5");
            _stringCalculatorService.Setup(x => x.Sum(It.Is<string>(s => s == "-4,-5"))).Throws(new ArgumentException(expectedMessage));
            _consoleCalculateService = new Mock<ConsoleCalculateService>(_stringCalculatorService.Object, _consoleService.Object) { CallBase = true };

            //act
            var exception = Assert.Throws<ArgumentException>(() => _consoleCalculateService.Object.Start());

            //assert
            Assert.Equal(expectedMessage, exception.Message);
        }

        

        //[Fact]
        //public void Start_InvokeMethod_ShouldVerified()
        //{
        //    //arrange
        //    _consoleService.SetupSequence(x => x.ReadLine())
        //        .Returns("1,2")
        //        .Returns("4,5")
        //        .Returns("");

        //    _stringCalculatorService.SetupSequence(x => x.Sum(It.Is<string>(s => s == "1,2" || s == "4,5")))
        //        .Returns(3)
        //        .Returns(9);

        //    _consoleCalculateService = new ConsoleCalculateService(_stringCalculatorService.Object, _consoleService.Object);

        //    //act
        //    _consoleCalculateService.Start();

        //    //assert
        //    _consoleService.Verify(x => x.ReadLine(), Times.AtLeast(3));
        //    _stringCalculatorService.Verify(x => x.Sum(It.Is<string>(s => s == "1,2" || s == "4,5")), Times.AtLeast(2));
        //    _consoleService.Verify(x => x.WriteLine(It.Is<string>(s => s == "3" ||
        //                                                                s == "9" ||
        //                                                                s == "Enter comma separated numbers (enter to exit):" ||
        //                                                                s == "Result is: 3" ||
        //                                                                s == "Result is: 9" ||
        //                                                                s == "you can enter other numbers (enter to exit)?")), Times.AtLeast(5));
        //}
    }
}
