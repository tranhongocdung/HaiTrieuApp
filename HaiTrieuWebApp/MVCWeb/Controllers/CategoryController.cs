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
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryService categoryService,
            ICategoryRepository categoryRepository)
        {
            _categoryService = categoryService;
            _categoryRepository = categoryRepository;
        }
        public ActionResult Manage()
        {
            var categoryEditViewModel = new CategoryEditViewModel
            {
                Category = new Category
                {
                    Id = 0
                },
                ParentCategories = _categoryService.GetParentListWithChildren()
            };
            categoryEditViewModel.ParentCategories.Insert(0, _categoryService.RootCategory());
            var model = new CategoryManageViewModel()
            {
                Categories = _categoryService.GetAllWithTreeViewOrder(),
                CategoryEditViewModel = categoryEditViewModel
            };
            
            return View("_Manage", model);
        }

        public ActionResult Edit(int id = 0)
        {
            var parentCategories = _categoryService.GetParentListWithChildren();
            var model = new CategoryEditViewModel();
            if (id != 0)
            {
                var category = _categoryService.GetWithChildren(id);
                if (category != null)
                {
                    model.Category = category;
                    if (category.ParentId == null)
                    {
                        model.ParentCategories = new List<Category> {_categoryService.RootCategory()};
                        model.IsParentCategoryDisabled = true;
                        if (category.ChildCategories.Any())
                        {
                            model.IsDeletionDisabled = true;
                        }
                    }
                    else
                    {
                        model.ParentCategories = parentCategories;
                    }
                }
            }
            else
            {
                parentCategories.Insert(0, _categoryService.RootCategory());
                model.Category = new Category
                {
                    Id = 0
                };
                model.ParentCategories = parentCategories;
            }
            return View("_Edit", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(CategoryEditViewModel model)
        {
            /*if (ModelState.IsValid)
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
            }*/
            return Json(new ReturnData { Success = false, Message = "Lỗi!" });
        }
    }
}