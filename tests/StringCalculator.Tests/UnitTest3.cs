using System;
using FluentAssertions;
using Task1.StringCalculator.Services;
using Xunit;

namespace Task1.StringCalculator.Test
{
    public class UnitTest3
    {
        [Fact]
        public void Test1_Success()
        {
            //arrange
            var numbers = "1\n2,3";
            var service = new StringCalculatorService();

            //act
            var result = service.Sum(numbers);

            //assert
            result.Should().Be(6);
        }        
    }
}
