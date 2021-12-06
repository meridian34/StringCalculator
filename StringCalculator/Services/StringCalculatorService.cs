using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator.Services
{
    public class StringCalculatorService
    {
        private const string _startCustomDelimiterDataMarker = "//";
        private const string _endCustomDelimiterDataMarker = "\n";

        public virtual int Sum(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return 0;
            }

            var delimiters = GetDelimiters(data);
            var numbers = GetNumbers(data, delimiters);
            var filtredNumbers = FilterNumbers(numbers);

            return filtredNumbers

                .Sum();
        }

        private IReadOnlyCollection<string> GetDelimiters(string data)
        {
            var defaultDelimiters = new string[] { ",", "\n" };
            var longDelimiterStartMarker = "[";
            var longDelimiterEndMarker = "]";
            var result = new List<string>();
           
            if (IsContainsCustomDelimiter(data))
            {
                var separatedData = data.Split(_endCustomDelimiterDataMarker);
                var delimiterBody = separatedData[0].Replace(_startCustomDelimiterDataMarker, string.Empty);
                var isLongCustomerDelimiter = delimiterBody.Contains(longDelimiterStartMarker) && delimiterBody.Contains(longDelimiterEndMarker);

                if (isLongCustomerDelimiter)
                {
                    var separator = new string[] { longDelimiterStartMarker, longDelimiterEndMarker };
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

            return defaultDelimiters;
        }

        private IReadOnlyCollection<int> GetNumbers(string data, IReadOnlyCollection<string> delimiters)
        {
            if (IsContainsCustomDelimiter(data))
            {
                data = data.Split(_endCustomDelimiterDataMarker)[1];
            }
            
            var splitData = data.Split(delimiters.ToArray<string>(), StringSplitOptions.None);
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

        private bool IsContainsCustomDelimiter(string data)
        {
            return data.Contains(_endCustomDelimiterDataMarker) && data.Contains(_startCustomDelimiterDataMarker);
        }


        private IReadOnlyCollection<int> FilterNumbers(IReadOnlyCollection<int> numbers)
        {
            var biggerNumber = 1000;
            var negativeNumbers = numbers.Where(x => x < 0).ToList();

            if (negativeNumbers.Count > 0)
            {
                throw new ArgumentException($"negatives not allowed: {string.Join(" ", negativeNumbers)}");
            }

            return numbers.Where(x => x < biggerNumber).ToList();
        }
    }
}
