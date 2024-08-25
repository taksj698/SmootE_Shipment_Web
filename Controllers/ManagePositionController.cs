using AutoMapper;
using Document_Control.Core.dbModels;
using Document_Control.Data.Repository.SQLServer;
using Document_Control.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Document_Control.Core.pageModels.ManagePosition;

namespace Document_Control.Controllers
{

    [Authorize(Roles = "admin")]
    public class ManagePositionController : Controller
    {
        private readonly WrapperRepository _wrapper;
        private SqlServerDbContext _dbContext;
        public ManagePositionController(IHttpContextAccessor haccess, WrapperRepository wrapper)
        {
            _wrapper = wrapper;
            _dbContext = _wrapper._dbContext;
        }
        [HttpGet("ManagePosition")]
        public IActionResult Index()
        {
            ViewBag.CurrentController = "ManagePosition";
            ViewBag.CurrentAction = "Index";
            return View(Getdata());
        }
        public PartialViewResult edit(int? id)
        {
            PageManagePositionSave obj = new PageManagePositionSave();
            var find = _dbContext.TbPosition.FirstOrDefault(x => x.Id == id);
            if (find != null)
            {
                var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TbPosition, PageManagePositionSave>());
                var mapper = new Mapper(config);
                mapper.Map(find, obj);
            }
            return PartialView("_ModalShow", obj);
        }
        public IActionResult update(PageManagePositionSave obj)
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
                var find = _dbContext.TbPosition.FirstOrDefault(x => x.Id == obj.Id);
                if (find != null)
                {
                    var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<PageManagePositionSave, TbPosition>()
                     .ForMember(dest => dest.Id, opt => opt.Ignore()));
                    var mapper = new Mapper(config);
                    mapper.Map(obj, find);
                    _dbContext.SaveChanges();
                }
            }
            else
            {
                TbPosition data = new TbPosition();
                var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<PageManagePositionSave, TbPosition>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore()));
                var mapper = new Mapper(config);
                mapper.Map(obj, data);
                _dbContext.TbPosition.Add(data);
                _dbContext.SaveChanges();
            }
            return Json(new { result = true, type = "success", message = "บันทึกสำเร็จ", url = "ManagePosition" });
        }
        public IActionResult delete(int id)
        {
            var find = _dbContext.TbPosition.FirstOrDefault(x => x.Id == id);
            if (find != null)
            {
                _dbContext.TbPosition.Remove(find);
                _dbContext.SaveChanges();
            }
            return Json(new { result = true, type = "success", message = "ลบรายการสำเร็จ", url = "ManagePosition" });
        }
        public PageManagePosition Getdata()
        {
            PageManagePosition obj = new PageManagePosition();
            obj.tbPosition = (from po in _dbContext.TbPosition
                              let check = _dbContext.TbUser.Any(x => x.PositionId == po.Id)
                              select new DataPosition()
                              {
                                  Id = po.Id,
                                  PositionName = po.PositionName,
                                  IsCanDelete = !check
                              }).ToList();

            return obj;
        }
    }
}
