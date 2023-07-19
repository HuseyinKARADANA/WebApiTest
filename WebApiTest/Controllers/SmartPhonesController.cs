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
//    public class SmartPhonesController : ControllerBase
//    {
//        private readonly AppDbContext _context;

//        public SmartPhonesController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/SmartPhones
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<SmartPhone>>> GetSmartPhones()
//        {
//          if (_context.SmartPhones == null)
//          {
//              return NotFound();
//          }
//            return await _context.SmartPhones.ToListAsync();
//        }

//        // GET: api/SmartPhones/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<SmartPhone>> GetSmartPhone(int id)
//        {
//          if (_context.SmartPhones == null)
//          {
//              return NotFound();
//          }
//            var smartPhone = await _context.SmartPhones.FindAsync(id);

//            if (smartPhone == null)
//            {
//                return NotFound();
//            }

//            return smartPhone;
//        }

//        // PUT: api/SmartPhones/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutSmartPhone(int id, SmartPhone smartPhone)
//        {
//            if (id != smartPhone.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(smartPhone).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!SmartPhoneExists(id))
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

//        // POST: api/SmartPhones
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<SmartPhone>> PostSmartPhone(SmartPhone smartPhone)
//        {
//          if (_context.SmartPhones == null)
//          {
//              return Problem("Entity set 'AppDbContext.SmartPhones'  is null.");
//          }
//            _context.SmartPhones.Add(smartPhone);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetSmartPhone", new { id = smartPhone.Id }, smartPhone);
//        }

//        // DELETE: api/SmartPhones/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteSmartPhone(int id)
//        {
//            if (_context.SmartPhones == null)
//            {
//                return NotFound();
//            }
//            var smartPhone = await _context.SmartPhones.FindAsync(id);
//            if (smartPhone == null)
//            {
//                return NotFound();
//            }

//            _context.SmartPhones.Remove(smartPhone);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool SmartPhoneExists(int id)
//        {
//            return (_context.SmartPhones?.Any(e => e.Id == id)).GetValueOrDefault();
//        }
//    }
//}
