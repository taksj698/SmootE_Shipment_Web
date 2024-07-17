using Document_Control.Core.comModels;
using Document_Control.Core.dbModels;
using Document_Control.Data.Repository.SQLServer;
using Document_Control.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Document_Control.Core.pageModels.PurchaseRequisition;

namespace Document_Control.ControllerComponent.PurchaseRequisition
{
	public class HistoryComponent : ViewComponent
	{
		private readonly WrapperRepository _wrapper;
		private SqlServerDbContext _dbContext;
		public HistoryComponent(IHttpContextAccessor haccess, WrapperRepository wrapper)
		{
			_wrapper = wrapper;
			_dbContext = _wrapper._dbContext;
		}
		public async Task<IViewComponentResult> InvokeAsync(int? Id)
		{
			HistoryCom obj = new HistoryCom();
			if (Id != null && Id != 0)
			{
				obj.data = (from his in _dbContext.TbHistoryTransaction
							join user in _dbContext.TbUser on his.UserId equals user.Id into userGroup
							from user in userGroup.DefaultIfEmpty()
							join po in _dbContext.TbPosition on his.PositionId equals po.Id into poGroup
							from po in poGroup.DefaultIfEmpty()
							join sta in _dbContext.TbStatus on his.StatusId equals sta.Id into staGroup
							from sta in staGroup.DefaultIfEmpty()
							where his.DocId == Id
							select new HistoryComData
							{
								StampDate = his.StampDate,
								Name = user == null ? null : user.Name,
								Position = po == null ? null : po.PositionName,
								Action = his.Action,
								Reason = his.Reason
							})
							.OrderBy(o => o.StampDate)
							.ToList();
			}
			return View("~/Views/PurchaseRequisition/_HistoryComponent.cshtml", obj);
		}
	}
}
