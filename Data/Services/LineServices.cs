using QuickVisualWebWood.Data.Repository.SQLServer;
using QuickVisualWebWood.Data.Repository;
using QuickVisualWebWood.Core.serviceModels;

namespace QuickVisualWebWood.Data.Services
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
            if (token != null && token.Count > 0)
            {
                foreach (var item in token)
                {
                    var getToken = await _restServices.PostAsync<dynamic>(new ParamsAPI
                    {
                        Url = "https://notify-api.line.me/api/notify",
                        Header = new List<BasicObject>() { new BasicObject() { key = "Authorization", values = $"Bearer {item}" } },
                        ContentType = "application/x-www-form-urlencoded",
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
