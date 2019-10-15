using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;

namespace Contoso.Apps.SportsLeague.Web.Controllers
{
    public class AccountController : Controller
    {
        // Controllers\AccountController.cs

        public void SignIn()
        {
            if (!Request.IsAuthenticated)
            {
                // To execute a policy, you simply need to trigger an OWIN challenge.
                // You can indicate which policy to use by specifying the policy id as the AuthenticationType
                HttpContext.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties() { RedirectUri = "/" }, Startup.SignUpPolicyId);
            }
        }

        //public void SignUp()
        //{
        //    if (!Request.IsAuthenticated)
        //    {
        //        HttpContext.GetOwinContext().Authentication.Challenge(
        //            new AuthenticationProperties() { RedirectUri = "/" }, Startup.SignUpPolicyId);
        //    }
        //}


        public void EditProfile()
        {
            if (Request.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties() { RedirectUri = "/" }, Startup.ProfilePolicyId);
            }
        }

        public void SignOut()
        {
            if (Request.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.SignOut();
            }
        }
    }
}