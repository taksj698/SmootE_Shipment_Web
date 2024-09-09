using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickVisualWebWood.Core.dbModels
{
	[Table(nameof(TbCompany))]
	public class TbCompany
	{
		[Key]
		public int Id { get; set; }
		public string? CompanyName { get; set; }
		public DateTime? CreateDate { get; set; }
		public DateTime? ModifyDate { get; set; }
	}
}
