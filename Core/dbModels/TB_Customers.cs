using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmootE_Shipment_Web.Core.dbModels
{
    [Table(nameof(TB_Customers))]
    public class TB_Customers
    {
        [Key]
        public string CustomerID { get; set; }
        public string? CustomerName { get; set; }
    }
}
