using QuickVisualWebWood.Core.comModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace QuickVisualWebWood.ControllerComponent
{
	public class TopMenuComponent : ViewComponent
	{
		private List<Claim>? UserProfile;
		private string? name;
		private string? position;
		public TopMenuComponent(IHttpContextAccessor haccess)
		{
			var identity = (ClaimsIdentity)haccess.HttpContext.User.Identity;
			UserProfile = identity.Claims.ToList();
			var fineName = UserProfile.FirstOrDefault(x => x.Type == ClaimTypes.Name);
			if (fineName != null)
			{
				name = fineName.Value;
			}
			var finePosition = UserProfile.FirstOrDefault(x => x.Type == "PositionName");
			if (finePosition != null)
			{
				position = finePosition.Value;
			}			
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			TopMenuModel obj = new TopMenuModel();
			obj.name = name;
			obj.positionName = position;
			return View("~/ViewComponents/TopMenuComponent.cshtml", obj);
		}
	}
}
