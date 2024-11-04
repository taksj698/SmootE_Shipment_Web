using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmootE_Shipment_Web.Configs.Options;
using SmootE_Shipment_Web.Core.dbModels;
using SmootE_Shipment_Web.Core.pageModels.PurchaseRequisition;
using SmootE_Shipment_Web.Core.serviceModels;
using System.IO;
using System.Security.Claims;

namespace SmootE_Shipment_Web.Data.Services
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


        public ResponseModel updateWeightData(string sequenceId, bool qualityState, string qualityByName)
        {
            var login = Login();
            return _client.Put<ResponseModel>(new ParamsAPI()
            {
                Header = new List<BasicObject>()
                { new BasicObject()
                    {
                        key = "Authorization",
                        values = string.Format("Bearer {0}",(string)login.Data.token)
                    }
                },
                Url = string.Format("{0}/{1}", _apiOption.endpoint, "api/TbWeightData/sequenceid"),
                Data = JsonConvert.SerializeObject(new { sequenceId = sequenceId, qualityState = qualityState, qualityByName = qualityByName })
            });
        }


        public ResponseModel SaveFile(string base64,string contextType,string filename, string path)
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
                Url = string.Format("{0}/{1}", _apiOption.endpoint, "api/Helper/SaveFile"),
                Data = JsonConvert.SerializeObject(new 
                {
                    base64 = base64,
                    contextType = contextType,
                    filename = filename,
                    path = path
                })
            });
        }

        
    }
}
