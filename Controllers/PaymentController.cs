using Microsoft.AspNetCore.Mvc;

namespace SpeedTime.Controllers
{
	public class PaymentController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
