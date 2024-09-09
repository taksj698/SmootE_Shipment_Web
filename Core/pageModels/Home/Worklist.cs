namespace QuickVisualWebWood.Core.pageModels.Home
{
	public class Worklist
	{
		public List<WorklistData>? data { get; set; }
	}
	public class WorklistData
	{
		public DateTime DocumentDate { get; set; }
		public string DocumentCode { get; set; }
		public int DocumentId { get; set; }
		public string Name { get; set; }
		public string Subject { get; set; }
		public string Priority { get; set; }
		public int PriorityId { get; set; }
		public string Status { get; set; }
		public WorklistDataApprover? Approver { get; set; }
	}
	public class WorklistDataApprover
	{
		public decimal Budget { get; set; }
		public int PositionId { get; set; }
		public string PositionName { get; set; }
		public string? UserName { get; set; }
	}
}
