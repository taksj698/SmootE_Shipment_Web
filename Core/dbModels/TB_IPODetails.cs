using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmootE_Shipment_Web.Core.dbModels
{
    [Table(nameof(TB_IPODetails))]
    public class TB_IPODetails
    {
        [Key]
        public int id { get; set; }
        public string? IPOCode { get; set; }
        public int? TransactionMasterId { get; set; }
        public string? FGCode { get; set; }
        public string? Description { get; set; }
        public string? Size { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Sale { get; set; }
        public decimal? GrandTotal { get; set; }
        public string? Remark { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyBy { get; set; }
    }
}
