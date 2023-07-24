using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntityLayer.DTOs;
using EntityLayer.Concrete;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //[Authorize]
        [HttpGet]
        public List<GetCategoryDTO> GetAllCategories()
        {
            List<Category> categories = _categoryService.GetListAll();

            List<GetCategoryDTO> categoryDTOs = categories.Select(category => new GetCategoryDTO
            {
                Name = category.Name
            }).ToList();

            return categoryDTOs;
        }

        //[Authorize]
        [HttpGet("get")]
        public Category GetCategory(string name)
        {
            var category = _categoryService.GetCategoryByName(name);

            if (category == null)
            {
                throw new Exception("NotFound");
            }

            return category;
        }

        //[Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategory(GetCategoryDTO dto)
        {

            if (ModelState.IsValid)
            {
                var categoryToUpdate = _categoryService.GetCategoryByName(dto.Name);
                if (categoryToUpdate == null)
                {
                    return NotFound();
                }

                categoryToUpdate.Name = dto.Name;

                _categoryService.Update(categoryToUpdate);

                return Ok("Category successfully updated");
            }
            else
            {
                return BadRequest("Invalid data provided.");
            }
        }

        //[Authorize]
        [HttpPost("addcategory")]
        public async Task<ActionResult<GetCategoryDTO>> AddCategory(GetCategoryDTO category)
        {
            _categoryService.Insert(new Category()
            {
                Name = category.Name
            });

            return category;
        }

        //[Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCategory(string categoryName)
        {
            var category = _categoryService.GetCategoryByName(categoryName);
            if (category == null)
            {
                return NotFound();
            }

            _categoryService.Delete(category);

            return Ok("Category deleted successfully");
        }

        private bool CategoryExists(string name)
        {
            var category = _categoryService.GetCategoryByName(name);

            return category != null;
        }
    }
}
