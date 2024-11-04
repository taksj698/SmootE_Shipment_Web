using Microsoft.AspNetCore.Mvc;

namespace SmootE_Shipment_Web.Controllers
{
    public class CheckLoadController : Controller
    {

        [HttpGet("CheckLoad")]
        public IActionResult Index()
        {

            ViewBag.CurrentController = "CheckLoad";
            ViewBag.CurrentAction = "Index";
            return View();
        }
    }
}
