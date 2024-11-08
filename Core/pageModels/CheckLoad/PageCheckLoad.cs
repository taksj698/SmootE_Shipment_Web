namespace SmootE_Shipment_Web.Core.pageModels.CheckLoad
{
    public class PageCheckLoad
    {
        public PageCheckLoadTop? pageCheckLoadTop { get; set; }
        public List<PageCheckLoadData>? pageCheckLoadData { get; set; }
    }

    public class PageCheckLoadTop
    {
        public string? efxNo { get; set; }
        public string? refNo { get; set; }
        public string? customer { get; set; }
        public string? country { get; set; }
    }
    public class PageCheckLoadData
    {
        public string? ctnNo { get; set; }
        public string? dimension { get; set; }
        public decimal? casePack { get; set; }
        public decimal? cartons { get; set; }
        public List<PageCheckLoadItem>? pageCheckLoadItems { get; set; }
    }
    public class PageCheckLoadItem
    {
        public int? no { get; set; }
        public string? description { get; set; }
        public string? barcode { get; set; }
        public string? lot { get; set; }
        public string? dateTime { get; set; }
        public string? status { get; set; }
    }
}
