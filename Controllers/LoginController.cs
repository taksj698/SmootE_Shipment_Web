using SmootE_Shipment_Web.Data.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmootE_Shipment_Web.Core.pageModels;
using SmootE_Shipment_Web.Data.Repository.SQLServer;
using Microsoft.AspNetCore.Authorization;
using SmootE_Shipment_Web.Data.Services;
using SmootE_Shipment_Web.Helper;

namespace SmootE_Shipment_Web.Controllers
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


			var user = EncDec.Encrypt(obj.userName, "").Trim();
			var pass = EncDec.Encrypt(obj.passWord, "").Trim();



			var find = await _wrapperRepository._dbContext.TB_Users.FirstOrDefaultAsync(x => x.UserName == user && x.Password1 == pass);
			if (find == null)
			{
				return Json(new { result = false, type = "error", message = "ชื่อผู้ใช้งานหรือรหัสผ่านไม่ถูกต้อง" });
			}
			else if (find.InActive != null && find.InActive.Value)
			{
				return Json(new { result = false, type = "error", message = "ชื่อผู้ใช้งานยังไม่ได้เปิดใช้งาน" });
			}
			else
			{
				var claims = new List<Claim> {
					new Claim(ClaimTypes.Sid, find.UserID.ToString()),
					new Claim(ClaimTypes.Name, find.FullUserName)
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
