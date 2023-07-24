using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EntityLayer.DTOs;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        //[Authorize]
        [HttpGet]
        public List<GetItemDTO> GetAllItems()
        {
            List<Item> items = _itemService.GetListAll();

            List<GetItemDTO> itemDTOs = items.Select(item => new GetItemDTO
            {
                Id = item.Id,
                CategoryId = item.CategoryId,
                SubCategoryId = item.SubCategoryId,
                CategoryDetailId = item.CategoryDetailId,
                UserId = item.UserId,
                FavoriteCount = item.FavoriteCount,
                Title = item.Title,
                Brand = item.Brand,
                Price = item.Price,
                Image = item.Image,
                Description = item.Description,
            }).ToList();

            return itemDTOs;
        }

        //[Authorize]
        [HttpGet("getById/{id:int}")]
        public Item GetItem(int id)
        {
            var item = _itemService.GetElementById(id);

            if (item == null)
            {
                throw new Exception("NotFound");
            }

            return item;
        }

        //[Authorize]
        [HttpGet("getByName/{name:alpha}")]
        public Item GetItem(string brand)
        {
            var item = _itemService.GetItemByBrand(brand);

            if (item == null)
            {
                throw new Exception("NotFound");
            }

            return item;
        }

        //[Authorize]
        [HttpPost("additem")]
        public async Task<ActionResult<AddItemDTO>> AddItem(AddItemDTO item)
        {
            _itemService.Insert(new Item()
            {
                CategoryId = item.CategoryId,
                SubCategoryId = item.SubCategoryId,
                CategoryDetailId = item.CategoryDetailId,
                UserId = item.UserId,
                FavoriteCount = item.FavoriteCount,
                Title = item.Title,
                Brand= item.Brand,
                Price = item.Price,
                Image = item.Image,
                Description = item.Description,
            });

            return item;
        }

        //[Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateItem(GetItemDTO dto)
        {

            if (ModelState.IsValid)
            {
                var itemToUpdate = _itemService.GetElementById(dto.Id);
                if (itemToUpdate == null)
                {
                    return NotFound();
                }

                itemToUpdate.CategoryId = dto.CategoryId;
                itemToUpdate.SubCategoryId = dto.SubCategoryId;
                itemToUpdate.CategoryDetailId = dto.CategoryDetailId;
                itemToUpdate.UserId = dto.UserId;
                itemToUpdate.FavoriteCount = dto.FavoriteCount;
                itemToUpdate.Title = dto.Title;
                itemToUpdate.Brand = dto.Brand;
                itemToUpdate.Price = dto.Price;
                itemToUpdate.Image = dto.Image;
                itemToUpdate.Description = dto.Description;
 

                _itemService.Update(itemToUpdate);

                return Ok("Item successfully updated");
            }
            else
            {
                return BadRequest("Invalid data provided.");
            }
        }

        //[Authorize]
        [HttpDelete("deleteById/{id:int}")]
        public async Task<IActionResult> DeleteItem(int itemId)
        {
            var item = _itemService.GetElementById(itemId);
            if (item == null)
            {
                return NotFound();
            }

            _itemService.Delete(item);

            return Ok("Item deleted successfully");
        }

        //[Authorize]
        [HttpDelete("deleteByBrand/{brand:alpha}")]
        public async Task<IActionResult> DeleteItem(string brand)
        {
            var item = _itemService.GetItemByBrand(brand);
            if (item == null)
            {
                return NotFound();
            }

            _itemService.Delete(item);

            return Ok("Item deleted successfully");
        }
        private bool ItemExists(int id)
        {
            var item = _itemService.GetElementById(id);

            return item != null;
        }

        private bool ItemExists(string brand)
        {
            var item = _itemService.GetItemByBrand(brand);

            return item != null;
        }
    }
}
