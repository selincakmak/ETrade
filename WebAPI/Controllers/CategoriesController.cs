 using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        
        private ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet(template: "getall")]

        public IActionResult GetList()
        {
            var result = _categoryService.GetAll();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet(template: "getlistbycategoryname")]

        public IActionResult GetListByCategoryName(string categoryName)
        {
            var result = _categoryService.GetAllByCategoryName(categoryName);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet(template: "getlistbycategorycount")]

        public IActionResult GetListByCategoryCount()
        {
            var result = _categoryService.GetCategoryCount();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost(template: "add")]

        public IActionResult Add(Category category)
        {
            var result = _categoryService.Add(category);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        //ilişkileri siliyo
        [HttpPost(template: "delete")]

        public IActionResult Remove(int productId, int categoryId)
        {
            var result = _categoryService.Remove(productId, categoryId);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPost(template: "deleteCategory")]

        public IActionResult deleteCategory( int categoryId)
        {
            var result = _categoryService.DeleteCategory(categoryId);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpPost(template: "update")]
        public IActionResult Update(Category category)
        {
            var result = _categoryService.Update(category);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet(template: "getbyid")]

        public IActionResult GetById(int categoryId)
        {
            var result = _categoryService.GetById(categoryId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

    }
}
