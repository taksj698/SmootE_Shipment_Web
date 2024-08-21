using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Document_Control.Controllers
{
	[Authorize(Roles = "admin")]
	public class ManageDivisionController : Controller
    {
        [HttpGet("ManageDivision")]
        public IActionResult Index()
        {
            ViewBag.CurrentController = "ManageDivision";
            ViewBag.CurrentAction = "Index";
            return View();
        }
    }
}
