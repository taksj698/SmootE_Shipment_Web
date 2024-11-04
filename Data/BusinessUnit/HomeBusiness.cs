using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using AutoMapper.Features;
using SmootE_Shipment_Web.Configs.Extensions;
using SmootE_Shipment_Web.Core.comModels;
using SmootE_Shipment_Web.Core.dbModels;
using SmootE_Shipment_Web.Core.pageModels.PurchaseRequisition;
using SmootE_Shipment_Web.Data.Repository;
using SmootE_Shipment_Web.Data.Repository.SQLServer;
using SmootE_Shipment_Web.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using WebGrease.Activities;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using SmootE_Shipment_Web.Core.pageModels.Home;
using Microsoft.IdentityModel.Tokens;
using SmootE_Shipment_Web.Core.serviceModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SmootE_Shipment_Web.Data.BusinessUnit
{
	public class HomeBusiness
	{
		private readonly WrapperRepository _wrapper;
		private SqlServerDbContext _dbContext;
		private IHttpContextAccessor _haccess;
		private readonly LineServices _lineServices;
		private readonly RisoServices _risoServices;
		private readonly SqlServerDbContext2 _dbContext2;


		private List<Claim>? UserProfile;
		private int userId;
		private string? name;
		private int positionId;
		private string? position;
		public HomeBusiness(IHttpContextAccessor haccess, WrapperRepository wrapper, LineServices lineServices, RisoServices risoServices, SqlServerDbContext2 dbContext2)
		{
			_wrapper = wrapper;
			_dbContext = _wrapper._dbContext;
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
		public dynamic Cancel(string id)
		{
			return new { result = true, type = "success", message = "บันทึกรายการสำเร็จ", url = "Home/MyTask" };
		}





	}
}

