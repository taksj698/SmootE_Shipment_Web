using System.Security.Claims;
using QuickVisualWebWood.Core.comModels;
using Microsoft.AspNetCore.Mvc;

namespace QuickVisualWebWood.ControllerComponent
{
	public class SidebarMenuComponent : ViewComponent
	{
		private List<Claim>? UserProfile;
		private string? name;
		private string? position;
		private string? role;
		public SidebarMenuComponent(IHttpContextAccessor haccess)
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
			var fineRole = UserProfile.FirstOrDefault(x => x.Type == ClaimTypes.Role);
			if (fineRole != null)
			{
				role = fineRole.Value;
			}
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			SidebarMenuModel obj  = new SidebarMenuModel();
			obj.role = role;
			return View("~/ViewComponents/SidebarMenuComponent.cshtml", obj);
		}
	}
}
