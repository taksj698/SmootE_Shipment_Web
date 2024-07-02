using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Document_Control.ControllerComponent
{
    public class TopMenuComponent : ViewComponent
    {
        //private List<Claim>? UserProfile;
        //private string? name;
        //public string? positionName;
        //private readonly ConvertHelper _chp;
        //IHttpContextAccessor haccess, ConvertHelper chp
        public TopMenuComponent()
        {
            //_chp = chp;
            //var identity = (ClaimsIdentity)haccess.HttpContext.User.Identity;
            //UserProfile = identity.Claims.ToList();

            //var fineName = UserProfile.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            //if (fineName != null)
            //{
            //    name = fineName.Value;
            //}

            //var findPosition = UserProfile.FirstOrDefault(x => x.Type == "PositionName");
            //if (findPosition != null)
            //{
            //    positionName = findPosition.Value;
            //}


        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View("~/ViewComponents/SidebarMenuComponent.cshtml");
        }
    }
}
