using SmootE_Shipment_Web.Data.Repository.SQLServer;
using SmootE_Shipment_Web.Data.Repository;
using System.Security.Claims;
using SmootE_Shipment_Web.Core.pageModels.PurchaseRequisition;
using SmootE_Shipment_Web.Core.pageModels.UserProfile;
using SmootE_Shipment_Web.Data.Services;
using SmootE_Shipment_Web.Core.serviceModels;
using Azure.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmootE_Shipment_Web.Data.BusinessUnit
{
    public class UserProfileBusiness
    {
        private readonly WrapperRepository _wrapper;
        private SqlServerDbContext _dbContext;
        private readonly RestServices _restServices;
        private readonly CryptographyServices _cryptographyServices;
        private IHttpContextAccessor _haccess;

        private readonly LineServices _lineServices;
        private List<Claim>? UserProfile;
        private int userId;
        private string? name;
        private int positionId;
        private string? position;
        public UserProfileBusiness(IHttpContextAccessor haccess, LineServices lineServices, WrapperRepository wrapper, RestServices restServices, CryptographyServices cryptographyServices)
        {
            _wrapper = wrapper;
            _dbContext = _wrapper._dbContext;
            _haccess = haccess;
            _restServices = restServices;
            _cryptographyServices = cryptographyServices;
            _lineServices = lineServices;
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
            //obj.profile = new Profileview();
            //var findUser = _dbContext.TbUser.FirstOrDefault(x => x.Id == userId);
            //if (findUser != null)
            //{
            //    obj.profile.name = findUser.Name;
            //    obj.profile.TelNo = findUser.TelNo;
            //    obj.profile.positionName = position;
            //}


            //var noti = _wrapper._dbContext.TbConfigs.Where(x => x.Group == "LineNoti").ToList();
            //if (noti != null)
            //{
            //    var EndpointRegis = noti.FirstOrDefault(x => x.Name == "EndpointRegis")?.Value;
            //    var ClientId = noti.FirstOrDefault(x => x.Name == "ClientId")?.Value;
            //    var ClientSecret = noti.FirstOrDefault(x => x.Name == "ClientSecret")?.Value;

            //    var CallBack = noti.FirstOrDefault(x => x.Name == "CallBack")?.Value;
            //    var ResponseType = noti.FirstOrDefault(x => x.Name == "ResponseType")?.Value;
            //    var Scope = noti.FirstOrDefault(x => x.Name == "Scope")?.Value;
            //    obj.notiSetting = new NotiSetting();

            //    //var findToken = _dbContext.TbUser.FirstOrDefault(x => x.Id == userId)?.NotifyToken;

            //    //obj.notiSetting.token = findToken;
            //    obj.notiSetting.link = $"{EndpointRegis}?response_type={ResponseType}&client_id={ClientId}&redirect_uri={_haccess?.HttpContext?.Request.Scheme}://{_haccess?.HttpContext?.Request.Host}/{CallBack}&scope={Scope}&state={userId}";
            //}
            return obj;
        }

        public async Task<UserProfileModel> UpdateNoto(string code, string state)
        {
            //var noti = _wrapper._dbContext.TbConfigs.Where(x => x.Group == "LineNoti").ToList();
            //if (noti != null)
            //{
            //    var EndpointToken = noti.FirstOrDefault(x => x.Name == "EndpointToken")?.Value;

            //    var ContentType = noti.FirstOrDefault(x => x.Name == "ContentType")?.Value;
            //    var ClientId = noti.FirstOrDefault(x => x.Name == "ClientId")?.Value;
            //    var ClientSecret = noti.FirstOrDefault(x => x.Name == "ClientSecret")?.Value;
            //    var GrantType = noti.FirstOrDefault(x => x.Name == "GrantType")?.Value;
            //    var CallBack = noti.FirstOrDefault(x => x.Name == "CallBack")?.Value;


            //    var getToken = await _restServices.PostAsync<dynamic>(new ParamsAPI
            //    {
            //        Url = EndpointToken,
            //        ContentType = ContentType,
            //        Data2 = new Dictionary<string, string>
            //        {
            //            { "grant_type", GrantType },
            //            { "code", code },
            //            { "client_id", ClientId },
            //            { "client_secret", ClientSecret },
            //            { "redirect_uri", $"{_haccess?.HttpContext?.Request.Scheme}://{_haccess?.HttpContext?.Request.Host}/{CallBack}" },
            //        },
            //    });

            //    if (getToken != null)
            //    {
            //        var status = (int?)getToken?.status;
            //        if (status == 200)
            //        {
            //            var token = (string?)getToken?.access_token;
            //            var findUser = _dbContext.TbUser.FirstOrDefault(x => x.Id == userId);
            //            if (findUser != null)
            //            {
            //                findUser.NotifyToken = token;
            //                _dbContext.TbUser.Update(findUser);
            //                _dbContext.SaveChanges();
            //            }
            //        }
            //    }
            //}
            var data = GetData();
            data.Tab = "noti-tab";
            return data;
        }

        public dynamic updateProfile(Profileview obj)
        {

            //var find = _dbContext.TbUser.FirstOrDefault(x => x.Id == userId);
            //if (find != null)
            //{
            //    find.Name = obj.name;
            //    find.TelNo = obj.TelNo;
            //    _dbContext.TbUser.Update(find);
            //    _dbContext.SaveChanges();
            //}
            return new { result = true, type = "success", message = "ทำรายการสำเร็จ", url = "UserProfile" };
        }


        public dynamic ChangePass(ChangePass obj)
        {
            if (obj.newpass != obj.renewpass)
            {
                return new { result = true, type = "error", message = "รหัสผ่านใหม่ไม่ตรงกัน" };
            }
            //var find = _dbContext.TbUser.FirstOrDefault(x => x.Id == userId && x.Password == obj.oldpass);
            //if (find != null)
            //{
            //    find.Password = _cryptographyServices.GetMd5Hash(obj.newpass);
            //    _dbContext.TbUser.Update(find);
            //    _dbContext.SaveChanges();
            //    return new { result = true, type = "success", message = "ทำรายการสำเร็จ", url = "UserProfile" };
            //}
            //else
            //{
                return new { result = true, type = "error", message = "รหัสผ่านเดิมไม่ถูกต้อง" };
            //}
        }


        public async Task<dynamic> linetest()
        {
            //var noti = _wrapper._dbContext.TbConfigs.Where(x => x.Group == "LineNoti").ToList();
            //if (noti != null)
            //{
            //    var EndpointNoti = noti.FirstOrDefault(x => x.Name == "EndpointNoti")?.Value;
            //    var ContentType = noti.FirstOrDefault(x => x.Name == "ContentType")?.Value;

            //    var find = _dbContext.TbUser.FirstOrDefault(x => x.Id == userId);
            //    if (find != null)
            //    {
            //        _lineServices.SendMessageByToken(new List<string>() { find.NotifyToken }, "ทดสอบ");
            //    }
            //}
            return new { result = true, type = "success", message = "ทดสอบ" };
        }


        public dynamic delline()
        {

            //var find = _dbContext.TbUser.FirstOrDefault(x => x.Id == userId);
            //if (find != null)
            //{
            //    find.NotifyToken = null;
            //    _dbContext.TbUser.Update(find);
            //    _dbContext.SaveChanges();
            //}

            return new { result = true, type = "success", message = "ยกเลิกสำเร็จ", url = "UserProfile" };
        }



    }
}


