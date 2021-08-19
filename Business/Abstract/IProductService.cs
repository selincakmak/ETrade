using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using  Entities.Response.Product;

namespace Business.Abstract
{
    public interface IProductService
    {

        IDataResult<ProductDetailsModel> GetById(int productId);
        IDataResult<IEnumerable<Product>> GetAll();
        IDataResult<IEnumerable<Product>> GetAllByCategory(int categoryId);

        IResult Add(Product product);
        IResult Delete(Product product);
        IResult Update(Product product);
    }
}
