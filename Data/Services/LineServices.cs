using Document_Control.Data.Repository.SQLServer;
using Document_Control.Data.Repository;
using Document_Control.Core.serviceModels;

namespace Document_Control.Data.Services
{
	public class LineServices
	{
		private readonly WrapperRepository _wrapper;
		private SqlServerDbContext _dbContext;
		private readonly RestServices _restServices;
		private IHttpContextAccessor _haccess;
		public LineServices(IHttpContextAccessor haccess, WrapperRepository wrapper, RestServices restServices)
		{
			_wrapper = wrapper;
			_dbContext = _wrapper._dbContext;
			_haccess = haccess;
			_restServices = restServices;
		}

		public async void SendMessageByToken(List<string> token, string message)
		{
			var noti = _wrapper._dbContext.TbConfigs.Where(x => x.Group == "LineNoti").ToList();
			if (noti != null)
			{
				var EndpointNoti = noti.FirstOrDefault(x => x.Name == "EndpointNoti")?.Value;
				var ContentType = noti.FirstOrDefault(x => x.Name == "ContentType")?.Value;

				if (token != null && token.Count > 0)
				{
					foreach (var item in token)
					{
						var getToken = await _restServices.PostAsync<dynamic>(new ParamsAPI
						{
							Url = EndpointNoti,
							Header = new List<BasicObject>() { new BasicObject() { key = "Authorization", values = $"Bearer {item}" } },
							ContentType = ContentType,
							Data2 = new Dictionary<string, string>
					{
						{ "message", message }
					},
						});
					}
				}
			}
		}
	}
}
