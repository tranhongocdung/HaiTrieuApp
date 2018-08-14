using System.Collections.Generic;
using MVCWeb.Cores.Entities;

namespace MVCWeb.Cores.IServices
{
    public interface ICategoryService : IWebAppService
    {
        int Create(Category category);
        bool UpdateCategory(Category category);
        List<Category> GetList(FilterParams fp, ref int totalCount);
        List<Category> GetAllWithTree();
        Category GetWithChildren(int categoryId);
    }
}
