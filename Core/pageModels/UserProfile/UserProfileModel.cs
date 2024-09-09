using System.ComponentModel.DataAnnotations;
namespace QuickVisualWebWood.Core.pageModels.UserProfile
{
	public class UserProfileModel
	{
		public Profileview? profile { get; set; }
		public NotiSetting? notiSetting { get; set; }
		public string? Tab { get; set; }
	}

	public class NotiSetting
	{
		public string? link { get; set; }
		public string? token { get; set; }
	}
	public class Profileview
	{
		[Required(ErrorMessage = "ระบุ ชื่อ-นามสกุล")]
		public string? name { get; set; }
		[Required(ErrorMessage = "ระบุ หมายเลขโทรศัพท์")]
		public string? TelNo { get; set; }
		public string? positionName { get; set; }
	}
}
