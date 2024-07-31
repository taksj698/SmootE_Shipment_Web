using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Document_Control.Core.pageModels.PurchaseRequisition
{
    public class PagePR
    {
        public int Id { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? DocumentCode { get; set; }
        public int? PositionId { get; set; }
        public int? StatusId { get; set; }
		[Required(ErrorMessage = "ระบุ ความสำคัญ")]
		public int? PriorityId { get; set; }
		[Required(ErrorMessage = "ระบุ หัวเรื่อง")]
		public string? Subject { get; set; }
        [Required(ErrorMessage = "ระบุ งบประมาน")]
        public decimal? Budget { get; set; } = 0;
		[Required(ErrorMessage = "ระบุ บริษัท")]
		public int? CompanyId { get; set; }
		[Required(ErrorMessage = "ระบุ แผนก")]
		public int? DivisionId { get; set; }
        public int? UserId { get; set; }
		[Required(ErrorMessage = "ระบุ ซัพพลายเออร์")]
		public string? SupplierName { get; set; }
		[Required(ErrorMessage = "ระบุ รายละเอียดเพิ่มเติม")]
		public string? Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreateBy { get; set; }
        //
        public string? CreateName { get; set; }
        public string? PositionName { get; set; }
        public string? Reason { get; set; }



        public List<SelectListItem>? lPriority { get; set; }
        public List<SelectListItem>? lCompany { get; set; }
        public List<SelectListItem>? lDivision { get; set; }
        public List<SelectListItem>? lUser { get; set; }



        public ApprovalPR? ApprovalPR { get; set; }
        public List<DocUpload>? DocUpload { get; set; }

		public ModalShowApproval? ModalShowApproval { get; set; }
	}
}
