using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmootE_Shipment_Web.Data.BusinessUnit;

namespace SmootE_Shipment_Web.Controllers
{
    //[Authorize]

    public class CheckLoadController : Controller
    {
        private readonly CheckLoadBusiness _checkLoadBusiness;
        public CheckLoadController(CheckLoadBusiness checkLoadBusiness) 
        {
            _checkLoadBusiness = checkLoadBusiness;
        }

        [HttpGet("CheckLoad/{invNo}")]
        public IActionResult Index(string invNo)
        {
            ViewBag.CurrentController = "CheckLoad";
            ViewBag.CurrentAction = "Index";
            return View(_checkLoadBusiness.GetDataById(invNo));
        }


    }
}
