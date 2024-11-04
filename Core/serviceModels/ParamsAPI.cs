namespace SmootE_Shipment_Web.Core.serviceModels
{
	public class ParamsAPI
	{
		public List<BasicObject> Header { get; set; }
		public string Url { get; set; }
		public string ContentType { get; set; } = "application/json";
		public dynamic Data { get; set; }
		public Dictionary<string, string> Data2 { get; set; }
		public Stream DataStream { get; set; }
	}
}
