
using System.Security.Claims;
using System.Text;
using AutoMapper;
using SmootE_Shipment_Web.Core.dbModels;
using SmootE_Shipment_Web.Core.pageModels.PurchaseRequisition;
using SmootE_Shipment_Web.Data.Repository;
using SmootE_Shipment_Web.Data.Repository.SQLServer;
using SmootE_Shipment_Web.Data.Services;
using Newtonsoft.Json;
using SmootE_Shipment_Web.Core.serviceModels;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SmootE_Shipment_Web.Core.pageModels.CheckLoad;

namespace SmootE_Shipment_Web.Data.BusinessUnit
{
    public class CheckLoadBusiness
    {
        private readonly WrapperRepository _wrapper;
        private IHttpContextAccessor _haccess;
        private readonly LineServices _lineServices;
        private readonly RisoServices _risoServices;
        private readonly SqlServerDbContext2 _dbContext2;

        private List<Claim>? UserProfile;
        private int userId;
        private string? name;
        private int positionId;
        private string? position;
        public CheckLoadBusiness(IHttpContextAccessor haccess, WrapperRepository wrapper, LineServices lineServices, RisoServices risoServices, SqlServerDbContext2 dbContext2)
        {
            _wrapper = wrapper;
            _haccess = haccess;
            _lineServices = lineServices;
            _risoServices = risoServices;
            _dbContext2 = dbContext2;

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



        public PageCheckLoad GetDataById(int Id)
        {
            PageCheckLoad data = new PageCheckLoad();

            data.pageCheckLoadTop = new PageCheckLoadTop();
            //data.pageCheckLoadData = new List<PageCheckLoadData>();

            data.pageCheckLoadData = _wrapper._dbContext.TB_PackingDetails
                .GroupBy(g => new
                {
                    ctnNo = g.CTN,
                    dimension = g.Dimension,
                    casePack = g.CasePacking,
                    cartons = g.Cartons
                })
                .Select(group => new PageCheckLoadData
                {
                    ctnNo = group.Key.ctnNo,
                    dimension = group.Key.dimension,
                    casePack = group.Key.casePack,
                    cartons = group.Key.cartons
                })
                .ToList();



            if (data.pageCheckLoadData != null && data.pageCheckLoadData.Any())
            {
                var allPackingDetails = _wrapper._dbContext.TB_PackingDetails
                    .Where(pd => data.pageCheckLoadData.Select(p => p.ctnNo).Contains(pd.CTN))
                    .ToList();

                foreach (var item in data.pageCheckLoadData)
                {
                    item.pageCheckLoadItems = allPackingDetails
                        .Where(pd => pd.CTN == item.ctnNo)
                        .GroupBy(g => new
                        {
                            g.Description,
                            g.Barcode,
                            g.Lot,
                            g.PackingDate
                        })
                        .Select(group => new PageCheckLoadItem
                        {
                            description = group.Key.Description,
                            barcode = group.Key.Barcode,
                            lot = group.Key.Lot,
                            dateTime = group.Key.PackingDate,
                            status = "-"
                        })
                        .ToList();
                }
            }

            return data;
        }






    }
}

