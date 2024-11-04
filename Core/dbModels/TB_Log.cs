using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmootE_Shipment_Web.Core.dbModels
{
    [Table(nameof(TB_Log))]
    public class TB_Log
    {
        [Key]
        public long Id { get; set; }
        public DateTime? LogDateTime { get; set; }
        public string? TraceID { get; set; }
        public string? Tier { get; set; }
        public string? Request { get; set; }
        public string? Method { get; set; }
        public string? Username { get; set; }
        public string? TableName { get; set; }
        public string? RequestPath { get; set; }
        public string? RecordCode { get; set; }
        public string? ExecuteTime { get; set; }
        public string? Response { get; set; }
        public string? MachineName { get; set; }
        public string? LocalIpAddress { get; set; }
    }
}
