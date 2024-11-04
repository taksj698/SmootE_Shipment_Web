using SmootE_Shipment_Web.Data.BusinessUnit;
using SmootE_Shipment_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace SmootE_Shipment_Web.Controllers
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

		public async Task<dynamic> Cancel(string id)
		{
			return await _workListBusiness.Cancel(id);
		}
	}
}
