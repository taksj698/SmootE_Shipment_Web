using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Document_Control.Core.dbModels
{
	[Table(nameof(TbHistoryTransaction))]
	public class TbHistoryTransaction
	{
		[Key]
		public int Id { get; set; }
		public int DocId { get; set; }
		public int UserId { get; set; }
		public int PositionId { get; set; }
		public int StatusId { get; set; }
		public string Action { get; set; }
		public string? Reason { get; set; }
		public DateTime StampDate { get; set; }
	}
}
