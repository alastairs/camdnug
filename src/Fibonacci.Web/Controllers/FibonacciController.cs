using Microsoft.AspNetCore.Mvc;

namespace Fibonacci.Web.Controllers
{
    public class FibonacciController : Controller
    {
        [HttpPut("~/fibonacci/{numberOfTerms}")]
        public IActionResult Calculate([FromRoute]int numberOfTerms)
        {
            var result = new FibonacciCalculator().Calculate(numberOfTerms);
            return Ok(result);
        }
    }
}