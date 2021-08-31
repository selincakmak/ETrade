using Business.Abstract;
using Business.Constant;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.Response.Product;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal; //iş katmanından veri erişim katmanına çağrı için
        private IProductImageDal _imageDal;
        private IProductAttributeDal _attributeDal;
        private readonly ICategoryDal _categoryDal;


        public ProductManager(ICategoryDal categoryDal, IProductDal productDal, IProductImageDal imageDal, IProductAttributeDal attributeDal)
        {
            _productDal = productDal;
            _imageDal = imageDal;
            _attributeDal = attributeDal;
            _categoryDal = categoryDal;
        }

        public IResult Add(Product product)
        {
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }


        public IDataResult<ProductDetailsModel> GetById(int productId)
        {
            ProductDetailsModel response = new ProductDetailsModel();
            var product = _productDal.Get(filter: p => p.ProductId == productId);
            response.Product = product;
            response.ProductImages = _imageDal.GetList(x => x.ProductId == productId).ToList();
            response.ProductAttributes = _attributeDal.GetList(x => x.ProductId == productId).ToList();
            var productCategory = _productDal.getProductCategories(x => x.ProductId == productId);
            List<string> cat = new();
            foreach (var item in productCategory)
            {
                var category = _categoryDal.Get(x => x.CategoryId == item.CategoryId).CategoryName;
                cat.Add(category);

            }
            response.Categories = cat;
            return new SuccessDataResult<ProductDetailsModel>(response);
        }


        public IDataResult<IEnumerable<Product>> GetAll(string categoryName)
        {
            if (!string.IsNullOrEmpty(categoryName))
            {
                List<Product> response = new List<Product>();
                var category = _categoryDal.Get(x => x.CategoryName == categoryName);
                var productCategory = _productDal.getProductCategories(x => x.CategoryId == category.CategoryId);
                foreach (var item in productCategory)
                {
                    var model = _productDal.Get(x => x.ProductId == item.ProductId);
                    response.Add(model);
                }
                return new SuccessDataResult<IEnumerable<Product>>(response.AsQueryable());
            }


            return new SuccessDataResult<IEnumerable<Product>>(_productDal.GetList());
        }



        //public IDataResult<IEnumerable<Product>> GetAllByCategory(int categoryId)
        //{
        //    return new SuccessDataResult<IEnumerable<Product>>(_productDal.GetList());
        //}

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }


        public IResult DeleteProduct(int ProductId)
        {
            var entity = _productDal.Get(filter: i => i.ProductId == ProductId);
            entity.IsActive = false;
            var entity2 = _imageDal.Get(filter: i => i.ProductId == ProductId);
            entity2.IsActive = false;
            var entity3 = _attributeDal.Get(filter: i => i.ProductId == ProductId);
            entity3.IsActive = false;

            _productDal.Update(entity);
            _imageDal.Update(entity2);
            _attributeDal.Update(entity3);
            return new SuccessResult(Messages.ProductDeleted);

        }

    }



    }


