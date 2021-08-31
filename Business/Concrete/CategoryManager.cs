using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constant;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Response;
using Entities.Response.Category;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {

        private readonly ICategoryDal _categoryDal;
        private readonly IProductDal _productDal;

        public CategoryManager(ICategoryDal categoryDal, IProductDal productDal)
        {
            _categoryDal = categoryDal;
            _productDal = productDal;
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
                var productCategory = _productDal.getProductCategories(x => x.CategoryId == item.CategoryId);
                response.Add(new CategoryModel()
                {
                    CategoryId = item.CategoryId,
                    CategoryName = item.CategoryName,
                    Count = productCategory.Count

                });

            }


            return new SuccessDataResult<List<CategoryModel>>(response);
        }

        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<AdminEditCategoryModel> GetById(int categoryId)
        {
            AdminEditCategoryModel response = new AdminEditCategoryModel();
            var entity = _categoryDal.Get(filter: p => p.CategoryId == categoryId);
            response.CategoryId = entity.CategoryId;
            response.CategoryName = entity.CategoryName;
            var product = _productDal.getProductCategories(filter: p=> p.CategoryId==categoryId);
            var model = new List<AdminEditCategoryProduct> ();
            foreach (var item in product)
            {
                var pro = _productDal.Get(filter: p => p.ProductId == item.ProductId);
                model.Add(new AdminEditCategoryProduct {

                ProductId = pro.ProductId,
                ProductName = pro.ProductName,
                Image = pro.Image,
                IsApproved = pro.IsApproved.Value,
                IsFeatured = pro.IsFeatured.Value,
                IsHome = pro.IsHome.Value
            });
            }
            response.Products = model;
            return new SuccessDataResult<AdminEditCategoryModel>(response);

        }

        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Remove(int ProductId, int CategoryId)
        {
           var entity= _productDal.getProductCategories(filter: i => i.ProductId == ProductId && i.CategoryId == CategoryId).FirstOrDefault();
         
            _categoryDal.Delete(entity);
            return new SuccessResult(Messages.ProductDeleted);

        }

        public IResult DeleteCategory( int CategoryId)
        {
            var entity = _categoryDal.Get(filter: i=>i.CategoryId == CategoryId);
            entity.IsActive = false;
            _categoryDal.Update(entity);
            return new SuccessResult(Messages.ProductDeleted);

        }


    }
}

