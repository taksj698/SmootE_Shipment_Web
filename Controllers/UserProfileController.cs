using Microsoft.AspNetCore.Mvc;

namespace SpeedTime.Controllers
{
	public class UserProfileController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
