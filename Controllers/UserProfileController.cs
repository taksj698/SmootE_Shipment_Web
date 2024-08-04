using Document_Control.Core.pageModels.UserProfile;
using Document_Control.Data.BusinessUnit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Document_Control.Controllers
{
	[Authorize]
	public class UserProfileController : Controller
	{
		private readonly UserProfileBusiness _userProfileBusiness;

		public UserProfileController(UserProfileBusiness userProfileBusiness)
		{
			_userProfileBusiness = userProfileBusiness;
		}
		[HttpGet("UserProfile")]
		public IActionResult Index()
		{
			return View(_userProfileBusiness.GetData());
		}

		[HttpGet("UserProfile/callback")]
		public async Task<IActionResult> callback(string code,string state)
		{
			return View("Index",await _userProfileBusiness.UpdateNoto(code, state));
		}


		 
		public dynamic updateProfile(Profileview obj)
		{
			if (!ModelState.IsValid)
			{
				var errors = ModelState.Select(x => x.Value.Errors)
							  .Where(y => y.Count > 0)
							  .FirstOrDefault();
				return Json(new { result = false, type = "warning", message = (errors != null && errors.Count > 0) ? errors.FirstOrDefault().ErrorMessage : string.Empty });
			}
			return _userProfileBusiness.updateProfile(obj);
		}


		public dynamic ChangePass(ChangePass obj)
		{
			if (!ModelState.IsValid)
			{
				var errors = ModelState.Select(x => x.Value.Errors)
							  .Where(y => y.Count > 0)
							  .FirstOrDefault();
				return Json(new { result = false, type = "warning", message = (errors != null && errors.Count > 0) ? errors.FirstOrDefault().ErrorMessage : string.Empty });
			}
			return _userProfileBusiness.ChangePass(obj);
		}

		public async Task<dynamic> linetest()
		{
			return await _userProfileBusiness.linetest();
		}




		
	}
}
