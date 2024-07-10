using System.Security.Claims;
using Document_Control.Core.comModels;
using Document_Control.Core.pageModels;
using Document_Control.Data.Repository;

namespace Document_Control.Data.BusinessUnit
{
	public class PurchaseRequisitionBusiness
	{
		private readonly WrapperRepository _wrapper;
		private List<Claim>? UserProfile;
		private int? userId;
		private string? name;
		private int? positionId;
		private string? position;
		public PurchaseRequisitionBusiness(IHttpContextAccessor haccess,WrapperRepository wrapper)
		{
			_wrapper = wrapper;

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



		public  PagePR GetData(int? Id)
		{
			PagePR obj = new PagePR();
			//GeneratePRAsync
			//var asd =  _wrapper._storedProcedureRepository.GenarateCode();

			if (Id == null)
			{
				obj.OrderDate = DateTime.Now;
				obj.DocumentCode = "Auto Generate";
				obj.CreateBy = userId;
				obj.PositionId = positionId;
				obj.CreateName = name;
				obj.PositionName = position;
			}
			else { 
			}


			return obj;
		}
	}
}
