using QuickVisualWebWood.Core.dbModels;
using QuickVisualWebWood.Core.pageModels.ManagePosition;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace QuickVisualWebWood.Core.pageModels.ManageUser
{
    public class PageManageUser
    {
        public List<DataUser>? tbUser { get; set; }
        public PageManageUserSave? pageManageUserSave { get; set; }
    }

    public class DataUser : TbUser 
    {
        public string? PositionName { get; set; }
        public string? RoleName { get; set; }
        public bool IsCanDelete { get; set; }
    }
    public class PageManageUserSave
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "ระบุชื่อผู้ใช้งาน")]
        public string? UserName { get; set; }
        //[Required(ErrorMessage = "ระบุชื่อรหัสผ่าน")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "ระบุชื่อ")]
        public string? Name { get; set; }
        public string? TelNo { get; set; }
        [Required(ErrorMessage = "ระบุตำแหน่ง")]
        public int? PositionId { get; set; }
        public bool IsApprove { get; set; } = false;
        public bool IsManager { get; set; } = false;
        public bool IsActive { get; set; } = true;
        [Required(ErrorMessage = "ระบุสิทธิ์การใช้งาน")]
        public int? RoleId { get; set; }



        public List<SelectListItem>? lPosition { get; set; }
        public List<SelectListItem>? lRole { get; set; }
    }
}
