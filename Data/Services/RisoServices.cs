using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QuickVisualWebWood.Configs.Options;
using QuickVisualWebWood.Core.dbModels;
using QuickVisualWebWood.Core.serviceModels;
using System.Security.Claims;

namespace QuickVisualWebWood.Data.Services
{
    public class RisoServices
    {
        private readonly ApiOption _apiOption;


        private readonly RestServices _client;
        private List<BasicObject> Header;
        private IHttpContextAccessor _haccess;
        public RisoServices(ApiOption apiOption, RestServices restServices)
        {
            _apiOption = apiOption;
            _client = restServices;
        }

        public ResponseModel Login()
        {
            var obj = new
            {
                userName = _apiOption.username,
                password1 = _apiOption.password
            };
            return _client.Post<ResponseModel>(new ParamsAPI()
            {
                Header = Header,
                Url = string.Format("{0}/{1}", _apiOption.endpoint, "api/Auth/login"),
                Data = JsonConvert.SerializeObject(obj)
            });
        }
        public ResponseModel TbQualityTransaction(TBQualityTransaction obj)
        {
            var login = Login();
            return _client.Post<ResponseModel>(new ParamsAPI()
            {
                Header = new List<BasicObject>()
                { new BasicObject()
                    {
                        key = "Authorization",
                        values = string.Format("Bearer {0}",(string)login.Data.token)
                    }
                },
                Url = string.Format("{0}/{1}", _apiOption.endpoint, "api/TbQualityTransaction"),
                Data = JsonConvert.SerializeObject(obj)
            });
        }
        public ResponseModel TbDocumentFile(TbDocumentFile obj)
        {
            var login = Login();
            return _client.Post<ResponseModel>(new ParamsAPI()
            {
                Header = new List<BasicObject>()
                { new BasicObject()
                    {
                        key = "Authorization",
                        values = string.Format("Bearer {0}",(string)login.Data.token)
                    }
                },
                Url = string.Format("{0}/{1}", _apiOption.endpoint, "api/TbDocumentFile"),
                Data = JsonConvert.SerializeObject(obj)
            });
        }

        public ResponseModel DeleteTbQualityTransaction(string sequenceId)
        {
            var login = Login();
            return _client.Delete<ResponseModel>(new ParamsAPI()
            {
                Header = new List<BasicObject>()
                { new BasicObject()
                    {
                        key = "Authorization",
                        values = string.Format("Bearer {0}",(string)login.Data.token)
                    }
                },
                Url = string.Format("{0}/{1}/{2}", _apiOption.endpoint, "api/TbQualityTransaction/sequenceid", sequenceId),
                Data = string.Empty
            });
        }
        public ResponseModel DeleteTbDocumentFile(string sequenceId)
        {
            var login = Login();
            return _client.Delete<ResponseModel>(new ParamsAPI()
            {
                Header = new List<BasicObject>()
                { new BasicObject()
                    {
                        key = "Authorization",
                        values = string.Format("Bearer {0}",(string)login.Data.token)
                    }
                },
                Url = string.Format("{0}/{1}/{2}", _apiOption.endpoint, "api/TbDocumentFile/sequenceid", sequenceId),
                Data = string.Empty
            });
        }
    }
}
