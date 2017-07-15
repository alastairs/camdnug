using System;
using System.Linq;
using Fibonacci.Web;
using Xunit;

namespace Fibonacci.Web.Test
{
    public class FibonacciFacts
    {
        [Fact]
        public void First_Term_Is_Zero()
        {
            var sut = new FibonacciCalculator();
            var result = sut.Calculate(1);
            Assert.Equal(new[] { 0 }, result.ToArray());
        }

        [Fact]
        public void Second_Term_Is_One()
        {
            var sut = new FibonacciCalculator();
            var result = sut.Calculate(2);
            Assert.Equal(new[] { 0, 1 }, result.ToArray());
        }

        [Theory]
        [InlineData(3, 1)]
        public void Later_Terms_Are_The_Sum_Of_The_Two_Previous_Terms(int term, int expected)
        {
            var sut = new FibonacciCalculator();
            var result = sut.Calculate(term).ToArray();
            Assert.Equal(expected, result[term - 1]);
        }
    }
}
