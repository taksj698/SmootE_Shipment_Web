using System.Linq;
using System.Security.Claims;
using QuickVisualWebWood.Core.pageModels.Home;
using QuickVisualWebWood.Data.Repository;
using QuickVisualWebWood.Data.Repository.SQLServer;
using Microsoft.EntityFrameworkCore;

namespace QuickVisualWebWood.Data.BusinessUnit
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
			var fineNameId = UserProfile.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
			if (fineNameId != null)
			{
				userId = Convert.ToInt32(fineNameId.Value);
			}

		}
		public Worklist MyTask()
		{

			Worklist obj = new Worklist();
			obj.data = (from weightData in _dbContext.TB_WeightData
						where weightData.CancelState == 0 && weightData.WeightState == false
                        select new WorklistData
						{
							WeighNumber = weightData.TicketCodeIn,
							SequenceID = weightData.SequenceID,
							Plate = weightData.Plate1,
							CustomerName = weightData.CustomerID,
                            TransctionDate = DateTime.Now,
							EvaluationResults = string.Empty,
							Status = string.Empty,
							Remark = string.Empty,
						}).ToList();
			return obj;
		}
	}
}
