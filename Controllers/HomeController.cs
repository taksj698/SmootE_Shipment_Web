using Document_Control.Data.BusinessUnit;
using Document_Control.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Document_Control.Controllers
{
	[Authorize]
	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WorkListBusiness _workListBusiness;
		
		public HomeController(ILogger<HomeController> logger, WorkListBusiness workListBusiness)
        {
            _logger = logger;
			_workListBusiness = workListBusiness;

		}

        public IActionResult MyTask()
        {
            ViewBag.CurrentController = "Home";
            ViewBag.CurrentAction = "MyTask";
            return View(_workListBusiness.MyTask());
        }
		public IActionResult Complete()
		{
			ViewBag.CurrentController = "Home";
			ViewBag.CurrentAction = "Complete";
			return View(_workListBusiness.Complete());
		}

		public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
