using System.ComponentModel.DataAnnotations;

namespace QuickVisualWebWood.Core.pageModels.UserProfile
{
	public class ChangePass
	{
		[Required(ErrorMessage = "ระบุ รหัสผ่านเดิม")]
		public string? oldpass { get; set; }
		[Required(ErrorMessage = "ระบุ รหัสผ่านใหม่")]
		public string? newpass { get; set; }
		[Required(ErrorMessage = "ระบุ ยืนยันรหัสผ่านใหม่")]
		public string? renewpass { get; set; }
	}
}
