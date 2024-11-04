using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmootE_Shipment_Web.Core.dbModels
{
    [Table(nameof(TB_WeightType))]
    public class TB_WeightType
    {
        [Key]
        public string WeightTypeID { get; set; }
        public string WeightTypeName { get; set; }
    }
}
