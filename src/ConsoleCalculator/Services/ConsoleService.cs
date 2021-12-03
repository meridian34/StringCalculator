using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringCalculator.Services;

namespace ConsoleCalculator.Services
{
    public class ConsoleService
    {
        private readonly StringCalculatorService _calculator = new StringCalculatorService();

        public void Start()
        {
            Console.WriteLine("Enter comma separated numbers (enter to exit):");
            var sum = _calculator.Sum(Console.ReadLine());
            Console.WriteLine($"Result is: {sum}");
            TryCalculateAgain();
        }

        private void TryCalculateAgain()
        {   
            Console.WriteLine("you can enter other numbers (enter to exit)?");
            var result = Console.ReadLine();
            if (!string.IsNullOrEmpty(result))
            {
                var sum = _calculator.Sum(result);
                Console.WriteLine($"Result is: {sum}");
                TryCalculateAgain();
            }
        }
    }
}
