namespace QuickVisualWebWood.Core.pageModels.PurchaseRequisition
{
	public class HistoryCom
	{
		public List<HistoryComData>? data { get; set; }
	}
	public class HistoryComData
	{
		public DateTime StampDate { get; set; }
		public string Name { get; set; }
		public string Position { get; set; }
		public string Action { get; set; }
		public string Reason { get; set; }
	}
}
