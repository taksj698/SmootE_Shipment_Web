using Microsoft.AspNetCore.Mvc;

namespace SpeedTime.Controllers
{
	public class OrderController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
