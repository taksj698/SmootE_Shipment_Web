using QuickVisualWebWood.Core.dbModels;
using QuickVisualWebWood.Core.pageModels.ManageDivision;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace QuickVisualWebWood.Core.pageModels.ManageApproval
{
    public class PageManageApproval
    {
        public List<DataApproval>? tbApproval { get; set; }
        public PageManageApprovalSave? pageManageApprovalSave { get; set; }
    }
    public class DataApproval : TbApprovalMatrix
    {
        public string? PositionName { get; set; }
    }
    public class PageManageApprovalSave
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "ระบุชื่อแผนก")]
        public int PositionId { get; set; }
        [Required(ErrorMessage = "ระบุงบประมาณ")]
        public decimal Budget { get; set; }

        public List<SelectListItem>? lPosition { get; set; }
    }
}
