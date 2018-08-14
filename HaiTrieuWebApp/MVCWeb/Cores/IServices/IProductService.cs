using System.Collections.Generic;
using MVCWeb.Cores.Entities;

namespace MVCWeb.Cores.IServices
{
    public interface IProductService : IWebAppService
    {
        int Create(Product product);
        bool UpdateProduct(Product product);
        List<Product> GetList(FilterParams fp, ref int totalCount);
    }
}
