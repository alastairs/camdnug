using System;
using System.Collections.Generic;

namespace Fibonacci.Web
{
    public class FibonacciCalculator
    {
        public IEnumerable<int> Calculate(int length)
        {
            yield return 0;
            if (length == 2) {
                yield return 1;
            }
        }
    }
}
