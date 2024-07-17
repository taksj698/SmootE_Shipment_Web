using Document_Control.Core.comModels;
using Document_Control.Core.dbModels;
using Microsoft.AspNetCore.Mvc;

namespace Document_Control.ControllerComponent.PurchaseRequisition
{
	public class TabStatusComponent : ViewComponent
	{
		public TabStatusComponent() 
		{

		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View("~/Views/PurchaseRequisition/_TabStatusComponent.cshtml");
		}
	}
}
