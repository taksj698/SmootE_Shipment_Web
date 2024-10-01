namespace QuickVisualWebWood.Core.pageModels.Home
{
	public class Worklist
	{
		public List<WorklistData>? data { get; set; }
	}
	public class WorklistData
	{
		public string? WeighNumber { get; set; }
		public string? SequenceID { get; set; }
		public string? Plate { get; set; }
		public string? CustomerName { get; set; }
		public string? TransctionDate { get; set; }
		public string? EvaluationResults { get; set; }
		public string? Status { get; set; }
		public string? Remark { get; set; }
		public string? QualityByName { get; set; }
		public string? Branch { get; set; }

    }

}
