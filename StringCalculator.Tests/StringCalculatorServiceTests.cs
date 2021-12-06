using System;
using StringCalculator.Services;
using Xunit;

namespace StringCalculator.Tests
{
    public class StringCalculatorServiceTests
    {
        private readonly StringCalculatorService _service = new StringCalculatorService();

        [Fact]
        public void Sum_EmptyString_ShouldReturnZero()
        {
            //arrange
            var numbers = "";
            var expectedResult = 0;

            //act
            var result = _service.Sum(numbers);

            //assert
            Assert.Equal(result, expectedResult);
        }

        [Fact]
        public void Sum_OneNumber_ShouldReturnOneNumber()
        {
            //arrange
            var numbers = "1";
            var expectedResult = 1;

            //act
            var result = _service.Sum(numbers);

            //assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Sum_TwoNumbers_ShouldReturnSum()
        {
            //arrange
            var numbers = "2,3";
            var expectedResult = 5;

            //act
            var result = _service.Sum(numbers);

            //assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Sum_MultipleNumbers_ShouldReturnSum()
        {
            //arrange
            var numbers = "2,3,4,5";
            var expectedResult = 14;

            //act
            var result = _service.Sum(numbers);

            //assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Sum_MultipleNumbersWithDifferentDefaultDelimiters_ShouldReturnSum()
        {
            //arrange
            var numbers = "1\n2,3";
            var expectedResult = 6;

            //act
            var result = _service.Sum(numbers);

            //assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Sum_TwoNumbersWithCustomDelimiter_ShouldReturnSum()
        {
            //arrange
            var numbers = "//;\n1;2";
            var expectedResult = 3;

            //act
            var result = _service.Sum(numbers);

            //assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Sum_MultipleNumbersWithNegativeNumbers_ShouldReturnException()
        {
            //arrange
            var numbers = "//;\n1;2;-3;-5";
            var expectedMessage = "negatives not allowed: -3 -5";
            //act
            var exception = Assert.Throws<ArgumentException>(() => _service.Sum(numbers));

            //assert
            Assert.Equal(expectedMessage, exception.Message);
        }

        [Fact]
        public void Sum_MultipleNumbersWithBiggerNumber_ShouldReturnSumWithoutBiggerNumber()
        {
            //arrange
            var numbers = "//;\n1;2;1001;1";
            var expectedResult = 4;
            //act
            var result = _service.Sum(numbers);

            //assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Sum_MultipleNumbersWithLongDelimiter_ShouldReturnSum()
        {
            //arrange
            var numbers = "//[***]\n1***2***3";
            var expectedResult = 6;

            //act
            var result = _service.Sum(numbers);

            //assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Sum_MultipleNumbersWithMultipleDelimiter_ShouldReturnSum()
        {
            //arrange
            var numbers = "//[*][%]\n1*2%3";
            var expectedResult = 6;

            //act
            var result = _service.Sum(numbers);

            //assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Sum_MultipleNumbersWithMultipleLongDelimiter_ShouldReturnSum()
        {
            //arrange
            var numbers = "//[***][%%%]\n1***2%%%3";
            var expectedResult = 6;

            //act
            var result = _service.Sum(numbers);

            //assert
            Assert.Equal(expectedResult, result);
        }
    }
}
