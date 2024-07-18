using System;
using System.Security.Claims;
using AutoMapper;
using Document_Control.Core.comModels;
using Document_Control.Core.dbModels;
using Document_Control.Core.pageModels.PurchaseRequisition;
using Document_Control.Data.Repository;
using Document_Control.Data.Repository.SQLServer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Document_Control.Data.BusinessUnit
{
	public class PurchaseRequisitionBusiness
	{
		private readonly WrapperRepository _wrapper;
		private SqlServerDbContext _dbContext;

		private List<Claim>? UserProfile;
		private int userId;
		private string? name;
		private int positionId;
		private string? position;
		public PurchaseRequisitionBusiness(IHttpContextAccessor haccess, WrapperRepository wrapper)
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

		public dynamic AddorUpdate(PagePR obj, string action)
		{
			int DocId = 0;
			if (obj.Id != null && obj.Id != 0)
			{
				var find = _dbContext.TbDocumentTransaction.FirstOrDefault(x => x.Id == obj.Id);
				if (find != null)
				{
					var config = new MapperConfiguration(cfg => cfg.CreateMap<PagePR, TbDocumentTransaction>());
					var mapper = new Mapper(config);
					mapper.Map(obj, find);
					find.StatusId = 1;
					_dbContext.TbDocumentTransaction.Update(find);
					_dbContext.SaveChanges();
					DocId = find.Id;
				}
			}
			else
			{
				var date = DateTime.Now;
				TbDocumentTransaction data = new TbDocumentTransaction();
				var config = new MapperConfiguration(cfg => cfg.CreateMap<PagePR, TbDocumentTransaction>());
				var mapper = new Mapper(config);
				mapper.Map(obj, data);

				data.DocumentCode = _wrapper._storedProcedureRepository.GenarateCode();
				data.CreateBy = userId;
				data.CreateDate = date;
				data.OrderDate = date;
				data.StatusId = 1;
				_dbContext.TbDocumentTransaction.Add(data);
				_dbContext.SaveChanges();
				DocId = data.Id;
			}

			StampApproval(DocId, obj.Budget.Value, action);
			StampHistory(DocId, action, obj.Reason);
			//flow
			//file
			//approval
			//history

			return new { result = true, type = "success", message = "บันทึกรายการสำเร็จ", url = "Home/MyTask" };
		}

		public void StampApproval(int DocId, decimal Budget, string action)
		{
			List<string> status = new List<string>() { "บันทึก", "บันทึกร่าง" };
			if (status.Contains(action))
			{
				var lineApprove = GetLineApprove(null, Budget);
				var find = _dbContext.TbApprovalTransaction.Where(x => x.DocId == DocId).ToList();
				if (find != null && find.Count > 0)
				{
					_dbContext.TbApprovalTransaction.RemoveRange(find);
					_dbContext.SaveChanges();
				}
				if (lineApprove != null && lineApprove.approvalLists != null && lineApprove.approvalLists.Count > 0)
				{
					List<TbApprovalTransaction> list = new List<TbApprovalTransaction>();

					foreach (var item in lineApprove.approvalLists)
					{
						list.Add(new TbApprovalTransaction()
						{
							DocId = DocId,
							Budget = Convert.ToDecimal(item.Budget),
							PositionId = item.PositionId.Value,
							IsApprove = false,
						});
					}
					if (list != null && list.Count > 0)
					{
						_dbContext.TbApprovalTransaction.AddRange(list);
						_dbContext.SaveChanges();
					}
				}
			}
		}
		public void StampHistory(int DocId, string action, string Reason)
		{
			_dbContext.TbHistoryTransaction.Add(new TbHistoryTransaction()
			{
				DocId = DocId,
				UserId = userId,
				PositionId = positionId,
				StatusId = 0,
				Action = action,
				Reason = Reason,
				StampDate = DateTime.Now
			});
			_dbContext.SaveChanges();
		}





		public ApprovalPR GetLineApprove(int? id, decimal? budget)
		{
			ApprovalPR obj = new ApprovalPR();
			obj.approvalLists = new List<ApprovalList>();
			var asd = _dbContext.TbApprovalMatrix.ToList();
			if (id != null && id != 0)
			{
				var find = (from ap in _dbContext.TbApprovalTransaction
							join po in _dbContext.TbPosition on ap.PositionId equals po.Id
							select new
							{
								Budget = ap.Budget,
								PositionId = ap.PositionId,
								PositionName = po.PositionName,
								IsApprove = ap.IsApprove,
							})
							.ToList();
				if (find != null && find.Count > 0)
				{
					foreach (var item in find)
					{
						obj.approvalLists.Add(new ApprovalList()
						{
							Budget = item.Budget.ToString("N2"),
							PositionId = item.PositionId,
							PositionName = item.PositionName,
							IsApproved = false
						});
					}
				}
			}
			else if (budget != null && budget != 0)
			{
				var findApp = _dbContext.TbApprovalMatrix.Where(x => x.IsActive && x.Budget <= budget).ToList();
				var findPo = _dbContext.TbPosition.ToList();
				if (findApp != null)
				{
					foreach (var item in findApp)
					{
						var position = findPo.FirstOrDefault(x => x.Id == item.PositionId);
						if (position != null)
						{
							obj.approvalLists.Add(new ApprovalList()
							{
								Budget = item.Budget.ToString("N2"),
								PositionId = position.Id,
								PositionName = position.PositionName
							});
						}
					}
				}
			}
			return obj;
		}
		public ModalShowApproval GetPositionApproval(int? id)
		{
			ModalShowApproval obj = new ModalShowApproval();

			if (id != null)
			{
				obj.approvalDetails = _dbContext.TbUser.Where(x => x.PositionId == id && x.IsApprove)
					.Select(s => new ApprovalDetail() { Name = s.Name, TelNo = s.TelNo })
					.ToList();
			}
			return obj;
		}
		public PagePR GetData(int? Id)
		{
			PagePR obj = new PagePR();
			obj.lPriority = _dbContext.TbPriority
			.OrderBy(o => o.Seq)
			.Select(s => new SelectListItem()
			{
				Text = s.PriorityName,
				Value = s.Id.ToString()
			}).ToList();
			obj.lCompany = _dbContext.TbCompany
			.Select(s => new SelectListItem()
			{
				Text = s.CompanyName,
				Value = s.Id.ToString()
			}).ToList();
			obj.lDivision = _dbContext.TbDivision
			.Select(s => new SelectListItem()
			{
				Text = s.DivisionName,
				Value = s.Id.ToString()
			}).ToList();
			obj.lUser = _dbContext.TbUser
			.Select(s => new SelectListItem()
			{
				Text = s.Name,
				Value = s.Id.ToString()
			}).ToList();
			if (Id == null)
			{
				obj.OrderDate = DateTime.Now;
				obj.DocumentCode = "Auto Generate";
				obj.CreateBy = userId;
				obj.PositionId = positionId;
				//
				obj.CreateName = name;
				obj.PositionName = position;
			}
			else
			{
				var find = _dbContext.TbDocumentTransaction.FirstOrDefault(x => x.Id == Id);
				if (find != null)
				{
					var config = new MapperConfiguration(cfg => cfg.CreateMap<TbDocumentTransaction, PagePR>());
					var mapper = new Mapper(config);
					mapper.Map(find, obj);

					var user = _dbContext.TbUser.FirstOrDefault(x => x.Id == find.CreateBy);
					var position = _dbContext.TbPosition.FirstOrDefault(x => x.Id == positionId);
					obj.CreateName = user?.Name;
					obj.PositionName = position?.PositionName;

					obj.ApprovalPR = GetLineApprove(find.Id, null);



				}
			}
			return obj;
		}
	}
}
