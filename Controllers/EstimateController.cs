
using QuickVisualWebWood.Core.pageModels;
using QuickVisualWebWood.Core.pageModels.PurchaseRequisition;
using QuickVisualWebWood.Data.BusinessUnit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuickVisualWebWood.Controllers
{
	//[Authorize(Roles = "user, admin")]
	public class EstimateController : Controller
	{
		private readonly PurchaseRequisitionBusiness _prBusiness;

		public EstimateController(PurchaseRequisitionBusiness prBusiness)
		{
			_prBusiness = prBusiness;
		}


		[HttpGet("Estimate/{Id:int?}")]
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

		public dynamic approve(PagePR obj)
		{
			return _prBusiness.AddorUpdate(obj, "อนุมัติ");
		}
		public dynamic reject(PagePR obj)
		{
			return _prBusiness.AddorUpdate(obj, "ส่งกลับ");
		}
		public dynamic cancel(PagePR obj)
		{
			return _prBusiness.AddorUpdate(obj, "ยกเลิก");
		}

		public async Task<dynamic> upload(IFormFile file)
		{
			return await _prBusiness.UploadDoc(file);
		}
		public dynamic deletefile(string id)
		{
			return _prBusiness.deletefile(id);
		}

		public dynamic deleteapproval(int id)
		{
			return _prBusiness.deleteapproval(id);
		}


		public dynamic SelectRowApproval(int id)
		{
			return _prBusiness.SelectRowApproval(id);

		}


		



		#region Component

		public PartialViewResult LoadComponentDocFile(int? id)
		{
			return PartialView("_FileComponent", _prBusiness.GetDocFile(id));
		}

		#endregion




	}
}
