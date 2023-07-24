using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EntityLayer.DTOs;
using EntityLayer.Concrete;
using BusinessLayer.Abstract;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoriesController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        //[Authorize]
        [HttpGet]
        public List<GetSubCategoryDTO> GetAllSubCategories()
        {
            List<SubCategory> subCategories = _subCategoryService.GetListAll();

            List<GetSubCategoryDTO> subCategoryDTOs = subCategories.Select(subCategory => new GetSubCategoryDTO
            {
                Id = subCategory.Id,
                CategoryId = subCategory.CategoryId,
                Name = subCategory.Name
            }).ToList();

            return subCategoryDTOs;
        }

        //[Authorize]
        [HttpGet("getByName/{name:alpha}")]
        public SubCategory GetSubCategory(string name)
        {
            var subCategory = _subCategoryService.GetSubCategoryByName(name);

            if (subCategory == null)
            {
                throw new Exception("NotFound");
            }

            return subCategory;
        }

        //[Authorize]
        [HttpGet("getById/{id:int}")]
        public SubCategory GetSubCategory(int id)
        {
            var subCategory = _subCategoryService.GetElementById(id);

            if (subCategory == null)    
            {
                throw new Exception("NotFound");
            }

            return subCategory;
        }

        //[Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateSubCategory(GetSubCategoryDTO dto)
        {

            if (ModelState.IsValid)
            {
                var subCategoryToUpdate = _subCategoryService.GetSubCategoryByName(dto.Name);
                if (subCategoryToUpdate == null)
                {
                    return NotFound();
                }

                subCategoryToUpdate.CategoryId = dto.CategoryId;
                subCategoryToUpdate.Name = dto.Name;

                _subCategoryService.Update(subCategoryToUpdate);

                return Ok("Sub Category successfully updated");
            }
            else
            {
                return BadRequest("Invalid data provided.");
            }
        }

        //[Authorize]
        [HttpPost("addsubcategory")]
        public async Task<ActionResult<AddSubCategoryDTO>> AddSubCategory(AddSubCategoryDTO subCategory)
        {
            _subCategoryService.Insert(new SubCategory()
            {
                CategoryId = subCategory.CategoryId,
                Name = subCategory.Name
            });

            return subCategory;
        }

        //[Authorize]
        [HttpDelete("deleteByName/{name:alpha}")]
        public async Task<IActionResult> DeleteSubCategory(string name)
        {
            var subCategory = _subCategoryService.GetSubCategoryByName(name);
            if (subCategory == null)
            {
                return NotFound();
            }

            _subCategoryService.Delete(subCategory);

            return Ok("Sub Category deleted successfully");
        }

        //[Authorize]
        [HttpDelete("deleteById/{id:int}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var subCategory = _subCategoryService.GetElementById(id);
            if (subCategory == null)
            {
                return NotFound();
            }

            _subCategoryService.Delete(subCategory);

            return Ok("Sub Category deleted successfully");
        }
        private bool SubCategoryExists(string name)
        {
            var subCategory = _subCategoryService.GetSubCategoryByName(name);

            return subCategory != null;
        }

        private bool SubCategoryExists(int id)
        {
            var subCategory = _subCategoryService.GetElementById(id);

            return subCategory != null;
        }
    }
}
