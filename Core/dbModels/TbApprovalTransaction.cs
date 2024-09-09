using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickVisualWebWood.Core.dbModels
{
	[Table(nameof(TbApprovalTransaction))]
	public class TbApprovalTransaction
	{
		[Key]
		public int Id { get; set; }
		public int DocId { get; set; }
		public int? UserId { get; set; }
		public int? PositionId { get; set; }
		public int? ApproveBy { get; set; }
		public decimal? Budget { get; set; }
		public bool IsApprove { get; set; }
	}
}
