
using QuickVisualWebWood.Core.pageModels;
using QuickVisualWebWood.Core.pageModels.PurchaseRequisition;
using QuickVisualWebWood.Data.BusinessUnit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading.Tasks;

namespace QuickVisualWebWood.Controllers
{
    [Authorize]
    public class EstimateController : Controller
    {
        private readonly EstimateBusiness _esBusiness;

        public EstimateController(EstimateBusiness esBusiness)
        {
            _esBusiness = esBusiness;
        }
        [HttpGet("Estimate/{Id?}")]
        public IActionResult Index(string? Id)
        {
            ViewBag.CurrentController = "Estimate";
            ViewBag.CurrentAction = "Index";
            return View(_esBusiness.GetData(Id));
        }
        public async Task<dynamic> save(PagePR obj)
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
            return await _esBusiness.AddorUpdate(obj, "บันทึก");
        }
        public async Task<dynamic> draft(PagePR obj)
        {
            return await _esBusiness.AddorUpdate(obj, "บันทึกร่าง");
        }
        public async Task<dynamic> upload(IFormFile file)
        {
            return await _esBusiness.UploadDoc(file);
        }
        public dynamic deletefile(string id)
        {
            return _esBusiness.deletefile(id);
        }







        #region Component
        [HttpGet("Estimate/LoadComponentDocFile")]
        public PartialViewResult LoadComponentDocFile()
        {
            return PartialView("_FileComponent", _esBusiness.GetDocFile());
        }

        #endregion




    }
}
