using FluentAssertions;
using System;
using Task1.StringCalculator.Services;
using Xunit;

namespace Task1.StringCalculator.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1_Success()
        {
            //arrange
            var numbers = "2,3";
            var service = new StringCalculatorService();

            //act
            var result = service.Sum(numbers);

            //assert
            result.Should().Be(5);
        }

        [Fact]
        public void Test2_Success()
        {
            //arrange
            var numbers = "";
            var service = new StringCalculatorService();

            //act
            var result = service.Sum(numbers);

            //assert
            result.Should().Be(0);
        }

        [Fact]
        public void Test3_Success()
        {
            //arrange
            var numbers = "1";
            var service = new StringCalculatorService();

            //act
            var result = service.Sum(numbers);

            //assert
            result.Should().Be(1);
        }
    }
}
