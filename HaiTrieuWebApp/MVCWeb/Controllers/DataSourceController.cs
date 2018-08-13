using System.Linq;
using System.Web.Mvc;
using MVCWeb.Cores.Entities;
using MVCWeb.Cores.Security;
using MVCWeb.Libraries;

namespace MVCWeb.Controllers
{
    //[CustomAuthorize(Roles = "Admin")]
    public class DataSourceController : BaseController
    {
        public ActionResult GetProductName(string query, int id = 0)
        {
            var db = new DbAppContext();
            if (id != 0)
            {
                var item = db.Products.First(o => o.Id == id);
                return Json(item, JsonRequestBehavior.AllowGet);
            }
            var list = db.Products.Where(o=>o.ProductName.Contains(query));
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCustomerSuggestion(string query, int id = 0)
        {
            var db = new DbAppContext();
            if (id != 0)
            {
                var item = db.Customers.First(o => o.Id == id);
                return Json(item, JsonRequestBehavior.AllowGet);
            }
            var list =
                db.Customers.Where(
                    o => o.CustomerName.Contains(query) || o.Email.Contains(query) || o.PhoneNo.Contains(query)).Take(10).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}