using Microsoft.AspNetCore.Mvc;

namespace SpeedTime.Controllers
{
    public class AuthenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
