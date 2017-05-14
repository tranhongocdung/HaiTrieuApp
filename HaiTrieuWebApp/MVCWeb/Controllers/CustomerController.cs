using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVCWeb.AppDataLayer;
using MVCWeb.AppDataLayer.Entities;
using MVCWeb.AppDataLayer.IRepositories;
using MVCWeb.AppDataLayer.Repositories;
using MVCWeb.Libraries;
using MVCWeb.Models;
using Newtonsoft.Json;

namespace MVCWeb.Controllers
{
    [WhitespaceFilter]
    //[CustomAuthorize(Roles = "Admin")]
    public class CustomerController : BaseController
    {
        private ICustomerRepository _customerRepository;

        public CustomerController()
        {
            var context = new DbAppContext();
            _customerRepository = new CustomerRepository(context);
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Manage()
        {

            var model = new CustomerManageModel()
            {
                CurrentPage = 1,
                PageSize = 10,
            };
            var totalCount = 0;
            model.Customers = _customerRepository.GetList(new FilterParams(), ref totalCount);
            model.ItemCount = totalCount;
            return View(model);
        }

        [HttpPost]
        public ActionResult Manage(CustomerManageModel model, int page)
        {
            model.CurrentPage = page;
            model.PageSize = 10;
            var totalCount = 0;
            model.Customers = _customerRepository.GetList(new FilterParams
            {
                PageNumber = page,
                Keyword = model.Keyword
            }, ref totalCount);
            model.ItemCount = totalCount;
            return View("_CustomerTable", model);
        }
        public ActionResult Edit(int id = 0)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit(OrderEditModel model)
        {
            return View();
        }
    }
}