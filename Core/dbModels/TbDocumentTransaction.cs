using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Document_Control.Core.dbModels
{
	[Table(nameof(TbDocumentTransaction))]
	public class TbDocumentTransaction
	{
		[Key]
		public int Id { get; set; }
		public DateTime? OrderDate { get; set; }
		public string? DocumentCode { get; set; }
		public int? PositionId { get; set; }
		public int? StatusId { get; set; }
		public int? PriorityId { get; set; }
		public string? Subject { get; set; }
		public decimal? Budget { get; set; }
		public int? CompanyId { get; set; }
		public int? DivisionId { get; set; }
		public int? UserId { get; set; }
		public string? SupplierName { get; set; }
		public string? Description { get; set; }
		public DateTime? CreateDate { get; set; }
		public int? CreateBy { get; set; }
		public DateTime? ModifyDate { get; set; }
		public int? ModifyBy { get; set; }
	}
}
