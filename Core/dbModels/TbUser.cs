using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickVisualWebWood.Core.dbModels
{
	[Table(nameof(TbUser))]
	public class TbUser
	{
		[Key]
		public int Id { get; set; }
		public string? UserName { get; set; }
		public string? Password { get; set; }
		public string Name { get; set; }
		public string? NotifyToken { get; set; }
		public string TelNo { get; set; }
		public int PositionId { get; set; }
		public bool IsApprove { get; set; }
		public bool IsManager { get; set; }

        public bool IsActive { get; set; }
        
        public int RoleId { get; set; }		
		public DateTime? LastLogDate { get; set; }
		public DateTime? CreateDate { get; set; }
		public DateTime? ModifyDate { get; set; }

	}
}
