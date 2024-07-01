using Microsoft.AspNetCore.Mvc;

namespace SpeedTime.Controllers
{
	public class ShipmentController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
