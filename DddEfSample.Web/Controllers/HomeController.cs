using Microsoft.AspNetCore.Mvc;

namespace DddEfSample.Web
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
