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
    }
}
