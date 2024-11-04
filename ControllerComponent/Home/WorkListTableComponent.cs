using SmootE_Shipment_Web.Core.comModels;
using SmootE_Shipment_Web.Core.dbModels;
using SmootE_Shipment_Web.Data.Repository.SQLServer;
using SmootE_Shipment_Web.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using SmootE_Shipment_Web.Core.pageModels.PurchaseRequisition;
using SmootE_Shipment_Web.Core.pageModels.Home;

namespace SmootE_Shipment_Web.ControllerComponent.Home
{
	public class WorkListTableComponent : ViewComponent
	{
		private readonly WrapperRepository _wrapper;
		private SqlServerDbContext _dbContext;
		public WorkListTableComponent(IHttpContextAccessor haccess, WrapperRepository wrapper)
		{
			_wrapper = wrapper;
			_dbContext = _wrapper._dbContext;
		}
		public async Task<IViewComponentResult> InvokeAsync(List<WorklistData>? data)
		{
			return View("~/Views/Home/_WorkListTableComponent.cshtml", data);
		}
	}
}
