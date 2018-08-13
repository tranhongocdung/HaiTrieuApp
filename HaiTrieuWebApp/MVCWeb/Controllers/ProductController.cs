using System.Web.Mvc;
using MVCWeb.Cores;
using MVCWeb.Cores.Entities;
using MVCWeb.Cores.IRepositories;
using MVCWeb.Cores.IServices;
using MVCWeb.Cores.Security;
using MVCWeb.Libraries;
using MVCWeb.Models;

namespace MVCWeb.Controllers
{
    [WhitespaceFilter]
    [CustomAuthorize(Roles = "Admin")]
    public class ProductController : BaseController
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;

        public ProductController(
            IProductService productService,
            IProductRepository productRepository
            )
        {
            _productService = productService;
            _productRepository = productRepository;
        }

        public ActionResult Manage()
        {

            var model = new ProductManageViewModel()
            {
                CurrentPage = 1,
                PageSize = 10,
            };
            var totalCount = 0;
            model.Products = _productService.GetList(new FilterParams(), ref totalCount);
            model.ItemCount = totalCount;
            return View(model);
        }

        [HttpPost]
        public ActionResult Manage(ProductManageViewModel model, int page)
        {
            model.CurrentPage = page;
            model.PageSize = 10;
            var totalCount = 0;
            model.Products = _productService.GetList(new FilterParams
            {
                PageNumber = page,
                Keyword = model.Keyword,
            }, ref totalCount);
            model.ItemCount = totalCount;
            return View("_ProductTable", model);
        }
        public ActionResult Edit(int id = 0)
        {
            var model = new ProductEditViewModel();
            if (id != 0)
            {
                var product = _productRepository.GetById(id);
                if (product != null)
                {
                    model.Product = product;
                }
            }
            else
            {
                model.Product = new Product
                {
                    Id = 0
                };
            }
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var message = "";
                var obj = _productRepository.GetById(model.Product.Id);
                if (obj == null)
                {
                    obj = new Product();
                    obj.Id = _productService.Create(model.Product);
                    message = "Đã thêm thành công!";
                }
                else
                {
                    _productService.UpdateProduct(model.Product);
                    message = "Đã cập nhật thành công!";
                }
                return Json(new ReturnData { Success = true, Message = message, Data = obj.Id.ToString() });
            }
            return Json(new ReturnData { Success = false, Message = "Lỗi!" });
        }
    }
}