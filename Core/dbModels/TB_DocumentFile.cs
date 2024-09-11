using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickVisualWebWood.Core.dbModels
{
	[Table(nameof(TB_DocumentFile))]
	public class TB_DocumentFile
	{
		[Key]
		public int Id { get; set; }
		public string SequenceID { get; set; }
		public string FileName { get; set; }
		public string FileParth { get; set; }
		public string ContentType { get; set; }
		public string Extension { get; set; }
		public DateTime CreateDate { get; set; }
		public string CreateBy { get; set; }
	}
}
