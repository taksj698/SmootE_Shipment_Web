using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Document_Control.Core.dbModels
{
	[Table(nameof(TbApprovalMatrix))]
	public class TbApprovalMatrix
	{
		[Key]
		public int Id { get; set; }
		public int PositionId { get; set; }
		public decimal Budget { get; set; }
		public string? Description { get; set; }
		public bool IsActive { get; set; }
	}
}
