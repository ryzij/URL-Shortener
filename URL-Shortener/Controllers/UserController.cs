using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URL_Shortener.Models;
using URL_Shortener.DTO;

namespace URL_Shortener.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController(AppDbContext db) : ControllerBase
    {
        private readonly AppDbContext _db = db;

        [HttpGet("email")]
        public async Task<IActionResult> GetUserByEmailAsync(string email)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }
    }
}
