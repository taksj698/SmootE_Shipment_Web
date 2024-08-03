namespace Document_Control.Core.pageModels.UserProfile
{



	public class UserProfileModel
	{
		public NotiSetting? notiSetting { get; set; }
		public string? Tab { get; set; }
	}

	public class NotiSetting
	{
		public string? link { get; set; }
		public string? token { get; set; }
	}

}
