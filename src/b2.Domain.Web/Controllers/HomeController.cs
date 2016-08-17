using Microsoft.AspNetCore.Mvc;

namespace b2.Domain.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Content("ok");
        }
    }
}