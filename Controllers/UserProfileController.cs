using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Document_Control.Controllers
{
	[Authorize]
	public class UserProfileController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
