using Document_Control.Core.pageModels.ManageDivision;
using Document_Control.Data.Repository.SQLServer;
using Document_Control.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Document_Control.Core.dbModels;
using Document_Control.Core.pageModels.PurchaseRequisition;

namespace Document_Control.Controllers
{
    [Authorize(Roles = "admin")]
    public class ManageDivisionController : Controller
    {
        private readonly WrapperRepository _wrapper;
        private SqlServerDbContext _dbContext;
        public ManageDivisionController(IHttpContextAccessor haccess, WrapperRepository wrapper)
        {
            _wrapper = wrapper;
            _dbContext = _wrapper._dbContext;
        }
        [HttpGet("ManageDivision")]
        public IActionResult Index()
        {
            ViewBag.CurrentController = "ManageDivision";
            ViewBag.CurrentAction = "Index";
            return View(Getdata());
        }
        public PartialViewResult edit(int? id)
        {
            PageManageDivisionSave obj = new PageManageDivisionSave();
            var find = _dbContext.TbDivision.FirstOrDefault(x => x.Id == id);
            if (find != null)
            {
                var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TbDivision, PageManageDivisionSave>());
                var mapper = new Mapper(config);
                mapper.Map(find, obj);
            }
            return PartialView("_ModalShow", obj);
        }
        public IActionResult update(PageManageDivisionSave obj)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                              .Where(y => y.Count > 0)
                              .LastOrDefault();
                return Json(new { result = false, type = "warning", message = (errors != null && errors.Count > 0) ? errors.FirstOrDefault().ErrorMessage : string.Empty });
            }
            if (obj.Id != null)
            {
                var find = _dbContext.TbDivision.FirstOrDefault(x => x.Id == obj.Id);
                if (find != null)
                {
                    var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<PageManageDivisionSave, TbDivision>()
                     .ForMember(dest => dest.Id, opt => opt.Ignore()));
                    var mapper = new Mapper(config);
                    mapper.Map(obj, find);
                    _dbContext.SaveChanges();
                }
            }
            else
            {
                TbDivision data = new TbDivision();
                var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<PageManageDivisionSave, TbDivision>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore()));
                var mapper = new Mapper(config);
                mapper.Map(obj, data);
                _dbContext.TbDivision.Add(data);
                _dbContext.SaveChanges();
            }
            return Json(new { result = true, type = "success", message = "บันทึกสำเร็จ", url = "ManageDivision" });
        }
        public IActionResult delete(int id)
        {
            var find = _dbContext.TbDivision.FirstOrDefault(x => x.Id == id);
            if (find != null)
            {
                _dbContext.TbDivision.Remove(find);
                _dbContext.SaveChanges();
            }
            return Json(new { result = true, type = "success", message = "ลบรายการสำเร็จ", url = "ManageDivision" });
        }
        public PageManageDivision Getdata()
        {
            PageManageDivision obj = new PageManageDivision();
            obj.tbDivision = (from divi in _dbContext.TbDivision
                              let check = _dbContext.TbDocumentTransaction.Any(x => x.DivisionId == divi.Id)
                              select new DataDivision()
                              {
                                  Id = divi.Id,
                                  DivisionName = divi.DivisionName,
                                  IsCanDelete = !check
                              }).ToList();

            return obj;
        }
    }
}
