using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URL_Shortener.Models;
using URL_Shortener.DTO;

namespace URL_Shortener.Controllers
{
    [ApiController]
    [Route("clickInfo")]
    public class ClickInfoController(AppDbContext db) : ControllerBase
    {
        private readonly AppDbContext _db = db;

        [HttpGet("{id:int}", Name = nameof(GetClickInfoByIdAsync))]
        public async Task<IActionResult> GetClickInfoByIdAsync(int id)
        {
            var clickInfo = await _db.ClickInfos.FindAsync(id);
            if (clickInfo == null)
                return NotFound();

            return Ok(clickInfo);
        }

        [HttpGet("by-short-url/{shortUrlId:int}")]
        public async Task<IActionResult> GetByShortUrlIdAsync(int shortUrlId)
        {
            var clickInfos = await _db.ClickInfos.Where(c => c.ShortUrlId == shortUrlId).ToListAsync();
            if (clickInfos.Count == 0)
                return NotFound();

            return Ok(clickInfos);
        }
    }
}
