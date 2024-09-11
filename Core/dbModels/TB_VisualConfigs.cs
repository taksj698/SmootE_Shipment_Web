using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickVisualWebWood.Core.dbModels
{
    [Table(nameof(TB_VisualConfigs))]
    public class TB_VisualConfigs
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string? Desc { get; set; }
    }

}
