using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using AutoMapper.Features;
using QuickVisualWebWood.Configs.Extensions;
using QuickVisualWebWood.Core.comModels;
using QuickVisualWebWood.Core.dbModels;
using QuickVisualWebWood.Core.pageModels.PurchaseRequisition;
using QuickVisualWebWood.Data.Repository;
using QuickVisualWebWood.Data.Repository.SQLServer;
using QuickVisualWebWood.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using WebGrease.Activities;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuickVisualWebWood.Data.BusinessUnit
{
	public class EstimateBusiness
	{
		private readonly WrapperRepository _wrapper;
		private SqlServerDbContext _dbContext;
		private IHttpContextAccessor _haccess;
		private readonly LineServices _lineServices;

		private List<Claim>? UserProfile;
		private int userId;
		private string? name;
		private int positionId;
		private string? position;
		public EstimateBusiness(IHttpContextAccessor haccess, WrapperRepository wrapper, LineServices lineServices)
		{
			_wrapper = wrapper;
			_dbContext = _wrapper._dbContext;
			_haccess = haccess;
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

		public dynamic AddorUpdate(PagePR obj, string action)
		{
			int DocId = 0;
			int StatusId = 0;

			if (action == "บันทึกร่าง")
			{
				StatusId = 2;
			}
			else if (action == "บันทึก")
			{
				StatusId = 4;
			}


			if (action == "บันทึก")
			{

			}
			//if (obj.Id != null && obj.Id != 0)
			//{
			//	var find = _dbContext.TbDocumentTransaction.FirstOrDefault(x => x.Id == obj.Id);
			//	if (find != null)
			//	{
			//		//Update All only 
			//		List<int> StatusActionForCreator = new List<int>() { 1, 2, 3, 4 };
			//		if (StatusActionForCreator.Contains(find.StatusId))
			//		{
			//			var config = new MapperConfiguration(cfg =>
			//			cfg.CreateMap<PagePR, TbDocumentTransaction>()
			//			 .ForMember(dest => dest.RequestDate, opt => opt.Ignore()));
			//			var mapper = new Mapper(config);
			//			mapper.Map(obj, find);
			//			find.StatusId = StatusId;
			//			if (!string.IsNullOrEmpty(obj.RequestDate))
			//			{
			//				find.RequestDate = DateTime.ParseExact(obj.RequestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
			//			}
			//			_dbContext.TbDocumentTransaction.Update(find);
			//			AddOrUpdateFile(find.Id, find.DocumentCode);
			//			_dbContext.SaveChanges();
			//		}
			//		DocId = find.Id;
			//	}
			//}
			//else
			//{
			//	var date = DateTime.Now;
			//	TbDocumentTransaction data = new TbDocumentTransaction();
			//	var config = new MapperConfiguration(cfg =>
			//	cfg.CreateMap<PagePR, TbDocumentTransaction>()
			//	 .ForMember(dest => dest.RequestDate, opt => opt.Ignore()));
			//	var mapper = new Mapper(config);
			//	mapper.Map(obj, data);

			//	data.DocumentCode = _wrapper._storedProcedureRepository.GenarateCode();
			//	data.CreateBy = userId;
			//	data.CreateDate = date;
			//	data.OrderDate = date;
			//	data.StatusId = StatusId;
			//	if (!string.IsNullOrEmpty(obj.RequestDate))
			//	{
			//		data.RequestDate = DateTime.ParseExact(obj.RequestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
			//	}
			//	_dbContext.TbDocumentTransaction.Add(data);
			//	_dbContext.SaveChanges();
			//	AddOrUpdateFile(data.Id, data.DocumentCode);
			//	DocId = data.Id;


			//}


			return new { result = true, type = "success", message = "บันทึกรายการสำเร็จ", url = "Home/MyTask" };
		}
		public void AddOrUpdateFile(int DocId, string DocumentCode)
		{
			//var sessionFile = _haccess.HttpContext.Session.GetString("docfile");
			//var reqFile = string.IsNullOrEmpty(sessionFile)
			//	? new List<DocUpload>()
			//	: JsonConvert.DeserializeObject<List<DocUpload>>(sessionFile);

			//var config = _dbContext.TbConfigs.FirstOrDefault(x => x.Name == "PathFile");
			//var PathConfig = (config != null) ? config.Value : string.Empty;
			//var part = Path.Combine(string.Format(@"{0}\{1}", PathConfig, DocumentCode));
			////remove
			//var find = _dbContext.TbDocumentFile.Where(x => x.DocId == DocId).ToList();
			//if (find != null)
			//{
			//	foreach (var item in find)
			//	{
			//		var del = Path.Combine(string.Format(@"{0}\{1}", PathConfig, item.FileParth));

			//		if (File.Exists(Path.Combine(del)))
			//		{
			//			File.Delete(Path.Combine(del));
			//		}
			//		_dbContext.TbDocumentFile.Remove(item);
			//		_dbContext.SaveChanges();
			//	}
			//}
			////create
			//if (reqFile != null && reqFile.Count > 0)
			//{
			//	bool exists = Directory.Exists(part);
			//	if (!exists)
			//		Directory.CreateDirectory(part);
			//	foreach (var item in reqFile)
			//	{
			//		using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(item.base64)))
			//		{
			//			Path.Combine(string.Format(@"{0}\{1}", part, item.filename));
			//		}
			//		byte[] fileBytes = Convert.FromBase64String(item.base64);
			//		string fullPath = Path.Combine(part, item.filename);
			//		Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
			//		File.WriteAllBytes(fullPath, fileBytes);

			//		var subpart = fullPath.Replace(PathConfig, string.Empty);
			//		_dbContext.TbDocumentFile.Add(new TbDocumentFile()
			//		{
			//			Extension = item.extension,
			//			CreateBy = userId,
			//			CreateDate = DateTime.Now,
			//			ContentType = item.ContentType,
			//			DocId = DocId,
			//			FileName = item.filename,
			//			FileParth = subpart
			//		});
			//		_dbContext.SaveChanges();
			//	}
			//}
		}


		public async Task<dynamic> UploadDoc(IFormFile file)
		{
			if (file == null || file.Length == 0)
			{
				return new { result = true, type = "error", message = "ไม่พบไฟล์" };
			}
			try
			{
				var sessionFile = _haccess.HttpContext.Session.GetString("docfile");
				var reqFile = string.IsNullOrEmpty(sessionFile)
					? new List<DocUpload>()
					: JsonConvert.DeserializeObject<List<DocUpload>>(sessionFile);

				if (reqFile.Any(x => x.filename.Equals(file.FileName)))
				{
					return new { result = true, type = "error", message = "ไฟล์ซ้ำกัน" };
				}

				byte[] fileBytes;
				using (var stream = new MemoryStream())
				{
					await file.CopyToAsync(stream);
					fileBytes = stream.ToArray();
				}
				reqFile.Add(new DocUpload
				{
					id = Guid.NewGuid().ToString(),
					filename = file.FileName,
					extension = Path.GetExtension(file.FileName),
					base64 = Convert.ToBase64String(fileBytes),
					ContentType = file.ContentType
				});

				_haccess.HttpContext.Session.SetString("docfile", JsonConvert.SerializeObject(reqFile));

				return new { result = true, type = "success", message = "อัพโหลดรายการสำเร็จ" };
			}
			catch (Exception ex)
			{
				return new { result = true, type = "error", message = ex.Message };
			}
		}


		public dynamic deletefile(string id)
		{
			try
			{
				var sessionFile = _haccess.HttpContext.Session.GetString("docfile");
				var reqFile = string.IsNullOrEmpty(sessionFile)
					? new List<DocUpload>()
					: JsonConvert.DeserializeObject<List<DocUpload>>(sessionFile);

				reqFile = reqFile.Where(x => x.id != id).ToList();
				_haccess.HttpContext.Session.SetString("docfile", JsonConvert.SerializeObject(reqFile));
				return new { result = true, type = "success", message = "ลบรายการสำเร็จ" };
			}
			catch (Exception ex)
			{
				return new
				{
					result = true,
					type = "error",
					message = ex.Message
				};
			}
		}



		public List<DocUpload> GetDocFile()
		{
			var sessionFile = _haccess.HttpContext.Session.GetString("docfile");
			var reqFile = string.IsNullOrEmpty(sessionFile)
				? new List<DocUpload>()
				: JsonConvert.DeserializeObject<List<DocUpload>>(sessionFile);
			return reqFile;
		}







		public PagePR GetData(string? Id)
		{
			PagePR obj = new PagePR();
			_haccess.HttpContext.Session.Remove("docfile");
			_haccess.HttpContext.Session.Remove("ApprovalList");

			if (Id == null)
			{
              
                obj.QualityDate = DateTime.Now;
			}
			else
			{
				var find = _dbContext.TB_QualityTransaction.FirstOrDefault(x => x.SequenceID == Id);
				if (find != null)
				{
					var findweight = _dbContext.TB_WeightData.FirstOrDefault(x => x.SequenceID == Id);
					if (findweight != null) 
					{
                        obj.SequenceID = findweight.SequenceID;
						obj.Plate = findweight.Plate1;
						obj.CustomerName = findweight.CustomerID;
                    }
                    obj.QualityDate = find.QualityDate;

					if (find.Quality1 != null && find.Quality1.Value)
					{
						obj.SelectedOption = "1";
					}
					else if (find.Quality2 != null && find.Quality2.Value)
					{
                        obj.SelectedOption = "2";
                    }
                    else if (find.Quality3 != null && find.Quality3.Value)
                    {
                        obj.SelectedOption = "3";
                    }
                    else if (find.Quality4 != null && find.Quality4.Value)
                    {
                        obj.SelectedOption = "4";
                    }
                    obj.Description = find.Description;
                    //GetDocFile
                    var fildata = GetDocFile();
					var file = _dbContext.TB_DocumentFile.Where(x => x.SequenceID == Id).OrderBy(o => o.CreateDate).ToList();
					if (file != null)
					{
						var configp = _dbContext.TB_VisualConfigs.FirstOrDefault(x => x.Name == "PathPicture");
						var PathConfig = (configp != null) ? configp.Value : string.Empty;
						foreach (var item in file)
						{
							var fullpart = Path.Combine(string.Format(@"{0}\{1}", PathConfig, item.FileParth));
							if (File.Exists(fullpart))
							{
								MemoryStream destination = new MemoryStream();
								using (FileStream source = File.Open(fullpart, FileMode.Open))
								{
									source.CopyTo(destination);
									fildata.Add(new DocUpload()
									{
										base64 = Convert.ToBase64String(destination.ToArray()),
										ContentType = item.ContentType,
										filename = item.FileName,
										extension = item.Extension,
										id = Guid.NewGuid().ToString(),
									});
								}
							}
						}
						_haccess.HttpContext.Session.SetString("docfile", JsonConvert.SerializeObject(fildata));
					}
					obj.DocUpload = fildata;
				}
			}
			return obj;
		}
	}
}

