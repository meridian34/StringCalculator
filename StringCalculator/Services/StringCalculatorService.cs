using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator.Services
{
    public class StringCalculatorService
    {
        private const string _startCustomDelimiterDataMarker = "//";
        private const string _endCustomDelimiterDataMarker = "\\n";

        public virtual int Sum(string data)
        {
            var biggerNumber = 1000;

            if (string.IsNullOrWhiteSpace(data))
            {
                return 0;
            }

            var delimiters = GetDelimiters(data);
            var numbersDataBlock = GetNumbersData(data);
            var numbers = GetNumbers(numbersDataBlock, delimiters);

            Validate(numbers);

            return numbers.Where(x => x < biggerNumber).Sum();
        }

        private string GetNumbersData(string data)
        {
            var isCustomBlock = data.Contains(_startCustomDelimiterDataMarker);
            if (isCustomBlock)
            {
                var numbersDataBlock = data.Split(_endCustomDelimiterDataMarker)[1];

                return numbersDataBlock;
            }

            return data;
        }

        private IReadOnlyCollection<string> GetDelimiters(string data)
        {
            var defaultDelimiters = new string[] { ",", "\n" };
            var longDelimiterStartMarker = "[";
            var longDelimiterEndMarker = "]";
            var result = new List<string>();

            var isCustomDelimiterBlock = data.Contains(_endCustomDelimiterDataMarker) && data.Contains(_startCustomDelimiterDataMarker);
            if (isCustomDelimiterBlock)
            {
                var separatedData = data.Split(_endCustomDelimiterDataMarker);
                var delimiterBody = separatedData[0].Replace(_startCustomDelimiterDataMarker, string.Empty);
                var isLongCustomerDelimiter = delimiterBody.Contains(longDelimiterStartMarker) && delimiterBody.Contains(longDelimiterEndMarker);

                if (isLongCustomerDelimiter)
                {
                    var tempDelimiterBody = delimiterBody.Remove(0, 1);
                    tempDelimiterBody = tempDelimiterBody.Remove(tempDelimiterBody.Length - 1, 1);
                    var separator = $"{longDelimiterEndMarker}{longDelimiterStartMarker}";
                    var splitDelimiters = tempDelimiterBody.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    result.AddRange(splitDelimiters);

                    return result;
                }
                else
                {
                    result.Add(delimiterBody);

                    return result;
                }
            }

            return defaultDelimiters;
        }

        

        private IReadOnlyCollection<int> GetNumbers(string dataBlock, IReadOnlyCollection<string> delimiters)
        {
            var splitData = dataBlock.Split(delimiters.ToArray<string>(), StringSplitOptions.None);
            var numbers = new List<int>(splitData.Length);

            foreach (var i in splitData)
            {
                var isConverted = int.TryParse(i, out int convertedNum);
                if (isConverted)
                {
                    numbers.Add(convertedNum);
                }
            }

            return numbers;
        }

        private void Validate(IReadOnlyCollection<int> numbers)
        {
            var isNegativeNumbers = numbers.Any(x => x < 0);

            if (isNegativeNumbers)
            {
                var negativeNumbers = numbers.Where(x => x < 0);

                throw new ArgumentException($"negatives not allowed: {string.Join(" ", negativeNumbers)}");
            }
        }
    }
}
