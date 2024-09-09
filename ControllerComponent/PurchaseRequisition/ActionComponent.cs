﻿using QuickVisualWebWood.Core.comModels;
using QuickVisualWebWood.Core.dbModels;
using QuickVisualWebWood.Data.Repository.SQLServer;
using QuickVisualWebWood.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using QuickVisualWebWood.Core.pageModels.PurchaseRequisition;
using System.Security.Claims;
using System.Linq;

namespace QuickVisualWebWood.ControllerComponent.PurchaseRequisition
{
	public class ActionComponent : ViewComponent
	{
		private readonly WrapperRepository _wrapper;
		private SqlServerDbContext _dbContext;

		private List<Claim>? UserProfile;
		private int userId;
		private string? name;
		private int positionId;
		private string? position;
		public ActionComponent(IHttpContextAccessor haccess, WrapperRepository wrapper)
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
			return View("~/Views/PurchaseRequisition/_ActionComponent.cshtml", CheckperAndStatusById(Id));
		}


		public ActionCom CheckperAndStatusById(int? Id)
		{
			List<int> StatusActionForCreator = new List<int>() { 1, 2, 3 };
			int StatusActionForApprover = 4;
			ActionCom obj = new ActionCom();
			obj.IsShow = false;
			obj.IsReadonly = false;
			if (Id == null || (Id != null && Id == 0))
			{
				obj.IsShow = true;
				obj.StatusId = 1;
				return obj;
			}

			var find = _dbContext.TbDocumentTransaction.FirstOrDefault(x => x.Id == Id);
			if (find == null)
			{
				obj.StatusId = 1;
			}
			else
			{
				obj.StatusId = find.StatusId;
			}

			if (find.CreateBy == userId && StatusActionForCreator.Contains(find.StatusId))
			{
				obj.IsShow = true;

			}
			else
			{
				obj.IsReadonly = true;
			}

			var FindNextApprover = _dbContext.TbApprovalTransaction.Where(x => x.DocId == Id && !x.IsApprove).OrderBy(o => o.Id).ToList();
			if (FindNextApprover != null && FindNextApprover.Count > 0 && !StatusActionForCreator.Contains(find.StatusId))
			{
				var lastRow = FindNextApprover.FirstOrDefault();
				var findUser = _dbContext.TbUser.FirstOrDefault(x => x.Id == userId);

				if ((lastRow != null && lastRow.UserId != null && lastRow.UserId == userId) || (findUser != null && findUser.IsManager))
				{
					obj.IsShow = true;
				}
				else if (lastRow != null && lastRow.UserId == null && lastRow.PositionId == positionId)
				{
					var FindUserInPosition = _dbContext.TbUser.FirstOrDefault(x => x.PositionId == lastRow.PositionId && x.Id == userId && x.IsApprove);
					if (FindUserInPosition != null && !StatusActionForCreator.Contains(find.StatusId))
					{
						obj.IsShow = true;
					}

				}

			}
			return obj;
		}
	}
}
