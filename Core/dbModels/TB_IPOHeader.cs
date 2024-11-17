using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmootE_Shipment_Web.Core.dbModels
{
    [Table(nameof(TB_IPOHeader))]
    public class TB_IPOHeader
    {
        [Key]
        public int id { get; set; }
        public string? IPOCode { get; set; }
        public DateTime? IPODate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int? CountryId { get; set; }
        public int? CustomerId { get; set; }
        public int? BankId { get; set; }
        public int? ForwarderId { get; set; }
        public string? InvNo { get; set; }
        public string? OrderNo { get; set; }
        public string? PONo { get; set; }
        public string? RefNo { get; set; }
        public string? DELIVERYTERMS { get; set; }
        public string? PAYMENTTERMS { get; set; }
        public string? CURRENCY { get; set; }
        public string? SELLINGTERM { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyAddress { get; set; }
        public decimal? TotalQuantity { get; set; }
        public decimal? GrandTotal { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyBy { get; set; }
        public bool? IsPacking { get; set; }
    }
}
