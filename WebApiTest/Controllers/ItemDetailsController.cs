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
//    public class ItemDetailsController : ControllerBase
//    {
//        private readonly AppDbContext _context;

//        public ItemDetailsController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/ItemDetails
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<ItemDetail>>> GetItemDetails()
//        {
//          if (_context.ItemDetails == null)
//          {
//              return NotFound();
//          }
//            return await _context.ItemDetails.ToListAsync();
//        }

//        // GET: api/ItemDetails/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<ItemDetail>> GetItemDetail(int id)
//        {
//          if (_context.ItemDetails == null)
//          {
//              return NotFound();
//          }
//            var itemDetail = await _context.ItemDetails.FindAsync(id);

//            if (itemDetail == null)
//            {
//                return NotFound();
//            }

//            return itemDetail;
//        }

//        // PUT: api/ItemDetails/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutItemDetail(int id, ItemDetail itemDetail)
//        {
//            if (id != itemDetail.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(itemDetail).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ItemDetailExists(id))
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

//        // POST: api/ItemDetails
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<ItemDetail>> PostItemDetail(ItemDetail itemDetail)
//        {
//          if (_context.ItemDetails == null)
//          {
//              return Problem("Entity set 'AppDbContext.ItemDetails'  is null.");
//          }
//            _context.ItemDetails.Add(itemDetail);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetItemDetail", new { id = itemDetail.Id }, itemDetail);
//        }

//        // DELETE: api/ItemDetails/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteItemDetail(int id)
//        {
//            if (_context.ItemDetails == null)
//            {
//                return NotFound();
//            }
//            var itemDetail = await _context.ItemDetails.FindAsync(id);
//            if (itemDetail == null)
//            {
//                return NotFound();
//            }

//            _context.ItemDetails.Remove(itemDetail);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool ItemDetailExists(int id)
//        {
//            return (_context.ItemDetails?.Any(e => e.Id == id)).GetValueOrDefault();
//        }
//    }
//}
