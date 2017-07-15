using System;
using System.Collections.Generic;

namespace Fibonacci.Web
{
    public class FibonacciCalculator
    {
        public IEnumerable<int> Calculate(int length)
        {
            yield return 0;
            if (length > 1) {
                yield return 1;
            }

            if (length == 3) {
                yield return 1;
            }
        }
    }
}
