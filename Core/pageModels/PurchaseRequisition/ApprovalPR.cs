namespace Document_Control.Core.pageModels.PurchaseRequisition
{
	public class ApprovalPR
	{
		public List<ApprovalList>? approvalLists { get; set; }
	}
	public class ApprovalList
	{
		public int? userId { get; set; }
		public string? userName { get; set; }
		public int? PositionId { get; set; }
		public string? PositionName { get; set; }
		public string? Budget { get; set; } = "-";
		public bool IsApproved { get; set; }
	}
}
