using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Response.Category;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<IEnumerable<Category>> GetAll();
        IDataResult<IEnumerable<Category>> GetAllByCategoryName(string categoryName);
        IDataResult<List<CategoryModel>> GetCategoryCount();
    }
}
