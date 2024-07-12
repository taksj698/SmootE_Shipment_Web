
using Document_Control.Core.pageModels;
using Document_Control.Data.BusinessUnit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Document_Control.Controllers
{
	[Authorize]
	public class PurchaseRequisitionController : Controller
	{
		private readonly PurchaseRequisitionBusiness _prBusiness;

		public PurchaseRequisitionController(PurchaseRequisitionBusiness prBusiness)
		{
			_prBusiness = prBusiness;
		}


		[HttpGet("PurchaseRequisition/{Id:int?}")]
		public IActionResult Index(int? Id)
		{
            ViewBag.CurrentController = "PurchaseRequisition";
            ViewBag.CurrentAction = "Index";
            return View(_prBusiness.GetData(Id));
		}




		public PartialViewResult LoadComponentApproval( int? id, decimal? budget)
		{
			return PartialView("_Approval", _prBusiness.GetLineApprove(id, budget));
		}




		
	}
}
