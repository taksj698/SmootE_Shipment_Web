namespace QuickVisualWebWood.Core.serviceModels
{
    public class TbDocumentFile
    {
        public int id { get; set; }
        public string sequenceId { get; set; }
        public string fileName { get; set; }
        public string fileParth { get; set; }
        public string contentType { get; set; }
        public string extension { get; set; }
        public DateTime createDate { get; set; }
        public string createBy { get; set; }
    }
}
