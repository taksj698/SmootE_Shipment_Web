
using Document_Control.Core.pageModels;
using Document_Control.Core.pageModels.PurchaseRequisition;
using Document_Control.Data.BusinessUnit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
		public dynamic save(PagePR obj)
		{
			if (!ModelState.IsValid)
			{
				var errors = ModelState.Select(x => x.Value.Errors)
							  .Where(y => y.Count > 0)
							  .FirstOrDefault();
				return Json(new
				{
					result = false,
					type = "warning",
					message = (errors != null && errors.Count > 0) ? errors.FirstOrDefault().ErrorMessage : string.Empty
				});
			}
			return _prBusiness.AddorUpdate(obj, "บันทึก");
		}
		public dynamic draft(PagePR obj)
		{
			return _prBusiness.AddorUpdate(obj, "บันทึกร่าง");
		}


		#region Component
		public PartialViewResult LoadComponentApproval(int? id, decimal? budget)
		{
			return PartialView("_Approval", _prBusiness.GetLineApprove(id, budget));
		}
		public PartialViewResult LoadPositionApproval(int? id)
		{
			return PartialView("_ModalShowAproval", _prBusiness.GetPositionApproval(id));
		}
		#endregion




	}
}
