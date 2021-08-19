using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal:IEntityRepository<Product>
    {
        List<ProductCategory> getProductCategories(Expression<Func<ProductCategory, bool>> filter = null);
        IQueryable<Product> getAll();
    }
}
