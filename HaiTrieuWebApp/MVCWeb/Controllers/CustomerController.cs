﻿using System.Collections.Generic;
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

            var model = new CustomerManageViewModel()
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
        public ActionResult Manage(CustomerManageViewModel model, int page)
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
            var model = new CustomerEditViewModel();
            if (id != 0)
            {
                var customer = _customerRepository.GetById(id);
                if (customer != null)
                {
                    model.Customer = customer;
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(OrderEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var message = "";
                var obj = _customerRepository.GetById(model.Customer.Id);
                if (obj == null)
                {
                    _customerRepository.Insert(model.Customer);
                    message = "Đã thêm thành công!";
                }
                else
                {
                    _customerRepository.UpdateCustomer(model.Customer);
                    message = "Đã cập nhật thành công!";
                }
                return Json(new ReturnData { Success = true, Message = message, Data = obj.Id.ToString() });
            }
            return Json(new ReturnData { Success = false, Message = "Lỗi!" });
        }
    }
}