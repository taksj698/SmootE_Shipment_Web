using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Document_Control.Core.dbModels
{
	[Table(nameof(TbDivision))]
	public class TbDivision
	{
		[Key]
		public int Id { get; set; }
		public string DivisionName { get; set; }
		public DateTime? CreateDate { get; set; }
		public DateTime? ModifyDate { get; set; }
	}
}
