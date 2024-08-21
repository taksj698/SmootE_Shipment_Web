using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Document_Control.Controllers
{
	[Authorize(Roles = "admin")]
	public class ManagePositionController : Controller
    {
        [HttpGet("ManagePosition")]
        public IActionResult Index()
        {
            ViewBag.CurrentController = "ManagePosition";
            ViewBag.CurrentAction = "Index";
            return View();
        }
    }
}
