using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickVisualWebWood.Core.dbModels
{
	[Table(nameof(TbApprovalMatrix))]
	public class TbApprovalMatrix
	{
		[Key]
		public int Id { get; set; }
		public int PositionId { get; set; }
		public decimal Budget { get; set; }
	}
}
