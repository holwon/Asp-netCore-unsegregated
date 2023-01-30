using Microsoft.AspNetCore.Mvc;

namespace test.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
