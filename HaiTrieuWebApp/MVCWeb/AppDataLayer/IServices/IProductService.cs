using System.Collections.Generic;
using MVCWeb.AppDataLayer.Entities;

namespace MVCWeb.AppDataLayer.IServices
{
    public interface IProductService : IWebAppService
    {
        int Create(Product Product);
        bool UpdateProduct(Product Product);
        List<Product> GetList(FilterParams fp, ref int totalCount);
    }
}
