using Document_Control.Core.comModels;
using Document_Control.Core.dbModels;
using Document_Control.Data.Repository.SQLServer;
using Document_Control.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Document_Control.Core.pageModels.PurchaseRequisition;

namespace Document_Control.ControllerComponent.PurchaseRequisition
{
	public class ActionComponent : ViewComponent
	{
		private readonly WrapperRepository _wrapper;
		private SqlServerDbContext _dbContext;
		public ActionComponent(IHttpContextAccessor haccess, WrapperRepository wrapper)
		{
			_wrapper = wrapper;
			_dbContext = _wrapper._dbContext;
		}
		public async Task<IViewComponentResult> InvokeAsync(int? Id)
		{
			ActionCom obj = new ActionCom();

			var find = _dbContext.TbDocumentTransaction.FirstOrDefault(x => x.Id == Id);
			if (find != null)
			{
				obj.StatusId = find.StatusId;
			}
			else
			{
				obj.StatusId = 1;
			}

			return View("~/Views/PurchaseRequisition/_ActionComponent.cshtml", obj);
		}
	}
}
