using FluentAssertions;
using System;
using StringCalculator.Services;
using Xunit;

namespace StringCalculator.Tests
{
    public class StringCalculatorServiceTests
    {
        private readonly Services.StringCalculatorService _service = new Services.StringCalculatorService();

        [Fact]
        public void Sum_TwoNumbers_ShouldReturnSum()
        {
            //arrange
            var numbers = "2,3";            

            //act
            var result = _service.Sum(numbers);

            //assert
            result.Should().Be(5);
        }

        [Fact]
        public void Sum_EmptyString_ShouldReturnZero()
        {
            //arrange
            var numbers = "";            

            //act
            var result = _service.Sum(numbers);

            //assert
            result.Should().Be(0);
        }

        [Fact]
        public void Sum_OneNumber_ShouldReturnOneNumber()
        {
            //arrange
            var numbers = "1";

            //act
            var result = _service.Sum(numbers);

            //assert
            result.Should().Be(1);
        }

        [Fact]
        public void Sum_MultipleNumbers_ShouldReturnSum()
        {
            //arrange
            var numbers = "2,3,4,5";

            //act
            var result = _service.Sum(numbers);

            //assert
            result.Should().Be(14);
        }

        [Fact]
        public void Sum_MultipleNumbersWithDifferentDefaultDelimiters_ShouldReturnSum()
        {
            //arrange
            var numbers = "1\n2,3";

            //act
            var result = _service.Sum(numbers);

            //assert
            result.Should().Be(6);
        }

        [Fact]
        public void Sum_TwoNumbersWithCustomDelimiter_ShouldReturnSum()
        {
            //arrange
            var numbers = "//;\n1;2";

            //act
            var result = _service.Sum(numbers);

            //assert
            result.Should().Be(3);
        }

        [Fact]
        public void Sum_MultipleNumbersWithNegativeNumbers_ShouldReturnException()
        {
            //arrange
            var numbers = "//;\n1;2;-3;-5";
            var exception = new Exception();
            var expectedType = typeof(ArgumentException);
            var expectedMessage = "negatives not allowed: -3 -5";
            //act
            try
            {
                _service.Sum(numbers);
            }
            catch (Exception e)
            {
                exception = e;
            }

            //assert
            exception.GetType().Should().Be(expectedType);
            exception.Message.Should().Be(expectedMessage);
        }

        [Fact]
        public void Sum_MultipleNumbersWithBiggerNumber_ShouldReturnSumWithoutBiggerNumber()
        {
            //arrange
            var numbers = "//;\n1;2;1001;1";

            //act
            var result = _service.Sum(numbers);

            //assert
            result.Should().Be(4);
        }

        [Fact]
        public void Sum_MultipleNumbersWithLongDelimiter_ShouldReturnSum()
        {
            //arrange
            var numbers = "//[***]\n1***2***3";

            //act
            var result = _service.Sum(numbers);

            //assert
            result.Should().Be(6);
        }

        [Fact]
        public void Sum_MultipleNumbersWithMultipleDelimiter_ShouldReturnSum()
        {
            //arrange
            var numbers = "//[*][%]\n1*2%3";

            //act
            var result = _service.Sum(numbers);

            //assert
            result.Should().Be(6);
        }

        [Fact]
        public void Sum_MultipleNumbersWithMultipleLongDelimiter_ShouldReturnSum()
        {
            //arrange
            var numbers = "//[***][%%%]\n1***2%%%3";

            //act
            var result = _service.Sum(numbers);

            //assert
            result.Should().Be(6);
        }
    }
}
