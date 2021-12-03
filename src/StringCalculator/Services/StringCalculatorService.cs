﻿using System;
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
        private readonly string[] _defaultDelimiters = { ",", "\n" };
        private readonly int _biggerNumber = 1000;
        private string _customDelimiter;

        public int Sum(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return 0;
            }

            if (IsCustomDelimiter(data))
            {
                _customDelimiter = GetCustomDelimiter(data);                
                return GetSum(GetNumbers(CustomDataClear(data), _customDelimiter));
            }

            return GetSum(GetNumbers(data, _defaultDelimiters));
            
        }

        private bool IsCustomDelimiter(string data)
        {
            return data.Contains(_customDelimiterStartMarker) && data.Contains(_customDelimiterEndMarker);
        }

        private string GetCustomDelimiter(string data)
        {
            var separatedData = data.Split(_customDelimiterEndMarker);
            return separatedData[0].Replace(_customDelimiterStartMarker, string.Empty);
        }

        private IReadOnlyCollection<string> GetNumbers(string data, IReadOnlyCollection<string> delimiters)
        {
            return data.Split(delimiters.ToArray<string>(), StringSplitOptions.None);            
        }
        private IReadOnlyCollection<string> GetNumbers(string data, string delimiter)
        {
            return data.Split(delimiter);
        }
        private string CustomDataClear(string data)
        {
            return data.Replace($"{_customDelimiterStartMarker}{_customDelimiter}{_customDelimiterEndMarker}", "");
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
                convertedNumbers.Add(int.Parse(i));
            }
            return convertedNumbers;
        }

        private IReadOnlyCollection<int> GetNegativeNumbers(IReadOnlyCollection<int> numbers)
        {
            var negativeNumbers = new List<int>();
            foreach(var number in numbers)
            {
                if (number < 0)
                {
                    negativeNumbers.Add(number);
                }
            }
            return negativeNumbers;
        }
        
        private IReadOnlyCollection<int> DeleteBiggerNumbers(IReadOnlyCollection<int> numbers)
        {
            return numbers.Where(x => x < _biggerNumber).ToList();
        }
    }
}
