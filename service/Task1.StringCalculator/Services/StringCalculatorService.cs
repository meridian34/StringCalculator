using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.StringCalculator.Services
{
    public class StringCalculatorService
    {
        public int Sum(string numbers)
        {
            if (string.IsNullOrWhiteSpace(numbers))
            {
                return 0;
            }
            var separatedSymbols = numbers.Split(',');
            var sum = 0;
            try
            {
                foreach (var i in separatedSymbols)
                {
                    sum += int.Parse(i);
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return sum;
        }
    }
}
