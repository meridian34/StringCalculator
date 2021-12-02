using FluentAssertions;
using System;
using Task1.StringCalculator.Services;
using Xunit;

namespace Task1.StringCalculator.Test
{
    public class UnitTest2
    {
        [Fact]
        public void Test1_Success()
        {
            //arrange
            var numbers = "2,3,4,5";
            var service = new StringCalculatorService();

            //act
            var result = service.Sum(numbers);

            //assert
            result.Should().Be(14);
        }
    }
}
