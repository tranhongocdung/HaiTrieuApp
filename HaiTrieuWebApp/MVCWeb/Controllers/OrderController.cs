using System.Web.Mvc;
using MVCWeb.AppDataLayer.Security;
using MVCWeb.Libraries;

namespace MVCWeb.Controllers
{
    [WhitespaceFilter]
    [CustomAuthorize(Roles = "Admin")]
    public class OrderController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
    }
}