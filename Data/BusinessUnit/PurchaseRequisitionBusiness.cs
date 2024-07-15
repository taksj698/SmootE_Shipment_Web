using System.Security.Claims;
using Document_Control.Core.comModels;
using Document_Control.Core.pageModels.PurchaseRequisition;
using Document_Control.Data.Repository;
using Document_Control.Data.Repository.SQLServer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Document_Control.Data.BusinessUnit
{
	public class PurchaseRequisitionBusiness
	{
		private readonly WrapperRepository _wrapper;
		private SqlServerDbContext _dbContext;

		private List<Claim>? UserProfile;
		private int? userId;
		private string? name;
		private int? positionId;
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





		public ApprovalPR GetLineApprove(int? id, decimal? budget)
		{
			ApprovalPR obj = new ApprovalPR();
			obj.approvalLists = new List<ApprovalList>();
			var asd = _dbContext.TbApprovalMatrix.ToList();
			if (id != null && id != 0)
			{

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
			//GeneratePRAsync
			//var asd =  _wrapper._storedProcedureRepository.GenarateCode();


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
				obj.CreateName = name;
				obj.PositionName = position;
			}
			else
			{
			}


			return obj;
		}
	}
}
