using QuickVisualWebWood.Core.dbModels;
using System.ComponentModel.DataAnnotations;

namespace QuickVisualWebWood.Core.pageModels.ManagePosition
{
    public class PageManagePosition
    {
        public List<DataPosition>? tbPosition { get; set; }
        public PageManagePositionSave? pageManagePositionSave { get; set; }
    }
    public class DataPosition// : TbPosition 
    {
        public bool IsCanDelete { get; set; }
    }
    public class PageManagePositionSave
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "ระบุชื่อตำแหน่ง")]
        public string? PositionName { get; set; }
    }
}
