using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Document_Control.Controllers
{
	[Authorize(Roles = "admin")]
	public class ManageApprovalController : Controller
    {
        [HttpGet("ManageApproval")]
        public IActionResult Index()
        {
            ViewBag.CurrentController = "ManageApproval";
            ViewBag.CurrentAction = "Index";
            return View();
        }
    }
}
