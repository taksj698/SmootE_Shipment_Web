using Document_Control.Data.Repository.SQLServer;
using Document_Control.Data.Repository;
using System.Security.Claims;
using Document_Control.Core.pageModels.PurchaseRequisition;
using Document_Control.Core.pageModels.UserProfile;
using Document_Control.Data.Services;
using Document_Control.Core.serviceModels;
using Azure.Core;

namespace Document_Control.Data.BusinessUnit
{
	public class UserProfileBusiness
	{
		private readonly WrapperRepository _wrapper;
		private SqlServerDbContext _dbContext;
		private readonly RestServices _restServices;
		private IHttpContextAccessor _haccess;

		private List<Claim>? UserProfile;
		private int userId;
		private string? name;
		private int positionId;
		private string? position;
		public UserProfileBusiness(IHttpContextAccessor haccess, WrapperRepository wrapper, RestServices restServices)
		{
			_wrapper = wrapper;
			_dbContext = _wrapper._dbContext;
			_haccess = haccess;
			_restServices = restServices;

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

		public UserProfileModel GetData()
		{
			UserProfileModel obj = new UserProfileModel();
			var noti = _wrapper._dbContext.TbConfigs.Where(x => x.Group == "LineNoti").ToList();
			if (noti != null)
			{
				var EndpointRegis = noti.FirstOrDefault(x => x.Name == "EndpointRegis")?.Value;
				var ClientId = noti.FirstOrDefault(x => x.Name == "ClientId")?.Value;
				var ClientSecret = noti.FirstOrDefault(x => x.Name == "ClientSecret")?.Value;

				var CallBack = noti.FirstOrDefault(x => x.Name == "CallBack")?.Value;
				var ResponseType = noti.FirstOrDefault(x => x.Name == "ResponseType")?.Value;
				var Scope = noti.FirstOrDefault(x => x.Name == "Scope")?.Value;
				obj.notiSetting = new NotiSetting();

				var findToken = _dbContext.TbUser.FirstOrDefault(x => x.Id == userId)?.NotifyToken;

				obj.notiSetting.token = findToken;
				obj.notiSetting.link = $"{EndpointRegis}?response_type={ResponseType}&client_id={ClientId}&redirect_uri={_haccess?.HttpContext?.Request.Scheme}://{_haccess?.HttpContext?.Request.Host}/{CallBack}&scope={Scope}&state={userId}";
			}
			return obj;
		}

		public async Task<UserProfileModel> UpdateNoto(string code, string state)
		{
			var noti = _wrapper._dbContext.TbConfigs.Where(x => x.Group == "LineNoti").ToList();
			if (noti != null)
			{
				var EndpointToken = noti.FirstOrDefault(x => x.Name == "EndpointToken")?.Value;

				var ContentType = noti.FirstOrDefault(x => x.Name == "ContentType")?.Value;
				var ClientId = noti.FirstOrDefault(x => x.Name == "ClientId")?.Value;
				var ClientSecret = noti.FirstOrDefault(x => x.Name == "ClientSecret")?.Value;
				var GrantType = noti.FirstOrDefault(x => x.Name == "GrantType")?.Value;
				var CallBack = noti.FirstOrDefault(x => x.Name == "CallBack")?.Value;


				var getToken = await _restServices.PostAsync<dynamic>(new ParamsAPI
				{
					Url = EndpointToken,
					ContentType = ContentType,
					Data2 = new Dictionary<string, string>
					{
						{ "grant_type", GrantType },
						{ "code", code },
						{ "client_id", ClientId },
						{ "client_secret", ClientSecret },
						{ "redirect_uri", $"{_haccess?.HttpContext?.Request.Scheme}://{_haccess?.HttpContext?.Request.Host}/{CallBack}" },
					},
				});

				if (getToken != null)
				{
					var status = (int?)getToken?.status;
					if (status == 200)
					{
						var token = (string?)getToken?.access_token;
						var findUser = _dbContext.TbUser.FirstOrDefault(x => x.Id == userId);
						if (findUser != null)
						{
							findUser.NotifyToken = token;
							_dbContext.TbUser.Update(findUser);
							_dbContext.SaveChanges();
						}
					}
				}
			}
			var data = GetData();
			data.Tab = "noti-tab";
			return data;
		}

	}
}


