﻿
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

namespace SmootE_Shipment_Web.Data.BusinessUnit
{
    public class EstimateBusiness
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
        public EstimateBusiness(IHttpContextAccessor haccess, WrapperRepository wrapper, LineServices lineServices, RisoServices risoServices, SqlServerDbContext2 dbContext2)
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
        public dynamic AddorUpdate(PagePR obj, string action)
        {
            //string SequenceID = string.Empty;
            //var wd = _dbContext.TB_WeightData.FirstOrDefault(x => x.SequenceID == obj.SequenceID);
            //var find = _dbContext.TB_QualityTransaction.FirstOrDefault(x => x.SequenceID == obj.SequenceID);
            //if (find != null)
            //{
            //    find.SequenceID = obj.SequenceID;
            //    find.QualityDate = DateTime.Now;
            //    find.Plate = wd?.Plate1;
            //    find.QueueNo = wd?.QueueNo;

            //    //
            //    find.Quality1 = false;
            //    find.Quality2 = false;
            //    find.Quality3 = false;
            //    find.Quality4 = false;

            //    if (obj.SelectedOption == "1")
            //    {
            //        find.Quality1 = true;
            //        find.ResultText = "ผ่าน";
            //    }
            //    else if (obj.SelectedOption == "2")
            //    {
            //        find.Quality2 = true;
            //        find.ResultText = "ไม่ผ่าน";
            //    }
            //    else if (obj.SelectedOption == "3")
            //    {
            //        find.Quality3 = true;
            //        find.ResultText = "อื่นๆ";
            //    }
            //    else if (obj.SelectedOption == "4")
            //    {
            //        find.Quality4 = true;
            //        find.ResultText = "ตัดราคา";
            //    }
            //    find.Description = obj.Description;
            //    find.Status = action;
            //    find.Inactive = false;
            //    find.ModifyDate = DateTime.Now;
            //    find.ModifyBy = name;

            //    _dbContext.TB_QualityTransaction.Update(find);
            //    _dbContext.SaveChanges();
            //    AddOrUpdateFile(find.SequenceID);
            //    SequenceID = find.SequenceID;
            //}
            //else
            //{
            //    TB_QualityTransaction data = new TB_QualityTransaction();
            //    data.SequenceID = obj.SequenceID;
            //    data.QualityDate = DateTime.Now;
            //    data.Plate = wd?.Plate1;
            //    data.QueueNo = wd?.QueueNo;
            //    //
            //    data.Quality1 = false;
            //    data.Quality2 = false;
            //    data.Quality3 = false;
            //    data.Quality4 = false;

            //    if (obj.SelectedOption == "1")
            //    {
            //        data.Quality1 = true;
            //        data.ResultText = "ผ่าน";
            //    }
            //    else if (obj.SelectedOption == "2")
            //    {
            //        data.Quality2 = true;
            //        data.ResultText = "ไม่ผ่าน";
            //    }
            //    else if (obj.SelectedOption == "3")
            //    {
            //        data.Quality3 = true;
            //        data.ResultText = "อื่นๆ";
            //    }
            //    else if (obj.SelectedOption == "4")
            //    {
            //        data.Quality4 = true;
            //        data.ResultText = "ตัดราคา";
            //    }
            //    data.Description = obj.Description;
            //    data.Status = action;
            //    data.Inactive = false;
            //    data.CreateDate = DateTime.Now;
            //    data.CreateBy = name;

            //    _dbContext.TB_QualityTransaction.Add(data);
            //    _dbContext.SaveChanges();
            //    AddOrUpdateFile(data.SequenceID);
            //    SequenceID = data.SequenceID;
            //}

            //if (action == "บันทึกร่าง")
            //{
            //    wd.QualityState = false;
            //    wd.QualityByName = name;
            //}
            //else if (action == "บันทึก")
            //{
            //    wd.QualityState = true;
            //    wd.QualityByName = name;
            //}
            //_risoServices.updateWeightData(SequenceID, wd.QualityState.Value, wd.QualityByName);





            //_dbContext.TB_WeightData.Update(wd);
            //_dbContext.SaveChanges();
            //NotiAction(SequenceID, action, obj.Description);
            //UpdateThirdParty(SequenceID, action);
            return new { result = true, type = "success", message = "บันทึกรายการสำเร็จ", url = "Home/MyTask" };
        }


        public string ResizeImage(string base64Image, int newWidth, int newHeight)
        {
            // แปลง Base64 เป็นภาพ
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            using (var ms = new MemoryStream(imageBytes))
            {
                using (var image = Image.Load(ms))
                {
                    // เปลี่ยนขนาดภาพ
                    image.Mutate(x => x.Resize(newWidth, newHeight));
                    using (var msResized = new MemoryStream())
                    {
                        // บันทึกภาพ resized เป็น Base64
                        image.SaveAsPng(msResized); // หรือ SaveAsJpeg(), SaveAsGif() ตามต้องการ
                        return Convert.ToBase64String(msResized.ToArray());
                    }
                }
            }
        }
        public void UpdateThirdParty(string SequenceID, string action)
        {
            //if (action == "บันทึก")
            //{
            //    var QtyTrans = _dbContext.TB_QualityTransaction.Where(x => x.SequenceID == SequenceID).FirstOrDefault();
            //    var DocFile = _dbContext.TB_DocumentFile.Where(x => x.SequenceID == SequenceID).ToList();

            //    if (QtyTrans != null)
            //    {
            //        TBQualityTransaction data = new TBQualityTransaction();
            //        var config = new MapperConfiguration(cfg =>
            //        {
            //            cfg.CreateMap<TB_QualityTransaction, TBQualityTransaction>()
            //               .ForMember(dest => dest.id, opt => opt.Ignore());
            //        });
            //        var mapper = new Mapper(config);
            //        mapper.Map(QtyTrans, data);

            //        var rs = _risoServices.DeleteTbQualityTransaction(SequenceID);
            //        var result = _risoServices.TbQualityTransaction(data);
            //    }
            //    if (DocFile != null)
            //    {
            //        var rs = _risoServices.DeleteTbDocumentFile(SequenceID);

            //        var sessionFile = _haccess.HttpContext.Session.GetString("docfile");
            //        var reqFile = string.IsNullOrEmpty(sessionFile)
            //            ? new List<DocUpload>()
            //            : JsonConvert.DeserializeObject<List<DocUpload>>(sessionFile);


            //        foreach (var item in DocFile)
            //        {
            //            TbDocumentFile data = new TbDocumentFile();
            //            var config = new MapperConfiguration(cfg =>
            //            {
            //                cfg.CreateMap<TB_DocumentFile, TbDocumentFile>()
            //                   .ForMember(dest => dest.id, opt => opt.Ignore());
            //            });
            //            var mapper = new Mapper(config);
            //            mapper.Map(item, data);
            //            var result = _risoServices.TbDocumentFile(data);

            //            if (reqFile != null && reqFile.Count > 0)
            //            {
            //                var find = reqFile.FirstOrDefault(x => x.filename == item.FileName);
            //                if (find != null)
            //                {
            //                    var result2 = _risoServices.SaveFile(find.base64, find.ContentType, find.filename, item.FileParth.Replace(find.filename, ""));
            //                }
            //            }
            //        }
            //    }
            //}
        }
        public void NotiAction(string SequenceID, string action, string reason)
        {
            //var find = (from weightData in _dbContext.TB_WeightData
            //            join qu in _dbContext.TB_QualityTransaction on weightData.SequenceID equals qu.SequenceID into quGroup
            //            from qu in quGroup.DefaultIfEmpty()
            //            let customer = _dbContext.TB_Customers.FirstOrDefault(x => x.CustomerID == weightData.CustomerID)
            //            where weightData.SequenceID == SequenceID
            //            select new
            //            {
            //                WeighNumber = weightData.TicketCodeIn,
            //                SequenceID = weightData.SequenceID,
            //                Plate = weightData.Plate1,
            //                CustomerName = (customer != null && !string.IsNullOrEmpty(customer.CustomerName)) ? customer.CustomerName : "-",
            //                TransctionDate = (qu != null && qu.QualityDate != null) ? qu.QualityDate.Value.ToString("dd/MM/yyyy") : "-",
            //                EvaluationResults = (qu != null && !string.IsNullOrEmpty(qu.ResultText)) ? qu.ResultText : "-",
            //                Status = (qu != null && !string.IsNullOrEmpty(qu.Status)) ? qu.Status : "-",
            //                Remark = (qu != null && !string.IsNullOrEmpty(qu.Description)) ? qu.Description : "-",
            //            }).FirstOrDefault();
            //if (find != null)
            //{
            //    var findToken = _dbContext.TB_VisualConfigs.FirstOrDefault(x => x.Name == "LineToken");

            //    string alertMsg = $"แจ้งเตือน\nสถานะ: {find.Status}\nทะเบียนรถ: {find.Plate}\nชื่อลูกค้า: {find.CustomerName}\nวันที่ทำรายการ: {find.TransctionDate}\nผลการประเมิน {find.EvaluationResults}\nสถานะ: {find.Status}\nหมายเหตุ: {find.Remark}";
            //    if (findToken != null && !string.IsNullOrEmpty(findToken.Value))
            //    {
            //        _lineServices.SendMessageByToken(new List<string>() { findToken.Value.Trim() }, alertMsg);


            //        if (action == "บันทึก")
            //        {
            //            var sessionFile = _haccess.HttpContext.Session.GetString("docfile");
            //            var reqFile = string.IsNullOrEmpty(sessionFile)
            //                ? new List<DocUpload>()
            //                : JsonConvert.DeserializeObject<List<DocUpload>>(sessionFile);
            //            if (reqFile != null)
            //            {
            //                foreach (var item in reqFile)
            //                {
            //                    var base64 = ResizeImage(item.base64, 800, 600);
            //                    _lineServices.LineImageNoti(new List<string>() { findToken.Value.Trim() }, find.CustomerName, base64, item.filename);
            //                }
            //            }
            //        }
            //    }
            //}
        }
        public void AddOrUpdateFile(string SequenceID)
        {
            //var sessionFile = _haccess.HttpContext.Session.GetString("docfile");
            //var reqFile = string.IsNullOrEmpty(sessionFile)
            //    ? new List<DocUpload>()
            //    : JsonConvert.DeserializeObject<List<DocUpload>>(sessionFile);

            //var config = _dbContext.TB_VisualConfigs.FirstOrDefault(x => x.Name == "PathPicture");
            //var PathConfig = (config != null) ? config.Value : string.Empty;
            //var part = Path.Combine(string.Format(@"{0}\{1}", PathConfig, SequenceID));
            ////remove
            //var find = _dbContext.TB_DocumentFile.Where(x => x.SequenceID == SequenceID).ToList();
            //if (find != null)
            //{
            //    foreach (var item in find)
            //    {
            //        var del = Path.Combine(string.Format(@"{0}\{1}", PathConfig, item.FileParth));
            //        if (File.Exists(Path.Combine(del)))
            //        {
            //            File.Delete(Path.Combine(del));
            //        }
            //        _dbContext.TB_DocumentFile.Remove(item);
            //        _dbContext.SaveChanges();
            //    }
            //}
            ////create
            //if (reqFile != null && reqFile.Count > 0)
            //{
            //    bool exists = Directory.Exists(part);
            //    if (!exists)
            //        Directory.CreateDirectory(part);
            //    foreach (var item in reqFile)
            //    {
            //        using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(item.base64)))
            //        {
            //            Path.Combine(string.Format(@"{0}\{1}", part, item.filename));
            //        }
            //        byte[] fileBytes = Convert.FromBase64String(item.base64);
            //        string fullPath = Path.Combine(part, item.filename);
            //        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            //        File.WriteAllBytes(fullPath, fileBytes);

            //        var subpart = fullPath.Replace(PathConfig, string.Empty);


            //        var file = new TB_DocumentFile()
            //        {
            //            Extension = item.extension,
            //            CreateBy = userId.ToString(),
            //            CreateDate = DateTime.Now,
            //            ContentType = item.ContentType,
            //            SequenceID = SequenceID,
            //            FileName = item.filename,
            //            FileParth = subpart
            //        };

            //        _dbContext.TB_DocumentFile.Add(file);
            //        _dbContext.SaveChanges();

            //    }
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
                string extension = Path.GetExtension(file.FileName);
                string newFileName = $"Doc_{DateTime.Now.ToString("dd_MM_yyyy_HHmmssfff")}{extension}";
                reqFile.Add(new DocUpload
                {
                    id = Guid.NewGuid().ToString(),
                    filename = newFileName,
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
            //_haccess.HttpContext.Session.Remove("docfile");
            //_haccess.HttpContext.Session.Remove("ApprovalList");
            //if (Id == null)
            //{
            //    obj.QualityDate = DateTime.Now;
            //}
            //else
            //{
            //    var findweight = _dbContext.TB_WeightData.FirstOrDefault(x => x.SequenceID == Id);
            //    if (findweight != null)
            //    {
            //        obj.QueueNo = findweight.QueueNo;
            //        obj.QualityDate = DateTime.Now;
            //        obj.SequenceID = findweight.SequenceID;
            //        obj.Plate = findweight.Plate1;
            //        var customer = _dbContext.TB_Customers.FirstOrDefault(x => x.CustomerID == findweight.CustomerID);
            //        obj.CustomerName = (customer != null) ? customer.CustomerName : "-";
            //    }
            //    var find = _dbContext.TB_QualityTransaction.FirstOrDefault(x => x.SequenceID == Id);
            //    if (find != null)
            //    {
            //        obj.QualityDate = find.QualityDate;
            //        if (find.Quality1 != null && find.Quality1.Value)
            //        {
            //            obj.SelectedOption = "1";
            //        }
            //        else if (find.Quality2 != null && find.Quality2.Value)
            //        {
            //            obj.SelectedOption = "2";
            //        }
            //        else if (find.Quality3 != null && find.Quality3.Value)
            //        {
            //            obj.SelectedOption = "3";
            //        }
            //        else if (find.Quality4 != null && find.Quality4.Value)
            //        {
            //            obj.SelectedOption = "4";
            //        }
            //        obj.Description = find.Description;
            //        //GetDocFile
            //        var fildata = GetDocFile();
            //        var file = _dbContext.TB_DocumentFile.Where(x => x.SequenceID == Id).OrderBy(o => o.CreateDate).ToList();
            //        if (file != null)
            //        {
            //            var configp = _dbContext.TB_VisualConfigs.FirstOrDefault(x => x.Name == "PathPicture");
            //            var PathConfig = (configp != null) ? configp.Value : string.Empty;
            //            foreach (var item in file)
            //            {
            //                var fullpart = Path.Combine(string.Format(@"{0}\{1}", PathConfig, item.FileParth));
            //                if (File.Exists(fullpart))
            //                {
            //                    MemoryStream destination = new MemoryStream();
            //                    using (FileStream source = File.Open(fullpart, FileMode.Open))
            //                    {
            //                        source.CopyTo(destination);
            //                        fildata.Add(new DocUpload()
            //                        {
            //                            base64 = Convert.ToBase64String(destination.ToArray()),
            //                            ContentType = item.ContentType,
            //                            filename = item.FileName,
            //                            extension = item.Extension,
            //                            id = Guid.NewGuid().ToString(),
            //                        });
            //                    }
            //                }
            //            }
            //            _haccess.HttpContext.Session.SetString("docfile", JsonConvert.SerializeObject(fildata));
            //        }
            //        obj.DocUpload = fildata;
            //    }
            //}
            return obj;
        }
    }
}

