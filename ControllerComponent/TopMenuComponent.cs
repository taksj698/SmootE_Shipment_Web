using Document_Control.Core.comModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Document_Control.ControllerComponent
{
	public class TopMenuComponent : ViewComponent
	{
		private List<Claim>? UserProfile;
		private string? name;
		public TopMenuComponent(IHttpContextAccessor haccess)
		{
			var identity = (ClaimsIdentity)haccess.HttpContext.User.Identity;
			UserProfile = identity.Claims.ToList();
			var fineName = UserProfile.FirstOrDefault(x => x.Type == ClaimTypes.Name);
			if (fineName != null)
			{
				name = fineName.Value;
			}
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			TopMenuModel obj = new TopMenuModel();
			obj.name = name;
			return View("~/ViewComponents/TopMenuComponent.cshtml", obj);
		}
	}
}
