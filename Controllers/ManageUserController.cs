using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Document_Control.Controllers
{
	[Authorize(Roles = "admin")]
	public class ManageUserController : Controller
    {
        [HttpGet("ManageUser")]
        public IActionResult Index()
        {
            ViewBag.CurrentController = "ManageUser";
            ViewBag.CurrentAction = "Index";
            return View();
        }
    }
}
