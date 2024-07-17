using Document_Control.Core.comModels;
using Document_Control.Core.dbModels;
using Document_Control.Data.Repository.SQLServer;
using Document_Control.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Document_Control.Core.pageModels.PurchaseRequisition;
using Document_Control.Core.pageModels.Home;

namespace Document_Control.ControllerComponent.Home
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
