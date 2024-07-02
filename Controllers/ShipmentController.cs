using Microsoft.AspNetCore.Mvc;

namespace Document_Control.Controllers
{
	public class ShipmentController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
