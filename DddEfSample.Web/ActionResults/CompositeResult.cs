using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using System.Linq;

namespace DddEfSample.Web.ActionResults
{
    public class CompositeResult : IActionResult, IEnumerable<IActionResult>
    {
        private readonly IActionResult[] _results;

        public CompositeResult(params IActionResult[] results)
        {
            _results = results ?? throw new ArgumentNullException(nameof(results));
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            foreach (var result in _results)
            {
                await result.ExecuteResultAsync(context);
            }
        }

        public IEnumerator<IActionResult> GetEnumerator()
        {
            return _results.Cast<IActionResult>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
