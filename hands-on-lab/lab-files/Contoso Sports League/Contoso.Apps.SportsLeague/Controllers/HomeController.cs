using Contoso.Apps.SportsLeague.Web.Models;
using System.Web.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Contoso.Apps.SportsLeague.Controllers
{
    public class HomeController : Controller {
        [HttpGet]
        public ActionResult Index() {
            var vm = new HomeModel();
            return View(vm);
        }

        [HttpGet]
        public ActionResult About() {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        [HttpGet]
        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Claims()
        {
            Claim displayName = ClaimsPrincipal.Current.FindFirst(ClaimsPrincipal.Current.Identities.First().NameClaimType);
            ViewBag.DisplayName = displayName != null ? displayName.Value : string.Empty;
            return View();
        }
    }
}