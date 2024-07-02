using Microsoft.AspNetCore.Mvc;

namespace Document_Control.Controllers
{
	public class UserProfileController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
