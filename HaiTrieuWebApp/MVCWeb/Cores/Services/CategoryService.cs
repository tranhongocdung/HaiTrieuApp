using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using MVCWeb.Cores.Entities;
using MVCWeb.Cores.IRepositories;
using MVCWeb.Cores.IServices;

namespace MVCWeb.Cores.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(
            ICategoryRepository categoryRepository
            )
        {
            _categoryRepository = categoryRepository;
        }


        public int Create(Category category)
        {
            _categoryRepository.Insert(category);
            return category.Id;
        }

        public bool UpdateCategory(Category category)
        {
            var currentCategory = _categoryRepository.GetById(category.Id);
            if (currentCategory == null) return false;
            currentCategory.CategoryName = category.CategoryName;
            currentCategory.ParentId = category.ParentId;
            _categoryRepository.Update(category);
            return true;
        }

        public List<Category> GetList(FilterParams fp, ref int totalCount)
        {
            var list = _categoryRepository.TableNoTracking;
            if (!string.IsNullOrEmpty(fp.Keyword))
            {
                list = list.Where(o => o.CategoryName.Contains(fp.Keyword));
            }
            totalCount = list.Count();
            list = list.OrderBy(fp.SortField + (fp.SortASC ? " ASC" : " DESC"));
            if (fp.PageNumber == 0) return list.ToList();
            var skip = (fp.PageNumber - 1) * fp.PageSize;
            var take = fp.PageSize;
            list = list.Skip(skip).Take(take);
            return list.ToList();
        }

        public List<Category> GetAllWithPrefixOnChildren()
        {
            var list = _categoryRepository.TableNoTracking.ToList();
            list.ForEach(category =>
            {
                if (category.ParentId != null)
                {
                    category.CategoryName = "-- " + category.CategoryName;
                }
            });
            return list.OrderBy(o => o.ParentId != null).ThenBy(o => o.ParentId).ToList();
        }

        public Category GetWithChildren(int categoryId)
        {
            return _categoryRepository.TableNoTracking.Include(o => o.ChildCategories).FirstOrDefault(o => o.Id == categoryId);
        }

        public List<Category> GetParentListWithChildren()
        {
            return
                _categoryRepository.TableNoTracking.Include(o => o.ChildCategories)
                    .Where(o => o.ParentId == null)
                    .ToList();
        }
    }
}