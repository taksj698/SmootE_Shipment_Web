using AutoMapper;
using Document_Control.Core.dbModels;
using Document_Control.Core.pageModels.ManagePosition;
using Document_Control.Data.Repository.SQLServer;
using Document_Control.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Document_Control.Core.pageModels.ManageUser;
using Document_Control.Core.pageModels.ManageApproval;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Document_Control.Controllers
{
    [Authorize(Roles = "admin")]
    public class ManageUserController : Controller
    {
        private readonly WrapperRepository _wrapper;
        private SqlServerDbContext _dbContext;
        public ManageUserController(IHttpContextAccessor haccess, WrapperRepository wrapper)
        {
            _wrapper = wrapper;
            _dbContext = _wrapper._dbContext;
        }
        [HttpGet("ManageUser")]
        public IActionResult Index()
        {
            ViewBag.CurrentController = "ManageUser";
            ViewBag.CurrentAction = "Index";
            return View(Getdata());
        }
        public PartialViewResult edit(int? id)
        {
            PageManageUserSave obj = new PageManageUserSave();
            var find = _dbContext.TbUser.FirstOrDefault(x => x.Id == id);
            if (find != null)
            {
                var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TbUser, PageManageUserSave>());
                var mapper = new Mapper(config);
                mapper.Map(find, obj);
            }

            obj.lPosition = _dbContext.TbPosition
            .Select(s => new SelectListItem()
            {
                Text = s.PositionName,
                Value = s.Id.ToString()
            }).ToList();
            obj.lRole = _dbContext.TbRole
            .Select(s => new SelectListItem()
            {
                Text = s.RoleName,
                Value = s.Id.ToString()
            }).ToList();
            return PartialView("_ModalShow", obj);
        }
        public IActionResult update(PageManageUserSave obj)
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
                var find = _dbContext.TbUser.FirstOrDefault(x => x.Id == obj.Id);
                if (find != null)
                {
                    var config = new MapperConfiguration(cfg =>
                    cfg.CreateMap<PageManageUserSave, TbUser>()
                     .ForMember(dest => dest.Id, opt => opt.Ignore()));
                    var mapper = new Mapper(config);
                    mapper.Map(obj, find);
                    _dbContext.SaveChanges();
                }
            }
            else
            {
                TbUser data = new TbUser();
                var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<PageManageUserSave, TbUser>()
                 .ForMember(dest => dest.Id, opt => opt.Ignore()));
                var mapper = new Mapper(config);
                mapper.Map(obj, data);
                _dbContext.TbUser.Add(data);
                _dbContext.SaveChanges();
            }
            return Json(new { result = true, type = "success", message = "บันทึกสำเร็จ", url = "ManageUser" });
        }
        public IActionResult delete(int id)
        {
            var find = _dbContext.TbUser.FirstOrDefault(x => x.Id == id);
            if (find != null)
            {
                _dbContext.TbUser.Remove(find);
                _dbContext.SaveChanges();
            }
            return Json(new { result = true, type = "success", message = "ลบรายการสำเร็จ", url = "ManageUser" });
        }
        public PageManageUser Getdata()
        {
            PageManageUser obj = new PageManageUser();
            obj.tbUser = (from user in _dbContext.TbUser
                          join position in _dbContext.TbPosition on user.PositionId equals position.Id
                          join role in _dbContext.TbRole on user.RoleId equals role.Id
                          let checkdoc = _dbContext.TbDocumentTransaction.Any(s => s.UserId == user.Id)
                          let checkapp = _dbContext.TbApprovalTransaction.Any(s => s.UserId == user.Id)
                          select new DataUser()
                          {
                              Id = user.Id,
                              Name = user.Name,
                              TelNo = user.TelNo,
                              PositionName = position.PositionName,
                              RoleName = role.RoleName,
                              IsApprove = user.IsApprove,
                              IsManager = user.IsManager,
                              IsCanDelete = (checkdoc || checkapp) ? false : true
                          }).ToList();


            obj.pageManageUserSave = new PageManageUserSave();
            obj.pageManageUserSave.lPosition = _dbContext.TbPosition
            .Select(s => new SelectListItem()
            {
                Text = s.PositionName,
                Value = s.Id.ToString()
            }).ToList();
            obj.pageManageUserSave.lRole = _dbContext.TbRole
            .Select(s => new SelectListItem()
            {
                Text = s.RoleName,
                Value = s.Id.ToString()
            }).ToList();

            return obj;
        }
    }

}
