using AutoMapper;
using Document_Control.Core.dbModels;
using Document_Control.Core.pageModels.ManagePosition;
using Document_Control.Data.Repository.SQLServer;
using Document_Control.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Document_Control.Core.pageModels.ManageApproval;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Document_Control.Controllers
{
    [Authorize(Roles = "admin")]
    public class ManageApprovalController : Controller
    {
        private readonly WrapperRepository _wrapper;
        private SqlServerDbContext _dbContext;
        public ManageApprovalController(IHttpContextAccessor haccess, WrapperRepository wrapper)
        {
            _wrapper = wrapper;
            _dbContext = _wrapper._dbContext;
        }
        [HttpGet("ManageApproval")]
        public IActionResult Index()
        {
            ViewBag.CurrentController = "ManageApproval";
            ViewBag.CurrentAction = "Index";
            return View(Getdata());
        }
        public PartialViewResult edit(int? id)
        {
            PageManageApprovalSave obj = new PageManageApprovalSave();
            var find = _dbContext.TbApprovalMatrix.FirstOrDefault(x => x.Id == id);
            if (find != null)
            {
                var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TbApprovalMatrix, PageManageApprovalSave>());
                var mapper = new Mapper(config);
                mapper.Map(find, obj);
            }
            obj.lPosition = _dbContext.TbPosition
            .Where(x => !_dbContext.TbApprovalMatrix.Select(s => s.PositionId).ToList().Contains(x.Id))
            .Select(s => new SelectListItem()
            {
                Text = s.PositionName,
                Value = s.Id.ToString()
            }).ToList();
            return PartialView("_ModalShow", obj);
        }
        public IActionResult update(PageManageApprovalSave obj)
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
                var find = _dbContext.TbApprovalMatrix.FirstOrDefault(x => x.Id == obj.Id);
                if (find != null)
                {
                    var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<PageManageApprovalSave, TbApprovalMatrix>()
                     .ForMember(dest => dest.Id, opt => opt.Ignore()));
                    var mapper = new Mapper(config);
                    mapper.Map(obj, find);
                    _dbContext.SaveChanges();
                }
            }
            else
            {
                TbApprovalMatrix data = new TbApprovalMatrix();
                var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<PageManageApprovalSave, TbApprovalMatrix>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore()));
                var mapper = new Mapper(config);
                mapper.Map(obj, data);
                _dbContext.TbApprovalMatrix.Add(data);
                _dbContext.SaveChanges();
            }
            return Json(new { result = true, type = "success", message = "บันทึกสำเร็จ", url = "ManageApproval" });
        }
        public IActionResult delete(int id)
        {
            var find = _dbContext.TbApprovalMatrix.FirstOrDefault(x => x.Id == id);
            if (find != null)
            {
                _dbContext.TbApprovalMatrix.Remove(find);
                _dbContext.SaveChanges();
            }
            return Json(new { result = true, type = "success", message = "ลบรายการสำเร็จ", url = "ManageApproval" });
        }
        public PageManageApproval Getdata()
        {
            PageManageApproval obj = new PageManageApproval();
            obj.tbApproval = (from app in _dbContext.TbApprovalMatrix
                              join position in _dbContext.TbPosition on app.PositionId equals position.Id
                              select new DataApproval()
                              {
                                  Id = app.Id,
                                  PositionName = position.PositionName,
                                  Budget = app.Budget
                              }).ToList();
            obj.pageManageApprovalSave = new PageManageApprovalSave();
            obj.pageManageApprovalSave.lPosition = _dbContext.TbPosition
            .Where(x=> !_dbContext.TbApprovalMatrix.Select(s=>s.PositionId).ToList().Contains(x.Id))
            .Select(s => new SelectListItem()
            {
                Text = s.PositionName,
                Value = s.Id.ToString()
            }).ToList();

            return obj;
        }
    }
}
