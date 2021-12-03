using ConsoleCalculator.Services.Abstractions;
using StringCalculator.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalculator.Services
{
    public class ConsoleCalculateService : IConsoleCalculateService
    {
        private readonly IConsoleService _consoleService;
        private readonly IStringCalculatorService _stringCalculatorService;
        public ConsoleCalculateService(IStringCalculatorService stringCalculatorService, IConsoleService consoleService)
        {
            _stringCalculatorService = stringCalculatorService;
            _consoleService = consoleService;
        }

        public void Start()
        {
            _consoleService.WriteLine("Enter comma separated numbers (enter to exit):");
            var data = _consoleService.ReadLine();
            var sum = _stringCalculatorService.Sum(data);
            _consoleService.WriteLine($"Result is: {sum}");
            TryCalculateAgain();
        }

        private void TryCalculateAgain()
        {
            _consoleService.WriteLine("you can enter other numbers (enter to exit)?");
            var data = _consoleService.ReadLine();
            if (!string.IsNullOrEmpty(data))
            {
                var sum = _stringCalculatorService.Sum(data);
                _consoleService.WriteLine($"Result is: {sum}");
                TryCalculateAgain();
            }
        }
    }
}
