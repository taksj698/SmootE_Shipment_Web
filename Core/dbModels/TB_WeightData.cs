using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmootE_Shipment_Web.Core.dbModels
{
    [Table(nameof(TB_WeightData))]
    public class TB_WeightData
    {
        [Key]
        public long LOGID { get; set; } // Primary Key, ไม่เป็น null
        public string? SequenceID { get; set; } // nvarchar(50)
        public string? TicketCodeIn { get; set; } // nvarchar(20)
        public string? TicketCodeOut { get; set; } // nvarchar(20)
        public int? QueueNo { get; set; } // int
        public string? Plate1 { get; set; } // nvarchar(50)
        public string? Plate2 { get; set; } // nvarchar(50)
        public string? WeightTypeID { get; set; } // varchar(20)
        public string? TruckTypeID { get; set; } // varchar(20)
        public string? CustomerTypeID { get; set; } // varchar(20)
        public string? CustomerID { get; set; } // varchar(20)
        public string? SupplierID { get; set; } // varchar(20)
        public string? PartnerID { get; set; } // varchar(20)
        public string? FactoryID { get; set; } // varchar(20)
        public string? TransportID { get; set; } // varchar(20)
        public string? LocationID { get; set; } // nvarchar(50)
        public string? WarehouseID { get; set; } // varchar(20)
        public DateTime? InboundDate { get; set; } // datetime
        public decimal? InboundWeight { get; set; } // decimal(18, 0)
        public DateTime? OutboundDate { get; set; } // datetime
        public decimal? OutboundWeight { get; set; } // decimal(18, 0)
                                                     //public decimal GrossWeight { get; set; } // Computed Column
        public string? ProductTypeID { get; set; } // varchar(20)
        public string? ProductID { get; set; } // varchar(20)
        public decimal? Quantity { get; set; } // decimal(18, 2)
        public string? PackageID { get; set; } // varchar(10)
        public decimal? SagWeight { get; set; } // decimal(18, 2)
                                                //public decimal cSagWeight { get; set; } // Computed Column
        public decimal? DeductWeight { get; set; } = 0; // decimal(18, 2) default 0
        public decimal? VariedDeductWeight { get; set; } = 0; // decimal(18, 2) default 0
        public decimal? FixDeductWeight { get; set; } = 0; // decimal(18, 2) default 0
                                                           //public decimal cDeductWeight { get; set; } // Computed Column
        public decimal? OtherDeduct { get; set; } // decimal(18, 2)
        public decimal? VariedOtherDeduct { get; set; } = 0; // decimal(18, 2) default 0
        public decimal? FixOtherDeduct { get; set; } = 0; // decimal(18, 2) default 0
                                                          //public decimal cOtherDeduct { get; set; } // Computed Column
        public decimal? Moisture { get; set; } // decimal(18, 2)
        public string? MoistureTableCode { get; set; } // nvarchar(max)
        public decimal? MoistureDeduct { get; set; } // decimal(18, 2)
                                                     //public decimal cMoistureDeduct { get; set; } // Computed Column
        public decimal? Price { get; set; } // decimal(18, 2)
        public string? UnitID { get; set; } // nvarchar(20)
        public decimal? KgPerTradingUnit { get; set; } // decimal(18, 2)
        public decimal? FixedDeductAmount1 { get; set; } // decimal(18, 2)
        public decimal? VariedDeductAmount1 { get; set; } // decimal(18, 2)
        public decimal? VariedDeductAmountFactor1 { get; set; } // decimal(18, 3)
        public decimal? FixedDeductAmount2 { get; set; } // decimal(18, 2)
        public decimal? VariedDeductAmount2 { get; set; } // decimal(18, 2)
        public decimal? VariedDeductAmountFactor2 { get; set; } // decimal(18, 3)
        public decimal? Tax { get; set; } // decimal(18, 2)
        public int? CheckState { get; set; } // int
        public string? Remark1 { get; set; } // nvarchar(max)
        public string? Remark2 { get; set; } // nvarchar(max)
        public string? Remark3 { get; set; } // nvarchar(max)
        public string? Remark4 { get; set; } // nvarchar(max)
        public string? Remark5 { get; set; } // nvarchar(max)
        public string? PictureIN1 { get; set; } // nvarchar(max)
        public string? PictureIN2 { get; set; } // nvarchar(max)
        public string? PictureIN3 { get; set; } // nvarchar(max)
        public string? PictureIN4 { get; set; } // nvarchar(max)
        public string? PictureIN5 { get; set; } // nvarchar(max)
        public string? PictureOut1 { get; set; } // nvarchar(max)
        public string? PictureOut2 { get; set; } // nvarchar(max)
        public string? PictureOut3 { get; set; } // nvarchar(max)
        public string? PictureOut4 { get; set; } // nvarchar(max)
        public string? PictureOUT5 { get; set; } // nvarchar(max)
        public bool? WeightState { get; set; } // bit
        public int? WeightFlag { get; set; } = 0; // int default 0
        public DateTime? WeightDate { get; set; } // datetime
        public string? WeightInByUserID { get; set; } // nvarchar(50)
        public string? WeightOutByUserID { get; set; } // nvarchar(50)
        public string? WeightInByScale { get; set; } // nvarchar(50)
        public string? WeightOutByScale { get; set; } // nvarchar(50)
        public short? ExportState { get; set; } // smallint
        public DateTime? ExportDateTime { get; set; } // datetime
        public string? ExportUserName { get; set; } // nvarchar(50)
        public short? PrintTime { get; set; } = 0; // smallint default 0
        public DateTime? PrintTimeDateTime { get; set; } // datetime
        public string? PrintTimeByUserName { get; set; } // nvarchar(50)
        public string? PrintTimeByMachineName { get; set; } // nvarchar(50)
        public short? CancelState { get; set; } = 0; // smallint default 0
        public DateTime? CancelDateTime { get; set; } // datetime
        public string? CancelDescription { get; set; } // nvarchar(50)
        public string? CancelByUserName { get; set; } // nvarchar(50)
        public string? CancelByMachineName { get; set; } // nvarchar(50)
        public short? WeightReturn { get; set; } = 0; // smallint default 0
        public DateTime? WeightReturnDateTime { get; set; } // datetime
        public string? WeightReturnByName { get; set; } // nvarchar(50)
        public string? WeightReturnDesc { get; set; } // nvarchar(50)
        public string? ScreenCaptureIN { get; set; } // nvarchar(max)
        public string? ScreenCaptureOUT { get; set; } // nvarchar(max)
        public string? BranchID { get; set; } // varchar(20)
        public decimal? cAmount1 { get; set; } // decimal(18, 2)
        public decimal? cVariedAmount1 { get; set; } // decimal(18, 2)
        public decimal? cVariedAmount2 { get; set; } // decimal(18, 2)
        public decimal? cNetAmount1 { get; set; } // decimal(18, 2)
        public string? BillingNo { get; set; } // nvarchar(50)
        public DateTime? BillingDate { get; set; } // date
        public string? BillingByUserName { get; set; } // nvarchar(50)
        public short? BillingState { get; set; } = 0; // smallint default 0
        public short? PaidState { get; set; } = 0; // smallint default 0
        public string? PayTransactionCode { get; set; } // nvarchar(50)
        public DateTime? PayTransactionDate { get; set; } // datetime
        public string? InvoiceCode { get; set; } // nvarchar(50)
        public DateTime? InvoiceDate { get; set; } // date
        public short? InvoiceState { get; set; } = 0; // smallint default 0
        public short? InvType { get; set; } // smallint
        public string? TagCode { get; set; } // varchar(50)
        public decimal? DesWeight { get; set; } // decimal(18, 2)
                                                //public decimal DiffWeight { get; set; } // Computed Column
        public string? SapDocNo { get; set; } // nvarchar(50)
        public string? Status { get; set; } // nvarchar(50)
        public string? ErrMsg { get; set; } // nvarchar(50)
        public DateTime? CreateDate { get; set; } // datetime
        public string? ActionType { get; set; } // nvarchar(50)
        public short? TaxInvoiceStatus { get; set; } // smallint
        public short? SendStatus { get; set; } // smallint
        public DateTime? SendDate { get; set; } // datetime
        public decimal? DiscountAmount { get; set; } // decimal(18, 2)
        public decimal? AdvanceAmount { get; set; } // decimal(18, 2)
        public decimal? TotalAmount { get; set; } // decimal(18, 2)
        public decimal? DecimalAmount { get; set; } // decimal(18, 2)
        public string? PurchaseNo { get; set; } // nvarchar(50)
        public string? ContractNo { get; set; } // nvarchar(50)
        public string? JobCode { get; set; } // nvarchar(50)
        public string? ShipmentID { get; set; } // varchar(50)
        public string? TransporterID { get; set; } // varchar(50)
        public bool? TransporterState { get; set; } // bit
        public string? InvoiceByUserName { get; set; } // nvarchar(50)
        public bool? IsShowPrice { get; set; } // bit
        public int? DocNum { get; set; } // int
        public string? ResponseTime { get; set; } // varchar(50)
        public string? ObjectType { get; set; } // varchar(50)
        public decimal? TaxAmount { get; set; } // decimal(18, 2)
        public decimal? GrandTotal { get; set; } // decimal(18, 2)
        public string? CompanyName { get; set; } // nvarchar(255)
        public string? AddressName { get; set; } // nvarchar(255)
        public bool? IsVat { get; set; } // bit
        public string? CompanyID { get; set; } // nvarchar(50)
        public int? BalanceEffect { get; set; } // int
        public int? AccountEffect { get; set; } // int
        public decimal? CarBodyLength { get; set; } // decimal(18, 2)
        public decimal? CarExtensionLength { get; set; } // decimal(18, 2)
        public decimal? CarTotalLength { get; set; } // decimal(18, 2)
        public long? HeadquarterID { get; set; } // bigint
        public string? DriverID { get; set; } // varchar(20)
        public string? UpdateStock { get; set; } // nvarchar(50)
        public string? SourceSystem { get; set; } // nvarchar(50)
        public bool? QualityState { get; set; } // bit
        public string? QualityByName { get; set; }

    }
}
