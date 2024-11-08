using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmootE_Shipment_Web.Controllers
{
    [Authorize]
    public class CompleteController : Controller
    {


        [HttpGet("Complete")] ///{Id:int?}
        public IActionResult Index()
        {
            ViewBag.CurrentController = "Complete";
            ViewBag.CurrentAction = "Index";

            return View();
        }
    }
}
