using QuickVisualWebWood.Data.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickVisualWebWood.Core.pageModels;
using QuickVisualWebWood.Data.Repository.SQLServer;
using Microsoft.AspNetCore.Authorization;
using QuickVisualWebWood.Data.Services;

namespace QuickVisualWebWood.Controllers
{
    public class LoginController : Controller
    {
        private readonly WrapperRepository _wrapperRepository;
        private readonly CryptographyServices _cryptographyServices;
        private SqlServerDbContext _dbContext;
        public LoginController(WrapperRepository wrapperRepository, CryptographyServices cryptographyServices)
        {
            _wrapperRepository = wrapperRepository;
            _cryptographyServices = cryptographyServices;
            _dbContext = _wrapperRepository._dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Login(loginModel obj)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                              .Where(y => y.Count > 0)
                              .LastOrDefault();
                return Json(new { result = false, type = "warning", message = (errors != null && errors.Count > 0) ? errors.FirstOrDefault().ErrorMessage : string.Empty });
            }
            var find = await _wrapperRepository._dbContext.TbUser.FirstOrDefaultAsync(x => x.UserName == obj.userName && x.Password == _cryptographyServices.GetMd5Hash(obj.passWord));
            if (find == null)
            {
                return Json(new { result = false, type = "error", message = "ชื่อผู้ใช้งานหรือรหัสผ่านไม่ถูกต้อง" });
            }
            else if (!find.IsActive)
            {
                return Json(new { result = false, type = "error", message = "ชื่อผู้ใช้งานยังไม่ได้เปิดใช้งาน" });
            }
            else
            {
                find.LastLogDate = DateTime.Now;
                _dbContext.TbUser.Update(find);
                _dbContext.SaveChanges();


                var findPosition = _wrapperRepository._dbContext.TbPosition.FirstOrDefault(x => x.Id == find.PositionId);
                var findRole = _wrapperRepository._dbContext.TbRole.FirstOrDefault(x => x.Id == find.RoleId);



                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Sid, find.Id.ToString()),
                    new Claim(ClaimTypes.Name, find.Name),
                    new Claim("PositionId",(findPosition != null) ?findPosition.Id.ToString() : string.Empty),
                    new Claim("PositionName",(findPosition != null) ?findPosition.PositionName : string.Empty),
                    new Claim(ClaimTypes.Role, (findRole != null) ?findRole.RoleName : string.Empty),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties { };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);


                return Json(new { result = true, type = "success", message = "สำเร็จ", url = "Home" });

            }

        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }
    }
}
