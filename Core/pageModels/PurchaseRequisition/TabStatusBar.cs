namespace SmootE_Shipment_Web.Core.pageModels.PurchaseRequisition
{
	public class TabStatusBar
	{
		public string? StatusName { get; set; }
		public List<TabStatusBarItem>? lStatus { get; set; }
	}
	public class TabStatusBarItem
	{
		public string? StatusName { get; set; }
		public int? StatusId { get; set; }
		public string? Name { get; set; }
		public int? Flag { get; set; } = 0;
	}
}
