using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVCWeb.AppDataLayer;
using MVCWeb.AppDataLayer.Entities;
using MVCWeb.AppDataLayer.IServices;
using MVCWeb.AppDataLayer.Security;
using MVCWeb.Libraries;
using MVCWeb.Models;
using Newtonsoft.Json;

namespace MVCWeb.Controllers
{
    [WhitespaceFilter]
    [CustomAuthorize(Roles = "*")]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrderController(
            IOrderService orderService,
            ICustomerService customerService
            )
        {
            _orderService = orderService;
            _customerService = customerService;
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
            model.Orders = _orderService.GetList(new FilterParams()
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
            model.Orders = _orderService.GetList(new FilterParams
            {
                PageNumber = page,
                FromDate = model.FromDate,
                ToDate = model.ToDate,
                CustomerIds = customerIds.ToList(),
                SortField = "CreatedOn",
                StatusId = model.StatusId
            }, ref totalCount);
            model.ItemCount = totalCount;
            return View("_OrderTable", model);
        }
        public ActionResult Edit(int id = 0)
        {
            var model = new OrderEditViewModel();
            if (id != 0)
            {
                var order = _orderService.GetWithCustomerAndOrderDetails(id);
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
                customerId = _customerService.Create(model.Customer);
            }
            else
            {
                _customerService.UpdateCustomer(model.Customer);
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
                _orderService.UpdateOrder(model.Order);
                _orderService.UpdateOrderDetail(orderDetails, model.Order.Id);
                return Content("");
            }

            //Add new
            model.Order.OrderDetails = orderDetails;
            orderId = _orderService.Create(model.Order);
            return Content(orderId.ToString());
        }
        public ActionResult Print(int id)
        {
            var model = new OrderPrintViewModel();
            if (id != 0)
            {
                var order = _orderService.GetWithCustomerAndOrderDetails(id);
                if (order != null)
                {
                    model.Order = order;
                    model.Customer = order.Customer;
                }
            }
            return View("Print58",model);
            /*return View(model);*/
        }

        public ActionResult Complete(int id)
        {
            _orderService.CompleteOrder(id);
            return Content("");
        }
        public ActionResult Cancel(int id)
        {
            _orderService.CancelOrder(id);
            return Content("");
        }
        public ActionResult Restore(int id)
        {
            _orderService.RestoreOrder(id);
            return Content("");
        }

        public ActionResult LoadStatistic(string customerIds, string fromDate, string toDate, int statusId = 0)
        {
            var model = new OrderStatisticViewModel();
            var totalCount = 0;
            var customerIdsInStr = !string.IsNullOrWhiteSpace(customerIds)
               ? customerIds.Split(',').Select(int.Parse)
               : new List<int>();
            var orders = _orderService.GetList(new FilterParams
            {
                PageNumber = 0,
                FromDate = fromDate,
                ToDate = toDate,
                StatusId = statusId,
                CustomerIds = customerIdsInStr.ToList()
            }, ref totalCount);
            var completedOrders = orders.Where(o => o.OrderStatusId == OrderStatus.Completed).ToList();
            //Total cash
            model.TotalCash = completedOrders.Sum(o => o.CompletedRealCash).ToString("#,##0");
            //Incompleted total cash
            model.IncompletedTotalCash = orders.Where(o => o.OrderStatusId != OrderStatus.Completed).Sum(o => o.RealCash).ToString("#,##0");
            //Order count
            model.OrderCount = completedOrders.Count() + "/" + orders.Count();
            //Sold product stat
            var productQuantityStat = orders.SelectMany(o => o.OrderDetails).GroupBy(o => o.ProductId).Select(o => new
            {
                ProductId = o.Key,
                Quantity = o.Sum(x => x.Quantity),
                LatestUnitPrice = orders.SelectMany(od => od.OrderDetails).OrderByDescending(od => od.OrderId).First(od => od.ProductId == o.Key).UnitPrice
            });
            var products = orders.SelectMany(o => o.OrderDetails).Select(o => o.Product).Select(o  =>new
            {
                o.ProductName, o.Id
            }).Distinct().ToList();
            model.SoldProductStat = products.Join(productQuantityStat, p => p.Id, s => s.ProductId, (p, s) => new {p, s})
                .OrderByDescending(o => o.s.Quantity).Select(o => new LabelValueViewModel
                {
                    Label = o.p.ProductName,
                    Value = o.s.Quantity.ToString(),
                    Value1 = o.s.LatestUnitPrice.ToString("#,##0")
                }).ToList();
            //Top 5 best customer stat
            var customerCashStat = completedOrders.GroupBy(o => o.CustomerId).Select(o => new
            {
                CustomerId = o.Key,
                TotalCash = o.Sum(x => x.CompletedRealCash)
            }).OrderByDescending(o => o.TotalCash).Take(10);
            var customer = completedOrders.Select(o => o.Customer).Select(o => new
            {
                o.CustomerName,
                o.Id
            }).Distinct().ToList();
            model.Top10BestCustomerStat = customer.Join(customerCashStat, c => c.Id, s => s.CustomerId, (c, s) => new { c, s })
                .OrderByDescending(o => o.s.TotalCash).Select(o => new LabelValueViewModel
                {
                    Label = o.c.CustomerName,
                    Value = o.s.TotalCash.ToString("#,##0")
                }).ToList();
            return View("_OrderStatisticDetail", model);
        }
    }
}