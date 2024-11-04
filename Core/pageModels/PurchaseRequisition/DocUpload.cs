namespace SmootE_Shipment_Web.Core.pageModels.PurchaseRequisition
{
	public class DocUpload
	{
		public string id { get; set; }
		public string base64 { get; set; }
		public string ContentType { get; set; }
		public string filename { get; set; }
		public string extension { get; set; }
		public bool IsReadonly { get; set; } = false;
	}
}
