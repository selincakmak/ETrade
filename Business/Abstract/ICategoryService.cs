using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Response;
using Entities.Response.Category;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<IEnumerable<Category>> GetAll();
        IDataResult<IEnumerable<Category>> GetAllByCategoryName(string categoryName);
        IDataResult<List<CategoryModel>> GetCategoryCount();
        public IDataResult<AdminEditCategoryModel> GetById(int categoryId);

        IResult Add(Category category);
        IResult Update(Category category);
       
        IResult Remove(int ProductId, int CategoryId);
        IResult DeleteCategory(int CategoryId);


    }
}
