using System;
using StringCalculator.Services;
using Xunit;

namespace StringCalculator.Tests
{
    public class StringCalculatorServiceTests
    {
        private readonly StringCalculatorService _service = new StringCalculatorService();

        [Theory]
        [InlineData("", 0)]
        [InlineData("1", 1)]
        [InlineData("1,2", 3)]
        [InlineData("2,3,4,5", 14)]
        public void Sum_String_ShouldReturnSum(string numbers, int expectedResult)
        {
            //arrange
            
            //act
            var result = _service.Sum(numbers);

            //assert
            Assert.Equal(result, expectedResult);
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
            var numbers = "//;\\n1;2";
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
            var numbers = "//;\\n1;2;-3;-5";
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
            var numbers = "//;\\n1;2;1001;1";
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
            var numbers = "//[***]\\n1***2***3";
            var expectedResult = 6;

            //act
            var result = _service.Sum(numbers);

            //assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("//[*][%]\\n1*2%3", 6)]
        [InlineData("//[***][%%%]\\n1***2%%%3", 6)]
        public void Sum_MultipleNumbersWithMultipleLongDelimiters_ShouldReturnSum(string dataWithCustomDelimiter, int expectedResult)
        {
            //arrange
            
            //act
            var result = _service.Sum(dataWithCustomDelimiter);

            //assert
            Assert.Equal(expectedResult, result);
        }
    }
}
