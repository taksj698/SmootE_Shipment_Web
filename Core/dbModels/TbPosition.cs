using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Document_Control.Core.dbModels
{
	[Table(nameof(TbPosition))]
	public class TbPosition
	{
		[Key]
		public int Id { get; set; }
		public string? PositionName { get; set; }
		public string? Desc { get; set; }
		public int Seq { get; set; }
		public DateTime? CreateDate { get; set; }
		public DateTime? ModifyDate { get; set; }
	}
}
