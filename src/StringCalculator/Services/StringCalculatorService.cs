using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Services
{
    public class StringCalculatorService
    {
        private readonly string _customDelimiterStartMarker = "//";
        private readonly string _customDelimiterEndMarker = "\n";
        private readonly string _customLenghtStartMarker = "[";
        private readonly string _customLenghtEndMarker = "]";
        private readonly string[] _defaultDelimiters = { ",", "\n" };
        private readonly int _biggerNumber = 1000;
        private IReadOnlyCollection<string> _customDelimiters;

        public int Sum(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return 0;
            }

            if (IsCustomDelimiter(data))
            {
                _customDelimiters = GetCustomDelimiters(data);

                return GetSum(GetNumbers(ClearCustomDelimiterData(data), _customDelimiters));
            }

            return GetSum(GetNumbers(data, _defaultDelimiters));
        }

        private bool IsCustomDelimiter(string data)
        {
            return data.Contains(_customDelimiterStartMarker) && data.Contains(_customDelimiterEndMarker);
        }

        private IReadOnlyCollection<string> GetCustomDelimiters(string data)
        {
            var result = new List<string>();
            var separatedData = data.Split(_customDelimiterEndMarker);
            var delimiterBody = separatedData[0].Replace(_customDelimiterStartMarker, string.Empty);
            var isLongCustomerDelimiter = delimiterBody.Contains(_customLenghtStartMarker) && delimiterBody.Contains(_customLenghtEndMarker);
            if (isLongCustomerDelimiter)
            {
                var separator = new string[] { _customLenghtStartMarker, _customLenghtEndMarker };
                var splitDelimiters = delimiterBody.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                result.AddRange(splitDelimiters);

                return result;
            }
            else
            {
                result.Add(delimiterBody);

                return result;
            }
        }
        private string ClearCustomDelimiterData(string data)
        {
            var dataWithoutCustomDelimiterData = data.Split(_customDelimiterEndMarker)[1];

            return dataWithoutCustomDelimiterData;
        }

        private IReadOnlyCollection<string> GetNumbers(string data, IReadOnlyCollection<string> delimiters)
        {
            return data.Split(delimiters.ToArray<string>(), StringSplitOptions.None);            
        }

        private int GetSum(IReadOnlyCollection<string> numbers)
        {
            var convertedNumbers = ConvertNumbersToInt(numbers);
            var negativeNumbers = GetNegativeNumbers(convertedNumbers);
            if (negativeNumbers.Count > 0)
            {
                throw new ArgumentException($"negatives not allowed: {string.Join(" ", negativeNumbers)}");
            }

            convertedNumbers = DeleteBiggerNumbers(convertedNumbers);

            return convertedNumbers.Sum(); 
        }

        private IReadOnlyCollection<int> ConvertNumbersToInt(IReadOnlyCollection<string> numbers)
        {
            var convertedNumbers = new List<int>();
            foreach (var i in numbers)
            {
                var isConverted = int.TryParse(i, out int convertedNum);
                if (isConverted)
                {
                    convertedNumbers.Add(convertedNum);
                }
            }

            return convertedNumbers;
        }

        private IReadOnlyCollection<int> GetNegativeNumbers(IReadOnlyCollection<int> numbers)
        {
            return numbers.Where(x => x < 0).ToList();
        }
        
        private IReadOnlyCollection<int> DeleteBiggerNumbers(IReadOnlyCollection<int> numbers)
        {
            return numbers.Where(x => x < _biggerNumber).ToList();
        }
    }
}
