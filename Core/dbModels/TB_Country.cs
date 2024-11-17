using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmootE_Shipment_Web.Core.dbModels
{
    [Table(nameof(TB_Country))]
    public class TB_Country
    {
        [Key]
        public int Id { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryNameTH { get; set; }
        public string? CountryNameEN { get; set; }
    }
}
