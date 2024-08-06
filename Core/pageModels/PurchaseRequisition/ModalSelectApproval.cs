namespace Document_Control.Core.pageModels.PurchaseRequisition
{
	public class ModalSelectApproval
	{
		public List<ModalSelectApprovalApprovalDetail>? approvalDetails { get; set; }
	}
	public class ModalSelectApprovalApprovalDetail
	{
		public int? id { get; set; }
		public string? Name { get; set; }
		public int? PositionId { get; set; }
		public string? PositionName { get; set; }
		public string? TelNo { get; set; }
	}
}
