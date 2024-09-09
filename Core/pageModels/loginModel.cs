using System.ComponentModel.DataAnnotations;

namespace QuickVisualWebWood.Core.pageModels
{
	public class loginModel
	{
		[Required(ErrorMessage = "ระบุชื่อผู้ใช้งาน")]
		public string? userName { get; set; }
		[Required(ErrorMessage = "ระบุรหัสผ่าน")]
		public string? passWord { get; set; }
	}
}
