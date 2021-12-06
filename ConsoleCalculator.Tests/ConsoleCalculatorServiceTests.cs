using ConsoleCalculator.Services;
using ConsoleCalculator.Services.Abstractions;
using Moq;
using StringCalculator.Services.Abstractions;
using System;
using Xunit;

namespace ConsoleCalculator.Tests
{
    public class ConsoleCalculatorServiceTests
    {
        private readonly Mock<IConsoleService> _consoleService;
        private readonly Mock<IStringCalculatorService> _stringCalculatorService;
        private IConsoleCalculateService _consoleCalculateService;

        public ConsoleCalculatorServiceTests()
        {
            
            _consoleService = new Mock<IConsoleService>();
            _stringCalculatorService = new Mock<IStringCalculatorService>();
        }

        [Fact]
        public void Start_InvokeMethod_ShouldVerified()
        {
            //arrange
            _consoleService.SetupSequence(x => x.ReadLine())
                .Returns("1,2")
                .Returns("4,5")
                .Returns("");

            _stringCalculatorService.SetupSequence(x => x.Sum(It.Is<string>(s => s == "1,2" || s == "4,5")))
                .Returns(3)
                .Returns(9);

            _consoleCalculateService = new ConsoleCalculateService(_stringCalculatorService.Object, _consoleService.Object);

            //act
            _consoleCalculateService.Start();

            //assert
            _consoleService.Verify(x => x.ReadLine(), Times.AtLeast(3));
            _stringCalculatorService.Verify(x => x.Sum(It.Is<string>(s => s == "1,2" || s == "4,5")), Times.AtLeast(2));
            _consoleService.Verify(x => x.WriteLine(It.Is<string>(s => s == "3" ||
                                                                        s == "9" ||
                                                                        s == "Enter comma separated numbers (enter to exit):" ||
                                                                        s == "Result is: 3" ||
                                                                        s == "Result is: 9" ||
                                                                        s == "you can enter other numbers (enter to exit)?")), Times.AtLeast(5));
        }
    }
}
