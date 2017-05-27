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
    public class OrderController : BaseController
    {
        private IOrderDetailRepository _orderDetailRepository;
        private IOrderRepository _orderRepository;
        private ICustomerRepository _customerRepository;

        public OrderController()
        {
            var context = new DbAppContext();
            _orderRepository = new OrderRepository(context);
            _orderDetailRepository = new OrderDetailRepository(context);
            _customerRepository = new CustomerRepository(context);
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Manage()
        {
            var model = new OrderManageViewModel()
            {
                CurrentPage = 1,
                PageSize = 10,
            };
            var totalCount = 0;
            model.Orders = _orderRepository.GetList(new FilterParams()
            {
                SortField = "CreatedOn"
            }, ref totalCount);
            model.ItemCount = totalCount;
            return View(model);
        }
        [HttpPost]
        public ActionResult Manage(OrderManageViewModel model, int page)
        {
            model.CurrentPage = page;
            model.PageSize = 10;
            var totalCount = 0;
            var customerIds = !string.IsNullOrWhiteSpace(model.CustomerIds)
                ? model.CustomerIds.Split(',').Select(int.Parse)
                : new List<int>();
            model.Orders = _orderRepository.GetList(new FilterParams
            {
                PageNumber = page,
                FromDate = model.FromDate,
                ToDate = model.ToDate,
                CustomerIds = customerIds.ToList(),
                SortField = "CreatedOn"
            }, ref totalCount);
            model.ItemCount = totalCount;
            return View("_OrderTable", model);
        }
        public ActionResult Edit(int id = 0)
        {
            var model = new OrderEditViewModel();
            if (id != 0)
            {
                var order = _orderRepository.GetWithCustomerAndOrderDetails(id);
                if (order != null)
                {
                    model.Order = order;
                    model.Customer = order.Customer;
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(OrderEditViewModel model)
        {
            var customerId = model.Customer.Id;
            var orderId = model.Order.Id;
            if (customerId == 0)
            {
                customerId = _customerRepository.Create(model.Customer);
            }

            model.Order.CustomerId = customerId;
            var orderDetails = new List<OrderDetail>();
            if (!string.IsNullOrEmpty(model.OrderDetailJson))
            {
                orderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(model.OrderDetailJson);
            }

            //Update
            if (model.Order.Id != 0)
            {
                _orderRepository.UpdateOrder(model.Order);
                _orderDetailRepository.UpdateOrderDetail(orderDetails, model.Order.Id);
                return Content("");
            }

            //Add new
            model.Order.OrderDetails = orderDetails;
            orderId = _orderRepository.Create(model.Order);
            return Content(orderId.ToString());
        }
        public ActionResult Print(int id)
        {
            var model = new OrderPrintViewModel();
            if (id != 0)
            {
                var order = _orderRepository.GetWithCustomerAndOrderDetails(id);
                if (order != null)
                {
                    model.Order = order;
                    model.Customer = order.Customer;
                }
            }
            return View(model);
        }

        public ActionResult Complete(int id)
        {
            _orderRepository.CompleteOrder(id);
            return Content("");
        }
    }
}