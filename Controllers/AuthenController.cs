using Microsoft.AspNetCore.Mvc;

namespace Document_Control.Controllers
{
    public class AuthenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
