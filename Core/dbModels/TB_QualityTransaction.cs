using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickVisualWebWood.Core.dbModels
{
    [Table(nameof(TB_QualityTransaction))]
    public class TB_QualityTransaction
    {
        [Key]
        public int Id { get; set; }
        public string? SequenceID { get; set; }
        public DateTime? QualityDate { get; set; }
        public bool? Quality1 { get; set; }
        public bool? Quality2 { get; set; }
        public bool? Quality3 { get; set; }
        public bool? Quality4 { get; set; }
        public string? ResultText { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string? ModifyBy { get; set; }
        public bool? Inactive { get; set; }
    }
}
