namespace Document_Control.Core.pageModels.PurchaseRequisition
{
	public class ApprovalPR
	{
		public List<ApprovalList>? approvalLists { get; set; }
	}
	public class ApprovalList
	{
		public int? PositionId { get; set; }
		public string? PositionName { get; set; }
		public string? Budget { get; set; }
	}
}
