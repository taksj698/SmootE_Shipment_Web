using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickVisualWebWood.Core.dbModels
{
	[Table(nameof(TB_WeightData))]
	public class TB_WeightData
	{
		[Key]
		public long LOGID { get; set; }
		public string? SequenceID { get; set; }
		public string? TicketCodeIn { get; set; }
		public string? TicketCodeOut { get; set; }
		public string? QueueNo { get; set; }
		public string? Plate1 { get; set; }
		public string? Plate2 { get; set; }
		public string? WeightTypeID { get; set; }
		public string? TruckTypeID { get; set; }
		public string? CustomerTypeID { get; set; }
		public string? CustomerID { get; set; }
		public string? SupplierID { get; set; }
		public string? PartnerID { get; set; }
		public string? FactoryID { get; set; }
		public string? TransportID { get; set; }
		public string? LocationID { get; set; }
		public string? WarehouseID { get; set; }
		public DateTime? InboundDate { get; set; }
		public decimal? InboundWeight { get; set; }
		public DateTime? OutboundDate { get; set; }
		public decimal? OutboundWeight { get; set; }
		//GrossWeight
		public string? ProductTypeID { get; set; }
		public string? ProductID { get; set; }
	}
}
