using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SmootE_Shipment_Web.Core.pageModels.PurchaseRequisition
{
    public class PagePR
    {

        public string? SequenceID { get; set; }
        public int? QueueNo { get; set; }
        public DateTime? QualityDate { get; set; }
        public string? Plate { get; set; }
        public string? CustomerName { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "ระบุการประเมิน")]
        public string? SelectedOption { get; set; }
        public List<DocUpload>? DocUpload { get; set; }

    }
}
