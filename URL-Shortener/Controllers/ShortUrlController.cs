using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URL_Shortener.DTO;
using URL_Shortener.Models;
using HashidsNet;

namespace URL_Shortener.Controllers
{
    [ApiController]
    [Route("shortUrl")]
    public class ShortUrlController(AppDbContext db, Hashids hashids) : ControllerBase
    {
        private readonly AppDbContext _db = db;
        private readonly Hashids _hashids = hashids;

        [HttpGet("all")]
        public async Task<IActionResult> GetAll() =>
            Ok(await _db.ShortUrls.ToListAsync());

        [HttpGet("{id:int}", Name = nameof(GetShortUrlByIdAsync))]
        public async Task<IActionResult> GetShortUrlByIdAsync(int id)
        {
            var shortUrl = await _db.ShortUrls.FindAsync(id);
            if (shortUrl == null)
                return NotFound();

            return Ok(shortUrl);
        }

        [HttpGet("by-user-id/{userId:int}")]
        public async Task<IActionResult> GetByUserIdAsync(int userId)
        {
            var shortUrls = await _db.ShortUrls.Where(s => s.UserId == userId).ToListAsync();
            if (shortUrls.Count == 0)
                return NotFound();

            return Ok(shortUrls);
        }

        [HttpPost]
        public async Task<IActionResult> CreateShortUrlAsync(CreateShortUrlDto dto)
        {
            var shortUrl = new ShortUrl
            {
                OriginalUrl = dto.OriginalUrl,
                ExpirationDateTime = dto.ExpirationDateTime,
                ClickLimit = dto.ClickLimit
            };
            await _db.ShortUrls.AddAsync(shortUrl);
            await _db.SaveChangesAsync();
            shortUrl.ShortCode = _hashids.Encode(shortUrl.Id);
            await _db.SaveChangesAsync();

            return CreatedAtRoute(nameof(GetShortUrlByIdAsync), new { id = shortUrl.Id }, shortUrl);
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> UpdateShortUrlAsync(int id, UpdateShortUrlDto dto)
        {
            var shortUrl = await _db.ShortUrls.FindAsync(id);
            if (shortUrl == null)
                return NotFound("Short URL not found");

            if (!string.IsNullOrEmpty(dto.OriginalUrl))
                shortUrl.OriginalUrl = dto.OriginalUrl;
            if (dto.ExpirationDateTime.HasValue)
                shortUrl.ExpirationDateTime = dto.ExpirationDateTime;
            if (dto.ClickLimit.HasValue)
                shortUrl.ClickLimit = dto.ClickLimit.Value;
            if (dto.ResetExpirationDateTime)
                shortUrl.ExpirationDateTime = null;

            await _db.SaveChangesAsync();

            return Ok(shortUrl);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteShortUrlByIdAsync(int id)
        {
            var shortUrl = await _db.ShortUrls.FindAsync(id);
            if (shortUrl == null)
                return NotFound();

            _db.ShortUrls.Remove(shortUrl);

            var clickInfos = _db.ClickInfos.Where(c => c.ShortUrlId == id);
            if (clickInfos != null)
                _db.ClickInfos.RemoveRange(clickInfos);

            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}