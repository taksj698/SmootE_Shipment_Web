using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmootE_Shipment_Web.Core.dbModels
{
    [Table(nameof(TB_Log))]
    public class TB_Log
    {
        [Key]
        public long AutoRun { get; set; }
        public DateTime? LogDateTime { get; set; }
        public string? Params { get; set; }
        public string? Method { get; set; }
        public string? Username { get; set; }
        public string? TableName { get; set; }
        public string? RecordCode { get; set; }
        public string? FieldName { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public string? MachineName { get; set; }
        public string? LocalIpAddress { get; set; }
    }
}
