using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Document_Control.Core.dbModels
{
	[Table(nameof(TbUser))]
	public class TbUser
	{
		[Key]
		public int Id { get; set; }
		public string? UserName { get; set; }
		public string? Password { get; set; }
		public string Name { get; set; }
		public int PositionId { get; set; }
		public bool IsApprove { get; set; }
		public DateTime? LastLogDate { get; set; }	
		public DateTime? CreateDate { get; set; }
		public DateTime? ModifyDate { get; set; }

	}
}
