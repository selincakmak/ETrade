using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;

using Entities.Concrete;
using Entities.Response.Category;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, ETradeContext>, ICategoryDal
    {
        public Category getCategoryByName(string name)
        {
            using (var context= new ETradeContext())
            {
                return context.Categories.Where(i => i.CategoryName == name).FirstOrDefault();
            }
        }

        public IEnumerable<CategoryModel> GetAllWithProductCount()
        {
            return GetList().Select(i => new CategoryModel()
            {
                CategoryId = i.CategoryId,
                CategoryName = i.CategoryName,
                Count = i.ProductCategories.Count()
                

            });
       
        }
    }
}
