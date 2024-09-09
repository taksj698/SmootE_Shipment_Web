using System.Security.Claims;
using QuickVisualWebWood.Core.comModels;
using QuickVisualWebWood.Core.dbModels;
using QuickVisualWebWood.Data.Repository.SQLServer;
using QuickVisualWebWood.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks.Dataflow;
using QuickVisualWebWood.Core.pageModels.PurchaseRequisition;

namespace QuickVisualWebWood.ControllerComponent.PurchaseRequisition
{
	public class TabStatusComponent : ViewComponent
	{
		private readonly WrapperRepository _wrapper;
		private SqlServerDbContext _dbContext;

		private List<Claim>? UserProfile;
		private int userId;
		private string? name;
		private int positionId;
		private string? position;
		public TabStatusComponent(IHttpContextAccessor haccess, WrapperRepository wrapper)
		{
			_wrapper = wrapper;
			_dbContext = _wrapper._dbContext;

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
			var fineNameId = UserProfile.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
			if (fineNameId != null)
			{
				userId = Convert.ToInt32(fineNameId.Value);
			}
			var finePositionId = UserProfile.FirstOrDefault(x => x.Type == "PositionId");
			if (finePositionId != null)
			{
				positionId = Convert.ToInt32(finePositionId.Value);
			}
		}
		public async Task<IViewComponentResult> InvokeAsync(int? Id)
		{
			TabStatusBar obj = new TabStatusBar();
			obj.lStatus = new List<TabStatusBarItem>();


			if (Id == null || (Id != null && Id == 0))
			{

				var status = _dbContext.TbStatus.Where(x => x.IsMainFlow).OrderBy(o => o.GroupStatus).ToList();
				int count = 0;

				obj.StatusName = status.FirstOrDefault().StatusName;
				foreach (var item in status)
				{
					obj.lStatus.Add(new TabStatusBarItem()
					{
						StatusName = item.StatusName,
						StatusId = item.Id,
						Flag = (count == 0) ? 1 : 0

					});
					count++;
				}
			}
			else
			{
				var findDoc = _dbContext.TbDocumentTransaction.FirstOrDefault(x => x.Id == Id);

				var fstatus = _dbContext.TbStatus.FirstOrDefault(x => x.Id == findDoc.StatusId);

				obj.StatusName = fstatus.StatusName;
				List<int> stat = new List<int>();
				var countstat = 0;

				if (findDoc.StatusId == 1)
				{
					countstat = 0;
					stat = new List<int>() { 1, 4, 5 };
				}
				else if (findDoc.StatusId == 2)
				{
					countstat = 0;
					stat = new List<int>() { 2, 4, 5 };
				}
				else if (findDoc.StatusId == 3)
				{
					countstat = 0;
					stat = new List<int>() { 3, 4, 5 };
				}
				else if (findDoc.StatusId == 4)
				{
					stat = new List<int>() { 1, 4, 5 };
					countstat = 1;
				}
				else if (findDoc.StatusId == 5)
				{
					stat = new List<int>() { 1, 4, 5 };
					countstat = 2;
				}
				else if (findDoc.StatusId == 6)
				{
					stat = new List<int>() { 1, 4, 6 };
					countstat = 2;
				}

				var status = _dbContext.TbStatus.Where(x => stat.Contains(x.Id)).OrderBy(o => o.GroupStatus).ToList();
				int count = 0;
				foreach (var item in status)
				{
					obj.lStatus.Add(new TabStatusBarItem()
					{
						StatusName = item.StatusName,
						StatusId = item.Id,
						Flag = (countstat >= count) ? 1 : 0
					});
					count++;
				}
			}
			return View("~/Views/PurchaseRequisition/_TabStatusComponent.cshtml", obj);
		}
	}


}
