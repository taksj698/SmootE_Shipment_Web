using Microsoft.AspNetCore.Mvc;

namespace Document_Control.Controllers
{
	public class OrderController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
