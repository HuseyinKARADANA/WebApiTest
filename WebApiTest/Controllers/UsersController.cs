using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Humanizer;
using System.Security.Cryptography;

namespace WebApiTest.Controllers
{
    [Route("api/[controller]")]  //api/Users
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;//loglama yaptık
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration; //configuraiton eklendi 

        public UsersController(IUserService userService, ILogger<UsersController> logger, IConfiguration configuration)
        {
            _userService = userService;
            _logger = logger;
            _configuration = configuration;

        }

        [Authorize]
        [HttpGet]
        public List<User> GetAllUsers()
        {
            return _userService.GetListAll();
        }

        [Authorize]
        [HttpPost("{id:int}")]
        public User GetUser([FromBody]int id)
        {
          var user = _userService.GetElementById(id);

          if (user == null)
          {
              throw new Exception("NotFound");
          }

            return user;
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> LogOut ()
        {
            HttpContext.Response.Cookies.Delete("access_token");
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("Signed out successfully");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO dto)
        {
            if(_userService.Login(dto.Email,dto.Password))
            {
                var token = GenerateAccessToken(dto);

                // Set the access token as a cookie in the response
                HttpContext.Response.Cookies.Append("access_token", token, new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddHours(3),
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                return Ok(new { message = "Login successful." });
            }

            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, dto.ToString()),
            //    new Claim(ClaimTypes.Role, "User")
            //};

            //var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //var authProperties = new AuthenticationProperties();
            //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProperties);

            //return dto;

            return NotFound();
        }

        [NonAction]
        private static string Generate128BitKey()
        {
            // Generate 16 bytes (128 bits) of random data
            byte[] randomBytes = new byte[16];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            // Convert the random bytes to a hexadecimal string
            string hexKey = BitConverter.ToString(randomBytes).Replace("-", "").ToLower();

            return hexKey;
        }
 

        [NonAction]
        private string GenerateAccessToken(LoginDTO dto)
        {
            // Replace this with your actual token generation login
            string coded = Generate128BitKey();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(coded));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
            new Claim(ClaimTypes.Name, dto.ToString()),
            new Claim(ClaimTypes.Role, "User")
        };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //[HttpGet("ValidateLogin")]
        //public bool ValidateLogin(string token)
        //{
        //    var signinKey = Generate128BitKey();
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey));
        //    try
        //    {
        //        JwtSecurityTokenHandler handler = new();
        //        handler.ValidateToken(token, new TokenValidationParameters()
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = securityKey,
        //            ValidateLifetime = true,
        //            ValidateAudience = false,
        //            ValidateIssuer = false,
        //        }, out SecurityToken validatedToken);
        //        var jwtToken = (JwtSecurityToken)validatedToken;
        //        var claims = jwtToken.Claims.ToList();
        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<RegisterDTO>> Register(RegisterDTO user)
        {
            _userService.Insert(new User()
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Gender = user.Gender,
                BirthDate = user.BirthDate,
                PhoneNumber = user.PhoneNumber,
                RegisterDate = System.DateTime.Now,
                UserName = user.UserName,
                Password = user.Password
            });

            return user;
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var user = _userService.GetElementByUsername(username);
            if (user == null)
            {
                return NotFound();
            }

            _userService.Delete(user);

            return Ok("User deleted successfully");
        }

        //[Authorize]
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(int id, User user)
        //{
        //    if (id != user.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(user).State = EntityState.Modified;
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}
        
    }
}
