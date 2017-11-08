using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using MVCWeb.AppDataLayer.Entities;
using MVCWeb.AppDataLayer.IRepositories;
using MVCWeb.AppDataLayer.IServices;

namespace MVCWeb.AppDataLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(
            IProductRepository productRepository
            )
        {
            _productRepository = productRepository;
        }

        public int Create(Product product)
        {
            _productRepository.Insert(product);
            return product.Id;
        }
        public bool UpdateProduct(Product product)
        {
            var currentProduct = _productRepository.GetById(product.Id);
            if (currentProduct == null) return false;
            currentProduct.ProductName = product.ProductName;
            _productRepository.Update(product);
            return true;
        }
        public List<Product> GetList(FilterParams fp, ref int totalCount)
        {
            var list = _productRepository.TableNoTracking;
            if (!string.IsNullOrEmpty(fp.Keyword))
            {
                list = list.Where(o => o.ProductName.Contains(fp.Keyword));
            }
            totalCount = list.Count();
            list = list.OrderBy(fp.SortField + (fp.SortASC ? " ASC" : " DESC"));
            if (fp.PageNumber == 0) return list.ToList();
            var skip = (fp.PageNumber - 1) * fp.PageSize;
            var take = fp.PageSize;
            list = list.Skip(skip).Take(take);
            return list.ToList();
        }
    }
}