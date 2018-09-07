using System.Collections.Generic;
using System.Linq;
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
        private readonly ICategoryService _categoryService;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            IProductRepository productRepository
            )
        {
            _productService = productService;
            _categoryService = categoryService;
            _productRepository = productRepository;
        }

        public ActionResult Manage()
        {
            var categories = _categoryService.GetAllWithPrefixOnChildren();
            categories.Insert(0, new Category {Id = 0, CategoryName = "-- Tất cả nhóm --"});
            var model = new ProductManageViewModel()
            {
                CurrentPage = 1,
                PageSize = 10,
            };
            var totalCount = 0;
            model.Categories = categories;
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
                CategoryId = model.CategoryId
            }, ref totalCount);
            model.ItemCount = totalCount;
            return View("_ProductTable", model);
        }
        public ActionResult Edit(int id = 0)
        {
            var model = new ProductEditViewModel();
            model.Categories = _categoryService.GetAllWithTreeViewOrder();
            if (id != 0)
            {
                var product = _productService.GetWithCategoriesById(id);
                if (product != null)
                {
                    product.MappedCategoryIds = string.Join(",", product.Categories.Select(o => o.Id));
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

        [HttpPost]
        public ActionResult ProductListForOrder(int categoryId)
        {
            var count = 0;
            var model = _productService.GetList(new FilterParams
            {
                CategoryId = categoryId,
                PageNumber = 0
            }, ref count);
            return View("_ProductListForOrder", model);
        }
    }
}