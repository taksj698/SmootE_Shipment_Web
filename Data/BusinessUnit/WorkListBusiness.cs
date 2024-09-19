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
                        join qu in _dbContext.TB_QualityTransaction on weightData.SequenceID equals qu.SequenceID into quGroup
                        from qu in quGroup.DefaultIfEmpty()
                        let customer = _dbContext.TB_Customers.FirstOrDefault(x => x.CustomerID == weightData.CustomerID)
                        //where (weightData.QualityState == null || (weightData.QualityState != null && !weightData.QualityState.Value))
                        select new WorklistData
                        {
                            WeighNumber = weightData.TicketCodeIn,
                            SequenceID = weightData.SequenceID,
                            Plate = weightData.Plate1,
                            CustomerName = (customer != null && !string.IsNullOrEmpty(customer.CustomerName)) ? customer.CustomerName : "-",
                            TransctionDate = (qu != null && qu.QualityDate != null) ? qu.QualityDate.Value.ToString("dd/MM/yyyy") : "-",
                            EvaluationResults = (qu != null && !string.IsNullOrEmpty(qu.ResultText)) ? qu.ResultText : "-",
                            Status = (qu != null && !string.IsNullOrEmpty(qu.Status)) ? qu.Status : "-",
                            Remark = (qu != null && !string.IsNullOrEmpty(qu.Description)) ? qu.Description : "-",
                        }).ToList();
            return obj;
        }
    }
}
