using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmootE_Shipment_Web.Core.dbModels
{
    [Table(nameof(TB_PackingHeader))]
    public class TB_PackingHeader
    {
        [Key]
        public int Id { get; set; }
        public string? IPOCode { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal? TotalQuantity { get; set; }
        public decimal? TotalCartons { get; set; }
        public decimal? TotalNetWeight { get; set; }
        public decimal? TotalGrossWeight { get; set; }
        public decimal? TotalMarks { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyBy { get; set; }
        public DateTime? PackingDate { get; set; }
        public string? PONo { get; set; }
        public string? RefNo { get; set; }
        public string? InvNo { get; set; }
        public int? TransactionMasterId { get; set; }
    }
}
