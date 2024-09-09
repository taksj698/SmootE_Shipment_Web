using QuickVisualWebWood.Core.comModels;
using QuickVisualWebWood.Core.dbModels;
using QuickVisualWebWood.Data.Repository.SQLServer;
using QuickVisualWebWood.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using QuickVisualWebWood.Core.pageModels.PurchaseRequisition;
using QuickVisualWebWood.Core.pageModels.Home;

namespace QuickVisualWebWood.ControllerComponent.Home
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
