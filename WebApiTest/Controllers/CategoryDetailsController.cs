//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WebApiTest.Models;

//namespace WebApiTest.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CategoryDetailsController : ControllerBase
//    {
//        private readonly AppDbContext _context;

//        public CategoryDetailsController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/CategoryDetails
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<CategoryDetail>>> GetCategoryDetails()
//        {
//          if (_context.CategoryDetails == null)
//          {
//              return NotFound();
//          }
//            return await _context.CategoryDetails.ToListAsync();
//        }

//        // GET: api/CategoryDetails/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<CategoryDetail>> GetCategoryDetail(int id)
//        {
//          if (_context.CategoryDetails == null)
//          {
//              return NotFound();
//          }
//            var categoryDetail = await _context.CategoryDetails.FindAsync(id);

//            if (categoryDetail == null)
//            {
//                return NotFound();
//            }

//            return categoryDetail;
//        }

//        // PUT: api/CategoryDetails/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutCategoryDetail(int id, CategoryDetail categoryDetail)
//        {
//            if (id != categoryDetail.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(categoryDetail).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!CategoryDetailExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/CategoryDetails
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<CategoryDetail>> PostCategoryDetail(CategoryDetail categoryDetail)
//        {
//          if (_context.CategoryDetails == null)
//          {
//              return Problem("Entity set 'AppDbContext.CategoryDetails'  is null.");
//          }
//            _context.CategoryDetails.Add(categoryDetail);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetCategoryDetail", new { id = categoryDetail.Id }, categoryDetail);
//        }

//        // DELETE: api/CategoryDetails/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteCategoryDetail(int id)
//        {
//            if (_context.CategoryDetails == null)
//            {
//                return NotFound();
//            }
//            var categoryDetail = await _context.CategoryDetails.FindAsync(id);
//            if (categoryDetail == null)
//            {
//                return NotFound();
//            }

//            _context.CategoryDetails.Remove(categoryDetail);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool CategoryDetailExists(int id)
//        {
//            return (_context.CategoryDetails?.Any(e => e.Id == id)).GetValueOrDefault();
//        }
//    }
//}
