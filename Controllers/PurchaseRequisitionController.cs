using Document_Control.Core.comModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Document_Control.Controllers
{
	[Authorize]
	public class PurchaseRequisitionController : Controller
    {
		[HttpGet("PurchaseRequisition/{Id:int?}")]
		public IActionResult Index(int? Id)
        {
			PagePR obj = new PagePR();

			return View();
        }




		private PagePR GetData() 
		{
			PagePR obj = new PagePR();
			return obj;
		}
	}
}
