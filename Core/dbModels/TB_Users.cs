using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickVisualWebWood.Core.dbModels
{
	[Table(nameof(TB_Users))]
	public class TB_Users
	{
		[Key]
		public int UserID { get; set; }	
		public string? UserName { get; set; }	
		public string? Password1 { get; set; }
		public string? FullUserName { get; set; }


		public bool? InActive { get; set; }
		public DateTime? ExpiredDate { get; set; }
		public string? BranchID { get; set; }
	}
}
