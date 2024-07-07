using Microsoft.AspNetCore.Mvc;

namespace Document_Control.ControllerComponent
{
	public class SidebarMenuComponent : ViewComponent
	{
		public SidebarMenuComponent()
		{
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("~/ViewComponents/SidebarMenuComponent.cshtml");
		}
	}
}
