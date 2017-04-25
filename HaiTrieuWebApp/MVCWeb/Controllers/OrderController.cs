using System.Collections.Generic;
using System.Web.Mvc;
using MVCWeb.AppDataLayer.Entities;
using MVCWeb.AppDataLayer.Repositories;
using MVCWeb.AppDataLayer.Security;
using MVCWeb.Libraries;
using MVCWeb.Models;
using Newtonsoft.Json;

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
        public ActionResult Edit(int id = 0)
        {
            var model = new OrderEditModel();
            if (id != 0)
            {
                var order = OrderRepository.GetWithCustomerAndOrderDetails(id);
                if (order != null)
                {
                    model.Order = order;
                    model.Customer = order.Customer;
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(OrderEditModel model)
        {
            var customerId = model.Customer.Id;
            var orderId = model.Order.Id;
            if (customerId == 0)
            {
                customerId = CustomerRepository.Create(model.Customer);
            }

            model.Order.CustomerId = customerId;
            var orderDetails = new List<OrderDetail>();
            if (!string.IsNullOrEmpty(model.OrderDetailJson))
            {
                orderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(model.OrderDetailJson);
            }
            if (model.Order.Id != 0)
            {
                OrderRepository.Update(model.Order);
                OrderRepository.UpdateOrderDetail(orderDetails, model.Order.Id);
                return Content(orderId.ToString());
            }

            //Add new
            model.Order.OrderDetails = orderDetails;
            orderId = OrderRepository.Create(model.Order);
            return RedirectToAction("Edit", new {id = orderId});
        }
        public ActionResult Print()
        {
            return View();
        }
    }
}