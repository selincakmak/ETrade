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


        public ProductManager(ICategoryDal categoryDal,IProductDal productDal, IProductImageDal imageDal, IProductAttributeDal attributeDal)
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
            response.Categories =cat;
            return new SuccessDataResult<ProductDetailsModel>(response);
        }

        public IDataResult<IEnumerable<Product>> GetAll()
        {
            return new SuccessDataResult<IEnumerable<Product>>(_productDal.GetList().Where(i => i.IsApproved == true));
        }

        public IDataResult<IEnumerable<Product>> GetAllByCategory(int categoryId)
        {
            return new SuccessDataResult<IEnumerable<Product>>(_productDal.GetList(filter: p => p.CategoryId == categoryId));
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        
    }
}
