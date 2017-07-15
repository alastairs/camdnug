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

            if (length > 2) {
                yield return CalculateNext(0, 1);
            } else {
                yield break;
            }
        }

        private int CalculateNext(int v1, int v2)
        {
            return v1 + v2;
        }
    }
}
