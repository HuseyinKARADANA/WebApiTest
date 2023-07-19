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
//    public class FavoriteItemUsersController : ControllerBase
//    {
//        private readonly AppDbContext _context;

//        public FavoriteItemUsersController(AppDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/FavoriteItemUsers
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<FavoriteItemUser>>> GetFavoriteItemUsers()
//        {
//          if (_context.FavoriteItemUsers == null)
//          {
//              return NotFound();
//          }
//            return await _context.FavoriteItemUsers.ToListAsync();
//        }

//        // GET: api/FavoriteItemUsers/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<FavoriteItemUser>> GetFavoriteItemUser(int id)
//        {
//          if (_context.FavoriteItemUsers == null)
//          {
//              return NotFound();
//          }
//            var favoriteItemUser = await _context.FavoriteItemUsers.FindAsync(id);

//            if (favoriteItemUser == null)
//            {
//                return NotFound();
//            }

//            return favoriteItemUser;
//        }

//        // PUT: api/FavoriteItemUsers/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutFavoriteItemUser(int id, FavoriteItemUser favoriteItemUser)
//        {
//            if (id != favoriteItemUser.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(favoriteItemUser).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!FavoriteItemUserExists(id))
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

//        // POST: api/FavoriteItemUsers
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<FavoriteItemUser>> PostFavoriteItemUser(FavoriteItemUser favoriteItemUser)
//        {
//          if (_context.FavoriteItemUsers == null)
//          {
//              return Problem("Entity set 'AppDbContext.FavoriteItemUsers'  is null.");
//          }
//            _context.FavoriteItemUsers.Add(favoriteItemUser);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetFavoriteItemUser", new { id = favoriteItemUser.Id }, favoriteItemUser);
//        }

//        // DELETE: api/FavoriteItemUsers/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteFavoriteItemUser(int id)
//        {
//            if (_context.FavoriteItemUsers == null)
//            {
//                return NotFound();
//            }
//            var favoriteItemUser = await _context.FavoriteItemUsers.FindAsync(id);
//            if (favoriteItemUser == null)
//            {
//                return NotFound();
//            }

//            _context.FavoriteItemUsers.Remove(favoriteItemUser);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool FavoriteItemUserExists(int id)
//        {
//            return (_context.FavoriteItemUsers?.Any(e => e.Id == id)).GetValueOrDefault();
//        }
//    }
//}
