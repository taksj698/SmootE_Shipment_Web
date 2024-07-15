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
        public int? PriorityId { get; set; }
        public string? Subject { get; set; }
        public decimal? Budget { get; set; }
        public int? CompanyId { get; set; }
        public int? DivisionId { get; set; }
        public int? UserId { get; set; }
        public string? SupplierName { get; set; }
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
        public ModalShowApproval? ModalShowApproval { get; set; }
	}
}
