using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;

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


        public void Delete(ProductCategory productCategory)
        {
            using (var context = new ETradeContext())
            {
                var deletedEntity = context.Entry(productCategory);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        //public void DeleteCategory(Category category)
        //{
        //    using (var context = new ETradeContext())
        //    {
                
                
        //        category.IsActive = false;
        //        var updatedEntity = context.Entry(entity);
        //        updatedEntity.State = EntityState.Modified;
        //        context.SaveChanges();
        //    }
        //}


    }
}
