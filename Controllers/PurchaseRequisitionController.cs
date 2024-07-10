
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
			return View(_prBusiness.GetData(Id));
		}




		private PagePR GetData()
		{
			PagePR obj = new PagePR();







			return obj;
		}
	}
}
