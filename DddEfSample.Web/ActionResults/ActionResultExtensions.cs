using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace DddEfSample.Web.ActionResults
{
    public static class ActionResultExtensions
    {
        public static IActionResult With(this IActionResult result, IActionResult other)
        {
            return new CompositeResult(result, other);
        }

        public static IActionResult WithHeader(this IActionResult result, string name, StringValues values)
        {
            return new SetHeaderResult(name, values).With(result);
        }
    }
}
