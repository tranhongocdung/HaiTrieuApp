using System.Collections.Generic;
using MVCWeb.Cores.Entities;

namespace MVCWeb.Cores.IServices
{
    public interface IProductService : IWebAppService
    {
        int Create(Product Product);
        bool UpdateProduct(Product Product);
        List<Product> GetList(FilterParams fp, ref int totalCount);
    }
}
