using Document_Control.Data.Repository.SQLServer;
using Document_Control.Data.Repository;
using System.Security.Claims;
using Document_Control.Core.pageModels.PurchaseRequisition;

namespace Document_Control.Data.BusinessUnit
{
	public class UserProfileBusiness
	{
		private readonly WrapperRepository _wrapper;
		private SqlServerDbContext _dbContext;
		private IHttpContextAccessor _haccess;

		private List<Claim>? UserProfile;
		private int userId;
		private string? name;
		private int positionId;
		private string? position;
		public UserProfileBusiness(IHttpContextAccessor haccess, WrapperRepository wrapper)
		{
			_wrapper = wrapper;
			_dbContext = _wrapper._dbContext;
			_haccess = haccess;

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

		public PagePR GetData() 
		{
			return null;
		}
	}
}
