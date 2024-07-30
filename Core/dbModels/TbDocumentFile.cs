using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Document_Control.Core.dbModels
{
	[Table(nameof(TbDocumentFile))]
	public class TbDocumentFile
	{
		[Key]
		public int Id { get; set; }
		public int DocId { get; set; }
		public string FileName { get; set; }
		public string FileParth { get; set; }
		public string ContentType { get; set; }
		public DateTime CreateDate { get; set; }
		public int CreateBy { get; set; }
	}
}
