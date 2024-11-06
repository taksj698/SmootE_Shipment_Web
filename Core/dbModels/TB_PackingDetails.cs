using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmootE_Shipment_Web.Core.dbModels
{
    [Table(nameof(TB_PackingDetails))]
    public class TB_PackingDetails
    {
        [Key]
        public int Id { get; set; }
        public int? PackingId { get; set; }
        public string? CTN { get; set; }
        public string? Dimension { get; set; }
        public string? Description { get; set; }
        public string? Barcode { get; set; }
        public string? Lot { get; set; }
        public string? CountryOfOrigin { get; set; }
        public int? Quantity { get; set; }
        public decimal? CasePacking { get; set; }
        public decimal? Cartons { get; set; }
        public decimal? NetWeight { get; set; }
        public decimal? GrossWeight { get; set; }
        public decimal? TotalNetWeight { get; set; }
        public decimal? TotalGrossWeight { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyBy { get; set; }
        public string? PackingDate { get; set; }
        public string? InvNo { get; set; }
        public string? PONo { get; set; }
        public string? RefNo { get; set; }
        public string? CTNRef { get; set; }
        public string? CTNPrefix { get; set; }
        public int? TransactionMasterId { get; set; }
    }
}
