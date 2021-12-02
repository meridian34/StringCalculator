using FluentAssertions;
using System;
using StringCalculator.Services;
using Xunit;

namespace StringCalculator.Tests
{
    public class UnitTests
    {
        private readonly StringCalculatorService _service = new StringCalculatorService();

        [Fact]
        public void Task1_1_Sum_Success()
        {
            //arrange
            var numbers = "2,3";            

            //act
            var result = _service.Sum(numbers);

            //assert
            result.Should().Be(5);
        }

        [Fact]
        public void Task1_2_Sum_Success()
        {
            //arrange
            var numbers = "";            

            //act
            var result = _service.Sum(numbers);

            //assert
            result.Should().Be(0);
        }

        [Fact]
        public void Task1_3_Sum_Success()
        {
            //arrange
            var numbers = "1";

            //act
            var result = _service.Sum(numbers);

            //assert
            result.Should().Be(1);
        }

        [Fact]
        public void Task2_Sum_Success()
        {
            //arrange
            var numbers = "2,3,4,5";

            //act
            var result = _service.Sum(numbers);

            //assert
            result.Should().Be(14);
        }

        [Fact]
        public void Task3_Sum_Success()
        {
            //arrange
            var numbers = "1\n2,3";

            //act
            var result = _service.Sum(numbers);

            //assert
            result.Should().Be(6);
        }

        [Fact]
        public void Task4_Sum_Success()
        {
            //arrange
            var numbers = "//;\n1;2";

            //act
            var result = _service.Sum(numbers);

            //assert
            result.Should().Be(3);
        }
    }
}
