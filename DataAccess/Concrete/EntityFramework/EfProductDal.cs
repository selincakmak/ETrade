using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;

using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Entities.Response.Product;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, ETradeContext>, IProductDal
    {
        public IQueryable<Product> getAll()
        {
            using (var context = new ETradeContext())
            {
                return context.Set<Product>();

            }
        }

        public List<ProductCategory> getProductCategories(Expression<Func<ProductCategory, bool>> filter = null)
        {
            using (var context = new ETradeContext())
            {
                return filter == null
                    ? context.Set<ProductCategory>().ToList()
                    : context.Set<ProductCategory>().Where(filter).ToList();

            }
        }

    

    }
}
