using System.Web.Mvc;
using MVCWeb.AppDataLayer.Entities;
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
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Print()
        {
            return View();
        }
    }
}