using System.Web.Mvc;
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

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public ActionResult Manage()
        {
            var model = new CategoryManageViewModel()
            {
                Categories = _categoryService.GetAllWithTreeViewOrder(),
                ParentCategories = _categoryService.GetParentListWithChildren()
            };
            return View("_Manage", model);
        }
    }
}