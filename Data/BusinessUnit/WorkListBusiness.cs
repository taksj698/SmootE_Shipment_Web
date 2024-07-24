﻿using System.Linq;
using System.Security.Claims;
using Document_Control.Core.pageModels.Home;
using Document_Control.Data.Repository;
using Document_Control.Data.Repository.SQLServer;
using Microsoft.EntityFrameworkCore;

namespace Document_Control.Data.BusinessUnit
{
	public class WorkListBusiness
	{
		private readonly WrapperRepository _wrapper;
		private SqlServerDbContext _dbContext;

		private List<Claim>? UserProfile;
		private int userId;
		private string? name;
		private int positionId;
		private string? position;
		public WorkListBusiness(IHttpContextAccessor haccess, WrapperRepository wrapper)
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
		public Worklist MyTask()
		{
			List<int> status = new List<int>() { 5, 6 };
			List<int> statusCreator = new List<int>() { 1, 2, 3 };
			Worklist obj = new Worklist();
			obj.data = (from doc in _dbContext.TbDocumentTransaction
						join user in _dbContext.TbUser on doc.CreateBy equals user.Id
						join sta in _dbContext.TbStatus on doc.StatusId equals sta.Id
						join pi in _dbContext.TbPriority on doc.PriorityId equals pi.Id
						let appNext = _dbContext.TbApprovalTransaction.Where(x => x.DocId == doc.Id && !x.IsApprove).OrderBy(o => o.Budget).FirstOrDefault()
						let appPass = _dbContext.TbApprovalTransaction.Where(x => x.DocId == doc.Id && x.IsApprove && x.PositionId == positionId).FirstOrDefault()
						where (doc.CreateBy == userId || (appNext != null && appNext.PositionId == positionId) || appPass != null) && !status.Contains(doc.StatusId)
						select new WorklistData
						{
							DocumentId = doc.Id,
							DocumentCode = doc.DocumentCode,
							DocumentDate = doc.OrderDate,
							Name = user.Name,
							Subject = doc.Subject,
							Priority = pi.PriorityName,
							PriorityId = pi.Id,
							Status = sta.StatusName,
							Approver = (from app in _dbContext.TbApprovalTransaction
										join po in _dbContext.TbPosition on app.PositionId equals po.Id
										where app.DocId == doc.Id && !app.IsApprove && !statusCreator.Contains(doc.StatusId)
										select new WorklistDataApprover
										{
											Budget = app.Budget,
											PositionId = po.Id,
											PositionName = po.PositionName
										}).OrderBy(o => o.Budget).FirstOrDefault()
						})
						  .OrderByDescending(o => o.DocumentDate)
						  .ToList();
			return obj;
		}
		public Worklist Complete()
		{
			List<int> status = new List<int>() { 5, 6 };
			Worklist obj = new Worklist();
			obj.data = (from doc in _dbContext.TbDocumentTransaction
						join user in _dbContext.TbUser on doc.CreateBy equals user.Id
						join sta in _dbContext.TbStatus on doc.StatusId equals sta.Id
						join pi in _dbContext.TbPriority on doc.PriorityId equals pi.Id
						let apper = _dbContext.TbApprovalTransaction.Where(x => x.DocId == doc.Id && x.IsApprove && x.PositionId == positionId).FirstOrDefault()
						where (doc.CreateBy == userId || (apper != null)) && status.Contains(doc.StatusId)
						select new WorklistData
						{
							DocumentId = doc.Id,
							DocumentCode = doc.DocumentCode,
							DocumentDate = doc.OrderDate,
							Name = user.Name,
							Subject = doc.Subject,
							Priority = pi.PriorityName,
							PriorityId = pi.Id,
							Status = sta.StatusName
						})
						  .OrderByDescending(o => o.DocumentDate)
						  .ToList();
			return obj;
		}
	}
}
