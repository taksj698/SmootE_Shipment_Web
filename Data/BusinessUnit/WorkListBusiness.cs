using System.Linq;
using System.Security.Claims;
using QuickVisualWebWood.Core.pageModels.Home;
using QuickVisualWebWood.Data.Repository;
using QuickVisualWebWood.Data.Repository.SQLServer;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuickVisualWebWood.Data.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuickVisualWebWood.Data.BusinessUnit
{
	public class WorkListBusiness
	{
		private readonly WrapperRepository _wrapper;
		private SqlServerDbContext _dbContext;
		private readonly RisoServices _risoServices;
		private List<Claim>? UserProfile;
		private int userId;
		private string? name;
		private int positionId;
		private string? position;
		public WorkListBusiness(IHttpContextAccessor haccess, WrapperRepository wrapper, RisoServices risoServices)
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

			_risoServices = risoServices;
		}
		public Worklist MyTask()
		{

			Worklist obj = new Worklist();
			obj.data = (from weightData in _dbContext.TB_WeightData
						join weightType in _dbContext.TB_WeightType on weightData.WeightTypeID equals weightType.WeightTypeID
						join qu in _dbContext.TB_QualityTransaction on weightData.SequenceID equals qu.SequenceID into quGroup
						from qu in quGroup.DefaultIfEmpty()
						let customer = _dbContext.TB_Customers.FirstOrDefault(x => x.CustomerID == weightData.CustomerID)
						let branch = _dbContext.TB_Branch.FirstOrDefault(x => x.BranchID == weightData.BranchID)
						where (weightData.WeightState != null && !weightData.WeightState.Value) &&
						(weightData.CancelState != null && weightData.CancelState.Value == 0) &&
						(string.IsNullOrEmpty(qu.Status) || (!string.IsNullOrEmpty(qu.Status)) && qu.Status == "บันทึกร่าง")
						select new WorklistData
						{
							WeighNumber = weightData.TicketCodeIn,
							SequenceID = weightData.SequenceID,
							Plate = weightData.Plate1,
							WeightTypeName = weightType.WeightTypeName,
							CustomerName = (customer != null && !string.IsNullOrEmpty(customer.CustomerName)) ? customer.CustomerName : "-",
							TransctionDate = (qu != null && qu.QualityDate != null) ? qu.QualityDate.Value.ToString("dd/MM/yyyy") : "-",
							EvaluationResults = (qu != null && !string.IsNullOrEmpty(qu.ResultText)) ? qu.ResultText : "-",
							Status = (qu != null && !string.IsNullOrEmpty(qu.Status)) ? qu.Status : "-",
							Remark = (qu != null && !string.IsNullOrEmpty(qu.Description)) ? qu.Description : "-",
							Branch = (branch != null) ? branch.BranchName : "-",
							QualityByName = (!string.IsNullOrEmpty(weightData.QualityByName)) ? weightData.QualityByName : "-",
							UpdateDate = (qu.ModifyDate != null) ? qu.ModifyDate.Value.ToString("dd/MM/yyyy HH:mm") : (qu.CreateDate != null) ? qu.CreateDate.Value.ToString("dd/MM/yyyy HH:mm") : "-"
						}).ToList();
			return obj;
		}
		public Worklist Complete()
		{

			Worklist obj = new Worklist();
			obj.data = (from weightData in _dbContext.TB_WeightData
						join weightType in _dbContext.TB_WeightType on weightData.WeightTypeID equals weightType.WeightTypeID
						join qu in _dbContext.TB_QualityTransaction on weightData.SequenceID equals qu.SequenceID into quGroup
						from qu in quGroup.DefaultIfEmpty()
						let customer = _dbContext.TB_Customers.FirstOrDefault(x => x.CustomerID == weightData.CustomerID)
						let branch = _dbContext.TB_Branch.FirstOrDefault(x => x.BranchID == weightData.BranchID)
						where (weightData.WeightState != null && !weightData.WeightState.Value) &&
						(weightData.CancelState != null && weightData.CancelState.Value == 0) &&
						((!string.IsNullOrEmpty(qu.Status)) && qu.Status == "บันทึก")
						select new WorklistData
						{
							WeighNumber = weightData.TicketCodeIn,
							SequenceID = weightData.SequenceID,
							Plate = weightData.Plate1,
							WeightTypeName = weightType.WeightTypeName,
							CustomerName = (customer != null && !string.IsNullOrEmpty(customer.CustomerName)) ? customer.CustomerName : "-",
							TransctionDate = (qu != null && qu.QualityDate != null) ? qu.QualityDate.Value.ToString("dd/MM/yyyy") : "-",
							EvaluationResults = (qu != null && !string.IsNullOrEmpty(qu.ResultText)) ? qu.ResultText : "-",
							Status = (qu != null && !string.IsNullOrEmpty(qu.Status)) ? qu.Status : "-",
							Remark = (qu != null && !string.IsNullOrEmpty(qu.Description)) ? qu.Description : "-",
							Branch = (branch != null) ? branch.BranchName : "-",
							QualityByName = (!string.IsNullOrEmpty(weightData.QualityByName)) ? weightData.QualityByName : "-",
							UpdateDate = (qu.ModifyDate != null) ? qu.ModifyDate.Value.ToString("dd/MM/yyyy HH:mm") : (qu.CreateDate != null) ? qu.CreateDate.Value.ToString("dd/MM/yyyy HH:mm") : "-",
							IsCancel = true
						}).ToList();
			return obj;
		}


		public dynamic Cancel(string id)
		{
			var context = _wrapper._dbContext;
			var findData = context.TB_WeightData.FirstOrDefault(x => x.SequenceID == id);
			var findDoc = context.TB_DocumentFile.Where(x => x.SequenceID == id).ToList();
			var findQty = context.TB_QualityTransaction.Where(x => x.SequenceID == id).ToList();

			if (findData != null)
			{
				findData.WeightState = false;
				findData.QualityState = false;
				findData.QualityByName = null;
			}
			context.TB_WeightData.Update(findData);
			context.TB_DocumentFile.RemoveRange(findDoc);
			context.TB_QualityTransaction.RemoveRange(findQty);
			context.SaveChanges();

			var resQty = _risoServices.DeleteTbQualityTransaction(id);
			var rsDoc = _risoServices.DeleteTbDocumentFile(id);
			var res = _risoServices.updateWeightData(id, findData.QualityState.Value, string.Empty);
			return new { result = true, type = "success", message = "ยกเลิกรายการสำเร็จ", url = "Home/Complete" };
		}

	}
}
