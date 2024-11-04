namespace SmootE_Shipment_Web.Core.serviceModels
{
    public class TBQualityTransaction
    {
        public int id { get; set; }
        public string? sequenceId { get; set; }
        public DateTime? qualityDate { get; set; }
        public bool? quality1 { get; set; }
        public bool? quality2 { get; set; }
        public bool? quality3 { get; set; }
        public bool? quality4 { get; set; }
        public string? resultText { get; set; }
        public string? description { get; set; }
        public DateTime? createDate { get; set; }
        public string? createBy { get; set; }
        public DateTime? modifyDate { get; set; }
        public string? modifyBy { get; set; }
        public bool? inactive { get; set; }
        public string? plate { get; set; }
        public int? queueNo { get; set; }
    }
}
