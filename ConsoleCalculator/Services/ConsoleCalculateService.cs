using StringCalculator.Services;

namespace ConsoleCalculator.Services
{
    public class ConsoleCalculateService
    {
        private readonly ConsoleService _consoleService;
        private readonly StringCalculatorService _stringCalculatorService;

        public ConsoleCalculateService(StringCalculatorService stringCalculatorService, ConsoleService consoleService)
        {
            _stringCalculatorService = stringCalculatorService;
            _consoleService = consoleService;
        }

        public virtual void Start()
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
