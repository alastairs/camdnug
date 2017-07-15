using System;
using System.Collections.Generic;
using System.Linq;

namespace Fibonacci.Web
{
    public class FibonacciCalculator
    {
        public IEnumerable<int> Calculate(int length)
        {
            if (length == 1)
            {
                yield return 0;
            }
            else if (length == 2)
            {
                yield return 0;
                yield return 1;
            }
            else
            {
                int remainingTerms = length - 1;
                foreach (var term in Calculate(remainingTerms))
                {
                    yield return term;
                }

                yield return Calculate(remainingTerms).Last() +
                    Calculate(remainingTerms - 1).Last();
            }
        }
    }
}
