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
	public class PurchaseRequisitionBusiness
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
		public PurchaseRequisitionBusiness(IHttpContextAccessor haccess, WrapperRepository wrapper, LineServices lineServices)
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
			else if (action == "ส่งกลับ")
			{
				StatusId = 3;
			}
			else if (action == "ยกเลิก")
			{
				StatusId = 6;
			}
			else if (action == "อนุมัติ")
			{
				StatusId = 4;
			}

			if (action == "บันทึก")
			{
				var lineApprove = GetLineApprove();
				if (lineApprove == null || (lineApprove != null && lineApprove?.approvalLists?.Count() == 0))
				{
					return new { result = true, type = "error", message = "ไม่พบผู้อนุมัติ" };
				}
			}
			if (obj.Id != null && obj.Id != 0)
			{
				var find = _dbContext.TbDocumentTransaction.FirstOrDefault(x => x.Id == obj.Id);
				if (find != null)
				{
					//Update All only 
					List<int> StatusActionForCreator = new List<int>() { 1, 2, 3, 4 };
					if (StatusActionForCreator.Contains(find.StatusId))
					{
						var config = new MapperConfiguration(cfg =>
						cfg.CreateMap<PagePR, TbDocumentTransaction>()
						 .ForMember(dest => dest.RequestDate, opt => opt.Ignore()));
						var mapper = new Mapper(config);
						mapper.Map(obj, find);
						find.StatusId = StatusId;
						if (!string.IsNullOrEmpty(obj.RequestDate))
						{
							find.RequestDate = DateTime.ParseExact(obj.RequestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
						}
						_dbContext.TbDocumentTransaction.Update(find);
						AddOrUpdateFile(find.Id, find.DocumentCode);
						_dbContext.SaveChanges();
					}
					DocId = find.Id;
				}
			}
			else
			{
				var date = DateTime.Now;
				TbDocumentTransaction data = new TbDocumentTransaction();
				var config = new MapperConfiguration(cfg =>
				cfg.CreateMap<PagePR, TbDocumentTransaction>()
				 .ForMember(dest => dest.RequestDate, opt => opt.Ignore()));
				var mapper = new Mapper(config);
				mapper.Map(obj, data);

				data.DocumentCode = _wrapper._storedProcedureRepository.GenarateCode();
				data.CreateBy = userId;
				data.CreateDate = date;
				data.OrderDate = date;
				data.StatusId = StatusId;
				if (!string.IsNullOrEmpty(obj.RequestDate))
				{
					data.RequestDate = DateTime.ParseExact(obj.RequestDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
				}
				_dbContext.TbDocumentTransaction.Add(data);
				_dbContext.SaveChanges();
				AddOrUpdateFile(data.Id, data.DocumentCode);
				DocId = data.Id;


			}
			StampApproval(DocId, obj.Budget.Value, action);
			StampHistory(DocId, action, obj.Reason, 0);
			NotiAction(DocId, action, obj.Reason);

			return new { result = true, type = "success", message = "บันทึกรายการสำเร็จ", url = "Home/MyTask" };
		}
		public void AddOrUpdateFile(int DocId, string DocumentCode)
		{
			var sessionFile = _haccess.HttpContext.Session.GetString("docfile");
			var reqFile = string.IsNullOrEmpty(sessionFile)
				? new List<DocUpload>()
				: JsonConvert.DeserializeObject<List<DocUpload>>(sessionFile);

			var config = _dbContext.TbConfigs.FirstOrDefault(x => x.Name == "PathFile");
			var PathConfig = (config != null) ? config.Value : string.Empty;
			var part = Path.Combine(string.Format(@"{0}\{1}", PathConfig, DocumentCode));
			//remove
			var find = _dbContext.TbDocumentFile.Where(x => x.DocId == DocId).ToList();
			if (find != null)
			{
				foreach (var item in find)
				{
					var del = Path.Combine(string.Format(@"{0}\{1}", PathConfig, item.FileParth));

					if (File.Exists(Path.Combine(del)))
					{
						File.Delete(Path.Combine(del));
					}
					_dbContext.TbDocumentFile.Remove(item);
					_dbContext.SaveChanges();
				}
			}
			//create
			if (reqFile != null && reqFile.Count > 0)
			{
				bool exists = Directory.Exists(part);
				if (!exists)
					Directory.CreateDirectory(part);
				foreach (var item in reqFile)
				{
					using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(item.base64)))
					{
						Path.Combine(string.Format(@"{0}\{1}", part, item.filename));
					}
					byte[] fileBytes = Convert.FromBase64String(item.base64);
					string fullPath = Path.Combine(part, item.filename);
					Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
					File.WriteAllBytes(fullPath, fileBytes);

					var subpart = fullPath.Replace(PathConfig, string.Empty);
					_dbContext.TbDocumentFile.Add(new TbDocumentFile()
					{
						Extension = item.extension,
						CreateBy = userId,
						CreateDate = DateTime.Now,
						ContentType = item.ContentType,
						DocId = DocId,
						FileName = item.filename,
						FileParth = subpart
					});
					_dbContext.SaveChanges();
				}
			}
		}

		public void StampApproval(int DocId, decimal? Budget, string action)
		{
			List<string> status = new List<string>() { "บันทึก", "บันทึกร่าง", "ส่งกลับ" };
			if (status.Contains(action))
			{
				var find = _dbContext.TbApprovalTransaction.Where(x => x.DocId == DocId).ToList();
				if (find != null && find.Count > 0)
				{
					_dbContext.TbApprovalTransaction.RemoveRange(find);
					_dbContext.SaveChanges();
				}

				var sessionFile = _haccess.HttpContext.Session.GetString("ApprovalList");
				var reqFile = string.IsNullOrEmpty(sessionFile)
					? new List<ApprovalList>()
					: JsonConvert.DeserializeObject<List<ApprovalList>>(sessionFile);


				if (reqFile != null && reqFile.Count > 0)
				{
					List<TbApprovalTransaction> list = new List<TbApprovalTransaction>();
					foreach (var item in reqFile)
					{
						list.Add(new TbApprovalTransaction()
						{
							DocId = DocId,
							UserId = item.userId,
							Budget = (item.Budget != null && item.Budget != "-") ? Convert.ToDecimal(item.Budget) : 0,
							PositionId = item.PositionId.Value,
							IsApprove = false,
						});
					}

					if (action == "บันทึก")
					{
						var findApp = _dbContext.TbApprovalMatrix.Where(x => Budget >= x.Budget).ToList();
						var findPo = _dbContext.TbPosition.ToList();
						if (findApp != null)
						{
							foreach (var item in findApp)
							{
								var position = findPo.FirstOrDefault(x => x.Id == item.PositionId);

								if (position != null)
								{
									var checkuser = _dbContext.TbUser.Any(x => x.PositionId == item.PositionId && x.IsApprove);
									if (checkuser)
									{
										list.Add(new TbApprovalTransaction()
										{
											DocId = DocId,
											Budget = item.Budget,
											PositionId = item.PositionId,
											IsApprove = false,
										});
									}
								}
							}
						}
					}
					if (list != null && list.Count > 0)
					{
						_dbContext.TbApprovalTransaction.AddRange(list);
						_dbContext.SaveChanges();
					}
				}
			}
			else if (action == "อนุมัติ")
			{
				var findUser = _dbContext.TbUser.FirstOrDefault(x => x.Id == userId);
				if (findUser != null && findUser.IsManager)
				{
					var find = _dbContext.TbDocumentTransaction.FirstOrDefault(x => x.Id == DocId);
					if (find != null)
					{
						//Update All only 
						List<int> StatusActionForApprover = new List<int>() { 4 };
						if (StatusActionForApprover.Contains(find.StatusId))
						{
							find.StatusId = 5;
							_dbContext.TbDocumentTransaction.Update(find);
							_dbContext.SaveChanges();
						}
					}
				}
				else
				{
					var FindNowApprover = _dbContext.TbApprovalTransaction.Where(x => x.DocId == DocId && !x.IsApprove).OrderBy(o => o.Budget).ToList();
					if (FindNowApprover != null && FindNowApprover.Count > 0)
					{
						var NowApprover = FindNowApprover.FirstOrDefault();
						if (NowApprover.PositionId == positionId)
						{
							NowApprover.IsApprove = true;
							NowApprover.ApproveBy = userId;
							_dbContext.TbApprovalTransaction.Update(NowApprover);
							_dbContext.SaveChanges();
						}
					}
					var find = _dbContext.TbDocumentTransaction.FirstOrDefault(x => x.Id == DocId);
					if (find != null)
					{
						//Update All only 
						List<int> StatusActionForApprover = new List<int>() { 4 };
						if (StatusActionForApprover.Contains(find.StatusId))
						{
							int StatusId = 0;
							var checklastapp = _dbContext.TbApprovalTransaction.Where(x => x.DocId == DocId && !x.IsApprove).ToList();
							if (checklastapp.Count == 0)
							{
								StatusId = 5;
							}
							else
							{
								StatusId = 4;
							}
							//update status only
							find.StatusId = StatusId;
							_dbContext.TbDocumentTransaction.Update(find);
							_dbContext.SaveChanges();
						}
					}
				}
			}
		}
		public void StampHistory(int DocId, string action, string Reason, int StatusId)
		{
			_dbContext.TbHistoryTransaction.Add(new TbHistoryTransaction()
			{
				DocId = DocId,
				UserId = userId,
				PositionId = positionId,
				StatusId = StatusId,
				Action = action,
				Reason = Reason,
				StampDate = DateTime.Now
			});
			_dbContext.SaveChanges();
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
		public dynamic deleteapproval(int id)
		{
			try
			{
				var sessionFile = _haccess.HttpContext.Session.GetString("ApprovalList");
				var reqFile = string.IsNullOrEmpty(sessionFile)
					? new List<ApprovalList>()
					: JsonConvert.DeserializeObject<List<ApprovalList>>(sessionFile);

				reqFile = reqFile.Where(x => x.userId != id).ToList();
				_haccess.HttpContext.Session.SetString("ApprovalList", JsonConvert.SerializeObject(reqFile));
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


		public List<DocUpload> GetDocFile(int? id)
		{
			var sessionFile = _haccess.HttpContext.Session.GetString("docfile");
			var reqFile = string.IsNullOrEmpty(sessionFile)
				? new List<DocUpload>()
				: JsonConvert.DeserializeObject<List<DocUpload>>(sessionFile);
			return reqFile;
		}




		public ApprovalPR GetLineApprove()
		{
			ApprovalPR obj = new ApprovalPR();
			obj.approvalLists = new List<ApprovalList>();
			var sessionFile = _haccess.HttpContext.Session.GetString("ApprovalList");
			var reqFile = string.IsNullOrEmpty(sessionFile)
				? new List<ApprovalList>()
				: JsonConvert.DeserializeObject<List<ApprovalList>>(sessionFile);

			obj.approvalLists = reqFile;
			return obj;
		}
		public ModalShowApproval GetPositionApproval(int? id)
		{
			ModalShowApproval obj = new ModalShowApproval();

			if (id != null)
			{
				obj.approvalDetails = _dbContext.TbUser.Where(x => x.PositionId == id && x.IsApprove)
					.Select(s => new ApprovalDetail() { Name = s.Name, TelNo = s.TelNo })
					.ToList();
			}
			return obj;
		}
		public ModalSelectApproval GetApproval()
		{
			ModalSelectApproval obj = new ModalSelectApproval();


			var sessionFile = _haccess.HttpContext.Session.GetString("ApprovalList");
			var reqFile = string.IsNullOrEmpty(sessionFile)
				? new List<ApprovalList>()
				: JsonConvert.DeserializeObject<List<ApprovalList>>(sessionFile);

			var luserId = reqFile.Select(s => s.userId).ToList();

			obj.approvalDetails = (from user in _dbContext.TbUser
								   join position in _dbContext.TbPosition on user.PositionId equals position.Id
								   where !luserId.Contains(user.Id) && user.Id != userId && user.IsApprove
								   select new ModalSelectApprovalApprovalDetail
								   {
									   id = user.Id,
									   Name = user.Name,
									   TelNo = user.TelNo,
									   PositionName = position.PositionName
								   }).ToList();
			return obj;
		}


		public dynamic SelectRowApproval(int id)
		{
			var sessionFile = _haccess.HttpContext.Session.GetString("ApprovalList");
			var reqFile = string.IsNullOrEmpty(sessionFile)
				? new List<ApprovalList>()
				: JsonConvert.DeserializeObject<List<ApprovalList>>(sessionFile);


			var find = (from user in _dbContext.TbUser
						join position in _dbContext.TbPosition on user.PositionId equals position.Id
						where user.Id == id && user.IsApprove
						select new ModalSelectApprovalApprovalDetail
						{
							id = user.Id,
							Name = user.Name,
							PositionId = position.Id,
							PositionName = position.PositionName
						}).FirstOrDefault();
			if (find != null)
			{
				reqFile.Add(new ApprovalList()
				{
					userId = find.id,
					userName = find.Name,
					PositionId = find.PositionId,
					PositionName = find.PositionName,
				});

			}
			_haccess.HttpContext.Session.SetString("ApprovalList", JsonConvert.SerializeObject(reqFile));
			return new { result = true, type = "success", message = "เพิ่มรายการสำเร็จ" };
		}




		public void NotiAction(int Id, string action, string reason)
		{
			var find = (from doc in _dbContext.TbDocumentTransaction
						join user in _dbContext.TbUser on doc.CreateBy equals user.Id
						join position in _dbContext.TbPosition on user.PositionId equals position.Id
						join status in _dbContext.TbStatus on doc.StatusId equals status.Id
						let pri = _dbContext.TbPriority.FirstOrDefault(x => x.Id == doc.PriorityId)
						let appNext = _dbContext.TbApprovalTransaction.Where(x => x.DocId == doc.Id && !x.IsApprove).OrderBy(o => o.Budget).FirstOrDefault()
						where doc.Id == Id
						select new
						{
							status = status.StatusName,
							subject = doc.Subject,
							Priority = (pri != null) ? pri.PriorityName : string.Empty,
							Description = doc.Description,
							budget = (doc.Budget != null) ? doc.Budget.Value.ToString("N2") : string.Empty,	
							documentNo = doc.DocumentCode,
							createDate = doc.CreateDate,
							createBy = user.Name,
							createByToken = user.NotifyToken,
							positionName = position.PositionName,
							nextPosition = (appNext != null) ? appNext.PositionId : null,
							nextUser = (appNext != null) ? appNext.UserId : null,

						})
						.FirstOrDefault();
			if (find != null)
			{
				var findTokenManager = _dbContext.TbUser.Where(x => x.IsManager && !string.IsNullOrEmpty(x.NotifyToken)).Select(s=>s.NotifyToken).ToList();
				List<string> token = new List<string>();
				string alertMsg = $"แจ้งเตือน\nสถานะ: {find.status}\nความสำคัญ: {find.Priority}\nเลขที่เอกสาร: {find.documentNo}\nหัวเรื่อง: {find.subject}\nงบประมาณ: {find.budget.ToString()}\nวันที่สร้าง: {find.createDate}\nผู้สร้าง: {find.createBy}\nตำแหน่ง: {find.positionName}\nรออนุมัติโดย: appnext\nรายละเอียดเพิ่มเติม: {find.Description}\nการดำเนินงาน: {action}\nหมายเหตุ: {reason}\nเปิดงาน: {_haccess?.HttpContext?.Request.Scheme}://{_haccess?.HttpContext?.Request.Host}/PurchaseRequisition/{Id}";

				if (findTokenManager != null && findTokenManager.Count  > 0)
				{
					token.AddRange(findTokenManager);
				}
				if (!string.IsNullOrEmpty(find.createByToken))
				{
					token.Add(find.createByToken);
				}
				if (action != "ส่งกลับ" && (action != "บันทึกร่าง"))
				{
					if (find.nextUser != null)
					{
						var finUser = _dbContext.TbUser.FirstOrDefault(x => x.Id == find.nextUser);
						if (finUser != null)
						{
							alertMsg = alertMsg.Replace("appnext", finUser.Name);
							if (!string.IsNullOrEmpty(finUser.NotifyToken))
							{
								token.Add(finUser.NotifyToken);
							}
						}
						else
						{
							alertMsg = alertMsg.Replace("appnext", string.Empty);
						}
					}
					else if (find.nextPosition != null)
					{
						var finPosition = _dbContext.TbPosition.FirstOrDefault(x => x.Id == find.nextPosition);
						if (finPosition != null)
						{
							alertMsg = alertMsg.Replace("appnext", finPosition.PositionName);
							var finPositionToken = _dbContext.TbUser.Where(x => x.PositionId == finPosition.Id && x.IsApprove && !string.IsNullOrEmpty(x.NotifyToken)).Select(s => s.NotifyToken).ToList();
							if (finPositionToken != null && finPositionToken.Count > 0)
							{
								token.AddRange(finPositionToken);
							}
						}
						else
						{
							alertMsg = alertMsg.Replace("appnext", string.Empty);
						}
					}
					else
					{
						alertMsg = alertMsg.Replace("appnext", string.Empty);
					}
				}
				else
				{
					alertMsg = alertMsg.Replace("appnext", string.Empty);
				}

				if (token != null && token.Count > 0)
				{
					_lineServices.SendMessageByToken(token, alertMsg);
				}
			}
		}
		public PagePR GetData(int? Id)
		{
			PagePR obj = new PagePR();
			_haccess.HttpContext.Session.Remove("docfile");
			_haccess.HttpContext.Session.Remove("ApprovalList");
			obj.lPriority = _dbContext.TbPriority
			.OrderBy(o => o.Seq)
			.Select(s => new SelectListItem()
			{
				Text = s.PriorityName,
				Value = s.Id.ToString()
			}).ToList();
			obj.lCompany = _dbContext.TbCompany
			.Select(s => new SelectListItem()
			{
				Text = s.CompanyName,
				Value = s.Id.ToString()
			}).ToList();
			obj.lDivision = _dbContext.TbDivision
			.Select(s => new SelectListItem()
			{
				Text = s.DivisionName,
				Value = s.Id.ToString()
			}).ToList();
			obj.lUser = _dbContext.TbUser
			.Select(s => new SelectListItem()
			{
				Text = s.Name,
				Value = s.Id.ToString()
			}).ToList();
			if (Id == null)
			{
				obj.OrderDate = DateTime.Now;
				obj.DocumentCode = "Auto Generate";
				obj.CreateBy = userId;
				obj.PositionId = positionId;
				//
				obj.CreateName = name;
				obj.PositionName = position;
			}
			else
			{
				var find = _dbContext.TbDocumentTransaction.FirstOrDefault(x => x.Id == Id);
				if (find != null)
				{
					var config = new MapperConfiguration(cfg => cfg.CreateMap<TbDocumentTransaction, PagePR>());
					var mapper = new Mapper(config);
					mapper.Map(find, obj);

					var user = _dbContext.TbUser.FirstOrDefault(x => x.Id == find.CreateBy);
					var position = _dbContext.TbPosition.FirstOrDefault(x => x.Id == positionId);
					obj.CreateName = user?.Name;
					obj.PositionName = position?.PositionName;

					if (find.RequestDate != null)
					{
						obj.RequestDate = find.RequestDate.Value.ToString("dd/MM/yyyy");
					}
					// ถ้าเป็น New,Draft,Reject จะต้อง Fetch Line Approve ใหม่เสมอ ยกเว้น Flow นั้น บันทึกไปแล้วเพิ่มรออนุมัติ
					#region Getapproval
					var sessionFile = _haccess.HttpContext.Session.GetString("ApprovalList");
					var reqFile = string.IsNullOrEmpty(sessionFile)
						? new List<ApprovalList>()
						: JsonConvert.DeserializeObject<List<ApprovalList>>(sessionFile);
					List<int> StatusActionForCreator = new List<int>() { 1, 2, 3 };
					if (StatusActionForCreator.Contains(find.StatusId))
					{
						var approval = _dbContext.TbApprovalTransaction.Where(x => x.DocId == Id).ToList();
						if (approval != null)
						{
							foreach (var item in approval)
							{
								var findUserapp = (from fuser in _dbContext.TbUser
												   join fposition in _dbContext.TbPosition on fuser.PositionId equals fposition.Id
												   where fuser.Id == item.UserId
												   select new ModalSelectApprovalApprovalDetail
												   {
													   id = fuser.Id,
													   Name = fuser.Name,
													   TelNo = fuser.TelNo,
													   PositionName = fposition.PositionName
												   }).FirstOrDefault();
								if (findUserapp != null)
								{
									reqFile.Add(new ApprovalList()
									{
										Budget = item.Budget.ToString(),
										IsApproved = item.IsApprove,
										PositionId = item.PositionId,
										PositionName = findUserapp.PositionName,
										userId = item.UserId,
										userName = findUserapp.Name
									});
								}
							}
						}
					}
					else
					{
						var approval = _dbContext.TbApprovalTransaction.Where(x => x.DocId == Id).ToList();
						if (approval != null)
						{
							foreach (var item in approval)
							{
								var findUserapp = (from fuser in _dbContext.TbUser
												   join fposition in _dbContext.TbPosition on fuser.PositionId equals fposition.Id
												   where fuser.Id == item.UserId
												   select new ModalSelectApprovalApprovalDetail
												   {
													   id = fuser.Id,
													   Name = fuser.Name,
													   TelNo = fuser.TelNo,
													   PositionName = fposition.PositionName
												   }).FirstOrDefault();
								if (findUserapp != null)
								{
									reqFile.Add(new ApprovalList()
									{
										Budget = item.Budget.ToString(),
										IsApproved = item.IsApprove,
										PositionId = item.PositionId,
										PositionName = findUserapp.PositionName,
										userId = item.UserId,
										userName = findUserapp.Name
									});
								}
								else
								{
									var findPo = _dbContext.TbPosition.FirstOrDefault(x => x.Id == item.PositionId);
									if (findPo != null)
									{
										reqFile.Add(new ApprovalList()
										{
											Budget = item.Budget.ToString(),
											IsApproved = item.IsApprove,
											PositionId = item.PositionId,
											PositionName = findPo.PositionName,
										});
									}
								}
							}
						}
					}
					if (reqFile != null && reqFile.Count > 0)
					{
						obj.ApprovalPR = new ApprovalPR();
						obj.ApprovalPR.approvalLists = reqFile;
						_haccess.HttpContext.Session.SetString("ApprovalList", JsonConvert.SerializeObject(reqFile));
					}
					#endregion





					//GetDocFile
					var fildata = GetDocFile(Id);
					var file = _dbContext.TbDocumentFile.Where(x => x.DocId == Id).OrderBy(o => o.CreateDate).ToList();
					if (file != null)
					{
						var configp = _dbContext.TbConfigs.FirstOrDefault(x => x.Name == "PathFile");
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

