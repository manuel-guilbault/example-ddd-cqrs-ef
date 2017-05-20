using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace DddEfSample.Web.ActionResults
{
    public class SetHeaderResult : IActionResult
    {
        private readonly string _name;
        private readonly StringValues _values;

        public SetHeaderResult(string name, StringValues values)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _values = values;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.Headers.Add(_name, _values);
            return Task.FromResult(0);
        }
    }
}
