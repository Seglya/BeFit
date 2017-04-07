using Microsoft.AspNetCore.Mvc;

namespace BeFit.Controllers
{
    public class AdminController : Controller

    {
        public IActionResult Index()
        {
            return View();
        }
    }
}