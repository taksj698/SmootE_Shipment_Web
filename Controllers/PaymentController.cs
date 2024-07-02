using Microsoft.AspNetCore.Mvc;

namespace Document_Control.Controllers
{
	public class PaymentController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
