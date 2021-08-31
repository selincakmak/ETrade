using System;
using System.Collections.Generic;
using System.Text;
using Core.DataAccess;
using Core.Entities;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Response.Category;

namespace DataAccess.Abstract
{
   public interface ICategoryDal :IEntityRepository<Category>
    {
        //sadece kategori için kullanılacak metotlar yazılır
        //Category getCategoryByName(string name); 

        IEnumerable<CategoryModel> GetAllWithProductCount();

       void Delete(ProductCategory productCategory);

       // void DeleteCategory(Category category);
    }
}
