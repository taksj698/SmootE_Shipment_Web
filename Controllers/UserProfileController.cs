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
	}
}
