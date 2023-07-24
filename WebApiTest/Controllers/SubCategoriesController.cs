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
                CategoryId = subCategory.CategoryId,
                Name = subCategory.Name
            }).ToList();

            return subCategoryDTOs;
        }

        //[Authorize]
        [HttpGet("get")]
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
        public async Task<ActionResult<GetSubCategoryDTO>> AddSubCategory(GetSubCategoryDTO subCategory)
        {
            _subCategoryService.Insert(new SubCategory()
            {
                CategoryId = subCategory.CategoryId,
                Name = subCategory.Name
            });

            return subCategory;
        }

        //[Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteSubCategory(string subCategoryName)
        {
            var subCategory = _subCategoryService.GetSubCategoryByName(subCategoryName);
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
    }
}
