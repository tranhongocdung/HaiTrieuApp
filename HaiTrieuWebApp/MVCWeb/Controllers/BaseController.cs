using System.Web.Mvc;
using MVCWeb.AppDataLayer.Security;

namespace MVCWeb.Controllers
{
    public class BaseController : Controller
    {
        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
    }
}