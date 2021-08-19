using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Response.Category;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {

        private readonly ICategoryDal _categoryDal;
        private readonly IProductDal _product;

        public CategoryManager(ICategoryDal categoryDal, IProductDal product)
        {
            _categoryDal = categoryDal;
            _product = product;
        }
        public IDataResult<IEnumerable<Category>> GetAll()
        {
            return new SuccessDataResult<IEnumerable<Category>>(_categoryDal.GetList());
        }

        IDataResult<IEnumerable<Category>> ICategoryService.GetAllByCategoryName(string categoryName)
        {
            return new SuccessDataResult<IEnumerable<Category>>(_categoryDal.GetList(filter: p => p.CategoryName == categoryName));
        }

        public IDataResult<List<CategoryModel>> GetCategoryCount()
        {
            List<CategoryModel> response = new List<CategoryModel>();

            var model = _categoryDal.GetList();
            foreach (var item in model)
            {
                var productCategory = _product.getProductCategories(x => x.CategoryId == item.CategoryId);
                response.Add(new CategoryModel()
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    Count = productCategory.Count

                });

            }


            return new SuccessDataResult<List<CategoryModel>>(response);
        }
    }
}

